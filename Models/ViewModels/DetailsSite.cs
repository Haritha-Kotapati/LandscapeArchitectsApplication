using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LandscapeArchitectsApplication.Models.ViewModels
{
    public class DetailsSite
    {
        public SiteDto SelectedSite { get; set; }
        public IEnumerable<PlantMaterialDto> ExistingPlants { get; set; }

        public IEnumerable<PlantMaterialDto> AvailablePlants { get; set; }
        //public IEnumerable
    }
}