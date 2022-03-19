using EasyLearn.Data.Enums;
using EasyLearn.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLearn.Data.Repositories.Interfaces
{
    public interface ICommonRelationRepository
    {
        Task<CommonRelation> CreateCommonRelation(string rusUnitValue, UnitType rusUnitType, string engUnitValue, UnitType engUnitType, int commonDictionaryId, string? comment);
        Task DeleteAllDictionaryRelations(int dictionaryId);
    }
}
