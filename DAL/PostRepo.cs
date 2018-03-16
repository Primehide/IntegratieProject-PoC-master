using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Posts;
using DAL.EF;
using System.Data.Entity;

namespace DAL
{
    public class PostRepo : IPostRepo
    {
        private readonly EFContext ctx;

        public PostRepo()
        {
            ctx = new EFContext();
        }

        public PostRepo(UnitOfWork uow)
        {
            ctx = uow.Context;
        }

        public void addPosts(List<Post> Posts)
        {
            ctx.Posts.AddRange(Posts);
            ctx.SaveChanges();
        }

        public List<Post> AllePosts()
        {
            return ctx.Posts.Include(x => x.Persoon).Include(x => x.Naam).ToList();
        }

        public void updatePost(Post postToUpdate)
        {
            ctx.Entry(postToUpdate).State = EntityState.Modified;
            ctx.SaveChanges();
        }
    }
}
