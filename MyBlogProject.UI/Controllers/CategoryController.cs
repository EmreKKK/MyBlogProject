using MyBlogProject.BLL;
using MyBlogProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyBlogProject.UI.Views.Home
{
    //public class CategoryController : Controller
    //{
    //    // GET: Category
    //    public ActionResult Select(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //        }
          
    //        CategoryManager cm = new CategoryManager();
    //        Category cat =cm.GetCategoryById(id.Value);

    //        if (cat == null)
    //        {
    //            return HttpNotFound();
    //        }

            

    //        TempData["blogs"] = cat.Blogs; // TempData Action'lar arası veri taşımayı sağlar; view,viewbag aksine.

    //        return RedirectToAction("Index", "Home");
    //    }
    //}
}