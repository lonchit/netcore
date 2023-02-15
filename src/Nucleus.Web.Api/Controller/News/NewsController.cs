using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nucleus.Application.News;
using Nucleus.Application.News.Dto;
using Nucleus.Core.Permissions;
using Nucleus.Core.Users;
using Nucleus.Utilities.Collections;
using Nucleus.Web.Core.Controllers;

namespace Nucleus.Web.Api.Controller.News
{
    [Route("api/[controller]")]
    public class NewsController : BaseController
    {
        private readonly INewsAppService _newsAppService;
        private readonly UserManager<User> _userManager;
        public NewsController(
            INewsAppService newsAppService,
            UserManager<User> userManager)
        {
            _userManager = userManager;
            _newsAppService = newsAppService;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<NewsListOutput>>> GetNews()
        {
            return Ok(await _newsAppService.GetNewsAsync());
        }

        [HttpPost("[action]")]
        [Authorize(Policy = DefaultPermissions.PermissionNameForAdministration)]
        public async Task<ActionResult<List<Nucleus.Core.News.News>>> PostNews([FromBody]CreateNewsInput news)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                Nucleus.Core.News.News createdNews = new Nucleus.Core.News.News()
                {
                    Title = news.Title,
                    Description = news.Description,
                    CreationDate = DateTime.Now,
                    CreatorId = user.Id
                };

                await _newsAppService.AddNewsAsync(createdNews);

                return Ok();
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }
    }
}
