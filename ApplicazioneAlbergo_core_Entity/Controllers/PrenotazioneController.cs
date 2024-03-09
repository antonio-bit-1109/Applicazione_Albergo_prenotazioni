using ApplicazioneAlbergo_core_Entity.data;
using ApplicazioneAlbergo_core_Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ApplicazioneAlbergo_core_Entity.Controllers
{
	public class PrenotazioneController : Controller
	{
		private readonly ApplicationDbContext _context;

		public PrenotazioneController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Prenotaziones
		public async Task<IActionResult> Index()
		{
			var applicationDbContext = _context.Prenotazioni.Include(p => p.Camera).Include(p => p.Cliente).Include(p => p.Pensione).Include(p => p.Servizio);
			return View(await applicationDbContext.ToListAsync());
		}

		// GET: Prenotaziones/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var prenotazione = await _context.Prenotazioni
				.Include(p => p.Camera)
				.Include(p => p.Cliente)
				.Include(p => p.Pensione)
				.Include(p => p.Servizio)
				.FirstOrDefaultAsync(m => m.IdPrenotazione == id);
			if (prenotazione == null)
			{
				return NotFound();
			}

			return View(prenotazione);
		}

		// GET: Prenotaziones/Create
		public IActionResult Create()
		{
			ViewData["IdCamera"] = new SelectList(_context.Camere, "IdCamera", "NumeroCamera");
			ViewData["IdCliente"] = new SelectList(_context.Clienti, "IdCliente", "Nome");
			ViewData["IdPensione"] = new SelectList(_context.Pensioni, "IdPensione", "TipoPensione");
			ViewData["IdServizio"] = new SelectList(_context.Servizi, "IdServizio", "DescrizioneServizio");
			return View();
		}

		// POST: Prenotaziones/Create

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("DataInizioPrenotazione,DataFinePrenotazione,Acconto,IdCliente,IdCamera,IdPensione,IdServizio")] Prenotazione prenotazione)
		{
			ModelState.Remove("Cliente");
			ModelState.Remove("Camera");
			ModelState.Remove("Servizio");
			ModelState.Remove("Pensione");

			if (ModelState.IsValid)
			{
				_context.Add(prenotazione);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["IdCamera"] = new SelectList(_context.Camere, "IdCamera", "TipoCamera", prenotazione.IdCamera);
			ViewData["IdCliente"] = new SelectList(_context.Clienti, "IdCliente", "Nome", prenotazione.IdCliente);
			ViewData["IdPensione"] = new SelectList(_context.Pensioni, "IdPensione", "TipoPensione", prenotazione.IdPensione);
			ViewData["IdServizio"] = new SelectList(_context.Servizi, "IdServizio", "DescrizioneServizio", prenotazione.IdServizio);
			return View(prenotazione);
		}

		// GET: Prenotaziones/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var prenotazione = await _context.Prenotazioni.FindAsync(id);
			if (prenotazione == null)
			{
				return NotFound();
			}
			ViewData["IdCamera"] = new SelectList(_context.Camere, "IdCamera", "TipoCamera", prenotazione.IdCamera);
			ViewData["IdCliente"] = new SelectList(_context.Clienti, "IdCliente", "Nome", prenotazione.IdCliente);
			ViewData["IdPensione"] = new SelectList(_context.Pensioni, "IdPensione", "TipoPensione", prenotazione.IdPensione);
			ViewData["IdServizio"] = new SelectList(_context.Servizi, "IdServizio", "DescrizioneServizio", prenotazione.IdServizio);
			return View(prenotazione);
		}

		// POST: Prenotaziones/Edit/5

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("IdPrenotazione,DataInizioPrenotazione,DataFinePrenotazione,Acconto,IdCliente,IdCamera,IdPensione,IdServizio")] Prenotazione prenotazione)
		{
			if (id != prenotazione.IdPrenotazione)
			{
				return NotFound();
			}

			ModelState.Remove("Cliente");
			ModelState.Remove("Camera");
			ModelState.Remove("Servizio");
			ModelState.Remove("Pensione");

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(prenotazione);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!PrenotazioneExists(prenotazione.IdPrenotazione))
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
			ViewData["IdCamera"] = new SelectList(_context.Camere, "IdCamera", "TipoCamera", prenotazione.IdCamera);
			ViewData["IdCliente"] = new SelectList(_context.Clienti, "IdCliente", "Nome", prenotazione.IdCliente);
			ViewData["IdPensione"] = new SelectList(_context.Pensioni, "IdPensione", "TipoPensione", prenotazione.IdPensione);
			ViewData["IdServizio"] = new SelectList(_context.Servizi, "IdServizio", "DescrizioneServizio", prenotazione.IdServizio);
			return View(prenotazione);
		}

		// GET: Prenotaziones/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var prenotazione = await _context.Prenotazioni
				.Include(p => p.Camera)
				.Include(p => p.Cliente)
				.Include(p => p.Pensione)
				.Include(p => p.Servizio)
				.FirstOrDefaultAsync(m => m.IdPrenotazione == id);
			if (prenotazione == null)
			{
				return NotFound();
			}

			return View(prenotazione);
		}

		// POST: Prenotaziones/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var prenotazione = await _context.Prenotazioni.FindAsync(id);
			if (prenotazione != null)
			{
				_context.Prenotazioni.Remove(prenotazione);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool PrenotazioneExists(int id)
		{
			return _context.Prenotazioni.Any(e => e.IdPrenotazione == id);
		}

		[HttpGet]
		public async Task<IActionResult> FetchTuttePrenotaz_PensCompleta()
		{
			//SELECT idprenotazione , datainizioprenotazione , datafineprenotazione , Acconto , TipoPensione
			//FROM Prenotazioni as pre inner join pensioni as pens on pre.IdPensione  =  pens.IdPensione
			//WHERE TipoPensione = 'All inclusive' 


			var prenotazioniAllInclusive = await _context.Prenotazioni
				.Include(p => p.Pensione)
				.Where(p => p.Pensione.TipoPensione == "All inclusive")
				.Select(p => new
				{
					p.IdPrenotazione,
					p.DataInizioPrenotazione,
					p.DataFinePrenotazione,
					p.Acconto,
					p.Pensione.TipoPensione
				})
				.ToListAsync();

			return Json(prenotazioniAllInclusive);
		}



		[HttpGet]
		public async Task<IActionResult> CostoTotSoggiorno(int? idPrenotazione)
		{
			var costoTotaleSoggiorno = await _context.Prenotazioni
				.Where(p => p.IdPrenotazione == idPrenotazione)
				.Select(p => p.Acconto + p.Servizio.CostoServizio + p.Pensione.Prezzo + p.Camera.Prezzo)
				.SumAsync();


			var giorniPermanenza = await _context.Prenotazioni
				.Where(p => p.IdPrenotazione == idPrenotazione)
				.Select(p => (p.DataFinePrenotazione - p.DataInizioPrenotazione).Days)
				.FirstOrDefaultAsync();

			return Json(Convert.ToInt32(costoTotaleSoggiorno) * Convert.ToInt32(giorniPermanenza));

		}

	}
}
