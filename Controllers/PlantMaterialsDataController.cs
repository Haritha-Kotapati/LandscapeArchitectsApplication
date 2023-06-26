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
    public class PlantMaterialsDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all Plant materials in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Plant materials in the database, including their associated site.
        /// </returns>
        /// <example>
        /// GET: api/PlantMaterialsData/ListPlantMaterials
        /// </example>
        //
        [HttpGet]
        public IEnumerable<PlantMaterialDto> ListPlantMaterials()
        {
            List<PlantMaterial> PlantMaterials = db.PlantMaterials.ToList();
            List<PlantMaterialDto> plantMaterialDtos = new List<PlantMaterialDto>();

            PlantMaterials.ForEach(a => plantMaterialDtos.Add(new PlantMaterialDto()
            {
                Plant_Id = a.Plant_Id,
                Plant_Name = a.Plant_Name,
                Plant_Type = a.Plant_Type
            }));

            return plantMaterialDtos;
        }

        /// <summary>
        /// Returns all plant materials in the system associated with a particular site.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all plant materials in the database taking belonging in a particular site
        /// </returns>
        /// <param name="id">Site Primary Key</param>
        /// <example>
        /// GET: api/PlantMaterialsData/ListPlantMaterialsForSite/1
        /// </example>
        [HttpGet]
        [ResponseType(typeof(PlantMaterialDto))]
        public IHttpActionResult ListPlantMaterialsForSite(int id)
        {
            List<PlantMaterial> PlantMaterials = db.PlantMaterials.Where(
                p => p.Sites.Any(
                    s => s.SiteID == id)
                ).ToList();
            List<PlantMaterialDto> PlantMaterialDtos = new List<PlantMaterialDto>();

            PlantMaterials.ForEach(p => PlantMaterialDtos.Add(new PlantMaterialDto()
            {
                Plant_Id = p.Plant_Id,
                Plant_Name = p.Plant_Name,
                Plant_Type = p.Plant_Type
            }));

            return Ok(PlantMaterialDtos);
        }


        /// <summary>
        /// Returns all Plant materials in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: A Plant materials in the system matching up to the Plant ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the Plant material</param>
        /// <example>
        /// GET: api/PlantMaterialsData/FindPlantMaterial/5
        /// </example>
        // 


        [ResponseType(typeof(PlantMaterial))]
        [HttpGet]
        public IHttpActionResult FindPlantMaterial(int id)
        {
            PlantMaterial PlantMaterial = db.PlantMaterials.Find(id);
            PlantMaterialDto PlantMaterialDto = new PlantMaterialDto()
            {
                Plant_Id = PlantMaterial.Plant_Id,
                Plant_Name = PlantMaterial.Plant_Name,
                Plant_Type = PlantMaterial.Plant_Type
            };
            if (PlantMaterial == null)
            {
                return NotFound();
            }

            return Ok(PlantMaterialDto);
        }

        /// <summary>
        /// Updates a particular plant material in the system with POST Data input
        /// </summary>
        /// <param name="id">Represents the Plant ID primary key</param>
        /// <param name="plantMaterial">JSON FORM DATA of a plant material</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/PlantMaterialsData/UpdatePlantMaterial/4
        /// FORM DATA: PlantMaterial JSON Object
        /// </example>
        // 
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdatePlantMaterial(int id, PlantMaterial plantMaterial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != plantMaterial.Plant_Id)
            {
                return BadRequest();
            }

            db.Entry(plantMaterial).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlantMaterialExists(id))
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
        /// Adds a plant material to the system
        /// </summary>
        /// <param name="plantMaterial">JSON FORM DATA of an site</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Site ID, Site Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/PlantMaterialsData/
        /// FORM DATA: plantMaterial JSON Object
        /// </example>
        // POST: api/PlantMaterialsData/AddPlantMaterial
        [ResponseType(typeof(PlantMaterial))]
        [HttpPost]
        public IHttpActionResult AddPlantMaterial(PlantMaterial plantMaterial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PlantMaterials.Add(plantMaterial);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = plantMaterial.Plant_Id }, plantMaterial);
        }

        /// <summary>
        /// Deletes a plant material from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the plant material</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/PlantMaterialsData/DeletePlantMaterial/7
        /// FORM DATA: (empty)
        /// </example>
        // 
        [ResponseType(typeof(PlantMaterial))]
        [HttpPost]
        public IHttpActionResult DeletePlantMaterial(int id)
        {
            PlantMaterial plantMaterial = db.PlantMaterials.Find(id);
            if (plantMaterial == null)
            {
                return NotFound();
            }

            db.PlantMaterials.Remove(plantMaterial);
            db.SaveChanges();

            return Ok(plantMaterial);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlantMaterialExists(int id)
        {
            return db.PlantMaterials.Count(e => e.Plant_Id == id) > 0;
        }
    }
}