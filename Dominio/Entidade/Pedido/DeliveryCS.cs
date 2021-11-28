using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidade.Pedido
{
    public class DeliveryCS
    {
        public string method { get; set; }
        public RecipientCS recipient { get; set; }
        public AddressCS address { get; set; }
        public DateTime expectedDate { get; set; }
    }
}
