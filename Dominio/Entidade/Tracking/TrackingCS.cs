using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidade.Tracking
{
    public class TrackingCS
    {
        public string Code { get; set; }
        public string Url { get; set; }
        public string ShippingDate { get; set; }
        public string ShippingProvider { get; set; }
        public string ShippingService { get; set; }
    }
}
