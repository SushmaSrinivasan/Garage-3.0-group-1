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
        public async Task<IActionResult> Index()
        {
            return _context.Member != null ?
                        View(await _context.Member.ToListAsync()) :
                        Problem("Entity set 'GarageContext.Member'  is null.");
        }

        private IEnumerable<Member> Search(OverviewMemberViewModel member, IEnumerable<Member> members)
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

            if (member.Membership != 0)
            {
                members = members.Where(m => m.Membership == member.Membership);
            }

            return members;
        }

        private IEnumerable<Member> Sort(OverviewMemberViewModel member, IEnumerable<Member> members)
        {
            //Here we make the SortOrder param value available in the view so it can be added when submiting or click an <a> tag
            OverviewMemberViewModel.SortParam sortParams = OverviewMemberViewModel.SortParams;

            ViewData[sortParams.FirstName] = sortParams.FirstName;
            ViewData[sortParams.LastName] = sortParams.LastName;
            ViewData[sortParams.Membership] = sortParams.Membership;
            ViewData[sortParams.BirthDate] = sortParams.BirthDate;

            if (string.IsNullOrWhiteSpace(member.Sort))
            {
                return members;
            }

            switch (member.Sort)
            {
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
                default:
                    return members;
            }
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
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
        public async Task<IActionResult> Create([Bind("Id,Personnummer,FirstName,LastName,BirthDate,Membership")] Member member)
        {
            if (ModelState.IsValid)
            {
                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(member);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Personnummer,FirstName,LastName,BirthDate,Membership")] Member member)
        {
            if (id != member.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(member);
                    await _context.SaveChangesAsync();
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
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
            return (_context.Member?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
