using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

/// <summary>
/// MailHelper 的摘要说明
/// </summary>
public class MailHelper
{
    public MailHelper()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    public bool SendMail(String toaddress, String bodyContent, String subject)
    {

        String emailSmtpServer = "smtp.163.com";
        String emailAccount = "17712245617@163.com";
        String emailPassword = "GS9WSG6Y4VnZ";
        String titleSubject = subject;

        SmtpClient client = new SmtpClient(emailSmtpServer);
        client.Credentials = new System.Net.NetworkCredential(emailAccount, emailPassword);
        client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
        client.EnableSsl = true;

        MailMessage message = new MailMessage(emailAccount, toaddress, titleSubject, bodyContent);
        message.BodyEncoding = System.Text.Encoding.UTF8;
        message.IsBodyHtml = true;
        message.Priority = System.Net.Mail.MailPriority.Normal;

        try
        {
            client.SendAsync(message, "");
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}