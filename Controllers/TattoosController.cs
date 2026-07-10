using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppTattoo.Models;
using WebAppTattoo.Models.ViewModels;
using WebAppTattoo.Services;

namespace WebAppTattoo.Controllers
{
    public class TattoosController : Controller
    {
        private readonly TattooService _tattooService;
        private readonly ClientService _clientService;

        public TattoosController(TattooService tattooService, ClientService clientService)
        {
            _tattooService = tattooService;
            _clientService = clientService;
        }

        // GET: Tattoos
        public async Task<IActionResult> Index()
        {
            return View(await _tattooService.GetAllTattoosWithClientsAsync());
        }

        // GET: Tattoos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var tattoo = await _tattooService.GetTattooWithClientAsync(id.Value);
            if (tattoo == null) return NotFound();

            return View(tattoo);
        }

        // GET: Tattoos/Create
        public async Task<IActionResult> Create()
        {
            // O ClientService é usado aqui para pegar os dados para o dropdown
            ViewData["ClientId"] = new SelectList(await _clientService.GetAllClientsAsync(), "Id", "Name");
            return View(new Tattoo { SessionDate = DateTime.Today });
        }

        // POST: Tattoos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SessionDate,ValuePaid,PaymentMethod,PostScript,ClientId")] Tattoo tattoo)
        {
            if (ModelState.IsValid)
            {
                await _tattooService.AddTattooAsync(tattoo);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(await _clientService.GetAllClientsAsync(), "Id", "Name", tattoo.ClientId);
            return View(tattoo);
        }

        // GET: Tattoos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var tattoo = await _tattooService.FindTattooAsync(id.Value);
            if (tattoo == null) return NotFound();
            ViewData["ClientId"] = new SelectList(await _clientService.GetAllClientsAsync(), "Id", "Name", tattoo.ClientId);
            return View(tattoo);
        }

        // POST: Tattoos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SessionDate,ValuePaid,PaymentMethod,PostScript,ClientId")] Tattoo tattoo)
        {
            if (id != tattoo.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _tattooService.UpdateTattooAsync(tattoo);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _tattooService.TattooExistsAsync(tattoo.Id))
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
            ViewData["ClientId"] = new SelectList(await _clientService.GetAllClientsAsync(), "Id", "Name", tattoo.ClientId);
            return View(tattoo);
        }

        // GET: Tattoos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var tattoo = await _tattooService.GetTattooWithClientAsync(id.Value);
            if (tattoo == null) return NotFound();

            return View(tattoo);
        }

        // POST: Tattoos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _tattooService.DeleteTattooAsync(id);
            return RedirectToAction(nameof(Index));
        }


        // GET: Tattoos/Summary
        public IActionResult Summary()
        {
            return View(new TattooSummaryViewModel());
        }

        // POST: Tattoos/Summary
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Summary(TattooSummaryViewModel viewModel)
        {
            if (viewModel.EndDate < viewModel.StartDate)
            {
                ModelState.AddModelError(nameof(TattooSummaryViewModel.EndDate), "A data final deve ser maior ou igual a data inicial.");
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var summary = await _tattooService.GetTattooSummaryAsync(viewModel.StartDate, viewModel.EndDate);

            viewModel.TotalTattoos = summary.totalTattoos;
            viewModel.TotalValue = summary.totalValue;
            viewModel.HasResult = true;

            return View(viewModel);
        }
    }
}
