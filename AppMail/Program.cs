using AppMail.Repository;
using System;

namespace AppMail
{
    public class Program
    {
        static void Main(string[] args)
        {
            AppService appService = new AppService();
            appService.SystemOptions();
        }
    }
}
