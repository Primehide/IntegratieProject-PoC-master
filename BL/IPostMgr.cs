using System.Collections.Generic;
using Domain.Posts;
using Domain.Entiteit;
using Domain.Alert;

namespace BL
{
    public interface IPostMgr
    {
        void AddPosts(List<Post> Posts);
        void ConvertJsonPosts();
        List<Post> AllePosts();
        List<Post> getPostsOnEntiteit(Entiteit entiteit);
    }
}
