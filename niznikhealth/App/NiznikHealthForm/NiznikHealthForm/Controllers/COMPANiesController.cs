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
    public class COMPANiesController : ApiController
    {
        private niznik_dbEntities db = new niznik_dbEntities();

        // GET: api/COMPANies
        public IQueryable<COMPANY> GetCOMPANies()
        {
            return db.COMPANies;
        }

        // GET: api/COMPANies/5
        [ResponseType(typeof(COMPANY))]
        public IHttpActionResult GetCOMPANY(int id)
        {
            COMPANY cOMPANY = db.COMPANies.Find(id);
            if (cOMPANY == null)
            {
                return NotFound();
            }

            return Ok(cOMPANY);
        }

        // PUT: api/COMPANies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCOMPANY(int id, COMPANY cOMPANY)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cOMPANY.CompanyID)
            {
                return BadRequest();
            }

            db.Entry(cOMPANY).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!COMPANYExists(id))
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

        // POST: api/COMPANies
        [ResponseType(typeof(COMPANY))]
        public IHttpActionResult PostCOMPANY(COMPANY cOMPANY)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.COMPANies.Add(cOMPANY);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cOMPANY.CompanyID }, cOMPANY);
        }

        // DELETE: api/COMPANies/5
        [ResponseType(typeof(COMPANY))]
        public IHttpActionResult DeleteCOMPANY(int id)
        {
            COMPANY cOMPANY = db.COMPANies.Find(id);
            if (cOMPANY == null)
            {
                return NotFound();
            }

            db.COMPANies.Remove(cOMPANY);
            db.SaveChanges();

            return Ok(cOMPANY);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool COMPANYExists(int id)
        {
            return db.COMPANies.Count(e => e.CompanyID == id) > 0;
        }
    }
}