using Dominio.Entidade.StatusPedido;
using Dominio.Entidade.Tracking;
using Dominio.Interface.Servico.Nf_e;
using Dominio.Interface.Servico.Tracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Services
{
    public class TrackingService : ITrackingService
    {
        private readonly INotaFiscalService _notaFiscalService;
        public TrackingService(INotaFiscalService notaFiscalService)
        {
            _notaFiscalService = notaFiscalService;
        }
        public string AdicionarTracking(TrackingCS tracking)
        {
            if (tracking.Code == "Faturado")
            {
                _notaFiscalService.BuscaXml(tracking.ShippingProvider);
            }

            return null;
        }

        public string AdicionaStatus(StatusPedidoCS statusPedidoCS)
        {
            {
                if (statusPedidoCS.Status == "Invoiced")
                {
                    _notaFiscalService.BuscaXml(statusPedidoCS.IdPedido);
                }

                return null;
            }
        }
    }
}
