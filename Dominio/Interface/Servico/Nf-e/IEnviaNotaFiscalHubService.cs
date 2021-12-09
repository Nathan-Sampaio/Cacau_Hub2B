using Dominio.Entidade.Nf_e;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interface.Servico.Nf_e
{
    public interface IEnviaNotaFiscalHubService
    {
        Task<string> EnviaNotaHub(NotaFiscalCS notaFiscalCS, string numeroPedido);
    }
}
