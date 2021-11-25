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
            await _integracaoService.IntegrarPedido(webHook.IdOrder);

            return Ok();
        }
    }
}
