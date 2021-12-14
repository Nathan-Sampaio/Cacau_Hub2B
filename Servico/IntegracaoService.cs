using AutoMapper;
using Dominio.Entidade.Pedido;
using Dominio.Interface.Servico;
using Dominio.Interface.Servico.Pedido;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Servico
{
    public class IntegracaoService : IIntegracaoService
    {
        private readonly IPedidoServico _pedidoService;
        private readonly IMapper _mapper;
        private readonly ILogger<IntegracaoService> _logger;   

        public IntegracaoService(IPedidoServico pedidoService, IMapper mapper, ILogger<IntegracaoService> logger)
        {
            _pedidoService = pedidoService;
            _mapper = mapper;
             _logger = logger;
        }

        public async Task IntegrarPedido(Webhook pedido)
        {
            _logger.LogInformation("O Pedido foi recebido");
            _logger.LogInformation("Numero do pedido " + pedido.IdOrder);
            _logger.LogInformation("Status " + pedido.OrderStatus);


            if (pedido.OrderStatus == "Approved")
            {
                var pedidoRecebido = await _pedidoService.BuscarPedidosHubPorOrderId(Convert.ToInt32(pedido.IdOrder));

                var pedidoOms = _mapper.Map<PedidoCS>(pedidoRecebido);

                pedidoOms.status = "Placed";
                pedidoOms.orderRef = $"HB-{ pedidoRecebido.Reference.Id.ToString()}";
                pedidoOms.Payload = JsonSerializer.Serialize(pedidoRecebido);
                pedidoOms.creation = DateTime.Now;

                await _pedidoService.EnviarPedidoParaOms(pedidoOms);
            }

            if (pedido.OrderStatus.ToLower() == "cancelled" || pedido.OrderStatus.ToLower() == "canceled")
            {
                var numeroPedido = await _pedidoService.BuscarPedidoPorReferenceIdOMS($"HB-{pedido.IdOrder}");

                await _pedidoService.EnviarSolicitacacaoCancelamentoOMS(numeroPedido);

                //await _pedidoService.CancelarPedidoOMS(numeroPedido);
            }
        }
    }
}
