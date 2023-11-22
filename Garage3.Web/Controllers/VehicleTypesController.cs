using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage3.Core.Entities;
using Garage3.Persistence.Data;
using AutoMapper.Execution;

namespace Garage3.Web.Controllers
{
    public class VehicleTypesController : Controller
    {
        private readonly GarageContext _context;

        public VehicleTypesController(GarageContext context)
        {
            _context = context;
        }

        // GET: VehicleTypes
        public async Task<IActionResult> Index()
        {
              return _context.VehicleTypes != null ? 
                          View(await _context.VehicleTypes.ToListAsync()) :
                          Problem("Entity set 'GarageContext.VehicleTypes'  is null.");
        }

        // GET: VehicleTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VehicleTypes == null)
            {
                return NotFound();
            }

            var vehicleType = await _context.VehicleTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleType == null)
            {
                return NotFound();
            }

            return View(vehicleType);
        }

        // GET: VehicleTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VehicleTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Spaces,Name")] VehicleType vehicleType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicleType);
                await _context.SaveChangesAsync();
                string informationToUser = $"Vehicle type <strong>{vehicleType.Name}</strong> has been added";
                TempData["feedback"] = informationToUser;
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleType);
        }

        // GET: VehicleTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VehicleTypes == null)
            {
                return NotFound();
            }

            var vehicleType = await _context.VehicleTypes.FindAsync(id);
            if (vehicleType == null)
            {
                return NotFound();
            }
            return View(vehicleType);
        }

        // POST: VehicleTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Spaces,Name")] VehicleType vehicleType)
        {
            if (id != vehicleType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    var existingVehicleType = await _context.VehicleTypes.AsNoTracking().FirstOrDefaultAsync(v => v.Id == vehicleType.Id);
                    //Checking for changes
                    string propertiesWithAChange = "";
                    List<string> changedProperties = new List<string>();

                    _context.Update(vehicleType);
                    await _context.SaveChangesAsync();

                    if (existingVehicleType.Name != vehicleType.Name)
                    {
                        changedProperties.Add("<strong>Name</strong>");
                    }
                    if (existingVehicleType.Spaces != vehicleType.Spaces)
                    {
                        changedProperties.Add("<strong>Spaces</strong>");
                    }
                    //Writing changes as feedback
                    if (changedProperties.Count > 0)
                    {
                        propertiesWithAChange = string.Join(" and ", changedProperties);

                        string informationToUser = $"The {propertiesWithAChange} for vehicle type <strong>{vehicleType.Name}</strong> has been updated";
                        TempData["feedback"] = informationToUser;
                    }
                    else
                    {
                        TempData["feedback"] = $"No changes to {vehicleType.Name} performed";
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleTypeExists(vehicleType.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleType);
        }

        // GET: VehicleTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VehicleTypes == null)
            {
                return NotFound();
            }

            var vehicleType = await _context.VehicleTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleType == null)
            {
                return NotFound();
            }

            return View(vehicleType);
        }

        // POST: VehicleTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VehicleTypes == null)
            {
                return Problem("Entity set 'GarageContext.VehicleTypes'  is null.");
            }
            var vehicleType = await _context.VehicleTypes.FindAsync(id);
            if (vehicleType != null)
            {
                _context.VehicleTypes.Remove(vehicleType);
            }
            
            await _context.SaveChangesAsync();
            string informationToUser = $"<strong>{vehicleType.Name}</strong> has been deleted";
            TempData["feedback"] = informationToUser;
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleTypeExists(int id)
        {
          return (_context.VehicleTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
