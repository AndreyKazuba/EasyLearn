using System.Threading.Tasks;
using EasyLearn.Data.Models;

namespace EasyLearn.Data.Repositories.Interfaces
{
    public interface IExamplesRepository
    {
        bool IsExampleExist(int exampleId);
        Task<bool> IsExampleExistAsync(int exampleId);
        Example GetExample(int exampleId);
        Task<Example> GetExampleAsync(int exampleId);
        Example? TryGetExample(int exampleId);
        Task<Example> CreateExample(string russianValue, string englishValue);
    }
}
