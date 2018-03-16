using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entiteit;
using DAL.EF;
using System.Data.Entity;

namespace DAL
{
    public class EntiteitRepo : IEntiteitRepo
    {
        private EFContext ctx;

        public EntiteitRepo()
        {
            ctx = new EFContext();
        }

        public EntiteitRepo(UnitOfWork uow)
        {
            ctx = uow.Context;
        }

        public void AddEntities(List<Entiteit> Entities)
        {
            ctx.Entiteiten.AddRange(Entities);
            ctx.SaveChanges();
        }

        public List<Persoon> getAlleEntiteiten()
        {
            return ctx.Personen.Include(x => x.Trends).Include(x => x.Posts).ToList();
        }

        public Entiteit getEntiteit(Entiteit entiteit)
        {
            return ctx.Entiteiten.Find(entiteit.EntiteitId);
        }

        public void addTrend(Trend trend)
        {
            ctx.Trends.Add(trend);
            ctx.SaveChanges();
        }

        public void UpdateEntiteit(Entiteit entiteitToUpdate)
        {
            /*
            foreach (var trend in entiteitToUpdate.Trends)
            {
                //ctx.Entry(trend).State = System.Data.Entity.EntityState.Added;
                ctx.Trends.Attach(trend);
            }
            */
            ctx.Entry(entiteitToUpdate).State = System.Data.Entity.EntityState.Modified;
            ctx.SaveChanges();
        }
    }
}
