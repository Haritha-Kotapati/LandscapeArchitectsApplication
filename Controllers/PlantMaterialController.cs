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
   
    public class PlantMaterialController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static PlantMaterialController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44344/api/");
        }

        // GET: PlantMaterial/List
        public ActionResult List()
        {
            string url = "PlantMaterialsData/ListPlantMaterials";
            HttpResponseMessage respose = client.GetAsync(url).Result;

            IEnumerable<PlantMaterialDto> plantMaterials = respose.Content.ReadAsAsync<IEnumerable<PlantMaterialDto>>().Result;

            return View(plantMaterials);
        }

        // GET: PlantMaterial/Details/5
        public ActionResult Details(int id)
        {
            DetailsPlantMaterial ViewModel = new DetailsPlantMaterial();

            //Objective: communicate with out plant material data api to retrive one plant material

            string url = "PlantMaterialsData/FindPlantMaterial/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            PlantMaterialDto SelectedPlantMaterial = response.Content.ReadAsAsync<PlantMaterialDto>().Result;
            Debug.WriteLine("PlantMaterial recieved");

            ViewModel.SelectedPlantMaterial = SelectedPlantMaterial;

            return View(ViewModel);

        }

        // GET: PlantMaterial/New
        public ActionResult New()
        {
            //information about all designs in the system.
            //GET api/PlantMaterialsData/ListPlantMaterials

            string url = "SitesData/ListSites/";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<SiteDto> SiteOptions = response.Content.ReadAsAsync<IEnumerable<SiteDto>>().Result;

            return View(SiteOptions);
        }

        // POST: PlantMaterial/Create
        [HttpPost]
        public ActionResult Create(PlantMaterial plantMaterial)
        {
            string url = "PlantMaterialsData/AddPlantMaterial/";

            string jsonpayload = jss.Serialize(plantMaterial);

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

        // GET: PlantMaterial/Edit/5
        public ActionResult Edit(int id)
        {
            UpdatePlantMaterial ViewModels = new UpdatePlantMaterial();

            //the exixting plant material information
            string url = "PlantMaterialsData/FindPlantMaterial" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PlantMaterialDto SelectedPlantMaterial = response.Content.ReadAsAsync<PlantMaterialDto>().Result;
            ViewModels.SelectedPlantMaterial = SelectedPlantMaterial;

            //also like to include all sites to choose from when updating this plant material
            //the existing information
            url = "SitesData/ListSites/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<SiteDto> SiteOptions = response.Content.ReadAsAsync<IEnumerable<SiteDto>>().Result;

            ViewModels.SelectedPlantMaterial = SelectedPlantMaterial;

            return View(ViewModels);
        }

        // POST: PlantMaterial/Update/5
        [HttpPost]
        public ActionResult Update(int id, PlantMaterial plantMaterial)
        {
            string url = "" + id;
            string jsonpayload = jss.Serialize(plantMaterial);

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

        // GET: PlantMaterial/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PlantMaterial/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
