using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nucleus.Application.Exceptions;
using Nucleus.Application.Exceptions.Dto;
using Nucleus.Web.Core.Controllers;

namespace Nucleus.Web.Api.Controller.Exceptions
{
    [Route("api/[controller]")]
    public class ExceptionController : BaseController
    {
        private readonly IExceptionAppService _exceptionAppService;

        public ExceptionController(IExceptionAppService exceptionAppService)
        {
            _exceptionAppService = exceptionAppService;
        }

        [HttpGet]
        public async Task<List<ExceptionsListOutput>> GetExceptions()
        {
            try
            {
                int balance = int.Parse("Ноль");
            }
            catch (Exception exc)
            {
                await _exceptionAppService.AddExceptionAsync(exc.Message);
            }

            return _exceptionAppService.GetExceptions();
        }
    }
}
