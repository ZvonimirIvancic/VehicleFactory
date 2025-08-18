using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.ViewModels;
using X.PagedList.Extensions;

public class VehicleModelController : Controller
{
    private readonly IVehicleService _service;

    public VehicleModelController(IVehicleService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index(string? sort, string? search, int? makeId, int page = 1, int pageSize = 10)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;

        ViewBag.Makes = await _service.GetAllMakesAsync(null, null, 1, 100);
        var models = await _service.GetAllModelsAsync(sort, search, page, pageSize, makeId);
        var pagedModels = models.ToPagedList(page, pageSize);
        return View(pagedModels);
    }


    public async Task<IActionResult> Details(int id)
    {
        var model = await _service.GetModelByIdAsync(id);
        if (model == null) return NotFound();
        return View(model);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Makes = await _service.GetAllMakesAsync(null, null, 1, 100);
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(VMVehicleModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Makes = await _service.GetAllMakesAsync(null, null, 1, 100);
            return View(model);
        }

        await _service.CreateModelAsync(model);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var model = await _service.GetModelByIdAsync(id);
        if (model == null) return NotFound();
        ViewBag.Makes = await _service.GetAllMakesAsync(null, null, 1, 100);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(VMVehicleModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Makes = await _service.GetAllMakesAsync(null, null, 1, 10);
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