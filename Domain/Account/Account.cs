using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Account
{
    public class Account
    {
        public int AccountId { get; set; }
        public List<Alert.Alert> Alerts { get; set; }
        public string Naam { get; set; }
    }
}
