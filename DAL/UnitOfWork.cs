using DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// Deze klasse wordt gebruikt om over instanties van de repositories heen
    /// een uniek context object te creëeren. Dit doen we door een private member bij te houden
    /// en een 'internal' property Context die je van buiten af binnen hetzelfde project kan oproepen.
    /// Bij de instantatie van de context geven we aan onze context mee dat deze leeft
    /// binnen een UnitOfWork klasse, zodanig dat de private member 'DelaySave' van de context klasse
    /// op true komt te staan.
    /// </summary>
    public class UnitOfWork
    {
        private EFContext ctx;

        internal EFContext Context
        {
            get
            {
                if (ctx == null) ctx = new EFContext(true);
                return ctx;
            }
        }


        /// <summary>
        /// Deze methode zorgt ervoor dat alle tot hier toe aangepaste domein objecten
        /// worden gepersisteert naar de databank
        /// </summary>
        public void CommitChanges()
        {
            ctx.CommitChanges();
        }
    }
}
