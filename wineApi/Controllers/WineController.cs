﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using wineApi.Cassandra;

namespace wineApi.Controllers
{
    [Route("api/wine")]
    [ApiController]
    public class WineController : ControllerBase
    {
        // GET: api/WineModels
        [HttpGet]
        public IEnumerable<WineModel> GetWineModels()
        {
            return new[]
            {
                new WineModel(1, 
                    "Wine1", 
                    "WineSlug1",
                    "Appellation",
                    "AppellationSlug",
                    "Red",
                    "Cool",
                    "SomeRegion",
                    "France",
                    "Luxury",
                    "Too Vintage",
                    "1950",
                    true,
                    10,
                    "2",
                    4,
                    "Lwin",
                    "Lwin11"),
                new WineModel(2, 
                    "Wine2", 
                    "WineSlug2",
                    "Appellation",
                    "AppellationSlug",
                    "Pink",
                    "So-so",
                    "SomeRegion",
                    "France",
                    "Luxury",
                    "Too Vintage",
                    "1870",
                    true,
                    10,
                    "2",
                    4,
                    "Lwin",
                    "Lwin11"),
            };
        }

        // GET: api/Wine/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<WineModel>> GetWineModel(int id)
        //{
        //    var wineModel = await _context.Wine.FindAsync(id);
        //    //var wineModel = null;

        //    if (wineModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return wineModel;
        //}

        // PUT: api/WineModels/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        //public async Task<IActionResult> PutWineModel(int id, WineModel wineModel)
        //{
            //if (id != wineModel.Wine_Id)
            //{
            //    return BadRequest();
            //}

            //_context.Entry(wineModel).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!WineModelExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return NoContent();
        //}

        // POST: api/WineModels
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        //public async Task<ActionResult<WineModel>> PostWineModel(WineModel wineModel)
        //{
        //    _context.Wine.Add(wineModel);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetWineModel", new { id = wineModel.Wine_Id }, wineModel);
        //}

        //// DELETE: api/WineModels/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<WineModel>> DeleteWineModel(int id)
        //{
        //    var wineModel = await _context.Wine.FindAsync(id);
        //    if (wineModel == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Wine.Remove(wineModel);
        //    await _context.SaveChangesAsync();

        //    return wineModel;
        //}

        private bool WineModelExists(int id)
        {
            //return _context.Wine.Any(e => e.Wine_Id == id);
            return false;
        }
    }
}