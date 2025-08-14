using Microsoft.AspNetCore.Mvc;
using Service.ViewModels;

namespace MVC.Controllers
{
    public class VehicleModelController : Controller
    {
        private readonly IVehicleService _service;

        public ModelController(IVehicleService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(string? sort, string? search, int? makeId, int page = 1)
        {
            ViewBag.Makes = await _service.GetAllMakesAsync();
            var models = await _service.GetAllModelsAsync(sort, search, page, 10, makeId);
            return View(models);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await _service.GetModelByIdAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Makes = await _service.GetAllMakesAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleModelViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Makes = await _service.GetAllMakesAsync();
                return View(model);
            }
            await _service.CreateModelAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _service.GetModelByIdAsync(id);
            if (model == null) return NotFound();
            ViewBag.Makes = await _service.GetAllMakesAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VehicleModelViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Makes = await _service.GetAllMakesAsync();
                return View(model);
            }
            var ok = await _service.UpdateModelAsync(model);
            if (!ok) return NotFound();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _service.GetModelByIdAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ok = await _service.DeleteModelAsync(id);
            if (!ok) return NotFound();
            return RedirectToAction(nameof(Index));
        }
    }
}
