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

        public event MessageRecievedHandler MessageRecieved;
        public BacnetAddressTypes Type { get { return BacnetAddressTypes.IP; } }
        public int HeaderLength { get { return 4; } }
        public BacnetMaxAdpu MaxAdpuLength { get { return BacnetMaxAdpu.MAX_APDU1476; } }
        public int MaxBufferLength { get { return 1472; } }
        public byte MaxInfoFrames { get; set; } = 0xFF;

        // Corrected constructor to match BAS-Tools
        public BacnetIpUdpProtocolTransport(int port, int timeout_not_used, bool exclusive_not_used, int retries_not_used, string local_endpoint_ip = "")
        {
            m_port = port;
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