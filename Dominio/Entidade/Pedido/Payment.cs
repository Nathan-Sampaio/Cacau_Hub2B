using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entidade
{
    public class Payment
    {
        public string Method { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ApprovedDate { get; set; }
        public float TotalAmount { get; set; }
        public float TotalAmountPlusShipping { get; set; }
        public float TotalDiscount { get; set; }
        public int Installments { get; set; }
        public AddressHub Address { get; set; }
    }
}
