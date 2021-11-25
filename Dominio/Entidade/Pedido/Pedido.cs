using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entidade.Pedido
{
    public class Pedido
    {
        public Reference Reference { get; set; }
        public Shipping Shipping { get; set; }
        public Payment Payment { get; set; }
        public Status Status { get; set; }
        public Customer Customer { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CanceledDate { get; set; }
        public List<Products> Products { get; set; }
        public List<OrderNotes> OrderNotes { get; set; }
        public List<OrderAdditionalInfos> OrderAdditionalInfos { get; set; }
    }
}
