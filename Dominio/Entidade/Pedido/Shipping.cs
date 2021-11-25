using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entidade
{
    public class Shipping
    {
        public DateTime ShippingDate { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
        public string Responsible { get; set; }
        public string Provider { get; set; }
        public string Service { get; set; }
        public float Price { get; set; }
        public string ReceiverName { get; set; }
        public Address Endereco { get; set; }
    }
}
