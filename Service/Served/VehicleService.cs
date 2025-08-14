using AutoMapper;
using AutoMapper.QueryableExtensions;
using Service.DataAccess;
using Service.Interfaces;
using Service.Models;
using Service.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Project.Service.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly VehicleFactoryContext _context;
        private readonly IMapper _mapper;

        public VehicleService(VehicleFactoryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VehicleMakeViewModel>> GetAllMakesAsync(string? sort, string? search, int page, int pageSize)
        {
            var query = _context.VehicleMakes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(m => m.Name.Contains(search) || m.Abrv.Contains(search));

            query = sort switch
            {
                "name_desc" => query.OrderByDescending(m => m.Name),
                "abrv" => query.OrderBy(m => m.Abrv),
                "abrv_desc" => query.OrderByDescending(m => m.Abrv),
                _ => query.OrderBy(m => m.Name)
            };

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<VehicleMakeViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<VehicleMakeViewModel?> GetMakeByIdAsync(int id) =>
            await _context.VehicleMakes
                .Where(m => m.Id == id)
                .ProjectTo<VehicleMakeViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

        public async Task<int> CreateMakeAsync(VehicleMakeViewModel model)
        {
            var entity = _mapper.Map<VehicleMake>(model);
            _context.VehicleMakes.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> UpdateMakeAsync(VehicleMakeViewModel model)
        {
            var entity = await _context.VehicleMakes.FindAsync(model.Id);
            if (entity == null) return false;

            _mapper.Map(model, entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteMakeAsync(int id)
        {
            var entity = await _context.VehicleMakes.FindAsync(id);
            if (entity == null) return false;

            _context.VehicleMakes.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<IEnumerable<VehicleModelViewModel>> GetAllModelsAsync(string? sort, string? search, int page, int pageSize, int? makeId)
        {
            var query = _context.VehicleModels.AsQueryable();

            if (makeId.HasValue)
                query = query.Where(m => m.MakeId == makeId.Value);

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(m => m.Name.Contains(search) || m.Abrv.Contains(search));

            query = sort switch
            {
                "name_desc" => query.OrderByDescending(m => m.Name),
                "abrv" => query.OrderBy(m => m.Abrv),
                "abrv_desc" => query.OrderByDescending(m => m.Abrv),
                _ => query.OrderBy(m => m.Name)
            };

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<VehicleModelViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<VehicleModelViewModel?> GetModelByIdAsync(int id) =>
            await _context.VehicleModels
                .Where(m => m.Id == id)
                .ProjectTo<VehicleModelViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

        public async Task<int> CreateModelAsync(VehicleModelViewModel model)
        {
            var entity = _mapper.Map<VehicleModel>(model);
            _context.VehicleModels.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> UpdateModelAsync(VehicleModelViewModel model)
        {
            var entity = await _context.VehicleModels.FindAsync(model.Id);
            if (entity == null) return false;

            _mapper.Map(model, entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteModelAsync(int id)
        {
            var entity = await _context.VehicleModels.FindAsync(id);
            if (entity == null) return false;

            _context.VehicleModels.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}