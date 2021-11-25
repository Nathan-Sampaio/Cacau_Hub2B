using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interface.Servico.Pedido
{
    public interface ILoginService
    {
        Task<string> RecuperarTokenAcessoHub();
        Task<string> RecuperarTokenAcessoOms();
    }
}
