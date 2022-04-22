using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EasyLearn.Data.Enums;
using EasyLearn.Data.Exceptions;
using EasyLearn.Data.Helpers;
using EasyLearn.Data.Models;
using EasyLearn.Data.Repositories.Interfaces;
using EasyLearn.Data.Constants;

namespace EasyLearn.Data.Repositories.Implementations
{
    public class EnglishUnitRepository : Repository, IEnglishUnitRepository
    {
        public EnglishUnitRepository(EasyLearnContext context) : base(context) { }

        #region Public members
        public bool IsUnitExist(int unitId) => context.EnglishUnits.Any(unit => unit.Id == unitId);
        public bool IsUnitExist(string value, UnitType type) => context.EnglishUnits.Any(unit => StringHelper.Equals(unit.Value, value) && unit.Type == type);
        public async Task<bool> IsUnitExistAsync(int unitId) => await context.EnglishUnits.AnyAsync(unit => unit.Id == unitId);
        public async Task<bool> IsUnitExistAsync(string value, UnitType type) => await context.EnglishUnits.AnyAsync(unit => StringHelper.Equals(unit.Value, value) && unit.Type == type);
        public EnglishUnit GetUnit(int unitId) => context.EnglishUnits.AsNoTracking().First(unit => unit.Id == unitId);
        public EnglishUnit GetUnit(string value, UnitType type) => context.EnglishUnits.AsNoTracking().First(unit => StringHelper.Equals(unit.Value, value) && unit.Type == type);
        public async Task<EnglishUnit> GetUnitAsync(int unitId) => await context.EnglishUnits.AsNoTracking().FirstAsync(unit => unit.Id == unitId);
        public async Task<EnglishUnit> GetUnitAsync(string value, UnitType type) => await context.EnglishUnits.AsNoTracking().FirstAsync(unit => StringHelper.Equals(unit.Value, value) && unit.Type == type);
        public EnglishUnit? TryGetUnit(int unitId) => context.EnglishUnits.AsNoTracking().FirstOrDefault(unit => unit.Id == unitId);
        public EnglishUnit? TryGetUnit(string value, UnitType type) => context.EnglishUnits.AsNoTracking().FirstOrDefault(unit => StringHelper.Equals(unit.Value, value) && unit.Type == type);
        public async Task<EnglishUnit> CreateUnit(string value, UnitType type)
        {
            ThrowIfAddingAttemptIncorrect(value, type);
            EnglishUnit newUnit = new EnglishUnit
            {
                Value = StringHelper.Prepare(value),
                Type = type,
                CreationDateUtc = DateTime.UtcNow,
            };
            context.EnglishUnits.Add(newUnit);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();
            return newUnit;
        }
        public async Task<EnglishUnit> GetOrCreateUnit(string value, UnitType type)
        {
            EnglishUnit? englistUnit = TryGetUnit(value, type);
            return englistUnit is not null ? englistUnit : await CreateUnit(value, type);
        }
        #endregion

        #region Private members
        private void ThrowIfAddingAttemptIncorrect(string value, UnitType type)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < ModelConstants.UnitValueMinLength || value.Length > ModelConstants.UnitValueMaxLength)
                throw new InvalidDbOperationException(ExceptionMessagesHelper.PropertyInvalidValue(nameof(EnglishUnit.Value), nameof(EnglishUnit), value));

            if (IsUnitExist(value, type))
                throw new InvalidDbOperationException($"Попытка добавить уже существующий {nameof(EnglishUnit)}: '{nameof(EnglishUnit.Value)} = {value}, {nameof(EnglishUnit.Type)} = {type}'");
        }
        #endregion
    }
}
