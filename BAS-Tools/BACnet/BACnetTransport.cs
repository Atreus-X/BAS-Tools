using System.Net;

namespace System.IO.BACnet
{
    public delegate void MessageRecievedHandler(IBacnetTransport sender, byte[] buffer, int offset, int msg_length, BacnetAddress remote_address);

    public interface IBacnetTransport : IDisposable
    {
        event MessageRecievedHandler MessageRecieved;
        int HeaderLength { get; }
        BacnetAddressTypes Type { get; }
        BacnetMaxAdpu MaxAdpuLength { get; }
        int MaxBufferLength { get; }
        byte MaxInfoFrames { get; set; }

        void Start();
        int Send(byte[] buffer, int offset, int data_length, BacnetAddress address, bool broadcast, int timeout);
        BacnetAddress GetBroadcastAddress();
        bool WaitForAllTransmits(int timeout);
    }
}