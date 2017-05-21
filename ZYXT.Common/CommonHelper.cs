using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ZYXT.Common
{
    public class CommonHelper
    {
        /// <summary>
        /// 计算字符串MD5值
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string CalcMD5( string str)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
            return CalcMD5(bytes);
        }
        /// <summary>
        /// 计算二进制文件MD5值
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string CalcMD5(byte[] bytes)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] computeBytes = md5.ComputeHash(bytes);
                string result = "";
                for (int i = 0; i < computeBytes.Length; i++)
                {
                    result += computeBytes[i].ToString("X").Length == 1 ? "0" +
                    computeBytes[i].ToString("X") : computeBytes[i].ToString("X");
                }
                return result;
            }
        }
        /// <summary>
        /// 计算流的MD5值
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string CalcMD5(Stream stream)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] computeBytes = md5.ComputeHash(stream);
                string result = "";
                for (int i = 0; i < computeBytes.Length; i++)
                {
                    result += computeBytes[i].ToString("X").Length == 1 ? "0" +
                    computeBytes[i].ToString("X") : computeBytes[i].ToString("X");
                }
                return result;
            }
        }
        /// <summary>
        /// 创建验证码
        /// </summary>
        /// <param name="len">验证码长度</param>
        /// <returns></returns>
        public static string GenerateCaptchaCode(int len)
        {
            char[] data = { 'a', 'c', 'd', 'e', 'f', 'g', 'k', 'm', 'p', 'r', 's', 't', 'w', 'x', 'y', '3', '4', '5', '7', '8' };
            StringBuilder sbCode = new StringBuilder();
            Random rand = new Random();
            for (int i = 0; i < len; i++)
            {
                char ch = data[rand.Next(data.Length)];
                sbCode.Append(ch);
            }
            return sbCode.ToString();
        }
        /// <summary>
        /// 向用户发送邮件
        /// </summary>
        /// <param name="toMail">要发送给谁的邮箱名</param>
        /// <param name="MailName">自己邮箱的用户名</param>
        /// <param name="MailPwd">自己邮箱的密码</param>
        /// <param name="title">邮件标题</param>
        /// <param name="content">邮件内容</param>
        public static void SendMail(string toMail,string MailName,string MailPwd,string title,string content)
        {
            string smtpServer = ConfigurationManager.AppSettings["smtpMailServer"];
            string smtpSendMail= ConfigurationManager.AppSettings["sendMail"];
            if (string.IsNullOrEmpty(smtpServer)||string.IsNullOrEmpty(smtpSendMail))
            {
                throw new AggregateException("没有读取到配置文件的内容");
            }         
            using (MailMessage mailMessage = new MailMessage())
            using (SmtpClient smtpClient = new SmtpClient(smtpServer))
            {
                mailMessage.To.Add(toMail);
                //mailMessage.To.Add(接收邮箱 2);
                mailMessage.Body = content;
                mailMessage.From = new MailAddress(smtpSendMail);
                mailMessage.Subject = title;
                smtpClient.Credentials = new System.Net.NetworkCredential(MailName, MailPwd);// 如果启用了“客户端授权码”，要用授权码代替密码
                smtpClient.Send(mailMessage);
            }
        }
    }
}
