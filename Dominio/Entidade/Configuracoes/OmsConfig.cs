using Dominio.Entidade.Autenticacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidade.Configuracoes
{
    public class OmsConfig
    {
        public string RedisTokenKey { get; set; }
        public string BaseUrl { get; set; }

        public string LoginUrl { get; set; }

        public OmsAuthRequest Autenticacao { get; set; }
    }
}
