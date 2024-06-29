using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace GetPush_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DividendosController : ControllerBase
    {
        public DividendosController()
        {
        }

        [HttpPost]
        [Route(nameof(GetDividendos))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Contas a pagar", Description = "Buscar lista de dividendos")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDividendos()
        {
            return Ok(new { message = "Lista de Dividendos" });
        }


        [HttpPost]
        [Route(nameof(InsertDividendos))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Contas a Receberr", Description = "Inserir dados de dividendos")]
        [AllowAnonymous]
        public async Task<IActionResult> InsertDividendos()
        {
            return Ok(new { message = "Inserir dados de dividendos" });
        }
    }
}
