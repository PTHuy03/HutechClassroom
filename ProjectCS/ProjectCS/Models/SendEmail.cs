using System;
using System.Net;
using System.Net.Mail;

namespace ProjectCS.Services
{
    public class SendEmail
    {
        public void SendPassword(string gmail, string password)
        {
            string smtpServer = "smtp.gmail.com";
            int port = 587;
            bool enableSSL = true;
            string userName = "richard.dataprotection@gmail.com";
            string passWord = "qzbh kgcx yoqn gpmp";

            using (var client = new SmtpClient(smtpServer, port))
            {
                client.EnableSsl = enableSSL;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(userName, passWord);

                var from = new MailAddress(userName, "Hutech");
                var to = new MailAddress(gmail, "Hutech");
                var message = new MailMessage(from, to)
                {
                    Subject = "Khôi phục mật khẩu",
                    Body = $"Lưu ý: Nhớ đổi mật khẩu" +
                           $"\nPassword: {password}",
                    IsBodyHtml = false,
                };

                try
                {
                    client.Send(message);
                    Console.WriteLine("Email sent successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to send email: {ex.Message}");
                }
            }
        }

        public void SendEmailRegis(string gmail,string password)
        {
            string smtpServer = "smtp.gmail.com";
            int port = 587;
            bool enableSSL = true;
            string userName = "richard.dataprotection@gmail.com";
            string passWord = "qzbh kgcx yoqn gpmp";

            using (var client = new SmtpClient(smtpServer, port))
            {
                client.EnableSsl = enableSSL;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(userName, passWord);

                var from = new MailAddress(userName, "Hutech");
                var to = new MailAddress(gmail, "Hutech");
                var message = new MailMessage(from, to)
                {
                    Subject = "Tạo tài khoảng thành công",
                    Body = $"Lưu ý: Nhớ đổi mật khẩu" +
                           $"\nTên đăng nhập: {gmail}"+
                           $"\nPassword: {password}",
                    IsBodyHtml = false,
                };

                try
                {
                    client.Send(message);
                    Console.WriteLine("Email sent successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to send email: {ex.Message}");
                }
            }
        }
    }
}