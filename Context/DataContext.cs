using imdb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace imdb.Context
{
    public class DataContext : DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<movie> movies { get; set; }
        public DbSet<actor> actors { get; set; }
        public DbSet<director> directors { get; set; }
        public DbSet<act_in_mov> act_In_Movs { get; set; }
        public DbSet<fav_movies> Fav_Movies { get; set; }
        public DbSet<fav_dir> Fav_Directors { get; set; }
        public DbSet<fav_actor> Fav_Actors { get; set; }
    }
}