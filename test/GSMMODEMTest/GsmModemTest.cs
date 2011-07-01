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
        private MockCom ms = new MockCom();
        GsmModem gm;

        //测试前运行的函数
        public void SetFixture(GsmModem data)
        {
            InitializeCompoenent();
        }

        //初始化各部件
        public void InitializeCompoenent()
        {
            gm = new GsmModem(ms.MockObject.Object);
            gm.SmsRecieved += new EventHandler(gm_SmsRecieved);
        }

        //接收短信事件
        void gm_SmsRecieved(object sender, EventArgs e)
        {
            int sMsgIndex = 0;
            DecodedMessage dm = gm.ReadNewMsg(out sMsgIndex);
            //throw new NotImplementedException();
            Assert.Equal(1, dm.Total);
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
            ms.SmsRecieved();
        }

        [Fact]
        public void GetUnreadMsgTest()
        {
            List<DecodedMessage> ldm = gm.GetUnreadMsg();
            Assert.Equal(3, ldm.Count);
        }
    }
}
