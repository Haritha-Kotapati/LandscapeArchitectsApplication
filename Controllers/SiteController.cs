using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using LandscapeArchitectsApplication.Models;
using LandscapeArchitectsApplication.Models.ViewModels;
using System.Web.Script.Serialization;


namespace LandscapeArchitectsApplication.Controllers
{
    public class SiteController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
       
        static SiteController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44344/api/");
        }

        // GET: Site/List
        public ActionResult List()
        {
            string url = "SitesData/ListSites";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<SiteDto> sites = response.Content.ReadAsAsync<IEnumerable<SiteDto>>().Result;

            return View(sites);
        }

        // GET: Site/Details/5
        public ActionResult Details(int id)
        {
            DetailsSite ViewModel = new DetailsSite();

          //Objective: communicate with out site data api to retrieve one site

            string url = "SitesData/FindSite/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            SiteDto SelectedSite = response.Content.ReadAsAsync<SiteDto>().Result;

            ViewModel.SelectedSite = SelectedSite;
            return View(ViewModel);
        }

        // GET: Site/New
        public ActionResult New()
        {
            //information about all Designs in the system.
            //GET api/LandscapeDesignsData/ListLandscapeDesigns

            string url = "LandscapeDesignsData/ListLandscapeDesigns/";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<LandscapeDesignDto> LandscapeDesignOptions = response.Content.ReadAsAsync<IEnumerable<LandscapeDesignDto>>().Result;

            return View(LandscapeDesignOptions);
            
        }

        // POST: Site/Create
        [HttpPost]
        public ActionResult Create(Site site)
        {
            string url = "SitesData/AddSite/";

            string jsonpayload = jss.Serialize(site);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            { 
                return RedirectToAction("Error");
            
            
            }
        }

        // GET: Site/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateSite ViewModels = new UpdateSite();
            
            //the existing site information
            string url = "SitesData/FindSite/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            SiteDto SelectedSite = response.Content.ReadAsAsync<SiteDto>().Result;
            ViewModels.SelectedSite = SelectedSite;

            //also like to include all designs to choose from when updating this site
            //the existing site information
            url = "LandscapeDesignsData/ListLandscapeDesigns/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<LandscapeDesignDto> LandscapeDesignOptions = response.Content.ReadAsAsync<IEnumerable<LandscapeDesignDto>>().Result;
            
            ViewModels.LandscapeDesignOptions = LandscapeDesignOptions;


            return View(ViewModels);
        }

        // POST: Site/Update/5
        [HttpPost]
        public ActionResult Update(int id, Site site  )
        {
            string url = "SitesData/UpdateSite/" +id;

            string jsonpayload = jss.Serialize(site);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            Debug.WriteLine(content);
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(response);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");


            }
        }

        // GET: Site/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "SitesData/FindSite/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            SiteDto selectedSite = response.Content.ReadAsAsync<SiteDto>().Result;

            return View(selectedSite);
        }

        // POST: Site/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "SitesData/DeleteSite/"+id;
            //string jsonpayload = jss.Serialize(site);

           // HttpContent content = new StringContent(jsonpayload);
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");


            }
        }
    }
}
