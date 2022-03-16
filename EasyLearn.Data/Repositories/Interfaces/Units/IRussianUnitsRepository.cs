using EasyLearn.Data.Enums;
using EasyLearn.Data.Models;
using System.Threading.Tasks;

namespace EasyLearn.Data.Repositories.Interfaces
{
    public interface IRussianUnitsRepository
    {
        bool IsUnitExist(int unitId);
        Task<bool> IsUnitExistAsync(int unitId);
        bool IsUnitExist(string value, UnitType type);
        Task<bool> IsUnitExistAsync(string value, UnitType type);
        RussianUnit? GetUnitByValueAndType(string value, UnitType type);
        Task<RussianUnit> GetUnitByValueAndTypeAsync(string value, UnitType type);
        RussianUnit GetUnitById(int unitId);
        Task<RussianUnit> GetUnitByIdAsync(int unitId);
        Task<RussianUnit?> CreateUnit(string value, UnitType type);
        Task<RussianUnit?> GetOrCreateUnit(string value, UnitType type);
    }
}
