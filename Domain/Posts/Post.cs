using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Posts
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        public List<Hashtag> HashTags { get; set; }
        public List<Word> Words { get; set; }
        public DateTime Date { get; set; }
        public Entiteit.Persoon Persoon { get; set; } //LINK DIT AAN EEN ENTITEIT DIE WIJ VOLGEN
        public Naam Naam { get; set; }
        public string geo { get; set; }
        public string id { get; set; }
        public sentiment sentiment { get; set; }
        public bool retweet { get; set; }
        public string source { get; set; }
        public List<url> urls { get; set; }
        public List<Mention> mentions { get; set; }
    }
}
