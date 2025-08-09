using LosNeteros.Datos;
using LosNeteros.Models;
using Microsoft.AspNetCore.Mvc;

namespace LosNeteros.Controllers
{
    public class ClienteBCPController : Controller
    {
        private readonly ClienteBCPRepository _repository;

        public ClienteBCPController(ClienteBCPRepository repository)
        {
            _repository = repository;
        }


        // GET: ClienteBCP/Index
        public async Task<IActionResult> Index()
        {
            var clientes = await _repository.ObtenerTodos();
            return View(clientes);
        }

        #region create
        // GET: ClienteBCP/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClienteBCP/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClienteBCP cliente)
        {
            if (ModelState.IsValid)
            {
                await _repository.Crear(cliente);
                return RedirectToAction("Index");
            }
            return View(cliente);
        }
        #endregion

        #region edit
        // GET: ClienteBCP/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var cliente = await _repository.ObtenerPorId(id);
            if (cliente == null) return NotFound();
            return View(cliente);
        }

        // POST: ClienteBCP/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClienteBCP cliente)
        {
            if (id != cliente.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _repository.Actualizar(cliente);
                return RedirectToAction("Index");
            }
            return View(cliente);
        }
        #endregion

        #region delete
        // GET: ClienteBCP/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _repository.ObtenerPorId(id);
            if (cliente == null) return NotFound();
            return View(cliente);
        }

        // POST: ClienteBCP/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.Eliminar(id);
            return RedirectToAction("Index");
        }
        #endregion

    }
}
