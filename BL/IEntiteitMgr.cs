using Domain.Alert;
using Domain.Entiteit;
using Domain.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IEntiteitMgr
    {
        void AddEntities(List<Entiteit> Entities);
        List<Persoon> getAlleEntiteiten();
        void UpdateEntiteit(Entiteit entiteitToUpdate);
        void LinkPosts();
        void berekendTrends(); //gaat per enteiteit elke soort trend berekenen.
        bool berekenTrends(int minVoorwaarde, List<Post> posts, Voorwaarde voorwaarde);
        void addTrend(Trend trend);
    }
}
