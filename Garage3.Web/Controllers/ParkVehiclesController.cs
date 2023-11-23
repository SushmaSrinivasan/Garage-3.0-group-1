using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Garage3.Core;
using Garage3.Core.Entities;
using Garage3.Persistence.Data;
using Garage3.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Humanizer;
using AutoMapper.Execution;

namespace Garage3.Web.Controllers
{
    public class ParkVehiclesController : Controller
    {
        private readonly GarageContext _context;

        public ParkVehiclesController(GarageContext context)
        {
            _context = context;
        }

        // GET: ParkVehicles
        public async Task<IActionResult> Index(OverviewVehicleViewModel searchview)
        {

            searchview.VehicleTypes = await GetVehicleTypes();

            var vehicles = _context.ParkVehicle.Include(v => v.Owner).Include(v => v.VehicleType).AsQueryable();

            vehicles = Search(searchview, vehicles);
            vehicles = Sort(searchview, vehicles);

            searchview.Vehicles = await vehicles.Select(v => new OverviewVehicleItemListViewModel
            {
                Owner = v.Owner.FullName,
                OwnerId = v.Owner.Id,
                Membership = v.Owner.Membership.ToString(),
                ParkTime = v.ParkingDate,
                ParkVehicleId = v.Id,
                RegistrationNumber = v.RegistrationNumber,
                Type = v.VehicleType.Name
            }).ToListAsync();


            return View(searchview);
        }

        public async Task<IEnumerable<SelectListItem>> GetVehicleTypes()
        {
            return await _context.VehicleTypes.Select(v => new SelectListItem(v.Name, v.Id.ToString())).ToArrayAsync();

        }

        private IQueryable<ParkVehicle> Search(OverviewVehicleViewModel vehicle, IQueryable<ParkVehicle> vehicles)
        {
            if (!string.IsNullOrWhiteSpace(vehicle.By) && !string.IsNullOrWhiteSpace(vehicle.Search))
            {
                if (vehicle.By == nameof(vehicle.RegistrationNumber))
                {
                    vehicles = vehicles.Where(v => v.RegistrationNumber.StartsWith(vehicle.Search));
                }
                else if (vehicle.By == nameof(vehicle.Owner))
                {
                    vehicles = vehicles.Where(v => v.Owner.FirstName.StartsWith(vehicle.Search));
                }
            }

            if (vehicle.Type != 0)
            {
                vehicles = vehicles.Where(v => v.Id == vehicle.Type);
            }

            return vehicles;
        }

        private IQueryable<ParkVehicle> Sort(OverviewVehicleViewModel vehicle, IQueryable<ParkVehicle> vehicles)
        {
            //Here we make the SortOrder param value available in the view so it can be added when submiting or click an <a> tag
            OverviewVehicleViewModel.SortParam sortParams = OverviewVehicleViewModel.SortParams;

            ViewData[sortParams.Owner] = sortParams.Owner;
            ViewData[sortParams.Membership] = sortParams.Membership;
            ViewData[sortParams.Type] = sortParams.Type;
            ViewData[sortParams.RegistrationNumber] = sortParams.RegistrationNumber;
            ViewData[sortParams.ParkTime] = sortParams.ParkTime;

            if (string.IsNullOrWhiteSpace(vehicle.Sort))
            {
                return vehicles;
            }

            switch (vehicle.Sort)
            {
                case string s when s.StartsWith(sortParams.Owner):
                    if (!s.EndsWith(sortParams.DescendingSuffix))
                    {
                        ViewData[sortParams.Owner] += sortParams.DescendingSuffix;//Adding descending suffix
                        return vehicles.OrderBy(v => v.Owner.FirstName);
                    }
                    return vehicles.OrderByDescending(v => v.Owner.FirstName);
                case string s when s.StartsWith(sortParams.Membership):
                    if (!s.EndsWith(sortParams.DescendingSuffix))
                    {
                        ViewData[sortParams.Membership] += sortParams.DescendingSuffix;//Adding descending suffix
                        return vehicles.OrderBy(v => v.Owner.Membership);
                    }
                    return vehicles.OrderByDescending(v => v.Owner.Membership);
                case string s when s.StartsWith(sortParams.Type):
                    if (!s.EndsWith(sortParams.DescendingSuffix))
                    {
                        ViewData[sortParams.Type] += sortParams.DescendingSuffix;//Adding descending suffix
                        return vehicles.OrderBy(v => v.VehicleType.Name);
                    }
                    return vehicles.OrderByDescending(v => v.VehicleType.Name);
                case string s when s.StartsWith(sortParams.RegistrationNumber):
                    if (!s.EndsWith(sortParams.DescendingSuffix))
                    {
                        ViewData[sortParams.RegistrationNumber] += sortParams.DescendingSuffix;//Adding descending suffix
                        return vehicles.OrderBy(v => v.RegistrationNumber);
                    }
                    return vehicles.OrderByDescending(v => v.RegistrationNumber);
                case string s when s.StartsWith(sortParams.ParkTime):
                    if (!s.EndsWith(sortParams.DescendingSuffix))
                    {
                        ViewData[sortParams.ParkTime] += sortParams.DescendingSuffix;//Adding descending suffix
                        return vehicles.OrderBy(v => v.ParkingDate);
                    }
                    return vehicles.OrderByDescending(v => v.ParkingDate);
                default:
                    return vehicles;
            }

        }

