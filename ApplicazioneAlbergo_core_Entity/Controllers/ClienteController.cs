using ApplicazioneAlbergo_core_Entity.data;
using ApplicazioneAlbergo_core_Entity.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ApplicazioneAlbergo_core_Entity.Controllers
{
    public class ClienteController : Controller
    {

        private readonly ApplicationDbContext _db;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        public ClienteController(ApplicationDbContext db, IAuthenticationSchemeProvider schemeProvider)
        {
            _db = db;
            _schemeProvider = schemeProvider;
        }
        // GET: ClienteController
        public ActionResult Index()
        {
            string nomeUtente = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            if (nomeUtente != "admin")
            {
                TempData["Errore"] = "non hai il premesso per accedere a questa sezione.";
                return RedirectToAction("Index", "Login");
            }

            var tuttiClienti = _db.Clienti.ToList();
            return View(tuttiClienti);
        }

        // GET: ClienteController/Details/5
        public ActionResult Details(int id)
        {
            var utenteSelezionato = _db.Clienti.FirstOrDefault(c => c.IdCliente == id);
            return View(utenteSelezionato);
        }

        // GET: ClienteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind(include: "Nome , Cognome , CodiceFiscale , Citta , Email , Cellulare")] Cliente cliente)
        {
            ModelState.Remove("Prenotazioni");

            if (ModelState.IsValid)
            {
                _db.Clienti.Add(cliente);
                _db.SaveChanges();
                TempData["Message"] = "Utente Inserito con successo.";
                return RedirectToAction("Index");
            }

            TempData["Errore"] = "Errore nell inserimento del cliente.";
            return RedirectToAction("Index");

        }

        // GET: ClienteController/Edit/5
        public ActionResult Edit(int id)
        {
            var ClienteDaModificare = _db.Clienti.Find(id);
            return View(ClienteDaModificare);
        }

        // POST: ClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind(include: "IdCliente ,Nome , Cognome , CodiceFiscale , Citta , Email , Cellulare")] Cliente cliente)
        {
            ModelState.Remove("Prenotazioni");

            if (ModelState.IsValid)
            {
                _db.Update(cliente);
                _db.SaveChanges();
                TempData["Message"] = "Utente Modificato con successo.";
                return RedirectToAction("Index");
            }

            TempData["Errore"] = "Errore nella modifica del cliente.";
            return RedirectToAction("Index");

        }

        // GET: ClienteController/Delete/5
        public ActionResult Delete(int id)
        {
            var ClienteDaEliminare = _db.Clienti.FirstOrDefault(c => c.IdCliente == id);
            return View(ClienteDaEliminare);
        }

        // POST: ClienteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Cliente cliente)
        {

            try
            {
                _db.Clienti.Remove(cliente);
                _db.SaveChanges();
                TempData["Message"] = "Utente Eliminato con successo.";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["Errore"] = "Errore nell eliminazione del cliente.";
                return RedirectToAction("Index");
            }

        }

        [HttpGet]
        public async Task<IActionResult> PrenotazioniCodiceFiscale(string? codiceFiscale)
        {

            var prenotazioniInbaseAlCodiceFiscale = await _db.Prenotazioni
                .Include(p => p.Cliente)
                .Where(p => p.Cliente.CodiceFiscale == codiceFiscale)
                .Select(p => new
                {
                    p.Cliente.Nome,
                    p.Cliente.Cognome,
                    p.Cliente.CodiceFiscale,
                    p.DataInizioPrenotazione,
                    p.DataFinePrenotazione,
                }).ToListAsync();

            //string query = $"SELECT * FROM Clienti AS cl INNER JOIN Prenotazioni AS pre ON cl.IdCliente = pre.IdCliente WHERE CodiceFiscale = {codiceFiscale}";
            ////var nomeParam = new SqlParameter("@param", codiceFiscale);

            //var prenotazioniInbaseAlCodiceFiscale = await _db.Prenotazioni.FromSqlRaw(query).ToListAsync();

            //string query = "select nome , cognome , codicefiscale , DataInizioprenotazione , DataFinePrenotazione FROM Clienti as cl inner join Prenotazioni as pre on cl.IdCliente = pre.IdCliente where CodiceFiscale = 'ewqewqeqfffff' "

            return Json(prenotazioniInbaseAlCodiceFiscale);

        }
    }
}
