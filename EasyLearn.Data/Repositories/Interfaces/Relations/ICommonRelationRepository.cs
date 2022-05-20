using EasyLearn.Data.Enums;
using EasyLearn.Data.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace EasyLearn.Data.Repositories.Interfaces
{
    public interface ICommonRelationRepository
    {
        bool IsCommonRelationExist(int russianUnitId, int englishUnitId, int dictionaryId);
        bool IsCommonRelationExist(string russianUnitValue, UnitType russianUnitType, string englishUnitValue, UnitType englishUnitType, int dictionaryId);
        Task<CommonRelation> GetCommonRelation(int commonRelationId);
        Task<CommonRelation> CreateCommonRelation(string russianUnitValue, UnitType russianUnitType, string englishUnitValue, UnitType englishUnitType, int dictionaryId, string? comment);
        Task DeleteAllDictionaryRelations(int dictionaryId);
        Task DeleteCommonRelation(int commonRelationId);
        Task AddExamples(int commonRelationId, List<Example> examples);
    }
}
