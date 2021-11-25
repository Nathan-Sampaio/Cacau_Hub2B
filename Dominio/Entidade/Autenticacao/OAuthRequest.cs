using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidade.Autenticacao
{
    public class OAuthRequest
    {
        public string client_id { get; set; }
        public string grant_type { get; set;}
        public string client_secret { get; set;}
        public string refresh_token { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string scope { get; set; }
    }
}
