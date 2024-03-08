using ApplicazioneAlbergo_core_Entity.data;
using ApplicazioneAlbergo_core_Entity.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

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
    }
}
