using Domain.Account;
using Domain.Entiteit;

namespace Domain.Alert
{
    public class Alert
    {
        public int AlertId { get; set; }
        public int MinTrendWaarde { get; set; }
        public TrendType Type { get; set; }
        public Entiteit.Entiteit Entiteit { get; set; }
        public bool Triggered { get; set; }
        public Account.Account User { get; set; }
        public PlatformType PlatformType { get; set; }
        public Voorwaarde voorwaarde { get; set; }
    }
}
