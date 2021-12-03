using Dominio.Entidade.Pedido;
using Dominio.Interface.Servico;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IIntegracaoService _integracaoService;
        public PedidoController(IIntegracaoService integracaoService)
        {
            _integracaoService = integracaoService;
        }
        public IActionResult Index()
        {
            return View();
        }

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
