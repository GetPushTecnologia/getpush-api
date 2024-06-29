using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace GetPush_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoRecebiveisController : ControllerBase
    {
        public TipoRecebiveisController()
        {
        }

        [HttpPost]
        [Route(nameof(GetTipoRecebiveis))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Contas a Receber", Description = "Buscar lista de tipo de recebiveis")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTipoRecebiveis()
        {
            return Ok(new { message = "Lista tipo de recebíveis" });
        }


        [HttpPost]
        [Route(nameof(InsertTipoRecebiveis))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Contas a Receber", Description = "Inserir dados tipo de recebiveis")]
        [AllowAnonymous]
        public async Task<IActionResult> InsertTipoRecebiveis()
        {
            return Ok(new { message = "Inserir dados tipos de recebíveis" });
        }
    }
}
