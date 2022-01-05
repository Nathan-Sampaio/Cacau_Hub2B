using Dominio.Entidade.Autenticacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidade.Configuracoes
{
    public class HubConfig
    {
        public string RedisTokenKey { get; set; }
        public string BaseURL { get; set; }
        public string LoginURL { get; set; }
        public string Pedido_BuscarURL { get; set; }

        public int IdTenant { get; set; }

        public OAuthRequest Autenticacao { get; set; }
    }
}
