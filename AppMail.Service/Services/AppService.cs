using AppMail.Domain;
using AppMail.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppMail.Repository
{
    public class AppService : IAppService
    {
        private string emailTo;
        private List<string> emailCc = new List<string>();
        private string tituloEmail;
        private string mensagemEmail;

        public async Task SystemOptions()
        {
            bool optionIsValid = false;
            Console.WriteLine("****************************************************************************************");
            Console.WriteLine("Bem vindo ao Serviço de envio de curriculo");
            Console.WriteLine("Opção 1 - Envio de email manual");
            Console.WriteLine("Opção 2 - Envio de email automatico");
            Console.WriteLine("Opção 3 - Finalizar serviço");
            Console.WriteLine("****************************************************************************************");
            while (optionIsValid != true)
            {
                string option = Console.ReadLine();
                if (await CheckOptionChoosedByUserIsOk(option))
                {
                    optionIsValid = true;
                    await CheckOptionChoosedByUser(option);
                }
            }
        }

        public async Task<bool> CheckOptionChoosedByUserIsOk(string Option)
        {
            if (!string.IsNullOrWhiteSpace(Option) && int.TryParse(Option, out _))
            {
                return int.Parse(Option) <= 3 ? await Task.FromResult(true) : await Task.FromResult(false);
            }
            return await Task.FromResult(false);
        }

        public async Task CheckOptionChoosedByUser(string Option)
        {
            switch (Convert.ToInt32(Option))
            {
                case 1:
                    await ExecuteOption_SendManualMail();
                    break;
                case 2:
                    await ExecuteOption_SendAutomaticMail();
                    break;
                case 3:
                    ExecuteOption_CloseApp();
                    break;
            }
        }

        private async Task setEmailSettings()
        {
            await setEmailTo();
            await setEmailCc();
            await setEmailTitle();
            await setEmailMessage();
        }

        private async Task setEmailTo()
        {
            ClearConsoleWindow();
            Console.WriteLine("****************************************************************************************");
            Console.WriteLine("Digite o email para quem será enviado da mensagem");
            Console.WriteLine("****************************************************************************************");
            emailTo = Console.ReadLine();
        }

        private async Task setEmailCc()
        {
            ClearConsoleWindow();
            Console.WriteLine("****************************************************************************************");
            Console.WriteLine("Digite o(s) email(s) que receberão essa mensagem em copia separador por ;");
            Console.WriteLine("****************************************************************************************");
            string email = Console.ReadLine();
            if (email != null)
            {
                foreach (var item in email.Split(";"))
                {
                    emailCc.Add(item);
                }
            }
        }

        private async Task setEmailTitle()
        {
            ClearConsoleWindow();
            Console.WriteLine("****************************************************************************************");
            Console.WriteLine("Digite o titulo do email");
            Console.WriteLine("****************************************************************************************");
            tituloEmail = Console.ReadLine();
        }

        private async Task setEmailMessage()
        {
            ClearConsoleWindow();
            Console.WriteLine("****************************************************************************************");
            Console.WriteLine("Digite a mensagem do seu email");
            Console.WriteLine("****************************************************************************************");
            mensagemEmail = Console.ReadLine();
        }

        private async Task ExecuteOption_SendManualMail()
        {
            FileService fileService = new FileService();
            await setEmailSettings();
            ClearConsoleWindow();
            Console.WriteLine("****************************************************************************************");
            Console.WriteLine("Processando Envio de Email Manual, por favor aguarde...");
            Console.WriteLine("****************************************************************************************");
            if (await fileService.generateEmail(emailTo, emailCc, tituloEmail, mensagemEmail, false))
            {
                ExecuteOption_CloseSuccess();
            }
            else
            {
                ExecuteOption_AlertFile();
            }
        }

        private async Task ExecuteOption_SendAutomaticMail()
        {
            FileService fileService = new FileService();
            ClearConsoleWindow();
            Console.WriteLine("****************************************************************************************");
            Console.WriteLine("Processando Envio de Email Automatico, aguarde...");
            Console.WriteLine("****************************************************************************************");
            if (await fileService.generateEmail("", null, "", "", true))
            {
                ExecuteOption_CloseSuccess();
            }
            else
            {
                ExecuteOption_AlertFile();
            }

        }

        private void ExecuteOption_CloseApp()
        {
            Environment.Exit(0);
        }

        private void ExecuteOption_AlertFile()
        {
            ClearConsoleWindow();
            Console.WriteLine("****************************************************************************************");
            Console.WriteLine("Não foi encontrado arquivo em anexo para envio do email, favor inclui-lo e tente novamente!");
            Console.WriteLine("****************************************************************************************");
            Console.ReadKey();
            ClearConsoleWindow();
            SystemOptions();

        }

        private void ExecuteOption_CloseSuccess()
        {
            Console.ReadKey();
            ClearConsoleWindow();
            SystemOptions();
        }

        private void ClearConsoleWindow()
        {
            Console.Clear();
        }
    }
}
