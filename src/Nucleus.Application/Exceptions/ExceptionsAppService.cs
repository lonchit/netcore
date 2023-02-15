using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nucleus.Application.Exceptions.Dto;
using Nucleus.Core.Exceptions;
using Nucleus.EntityFramework;

namespace Nucleus.Application.Exceptions
{
    public class ExceptionsAppService : IExceptionAppService
    {
        private readonly NucleusDbContext _dbContext;
        public ExceptionsAppService(NucleusDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ExceptionsListOutput> GetExceptions()
        {
            return _dbContext.Exceptions.Select(x => new ExceptionsListOutput()
            {
                Text = x.Text,
                CreationDate = x.CreationDate
            }).ToList();
        }

        public async Task AddExceptionAsync(string text)
        {
            await _dbContext.Exceptions.AddAsync(new Exception()
            {
                Text = text
            });

            await _dbContext.SaveChangesAsync();
        }

    }
}
