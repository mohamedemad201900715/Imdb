using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace imdb.Models
{
    public class movie
    {
        [Key]
        public int id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "the string length must be 50")]
        public string name { get; set; }
        [FileExtensions(Extensions = "jpg,jpeg,png")]
        [DataType(DataType.ImageUrl)]
        public string photo_movie { get; set; }
        [Required]
        [StringLength(200)]
        public string description { get; set; }
        public int id_director { get; set; }
        public virtual director director { get; set; }

        public virtual int act_In_Mov_id { get; set; }
        public virtual List<act_in_mov> act_In_Mov { get; set; }
    }
}