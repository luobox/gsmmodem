using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using GSMMODEM;

namespace GSMMODEMTest
{
    public class DecodedMessageTest
    {
        [Fact]
        public void GeneralSmsTest()
        {
            string sca = "8613800511500";
            string t = "2011-05-29 10:53:20";
            string p = "15050850677";
            string c = "你好";
            DecodedMessage dm = new DecodedMessage(sca, t, p, c);

            Assert.Equal(sca, dm.ServiceCenterAddress);
            Assert.Equal(t, dm.SendTime.ToString("yyyy-MM-dd HH:mm:ss"));
            Assert.Equal(p, dm.PhoneNumber);
            Assert.Equal(c, dm.SmsContent);
        }

        [Fact]
        public void NotCompletedLongSmsTest()   //未完成长短信
        {
            string head = "0201B2";
            string sca = "8613800511500";
            string t = "2011-05-20 21:30:59";
            string p = "8615050850677";
            string c = "GUDGKVBBgsscghjgdbgdfggrddswdh.kjhffyyhujko(;;((((((((())))))))))(;;..:,,((((((((((((.::...)))))))))))))....;;;;((((((((((((((;..:..;;((((.))))))))))))))";
            DecodedMessage dm = new DecodedMessage(head,sca, t, p, c);

            Assert.Equal(sca, dm.ServiceCenterAddress);
            Assert.Equal(t, dm.SendTime.ToString("yyyy-MM-dd HH:mm:ss"));
            Assert.Equal(p, dm.PhoneNumber);
            Assert.Equal(c + "(...)", dm.SmsContent);
        }

        [Fact]
        public void CompletedLongSmsTest()
        {
            string head = "0201B2";
            string sca = "8613800511500";
            string t = "2011-05-20 21:30:59";
            string p = "8615050850677";
            string c = "GUDGKVBBgsscghjgdbgdfggrddswdh.kjhffyyhujko(;;((((((((())))))))))(;;..:,,((((((((((((.::...)))))))))))))....;;;;((((((((((((((;..:..;;((((.))))))))))))))";
            DecodedMessage dm = new DecodedMessage(head, sca, t, p, c);

            dm.Add(new DecodedMessage("0202B2", sca, "2011-05-20 21:31:01", p, ")))))))."));

            Assert.Equal(sca, dm.ServiceCenterAddress);
            Assert.Equal(t, dm.SendTime.ToString("yyyy-MM-dd HH:mm:ss"));
            Assert.Equal(p, dm.PhoneNumber);
            Assert.Equal(c + "))))))).", dm.SmsContent);
        }
    }
}
