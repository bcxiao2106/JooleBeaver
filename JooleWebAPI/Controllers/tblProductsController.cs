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
    public class tblProductsController : ApiController
    {
        private JooleContext db = new JooleContext();

        // GET: api/tblProducts
        public IQueryable<tblProduct> GettblProducts()
        {
            return db.tblProducts;
        }

        // GET: api/tblProducts/5
        [ResponseType(typeof(tblProduct))]
        public async Task<IHttpActionResult> GettblProduct(int id)
        {
            tblProduct tblProduct = await db.tblProducts.FindAsync(id);
            if (tblProduct == null)
            {
                return NotFound();
            }

            return Ok(tblProduct);
        }

        // PUT: api/tblProducts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttblProduct(int id, tblProduct tblProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblProduct.Product_ID)
            {
                return BadRequest();
            }

            db.Entry(tblProduct).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblProductExists(id))
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

        // POST: api/tblProducts
        [ResponseType(typeof(tblProduct))]
        public async Task<IHttpActionResult> PosttblProduct(tblProduct tblProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblProducts.Add(tblProduct);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tblProduct.Product_ID }, tblProduct);
        }

        // DELETE: api/tblProducts/5
        [ResponseType(typeof(tblProduct))]
        public async Task<IHttpActionResult> DeletetblProduct(int id)
        {
            tblProduct tblProduct = await db.tblProducts.FindAsync(id);
            if (tblProduct == null)
            {
                return NotFound();
            }

            db.tblProducts.Remove(tblProduct);
            await db.SaveChangesAsync();

            return Ok(tblProduct);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblProductExists(int id)
        {
            return db.tblProducts.Count(e => e.Product_ID == id) > 0;
        }
    }
}