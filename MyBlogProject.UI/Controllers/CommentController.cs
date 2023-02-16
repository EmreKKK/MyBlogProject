using MyBlogProject.BLL;
using MyBlogProject.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyBlogProject.UI.Controllers
{
    public class CommentController : Controller
    {
        private BlogManager blogManager = new BlogManager();
        // GET: Comment
        public ActionResult ShowBlogComments(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = blogManager.ListQueryable().Include("Comment").FirstOrDefault(x => x.Id == id);
            //Blog blog = blogManager.ListQueryable().Include("Comment").Where(x => x.Id == blogid).FirstOrDefault();

            if (blog == null)
            {
                return HttpNotFound();
            }
            return PartialView("_PartialComments", blog.Comments);
        }
    }
}