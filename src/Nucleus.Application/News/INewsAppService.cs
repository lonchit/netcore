using System.Collections.Generic;
using System.Threading.Tasks;
using Nucleus.Application.News.Dto;
using Nucleus.Utilities.Collections;

namespace Nucleus.Application.News
{
    public interface INewsAppService
    {
        Task<List<NewsListOutput>> GetNewsAsync();
        Task AddNewsAsync(Core.News.News news);
    }
}
