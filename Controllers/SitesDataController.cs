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
    public class SitesDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returs all sites in the system
        /// </summary>
        /// <returns>
        /// HEADER:200(OK)
        /// CONTENT: all sites in the database, including their associated landscape design
        /// </returns>
        /// <example>
        ///  GET: api/SitesData/ListSites
        /// </example>
        [HttpGet]
        public IEnumerable<SiteDto> ListSites()
        {
            List<Site> Sites = db.Sites.ToList();
            List<SiteDto> siteDtos = new List<SiteDto>();

            Sites.ForEach(a => siteDtos.Add(new SiteDto()
            {
                SiteID = a.SiteID,
                Address= a.Address,
                ClientName = a.ClientName,
                DesignID = a.LandscapeDesign.DesignID,
                LeadArhitect = a.LandscapeDesign.LeadArhitect
            }));


            return siteDtos;
        }

        /// <summary>
        /// Returns all sites in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: A site in the system matching up to the site ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the site</param>
        /// <example>
        /// GET: api/SitesData/FindSite/6
        /// </example>
        
        [ResponseType(typeof(Site))]
        [HttpGet]
        public IHttpActionResult FindSite(int id)
        {
            Site Site = db.Sites.Find(id);
            SiteDto SiteDto = new SiteDto()
            {
                SiteID = Site.SiteID,
                ClientName = Site.ClientName,
                Address = Site.Address,
                DesignID = Site.LandscapeDesign.DesignID,
                LeadArhitect = Site.LandscapeDesign.LeadArhitect
                
            };

            //Site site = db.Sites.Find(id);
            if (Site == null)
            {
                return NotFound();
            }

            return Ok(SiteDto);
        }

        /// <summary>
        /// Updates a particular site in the system with POST Data input
        /// </summary>
        /// <param name="id">Represents the Site ID primary key</param>
        /// <param name="site">JSON FORM DATA of a site</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/SitesData/UpdateSite/5
        /// FORM DATA: Site JSON Object
        /// </example>
        // PUT: api/SitesData/UpdateSite/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateSite(int id, Site site)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != site.SiteID)
            {
                return BadRequest();
            }

            db.Entry(site).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SiteExists(id))
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

        /// <summary>
        /// Adds a site to the system
        /// </summary>
        /// <param name="site">JSON FORM DATA of an site</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Site ID, Site Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/SitesData/AddSite
        /// FORM DATA: Site JSON Object
        /// </example>
        // POST: api/SitesData/AddSite
        [ResponseType(typeof(Site))]
        [HttpPost]
        public IHttpActionResult AddSite(Site site)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Sites.Add(site);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = site.SiteID }, site);
        }

        /// <summary>
        /// Deletes a site from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the site</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/SitesData/DeleteSite/7
        /// FORM DATA: (empty)
        /// </example>
        // 
        [ResponseType(typeof(Site))]
        [HttpPost]
        public IHttpActionResult DeleteSite(int id)
        {
            Site site = db.Sites.Find(id);
            if (site == null)
            {
                return NotFound();
            }

            db.Sites.Remove(site);
            db.SaveChanges();

            return Ok(site);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SiteExists(int id)
        {
            return db.Sites.Count(e => e.SiteID == id) > 0;
        }
    }
}