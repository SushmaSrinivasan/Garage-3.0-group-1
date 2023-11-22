using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage3.Core.Entities;
using Garage3.Persistence.Data;
using Garage3.Web.Models.ViewModels;
using System.Text.RegularExpressions;
using AutoMapper.Execution;
using EntityMember = Garage3.Core.Entities.Member;

namespace Garage3.Web.Controllers
{
    public class MembersController : Controller
    {
        private readonly GarageContext _context;

        public MembersController(GarageContext context)
        {
            _context = context;
        }

        // GET: Members
        public async Task<IActionResult> Index(OverviewMemberViewModel member)
        {
            IQueryable<MemberIndexViewModel> members = _context.Member.Include(m => m.Vehicles).Select(m => new MemberIndexViewModel
            {
                Id = m.Id,
                Personnummer = m.Personnummer,
                FirstName = m.FirstName,
                LastName = m.LastName,
                FullName = m.FullName,
                BirthDate = m.BirthDate,
                Membership = m.Membership,
                VehicleCount = m.Vehicles!.Count()
            }).AsQueryable();

            members = Search(member, members);
            members = Sort(member, members);

            member.Members = await members.ToListAsync();
            return View(member);
        }

        private IQueryable<MemberIndexViewModel> Search(OverviewMemberViewModel member, IQueryable<MemberIndexViewModel> members)
        {
            if (!string.IsNullOrWhiteSpace(member.By) && !string.IsNullOrWhiteSpace(member.Search))
            {
                if (member.By == nameof(member.FirstName))
                {
                    members = members.Where(m => m.FirstName.StartsWith(member.Search));
                }
                else if (member.By == nameof(member.LastName))
                {
                    members = members.Where(m => m.LastName.StartsWith(member.Search));
                }
            }

            if (member.Membership.HasValue)
            {
                members = members.Where(m => m.Membership == member.Membership);
            }

            return members;
        }

        private IQueryable<MemberIndexViewModel> Sort(OverviewMemberViewModel member, IQueryable<MemberIndexViewModel> members)
        {
            //Here we make the SortOrder param value available in the view so it can be added when submiting or click an <a> tag
            OverviewMemberViewModel.SortParam sortParams = OverviewMemberViewModel.SortParams;

            ViewData[sortParams.Personnummer] = sortParams.Personnummer;
            ViewData[sortParams.FirstName] = sortParams.FirstName;
            ViewData[sortParams.LastName] = sortParams.LastName;
            ViewData[sortParams.Membership] = sortParams.Membership;
            ViewData[sortParams.BirthDate] = sortParams.BirthDate;
            ViewData[sortParams.VehicleCount] = sortParams.VehicleCount;

            if (string.IsNullOrWhiteSpace(member.Sort))
            {
                return members;
            }

            switch (member.Sort)
            {
                case string s when s.StartsWith(sortParams.Personnummer):
                    if (!s.EndsWith(sortParams.DescendingSuffix))
                    {
                        ViewData[sortParams.Personnummer] += sortParams.DescendingSuffix;//Adding descending suffix
                        return members.OrderBy(m => m.Personnummer);
                    }
                    return members.OrderByDescending(m => m.Personnummer);
                case string s when s.StartsWith(sortParams.FirstName):
                    if (!s.EndsWith(sortParams.DescendingSuffix))
                    {
                        ViewData[sortParams.FirstName] += sortParams.DescendingSuffix;//Adding descending suffix
                        return members.OrderBy(m => m.FirstName.Length > 1 ? m.FirstName.Substring(0,2) : m.FirstName);
                    }
                    return members.OrderByDescending(m => m.FirstName.Length > 1 ? m.FirstName.Substring(0, 2) : m.FirstName);
                case string s when s.StartsWith(sortParams.LastName):
                    if (!s.EndsWith(sortParams.DescendingSuffix))
                    {
                        ViewData[sortParams.LastName] += sortParams.DescendingSuffix;//Adding descending suffix
                        return members.OrderBy(m => m.LastName);
                    }
                    return members.OrderByDescending(m => m.LastName);
                case string s when s.StartsWith(sortParams.Membership):
                    if (!s.EndsWith(sortParams.DescendingSuffix))
                    {
                        ViewData[sortParams.Membership] += sortParams.DescendingSuffix;//Adding descending suffix
                        return members.OrderBy(m => m.Membership);
                    }
                    return members.OrderByDescending(m => m.Membership);
                case string s when s.StartsWith(sortParams.BirthDate):
                    if (!s.EndsWith(sortParams.DescendingSuffix))
                    {
                        ViewData[sortParams.BirthDate] += sortParams.DescendingSuffix;//Adding descending suffix
                        return members.OrderBy(m => m.BirthDate);
                    }
                    return members.OrderByDescending(m => m.BirthDate);
                case string s when s.StartsWith(sortParams.VehicleCount):
                    if (!s.EndsWith(sortParams.DescendingSuffix))
                    {
                        ViewData[sortParams.VehicleCount] += sortParams.DescendingSuffix;//Adding descending suffix
                        return members.OrderBy(m => m.VehicleCount);
                    }
                    return members.OrderByDescending(m => m.VehicleCount);
                default:
                    return members;
            }
        }

