using EasyLearn.Data.Enums;
using EasyLearn.Data.Models;
using System.Threading.Tasks;

namespace EasyLearn.Data.Repositories.Interfaces
{
    public interface IRussianUnitsRepository
    {
        bool IsUnitExist(int wordId);
        Task<bool> IsUnitExistAsync(int wordId);
        bool IsUnitExist(string value, UnitType type);
        Task<bool> IsUnitExistAsync(string value, UnitType type);
        RussianUnit GetUnitByValueAndType(string value, UnitType type);
        Task<RussianUnit> GetUnitByValueAndTypeAsync(string value, UnitType type);
        RussianUnit GetUnitById(int wordId);
        Task<RussianUnit> GetUnitByIdAsync(int wordId);
        Task<bool> AddUnit(string value, UnitType type);
    }
}
