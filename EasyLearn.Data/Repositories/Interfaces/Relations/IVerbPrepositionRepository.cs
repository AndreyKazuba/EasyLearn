using EasyLearn.Data.Models;
using System.Threading.Tasks;

namespace EasyLearn.Data.Repositories.Interfaces
{
    public interface IVerbPrepositionRepository
    {
        Task<VerbPreposition?> CreateVerbPreposition(string verbValue, string prepositionValue, int verbPrepositionDictionaryId, string comment);
    }
}
