using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SDWrox.DataModel.Models;

namespace SDWrox.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly SdwroxModelContext _context;

        public ProductsController(SdwroxModelContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TbProduct>>> GetTbProducts()
        {
            return await _context.TbProducts.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TbProduct>> GetTbProduct(int id)
        {
            var tbProduct = await _context.TbProducts.FindAsync(id);

            if (tbProduct == null)
            {
                return NotFound();
            }

            return tbProduct;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTbProduct(int id, TbProduct tbProduct)
        {
            if (id != tbProduct.Id)
            {
                return BadRequest();
            }

            _context.Entry(tbProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TbProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TbProduct>> PostTbProduct(TbProduct tbProduct)
        {
            _context.TbProducts.Add(tbProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTbProduct", new { id = tbProduct.Id }, tbProduct);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTbProduct(int id)
        {
            var tbProduct = await _context.TbProducts.FindAsync(id);
            if (tbProduct == null)
            {
                return NotFound();
            }

            _context.TbProducts.Remove(tbProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TbProductExists(int id)
        {
            return _context.TbProducts.Any(e => e.Id == id);
        }
    }
}
