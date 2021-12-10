using Dominio.Entidade.Pedido;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interface.Servico.Pedido
{
    public interface IPedidoServico
    {
        Task<PedidosResponse> BuscarPedidosHub(FiltroPedido filtro);
        Task<Entidade.Pedido.Pedido> BuscarPedidosHubPorOrderId(int orderId);
        Task EnviarPedidoParaOms(PedidoCS pedido);
        Task<int> BuscarPedidoPorReferenceIdOMS(string referenceId);
        Task EnviarSolicitacacaoCancelamentoOMS(int IdPedido);
        Task CancelarPedidoOMS(int IdPedido);
    }
}
