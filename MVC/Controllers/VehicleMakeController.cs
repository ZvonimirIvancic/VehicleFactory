using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.ViewModels;
using X.PagedList.Extensions;

namespace MVC.Controllers
{
    public class VehicleMakeController : Controller
    {
        private readonly IVehicleService _service;
        public VehicleMakeController(IVehicleService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index(string? sort, string? search, int page = 1, int pageSize = 10)
        {
            var makes = await _service.GetAllMakesAsync(sort, search, page, pageSize);
            var pagedMakes = makes.ToPagedList(page, pageSize);
            return View(pagedMakes);
        }
        public async Task<IActionResult> Details(int id)
        {
            var make = await _service.GetMakeByIdAsync(id);
            if (make == null) return NotFound();
            return View(make);
        }
        public IActionResult Create() => View();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VMVehicleMake model)
        {
            if (!ModelState.IsValid) return View(model);
            await _service.CreateMakeAsync(model);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var make = await _service.GetMakeByIdAsync(id);
            if (make == null) return NotFound();
            return View(make);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VMVehicleMake model)
        {
            if (!ModelState.IsValid) return View(model);
            var ok = await _service.UpdateMakeAsync(model);
            if (!ok) return NotFound();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var make = await _service.GetMakeByIdAsync(id);
            if (make == null) return NotFound();
            return View(make);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ok = await _service.DeleteMakeAsync(id);
            if (!ok) return NotFound();
            return RedirectToAction(nameof(Index));
        }
    }
}