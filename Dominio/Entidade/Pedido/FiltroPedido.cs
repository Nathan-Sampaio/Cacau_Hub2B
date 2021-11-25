using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidade.Pedido
{
    public class FiltroPedido
    {
        // Documento cliente
        public string Client { get; set; }
        public string purchaseFrom { get; set; }
        public string purchaseTo { get; set; }
    }
}
