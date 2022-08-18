﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZeroWindApi.Models;

namespace Tools
{
    public class EmailSend
    {

        private readonly string host;//服务器
        private readonly string emil;//你的邮箱
        private readonly string pwd;//邮箱密钥
        private readonly string target;//目标邮箱
        public EmailSend(string em,string password)
        {
            host = "smtp.qq.com";
            emil = "prozerowind@foxmail.com";
            pwd = password;
            target = em;
        }
        /// <summary>
        /// 发送邮件给目标邮件地址
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public Task<string> toEmil()
        {
            SmtpClient sct = new SmtpClient();
            string result = GetPass();
            //设置email的基本信息
            sct.Host = host;
            sct.Port = 587;//端口号
            MailAddress me = new MailAddress(emil);
            MailAddress you = new MailAddress(target);
            MailMessage mm = new MailMessage(me, you);
            //邮件标题
            mm.Subject = "ZeroWind个人网站验证码";
            //编码方式
            mm.SubjectEncoding = Encoding.UTF8;
            //邮件内容
            mm.Body = result;
            mm.BodyEncoding = Encoding.UTF8;
            sct.DeliveryMethod = SmtpDeliveryMethod.Network;
            //发送
            try
            {
                sct.EnableSsl = true;
                sct.UseDefaultCredentials = false;
                NetworkCredential nc = new NetworkCredential(emil, pwd);
                sct.Credentials = nc;
                sct.Send(mm);
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            return Task.FromResult(result);
        }

        /// <summary>
        /// 构建一个随机的四位字符作为验证码
        /// </summary>
        /// <returns></returns>
        public string GetPass()
        {
            string[] Is = new string[4];
            for (int i = 0; i < Is.Length; i++)
            {
                int num = new Random().Next(10);
                if (num > 7 || num == 0)
                {
                    Is[i] = new Random().Next(10).ToString();
                }
                else if (num < 3)
                {
                    Is[i] = ((char)('A' + new Random().Next(26))).ToString();
                }
                else
                {
                    Is[i] = ((char)('a' + new Random().Next(26))).ToString();
                }
                Thread.Sleep(100);
            }
            string result = "";
            foreach (string item in Is)
            {
                result += item;
            }
            return result;
        }
    }
}
