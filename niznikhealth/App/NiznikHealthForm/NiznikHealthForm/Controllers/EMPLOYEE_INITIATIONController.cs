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
    public class EMPLOYEE_INITIATIONController : ApiController
    {
        private niznik_dbEntities db = new niznik_dbEntities();

        // GET: api/EMPLOYEE_INITIATION
        public IQueryable<EMPLOYEE_INITIATION> GetEMPLOYEE_INITIATION()
        {
            return db.EMPLOYEE_INITIATION;
        }

        // GET: api/EMPLOYEE_INITIATION/5
        [ResponseType(typeof(EMPLOYEE_INITIATION))]
        public IHttpActionResult GetEMPLOYEE_INITIATION(int id)
        {
            EMPLOYEE_INITIATION eMPLOYEE_INITIATION = db.EMPLOYEE_INITIATION.Find(id);
            if (eMPLOYEE_INITIATION == null)
            {
                return NotFound();
            }

            return Ok(eMPLOYEE_INITIATION);
        }

        // PUT: api/EMPLOYEE_INITIATION/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEMPLOYEE_INITIATION(int id, EMPLOYEE_INITIATION eMPLOYEE_INITIATION)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eMPLOYEE_INITIATION.EmpID)
            {
                return BadRequest();
            }

            db.Entry(eMPLOYEE_INITIATION).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EMPLOYEE_INITIATIONExists(id))
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

        // POST: api/EMPLOYEE_INITIATION
        [ResponseType(typeof(EMPLOYEE_INITIATION))]
        public IHttpActionResult PostEMPLOYEE_INITIATION(EMPLOYEE_INITIATION eMPLOYEE_INITIATION)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EMPLOYEE_INITIATION.Add(eMPLOYEE_INITIATION);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = eMPLOYEE_INITIATION.EmpID }, eMPLOYEE_INITIATION);
        }

        // DELETE: api/EMPLOYEE_INITIATION/5
        [ResponseType(typeof(EMPLOYEE_INITIATION))]
        public IHttpActionResult DeleteEMPLOYEE_INITIATION(int id)
        {
            EMPLOYEE_INITIATION eMPLOYEE_INITIATION = db.EMPLOYEE_INITIATION.Find(id);
            if (eMPLOYEE_INITIATION == null)
            {
                return NotFound();
            }

            db.EMPLOYEE_INITIATION.Remove(eMPLOYEE_INITIATION);
            db.SaveChanges();

            return Ok(eMPLOYEE_INITIATION);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EMPLOYEE_INITIATIONExists(int id)
        {
            return db.EMPLOYEE_INITIATION.Count(e => e.EmpID == id) > 0;
        }
    }
}