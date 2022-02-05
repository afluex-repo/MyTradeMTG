using System;
using System.Net;
using System.Net.Mail;

namespace MyTrade
{
    public static class BLMail
    {
        public static string SendMail(string To, string Subject, string Body, bool IsHTML)
        {
            try
            {
                var fromAddress = new MailAddress("coustomer.mytrade@gmail.com");
                var toAddress = new MailAddress(To);

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                {

                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, "Mytrade@2022")

                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    IsBodyHtml = IsHTML,
                    Subject = Subject,
                    Body = Body
                })
                    smtp.Send(message);
                return "Mail Sent Successfully";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}
