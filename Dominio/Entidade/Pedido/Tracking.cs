using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidade.Pedido
{
    public class Tracking
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Number { get; set; }
        public string Client { get; set; }
        public string ClientCnpj { get; set; }
        public string DateSend { get; set; }
    }
}
