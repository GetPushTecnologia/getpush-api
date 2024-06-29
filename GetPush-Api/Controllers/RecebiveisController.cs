using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace GetPush_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecebiveisController : ControllerBase
    {
        public RecebiveisController()
        {
        }

        [HttpPost]
        [Route(nameof(GetRecebiveis))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Contas a Receber", Description = "Buscar lista de recebiveis")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRecebiveis()
        {
            return Ok(new { message = "Lista de recebíveis" });
        }


        [HttpPost]
        [Route(nameof(InsertRecebiveis))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Contas a Receberr", Description = "Inserir dados de recebiveis")]
        [AllowAnonymous]
        public async Task<IActionResult> InsertRecebiveis()
        {
            return Ok(new { message = "Inserir dados de recebíveis" });
        }
    }
}
