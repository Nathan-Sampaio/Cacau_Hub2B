using AutoMapper;
using Dominio.Entidade.Pedido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidade.AutoMapper
{
    public class PaymentToPaymentCSTypeConverter : ITypeConverter<Payment, PaymentCS>
    {
        public PaymentCS Convert(Payment source, PaymentCS destination, ResolutionContext context)
        {
            return new PaymentCS()
            {
                method = source.Method,
                amount = source.TotalAmountPlusShipping,
                installments = source.Installments,
            };
        }
    }
}
