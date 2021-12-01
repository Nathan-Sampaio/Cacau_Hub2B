using AutoMapper;
using Dominio.Entidade.Pedido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dominio.Entidade.AutoMapper
{
    public class CustomerToCustomerCSTypeConverter : ITypeConverter<Customer, CustomerCS>
	{
        public CustomerCS Convert(Customer source, CustomerCS destination, ResolutionContext context)
        {
			return new CustomerCS()
			{
				document = source.DocumentNumber,
				firstName = ObterPrimeiroNome(source.Name),
				lastName = ObterUltimoNome(source.Name),
				phoneNumber = source.Telephone,
				mobileNumber = source.MobileNumber,
				emailAddress = source.Email
			};
		}

		private string ObterPrimeiroNome(string name)
        {
			return Regex.Split(name, @"/(?<=^[^ ]+) /")[0];
		}

		private string ObterUltimoNome(string name)
		{
			var resultadoRegex = Regex.Split(name, @"/(?<=^[^ ]+) /");
			return resultadoRegex[resultadoRegex.Length - 1];
		}
	}
}
