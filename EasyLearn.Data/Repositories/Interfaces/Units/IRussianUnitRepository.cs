using System.Threading.Tasks;
using EasyLearn.Data.Enums;
using EasyLearn.Data.Models;

namespace EasyLearn.Data.Repositories.Interfaces
{
    public interface IRussianUnitRepository
    {
        bool IsUnitExist(int unitId);
        bool IsUnitExist(string value, UnitType type);
        Task<bool> IsUnitExistAsync(int unitId);
        Task<bool> IsUnitExistAsync(string value, UnitType type);
        RussianUnit GetUnit(int unitId);
        RussianUnit GetUnit(string value, UnitType type);
        Task<RussianUnit> GetUnitAsync(int unitId);
        Task<RussianUnit> GetUnitAsync(string value, UnitType type);
        RussianUnit? TryGetUnit(int unitId);
        RussianUnit? TryGetUnit(string value, UnitType type);
        Task<RussianUnit> CreateUnit(string value, UnitType type);
        Task<RussianUnit?> GetOrCreateUnit(string value, UnitType type);
    }
}
