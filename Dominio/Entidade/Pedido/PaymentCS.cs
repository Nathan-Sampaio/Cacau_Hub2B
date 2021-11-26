using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidade.Pedido
{
    public class PaymentCS
    {
        public string method { get; set; }
        public double amount { get; set; }
        public string transactionRef { get; set; }
        public string transactionAccount { get; set; }
        public int installments { get; set; }
        public string creditCardBrand { get; set; }
        public string creditCardBrandId { get; set; }
        public string acquirer { get; set; }
        public string cnpj { get; set; }
    }
}
