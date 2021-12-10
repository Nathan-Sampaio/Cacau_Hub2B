using Dominio.Entidade.Pedido;
using Dominio.Interface.Servico;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    [Route("api/[controller]")]
    public class PedidoController : Controller
    {
        private readonly IIntegracaoService _integracaoService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="integracaoService"></param>
        public PedidoController(IIntegracaoService integracaoService)
        {
            _integracaoService = integracaoService;
        }

        [HttpPost("ReceberPedidosIntegracaoHub")]
        public async Task<IActionResult> ReceberPedidosIntegracaoHub([FromBody]Webhook webHook)
        {
            try
            {
                if (webHook.IdTenant != 2032)
                {
                    return BadRequest("Requisição inválida");
                }

                if (webHook.IdOrder == 0 && webHook.IdTenant == 2032 && webHook.OrderStatus == "Cancelled")
                {
                    return Ok();
                }

                if (webHook.OrderStatus != "Created")
                {
                    await _integracaoService.IntegrarPedido(webHook);
                }

                return Ok();
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
