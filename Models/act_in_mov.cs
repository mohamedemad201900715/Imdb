using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace imdb.Models
{
    public class act_in_mov
    {
        public int id { get; set; }
        public int id_mov { set; get; }
        public int id_act { get; set; }
        public virtual actor actors { get; set; }
        public virtual movie movies { get; set; }
    }
}