        public bool IsValidPersonnummer(long personnummer)
        {
            const int validLength = 12;
            var personNumberString = personnummer.ToString();

            if(personNumberString.Length!=validLength)
            {
                return false;
            }

            string pattern = @"^\d{8}\d{4}$";
            Regex regex= new Regex(pattern);

            return regex.IsMatch(personNumberString);
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Member == null)
            {
                return NotFound();
            }

            var member = await _context.Member.Include(m => m.Vehicles).ThenInclude(v => v.VehicleType).FirstOrDefaultAsync(m => m.Id == id);

            if (member == null)
            {
                return NotFound();
            }

            var model = new DetailsMemberViewModel
            {
                Personnummer = member.Personnummer,
                FirstName = member.FirstName,
                LastName = member.LastName,
                Membership = member.Membership,
                BirthDate = member.BirthDate,
                Id = member.Id
            };

            model.Vehicles = member.Vehicles is null ? new List<OverviewVehicleItemListViewModel>() 
                : member.Vehicles.Select(v => new OverviewVehicleItemListViewModel
            {
                Type = v.VehicleType.Name,
                RegistrationNumber = v.RegistrationNumber,
                ParkTime = v.ParkingDate,
                ParkVehicleId = v.Id


            }).ToList();
                

            return View(model);
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddMemberViewModel viewModel)
        {
            try
            {
                // Validate social security number format
                if (!IsValidPersonnummer(viewModel.Personnummer))
                {
                    ModelState.AddModelError("personnummer", "Invalid Personnummer format.");
                }


                // Validate age for parking
                if (viewModel.Age < 18)
                {
                    ModelState.AddModelError("Age", "Members must be over 18 years old.");
                }
                if (ModelState.IsValid)
                {
                    var member = new EntityMember
                    {
                        Personnummer = viewModel.Personnummer,
                        FirstName = viewModel.FirstName,
                        LastName = viewModel.LastName,
                        Age = viewModel.Age,
                        // Membership= viewModel.Membership
                    };

                    _context.Add(member);
                    await _context.SaveChangesAsync();
                    string informationToUser = $"Member <strong>{member.Personnummer}</strong> has been registered";
                    TempData["feedback"] = informationToUser;
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException ex)
            {
                // Log the error
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return View(viewModel);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Member == null)
            {
                return NotFound();
            }

            var member = await _context.Member.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Personnummer,FirstName,LastName,BirthDate,Membership")] EntityMember member)
        {
            if (id != member.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
					var existingMember = await _context.Member.AsNoTracking().FirstOrDefaultAsync(m => m.Id == member.Id);
					//Checking for changes
					string propertiesWithAChange = "";
					List<string> changedProperties = new List<string>();

					_context.Update(member);
					await _context.SaveChangesAsync();

					if (existingMember.Personnummer != member.Personnummer)
					{
						changedProperties.Add("<strong>Personnummer</strong>");
					}
                    if (existingMember.FirstName != member.FirstName)
                    {
                        changedProperties.Add("<strong>First name</strong>");
                    }
                    if (existingMember.LastName != member.LastName)
					{
						changedProperties.Add("<strong>Last name</strong>");
					}
					if (existingMember.Membership != member.Membership)
					{
						changedProperties.Add("<strong>Membership</strong>");
					}
					//Writing changes as feedback
					if (changedProperties.Count > 0)
					{
						propertiesWithAChange = string.Join(" and ", changedProperties);

						string informationToUser = $"The {propertiesWithAChange} for member <strong>{member.Personnummer}</strong> has been updated";
						TempData["feedback"] = informationToUser;
					}
					else
					{
						TempData["feedback"] = $"No changes to {member.Personnummer} performed";
					}
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.Id))
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
            return View(member);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Member == null)
            {
                return NotFound();
            }

            var member = await _context.Member
                .FirstOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Member == null)
            {
                return Problem("Entity set 'GarageContext.Member'  is null.");
            }
            var member = await _context.Member.FindAsync(id);
            if (member != null)
            {
                _context.Member.Remove(member);
            }

            await _context.SaveChangesAsync();
            string informationToUser = $"{member.FullName} <strong>{member.Personnummer}</strong> has been deleted";
            TempData["feedback"] = informationToUser;
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
            return (_context.Member?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
