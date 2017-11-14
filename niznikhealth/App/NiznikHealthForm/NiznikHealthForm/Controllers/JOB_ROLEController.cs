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
    public class JOB_ROLEController : ApiController
    {
        private niznik_dbEntities db = new niznik_dbEntities();

        // GET: api/JOB_ROLE
        public IQueryable<JOB_ROLE> GetJOB_ROLE()
        {
            return db.JOB_ROLE;
        }

        // GET: api/JOB_ROLE/5
        [ResponseType(typeof(JOB_ROLE))]
        public IHttpActionResult GetJOB_ROLE(int id)
        {
            JOB_ROLE jOB_ROLE = db.JOB_ROLE.Find(id);
            if (jOB_ROLE == null)
            {
                return NotFound();
            }

            return Ok(jOB_ROLE);
        }

        // PUT: api/JOB_ROLE/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutJOB_ROLE(int id, JOB_ROLE jOB_ROLE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jOB_ROLE.JobID)
            {
                return BadRequest();
            }

            db.Entry(jOB_ROLE).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JOB_ROLEExists(id))
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

        // POST: api/JOB_ROLE
        [ResponseType(typeof(JOB_ROLE))]
        public IHttpActionResult PostJOB_ROLE(JOB_ROLE jOB_ROLE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.JOB_ROLE.Add(jOB_ROLE);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = jOB_ROLE.JobID }, jOB_ROLE);
        }

        // DELETE: api/JOB_ROLE/5
        [ResponseType(typeof(JOB_ROLE))]
        public IHttpActionResult DeleteJOB_ROLE(int id)
        {
            JOB_ROLE jOB_ROLE = db.JOB_ROLE.Find(id);
            if (jOB_ROLE == null)
            {
                return NotFound();
            }

            db.JOB_ROLE.Remove(jOB_ROLE);
            db.SaveChanges();

            return Ok(jOB_ROLE);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JOB_ROLEExists(int id)
        {
            return db.JOB_ROLE.Count(e => e.JobID == id) > 0;
        }
    }
}