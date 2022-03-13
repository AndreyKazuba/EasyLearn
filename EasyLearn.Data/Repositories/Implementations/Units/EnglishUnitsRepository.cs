using EasyLearn.Data.Enums;
using EasyLearn.Data.Helpers;
using EasyLearn.Data.Models;
using EasyLearn.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EasyLearn.Data.Repositories.Implementations
{
    public class EnglishUnitsRepository : IEnglishUnitsRepository
    {
        private readonly EasyLearnDbContext context;

        public EnglishUnitsRepository(EasyLearnDbContext context)
        {
            this.context = context;
        }

        public bool IsUnitExist(int unitId)
        {
            return context.EnglishUnits.Any(unit => unit.Id == unitId);
        }

        public async Task<bool> IsUnitExistAsync(int unitId)
        {
            return await context.EnglishUnits.AnyAsync(unit => unit.Id == unitId);
        }

        public bool IsUnitExist(string value, UnitType type)
        {
            return context.EnglishUnits.Any(unit => unit.Value == value && unit.Type == type);
        }

        public async Task<bool> IsUnitExistAsync(string value, UnitType type)
        {
            return await context.EnglishUnits.AnyAsync(unit => unit.Value == value && unit.Type == type);
        }

        public EnglishUnit GetUnitByValueAndType(string value, UnitType type)
        {
            return context.EnglishUnits.FirstOrDefault(unit => StringHelper.Equals(unit.Value, value) && unit.Type == type);
        }

        public async Task<EnglishUnit> GetUnitByValueAndTypeAsync(string value, UnitType type)
        {
            return await context.EnglishUnits.FirstOrDefaultAsync(unit => StringHelper.Equals(unit.Value, value) && unit.Type == type);
        }

        public EnglishUnit GetUnitById(int unitId)
        {
            return context.EnglishUnits.FirstOrDefault(unit => unit.Id == unitId);
        }

        public async Task<EnglishUnit> GetUnitByIdAsync(int unitId)
        {
            return await context.EnglishUnits.FirstOrDefaultAsync(unit => unit.Id == unitId);
        }

        public async Task<bool> AddUnit(string value, UnitType type)
        {
            if (value == null || value.Length < AppConstants.UnitMinLength)
            {
                return false;
            }

            if (await IsUnitExistAsync(value, type))
            {
                return false;
            }

            EnglishUnit newUnit = new EnglishUnit
            {
                Value = value,
                Type = type,
                CreationDateUtc = DateTime.UtcNow,
            };

            context.EnglishUnits.Add(newUnit);
            await context.SaveChangesAsync();

            return true;
        }
    }
}
