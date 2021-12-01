using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidade.Autenticacao
{
    public class OmsAuthResponse
    {
        public string Token { get; set;}
        public DateTime Expires { get; set; }
    }
}
