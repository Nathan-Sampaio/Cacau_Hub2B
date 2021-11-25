using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entidade
{
    public class Products
    {
        public int IdProduct { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public float Quantity { get; set; }
        public string Unity { get; set; }
        public float Price { get; set; }
        public float ShippingCost { get; set; }
        public float Discount { get; set; }
        public string Type { get; set; }
    }
}
