using AutoMapper;
using Dominio.Entidade.Pedido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidade.AutoMapper
{
    public class ShippingToDeliveryTypeConverter : ITypeConverter<Shipping, DeliveryCS>
    {
        public DeliveryCS Convert(Shipping source, DeliveryCS destination, ResolutionContext context)
        {
            return new DeliveryCS()
            {
                method = source.Provider,
                recipient = new RecipientCS()
                {
                    firstName = source.ReceiverName,
                },
                address = new AddressCS()
                {
                    Street = source.Address.Address,
                    City = source.Address.City,
                    State = source.Address.State,
                    Country = source.Address.Country,
                    PostalCode = source.Address.ZipCode,
                    StreetNumber = source.Address.Number,
                },
                expectedDate = source.EstimatedDeliveryDate
            };
        }
    }
}
