using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EasyLearn.Data.Models;
using EasyLearn.Data.Enums;
using EasyLearn.Data.Helpers;
using EasyLearn.Data.Exceptions;
using EasyLearn.Data.Repositories.Interfaces;

namespace EasyLearn.Data.Repositories.Implementations
{
    public class RussianUnitRepository : Repository, IRussianUnitRepository
    {
        public RussianUnitRepository(EasyLearnContext context) : base(context) { }

        #region Public members
        public bool IsUnitExist(int unitId) => context.RussianUnits.Any(unit => unit.Id == unitId);
        public bool IsUnitExist(string value, UnitType type) => context.RussianUnits.Any(unit => StringHelper.Equals(unit.Value, value) && unit.Type == type);
        public async Task<bool> IsUnitExistAsync(int unitId) => await context.RussianUnits.AnyAsync(unit => unit.Id == unitId);
        public async Task<bool> IsUnitExistAsync(string value, UnitType type) => await context.RussianUnits.AnyAsync(unit => StringHelper.Equals(unit.Value, value) && unit.Type == type);
        public RussianUnit GetUnit(int unitId) => context.RussianUnits.AsNoTracking().First(unit => unit.Id == unitId);
        public RussianUnit GetUnit(string value, UnitType type) => context.RussianUnits.AsNoTracking().First(unit => StringHelper.Equals(unit.Value, value) && unit.Type == type);
        public async Task<RussianUnit> GetUnitAsync(int unitId) => await context.RussianUnits.AsNoTracking().FirstAsync(unit => unit.Id == unitId);
        public async Task<RussianUnit> GetUnitAsync(string value, UnitType type) => await context.RussianUnits.AsNoTracking().FirstAsync(unit => StringHelper.Equals(unit.Value, value) && unit.Type == type);
        public RussianUnit? TryGetUnit(int unitId) => context.RussianUnits.AsNoTracking().FirstOrDefault(unit => unit.Id == unitId);
        public RussianUnit? TryGetUnit(string value, UnitType type) => context.RussianUnits.AsNoTracking().FirstOrDefault(unit => StringHelper.Equals(unit.Value, value) && unit.Type == type);
        public async Task<RussianUnit> CreateUnit(string value, UnitType type)
        {
            ThrowIfAddingAttemptIncorrect(value, type);
            RussianUnit newUnit = new RussianUnit
            {
                Value = StringHelper.Prepare(value),
                Type = type,
                CreationDateUtc = DateTime.UtcNow,
            };
            context.RussianUnits.Add(newUnit);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();
            return newUnit;
        }
        public async Task<RussianUnit> GetOrCreateUnit(string value, UnitType type)
        {
            RussianUnit? russianUnit = TryGetUnit(value, type);
            return russianUnit is not null ? russianUnit : await CreateUnit(value, type);
        }
        #endregion

        #region Private members
        protected void ThrowIfAddingAttemptIncorrect(string value, UnitType unitType)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < ModelConstants.UnitValueMinLength || value.Length > ModelConstants.UnitValueMaxLength)
                throw new InvalidDbOperationException(ExceptionMessagesHelper.PropertyInvalidValue(nameof(RussianUnit.Value), nameof(RussianUnit), value));

            if (IsUnitExist(value, unitType))
                throw new InvalidDbOperationException($"Попытка добавить уже существующий {nameof(RussianUnit)}: '{nameof(RussianUnit.Value)} = {value}, {nameof(RussianUnit.Type)} = {unitType}'");
        }
        #endregion
    }
}
