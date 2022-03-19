using System.Threading.Tasks;
using EasyLearn.Data.Enums;
using EasyLearn.Data.Models;

namespace EasyLearn.Data.Repositories.Interfaces
{
    public interface IEnglishUnitRepository
    {
        bool IsUnitExist(int unitId);
        bool IsUnitExist(string value, UnitType type);
        Task<bool> IsUnitExistAsync(int unitId);
        Task<bool> IsUnitExistAsync(string value, UnitType type);
        EnglishUnit GetUnit(int unitiId);
        EnglishUnit GetUnit(string value, UnitType type);
        Task<EnglishUnit> GetUnitAsync(int unitId);
        Task<EnglishUnit> GetUnitAsync(string value, UnitType type);
        EnglishUnit? TryGetUnit(int unitId);
        EnglishUnit? TryGetUnit(string value, UnitType type);
        Task<EnglishUnit> CreateUnit(string value, UnitType type);
        Task<EnglishUnit> GetOrCreateUnit(string value, UnitType type);
    }
}
