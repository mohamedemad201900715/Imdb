using imdb.Context;
using imdb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net;

using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace imdb.Controllers
{
    public class ProFileController : Controller
    {
        private DataContext db = new DataContext();
        public ActionResult index()
        {
            if (Session["Userid"] == "0" || Session["Userid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult MyProfile()
        {
            if (Session["Userid"] == "0" || Session["Userid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int x = Convert.ToInt16(Session["Userid"]);
            return View(db.users.Find(x));

        }
        
        public ActionResult Updateinfo()
        {
            int x = Convert.ToInt16(Session["Userid"]);
            var user = db.users.FirstOrDefault(a => a.id == x);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        [HttpPost]
        public ActionResult Updateinfo(HttpPostedFileBase photo, User user) {
            var UserInDb = db.users.ToList().First(a => a.id == user.id);
            UserInDb.FristName = user.FristName;
            UserInDb.lastName = user.lastName;
            HttpPostedFileBase postedFile = Request.Files["photo"];
            if (postedFile != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                postedFile.SaveAs(path + Path.GetFileName(postedFile.FileName));

                user.Photo_user = "/Uploads/" + Path.GetFileName(postedFile.FileName);
                UserInDb.Photo_user = user.Photo_user;
            }
            db.SaveChanges();
            int x = Convert.ToInt16(Session["Userid"]);
            return View(db.users.Find(x));
        }
    public ActionResult myfavMovies()
        {
            if (Session["Userid"] == "0" || Session["Userid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            int x = Convert.ToInt16(Session["Userid"]);
            return View(db.users.Find(x));
        }
        [System.Web.Http.Route("DeleteFavMovie/{id}")]
        public ActionResult DeleteFavMovie(int id)
        {
            var movieInDb = db.Fav_Movies.FirstOrDefault(c => c.id == id );
            db.Fav_Movies.Remove(movieInDb);
            db.SaveChanges();
           return RedirectToAction("myfavMovies", "profile");
        }
        [System.Web.Http.Route("DeleteFavactor/{id}")]
        public ActionResult DeleteFavactor(int id)
        {
            var ActorInDb = db.Fav_Actors.FirstOrDefault(c => c.id == id);
            db.Fav_Actors.Remove(ActorInDb);
            db.SaveChanges();
            return RedirectToAction("myfavActs", "profile");
        }
        [System.Web.Http.Route("DeleteFavdir/{id}")]
        public ActionResult DeleteFavdir(int id)
        {
            var DirInDb = db.Fav_Directors.FirstOrDefault(c => c.id == id);
            db.Fav_Directors.Remove(DirInDb);
            db.SaveChanges();
            return RedirectToAction("myfavDirs", "profile");
        }
        public ActionResult myfavDirs()
        {
            if (Session["Userid"] == "0" || Session["Userid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            int x = Convert.ToInt16(Session["Userid"]);
            return View(db.users.Find(x));
        }
        public ActionResult myfavActs()
        {
            if (Session["Userid"] == "0" || Session["Userid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            int x = Convert.ToInt16(Session["Userid"]);
            return View(db.users.Find(x));
        }
        public ActionResult myfav()
        {
            if (Session["Userid"] == "0" || Session["Userid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            int x = Convert.ToInt16(Session["Userid"]);

            return View(db.users.Find(x));
        }
        [HttpPost]
        public ActionResult addfav(FormCollection form)
        {
            int i = Convert.ToInt16(form["mo"]);
            int x = Convert.ToInt16(Session["Userid"]);
            try
            {
                db.Fav_Movies.RemoveRange(
                    db.Fav_Movies.Where(m => m.id_User == x && m.id_movie == i));

                fav_movies fav = new fav_movies();

                fav.id_movie = i;
                fav.id_User = x;
                fav.Movie = db.movies.Find(i);
                fav.User = db.users.Find(x);

                db.Fav_Movies.Add(fav);

                db.SaveChanges();
            }
            catch
            {

            }
            return RedirectToAction("AllMovie");
        }
        public ActionResult addfavAcors(FormCollection form)
        {
            int i = Convert.ToInt16(form["mo"]);
            int x = Convert.ToInt16(Session["Userid"]);
            try
            {
                db.Fav_Actors.RemoveRange(
                    db.Fav_Actors.Where(m => m.id_User == x && m.id_actor == i));

                fav_actor fav = new fav_actor();

                fav.id_actor = i;
                fav.id_User = x;
                fav.Actor = db.actors.Find(i);
                fav.User = db.users.Find(x);

                db.Fav_Actors.Add(fav);

                db.SaveChanges();
            }
            catch
            {

            }
            return RedirectToAction("AllActor");
        }
        public ActionResult addfavdirs(FormCollection form)
        {
            int i = Convert.ToInt16(form["mo"]);
            int x = Convert.ToInt16(Session["Userid"]);
            try
            {
                db.Fav_Directors.RemoveRange(
                    db.Fav_Directors.Where(m => m.id_User == x && m.id_director == i));

                fav_dir fav = new fav_dir();

                fav.id_director = i;
                fav.id_User = x;
                fav.Director = db.directors.Find(i);
                fav.User = db.users.Find(x);

                db.Fav_Directors.Add(fav);

                db.SaveChanges();
            }
            catch
            {

            }
            return RedirectToAction("Alldirectors");
        }


        public ActionResult searsh(FormCollection form)
        {
            string name = form["nmovie"].ToString();
            return View(db.movies.Where(m => m.name == name).ToList());
        }





        public ActionResult AllMovie()
        {
            if (Session["Userid"] == "0" || Session["Userid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(db.movies.ToList());
        }
        public ActionResult oneMovie(int? id)
        {
            if (Session["Userid"] == "0" || Session["Userid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int idi = Convert.ToInt32(id);
            return View(db.movies.Find(id));
        }

        public ActionResult Alldirectors()
        {
            if (Session["Userid"] == "0" || Session["Userid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(db.directors.ToList());
        }
        public ActionResult oneDirector(int? id)
        {
            if (Session["Userid"] == "0" || Session["Userid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int idi = Convert.ToInt32(id);
            return View(db.directors.Find(idi));
        }

        public ActionResult AllActor()
        {
           if (Session["Userid"] == "0" || Session["Userid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(db.actors.ToList());
        }
        public ActionResult oneActor(int? id)
        {
            if (Session["Userid"] == "0" || Session["Userid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int idi = Convert.ToInt32(id);
            return View(db.actors.Find(id));
        }
    }
}