using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wineApi.Models;

namespace wineApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WineModelsController : ControllerBase
    {
        private readonly WineContext _context;

        public WineModelsController(WineContext context)
        {
            _context = context;
        }

        // GET: api/WineModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WineModel>>> GetWineModels()
        {
            return await _context.WineModels.ToListAsync();
        }

        // GET: api/WineModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WineModel>> GetWineModel(int id)
        {
            var wineModel = await _context.WineModels.FindAsync(id);

            if (wineModel == null)
            {
                return NotFound();
            }

            return wineModel;
        }

        // PUT: api/WineModels/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWineModel(int id, WineModel wineModel)
        {
            if (id != wineModel.Wine_Id)
            {
                return BadRequest();
            }

            _context.Entry(wineModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WineModelExists(id))
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

        // POST: api/WineModels
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<WineModel>> PostWineModel(WineModel wineModel)
        {
            _context.WineModels.Add(wineModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWineModel", new { id = wineModel.Wine_Id }, wineModel);
        }

        // DELETE: api/WineModels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WineModel>> DeleteWineModel(int id)
        {
            var wineModel = await _context.WineModels.FindAsync(id);
            if (wineModel == null)
            {
                return NotFound();
            }

            _context.WineModels.Remove(wineModel);
            await _context.SaveChangesAsync();

            return wineModel;
        }

        private bool WineModelExists(int id)
        {
            return _context.WineModels.Any(e => e.Wine_Id == id);
        }
    }
}
