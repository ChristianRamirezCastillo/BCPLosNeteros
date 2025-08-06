using LosNeteros.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace LosNeteros.Web.Controllers
{
    public class ClienteBCPController : Controller
    {

        // Simulación de una lista en memoria (reemplázala con tu DbContext)
        private static List<ClienteBCP> _clientes = new List<ClienteBCP>();

        // GET: ClienteBCP/Index
        public IActionResult Index()
        {
            return View(_clientes);
        }

        // GET: ClienteBCP/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClienteBCP/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ClienteBCP cliente)
        {
            if (ModelState.IsValid)
            {
                cliente.Id = _clientes.Count + 1;
                _clientes.Add(cliente);
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: ClienteBCP/Edit/5
        public IActionResult Edit(int id)
        {
            var cliente = _clientes.FirstOrDefault(c => c.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: ClienteBCP/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ClienteBCP cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var clienteExistente = _clientes.FirstOrDefault(c => c.Id == id);
                if (clienteExistente == null)
                {
                    return NotFound();
                }

                clienteExistente.Nombres = cliente.Nombres;
                clienteExistente.Apellidos = cliente.Apellidos;
                clienteExistente.DNI = cliente.DNI;
                clienteExistente.Direccion = cliente.Direccion;
                clienteExistente.NumeroCuenta = cliente.NumeroCuenta;

                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: ClienteBCP/Delete/5
        public IActionResult Delete(int id)
        {
            var cliente = _clientes.FirstOrDefault(c => c.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: ClienteBCP/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var cliente = _clientes.FirstOrDefault(c => c.Id == id);
            if (cliente != null)
            {
                _clientes.Remove(cliente);
            }
            return RedirectToAction("Index");
        }

    }
}
