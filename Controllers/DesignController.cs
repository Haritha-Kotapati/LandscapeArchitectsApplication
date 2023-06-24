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
    public class DesignController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static DesignController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44344/api/");
        }
        // GET: Design/List
        public ActionResult List()
        {
            //objective: communicatewith our animal data api to retrieve a list of designs
            //curl https://localhost:44344/api/LandscapeDesignsData/ListLandscapeDesigns

            string url = "LandscapeDesignsData/ListLandscapeDesigns";
            HttpResponseMessage response = client.GetAsync(url).Result;

           // Debug.WriteLine("The respons code is ");
           // Debug.WriteLine(response.StatusCode);

            IEnumerable<LandscapeDesignDto> designs = response.Content.ReadAsAsync < IEnumerable <LandscapeDesignDto>>().Result;
           // Debug.WriteLine("Number of designs received");
           // Debug.WriteLine(designs.Count());

            return View(designs);
        }
        //https://www.youtube.com/watch?v=dFIaeluKcAA 16.05 continue
        // GET: Design/Details/5
        public ActionResult Details(int id)
        {
            //objective: communicatewith our landscape designs data api to retrieve a list of designs

            DetailsLandscapeDesign ViewModel = new DetailsLandscapeDesign();

            string url = "LandscapeDesignsData/FindLandscapeDesign/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The respons code is ");
            Debug.WriteLine(response.StatusCode);

           LandscapeDesignDto SelectedDesign = response.Content.ReadAsAsync<LandscapeDesignDto>().Result;
            
            Debug.WriteLine(SelectedDesign.LeadArhitect);

            ViewModel.SelectedDesign = SelectedDesign;

            //url = "";

            //showcase information about plants related to this design
            return View(SelectedDesign);
        }
        public ActionResult Error()
        {

            return View();
        }

        // GET: Design/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Design/Create
        [HttpPost]
        public ActionResult Create(LandscapeDesign design)
        {
            Debug.WriteLine("the json payload is : ");
            Debug.WriteLine(design.LeadArhitect);
            //objective: add a new design data into our system using the API
            //curl -d @landscapedesign.json -H "Content-type:application/json" https://localhost:44344/api/LandscapeDesignsData/AddLandscapeDesign
            string url = "LandscapeDesignsData/AddLandscapeDesign";

            string jsonpayload = jss.Serialize(design);
            Debug.WriteLine(jsonpayload);

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

        // GET: Design/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Design/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Design/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Design/Delete/5
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
