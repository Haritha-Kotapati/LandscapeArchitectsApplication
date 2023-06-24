using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LandscapeArchitectsApplication.Models
{
    public class PlantMaterial
    {
        [Key]
        public int Plant_Id { get; set; }
        public string Plant_Name { get; set; }
        public string Plant_Type { get; set; }

        //Plant material can be used in many sites
        public ICollection<Site> Sites { get; set; }

    }

    public class PlantMaterialDto
    {
        public int Plant_Id { get; set; }
        public string Plant_Name { get; set; }
        public string Plant_Type { get; set; }
    }
}