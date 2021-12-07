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
            CreateMap<Entidade.Pedido.Pedido, PedidoCS>()
                     //.ForMember(dest => dest.status, act => act.MapFrom(src => "Placed"))
                     .ForMember(dest => dest.merchantRef, act => act.MapFrom(src => "9009"))
                     .ForPath(dest => dest.Delivery.recipient.phoneNumber, act => act.MapFrom(src => src.Customer.Telephone))
                     .ForPath(dest => dest.Delivery.recipient.emailAddress, act => act.MapFrom(src => src.Customer.Email))
                     .ForPath(dest => dest.Delivery.recipient.mobileNumber, act => act.MapFrom(src => src.Customer.Telephone))
                     .ForMember(dest => dest.Delivery, act => act.MapFrom(src => src.Shipping))
                     .ForMember(dest => dest.Customer, act => act.MapFrom(src => src.Customer))
                     .ForMember(dest => dest.Payments, act => act.MapFrom(src => src.Payment))
                     .ForMember(dest => dest.items, act => act.MapFrom(src => src.Products));

            CreateMap<Shipping, DeliveryCS>().ConvertUsing<ShippingToDeliveryTypeConverter>();
            CreateMap<Customer, CustomerCS>().ConvertUsing<CustomerToCustomerCSTypeConverter>();
            CreateMap<Payment, List<PaymentCS>>().ConvertUsing<PaymentToPaymentCSTypeConverter>();
            CreateMap<List<Products>, List<ItemCS>>().ConvertUsing<ProductsToItemCSTypeConverter>();
        }
    }
}
