using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidade.Autenticacao
{
    public class OmsAuthRequest
    {
        public string clientid { get; set; }
        public string clientsecret { get; set;}
        public string userName { get; set; }
    }
}
