using KeywordDrivenFramework.CommonUtilities;
using NUnit.Framework;
using System;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;

namespace TestFramework.TestCases
{
    [SetUpFixture]
    public class SendEmail
    {
        string body = string.Empty;

        [OneTimeTearDown]
        public void sendMail()
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            GeneralMethod.createZipFile();
            string fromEmail = ConfigurationManager.AppSettings["FromEmail"];
            string fromPassword = ConfigurationManager.AppSettings["FromPassword"];
            string smtpHost = ConfigurationManager.AppSettings["SMTPHost"];
            int portNumber = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
            try
            {
                ContentType ctype = new ContentType("application/zip");
                string zipFile = GeneralMethod.GetZipFolderPath() + @"\ExtentReport.zip";
                mail.From = new MailAddress(fromEmail, "TestingXperts");
                mail.To.Add(new MailAddress("rajbir.kaur@testingxperts.com", "rajbir"));
                mail.To.Add(new MailAddress("gunjan.garg@testingxperts.com", "TestingXperts"));
                mail.Subject = "Automation Test Reports";
                mail.IsBodyHtml = true;
                body = PopulateBody();
                mail.Body = body.ToString();
                mail.Attachments.Add(new Attachment(zipFile, ctype));
                smtp.Host = smtpHost;
                smtp.Port = portNumber;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(fromEmail, fromPassword);
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
        private string PopulateBody()
        {
            using (StreamReader reader = new StreamReader(@".\TestFramework\Email\EmailContent.html"))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{Url}", ConfigurationManager.AppSettings["ReportUrl"]);
            return body;
        }
    }
}