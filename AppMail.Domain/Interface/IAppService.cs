using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppMail.Domain.Interface
{
    public interface IAppService
    {
        public Task SystemOptions();
        public Task<bool> CheckOptionChoosedByUserIsOk(string Option);
        public Task CheckOptionChoosedByUser(string Option);
    }
}
