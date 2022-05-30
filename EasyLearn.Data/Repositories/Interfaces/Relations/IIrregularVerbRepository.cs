using EasyLearn.Data.DTO;
using EasyLearn.Data.Models;
using System.Collections.Generic;

namespace EasyLearn.Data.Repositories.Interfaces
{
    public interface IIrregularVerbRepository
    {
        IEnumerable<IrregularVerb> GetAllIrregularVerbs();
        void SaveDictationResults(List<Answer> answers);
    }
}
