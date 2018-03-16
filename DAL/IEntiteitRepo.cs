using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entiteit;

namespace DAL
{
    public interface IEntiteitRepo
    {
        void AddEntities(List<Entiteit> Entities);
        List<Persoon> getAlleEntiteiten();
        void UpdateEntiteit(Entiteit entiteitToUpdate);
        Entiteit getEntiteit(Entiteit entiteit);
        void addTrend(Trend trend);
    }
}
