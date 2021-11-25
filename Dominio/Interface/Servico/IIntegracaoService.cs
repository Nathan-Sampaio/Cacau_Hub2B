using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interface.Servico
{
    public interface IIntegracaoService
    {
        Task IntegrarPedido(int pedido);
    }
}