        [AcceptVerbs("GET", "POST")]
        //this method is defined under [Remote] data annotation to check uniqueness
        public async Task<IActionResult> IsRegistrationNumberExists(string registrationNumber, string existingRegistrationNumber)
        {
            if (registrationNumber == existingRegistrationNumber)
            {
                return Json(true);
            }
            bool isRegistrationNumberExists = false;
            try
            {
                //checks if the existing reg num equals with the entered reg num
                isRegistrationNumberExists = await _context.ParkVehicle.AnyAsync(v => v.RegistrationNumber.Equals(registrationNumber));
                return Json(!isRegistrationNumberExists);
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }

        // GET: ParkVehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ParkVehicle == null)
            {
                return NotFound();
            }
            //creating instance for model and assigning values to the model
            var detailsViewModel = await _context.ParkVehicle.Include(v => v.VehicleType)
                .Select(v => new Garage3.Web.Models.ViewModels.DetailViewModel
                {
                    Id = v.Id,
                    RegistrationNumber = v.RegistrationNumber,
                    Brand = v.Brand,
                    Color = v.Color,
                    Model = v.Model,
                    NumberOfWheels = v.NumberOfWheels,
                    ParkingDate = v.ParkingDate,
                    VehicleType = v.VehicleType.Name,
                    Owner = v.Owner.FullName,
                    OwnerId = v.Owner.Id
                })
                .FirstOrDefaultAsync(m => m.Id == id);

            if (detailsViewModel == null)
            {
                return NotFound();
            }

            return View(detailsViewModel);
        }



        // GET: ParkVehicles/Park
        public async Task<IActionResult> Park()
        {
            var model = new ParkVehicleViewModel();
            model.VehicleTypes = await GetParkVehicleTypes();

            ViewBag.MembershipType = new SelectList(Enum.GetValues(typeof(Membership)));


            return View(model);
        }

        // POST: ParkVehicles/Park
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Park([Bind("Personnummer, MembershipType, RegistrationNumber, VehicleTypeId, Color, Brand, Model, NumberOfWheels")] ParkVehicleViewModel parkVehicleViewModel)
        {
            var member = await _context.Member
                .SingleOrDefaultAsync(m => m.Personnummer == parkVehicleViewModel.Personnummer);

            if (member == null)
            {
                ModelState.AddModelError("Personnummer", "Member does not exist");
            }
            if (ModelState.IsValid)
            {
                if (await _context.ParkVehicle.AnyAsync(v => v.RegistrationNumber.Equals(parkVehicleViewModel.RegistrationNumber)))
                {
                    ModelState.AddModelError("RegistrationNumber", "Registration Number already exists");
                }
                else
                {
                    var parkVehicle = new ParkVehicle
                    {
                        Owner = member,
                        RegistrationNumber = parkVehicleViewModel.RegistrationNumber,
                        VehicleTypeId = parkVehicleViewModel.VehicleTypeId,
                        Color = parkVehicleViewModel.Color,
                        Brand = parkVehicleViewModel.Brand,
                        Model = parkVehicleViewModel.Model,
                    };

                    ICollection<ParkingSpace> spots = await GetWhereToPark(parkVehicle.VehicleTypeId);

                    if (spots.Any())
                    {
                        parkVehicle.Spots = spots;
                        _context.Add(parkVehicle);
                        await _context.SaveChangesAsync();

                        string informationToUser = $"Vehicle <strong>{parkVehicleViewModel.RegistrationNumber}</strong> has been parked at {string.Join(", ", parkVehicle.Spots.Select(s => s.Id))}";
                        TempData["feedback"] = informationToUser;

                        return RedirectToAction(nameof(Index));
                    }
                }
            }

            parkVehicleViewModel.VehicleTypes = await GetParkVehicleTypes();
            ViewBag.MembershipType = new SelectList(Enum.GetValues(typeof(Membership)), "Value", "Text");
            return View(parkVehicleViewModel);
        }

