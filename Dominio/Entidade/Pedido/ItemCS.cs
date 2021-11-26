using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidade.Pedido
{
    public class ItemCS
    {
        public string productRef { get; set; }
        public string name { get; set; }
        public double quantity { get; set; }
        public double price { get; set; }
        public double subTotal { get; set; }
        public double discount { get; set; }
        public double total { get; set; }
        public string type { get; set; }
    }
}
