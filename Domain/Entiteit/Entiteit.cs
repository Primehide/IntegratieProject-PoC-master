using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Posts;

namespace Domain.Entiteit
{
    public class Entiteit
    {
        public int EntiteitId { get; set; }
        public List<Post> Posts { get; set; }
        public List<Trend> Trends { get; set; }
    }
}