        private async Task<IEnumerable<ParkVehicleTypeViewModel>> GetParkVehicleTypes()
        {
            var spots = _context.ParkingSpaces.AsQueryable();

            ICollection<OverviewParkingSpaceViewModel> finalSpots;

            finalSpots = new List<OverviewParkingSpaceViewModel>();
            var spotsList = await spots.Where(s => s.VehicleId == null).OrderBy(s => s.Id).ToListAsync();

            int maxConsecutiveCount = 0, count = 0;
            int? prevSpotId = null;

            foreach (int id in spotsList.Select(s => s.Id))
            {
                if (!prevSpotId.HasValue || id - prevSpotId == 1)
                {
                    count++;
                }
                else
                {
                    maxConsecutiveCount = Math.Max(maxConsecutiveCount, count);
                    count = 1;
                }
                prevSpotId = id;
            }

            maxConsecutiveCount = Math.Max(maxConsecutiveCount, count);

            var vehicleTypes = await _context.VehicleTypes.ToListAsync();
            List<ParkVehicleTypeViewModel> parkVehicleType = new(vehicleTypes.Count);

            foreach (VehicleType v in vehicleTypes)
            {
                parkVehicleType.Add(new ParkVehicleTypeViewModel
                {
                    Id = v.Id,
                    Name = v.Name,
                    HasWhereToPark = maxConsecutiveCount >= v.Spaces,
                });
            }

            return parkVehicleType;

        }

        private async Task<ICollection<ParkingSpace>> GetWhereToPark(int vehicleTypeId)
        {
            VehicleType? type = _context.VehicleTypes.FirstOrDefault(vt => vt.Id == vehicleTypeId);

            if (type is null)
            {
                return new List<ParkingSpace>();
            }

            var spots = _context.ParkingSpaces.AsQueryable();

            List<ParkingSpace> finalSpots = new(type.Spaces);

            var spotsList = await spots.Where(s => s.VehicleId == null).OrderBy(s => s.Id).ToListAsync();

            int? prevSpotId = null;

            foreach (ParkingSpace spot in spotsList)
            {
                if (!prevSpotId.HasValue || spot.Id - prevSpotId == 1)
                {
                    finalSpots.Add(spot);

                    if (finalSpots.Count == type.Spaces)
                    {
                        return finalSpots;
                    }
                }
                else
                {
                    finalSpots.Clear();
                    finalSpots.Add(spot);
                }
                prevSpotId = spot.Id;
            }

            return new List<ParkingSpace>();

        }

        // GET: ParkVehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ParkVehicle == null)
            {
                return NotFound();
            }

            var parkVehicle = await _context.ParkVehicle
                .Include(pv => pv.VehicleType)
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == id);

            if (parkVehicle == null)
            {
                return NotFound();
            }

            if (TempData.ContainsKey("feedback"))
            {
                ViewBag.Feedback = TempData["feedback"].ToString();
            }

            var editParkVehicle = new EditParkVehicleViewModel
            {
                Id = parkVehicle.Id,
                ExistingRegistrationNumber = parkVehicle.RegistrationNumber,
                RegistrationNumber = parkVehicle.RegistrationNumber,
                Brand = parkVehicle.Brand,
                Color = parkVehicle.Color,
                Model = parkVehicle.Model,
                NumberOfWheels = parkVehicle.NumberOfWheels,
                ParkingDate = parkVehicle.ParkingDate,
                VehicleTypeId = parkVehicle.VehicleType.Id // Assign the VehicleTypeId
            };

            // Populate the dropdown for VehicleType
            ViewBag.VehicleTypes = new SelectList(await _context.VehicleTypes.ToListAsync(), "Id", "Name", parkVehicle.VehicleType.Id);

