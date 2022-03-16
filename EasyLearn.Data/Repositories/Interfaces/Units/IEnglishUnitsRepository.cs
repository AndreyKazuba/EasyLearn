using EasyLearn.Data.Enums;
using EasyLearn.Data.Models;
using System.Threading.Tasks;

namespace EasyLearn.Data.Repositories.Interfaces
{
    public interface IEnglishUnitsRepository
    {
        bool IsUnitExist(int unitId);
        Task<bool> IsUnitExistAsync(int unitId);
        bool IsUnitExist(string value, UnitType type);
        Task<bool> IsUnitExistAsync(string value, UnitType type);
        EnglishUnit? GetUnitByValueAndType(string value, UnitType type);
        Task<EnglishUnit> GetUnitByValueAndTypeAsync(string value, UnitType type);
        EnglishUnit GetUnitById(int unitiId);
        Task<EnglishUnit> GetUnitByIdAsync(int unitId);
        Task<EnglishUnit?> CreateUnit(string value, UnitType type);
        Task<EnglishUnit?> GetOrCreateUnit(string value, UnitType type);
    }
}
