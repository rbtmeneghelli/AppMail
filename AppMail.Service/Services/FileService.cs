using AppMail.Domain;
using AppMail.Domain.Interface;
using AppMail.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AppMail.Repository
{
    public class FileService : IFileService
    {
        private List<string> getListaEmail(string filePath)
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
            return listaEmails;
        }

        private string[] getEmailFile(string Path)
        {
            if (Directory.Exists(Path))
            {
                string[] files = Directory.GetFiles(Path, "*.txt");
                if (files.Length > 0)
                {
                    return files;
                }
            }
            return null;
        }

        private string[] getPdfOrDocFile(string Path)
        {
            if (Directory.Exists(Path))
            {
                string[] filesPdf = Directory.GetFiles(Path, "*.pdf");
                string[] filesWordx = Directory.GetFiles(Path, "*.docx");
                string[] filesWord = Directory.GetFiles(Path, "*.doc");
                if (filesPdf.Length > 0)
                    return filesPdf;
                else if (filesWordx.Length > 0)
                    return filesWordx;
                else if (filesWord.Length > 0)
                    return filesWord;
            }
            return null;
        }

        public bool generateEmail(string? EmailTo, List<string> EmailCc, string? TituloEmail, string? MensagemEmail, bool? EmailAutomatico = true)
        {
            MailService mailService = new MailService();
            string[] listaEmail = getEmailFile(System.AppDomain.CurrentDomain.BaseDirectory.ToString());
            string[] anexoEmail = getPdfOrDocFile(System.AppDomain.CurrentDomain.BaseDirectory.ToString());

            if (anexoEmail == null)
            {
                return false;
            }
            else
            {

                if ((bool)EmailAutomatico)
                {
                    foreach (var emailTo in getListaEmail(listaEmail[0]))
                    {
                        mailService.SendMailService(emailTo, null, TituloEmail, MensagemEmail, anexoEmail[0], true);
                    }
                }
                else
                {
                    mailService.SendMailService(EmailTo, string.Join(";", EmailCc), TituloEmail, MensagemEmail, anexoEmail[0], false);
                }
                return true;
            }
        }
    }
}
