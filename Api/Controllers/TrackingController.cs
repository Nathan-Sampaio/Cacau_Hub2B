using Dominio.Entidade.Tracking;
using Dominio.Interface.Servico.Tracking;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    public class TrackingController : Controller
    {
        private readonly ITrackingService _trackingService;
        public TrackingController(ITrackingService trackingService)
        {
            _trackingService = trackingService;
        }

        [HttpPost("RecebeRequisicaoOms")]
        public async Task<IActionResult> RecebeRequisicaoOms(TrackingCS tracking)
        {
            var retorno = _trackingService.AdicionarTracking(tracking);

            return Ok();
        }
    }
}
