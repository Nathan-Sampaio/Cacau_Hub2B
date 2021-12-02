using AutoMapper;
using Dominio.Entidade.Pedido;
using System.Collections.Generic;

namespace Dominio.Entidade.AutoMapper
{
    public class PaymentToPaymentCSTypeConverter : ITypeConverter<Payment, List<PaymentCS>>
    {
        public List<PaymentCS> Convert(Payment source, List<PaymentCS> destination, ResolutionContext context)
        {
            return new List<PaymentCS>()
            {
                new PaymentCS(){
                    method = "CreditCard",
                    amount = source.TotalAmountPlusShipping,
                    installments = source.Installments,
                }
            };
        }
    }
}
