using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using Moq;
using Xunit;
using GSMMODEM;

namespace GSMMODEMTest
{
    public class GsmModemTest:IUseFixture<GsmModem>
    {
        private MockSerialPort ms = new MockSerialPort();
        GsmModem gm;
        
        public void InitializeCompoEnent()
        {
        }

        [Fact]
        public void OpenTest()
        {
            gm.Open();
        }

        [Fact]
        public void SendMsgTest()
        {
            try
            {
                gm.Open();
                gm.SendMsg("15050850677", "刘中原");
            }
            finally
            {
                gm.Close();
            }
        }

        [Fact]
        public void NewMsgTest()
        {
            ms.DataRecieved();
        }

        public void SetFixture(GsmModem data)
        {
            gm = new GsmModem(ms.MockObject.Object);
        }
    }
}
