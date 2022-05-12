using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace imdb.Models
{
    public class fav_dir
    {
        public int id { get; set; }
        public int id_User { get; set; }
        public int id_director { get; set; }
        public virtual User User { get; set; }
        public virtual director Director { get; set; }
    }
}