using System.Threading.Tasks;
using InternetSpeedLogger.Models;

namespace InternetSpeedLogger.Database
{
    public interface IResultRepository
    {
        Task AddResult(Result result);
    }
}