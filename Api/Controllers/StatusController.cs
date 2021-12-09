using Dominio.Entidade.StatusPedido;
using Dominio.Entidade.Tracking;
using Dominio.Interface.Servico.Tracking;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class StatusController : Controller
    {
        private readonly ITrackingService _trackingService;
        public StatusController(ITrackingService trackingService)
        {
            _trackingService = trackingService;
        }

        [HttpPost("RecebeRequisicaoOms")]
        public async Task<IActionResult> RecebeRequisicaoOms(StatusPedidoCS statusPedidoCS)
        {
            var retorno = _trackingService.AdicionaStatus(statusPedidoCS);

            return Ok();
        }
    }
}
