using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;
using GSMMODEM;
 
namespace 短信猫
{
    public partial class Form1 : Form
    {
        private GsmModem gm = new GsmModem();

        //委托 收到短信的回调函数委托
        delegate void UpdataDelegate();         //可以有参数，本处不需要
        UpdataDelegate UpdateHandle = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //初始化控件
            foreach (string s in SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(s);
            }
            if (comboBox1.Items.Count == 0)
            {
                label3.Text = "未检测到串口";
                label3.ForeColor = Color.Red;
            }
            else
            {
                comboBox1.SelectedIndex = 0;
            }
            comboBox2.SelectedIndex = 0;

            //初始化设备类对象 gm
            if (comboBox1.Items.Count == 0)
            {

            }
            else
            {
                gm.ComPort = comboBox1.SelectedItem.ToString();
            }
            gm.BaudRate = Convert.ToInt32(comboBox2.SelectedItem);

            gm.SmsRecieved +=new EventHandler(gm_SmsRecieved);

            UpdateHandle = new UpdataDelegate(UpdateLabel8);        //实例化委托

        }

        void gm_SmsRecieved(object sender, EventArgs e)
        {
            Invoke(UpdateHandle, null);
        }

        void UpdateLabel8()
        {
            label8.Text = "有新消息";
            label8.ForeColor = Color.Green;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gm.IsOpen)
            {
                label3.Text = "更改无效";
                label3.ForeColor = Color.Red;
            }
            else
            {
                gm.ComPort = comboBox1.SelectedItem.ToString();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            gm.BaudRate = Convert.ToInt32(comboBox2.SelectedItem);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (gm.IsOpen == false)
            {
                string sResult = "";
                try
                {
                    gm.Open(out sResult);
                    label3.Text = "连接成功";
                    label3.ForeColor = Color.Green;
                    LogInfo(sResult);

                }
                catch (Exception ee)
                {
                    LogInfo("Connect Modem: Exception:" + ee.ToString() + " sResult:" + sResult);
                    label3.Text = "连接失败";
                    label3.ForeColor = Color.Red;
                }
                 
                try {
                    string gmInfo = gm.GetMachineNo();
                    LogInfo(" Connect Modem:" + gmInfo);
                }
                catch (Exception ee)
                {
                    LogInfo("Get Modem Info Exception:" + ee.ToString());
                     
                }
            }
            else
            {
                label3.Text = "已连接";
                label3.ForeColor = Color.Green;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(gm.IsOpen)
            {
                try
                {
                    gm.Close();
                    label3.Text = "断开成功";
                    label3.ForeColor = Color.Red;
                }
                catch
                {
                    label3.Text="断开失败";
                    label3.ForeColor=Color.Red;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
           /* //短信过长，通知用户字数 直接返回  原来不能发送长短信时用
            if (textBox2.Text.Length > 70)
            {
                label6.Text = "短信" + textBox2.Text.Length.ToString() + "字";
                label6.ForeColor = Color.Red;
                return;
            }*/
            if (gm.IsOpen)
            {
                try
                {
                    gm.SendMsg(textBox1.Text, textBox2.Text);
                }
                catch (Exception ee)
                {
                    LogInfo("SendMsg: Exception:" + ee.ToString());
                    label6.Text = "发送失败";
                    label6.ForeColor = Color.Red;
                    return;
                }
                label6.Text = "发送成功";
                label6.ForeColor = Color.Green;
            }
            else
            {
                label6.Text = "设备未连接";
                label6.ForeColor = Color.Red;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox4.Text = null;

            if (gm.IsOpen)
            {
                try
                {
                    DecodedMessage dm = gm.ReadMsgByIndex(Convert.ToInt32(textBox3.Text));
                    textBox4.Text = "Index：" + dm.SmsIndex + "短信中心：" + dm.ServiceCenterAddress + "\r\n" + "手机号码：" + dm.PhoneNumber + "\r\n" +
                            "短信内容：" + dm.SmsContent + "\r\n" + "发送时间：" + dm.SendTime;
                    label9.Text = "读取成功";
                    label9.ForeColor = Color.Green;
                }
                catch (Exception ee)
                {
                    LogInfo("ReadMsgByIndex: Exception:" + ee.ToString());
                    label9.Text = "读取失败";
                    label9.ForeColor = Color.Red;
                    return;
                }
            }
            else
            {
                label9.Text = "设备未连接";
                label9.ForeColor = Color.Red;
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //
            textBox5.Text = null;

            if (label8.Text != "有新消息")
            {
                label8.Text = "没有新消息";
                label8.ForeColor = Color.Red;
                return;
            }

            if (gm.IsOpen)
            {
                try
                {
                    int sMsgIndex = 0;
                    DecodedMessage dm = gm.ReadNewMsg(out sMsgIndex);
                    textBox5.Text = "Index：" + dm.SmsIndex + "\r\n 短信中心：" + dm.ServiceCenterAddress + "\r\n" + "手机号码：" + dm.PhoneNumber + "\r\n" +
                            "短信内容：" + dm.SmsContent + "\r\n" + "发送时间：" + dm.SendTime;
                    label8.Text = "读取成功";
                    label8.ForeColor = Color.Green;
                }
                catch (Exception ee)
                {
                    LogInfo("ReadNewMsg: Exception:" + ee.ToString());
                    label8.Text = "读取失败";
                    label8.ForeColor = Color.Red;
                    return;
                }
            }
            else
            {
                label8.Text = "设备未连接";
                label8.ForeColor = Color.Red;
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (gm.IsOpen)
            {
                try
                {
                    gm.DeleteMsgByIndex(Convert.ToInt32(textBox3.Text));
                }
                catch(Exception ee)
                {
                    LogInfo("DeleteMsg:" + Convert.ToInt32(textBox3.Text) + " Exception:" + ee.ToString());
                    label9.Text = "删除失败";
                    label9.ForeColor = Color.Red;
                    return;
                }
            }
            else
            {
                label9.Text = "设备未连接";
                label9.ForeColor = Color.Red;
                return;
            }
            label9.Text = "删除成功";
            label9.ForeColor = Color.Green;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            label10.Text = textBox2.Text.Length + "字";
            label10.ForeColor = Color.Green;
        }
 

        private void LogInfo(string sLogInfo){
            txtLogInfo.AppendText( System.DateTime.Now+":" + sLogInfo + "\r\n");
        }

        private void btnSendAT_Click(object sender, EventArgs e)
        {
            string sResult = "";
            if (gm.IsOpen) {
                try
                {
                    sResult = gm.SendAT(txtATCMD.Text);
                }
                catch (Exception ee)
                {
                    LogInfo("SendAT:" + txtATCMD.Text + " Exception:" + ee.ToString());
                    return;
                }            
               
            }
            LogInfo("SendAT:" + txtATCMD.Text + " Result:" + sResult);
        }

        private void btn_allunread_Click(object sender, EventArgs e)
        {
            List<DecodedMessage> result;
            string sResult  = "";
            if (gm.IsOpen)
            {
                try
                {

                    result = gm.GetUnreadMsg(out sResult);

                    for (int i = 0; i < result.Count; i++)
                    {
                        DecodedMessage dm = result[i];
                        sResult += "Index：" + dm.SmsIndex + "短信中心：" + dm.ServiceCenterAddress + "\r\n" + "手机号码：" + dm.PhoneNumber + "\r\n" +
                                "短信内容：" + dm.SmsContent + "\r\n" + "发送时间：" + dm.SendTime;
                    }
                }
                catch (Exception ee)
                {
                    LogInfo("allunread:at+cmgl=0 Exception:" + ee.ToString());
                    return;
                }

            }
            LogInfo("allunread:at+cmgl=0 Result:" + sResult);
        }
 
    }
}
