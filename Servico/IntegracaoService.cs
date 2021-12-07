using AutoMapper;
using Dominio.Entidade.Pedido;
using Dominio.Interface.Servico;
using Dominio.Interface.Servico.Pedido;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Servico
{
    public class IntegracaoService : IIntegracaoService
    {
        private readonly IPedidoServico _pedidoService;
        private readonly IMapper _mapper;

        public IntegracaoService(IPedidoServico pedidoService, IMapper mapper)
        {
            _pedidoService = pedidoService;
            _mapper = mapper;
        }

        public async Task IntegrarPedido(Webhook pedido)
        {
            var pedidoRecebido = await _pedidoService.BuscarPedidosHubPorOrderId(pedido.IdOrder);

            var pedidoOms = _mapper.Map<PedidoCS>(pedidoRecebido);

            pedidoOms.status = "Placed";
            pedidoOms.orderRef = $"HB-{ pedidoRecebido.Reference.Id.ToString()}";
            pedidoOms.Payload = JsonSerializer.Serialize(pedidoRecebido);
            pedidoOms.creation = DateTime.Now;

            await _pedidoService.EnviarPedidoParaOms(pedidoOms);
        }
    }
}
