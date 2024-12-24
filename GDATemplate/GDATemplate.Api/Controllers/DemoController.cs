using System;
using System.Linq;
using System.Threading.Tasks;
using GDATemplate.Api.Controllers.Main;
using GDATemplate.Api.Models;
using GDATemplate.Application.Interfaces;
using GDATemplate.Application.Services.Entities;
using GDATemplate.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProdespGDA.Commom.Utilities;

namespace GDATemplate.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class DemoController : MainController
    {
        private readonly IGenericService _generic;
        private DemoService _demoService;

        private readonly ILogger _logger;

        public DemoController(IGenericService generic, ILoggerFactory logger, DemoService demoService)
        {
            _generic = generic;
            _logger = logger.CreateLogger<DemoController>();
            _demoService = demoService;

        }

        /// <summary>
        /// Executar método Example1
        /// </summary>
        [HttpPost("example")]
        public IActionResult PostExample()
        {
            try
            {

                _generic.Example1();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao executar método GetExample1");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Executar método Example2
        /// </summary>
        [HttpGet("example")]
        public async Task<IActionResult> GetExample()
        {
            try
            {
                var retorno = await _generic.Example2();

                if (retorno.Any())
                    return Ok(retorno);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao executar método GetExample2");
                return BadRequest(ex.Message);
            }
        }
    }
}
