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
        private readonly EasyLearnContext context;

        public RussianUnitsRepository(EasyLearnContext context)
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

        public RussianUnit? GetUnitByValueAndType(string value, UnitType type)
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

        public async Task<RussianUnit?> CreateUnit(string value, UnitType type)
        {
            if (string.IsNullOrEmpty(value) || value.Length < ModelConstants.UnitValueMinLength)
            {
                return null;
            }

            if (await IsUnitExistAsync(value, type))
            {
                return null;
            }

            RussianUnit newUnit = new RussianUnit
            {
                Value = value,
                Type = type,
                CreationDateUtc = DateTime.UtcNow,
            };

            context.RussianUnits.Add(newUnit);
            await context.SaveChangesAsync();

            return newUnit;
        }

        public async Task<RussianUnit?> GetOrCreateUnit(string value, UnitType type)
        {
            RussianUnit? russianUnit = await context.RussianUnits.FirstOrDefaultAsync(unit => unit.Value == value && unit.Type == type);

            if (russianUnit is not null)
            {
                return russianUnit;
            }

            return await CreateUnit(value, type);
        }
    }
}
