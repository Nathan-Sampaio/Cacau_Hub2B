using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidade.Pedido
{
    public class PedidosResponse
    {
        public int totalObjects { get; set; }
        public List<Pedido> response { get; set; }
    }
}
