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
            CreateMap<Customer, CustomerCS>();
            CreateMap<Shipping, DeliveryCS>();
            CreateMap<Pedido.Pedido, PedidoCS>();

        }
    }
}
