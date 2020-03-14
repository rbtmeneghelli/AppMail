using AppMail.Domain;
using AppMail.Domain.Interface;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace AppMail.Service
{
    public class MailService : IMailService
    {
        private async Task<Email> getEmailSettings(string? EmailTo, string? EmailCc, string? TituloEmail, string? MensagemEmail, string? ArquivoAnexo, bool? EmailAutomatico)
        {
            Email email = new Email();
            email.Id = new Guid();
            email.ArquivoAnexo = ArquivoAnexo;
            email.EmailTo = EmailTo;
            if (!(bool)EmailAutomatico)
            {
                email.EmailCc = EmailCc;
                email.TituloEmail = TituloEmail;
                email.MensagemEmail = MensagemEmail;
                email.ArquivoAnexo = ArquivoAnexo;
            }
            return await Task.FromResult(email);
        }

        public async Task SendMailService(string? EmailTo, string? EmailCc, string? TituloEmail, string? MensagemEmail, string? ArquivoAnexo, bool? EmailAutomatico)
        {
            // Habilitar a opção, através desse link: https://myaccount.google.com/lesssecureapps
            try
            {
                Email email = await getEmailSettings(EmailTo, EmailCc, TituloEmail, MensagemEmail, ArquivoAnexo, EmailAutomatico);
                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
                mail.From = new MailAddress("email@gmail.com");
                mail.To.Add(EmailTo);
                mail.Subject = email.TituloEmail;
                mail.Body = email.MensagemEmail;
                mail.Attachments.Add(new Attachment(ArquivoAnexo));
                using (var smtp = new SmtpClient("smtp.gmail.com"))
                {
                    smtp.Port = 587;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential("email@gmail.com", "XXXXXXXX");
                    await Task.FromResult(smtp.SendMailAsync(mail));
                    Thread.Sleep(1);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
    }
}
