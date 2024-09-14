using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using proyecto.Data;
using proyecto.Models;

namespace proyecto.Controllers
{
    public class CareStaffController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CareStaffController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _context.CareStaff
            .Include(d => d.Departament)
            .ToListAsync();
            return View(items);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var careStaff = await _context.CareStaff.Include(d => d.Departament)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (careStaff == null)
            {
                return NotFound();
            }

            return View(careStaff);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["Departament"] = new SelectList(await _context.Departament.ToListAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Password,Email,Iddepartament")] CareStaff careStaff)
        {
            if (ModelState.IsValid)
            {
                _context.Add(careStaff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Departament"] = new SelectList(await _context.Departament.ToListAsync(), "Id", "Name", careStaff.Iddepartament);
            return View(careStaff);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var careStaff = await _context.CareStaff.FindAsync(id);
            if (careStaff == null)
            {
                return NotFound();
            }

            ViewData["Departament"] = new SelectList(await _context.Departament.ToListAsync(), "Id", "Name", careStaff.Iddepartament);
            return View(careStaff);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Password,Email,Iddepartament")] CareStaff careStaff)
        {
            if (id != careStaff.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(careStaff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarestaffExists(careStaff.Id))
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
            ViewData["Departament"] = new SelectList(await _context.Departament.ToListAsync(), "Id", "Name", careStaff.Iddepartament);
            return View(careStaff);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var careStaff = await _context.CareStaff.FindAsync(id);
            if (careStaff == null)
            {
                return NotFound();
            }

            _context.CareStaff.Remove(careStaff);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarestaffExists(int id)
        {
            return _context.CareStaff.Any(a => a.Id == id);
        }
    }
}
