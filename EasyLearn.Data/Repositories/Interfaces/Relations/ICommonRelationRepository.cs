using EasyLearn.Data.Enums;
using EasyLearn.Data.Models;
using System.Threading.Tasks;

namespace EasyLearn.Data.Repositories.Interfaces
{
    public interface ICommonRelationRepository
    {
        bool IsCommonRelationExist(int russianUnitId, int englishUnitId, int dictionaryId);
        bool IsCommonRelationExist(string russianUnitValue, UnitType russianUnitType, string englishUnitValue, UnitType englishUnitType, int dictionaryId);
        CommonRelation GetCommonRelation(int commonRelationId);
        Task<CommonRelation> GetCommonRelationAsync(int commonRelationId);
        Task<CommonRelation> CreateCommonRelation(
            string russianUnitValue,
            UnitType russianUnitType,
            string englishUnitValue,
            UnitType englishUnitType,
            int dictionaryId,
            string? comment,
            string? firstExampleRussianValue,
            string? firstExampleEnglishValue,
            string? secondExampleRussianValue,
            string? secondExampleEnglishValue);
        Task<CommonRelation> UpdateCommonRelation(
            int commonRelationId,
            string? comment,
            string? firstExampleRussianValue,
            string? firstExampleEnglishValue,
            string? secondExampleRussianValue,
            string? secondExampleEnglishValue);
        Task DeleteAllDictionaryRelations(int dictionaryId);
        Task DeleteCommonRelation(int commonRelationId);
    }
}
