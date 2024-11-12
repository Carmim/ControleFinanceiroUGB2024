using Apoio.Data;
using Apoio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Apoio.Controllers
{
    public class ApostaController : Controller
    {
        private readonly WebetContext _context;

        public ApostaController(WebetContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var apostaContext = _context.Apostas.Include(ap => ap.Apostador);
            return View(apostaContext.ToList());
        }
        //Create
        public IActionResult Create()
        {
            ViewData["ApostadorId"] = new SelectList(_context.Apostadores, "Id", "Nome");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Valor", "PrevisaoDeGanho","Vencedora","ApostadorId")] Aposta aposta)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ApostadorId"] = new SelectList(_context.Apostadores, "Id", "Nome", aposta.Id);
                return View(aposta);
            }
            if(aposta == null)
            {
                return NotFound();
            }

            _context.Add(aposta);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        //Edit Get
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aposta = await _context.Apostas.FindAsync(id);
            if (aposta == null)
            {
                return NotFound();
            }
            ViewData["ApostadorId"] = new SelectList(_context.Apostadores, "Id", "Nome");
            return View(aposta);
        }
        //Edit POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Valor, PrevisaoDeGanho, Vencedora, ApostadorId")] Aposta aposta)
        {
            if (id != aposta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aposta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApostaExists(aposta.Id))
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
            return View(aposta);
        }
        private bool ApostaExists(int id)
        {
            return _context.Apostas.Any(e => e.Id == id);
        }
        //Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aposta = await _context.Apostas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aposta == null)
            {
                return NotFound();
            }

            return View(aposta);
        }
        //Delete GET
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aposta = await _context.Apostas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aposta == null)
            {
                return NotFound();
            }

            return View(aposta);
        }
        //Delete POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aposta = await _context.Apostas.FindAsync(id);
            if (aposta != null)
            {
                _context.Apostas.Remove(aposta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
    