            return View(editParkVehicle);
        }


        // POST: ParkVehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, EditParkVehicleViewModel parkVehicle)
        {
            if (id != parkVehicle.Id)
            {
                return NotFound();
            }

            if (parkVehicle.ExistingRegistrationNumber != parkVehicle.RegistrationNumber && await _context.ParkVehicle.AnyAsync(v => v.RegistrationNumber.Equals(parkVehicle.RegistrationNumber)))
            {
                ModelState.AddModelError("RegistrationNumber", "Registration Number already exists");
                return View(parkVehicle);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingVehicle = await _context.ParkVehicle
                        .Include(v => v.VehicleType) // Make sure to include related entities
                        .FirstOrDefaultAsync(v => v.Id == parkVehicle.Id);

                    if (existingVehicle == null)
                    {
                        return NotFound();
                    }

                    // Checking for changes
                    List<string> changedProperties = new List<string>();

                    if (existingVehicle.RegistrationNumber != parkVehicle.RegistrationNumber)
                    {
                        changedProperties.Add("<strong>registration number</strong>");
                    }
                    if (existingVehicle.VehicleTypeId != parkVehicle.VehicleTypeId)
                    {
                        changedProperties.Add("<strong>vehicle type</strong>");
                    }
                    if (existingVehicle.Color != parkVehicle.Color)
                    {
                        changedProperties.Add("<strong>color</strong>");
                    }
                    if (existingVehicle.Brand != parkVehicle.Brand)
                    {
                        changedProperties.Add("<strong>brand</strong>");
                    }
                    if (existingVehicle.Model != parkVehicle.Model)
                    {
                        changedProperties.Add("<strong>model</strong>");
                    }
                    if (existingVehicle.NumberOfWheels != parkVehicle.NumberOfWheels)
                    {
                        changedProperties.Add("<strong>number of wheels</strong>");
                    }

                    // Writing changes as feedback
                    if (changedProperties.Count > 0)
                    {
                        string propertiesWithAChange = string.Join(" and ", changedProperties);

                        string informationToUser = $"The {propertiesWithAChange} for vehicle <strong>{parkVehicle.RegistrationNumber}</strong> has been updated";
                        TempData["feedback"] = informationToUser;
                    }
                    else
                    {
                        TempData["feedback"] = $"No changes to {parkVehicle.RegistrationNumber} performed";
                    }

                    // Performing changes
                    existingVehicle.RegistrationNumber = parkVehicle.RegistrationNumber;
                    existingVehicle.VehicleTypeId = parkVehicle.VehicleTypeId;
                    existingVehicle.Color = parkVehicle.Color;
                    existingVehicle.Brand = parkVehicle.Brand;
                    existingVehicle.Model = parkVehicle.Model;
                    existingVehicle.NumberOfWheels = parkVehicle.NumberOfWheels;

                    // Save changes
                    await _context.SaveChangesAsync();

                    // Redirect to Index or another appropriate action
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkVehicleExists(parkVehicle.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            // If ModelState is not valid, return to the view with the invalid model
            return View(parkVehicle);
        }
        // GET: ParkVehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ParkVehicle == null)
            {
                return NotFound();
            }

            var detailsViewModel = await _context.ParkVehicle.Include(v => v.VehicleType)
                .Select(v => new Garage3.Web.Models.ViewModels.DetailViewModel
                {
                    Id = v.Id,
                    RegistrationNumber = v.RegistrationNumber,
                    Brand = v.Brand,
                    Color = v.Color,
                    Model = v.Model,
                    NumberOfWheels = v.NumberOfWheels,
                    ParkingDate = v.ParkingDate,
                    VehicleType = v.VehicleType.Name,
                    Owner = v.Owner.FullName,
                    OwnerId = v.Owner.Id
                })
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detailsViewModel == null)
            {
                return NotFound();
            }

            return View(detailsViewModel);
        }

        // POST: ParkVehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ParkVehicle == null)
            {
                return Problem("Entity set 'Exercise_12_Garage_2_0___part_1_Group1Context.ParkVehicle' is null.");
            }

            var parkVehicle = await _context.ParkVehicle.FindAsync(id);

            if (parkVehicle != null)
            {


                var timePassed = DateTime.Now - parkVehicle.ParkingDate; // Gets the amount of time by comparing current date with date of parking.
                var hoursRoundedDown = (int)Math.Floor(timePassed.TotalHours);  // Converts timePassed and rounds down its Hours to a full number.
                var minutesRoundedDown = (int)Math.Floor((timePassed.TotalMinutes - (hoursRoundedDown * 60))); // Rounds down and Resets Minutes every 60 minutes

                // Receipt data. Cost is calculated and rounded down. 
                var receiptData = new ReceiptViewModel
                {
                    RegistrationNumber = parkVehicle.RegistrationNumber,
                    Brand = parkVehicle.Brand,
                    Model = parkVehicle.Model,
                    HoursParked = hoursRoundedDown,
                    MinutesParked = minutesRoundedDown,
                    Cost = Math.Floor((hoursRoundedDown * 70) + (minutesRoundedDown * 1.2)),

                };

                _context.ParkVehicle.Remove(parkVehicle);
                await _context.SaveChangesAsync();

                // Pass the receipt data to the view
                string informationToUser = $"{parkVehicle.VehicleType} <strong>{parkVehicle.RegistrationNumber}</strong> has been collected";
                TempData["feedback"] = informationToUser;
                return View("ReceiptView", receiptData);
            }

            return RedirectToAction(nameof(Index));
        }
        private StatisticsViewModel CalculateStatistics()
        {
            var totalWheels = _context.ParkVehicle.Sum(v => v.NumberOfWheels);

            var totalRevenue = _context.ParkVehicle
                .AsEnumerable()
                .Select(v =>
                {
                    var timePassed = DateTime.Now - v.ParkingDate;
                    var hoursRoundedDown = (int)Math.Floor(timePassed.TotalHours);
                    var minutesRoundedDown = (int)Math.Floor((timePassed.TotalMinutes - (hoursRoundedDown * 60)));
                    return (hours: hoursRoundedDown, minutes: minutesRoundedDown);
                })
                .Sum(time => (time.hours * 70) + (time.minutes * 1.2));

            var vehicleTypeAmount = _context.ParkVehicle
                .GroupBy(v => v.VehicleTypeId) // Assuming "VehicleTypeId" is the foreign key property in ParkVehicle
                .ToDictionary(group => _context.VehicleTypes.Find(group.Key)?.Name ?? "Unknown", group => group.Count());

            // New: Calculate the number of members
            var numberOfMembers = _context.Member.Count();

            var statistics = new StatisticsViewModel
            {
                TotalWheels = totalWheels,
                TotalRevenue = totalRevenue,
                VehicleTypeAmount = vehicleTypeAmount,
                NumberOfMembers = numberOfMembers  // Add this property to your StatisticsViewModel
            };

            return statistics;
        }
        public IActionResult Statistics()
        {
            var statistics = CalculateStatistics();
            return View(statistics);
        }

        public async Task<IActionResult> ParkingLotOverview(OverviewParkingLotViewModel parkingLot)
        {
            var spots = _context.ParkingSpaces.Include(p => p.Vehicle).ThenInclude(v => v.VehicleType).AsQueryable();

            spots = FilterSpots(parkingLot, spots);

            ICollection<OverviewParkingSpaceViewModel> finalSpots;

            finalSpots = new List<OverviewParkingSpaceViewModel>();
            var spotsList = await spots.OrderBy(s => s.Id).ToListAsync();

            int prevSpotId = 0, groupId = 0;
            int? prevVehicleId = 0;

            var groupedSpots = spotsList.GroupBy(s =>
            {
                groupId = (s.Id - prevSpotId != 1 || prevVehicleId != s.VehicleId) ? groupId + 1 : groupId;

                prevSpotId = s.Id;
                prevVehicleId = s.VehicleId;
                return groupId;
            }).Select(g => new
            {
                MinSpot = g.Min(s => s.Id),
                MaxSpot = g.Max(s => s.Id),
                Vehicle = g.FirstOrDefault().Vehicle
            });

            foreach (var s in groupedSpots)
            {
                var spot = new OverviewParkingSpaceViewModel();
                spot.MinSpot = s.MinSpot;
                spot.MaxSpot = s.MaxSpot;

                if (s.Vehicle is not null)
                {
                    spot.VehicleId = s.Vehicle.Id;
                    spot.VehicleRegistrationNumber = s.Vehicle.RegistrationNumber;
                    spot.VehicleType = s.Vehicle.VehicleType.Name;
                }
                finalSpots.Add(spot);
            }

            parkingLot.spots = finalSpots;

            return View(parkingLot);
        }

        public IQueryable<ParkingSpace> FilterSpots(OverviewParkingLotViewModel parkingLot, IQueryable<ParkingSpace> spots)
        {
            switch (parkingLot.StateFilter)
            {
                case ParkingSpaceFilters.Free:
                    return spots.Where(s => s.Vehicle == null);
                case ParkingSpaceFilters.Occupied:
                    return spots.Where(s => s.Vehicle != null);
                default:
                    return spots;
            }
        }


        private bool ParkVehicleExists(int id)
        {
            return (_context.ParkVehicle?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}