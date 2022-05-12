using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace imdb.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "the string length must be 50")]
        public string FristName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "the string length must be 50")]
        public string lastName { get; set; }
        [FileExtensions(Extensions = "jpg,jpeg,png")]
        [DataType(DataType.ImageUrl)]
        public string Photo_user { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "the string length must be 50")]
        public string username { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "the string length must be 50")]
        public string password { get; set; }
        public virtual List<fav_movies> Fav_Movies { get; set; }
        public virtual List<fav_actor> Fav_Actors { get; set; }
        public virtual List<fav_dir> Fav_Directors { get; set; }

    }
}