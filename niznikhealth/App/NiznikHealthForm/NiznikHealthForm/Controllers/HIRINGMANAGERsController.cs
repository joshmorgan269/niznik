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
    public class HIRINGMANAGERsController : ApiController
    {
        private niznik_dbEntities db = new niznik_dbEntities();

        // GET: api/HIRINGMANAGERs
        public IQueryable<HIRINGMANAGER> GetHIRINGMANAGERs()
        {
            return db.HIRINGMANAGERs;
        }

        // GET: api/HIRINGMANAGERs/5
        [ResponseType(typeof(HIRINGMANAGER))]
        public IHttpActionResult GetHIRINGMANAGER(int id)
        {
            HIRINGMANAGER hIRINGMANAGER = db.HIRINGMANAGERs.Find(id);
            if (hIRINGMANAGER == null)
            {
                return NotFound();
            }

            return Ok(hIRINGMANAGER);
        }

        // PUT: api/HIRINGMANAGERs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHIRINGMANAGER(int id, HIRINGMANAGER hIRINGMANAGER)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hIRINGMANAGER.Hiring_ManagerID)
            {
                return BadRequest();
            }

            db.Entry(hIRINGMANAGER).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HIRINGMANAGERExists(id))
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

        // POST: api/HIRINGMANAGERs
        [ResponseType(typeof(HIRINGMANAGER))]
        public IHttpActionResult PostHIRINGMANAGER(HIRINGMANAGER hIRINGMANAGER)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HIRINGMANAGERs.Add(hIRINGMANAGER);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = hIRINGMANAGER.Hiring_ManagerID }, hIRINGMANAGER);
        }

        // DELETE: api/HIRINGMANAGERs/5
        [ResponseType(typeof(HIRINGMANAGER))]
        public IHttpActionResult DeleteHIRINGMANAGER(int id)
        {
            HIRINGMANAGER hIRINGMANAGER = db.HIRINGMANAGERs.Find(id);
            if (hIRINGMANAGER == null)
            {
                return NotFound();
            }

            db.HIRINGMANAGERs.Remove(hIRINGMANAGER);
            db.SaveChanges();

            return Ok(hIRINGMANAGER);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HIRINGMANAGERExists(int id)
        {
            return db.HIRINGMANAGERs.Count(e => e.Hiring_ManagerID == id) > 0;
        }
    }
}