using AutoMapper;
using Dominio.Entidade.Pedido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidade.AutoMapper
{
    internal class ShippingDeliverySolver : IValueResolver<Shipping, DeliveryCS, DeliveryCS>
    {
        public DeliveryCS Resolve(Shipping source, DeliveryCS destination, DeliveryCS member, ResolutionContext context)
        {
            return new DeliveryCS();
        }
    }
}
