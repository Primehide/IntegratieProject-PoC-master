using System.Collections.Generic;
using Domain.Posts;

namespace DAL
{
    public interface IPostRepo
    {
        void addPosts(List<Post> Posts);
        List<Post> AllePosts();
        void updatePost(Post postToUpdate);
    }
}
