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
    public class COMPANY_ACCESSLEVELController : ApiController
    {
        private niznik_dbEntities db = new niznik_dbEntities();

        // GET: api/COMPANY_ACCESSLEVEL
        public IQueryable<COMPANY_ACCESSLEVEL> GetCOMPANY_ACCESSLEVEL()
        {
            return db.COMPANY_ACCESSLEVEL;
        }

        // GET: api/COMPANY_ACCESSLEVEL/5
        [ResponseType(typeof(COMPANY_ACCESSLEVEL))]
        public IHttpActionResult GetCOMPANY_ACCESSLEVEL(int id)
        {
            COMPANY_ACCESSLEVEL cOMPANY_ACCESSLEVEL = db.COMPANY_ACCESSLEVEL.Find(id);
            if (cOMPANY_ACCESSLEVEL == null)
            {
                return NotFound();
            }

            return Ok(cOMPANY_ACCESSLEVEL);
        }

        // PUT: api/COMPANY_ACCESSLEVEL/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCOMPANY_ACCESSLEVEL(int id, COMPANY_ACCESSLEVEL cOMPANY_ACCESSLEVEL)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cOMPANY_ACCESSLEVEL.Company_AccessID)
            {
                return BadRequest();
            }

            db.Entry(cOMPANY_ACCESSLEVEL).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!COMPANY_ACCESSLEVELExists(id))
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

        // POST: api/COMPANY_ACCESSLEVEL
        [ResponseType(typeof(COMPANY_ACCESSLEVEL))]
        public IHttpActionResult PostCOMPANY_ACCESSLEVEL(COMPANY_ACCESSLEVEL cOMPANY_ACCESSLEVEL)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.COMPANY_ACCESSLEVEL.Add(cOMPANY_ACCESSLEVEL);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cOMPANY_ACCESSLEVEL.Company_AccessID }, cOMPANY_ACCESSLEVEL);
        }

        // DELETE: api/COMPANY_ACCESSLEVEL/5
        [ResponseType(typeof(COMPANY_ACCESSLEVEL))]
        public IHttpActionResult DeleteCOMPANY_ACCESSLEVEL(int id)
        {
            COMPANY_ACCESSLEVEL cOMPANY_ACCESSLEVEL = db.COMPANY_ACCESSLEVEL.Find(id);
            if (cOMPANY_ACCESSLEVEL == null)
            {
                return NotFound();
            }

            db.COMPANY_ACCESSLEVEL.Remove(cOMPANY_ACCESSLEVEL);
            db.SaveChanges();

            return Ok(cOMPANY_ACCESSLEVEL);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool COMPANY_ACCESSLEVELExists(int id)
        {
            return db.COMPANY_ACCESSLEVEL.Count(e => e.Company_AccessID == id) > 0;
        }
    }
}