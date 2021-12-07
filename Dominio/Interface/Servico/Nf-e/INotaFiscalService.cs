using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interface.Servico.Nf_e
{
    public interface INotaFiscalService
    {
        Task<string> CadastraNfe();
        Task<string> BuscaXml(string numeroPedido);
    }
}
