using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GSMMODEM;
using Xunit;

namespace GSMMODEMTest
{
    public class CodedMessageTest
    {
        [Fact]
        public void LengthTest()
        {
            //向号码为15050850677的手机发送“你好”
            string pdu = "0011000D91685150800576F70008C4044F60597D";
            CodedMessage c = new CodedMessage(pdu);
            Assert.Equal(19, c.Length);

            pdu = "0891683108501105F011000D91685150800576F70008C4044F60597D";

            c = new CodedMessage(pdu);

            Assert.Equal(19, c.Length);
        }
    }
}
