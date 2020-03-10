using AppMail.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppMail.Domain.Interface
{
    public interface IMailService
    {
        public void SendMailService(string? EmailTo, string? EmailCc, string? TituloEmail, string? MensagemEmail, string? ArquivoAnexo, bool? EmailAutomatico);
    }
}
