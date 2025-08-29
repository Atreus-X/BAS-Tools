using System;
using System.IO.Ports;

namespace System.IO.BACnet
{
    // This is a placeholder class to allow the project to compile.
    // It does not contain a functional MSTP implementation.
    public class BacnetMstpProtocolTransport : IBacnetTransport
    {
        public BacnetMstpProtocolTransport(string portName, int slaveId) { /* Placeholder */ }
        public event MessageRecievedHandler MessageRecieved;
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