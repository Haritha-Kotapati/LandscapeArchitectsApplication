using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LandscapeArchitectsApplication.Models.ViewModels
{
    public class UpdateSite
    {
        //This viewmodel is a class which stores information that we need to present to /Site/Update/{}

        //The existing LandscapeDesign information
        public SiteDto SelectedSite { get; set; }
        public string SiteID { get; set; }
        //all architects to choose from when updating this site

        public IEnumerable<LandscapeDesignDto> LandscapeDesignOptions { get; set; }
    }
}