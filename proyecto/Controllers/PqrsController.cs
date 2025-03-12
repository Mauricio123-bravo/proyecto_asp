using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using proyecto.Data;
using proyecto.Models;

namespace proyecto.Controllers
{
    public class PqrsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public PqrsController(ApplicationDbContext context)
        {

            _context = context;
            
        }

        public async Task<IActionResult> Index()
        {
                var items = await _context.Pqrs
                .Include(u => u.User)
                .Include(c => c.Category)
                .ToListAsync();
            return View(items);
        }


        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var pqrs = await _context.Pqrs.Include(u => u.User).Include(c => c.Category).FirstOrDefaultAsync(x => x.Id == id);

            if (pqrs == null)
            {
                return NotFound();
            }

            return View(pqrs);
        }


        public async Task<IActionResult> Create()
        {
            ViewData["Category"] = new SelectList(await _context.Category.ToListAsync(), "Id", "Name");
            ViewData["User"] = new SelectList(await _context.User.ToListAsync(), "Id", "Fullname");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Creation_date,Description,Idcategory,Iduser")] Pqrs pqrs)
        {
            if (ModelState.IsValid)
            {
                pqrs.Code = GenerateUniqueCode(_context);
                _context.Add(pqrs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Category"] = new SelectList(await _context.Category.ToListAsync(), "Id", "Name", pqrs.Idcategory);
            ViewData["User"] = new SelectList(await _context.User.ToListAsync(), "Id", "Fullname", pqrs.Iduser);
            return View(pqrs);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pqrs = await _context.Pqrs.FindAsync(id);
            if (pqrs == null)
            {
                return NotFound();
            }

            ViewData["Category"] = new SelectList(await _context.Category.ToListAsync(), "Id", "Name", pqrs.Idcategory);
            ViewData["User"] = new SelectList(await _context.User.ToListAsync(), "Id", "Fullname", pqrs.Iduser);
            return View(pqrs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Creation_date,Description,Code,Idcategory,Iduser")] Pqrs pqrs)
        {
            if (id != pqrs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pqrs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PqrsExists(pqrs.Id))
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
            ViewData["Category"] = new SelectList(await _context.Category.ToListAsync(), "Id", "Name", pqrs.Idcategory);
            ViewData["User"] = new SelectList(await _context.User.ToListAsync(), "Id", "Fullname", pqrs.Iduser);
            return View(pqrs);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var pqrs = await _context.Pqrs.FindAsync(id);
            if (pqrs == null)
            {
                return NotFound();
            }

            _context.Pqrs.Remove(pqrs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PqrsExists(int id)
        {
            return _context.Pqrs.Any(a => a.Id == id);
        }

        private static String GenerateUniqueCode(ApplicationDbContext context)
        {
            var random = new Random();
            String code;

            do
            {
                code = random.Next(100000000, 1000000000).ToString(); // Genera un número aleatorio de 9 dígitos

            }
            while (context.Pqrs.Any(p => p.Code == code)); // Verifica si el código ya existe en la base de datos

            return code;
        }
    }

}