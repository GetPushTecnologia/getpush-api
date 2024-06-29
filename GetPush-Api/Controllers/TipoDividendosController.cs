using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace GetPush_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDividendosController : ControllerBase
    {
        public TipoDividendosController()
        {
        }

        [HttpPost]
        [Route(nameof(GetTipoDividendos))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Contas a Paga", Description = "Buscar lista tipo de dividendos")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTipoDividendos()
        {
            return Ok(new { message = "Lista tipo de Dividendos" });
        }


        [HttpPost]
        [Route(nameof(InsertTipoDividendos))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Contas a Pagar", Description = "Inserir dados tipo de dividendos")]
        [AllowAnonymous]
        public async Task<IActionResult> InsertTipoDividendos()
        {
            return Ok(new { message = "Inserir dados tipo de dividendos" });
        }
    }
}
