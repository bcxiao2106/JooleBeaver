using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using JooleWebAPI;

namespace JooleWebAPI.Controllers
{
    public class tblUsersController : ApiController
    {
        private JooleContext db = new JooleContext();

        // GET: api/tblUsers
        public IQueryable<tblUser> GettblUsers()
        {
            return db.tblUsers;
        }

        // GET: api/tblUsers/5
        [ResponseType(typeof(tblUser))]
        public async Task<IHttpActionResult> GettblUser(int id)
        {
            tblUser tblUser = await db.tblUsers.FindAsync(id);
            if (tblUser == null)
            {
                return NotFound();
            }

            return Ok(tblUser);
        }

        // PUT: api/tblUsers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttblUser(int id, tblUser tblUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblUser.User_ID)
            {
                return BadRequest();
            }

            db.Entry(tblUser).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblUserExists(id))
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

        // POST: api/tblUsers
        [ResponseType(typeof(tblUser))]
        public async Task<IHttpActionResult> PosttblUser(tblUser tblUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblUsers.Add(tblUser);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tblUser.User_ID }, tblUser);
        }

        // DELETE: api/tblUsers/5
        [ResponseType(typeof(tblUser))]
        public async Task<IHttpActionResult> DeletetblUser(int id)
        {
            tblUser tblUser = await db.tblUsers.FindAsync(id);
            if (tblUser == null)
            {
                return NotFound();
            }

            db.tblUsers.Remove(tblUser);
            await db.SaveChangesAsync();

            return Ok(tblUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblUserExists(int id)
        {
            return db.tblUsers.Count(e => e.User_ID == id) > 0;
        }
    }
}