using MyBlogProject.BLL.Abstract;
using MyBlogProject.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogProject.BLL
{
    public class CategoryManager:ManagerBase<Category>
    {
        public override int Delete(Category cat)
        {
            BlogManager blogManager = new BlogManager();
            LikeManager likeManager = new LikeManager();
            CommentManager commentManager = new CommentManager();  

            foreach(Blog item in cat.Blogs.ToList())
            {

                Blog blog = blogManager.ListQueryable().Include("Comment").Include("Likes").FirstOrDefault(x=>x.Id==item.Id);
                foreach(Comment comments in blog.Comments.ToList())
                {
                    commentManager.Delete(comments);
                }
                foreach(Liked likes in blog.Likes.ToList())
                {
                    likeManager.Delete(likes);
                }
                blogManager.Delete(blog);
            }
            return base.Delete(cat);
        }
    }
}
