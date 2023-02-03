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
    public class BlogManager
    {
        private Repository<Blog> repo_blog = new Repository<Blog>();

        public List<Blog> GetAllBlogs()
        {

            return repo_blog.List();
        }
        public IQueryable<Blog> GetAllBlogsQueryable()
        {
            var blogs = repo_blog.ListQueryable();
            return blogs.Include("Owner");
        }

    }
}
