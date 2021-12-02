using AutoMapper;
using Dominio.Entidade.Pedido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidade.AutoMapper
{
    public class PedidoToPedidoCSTypeConverter : ITypeConverter<Pedido.Pedido, PedidoCS>
    {
        public PedidoCS Convert(Pedido.Pedido source, PedidoCS destination, ResolutionContext context)
        {
            return new PedidoCS()
            {
               deliveryFee = source.Shipping.Price,
            };
        }
    }
}
