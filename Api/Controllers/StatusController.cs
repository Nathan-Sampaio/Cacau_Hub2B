using Dominio.Entidade.StatusPedido;
using Dominio.Entidade.Tracking;
using Dominio.Interface.Servico.Tracking;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class StatusController : Controller
    {
        private readonly ITrackingService _trackingService;
        private readonly ILogger<StatusController> _logger;

        public StatusController(ITrackingService trackingService, ILogger<StatusController> logger)
        {
            _trackingService = trackingService;
            _logger = logger;
        }

        [HttpPost("RecebeRequisicaoOms")]
        public async Task<IActionResult> RecebeRequisicaoOms([FromBody]StatusPedidoCS statusPedidoCS)
        {
            _logger.LogInformation("RecebeRequisicaoOms");
            _logger.LogInformation($"Objeto recebido : {JsonSerializer.Serialize(statusPedidoCS)}");

            await _trackingService.AdicionaStatus(statusPedidoCS);

            _logger.LogInformation($"O método Status.RecebeRequisicaoOms terminou!");

            return Ok();
        }
    }
}
