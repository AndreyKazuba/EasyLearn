using EasyLearn.Data.Enums;
using EasyLearn.Data.Models;
using System.Threading.Tasks;

namespace EasyLearn.Data.Repositories.Interfaces
{
    public interface IEnglishUnitsRepository
    {
        bool IsUnitExist(int wordId);
        Task<bool> IsUnitExistAsync(int wordId);
        bool IsUnitExist(string value, UnitType type);
        Task<bool> IsUnitExistAsync(string value, UnitType type);
        EnglishUnit GetUnitByValueAndType(string value, UnitType type);
        Task<EnglishUnit> GetUnitByValueAndTypeAsync(string value, UnitType type);
        EnglishUnit GetUnitById(int wordId);
        Task<EnglishUnit> GetUnitByIdAsync(int wordId);
        Task<bool> AddUnit(string value, UnitType type);
    }
}
