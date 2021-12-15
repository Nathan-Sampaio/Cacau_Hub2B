using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidade.Pedido
{
    public class Invoice
    {
        public string Key { get; set; }
        public string MediaMimeType { get; set; }
        public string Tracking { get; set; }
        public string Xml { get; set; }
    }
}
