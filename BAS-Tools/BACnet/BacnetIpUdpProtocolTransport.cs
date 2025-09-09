using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using MainApp;

namespace System.IO.BACnet
{
    public class BacnetIpUdpProtocolTransport : IBacnetTransport
    {
        private UdpClient m_client;
        private Thread m_receive_thread;
        private int m_port;
        private string m_local_endpoint_ip;
        private bool m_dont_fragment;
        private volatile bool _is_running;

        public event MessageRecievedHandler MessageRecieved;
        public BacnetAddressTypes Type { get { return BacnetAddressTypes.IP; } }
        public int HeaderLength { get { return 4; } } // BVLC header length
        public BacnetMaxAdpu MaxAdpuLength { get { return BacnetMaxAdpu.MAX_APDU1476; } }
        public int MaxBufferLength { get { return 1472; } }
        public byte MaxInfoFrames { get; set; } = 0xFF;

        public BacnetIpUdpProtocolTransport(int port, bool dont_fragment = false, bool is_foreign_device = false, int max_payload = 1472, string local_endpoint_ip = "")
        {
            m_port = port;
            m_dont_fragment = dont_fragment;
            m_local_endpoint_ip = local_endpoint_ip;
        }

        private void ReceiveThread()
        {
            while (_is_running)
            {
                try
                {
                    IPEndPoint remote_ep = new IPEndPoint(IPAddress.Any, 0);
                    byte[] buffer = m_client.Receive(ref remote_ep);
                    if (buffer != null && buffer.Length > 0)
                    {
                        MessageRecieved?.Invoke(this, buffer, 0, buffer.Length, new BacnetAddress(BacnetAddressTypes.IP, (ushort)remote_ep.Port, remote_ep.Address.GetAddressBytes()));
                    }
                }
                catch (ObjectDisposedException) { return; }
                catch (SocketException se)
                {
                    if (_is_running) // Only log if we're not shutting down
                    {
                        string msg = "A socket exception occurred in ReceiveThread: " + se.Message;
                        Trace.TraceWarning(msg);
                        GlobalLogger.Log($"--- TRANSPORT ERROR: {msg} ---");
                    }
                }
                catch (Exception ex)
                {
                    if (_is_running)
                    {
                        string msg = "An unhandled error occurred in ReceiveThread: " + ex.ToString();
                        Trace.TraceError(msg);
                        GlobalLogger.Log($"--- TRANSPORT ERROR: {msg} ---");
                    }
                }
            }
        }

        public void Start()
        {
            if (m_client != null) return;
            try
            {
                IPEndPoint local_ep = new IPEndPoint(string.IsNullOrEmpty(m_local_endpoint_ip) || m_local_endpoint_ip == "0.0.0.0" ? IPAddress.Any : IPAddress.Parse(m_local_endpoint_ip), m_port);
                m_client = new UdpClient();
                m_client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                m_client.Client.Bind(local_ep);
                m_client.EnableBroadcast = true;
                _is_running = true;

                if (m_dont_fragment) m_client.DontFragment = true;

                if (m_receive_thread == null)
                {
                    m_receive_thread = new Thread(ReceiveThread) { IsBackground = true, Name = "BACnet UDP Receive" };
                    m_receive_thread.Start();
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError("Error starting UDP transport: " + ex.Message);
                m_client = null;
            }
        }

        public void Stop()
        {
            if (m_client == null) return;
            _is_running = false;
            m_client.Close();
            if (m_receive_thread != null)
            {
                m_receive_thread.Join(1000);
                m_receive_thread = null;
            }
            m_client = null;
        }

        public int Send(byte[] buffer, int offset, int data_length, BacnetAddress address, bool broadcast, int timeout)
        {
            try
            {
                // Prepend the BVLC header to the NPDU/APDU payload
                int bvlc_length = 4;
                int full_length = data_length + bvlc_length;
                byte[] full_buffer = new byte[full_length];

                // BVLC Header
                full_buffer[0] = 0x81; // BACnet/IP
                full_buffer[1] = broadcast ? (byte)0x0B : (byte)0x0A; // Original-Broadcast-NPDU or Original-Unicast-NPDU
                full_buffer[2] = (byte)((full_length >> 8) & 0xFF);
                full_buffer[3] = (byte)(full_length & 0xFF);

                // Copy the NPDU/APDU payload
                Array.Copy(buffer, offset, full_buffer, bvlc_length, data_length);

                IPEndPoint ep = broadcast
                    ? new IPEndPoint(IPAddress.Broadcast, address.net)
                    : new IPEndPoint(new IPAddress(address.adr), address.net != 0 ? address.net : m_port);

                return m_client.Send(full_buffer, full_buffer.Length, ep);
            }
            catch (Exception ex)
            {
                Trace.TraceError("Error during send: " + ex.Message);
                return 0;
            }
        }

        public bool SendRegisterAsForeignDevice(IPEndPoint ep, short ttl)
        {
            // BVLC Type (1 byte) + BVLC Function (1 byte) + BVLC Length (2 bytes) + TTL (2 bytes) = 6 bytes.
            // The spec requires a 10-byte frame for this message.
            byte[] buffer = new byte[10];
            buffer[0] = 0x81; // BACnet/IP
            buffer[1] = 0x05; // Register-Foreign-Device
            buffer[2] = 0x00; // Length (High Byte)
            buffer[3] = 0x0A; // Length (Low Byte) = 10
            buffer[4] = (byte)(ttl >> 8);
            buffer[5] = (byte)(ttl & 0xFF);

            try
            {
                return m_client.Send(buffer, buffer.Length, ep) == buffer.Length;
            }
            catch (Exception ex)
            {
                Trace.TraceError("Error sending register as foreign device: " + ex.Message);
                return false;
            }
        }
        public bool SendRemoteWhois(byte[] buffer, IPEndPoint ep, int msg_length)
        {
            try
            {
                int bvlc_length = 4;
                int full_length = msg_length + bvlc_length;
                byte[] full_buffer = new byte[full_length];

                // BVLC Header for Distribute-Broadcast-To-Network
                full_buffer[0] = 0x81;
                full_buffer[1] = 0x09;
                full_buffer[2] = (byte)((full_length >> 8) & 0xFF);
                full_buffer[3] = (byte)(full_length & 0xFF);

                // Copy the NPDU/APDU payload
                Array.Copy(buffer, 0, full_buffer, bvlc_length, msg_length);

                return m_client.Send(full_buffer, full_buffer.Length, ep) == full_buffer.Length;
            }
            catch (Exception ex)
            {
                Trace.TraceError("Error sending remote whois: " + ex.Message);
                return false;
            }
        }
        public BacnetAddress GetBroadcastAddress()
        {
            return new BacnetAddress(BacnetAddressTypes.IP, (ushort)m_port, new byte[] { 255, 255, 255, 255 });
        }

        public bool WaitForAllTransmits(int timeout) { return true; }
        public void Dispose() { Stop(); }
    }
}

