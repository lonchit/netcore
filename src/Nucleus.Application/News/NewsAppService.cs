using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nucleus.Application.News.Dto;
using Nucleus.EntityFramework;
using Nucleus.Utilities.Collections;
using Nucleus.Utilities.Extensions.Collections;

namespace Nucleus.Application.News
{
    public class NewsAppService : INewsAppService
    {
        private readonly NucleusDbContext _dbContext;
        public NewsAppService(NucleusDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<NewsListOutput>> GetNewsAsync()
        {
            return _dbContext.News.Select(x => new NewsListOutput()
            {
                Title = x.Title,
                Description = x.Description
            }).ToList();
        }

        public async Task AddNewsAsync(Core.News.News news)
        {
            _dbContext.News.Add(news);
            await _dbContext.SaveChangesAsync();
        }
    }
}
