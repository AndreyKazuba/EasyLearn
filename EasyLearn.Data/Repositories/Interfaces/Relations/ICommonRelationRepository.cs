﻿using EasyLearn.Data.Enums;
using EasyLearn.Data.Models;
using System.Threading.Tasks;

namespace EasyLearn.Data.Repositories.Interfaces
{
    public interface ICommonRelationRepository
    {
        bool IsCommonRelationExist(int russianUnitId, int englishUnitId, int dictionaryId);
        Task<CommonRelation> CreateCommonRelation(string russianUnitValue, UnitType russianUnitType, string englishUnitValue, UnitType englishUnitType, int dictionaryId, string? comment);
        Task DeleteAllDictionaryRelations(int dictionaryId);
    }
}
