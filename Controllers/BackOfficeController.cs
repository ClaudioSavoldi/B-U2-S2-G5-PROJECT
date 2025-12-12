using B_U2_S2_G5_PROJECT.Models.Dto;
using B_U2_S2_G5_PROJECT.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace B_U2_S2_G5_PROJECT.Controllers
{
    public class BackOfficeController : Controller
    {
        private readonly ClienteService _clienteService;
        private readonly CameraService _cameraService;
        private readonly PrenotazioneService _prenotazioneService;

        public BackOfficeController(ClienteService clienteService, CameraService cameraService, PrenotazioneService prenotazioneService)
        {
            _clienteService = clienteService;
            _cameraService = cameraService;
            _prenotazioneService = prenotazioneService;
        }

        public async Task<SelectList> ClienteData()
        {
            var clienteData = (await _clienteService.GetClientiAsync()).Select(c => new
            {
                Id = c.Id,
                Text = $"{c.Nome} {c.Cognome} Email:{c.Email}"
            });
            return new SelectList(clienteData, "Id", "Text");
        }

        public async Task<IActionResult> Index()
        {
            var model = new BackOfficeViewModel();
            ViewBag.Clienti = await ClienteData();
            ViewBag.Camere = new SelectList(await _cameraService.GetCamereAsync(), "Id", "Numero");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCliente(Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Clienti = await ClienteData();
                ViewBag.Camere = new SelectList(await _cameraService.GetCamereAsync(), "Id", "Numero");
                return View("Index", new BackOfficeViewModel { Cliente = cliente });
            }

            if (cliente.Id == Guid.Empty)
                cliente.Id = Guid.NewGuid();

            await _clienteService.CreateClienteAsync(cliente);
            TempData["SuccessMessage"] = "Cliente creato con successo!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateCamera(Camera camera)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Clienti = await ClienteData();
                ViewBag.Camere = new SelectList(await _cameraService.GetCamereAsync(), "Id", "Numero");
                return View("Index", new BackOfficeViewModel { Camera = camera });
            }

            if (camera.Id == Guid.Empty)
                camera.Id = Guid.NewGuid();

            await _cameraService.CreateCameraAsync(camera);
            TempData["SuccessMessage"] = "Camera creata con successo!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreatePrenotazione(Prenotazione prenotazione)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Clienti = await ClienteData();
                ViewBag.Camere = new SelectList(await _cameraService.GetCamereAsync(), "Id", "Numero");
                return View("Index", new BackOfficeViewModel { Prenotazione = prenotazione });
            }

            if (prenotazione.Id == Guid.Empty)
                prenotazione.Id = Guid.NewGuid();

            await _prenotazioneService.CreatePrenotazioneAsync(prenotazione);
            TempData["SuccessMessage"] = "Prenotazione creata con successo!";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ListaClienti()
        {
            var clienti = await _clienteService.GetClientiAsync();
            return View(clienti);
        }

        public async Task<IActionResult> ListaCamere()
        {
            var camere = await _cameraService.GetCamereAsync();
            return View(camere);
        }

        public async Task<IActionResult> ListaPrenotazioni()
        {
            var prenotazioni = await _prenotazioneService.GetPrenotazioneAsync();
            var clienti = await _clienteService.GetClientiAsync();
            var camere = await _cameraService.GetCamereAsync();

            var model = prenotazioni.Select(p => new PrenotazioneListItemViewModel
            {
                Prenotazione = p,
                Cliente = clienti.FirstOrDefault(c => c.Id == p.ClienteId),
                Camera = camere.FirstOrDefault(c => c.Id == p.CameraId)
            }).ToList();

            return View(model);
        }

        // ------------------- UPDATE METODI -------------------

        // EDIT Cliente
        [HttpGet]
        public async Task<IActionResult> EditCliente(Guid id)
        {
            var cliente = await _clienteService.GetClienteById(id);
            if (cliente == null)
                return NotFound();

            return View(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCliente(Cliente cliente)
        {
            if (!ModelState.IsValid)
                return View(cliente);

            await _clienteService.UpdateCliente(cliente);
            TempData["SuccessMessage"] = "Cliente aggiornato!";
            return RedirectToAction("Index");
        }

        // EDIT Camera
        [HttpGet]
        public async Task<IActionResult> EditCamera(Guid id)
        {
            var camera = await _cameraService.GetCameraById(id);
            if (camera == null)
                return NotFound();

            return View(camera);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCamera(Camera camera)
        {
            if (!ModelState.IsValid)
                return View(camera);

            await _cameraService.UpdateCamera(camera);
            TempData["SuccessMessage"] = "Camera aggiornata!";
            return RedirectToAction("Index");
        }

        // EDIT Prenotazione
        [HttpGet]
        public async Task<IActionResult> EditPrenotazione(Guid id)
        {
            var prenotazione = await _prenotazioneService.GetPrenotazioneById(id);
            if (prenotazione == null)
                return NotFound();

            ViewBag.Clienti = await ClienteData();
            ViewBag.Camere = new SelectList(await _cameraService.GetCamereAsync(), "Id", "Numero");

            return View(prenotazione);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePrenotazione(Prenotazione prenotazione)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Clienti = await ClienteData();
                ViewBag.Camere = new SelectList(await _cameraService.GetCamereAsync(), "Id", "Numero");
                return View(prenotazione);
            }

            await _prenotazioneService.UpdatePrenotazione(prenotazione);
            TempData["SuccessMessage"] = "Prenotazione aggiornata!";
            return RedirectToAction("Index");
        }

        // ------------------- DELETE -------------------
        [HttpPost]
        public async Task<IActionResult> DeleteCliente(Guid id)
        {
            var cliente = await _clienteService.GetClienteById(id);
            if (cliente == null)
            {
                TempData["ErrorMessage"] = "Cliente non trovato!";
                return RedirectToAction("Index");
            }

            await _clienteService.DeleteCliente(cliente);
            TempData["SuccessMessage"] = "Cliente eliminato!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCamera(Guid id)
        {
            var camera = await _cameraService.GetCameraById(id);
            if (camera == null)
            {
                TempData["ErrorMessage"] = "Camera non trovata!";
                return RedirectToAction("Index");
            }

            await _cameraService.DeleteCamera(camera);
            TempData["SuccessMessage"] = "Camera eliminata!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeletePrenotazione(Guid id)
        {
            var prenotazione = await _prenotazioneService.GetPrenotazioneById(id);
            if (prenotazione == null)
            {
                TempData["ErrorMessage"] = "Prenotazione non trovata!";
                return RedirectToAction("Index");
            }

            await _prenotazioneService.DeletePrenotazione(prenotazione);
            TempData["SuccessMessage"] = "Prenotazione eliminata!";
            return RedirectToAction("Index");
        }
    }
}
