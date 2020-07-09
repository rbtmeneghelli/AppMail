using AppMail.Domain;
using AppMail.Domain.Interface;
using AppMail.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppMail.Repository
{
    public class FileService : IFileService
    {
        private async Task<List<string>> getListaEmail(string filePath)
        {
            List<string> listaEmails = new List<string>();
            if (File.Exists(filePath))
            {
                foreach (var textoArquivo in File.ReadAllLines(filePath))
                {
                    string[] resultado = textoArquivo.Split(";", StringSplitOptions.RemoveEmptyEntries);
                    foreach (var email in resultado)
                    {
                        listaEmails.Add(email.Trim());
                    }
                }
            }
            return await Task.FromResult(listaEmails);
        }

        private async Task<string[]> getEmailFile(string Path)
        {
            if (Directory.Exists(Path))
            {
                string[] files = Directory.GetFiles(Path, "*.txt");
                if (files.Length > 0)
                {
                    return await Task.FromResult(files);
                }
            }
            return null;
        }

        private async Task<string[]> getPdfOrDocFile(string Path)
        {
            if (Directory.Exists(Path))
            {
                string[] filesPdf = Directory.GetFiles(Path, "*.pdf");
                string[] filesWordx = Directory.GetFiles(Path, "*.docx");
                string[] filesWord = Directory.GetFiles(Path, "*.doc");
                if (filesPdf.Length > 0)
                    return await Task.FromResult(filesPdf);
                else if (filesWordx.Length > 0)
                    return await Task.FromResult(filesWordx);
                else if (filesWord.Length > 0)
                    return await Task.FromResult(filesWord);
            }
            return null;
        }

        private void ExecuteOption_MailProcess()
        {
            Console.WriteLine("****************************************************************************************");
            Console.WriteLine($"O processo de envio de email foi finalizado. Pressione a tecla <Enter> para continuar usando o sistema");
        }

        public async Task<bool> generateEmail(string? EmailTo, List<string> EmailCc, string? TituloEmail, string? MensagemEmail, bool? EmailAutomatico = true)
        {
            MailService mailService = new MailService();
            string[] listaEmail = await getEmailFile(System.AppDomain.CurrentDomain.BaseDirectory.ToString());
            string[] anexoEmail = await getPdfOrDocFile(System.AppDomain.CurrentDomain.BaseDirectory.ToString());

            if (anexoEmail == null)
            {
                return await Task.FromResult(false);
            }
            else
            {

                if ((bool)EmailAutomatico)
                {
                    foreach (var emailTo in await getListaEmail(listaEmail[0]))
                    {
                        await mailService.SendMailService(emailTo, null, TituloEmail, MensagemEmail, anexoEmail[0], true);
                    }
                }
                else
                {
                    await mailService.SendMailService(EmailTo, string.Join(";", EmailCc), TituloEmail, MensagemEmail, anexoEmail[0], false);
                }
                ExecuteOption_MailProcess();
                Thread.Sleep(TimeSpan.FromSeconds(10));
                return await Task.FromResult(true);
            }
        }
    }
}
