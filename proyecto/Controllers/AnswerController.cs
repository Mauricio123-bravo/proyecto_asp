using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using proyecto.Data;
using proyecto.Models;

namespace proyecto.Controllers
{
    public class AnswerController : Controller
    {

        private readonly ApplicationDbContext _context;

        public AnswerController(ApplicationDbContext context)
        {

            _context = context;
            
        }
        public async Task<IActionResult> Index()
        {
            var items = await _context.Answer
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

            var answer = await _context.Answer.Include(p => p.Pqrs).FirstOrDefaultAsync(x => x.Id == id);

            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["Pqrs"] = new SelectList(await _context.Pqrs.ToListAsync(), "Id", "Code");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Content,Answer_date,Idpqrs")] Answer answer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(answer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Pqrs"] = new SelectList(await _context.Pqrs.ToListAsync(), "Id", "Code", answer.Idpqrs);
            return View(answer);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answer.FindAsync(id);
            if (answer == null)
            {
                return NotFound();
            }
            ViewData["Pqrs"] = new SelectList(await _context.Pqrs.ToListAsync(), "Id", "Code", answer.Idpqrs);
            return View(answer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Content,Answer_date,Idpqrs")] Answer answer)
        {
            if (id != answer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(answer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnswerExists(answer.Id))
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
            ViewData["Pqrs"] = new SelectList(await _context.Pqrs.ToListAsync(), "Id", "Description", answer.Idpqrs);
            return View(answer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var answer = await _context.Answer.FindAsync(id);
            _context.Answer.Remove(answer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnswerExists(int id)
        {
            return _context.Answer.Any(a => a.Id == id);
        }
    }
}
