using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppTattoo.Data;
using WebAppTattoo.Models;
using WebAppTattoo.Services;

namespace WebAppTattoo.Controllers
{
    public class ClientsController : Controller
    {
        private readonly ClientService _clientService;

        // O construtor agora injeta o serviço, não o DbContext
        public ClientsController(ClientService clientService)
        {
            _clientService = clientService;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            // O serviço é responsável por buscar a lista de clientes
            return View(await _clientService.GetAllClientsAsync());
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // O serviço busca o cliente, incluindo suas tatuagens
            var client = await _clientService.GetClientWithTattoosAsync(id.Value);

            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CellPhone,Email,BirthDate,CPF,Address,Instagram")] Client client)
        {
            if (ModelState.IsValid)
            {
                // O serviço lida com a lógica de adição e o salvamento
                await _clientService.AddClientAsync(client);
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // O serviço busca o cliente pelo Id
            var client = await _clientService.FindClientAsync(id.Value);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CellPhone,Email,BirthDate,CPF,Address,Instagram")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // O serviço lida com a atualização
                    await _clientService.UpdateClientAsync(client);
                }
                catch (DbUpdateConcurrencyException)
                {
                    // O serviço pode incluir o método de checagem, 
                    // mas podemos manter aqui por ser uma checagem de concorrência.
                    if (!await _clientService.ClientExistsAsync(client.Id))
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
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // O serviço busca o cliente
            var client = await _clientService.FindClientAsync(id.Value);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // O serviço lida com a exclusão
            await _clientService.DeleteClientAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}