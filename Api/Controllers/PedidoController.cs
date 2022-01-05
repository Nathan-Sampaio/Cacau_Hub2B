using Dominio.Entidade.Configuracoes;
using Dominio.Entidade.Pedido;
using Dominio.Interface.Servico;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;
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
        private readonly ILogger<PedidoController> _logger;
        private readonly HubConfig config;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="integracaoService"></param>
        /// <param name="logger"></param>
        /// <param name=""></param>
        public PedidoController(IIntegracaoService integracaoService, ILogger<PedidoController> logger, IOptions<HubConfig> hubconfig)
        {
            _integracaoService = integracaoService;
            _logger = logger;
            config = hubconfig.Value;
        }

        [HttpPost("ReceberPedidosIntegracaoHub")]
        public async Task<IActionResult> ReceberPedidosIntegracaoHub([FromBody]Webhook webHook)
        {
            try
            {
                _logger.LogInformation("ReceberPedidosIntegracaoHub");
                _logger.LogInformation("Recebida integração : ", JsonSerializer.Serialize(webHook));

                if (webHook.IdTenant != config.IdTenant)
                {
                    return BadRequest("Requisição inválida");
                }

                if (webHook.IdOrder == 0 && webHook.IdTenant == config.IdTenant && webHook.OrderStatus.ToLower() == "cancelled" || webHook.OrderStatus.ToLower() == "canceled")
                {
                    return Ok();
                }

                if (webHook.OrderStatus != "Created")
                {
                    await _integracaoService.IntegrarPedido(webHook);
                }

                return Ok();
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Erro no endpoint ReceberPedidosIntegracaoHub ", ex);
                throw;
            }
        }
    }
}
