using System;
using System.Collections.Generic;
using System.Text;

namespace GSMMODEM
{
    public class DecodedMessage
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="serviceCenterAddress">短信中心号码</param>
        /// <param name="sendTime">发送时间 字符串</param>
        /// <param name="phoneNumber">手机号码</param>
        /// <param name="smsContent">短信内容</param>
        public DecodedMessage(string serviceCenterAddress, string sendTime, string phoneNumber, string smsContent) : this("010100", serviceCenterAddress, sendTime, phoneNumber, smsContent) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="head">长短信 头部（非常短信 本参数为 010100）</param>
        /// <param name="serviceCenterAddress">短信中心</param>
        /// <param name="sendTime">发送时间 字串</param>
        /// <param name="phoneNumber">手机号码</param>
        /// <param name="smsContent">短信内容</param>
        public DecodedMessage(string head, string serviceCenterAddress, string sendTime, string phoneNumber, string smsContent)
        {
            ServiceCenterAddress = serviceCenterAddress;
            SendTime = DateTime.Parse(sendTime);
            PhoneNumber = phoneNumber;

            Flag = head.Substring(4, 2);
            Current = Convert.ToInt16(head.Substring(2, 2), 16);

            //sd初始化
            for (int i = 1; i <= Convert.ToInt16(head.Substring(0, 2), 16); i++)
            {
                sd.Add(i, "");
            }
            li.Add(Convert.ToInt16(head.Substring(2, 2), 16));
            sd[li[0]] = smsContent;
        }
        #endregion 构造函数

        //已解码 索引和短信内容
        private SortedDictionary<int, string> sd = new SortedDictionary<int, string>();

        //长短信时 已完成序号列表
        private List<int> li = new List<int>();

        #region 公有字段 或是属性
        //短信总条数 //针对长短信
        public int Total
        {
            get
            {
                return sd.Count;
            }
        }

        /// <summary>
        /// 长短信解码完成
        /// </summary>
        public bool IsCompleted
        {
            get { return (li.Count == Total); }
        }


        //当前修改值  合并长短信时使用  
        public readonly int Current;

        //这批长短信的唯一标识
        public readonly string Flag;

        //短信中心
        public readonly string ServiceCenterAddress;
        
        //发送时间
        public DateTime SendTime;

        //手机号码
        public readonly string PhoneNumber;

        //短信内容
        public string SmsContent
        {
            get
            {
                string result = string.Empty;
                foreach (string s in sd.Values)
                {
                    if (s.Length == 0 || s == null)
                    {
                        result += "(...)";
                    }
                    else
                    {
                        result += s;
                    }
                }
                return result;
            }
        }
        #endregion 公有字段 或是属性

        /// <summary>
        /// 长短信合并
        /// </summary>
        /// <param name="dm">被合并的短信</param>
        public void Add(DecodedMessage dm)
        {
            if (this.Flag != dm.Flag || dm.PhoneNumber != this.PhoneNumber)
            {
                throw new ArgumentException("不是本条的一部分");
            }

            int current = dm.Current;

            if (this.li.Contains(current))
            {
                return;
            }

            this.SendTime = dm.SendTime;
            sd[current] = dm.sd[current];
        }

        public override string ToString()
        {
            return this.Total.ToString("X2") + this.Current.ToString("X2") + this.Flag
                + "," + this.ServiceCenterAddress + "," + this.PhoneNumber + ","
                + this.SendTime.ToString("yyyyMMddHHmmss")
                + "," + this.SmsContent;
        }
    }
}
