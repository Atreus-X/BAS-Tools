using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace System.IO.BACnet
{
    public class BacnetIpUdpProtocolTransport : IBacnetTransport
    {
        private UdpClient m_client;
        private Thread m_receive_thread;
        private int m_port;
        private string m_local_endpoint_ip;
        private bool m_dont_fragment;

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
            while (m_client != null)
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
                catch (SocketException se) { Trace.TraceWarning("A socket exception occurred in ReceiveThread: " + se.Message); }
                catch (Exception ex) { Trace.TraceError("An unhandled error occurred in ReceiveThread: " + ex.ToString()); }
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
                IPEndPoint ep = broadcast
                    ? new IPEndPoint(IPAddress.Broadcast, address.net)
                    : new IPEndPoint(new IPAddress(address.adr), address.net != 0 ? address.net : m_port);

                byte[] payload = new byte[data_length];
                Array.Copy(buffer, offset, payload, 0, data_length);
                return m_client.Send(payload, payload.Length, ep);
            }
            catch (Exception ex)
            {
                Trace.TraceError("Error during send: " + ex.Message);
                return 0;
            }
        }

        public bool SendRegisterAsForeignDevice(IPEndPoint ep, short ttl)
        {
            byte[] buffer = { 0x81, 0x05, 0x00, 0x0A, (byte)(ttl >> 8), (byte)(ttl & 0xFF) };
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
                buffer[0] = 0x81;
                buffer[1] = 0x09;
                buffer[2] = (byte)((msg_length >> 8) & 0xFF);
                buffer[3] = (byte)(msg_length & 0xFF);
                return m_client.Send(buffer, msg_length, ep) == msg_length;
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