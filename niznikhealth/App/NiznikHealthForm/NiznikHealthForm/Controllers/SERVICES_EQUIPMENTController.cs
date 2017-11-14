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
using NiznikHealthForm.Models;

namespace NiznikHealthForm.Controllers
{
    public class SERVICES_EQUIPMENTController : ApiController
    {
        private niznik_dbEntities db = new niznik_dbEntities();

        // GET: api/SERVICES_EQUIPMENT
        public IQueryable<SERVICES_EQUIPMENT> GetSERVICES_EQUIPMENT()
        {
            return db.SERVICES_EQUIPMENT;
        }

        // GET: api/SERVICES_EQUIPMENT/5
        [ResponseType(typeof(SERVICES_EQUIPMENT))]
        public IHttpActionResult GetSERVICES_EQUIPMENT(int id)
        {
            SERVICES_EQUIPMENT sERVICES_EQUIPMENT = db.SERVICES_EQUIPMENT.Find(id);
            if (sERVICES_EQUIPMENT == null)
            {
                return NotFound();
            }

            return Ok(sERVICES_EQUIPMENT);
        }

        // PUT: api/SERVICES_EQUIPMENT/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSERVICES_EQUIPMENT(int id, SERVICES_EQUIPMENT sERVICES_EQUIPMENT)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sERVICES_EQUIPMENT.Service_EquipmentID)
            {
                return BadRequest();
            }

            db.Entry(sERVICES_EQUIPMENT).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SERVICES_EQUIPMENTExists(id))
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

        // POST: api/SERVICES_EQUIPMENT
        [ResponseType(typeof(SERVICES_EQUIPMENT))]
        public IHttpActionResult PostSERVICES_EQUIPMENT(SERVICES_EQUIPMENT sERVICES_EQUIPMENT)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SERVICES_EQUIPMENT.Add(sERVICES_EQUIPMENT);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = sERVICES_EQUIPMENT.Service_EquipmentID }, sERVICES_EQUIPMENT);
        }

        // DELETE: api/SERVICES_EQUIPMENT/5
        [ResponseType(typeof(SERVICES_EQUIPMENT))]
        public IHttpActionResult DeleteSERVICES_EQUIPMENT(int id)
        {
            SERVICES_EQUIPMENT sERVICES_EQUIPMENT = db.SERVICES_EQUIPMENT.Find(id);
            if (sERVICES_EQUIPMENT == null)
            {
                return NotFound();
            }

            db.SERVICES_EQUIPMENT.Remove(sERVICES_EQUIPMENT);
            db.SaveChanges();

            return Ok(sERVICES_EQUIPMENT);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SERVICES_EQUIPMENTExists(int id)
        {
            return db.SERVICES_EQUIPMENT.Count(e => e.Service_EquipmentID == id) > 0;
        }
    }
}