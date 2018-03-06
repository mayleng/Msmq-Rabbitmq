using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Messaging;

namespace msmq
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //加载页面便运行如下代码
            // 创建一个私有消息队列
            if (!MessageQueue.Exists(@".\Private$\LearningHardMSMQ"))
            {
                 using (MessageQueue mq = MessageQueue.Create(@".\Private$\LearningHardMSMQ"))
                     {
                     mq.Label = "LearningHardPrivateQueue";
                     
                     mq.Send("MSMQ Private Message", "Leaning Hard"); // 发送消息
                                 }
                        }
            if (MessageQueue.Exists(@".\Private$\LearningHardMSMQ"))
                     {
                // 获得私有消息队列
                MessageQueue mq = new MessageQueue(@".\Private$\LearningHardMSMQ");
                  mq.Send("Sending MSMQ private message" + DateTime.Now.ToLongDateString(), "Leaning Hard");
               
                     }


        }
    }
}