using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppMail.Domain.Interface
{
    public interface IAppService
    {
        public void SystemOptions();
        public bool CheckOptionChoosedByUserIsOk(string Option);
        public void CheckOptionChoosedByUser(string Option);
    }
}
