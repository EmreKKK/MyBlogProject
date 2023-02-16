using MyBlogProject.BLL.Abstract;
using MyBlogProject.DAL.EF;
using MyBlogProject.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogProject.BLL
{
    public class BlogManager : ManagerBase<Blog>
    {
        //public override int Delete(Blog blogg)
        //{
        //    BlogManager blogManager = new BlogManager();
        //    LikeManager likeManager = new LikeManager();
        //    CommentManager commentManager = new CommentManager();


        //    Blog blog = blogManager.ListQueryable().Include("Comments").Include("Likes").FirstOrDefault(x => x.Id == blogg.Id);
        //    foreach (Comment comments in blog.Comments.ToList())
        //    {
        //        commentManager.Delete(comments);
        //    }
        //    foreach (Liked likes in blog.Likes.ToList())
        //    {
        //        likeManager.Delete(likes);
        //    }
        //    blogManager.Delete(blog);

        //    return base.Delete(blogg);
        //}
    }
}

