using AutoMapper;
using Dominio.Entidade.Pedido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidade.AutoMapper
{
    public class PedidoProfile : Profile
    {
        public PedidoProfile()
        {
            CreateMap<Customer, CustomerCS>().ConvertUsing<CustomerToCustomerCSTypeConverter>();
            CreateMap<Shipping, DeliveryCS>().ConvertUsing<ShippingToDeliveryTypeConverter>();
            CreateMap<Payment, PaymentCS>().ConvertUsing<PaymentToPaymentCSTypeConverter>();
            CreateMap<Pedido.Pedido, PedidoCS>().ConvertUsing<PedidoToPedidoCSTypeConverter>();
        }
    }
}
