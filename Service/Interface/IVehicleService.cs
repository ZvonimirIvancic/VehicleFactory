using Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IVehicleService
    {
        Task<IEnumerable<VehicleMakeViewModel>> GetAllMakesAsync(string? sort, string? search, int page, int pageSize);
        Task<VehicleMakeViewModel?> GetMakeByIdAsync(int id);
        Task<int> CreateMakeAsync(VehicleMakeViewModel model);
        Task<bool> UpdateMakeAsync(VehicleMakeViewModel model);
        Task<bool> DeleteMakeAsync(int id);
        Task<IEnumerable<VehicleModelViewModel>> GetAllModelsAsync(string? sort, string? search, int page, int pageSize, int? makeId);
        Task<VehicleModelViewModel?> GetModelByIdAsync(int id);
        Task<int> CreateModelAsync(VehicleModelViewModel model);
        Task<bool> UpdateModelAsync(VehicleModelViewModel model);
        Task<bool> DeleteModelAsync(int id);
    }
}
