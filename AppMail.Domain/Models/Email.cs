using System;
using System.Collections.Generic;

namespace AppMail.Domain
{
    public class Email : Base
    {
        public string EmailTo { get; set; }
        public string EmailCc { get; set; }
        public string TituloEmail { get; set; }
        public string MensagemEmail { get; set; }
        public string ArquivoAnexo { get; set; }

        public Email()
        {
            this.EmailTo = "roberto.mng.89@gmail.com";
            this.EmailCc = "";
            this.TituloEmail = "Teste de envio de email";
            this.MensagemEmail = "<p>Ola mundo</p> <b>Como vai</b>";
            this.ArquivoAnexo = "";
        }
    }
}