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

        public void SystemOptions()
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
                if (CheckOptionChoosedByUserIsOk(option))
                {
                    optionIsValid = true;
                    CheckOptionChoosedByUser(option);
                }
            }
        }

        public bool CheckOptionChoosedByUserIsOk(string Option)
        {
            if (string.IsNullOrWhiteSpace(Option))
            {
                ClearConsoleWindow();
                Console.WriteLine("****************************************************************************************");
                Console.WriteLine("Escolha uma das opções acima digitando seu numero na tela e pressione a tecla Enter");
                Console.WriteLine("****************************************************************************************");
                Console.ReadLine();
                return false;
            }
            return true;
        }

        public void CheckOptionChoosedByUser(string Option)
        {
            switch (Convert.ToInt32(Option))
            {
                case 1:
                    ExecuteOption_SendManualMail();
                    break;
                case 2:
                    ExecuteOption_SendAutomaticMail();
                    break;
                case 3:
                    ExecuteOption_CloseApp();
                    break;
            }
        }

        private void setEmailSettings()
        {
            setEmailTo();
            setEmailCc();
            setEmailTitle();
            setEmailMessage();
        }

        private void setEmailTo()
        {
            ClearConsoleWindow();
            Console.WriteLine("****************************************************************************************");
            Console.WriteLine("Digite o email para quem será enviado da mensagem");
            Console.WriteLine("****************************************************************************************");
            emailTo = Console.ReadLine();
        }

        private void setEmailCc()
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

        private void setEmailTitle()
        {
            ClearConsoleWindow();
            Console.WriteLine("****************************************************************************************");
            Console.WriteLine("Digite o titulo do email");
            Console.WriteLine("****************************************************************************************");
            tituloEmail = Console.ReadLine();
        }

        private void setEmailMessage()
        {
            ClearConsoleWindow();
            Console.WriteLine("****************************************************************************************");
            Console.WriteLine("Digite a mensagem do seu email");
            Console.WriteLine("****************************************************************************************");
            mensagemEmail = Console.ReadLine();
        }

        private void ExecuteOption_SendManualMail()
        {
            FileService fileService = new FileService();
            setEmailSettings();
            ClearConsoleWindow();
            Console.WriteLine("****************************************************************************************");
            Console.WriteLine("Processando Envio de Email Manual, por favor aguarde...");
            if(fileService.generateEmail(emailTo, emailCc, tituloEmail, mensagemEmail, false))
            {
                ExecuteOption_CloseSuccess();
            }
           else
            {
                ExecuteOption_AlertFile();
            }
        }

        private void ExecuteOption_SendAutomaticMail()
        {
            FileService fileService = new FileService();
            ClearConsoleWindow();
            Console.WriteLine("****************************************************************************************");
            Console.WriteLine("Processando Envio de Email Automatico, aguarde...");
            if(fileService.generateEmail("", null, "", "", true))
            {
                ExecuteOption_CloseSuccess();
            } else
            {
                ExecuteOption_AlertFile();
            }
       
        }

        private void ExecuteOption_CloseApp()
        {
            ClearConsoleWindow();
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
            ClearConsoleWindow();
            Console.WriteLine("****************************************************************************************");
            Console.WriteLine("Os emails foram enviados a seus destinatarios com sucesso");
            Console.WriteLine("****************************************************************************************");
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
