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


    public class acts_in_movie
    {
        public movie movie1 = new movie();
        public List<actor> actors = new List<actor>();
    } 

    public class AdminController : Controller
    {

        private DataContext db = new DataContext();
        // GET: Admin
        public ActionResult Index()
        {
            Session["Userid"] = "0";
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            string username = form["Usename"].ToString();
            string password = form["Password"].ToString();

            if(username == "admin" && password =="admin"){
                Session["Userid"] = "1";
            }
            return RedirectToAction("AdminActions","Admin");
        }

        public ActionResult AdminActions()
        {
            if (Session["Userid"].Equals("0") )
            {
                return RedirectToAction("Index", "Admin");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Addmovie()
        {
            if (Session["Userid"].Equals("0") || Session["Userid"] == null || !Session["Userid"].Equals("1"))
            {
                return RedirectToAction("Index", "Admin");
            }

            return View(db.directors.ToList());
        }



        [HttpPost]
        public ActionResult Addmovie(FormCollection form , HttpPostedFileBase photo)
        {
            movie movie = new movie();

            movie.name = form["name"].ToString();
            movie.description = form["description"].ToString();


            HttpPostedFileBase postedFile = Request.Files["photo"];
            if (postedFile != null ){
                string path = Server.MapPath("~/movie/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                postedFile.SaveAs(path + Path.GetFileName(postedFile.FileName));
            }
            movie.photo_movie = "/movie/" + Path.GetFileName(postedFile.FileName);

            int id_director = Convert.ToInt32(form["director"]);

            movie.director = db.directors.Find(id_director);
            movie.id_director = id_director;
            
            db.movies.Add(movie);
            db.SaveChanges();
            return View(db.directors.ToList());
        }


        public ActionResult Add_Dir()
        {

            return View();
        }


        [HttpPost]
        public ActionResult Add_Dir( FormCollection form, HttpPostedFileBase photo)
        {
            string name = form["name"].ToString();

            HttpPostedFileBase postedFile = Request.Files["photo"];
            
            string path = Server.MapPath("~/Uploads_dir/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            postedFile.SaveAs(path + Path.GetFileName(postedFile.FileName));

            director director = new director();
            director.name = name;
            director.photo_dir = "/Uploads_dir/" + Path.GetFileName(postedFile.FileName);

            db.directors.Add(director);
            db.SaveChanges();

            return View();
        }
        
        public ActionResult AllMovie()
        {
            return View(db.movies.ToList());
        }

        public ActionResult one_movie(int id)
        {
            int idi = Convert.ToInt32(id);
           
            acts_in_movie acts_In_Movie = new acts_in_movie();
            
            acts_In_Movie.movie1 = db.movies.Find(idi);
            acts_In_Movie.actors = db.actors.ToList();

            return View(acts_In_Movie);
        }

        [HttpPost]
        public ActionResult one_movie( int? id , FormCollection form)
        {
            int idi = Convert.ToInt32(id);
            int id_act = Convert.ToInt32(form["act"]);


            act_in_mov act_In_Mov = new act_in_mov();

            act_In_Mov.id_act = id_act;
            act_In_Mov.actors = db.actors.Find(id_act);

            act_In_Mov.id_mov = idi;
            act_In_Mov.movies = db.movies.Find(idi);


            db.act_In_Movs.RemoveRange(
                db.act_In_Movs.Where(m => m.id_act == id_act && m.id_mov == idi).ToList());

            db.act_In_Movs.Add(act_In_Mov);
            db.SaveChanges();

            acts_in_movie acts_In_Movie = new acts_in_movie();
            acts_In_Movie.movie1 = db.movies.Find(idi);
            acts_In_Movie.actors = db.actors.ToList();
            List<act_in_mov> act_Movs = db.act_In_Movs.Where(m => m.id_mov == idi).ToList();



            try
            {
                foreach (var item in act_Movs)
                {
                    act_Movs.Remove(item);
                }
            }
            catch
            {

            }

            return View(acts_In_Movie);
        }

        public ActionResult addact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult addact(FormCollection form, HttpPostedFileBase photo)
        {
            string name = form["name"].ToString();

            HttpPostedFileBase postedFile = Request.Files["photo"];

            string path = Server.MapPath("~/Uploads_acr/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            postedFile.SaveAs(path + Path.GetFileName(postedFile.FileName));

            actor actor = new actor();
            actor.name = name;
            actor.photo_act = "/Uploads_dir/" + Path.GetFileName(postedFile.FileName);

            db.actors.Add(actor);
            db.SaveChanges();

            return View();
        }
    }
}