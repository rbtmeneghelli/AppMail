using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppMail.Domain.Interface
{
    public interface IFileService
    {
        public bool generateEmail(string? EmailTo, List<string> EmailCc, string? TituloEmail, string? MensagemEmail, bool? EmailAutomatico = true);
    }
}
