using System;
using System.IO.Ports;

namespace System.IO.BACnet
{
    // This is a placeholder class to allow the project to compile.
    // It does not contain a functional MSTP implementation.
    public class BacnetMstpProtocolTransport : IBacnetTransport
    {
#pragma warning disable 0067 // Event is never used
        public event MessageRecievedHandler MessageRecieved;
#pragma warning restore 0067

        public BacnetMstpProtocolTransport(string portName, int baudRate, byte nodeAddress, byte maxMasters, byte maxInfoFrames) { /* Placeholder */ }
        public int HeaderLength => 0;
        public BacnetAddressTypes Type => BacnetAddressTypes.MSTP;
        public BacnetMaxAdpu MaxAdpuLength => BacnetMaxAdpu.MAX_APDU480;
        public int MaxBufferLength => 502;
        public byte MaxInfoFrames { get; set; } = 1;
        public void Start() { throw new NotImplementedException(); }
        public int Send(byte[] buffer, int offset, int data_length, BacnetAddress address, bool broadcast, int timeout) { throw new NotImplementedException(); }
        public BacnetAddress GetBroadcastAddress() { throw new NotImplementedException(); }
        public bool WaitForAllTransmits(int timeout) { return true; }
        public void Dispose() { }
    }
}