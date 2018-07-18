using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace JooleWebAPI.Controllers
{
    public class tblProjectsController : ApiController
    {
        private JooleContext db = new JooleContext();

        // GET: api/tblProjects
        public IQueryable<tblProject> GettblProjects()
        {
            return db.tblProjects;
        }

        // GET: api/tblProjects/5
        [ResponseType(typeof(tblProject))]
        public async Task<IHttpActionResult> GettblProject(int id)
        {
            tblProject tblProject = await db.tblProjects.FindAsync(id);
            if (tblProject == null)
            {
                return NotFound();
            }

            return Ok(tblProject);
        }

        // PUT: api/tblProjects/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttblProject(int id, tblProject tblProject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblProject.Project_ID)
            {
                return BadRequest();
            }

            db.Entry(tblProject).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblProjectExists(id))
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

        // POST: api/tblProjects
        [ResponseType(typeof(tblProject))]
        public async Task<IHttpActionResult> PosttblProject(tblProject tblProject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblProjects.Add(tblProject);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tblProject.Project_ID }, tblProject);
        }

        // DELETE: api/tblProjects/5
        [ResponseType(typeof(tblProject))]
        public async Task<IHttpActionResult> DeletetblProject(int id)
        {
            tblProject tblProject = await db.tblProjects.FindAsync(id);
            if (tblProject == null)
            {
                return NotFound();
            }

            db.tblProjects.Remove(tblProject);
            await db.SaveChangesAsync();

            return Ok(tblProject);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblProjectExists(int id)
        {
            return db.tblProjects.Count(e => e.Project_ID == id) > 0;
        }
    }
}