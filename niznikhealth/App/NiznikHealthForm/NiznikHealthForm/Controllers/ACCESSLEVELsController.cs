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
    public class ACCESSLEVELsController : ApiController
    {
        private niznik_dbEntities db = new niznik_dbEntities();

        // GET: api/ACCESSLEVELs
        public IQueryable<ACCESSLEVEL> GetACCESSLEVELs()
        {
            return db.ACCESSLEVELs;
        }

        // GET: api/ACCESSLEVELs/5
        [ResponseType(typeof(ACCESSLEVEL))]
        public IHttpActionResult GetACCESSLEVEL(int id)
        {
            ACCESSLEVEL aCCESSLEVEL = db.ACCESSLEVELs.Find(id);
            if (aCCESSLEVEL == null)
            {
                return NotFound();
            }

            return Ok(aCCESSLEVEL);
        }

        // PUT: api/ACCESSLEVELs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutACCESSLEVEL(int id, ACCESSLEVEL aCCESSLEVEL)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != aCCESSLEVEL.AccessID)
            {
                return BadRequest();
            }

            db.Entry(aCCESSLEVEL).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ACCESSLEVELExists(id))
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

        // POST: api/ACCESSLEVELs
        [ResponseType(typeof(ACCESSLEVEL))]
        public IHttpActionResult PostACCESSLEVEL(ACCESSLEVEL aCCESSLEVEL)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ACCESSLEVELs.Add(aCCESSLEVEL);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = aCCESSLEVEL.AccessID }, aCCESSLEVEL);
        }

        // DELETE: api/ACCESSLEVELs/5
        [ResponseType(typeof(ACCESSLEVEL))]
        public IHttpActionResult DeleteACCESSLEVEL(int id)
        {
            ACCESSLEVEL aCCESSLEVEL = db.ACCESSLEVELs.Find(id);
            if (aCCESSLEVEL == null)
            {
                return NotFound();
            }

            db.ACCESSLEVELs.Remove(aCCESSLEVEL);
            db.SaveChanges();

            return Ok(aCCESSLEVEL);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ACCESSLEVELExists(int id)
        {
            return db.ACCESSLEVELs.Count(e => e.AccessID == id) > 0;
        }
    }
}