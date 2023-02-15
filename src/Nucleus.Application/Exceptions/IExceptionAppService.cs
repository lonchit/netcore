using System.Collections.Generic;
using System.Threading.Tasks;
using Nucleus.Application.Exceptions.Dto;

namespace Nucleus.Application.Exceptions
{
    public interface IExceptionAppService
    {
        List<ExceptionsListOutput> GetExceptions();
        Task AddExceptionAsync(string text);
    }
}
