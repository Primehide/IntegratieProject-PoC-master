using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entiteit;
using DAL;
using Domain.Posts;
using Domain.Alert;

namespace BL
{
    public class EntiteitMgr : IEntiteitMgr
    {
        private IEntiteitRepo repo;
        private UnitOfWorkManager uowManager;

        public EntiteitMgr()
        {

        }

        public EntiteitMgr(UnitOfWorkManager uofMgr)
        {
            repo = new EntiteitRepo();
            uowManager = uofMgr;
        }

        public void AddEntities(List<Entiteit> Entities)
        {
            initNonExistingRepo();
            repo.AddEntities(Entities);
        }

        public List<Persoon> getAlleEntiteiten()
        {
            initNonExistingRepo();
            return repo.getAlleEntiteiten();
        }

        public void UpdateEntiteit(Entiteit entiteitToUpdate)
        {
            initNonExistingRepo();
            repo.UpdateEntiteit(entiteitToUpdate);
        }

        public void LinkPosts()
        {
            initNonExistingRepo(true);
            PostMgr postMgr = new PostMgr(uowManager);
            List<Persoon> personen = getAlleEntiteiten();
            List<Post> posts = postMgr.AllePosts();

            foreach (var persoon in personen)
            {
                if (persoon.Posts == null)
                {
                    persoon.Posts = new List<Post>();
                }

                foreach (var post in posts)
                {
                    if (persoon.Voornaam == post.Naam.Voornaam && persoon.Achternaam == post.Naam.Achternaam)
                    {
                        persoon.Posts.Add(post);
                    }
                }
                UpdateEntiteit(persoon);
            }
            uowManager.Save();
        }

        public void berekendTrends()
        {
            DateTime vandaag = DateTime.Today;
            DateTime gisteren = vandaag.AddDays(-1);
            List<Persoon> personen = getAlleEntiteiten();
            foreach (var persoon in personen)
            {
                List<Post> posts = persoon.Posts;
                List<Post> oldPosts = posts.Where(x => x.Date > gisteren && x.Date < vandaag).ToList();
                List<Post> recentPosts = posts.Where(x => x.Date >= vandaag).ToList();
                List<Trend> trends = persoon.Trends;
                List<Trend> newTrends = berekenTrends(recentPosts);
                List<Trend> relatieveTrends = vergelijkTrends(trends, newTrends);
                persoon.Trends = newTrends;
            }

        }

        private List<Trend> berekenTrends(List<Post> recentPosts)
        {
            throw new NotImplementedException();
        }

        private List<Trend> vergelijkTrends(List<Trend> trends, List<Trend> newTrends)
        {
            throw new NotImplementedException();
        }

        public void initNonExistingRepo(bool withUnitOfWork = false)
        {
            // Als we een repo met UoW willen gebruiken en als er nog geen uowManager bestaat:
            // Dan maken we de uowManager aan en gebruiken we de context daaruit om de repo aan te maken.

            if (withUnitOfWork)
            {
                if (uowManager == null)
                {
                    uowManager = new UnitOfWorkManager();
                    repo = new EntiteitRepo(uowManager.UnitOfWork);
                }
            }
            // Als we niet met UoW willen werken, dan maken we een repo aan als die nog niet bestaat.
            else
            {
                repo = (repo == null) ? new DAL.EntiteitRepo() : repo;
            }
        }

