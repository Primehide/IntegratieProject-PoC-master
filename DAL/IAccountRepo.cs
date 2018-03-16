using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Alert;

namespace DAL
{
    public interface IAccountRepo
    {
        List<Alert> getAlleAlerts();
        void UpdateAlert(Alert alert);
    }
}
