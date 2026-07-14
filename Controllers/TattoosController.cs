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
        public async Task<IActionResult> Create(int? clientId)
        {
            var viewModel = new TattooFormViewModel { SessionDate = DateTime.Today };

            if (clientId.HasValue)
            {
                viewModel.ClientId = clientId.Value;
                viewModel.ReturnToClientId = clientId.Value;
            }

            await LoadClientSelectListAsync(viewModel.ClientId);
            return View(viewModel);
        }

        // POST: Tattoos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TattooFormViewModel viewModel)
        {
            await ValidateClientAsync(viewModel.ClientId);

            if (ModelState.IsValid)
            {
                var tattoo = ToTattoo(viewModel);

                await _tattooService.AddTattooAsync(tattoo);

                if (viewModel.ReturnToClientId.HasValue)
                {
                    return RedirectToAction("Details", "Clients", new { id = tattoo.ClientId });
                }

                return RedirectToAction(nameof(Index));
            }

            await LoadClientSelectListAsync(viewModel.ClientId);
            return View(viewModel);
        }

        // GET: Tattoos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var tattoo = await _tattooService.FindTattooAsync(id.Value);
            if (tattoo == null) return NotFound();

            var viewModel = ToFormViewModel(tattoo);
            viewModel.ReturnToClientId = tattoo.ClientId;

            await LoadClientSelectListAsync(viewModel.ClientId);
            return View(viewModel);
        }

        // POST: Tattoos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TattooFormViewModel viewModel)
        {
            if (id != viewModel.Id) return NotFound();

            await ValidateClientAsync(viewModel.ClientId);

            if (ModelState.IsValid)
            {
                try
                {
                    var tattoo = ToTattoo(viewModel);
                    await _tattooService.UpdateTattooAsync(tattoo);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _tattooService.TattooExistsAsync(viewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                if (viewModel.ReturnToClientId.HasValue)
                {
                    return RedirectToAction("Details", "Clients", new { id = viewModel.ClientId });
                }

                return RedirectToAction(nameof(Index));
            }

            await LoadClientSelectListAsync(viewModel.ClientId);
            return View(viewModel);
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

        private async Task LoadClientSelectListAsync(int? selectedClientId = null)
        {
            ViewData["ClientId"] = new SelectList(await _clientService.GetAllClientsAsync(), "Id", "Name", selectedClientId);
        }

        private async Task ValidateClientAsync(int clientId)
        {
            if (!await _clientService.ClientExistsAsync(clientId))
            {
                ModelState.AddModelError(nameof(TattooFormViewModel.ClientId), "Selecione um cliente valido.");
            }
        }

        private static Tattoo ToTattoo(TattooFormViewModel viewModel)
        {
            return new Tattoo
            {
                Id = viewModel.Id,
                SessionDate = viewModel.SessionDate,
                ValuePaid = viewModel.ValuePaid,
                PaymentMethod = viewModel.PaymentMethod,
                PostScript = viewModel.PostScript,
                ClientId = viewModel.ClientId
            };
        }

        private static TattooFormViewModel ToFormViewModel(Tattoo tattoo)
        {
            return new TattooFormViewModel
            {
                Id = tattoo.Id,
                SessionDate = tattoo.SessionDate,
                ValuePaid = tattoo.ValuePaid,
                PaymentMethod = tattoo.PaymentMethod,
                PostScript = tattoo.PostScript,
                ClientId = tattoo.ClientId
            };
        }
    }
}
