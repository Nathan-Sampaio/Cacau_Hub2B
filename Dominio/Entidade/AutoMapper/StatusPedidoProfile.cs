using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidade.AutoMapper
{
    public class StatusPedidoProfile : Profile
    {
        public StatusPedidoProfile()
        {
            CreateMap<StatusPedido.StatusPedido, StatusPedido.StatusPedidoCS>();
        }
    }
}
