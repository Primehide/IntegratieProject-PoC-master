using System.Collections.Generic;
using System.Linq;
using Domain.Posts;
using DAL;
using System.IO;
using Newtonsoft.Json;
using Domain.DTO;
using Domain.Entiteit;

namespace BL
{
    public class PostMgr : IPostMgr
    {
        private IPostRepo repo;
        private UnitOfWorkManager uowManager;

        public PostMgr()
        {

        }

        public PostMgr(UnitOfWorkManager uofMgr)
        {
            uowManager = uofMgr;
            repo = new PostRepo(uowManager.UnitOfWork);
        }

        public void AddPosts(List<Post> Posts)
        {
            initNonExistingRepo();
            repo.addPosts(Posts);
        }

        public List<Post> AllePosts()
        {
            initNonExistingRepo();
            return repo.AllePosts();
        }

        //zet json om naar onze objecten en schrijft ze weg naar de databank.
        public void ConvertJsonPosts()
        {
            initNonExistingRepo();
            using (StreamReader r = new StreamReader(@"C:\Users\Sander\Desktop\IntegratieProject-PoC-master\BL\TestData\textgaindump.json"))
            {
                var json = r.ReadToEnd();

                //json omzetten in een data object
                RecordDTO recordDTO = JsonConvert.DeserializeObject<RecordDTO>(json);
                //een object aanmaken hoe dat wij het gaan gebruiken
                record record = new record();
                record.records = new List<Post>();

                //elk DTO object omzetten naar ons object
                foreach (var post in recordDTO.records)
                {
                    Post postToAdd = new Post()
                    {
                        Date = post.Date,
                        geo = post.geo,
                        retweet = post.retweet,
                        source = post.source,
                        HashTags = new List<Hashtag>(),
                        Words = new List<Word>(),
                        Naam = new Naam(),
                        sentiment = new sentiment(),
                        urls = new List<url>(),
                        mentions = new List<Mention>(),
                        id = post.id
                    };
                    postToAdd.Naam.Voornaam = post.Politician.First();
                    postToAdd.Naam.Achternaam = post.Politician.Last();

                    //zet voorlopig gewoon ruw de data om
                    postToAdd.sentiment.polariteit = post.sentiment.First();
                    postToAdd.sentiment.objectiviteit = post.sentiment.Last();



                    //hash tags omzetten naar ons object
                    foreach (var hashtag in post.HashTags)
                    {
                        Hashtag h = new Hashtag()
                        {
                            tag = hashtag
                        };
                        postToAdd.HashTags.Add(h);
                    }

                    //words omzetten naar ons object
                    foreach (var word in post.Words)
                    {
                        Word w = new Word()
                        {
                            word = word
                        };
                        postToAdd.Words.Add(w);
                    }

                    foreach (var url in post.urls)
                    {
                        url u = new url()
                        {
                            URL = url
                        };
                        postToAdd.urls.Add(u);
                    }

                    foreach (var mention in post.mentions)
                    {
                        Mention m = new Mention()
                        {
                            mention = mention
                        };
                        postToAdd.mentions.Add(m);
                    }

                    record.records.Add(postToAdd);
                }
                //wegschrijven naar onze databank
                AddPosts(record.records);
            }
        }

        public List<Post> getPostsOnEntiteit(Entiteit entiteit)
        {
            return AllePosts().Where(x => x.Persoon == entiteit).ToList();
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
                    repo = new PostRepo(uowManager.UnitOfWork);
                }
            }
            // Als we niet met UoW willen werken, dan maken we een repo aan als die nog niet bestaat.
            else
            {
                repo = (repo == null) ? new PostRepo() : repo;
            }
        }
    }
}
