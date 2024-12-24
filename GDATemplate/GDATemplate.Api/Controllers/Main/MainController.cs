using System.Linq;
using System.Net;
using GDATemplate.Domain;
using GDATemplate.Domain.Model.Retorno;
using Microsoft.AspNetCore.Mvc;

namespace GDATemplate.Api.Controllers.Main
{
    public abstract class MainController : ControllerBase
    {
        /// <summary>
        /// Retorno padrão para o tipo BP_Retorno<T>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="retorno"></param>
        /// <returns></returns>
        public IActionResult CustomResponse<T>(BP_Retorno<T> retorno)
        {
            if (retorno.Erros.Any(x => x.Id == HttpStatusCode.NotFound))
                return NotFound(retorno);

            if (retorno.Erros.Count > 0)
                return BadRequest(retorno);

            return Ok(retorno);
        }

        /// <summary>
        /// Retorno padrão para o tipo BP_RetornoObjeto<T>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="retorno"></param>
        /// <returns></returns>
        public IActionResult CustomResponse<T>(BP_RetornoObjeto<T> retorno)
        {
            if (retorno.Erros.Any(x => x.Id == HttpStatusCode.NotFound))
                return NotFound(retorno);

            if (retorno.Erros.Count > 0)
                return BadRequest(retorno);

            return Ok(retorno);
        }
    }
}
