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

namespace LandscapeArchitectsApplication.Controllers
{
    public class LandscapeDesignsDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/LandscapeDesignsData/ListLandscapeDesigns
        [HttpGet]
        public IQueryable<LandscapeDesign> ListLandscapeDesigns()
        {
            return db.LandscapeDesigns;
        }
        //VIDEO 17.03 https://www.youtube.com/watch?v=uEgWxIZmX48 REFER
        // GET: api/LandscapeDesignsData/FindLandscapeDesign/5
        [ResponseType(typeof(LandscapeDesign))]
        [HttpGet]
        public IHttpActionResult FindLandscapeDesign(int id)
        {
            LandscapeDesign landscapeDesign = db.LandscapeDesigns.Find(id);
            if (landscapeDesign == null)
            {
                return NotFound();
            }

            return Ok(landscapeDesign);
        }

        // PUT: api/LandscapeDesignsData/UpdateLandscapeDesign/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateLandscapeDesign(int id, LandscapeDesign landscapeDesign)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != landscapeDesign.DesignID)
            {
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
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

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