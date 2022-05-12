using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using imdb.Context;
using imdb.Models;

namespace imdb.Controllers
{
    public class HomeController : Controller
    {
        private DataContext db = new DataContext();
        public ActionResult Index()
        {
            Session["Userid"] = "0";
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            string username = form["uname"].ToString();
            string password = form["pass"].ToString();
            
            User user = db.users.Where(
                    m => m.username == username && m.password == password
                    ).First();

                if (user == null)
                {
                    Session["Userid"] = "0";
                    return View();
                }

                Session["Userid"] = user.id.ToString();
            
            return RedirectToAction("index","ProFile");
        }



        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(FormCollection form , HttpPostedFileBase photo)
        {
            User user = new User();
            user.FristName = form["Fname"].ToString();
            user.lastName  = form["Lname"].ToString();
            user.username  = form["uname"].ToString();
            user.password  = form["pass"].ToString();

            HttpPostedFileBase postedFile = Request.Files["photo"];
            
            string path = Server.MapPath("~/Uploads/");
            
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            
            postedFile.SaveAs(path + Path.GetFileName(postedFile.FileName));

            user.Photo_user = "/Uploads/" + Path.GetFileName(postedFile.FileName);

            db.users.Add(user);
            db.SaveChanges();
            ViewBag.mss = "your acount is created ";

            return RedirectToAction("Index");
        }

    }
}