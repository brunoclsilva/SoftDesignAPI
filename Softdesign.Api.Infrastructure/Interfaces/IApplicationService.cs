using Softdesign.API.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softdesign.Api.Infrastructure.Interfaces
{
    public interface IApplicationService
    {
        Task<List<Application>> Get();
        Task<Application> GetById(int id);
        Task<bool> Post(Application application);
        Task<bool> Path(Application application);
        Task<bool> Delete(int id);
    }
}
