using AutoMapper;
using Dominio.Entidade.Pedido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidade.AutoMapper
{
    public class PaymentToPaymentCS : IValueResolver<Payment, PaymentCS, PaymentCS>
    {
        public PaymentCS Resolve(Payment source, PaymentCS destination, PaymentCS member, ResolutionContext context)
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
