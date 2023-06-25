using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LandscapeArchitectsApplication.Models.ViewModels
{
    public class UpdatePlantMaterial
    {
        public PlantMaterialDto SelectedPlantMaterial { get; set; }

        public string Plant_Id { get; set; }
        public IEnumerable<SiteDto> SiteOptions { get; set; }
    }
}