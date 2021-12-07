using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dominio.Entidade.Tracking;

namespace Dominio.Entidade.AutoMapper
{
    public class TrackingProfile : Profile
    {
        public TrackingProfile()
        {
            CreateMap<Tracking.Tracking, TrackingCS>();
        }
    }
}
