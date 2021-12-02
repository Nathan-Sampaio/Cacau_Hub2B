using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidade.Pedido
{
    public class PedidoCS
    {
        public int id { get; set; }
        public string orderRef { get; set; }
        public DateTime creation { get; set; }
        public string merchantRef { get; set; }
        public string carrierRef { get; set; }
        public string status { get; set; }
        public double merchantKmAwayHaversine { get; set; }
        public double merchantKmAwayRoute { get; set; }
        public CustomerCS Customer { get; set; }
        public DeliveryCS Delivery { get; set; }
        public List<ItemCS> items { get; set; }
        public double deliveryFee { get; set; }
        public double discount { get; set; }
        public double subTotal { get; set; }
        public double total { get; set; }
        public double tax { get; set; }
        public string notes { get; set; }
        public string paymentStatus { get; set; }
        public List<PaymentCS> Payments { get; set; }
        public string elapsedTimeSinceLastStatusChange { get; set; }
        public string elapsedTimeSinceIntegration { get; set; }
        public string elapsedTimeBetweenStatusChange { get; set; }
        public string criticality { get; set; }
        public string currentStatusCriticality { get; set; }
        public List<object> coupons { get; set; }
        public List<string> campaigns { get; set; }
        public bool isGift { get; set; }
        public bool isReseller { get; set; }
        public string Payload { get; set; }
        public string Client { get; set; }

        public ClientDetails ClientDetails { get; set; }
    }
}
