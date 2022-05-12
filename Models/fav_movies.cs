using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using imdb.Models;
namespace imdb.Models
{
    public class fav_movies
    {
        public int id { get; set; }
        public int id_User { get; set; }
        public int id_movie { get; set; }
        public virtual User User { get; set; }
        public virtual movie Movie { get; set; }
    }
}