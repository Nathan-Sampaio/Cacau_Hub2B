using Dominio.Entidade.StatusPedido;
using Dominio.Entidade.Tracking;
using Dominio.Interface.Servico.Nf_e;
using Dominio.Interface.Servico.Status;
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
        private readonly IStatuService _statuService;
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
                if (statusPedidoCS.Status.ToLower() == "invoiced")
                {
                    _notaFiscalService.BuscaXml(statusPedidoCS.IdPedido);
                }

                else if(statusPedidoCS.Status == "Dispatched")
                {
                    //Pegar o dados de tracking e mandar para a Hub
                }

                else
                {
                    //enviar qualquer status diferentes direto no endpoint de put do status
                    _statuService.AdicionaStatusDiferentes(statusPedidoCS);
                }

                return null;
            }
        }
    }
}
