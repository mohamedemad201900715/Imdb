using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace imdb.Models
{
    public class fav_actor
    {
        public int id { get; set; }
        public int id_User { get; set; }
        public int id_actor { get; set; }
        public virtual User User { get; set; }
        public virtual actor Actor { get; set; }

    }
}