using ApplicazioneAlbergo_core_Entity.data;
using ApplicazioneAlbergo_core_Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApplicazioneAlbergo_core_Entity.Controllers
{
    public class CameraController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CameraController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cameras
        public async Task<IActionResult> Index()
        {
            return View(await _context.Camere.ToListAsync());
        }

        // GET: Cameras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var camera = await _context.Camere
                .FirstOrDefaultAsync(m => m.IdCamera == id);
            if (camera == null)
            {
                return NotFound();
            }

            return View(camera);
        }

        // GET: Cameras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cameras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumeroCamera,TipoCamera,Prezzo")] Camera camera)
        {
            ModelState.Remove("Prenotazioni");

            if (ModelState.IsValid)
            {
                _context.Add(camera);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Camera Inserita con successo.";
                return RedirectToAction(nameof(Index));
            }
            TempData["Errore"] = "Errore nell'inserimento della nuova camera.";
            return View(camera);
        }

        // GET: Cameras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var camera = await _context.Camere.FindAsync(id);
            if (camera == null)
            {
                return NotFound();
            }
            return View(camera);
        }

        // POST: Cameras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCamera,NumeroCamera,TipoCamera,Prezzo")] Camera camera)
        {
            if (id != camera.IdCamera)
            {
                return NotFound();
            }

            ModelState.Remove("Prenotazioni");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(camera);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Camera Modificata con successo.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CameraExists(camera.IdCamera))
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
            TempData["Errore"] = "Errore durante la validazione del modello.";
            return View(camera);
        }

        // GET: Cameras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var camera = await _context.Camere
                .FirstOrDefaultAsync(m => m.IdCamera == id);
            if (camera == null)
            {
                return NotFound();
            }

            return View(camera);
        }

        // POST: Cameras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var camera = await _context.Camere.FindAsync(id);
            if (camera != null)
            {
                _context.Camere.Remove(camera);
            }

            await _context.SaveChangesAsync();
            TempData["Message"] = "Camera Eliminata con successo.";

            return RedirectToAction(nameof(Index));
        }

        private bool CameraExists(int id)
        {
            return _context.Camere.Any(e => e.IdCamera == id);
        }
    }
}
