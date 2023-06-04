using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LandscapeArchitectsApplication.Models
{
    public class LandscapeDesign
    {
        [Key]
        public int DesignID { get; set; }
        public string LeadArhitect { get; set; }
        public DateTime DateCreated { get; set; } 

        
    }
}