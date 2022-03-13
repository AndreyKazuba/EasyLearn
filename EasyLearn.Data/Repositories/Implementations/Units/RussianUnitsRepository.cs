using EasyLearn.Data.Repositories.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using EasyLearn.Data.Models;
using EasyLearn.Data.Enums;
using EasyLearn.Data.Helpers;
using System;

namespace EasyLearn.Data.Repositories.Implementations
{
    public class RussianUnitsRepository : IRussianUnitsRepository
    {
        private readonly EasyLearnDbContext context;

        public RussianUnitsRepository(EasyLearnDbContext context)
        {
            this.context = context;
        }

        public bool IsUnitExist(int unitId)
        {
            return context.RussianUnits.Any(unit => unit.Id == unitId);
        }

        public async Task<bool> IsUnitExistAsync(int unitId)
        {
            return await context.RussianUnits.AnyAsync(unit => unit.Id == unitId);
        }

        public bool IsUnitExist(string value, UnitType type)
        {
            return context.RussianUnits.Any(unit => StringHelper.Equals(unit.Value, value) && unit.Type == type);
        }

        public async Task<bool> IsUnitExistAsync(string value, UnitType type)
        {
            return await context.RussianUnits.AnyAsync(unit => StringHelper.Equals(unit.Value, value) && unit.Type == type);
        }

        public RussianUnit GetUnitByValueAndType(string value, UnitType type)
        {
            return context.RussianUnits.FirstOrDefault(unit => StringHelper.Equals(unit.Value, value) && unit.Type == type);
        }

        public async Task<RussianUnit> GetUnitByValueAndTypeAsync(string value, UnitType type)
        {
            return await context.RussianUnits.FirstOrDefaultAsync(unit => StringHelper.Equals(unit.Value, value) && unit.Type == type);
        }

        public RussianUnit GetUnitById(int unitId)
        {
            return context.RussianUnits.FirstOrDefault(unit => unit.Id == unitId);
        }

        public async Task<RussianUnit> GetUnitByIdAsync(int unitId)
        {
            return await context.RussianUnits.FirstOrDefaultAsync(unit => unit.Id == unitId);
        }

        public async Task<bool> AddUnit(string value, UnitType type)
        {
            if (await IsUnitExistAsync(value, type))
            {
                return false;
            }

            RussianUnit newUnit = new RussianUnit
            {
                Value = value,
                Type = type,
                CreationDateUtc = DateTime.UtcNow,
            };

            context.RussianUnits.Add(newUnit);
            await context.SaveChangesAsync();

            return true;
        }
    }
}
