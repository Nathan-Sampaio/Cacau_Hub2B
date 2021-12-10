using Dominio.Entidade.StatusPedido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interface.Servico.Status
{
    public interface IStatuService
    {
        Task<string> AdicionaStatusDiferentes(StatusPedidoCS status);
    }
}
