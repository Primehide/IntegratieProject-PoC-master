using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class PostDTO
    {
        public List<String> HashTags { get; set; }
        public List<String> Words { get; set; }
        public DateTime Date { get; set; }
        public List<string> Politician { get; set; } //verwacht ne lijst maar ze geven gewoon voor en achternaam terug, raar ze
        public string geo { get; set; }
        public string id { get; set; }
        public List<double> sentiment { get; set; }
        public bool retweet { get; set; }
        public string source { get; set; }
        public List<string> urls { get; set; }
        public List<string> mentions { get; set; }
    }
}
