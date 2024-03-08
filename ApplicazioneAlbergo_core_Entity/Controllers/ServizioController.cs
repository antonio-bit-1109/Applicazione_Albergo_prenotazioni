using ApplicazioneAlbergo_core_Entity.data;
using ApplicazioneAlbergo_core_Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ApplicazioneAlbergo_core_Entity.Controllers
{
    public class ServizioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServizioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Servizios
        public async Task<IActionResult> Index()
        {
            string nomeUtente = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;


            if (nomeUtente != "admin")
            {
                TempData["Errore"] = "non hai il premesso per accedere a questa sezione.";
                return RedirectToAction("Index", "Login");
            }

            return View(await _context.Servizi.ToListAsync());
        }

        // GET: Servizios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servizio = await _context.Servizi
                .FirstOrDefaultAsync(m => m.IdServizio == id);
            if (servizio == null)
            {
                return NotFound();
            }

            return View(servizio);
        }

        // GET: Servizios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Servizios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DescrizioneServizio,CostoServizio")] Servizio servizio)
        {
            ModelState.Remove("Prenotazioni");

            if (ModelState.IsValid)
            {
                _context.Add(servizio);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Servizio Inserito con successo.";
                return RedirectToAction(nameof(Index));
            }
            TempData["Errore"] = "Errore nell inserimento del servizio.";
            return View(servizio);

        }

        // GET: Servizios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servizio = await _context.Servizi.FindAsync(id);
            if (servizio == null)
            {
                return NotFound();
            }
            return View(servizio);
        }

        // POST: Servizios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdServizio,DescrizioneServizio,CostoServizio")] Servizio servizio)
        {
            if (id != servizio.IdServizio)
            {
                return NotFound();
            }

            ModelState.Remove("Prenotazioni");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servizio);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Servizio Modificato con successo.";

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServizioExists(servizio.IdServizio))
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
            TempData["Errore"] = "Modello non valido.";
            return View(servizio);
        }

        // GET: Servizios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servizio = await _context.Servizi
                .FirstOrDefaultAsync(m => m.IdServizio == id);
            if (servizio == null)
            {
                return NotFound();
            }

            return View(servizio);
        }

        // POST: Servizios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var servizio = await _context.Servizi.FindAsync(id);
            if (servizio != null)
            {
                _context.Servizi.Remove(servizio);
            }

            await _context.SaveChangesAsync();
            TempData["Message"] = "Servizio cancellato con successo.";
            return RedirectToAction(nameof(Index));
        }

        private bool ServizioExists(int id)
        {
            return _context.Servizi.Any(e => e.IdServizio == id);
        }
    }
}
