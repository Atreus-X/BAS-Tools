using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace System.IO.BACnet
{
    public class BacnetIpUdpProtocolTransport : IBacnetTransport
    {
        private UdpClient m_client;
        private int m_port;
        private string m_local_endpoint_ip;
        private bool m_dont_fragment;
        private int m_max_payload;
        private bool m_is_foreign_device;

        public event MessageRecievedHandler MessageRecieved;
        public BacnetAddressTypes Type { get { return BacnetAddressTypes.IP; } }
        public int HeaderLength { get { return 4; } }
        public BacnetMaxAdpu MaxAdpuLength { get { return BacnetMaxAdpu.MAX_APDU1476; } }
        public int MaxBufferLength { get { return 1472; } }
        public byte MaxInfoFrames { get; set; } = 0xFF;

        public BacnetIpUdpProtocolTransport(int port, bool dont_fragment = false, bool is_foreign_device = false, int max_payload = 1472, string local_endpoint_ip = "")
        {
            m_port = port;
            m_dont_fragment = dont_fragment;
            m_max_payload = max_payload;
            m_is_foreign_device = is_foreign_device;
            m_local_endpoint_ip = local_endpoint_ip;
        }

        public void Start()
        {
            if (m_client != null) return;
            IPEndPoint local_ep = new IPEndPoint(string.IsNullOrEmpty(m_local_endpoint_ip) ? IPAddress.Any : IPAddress.Parse(m_local_endpoint_ip), m_port);
            m_client = new UdpClient();
            m_client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            m_client.Client.Bind(local_ep);
            m_client.BeginReceive(OnReceive, null);
        }

        public void Stop()
        {
            if (m_client == null) return;
            m_client.Close();
            m_client = null;
        }

        private void OnReceive(IAsyncResult ar)
        {
            try
            {
                if (m_client == null) return;
                IPEndPoint ep = new IPEndPoint(IPAddress.Any, 0);
                byte[] buffer = m_client.EndReceive(ar, ref ep);
                MessageRecieved?.Invoke(this, buffer, 0, buffer.Length, new BacnetAddress(BacnetAddressTypes.IP, (ushort)ep.Port, ep.Address.GetAddressBytes()));
            }
            catch (Exception) { /* socket is closed */ }
            finally
            {
                if (m_client != null) m_client.BeginReceive(OnReceive, null);
            }
        }

        public int Send(byte[] buffer, int offset, int data_length, BacnetAddress address, bool broadcast, int timeout)
        {
            try
            {
                IPEndPoint ep;
                if (broadcast)
                    ep = new IPEndPoint(IPAddress.Broadcast, address.net);
                else
                    ep = new IPEndPoint(new IPAddress(address.adr), address.net);

                return m_client.Send(buffer, data_length, ep);
            }
            catch
            {
                return 0;
            }
        }

        public bool SendRegisterAsForeignDevice(IPEndPoint ep, short ttl)
        {
            byte[] buffer = new byte[10];
            buffer[0] = 0x81; // BVLC_TYPE_BACNET_IP
            buffer[1] = 0x05; // BVLC_FUNCTION_REGISTER_FOREIGN_DEVICE
            buffer[2] = 0; // Length
            buffer[3] = 10;
            buffer[4] = (byte)(ttl >> 8);
            buffer[5] = (byte)(ttl & 0xFF);
            try
            {
                m_client.Send(buffer, 10, ep);
                return true;
            }
            catch (Exception ex)
            {
                Trace.TraceError("Error sending register as foreign device: " + ex.Message);
                return false;
            }
        }

        public bool SendRemoteWhois(byte[] buffer, IPEndPoint ep, int msg_length)
        {
            buffer[0] = 0x81;
            buffer[1] = 0x09; // Distribute-Broadcast-To-Network
            buffer[2] = (byte)((msg_length >> 8) & 0xFF);
            buffer[3] = (byte)(msg_length & 0xFF);
            try
            {
                m_client.Send(buffer, msg_length, ep);
                return true;
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