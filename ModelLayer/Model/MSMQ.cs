using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;

namespace ModelLayer.Model
{
    public class MSMQ
    {
        MessageQueue messageQueue = new MessageQueue();
        private string recieverEmail;
        private string recieverName;
        public void SendMessage(string token,string email,string name)
        {
            recieverEmail= email;
            recieverName= name;
            messageQueue.Path = @".\Private$\Token";
            try
            {
                if (!MessageQueue.Exists(messageQueue.Path))
                {
                    MessageQueue.Create(messageQueue.Path);
                }
                messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                messageQueue.ReceiveCompleted += MessageQueue_ReceiveCompleted;
                messageQueue.Send(token);
                messageQueue.BeginReceive();
                messageQueue.Close();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        private void MessageQueue_ReceiveCompleted(Object sender,ReceiveCompletedEventArgs e)
        {
            try
            {
                var msg = messageQueue.EndReceive(e.AsyncResult);
                string token = msg.Body.ToString();
                MailMessage mailMessage = new MailMessage();
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential("doppaniganesh@gmail.com", "ixvwyoswrtvzrbdx"),
                };
                mailMessage.From = new MailAddress("doppaniganesh@gmail.com");
                mailMessage.To.Add(new MailAddress(recieverEmail));
                string mailBody = $"<!DOCTYPE html>" + 
                                  $"<html>" + 
                                  $"<style>" + 
                                  $".blink" + 
                                  $"</style>" +
                                  $"<body style =\"background-color:#DBFF73;text-align:center;padding:5px;\">" +
                                  $"<h1 style =\"color:#6A8D02;border-bottom:3px sokid #84AF08;margin-top:5px;\"> Dear <b>{recieverName}</b> </h1>\n" +
                                  $"<h3 style = \"color:#BAB411;\"> For resetting Password the Below Link is Issued</h3>" +
                                  $"<h3 style = \"color:#BAB411;\"> Please Click the Link Below To Reset Your Password</h3>" +
                                  $"<a style =\"color:#00802b; text-decoration:none;font-size:20px;\"href=''>Click me</a>" +
                                  $"<h3 style = \"color:#BAB411;margin-bottom:5px;\"><blink> This Token is valid for Next 6 hrs<blink></h3>" +
                                  $"</body>" + 
                                  $"</html>";

                mailMessage.Body = mailBody;
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = "Fundo notes password reset link";
                smtpClient.Send(mailMessage);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
