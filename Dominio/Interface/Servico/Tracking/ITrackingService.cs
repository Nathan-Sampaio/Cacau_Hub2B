using Dominio.Entidade.Tracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interface.Servico.Tracking
{
    public interface ITrackingService
    {
        string AdicionarTracking(TrackingCS tracking);
    }
}
