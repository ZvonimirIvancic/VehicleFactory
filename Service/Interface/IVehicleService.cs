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
        Task<IEnumerable<VMVehicleMake>> GetAllMakesAsync(string? sort, string? search, int page, int pageSize);
        Task<VMVehicleMake?> GetMakeByIdAsync(int id);
        Task<int> CreateMakeAsync(VMVehicleMake model);
        Task<bool> UpdateMakeAsync(VMVehicleMake model);
        Task<bool> DeleteMakeAsync(int id);
        Task<IEnumerable<VMVehicleModel>> GetAllModelsAsync(string? sort, string? search, int page, int pageSize, int? makeId);
        Task<VMVehicleModel?> GetModelByIdAsync(int id);
        Task<int> CreateModelAsync(VMVehicleModel model);
        Task<bool> UpdateModelAsync(VMVehicleModel model);
        Task<bool> DeleteModelAsync(int id);
    }
}
