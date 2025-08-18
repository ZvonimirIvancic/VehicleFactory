using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.ViewModels;

namespace MVC.Controllers
{
    public class MakeController : Controller
    {
        private readonly IVehicleService _service;

        public MakeController(IVehicleService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(string? sort, string? search, int page = 1)
        {
            var makes = await _service.GetAllMakesAsync(sort, search, page, 10);
            return View(makes);
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
