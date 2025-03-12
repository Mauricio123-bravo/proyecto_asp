using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using proyecto.Data;
using proyecto.Models;

namespace proyecto.Controllers
{
    public class FollowupController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FollowupController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _context.Followup
            .Include(c => c.CareStaff)
            .Include(s => s.State)
            .Include(p => p.Pqrs)
            .ToListAsync();
            return View(items);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var followup = await _context.Followup.Include(c => c.CareStaff)
            .Include(s => s.State)
            .Include(p => p.Pqrs)
            .FirstOrDefaultAsync(x => x.Id == id);
            if (followup == null)
            {
                return NotFound();
            }

            return View(followup);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["CareStaff"] = new SelectList(await _context.CareStaff.ToListAsync(), "Id", "Name");
            ViewData["State"] = new SelectList(await _context.State.ToListAsync(), "Id", "Name");
            ViewData["Pqrs"] = new SelectList(await _context.Pqrs.ToListAsync(), "Id", "Code");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,Date,Idcarestaff,Idstate,Idpqrs")] Followup followup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(followup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CareStaff"] = new SelectList(await _context.CareStaff.ToListAsync(), "Id", "Name", followup.Idcarestaff);
            ViewData["State"] = new SelectList(await _context.State.ToListAsync(), "Id", "Name", followup.Idstate);
            ViewData["Pqrs"] = new SelectList(await _context.Pqrs.ToListAsync(), "Id", "Code", followup.Idpqrs);
            return View(followup);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var followup = await _context.Followup.FindAsync(id);
            if (followup == null)
            {
                return NotFound();
            }

            ViewData["CareStaff"] = new SelectList(await _context.CareStaff.ToListAsync(), "Id", "Name", followup.Idcarestaff);
            ViewData["State"] = new SelectList(await _context.State.ToListAsync(), "Id", "Name", followup.Idstate);
            ViewData["Pqrs"] = new SelectList(await _context.Pqrs.ToListAsync(), "Id", "Code", followup.Idpqrs);
            return View(followup);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,Date,Idcarestaff,Idstate,Idpqrs")] Followup followup)
        {
            if (id != followup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(followup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FollowExists(followup.Id))
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
            ViewData["CareStaff"] = new SelectList(await _context.CareStaff.ToListAsync(), "Id", "Name", followup.Idcarestaff);
            ViewData["State"] = new SelectList(await _context.State.ToListAsync(), "Id", "Name", followup.Idstate);
            ViewData["Pqrs"] = new SelectList(await _context.Pqrs.ToListAsync(), "Id", "Code", followup.Idpqrs);
            return View(followup);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var followup = await _context.Followup.FindAsync(id);
            if (followup == null)
            {
                return NotFound();
            }

            _context.Followup.Remove(followup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FollowExists(int id)
        {
            return _context.Pqrs.Any(a => a.Id == id);
        }
    }
}
