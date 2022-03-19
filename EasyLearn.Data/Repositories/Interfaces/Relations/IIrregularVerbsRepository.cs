using EasyLearn.Data.Models;
using System.Collections.Generic;

namespace EasyLearn.Data.Repositories.Interfaces
{
    public interface IIrregularVerbsRepository
    {
        IEnumerable<IrregularVerb> GetAllIrregularVerbs();
    }
}
