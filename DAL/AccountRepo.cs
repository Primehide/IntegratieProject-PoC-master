using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Alert;
using System.Data.Entity;

namespace DAL
{
    public class AccountRepo : IAccountRepo
    {
        EF.EFContext ctx;

        public AccountRepo()
        {
            ctx = new EF.EFContext();
        }

        public AccountRepo(UnitOfWork uow)
        {
            ctx = uow.Context;
        }

        public List<Alert> getAlleAlerts()
        {
            return ctx.Alerts.Include(x => x.Entiteit).Include(x => x.Entiteit.Posts).Include(x => x.Entiteit.Trends).Include(x => x.User).ToList();
        }

        public void UpdateAlert(Alert alert)
        {
            ctx.Entry(alert).State = EntityState.Modified;
            ctx.SaveChanges();
        }
    }
}
