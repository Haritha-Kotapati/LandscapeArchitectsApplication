using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using LandscapeArchitectsApplication.Models;
using System.Diagnostics;

namespace LandscapeArchitectsApplication.Controllers
{
    public class LandscapeDesignsDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returs all landscape designs in the system
        /// </summary>
        /// <returns>
        /// HEADER:200(OK)
        /// CONTENT: all landscape design in the database
        /// </returns>
        
        // GET: api/LandscapeDesignsData/ListLandscapeDesigns
        [HttpGet]
        public IEnumerable<LandscapeDesignDto> ListLandscapeDesigns()
        {
            List<LandscapeDesign> LandscapeDesigns = db.LandscapeDesigns.ToList();
            List<LandscapeDesignDto> landscapeDesignDtos = new List<LandscapeDesignDto>();

            LandscapeDesigns.ForEach(a => landscapeDesignDtos.Add(new LandscapeDesignDto() {
                    DesignID = a.DesignID,
                    LeadArhitect = a.LeadArhitect,
                    DateCreated = a.DateCreated
                    
            }));

            return landscapeDesignDtos;
        }
        //curl https://localhost:44344/api/LandscapeDesignsData/FindLandscapeDesign/2
        // GET: api/LandscapeDesignsData/FindLandscapeDesign/5
        [ResponseType(typeof(LandscapeDesign))]
        [HttpGet]
        public IHttpActionResult FindLandscapeDesign(int id)
        {
            LandscapeDesign LandscapeDesign = db.LandscapeDesigns.Find(id);
            LandscapeDesignDto landscapeDesignDto = new LandscapeDesignDto()

           
            {
                DesignID = LandscapeDesign.DesignID,
                LeadArhitect = LandscapeDesign.LeadArhitect,
                DateCreated = LandscapeDesign.DateCreated

            };

            
            if (LandscapeDesign == null)
            {
                return NotFound();
            }

            return Ok(landscapeDesignDto);
        }
        //curl -d @landscapedesign.json -H "Content-type:application/json" https://localhost:44344/api/LandscapeDesignsData/UpdateLandscapeDesign/2
        // PUT: api/LandscapeDesignsData/UpdateLandscapeDesign/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateLandscapeDesign(int id, LandscapeDesign landscapeDesign)
        {
            Debug.WriteLine("I have reached the update method");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Invalid model state");
                return BadRequest(ModelState);
            }

            if (id != landscapeDesign.DesignID)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("GET parameter" + id);
                Debug.WriteLine("POST parameter" + landscapeDesign.DesignID);
                return BadRequest();
            }

            db.Entry(landscapeDesign).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LandscapeDesignExists(id))
                {
                    Debug.WriteLine("Landscape design not found");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            Debug.WriteLine("None of the method was triggered");
            return StatusCode(HttpStatusCode.NoContent);
        }
        //curl -d @landscapedesign.json -H "Content-type:application/json" https://localhost:44344/api/LandscapeDesignsData/AddLandscapeDesign
        // POST: api/LandscapeDesignsData/AddLandscapeDesign
        [ResponseType(typeof(LandscapeDesign))]
        [HttpPost]
        public IHttpActionResult AddLandscapeDesign(LandscapeDesign landscapeDesign)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LandscapeDesigns.Add(landscapeDesign);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = landscapeDesign.DesignID }, landscapeDesign);
        }
        //curl -d "" https://localhost:44344/api/LandscapeDesignsData/DeleteLandscapeDesign/16
        // DELETE: api/LandscapeDesignsData/DeleteLandscapeDesign/5
        [ResponseType(typeof(LandscapeDesign))]
        [HttpPost]
        public IHttpActionResult DeleteLandscapeDesign(int id)
        {
            LandscapeDesign landscapeDesign = db.LandscapeDesigns.Find(id);
            if (landscapeDesign == null)
            {
                return NotFound();
            }

            db.LandscapeDesigns.Remove(landscapeDesign);
            db.SaveChanges();

            return Ok(landscapeDesign);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LandscapeDesignExists(int id)
        {
            return db.LandscapeDesigns.Count(e => e.DesignID == id) > 0;
        }
    }
}