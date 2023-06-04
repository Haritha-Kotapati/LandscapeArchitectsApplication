using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LandscapeArchitectsApplication.Models
{
    public class Site
    {
        [Key]
        public int SiteID { get; set; }
        public string ClientName { get; set; }
        public string Address { get; set; }

        //An site belongs to one LandscapeDesign
        //A LandscapeDesign can belong to one site.
        [ForeignKey("LandscapeDesign")]
        public int DesignID { get; set; }
        public virtual LandscapeDesign LandscapeDesign {get; set; }

        //a plant material canbe used in many sites
        public ICollection<PlantMaterial> PlantMaterials { get; set; }

    }
}