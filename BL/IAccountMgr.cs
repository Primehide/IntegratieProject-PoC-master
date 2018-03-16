using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Alert;

namespace BL
{
    public interface IAccountMgr
    {
        List<Alert> getAlleAlerts();
        void genereerAlerts();
        void UpdateAlert(Alert alert);
        List<Alert> getAllertOnAccountId(int accountId);
    }
}
