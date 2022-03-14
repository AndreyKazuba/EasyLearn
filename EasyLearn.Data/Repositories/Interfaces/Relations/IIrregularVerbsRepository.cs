using EasyLearn.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLearn.Data.Repositories.Interfaces
{
    public interface IIrregularVerbsRepository
    {
        IEnumerable<IrregularVerb> GetAllIrregularVerbs();
    }
}
