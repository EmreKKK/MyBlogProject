using MyBlogProject.BLL;
using MyBlogProject.Entity;
using MyBlogProject.Entity.Messages;
using MyBlogProject.Entity.ViewModels;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyBlogProject.UI.Controllers
{
    public class HomeController : Controller
    {
        BlogManager bm = new BlogManager();
        CategoryManager cm = new CategoryManager();
        UserManager um = new UserManager();

        // GET: Home
        public ActionResult Index()
        {
            //MyBlogProject.BLL.Test test = new MyBlogProject.BLL.Test();
            //test.InsertTest();
            //test.UpdateTest();
            //test.CommentTest();
            //if (TempData["blogs"] != null)
            //{
            //    return View(TempData["blogs"] as List<Blog>);
            //}


            //return View(bm.GetAllBlogs().OrderByDescending(i => i.ModifiedOn).ToList());

            return View(bm.GetAllBlogsQueryable().Include("Owner").OrderByDescending(i => i.ModifiedOn).ToList());
        }

        public ActionResult ByCategory(int? id)
        {

            // GET: Category
            // PartialCategories içerisindeki adresi değiştirildi ve CategoryController Kapatıldı.
            // HATA !!!: KATEGORİLERE HER TIKLANDIĞINDA SAYFA EN TEPEYE ATIYOR, TEMPDATA YÖNTEMİNDE BU OLMAMIŞTI.

            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }


                Category cat = cm.GetCategoryById(id.Value);

                if (cat == null)
                {
                    return HttpNotFound();
                }


                return View("Index", cat.Blogs.OrderByDescending(i => i.ModifiedOn).ToList());
            }

        }

        public ActionResult MostLiked()
        {
            return View("Index", bm.GetAllBlogsQueryable().OrderByDescending(i => i.LikeCount).ToList());
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<User> res = um.LoginUser(model);


                if (res.Errors.Count > 0)
                {
                    if (res.Errors.Find(x => x.Code == ErrorMessageCode.UserIsNotActive) != null)
                    {
                        ViewBag.Setlink = "http://localhost:57227/Home/Activate/1234-5678-9999";
                    }
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }

                Session["login"] = res.Result;
                return RedirectToAction("Index");
            }

            return View(model);
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                #region controlmailerror

                //if (model.Username == "emrekocak")
                //{
                //    ModelState.AddModelError("Username", "This username is already taken");
                //}
                //if (model.Email == "emrekocak@gmail.com")
                //{
                //    ModelState.AddModelError("Email", "This email is already taken");
                //}
                //foreach (var item in ModelState)
                //{
                //    if (item.Value.Errors.Count > 0)
                //    {
                //        return View(model);
                //    }
                //}
                #endregion

                #region Control
                //try
                //{
                //    um.RegisterUser(model);
                //}
                //catch (Exception ex)
                //{
                //    ModelState.AddModelError("", ex.Message);
                //}

                #endregion


                BusinessLayerResult<User> res = um.RegisterUser(model);

                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }

                return RedirectToAction("RegisterOK");
            }




            return View(model);
        }

        public ActionResult RegisterOK()
        {
            return View();
        }
        public ActionResult UserActivate(Guid id)
        {
            BusinessLayerResult<User> res = um.ActivateUser(id);

            if (res.Errors.Count > 0)
            {
                TempData["errors"] = res.Errors;

                return RedirectToAction("UserActivateCancel");
            }
            return RedirectToAction("UserActivateOK");
        }

        public ActionResult UserActivateOK()
        {
            return View();
        }

        public ActionResult UserActivateCancel()
        {
            List<ErrorMessageObj> errors = null;

            if (TempData["errors"] != null)
            {
                errors = TempData["errors"] as List<ErrorMessageObj>;
            }

            return View(errors);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult ShowProfile()
        {
            return View();
        }

        public ActionResult EditProfile()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EditProfile(User user)
        {
            return View();
        }

        public ActionResult DeleteProfile()
        {
            return View();
        }
    }
}