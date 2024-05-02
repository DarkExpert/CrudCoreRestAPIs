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
    public class ShopsController : ControllerBase
    {
        private readonly SdwroxModelContext _context;

        public ShopsController(SdwroxModelContext context)
        {
            _context = context;
        }

        // GET: api/Shops
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TbShop>>> GetTbShops()
        {
            return await _context.TbShops.ToListAsync();
        }

        // GET: api/Shops/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TbShop>> GetTbShop(int id)
        {
            var tbShop = await _context.TbShops.FindAsync(id);

            if (tbShop == null)
            {
                return NotFound();
            }

            return tbShop;
        }

        // PUT: api/Shops/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTbShop(int id, TbShop tbShop)
        {
            if (id != tbShop.Id)
            {
                return BadRequest();
            }

            _context.Entry(tbShop).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TbShopExists(id))
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

        // POST: api/Shops
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TbShop>> PostTbShop(TbShop tbShop)
        {
            _context.TbShops.Add(tbShop);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTbShop", new { id = tbShop.Id }, tbShop);
        }

        // DELETE: api/Shops/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTbShop(int id)
        {
            var tbShop = await _context.TbShops.FindAsync(id);
            if (tbShop == null)
            {
                return NotFound();
            }

            _context.TbShops.Remove(tbShop);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TbShopExists(int id)
        {
            return _context.TbShops.Any(e => e.Id == id);
        }
    }
}
