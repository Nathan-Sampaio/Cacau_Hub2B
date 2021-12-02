using AutoMapper;
using Dominio.Entidade.Pedido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidade.AutoMapper
{
    public class ProductsToItemCSTypeConverter : ITypeConverter<List<Products>, List<ItemCS>>
    {
        public List<ItemCS> Convert(List<Products> source, List<ItemCS> destination, ResolutionContext context)
        {
            var itensPedido = new List<ItemCS>();

            foreach (var product in source)
            {
                itensPedido.Add(new ItemCS()
                {
                    name = product.Name,
                    price = product.Price,
                    discount = product.Discount,
                    quantity = product.Quantity,
                    total = product.Price,
                    productRef = "1000642"
                });
            }

            return itensPedido;
        }
    }
}
