using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;

namespace PoC_UI.Models
{
    public class PoCViewModel
    {

        public List<Domain.Entiteit.Persoon> Entiteiten { get; set; }
        public List<Domain.Alert.Alert> Alerts { get; set; }
    }
}