        public bool berekenTrends(int minVoorwaarde, Entiteit entiteit, TrendType type, Domain.Alert.Voorwaarde voorwaarde)
        {
            initNonExistingRepo();
            //DateTime vandaag = DateTime.Today;
            //DateTime gisteren = DateTime.Today.AddDays(-1);
            DateTime vandaag = new DateTime(2018, 01, 14);
            DateTime gisteren = new DateTime(2018, 01, 13);
            List<Post> AllePosts = entiteit.Posts;
            List<Post> PostsGisteren = AllePosts.Where(x => x.Date.Day == gisteren.Day).Where(x => x.Date.Month == gisteren.Month).Where(x => x.Date.Year == gisteren.Year).ToList();
            List<Post> PostsVandaag = AllePosts.Where(x => x.Date.Day == vandaag.Day).Where(x => x.Date.Month == vandaag.Month).Where(x => x.Date.Year == vandaag.Year).ToList();
            int AantalGisteren = PostsGisteren.Count;
            int AantalVandaag = PostsVandaag.Count;
            //We MOETEN entiteit even zelf ophalen zodat de context op de hoogte is van welke entiteit we gebruiken
            Entiteit p = getAlleEntiteiten().Single(x => x.EntiteitId == entiteit.EntiteitId);
            Trend newTrend = new Trend();

            double trendVerandering = 1.3;


            //controle of trend al bestaat, zoja moeten we de berekening niet maken
            foreach (var trend in p.Trends)
            {
                if (trend.Type == type)
                {
                    return true;
                }
            }

            //PRESET voor berekening juist zetten
            switch (type)
            {
                case TrendType.STERKOPWAARDS:
                    trendVerandering = 1.3;
                    newTrend.Type = TrendType.STERKOPWAARDS;
                    break;
                case TrendType.MATIGOPWAARDS:
                    trendVerandering = 1.1;
                    newTrend.Type = TrendType.MATIGOPWAARDS;
                    break;
                case TrendType.MATIGDALEND:
                    trendVerandering = 0.9;
                    newTrend.Type = TrendType.MATIGDALEND;
                    break;
                case TrendType.STERKDALEND:
                    trendVerandering = 0.7;
                    newTrend.Type = TrendType.STERKDALEND;
                    break;
            }

            switch (voorwaarde)
            {
                case Voorwaarde.SENTIMENT:
                    if(type == TrendType.STIJGEND)
                    {
                        double sentimentGisteren = 0;
                        double sentimentVandaag = 0;

                        foreach (var post in PostsGisteren)
                        {
                            sentimentGisteren += (post.sentiment.polariteit * post.sentiment.objectiviteit) / AantalGisteren;
                        }

                        foreach (var post in PostsVandaag)
                        {
                            sentimentVandaag += (post.sentiment.polariteit * post.sentiment.objectiviteit) / AantalVandaag;
                        }
                        double sentimentVerschil = sentimentVandaag - sentimentGisteren;
                        if (sentimentVerschil >= minVoorwaarde)
                        {
                            newTrend.Type = TrendType.STIJGEND;
                            entiteit.Trends.Add(newTrend);
                            UpdateEntiteit(entiteit);
                            return true;
                        }
                    }
                    break;
                case Voorwaarde.AANTALVERMELDINGEN:
                    if(type == TrendType.STIJGEND)
                    {
                        if ((AantalVandaag - AantalGisteren) >= minVoorwaarde)
                        {
                            return true;
                        }
                    }
                    if (type == TrendType.DALEND)
                    {
                        if ((AantalGisteren - AantalVandaag) >= minVoorwaarde)
                        {
                            return true;
                        }
                    }
                    if (type == TrendType.STERKOPWAARDS)
                    {
                        if ((AantalVandaag / AantalGisteren) >= trendVerandering)
                        {
                            if (entiteit.Trends == null)
                            {
                                entiteit.Trends = new List<Trend>();
                            }
                            entiteit.Trends.Add(newTrend);
                            UpdateEntiteit(p);
                            return true;
                        }
                    }
                    break;
                case Voorwaarde.KEYWORDS:
                    break;
                default:
                    break;
            }
            
            //ALS het een type stijging is, dit is een waarde die de user heeft opgegeven kijken we of deze stijging voldoet aan de voorwaarde.
            if (type == TrendType.STIJGEND)
            {
                if ((AantalVandaag - AantalGisteren) >= minVoorwaarde)
                {
                    return true;
                }
            }
            //zelde als bij stijging maar hier kijken we of de daling voldoet.
            else if(type == TrendType.DALEND)
            {
                if ((AantalGisteren - AantalVandaag) >= minVoorwaarde)
                {
                    return true;
                }
            }
            else
            {
                if ((AantalVandaag / AantalGisteren) >= trendVerandering)
                {
                    if (entiteit.Trends == null)
                    {
                        entiteit.Trends = new List<Trend>();
                    }
                    entiteit.Trends.Add(newTrend);
                    UpdateEntiteit(p);
                    return true;
                }
            }
            //als we hier komen wil het zeggen dat er geen trend aanwezig is dus moet de alert niet getriggered worden.
            return false;
        }

        public bool berekenTrends(int minVoorwaarde, List<Post> posts, Voorwaarde voorwaarde)
        {
            throw new NotImplementedException();
        }

        public void addTrend(Trend trend)
        {
            repo.addTrend(trend);
        }
    }
}
