using MyBlogProject.DAL.EF;
using MyBlogProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogProject.BLL
{
    public class Test
    {
        Repository<User> repo_user = new Repository<User>();
        Repository<Comment> repo_comment = new Repository<Comment>();
        Repository<Blog> repo_blog = new Repository<Blog>();
        public Test()
        {

        }
        public void InsertTest()
        {


            int result = repo_user.Insert(new User()
            {
                Name = "aaa",
                Surname = "bbb",
                Email = "aaa@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                Username = "aaa",
                Password = "123",
                CreateOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUsername = "aaa"

            });
        }
        public void UpdateTest()
        {
            User user = repo_user.Find(x => x.Username == "aaa");
            if (user != null)
            {
                user.Username = "xxx";
                int result = repo_user.Update(user);
            }
        }

        public void CommentTest()
        {
            User user = repo_user.Find(i => i.Id == 1);
            Blog blog = repo_blog.Find(i => i.Id == 3);
            Comment comment = new Comment()
            {
                Text = "Bu bir testtir.",
                CreateOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                ModifiedUsername = "emre",
                Blog = blog,
                Owner = user,
            };
            repo_comment.Insert(comment);
        }
    }
}
