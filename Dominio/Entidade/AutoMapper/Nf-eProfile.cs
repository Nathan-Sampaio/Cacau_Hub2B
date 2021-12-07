using AutoMapper;
using Dominio.Entidade.Nf_e;

namespace Dominio.Entidade.AutoMapper
{
    public class Nf_eProfile : Profile
    {
        public Nf_eProfile()
        {
            CreateMap<NotaFiscal, NotaFiscalCS>();
        }
    }
}
