using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LandscapeArchitectsApplication.Models.ViewModels
{
    public class DetailsLandscapeDesign
    {
        public LandscapeDesignDto SelectedDesign { get; set; }

        public IEnumerable<SiteDto> RelatedSites { get; set; }
    }
}