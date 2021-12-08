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

       
        /// <summary>
        /// Endpoint para receber os dados do pedido criado na Hub
        /// </summary>
        /// <param name="webHook">Parametro enviado pela Hub com os dados do Pedido</param>
        /// <returns></returns>
        [HttpPost("ReceberPedidosIntegracaoHub")]
        public async Task<IActionResult> ReceberPedidosIntegracaoHub(Webhook webHook)
        {
            try
            {
                if (webHook.IdTenant != 2032)
                {
                    return BadRequest("Requisição inválida");
                }

                if (webHook.IdOrder == 0 && webHook.IdTenant == 2032 && webHook.OrderStatus == "canceled")
                {
                    return Ok();
                }

                await _integracaoService.IntegrarPedido(webHook);

                return Ok();
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
