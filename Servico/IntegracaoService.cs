using Dominio.Interface.Servico;
using Dominio.Interface.Servico.Pedido;
using System;
using System.Threading.Tasks;

namespace Servico
{
    public class IntegracaoService : IIntegracaoService
    {
        private readonly IPedidoServico _pedidoService;

        public IntegracaoService(IPedidoServico pedidoService)
        {
            _pedidoService = pedidoService;
        }

        public async Task IntegrarPedido(int pedido)
        {
            var pedidoRecebido = await _pedidoService.BuscarPedidosHubPorOrderId(pedido);

            //ToDo: Montar objeto para o OMS com base no Objeto da Hub e enviar pedido para o OMS
        }
    }
}
