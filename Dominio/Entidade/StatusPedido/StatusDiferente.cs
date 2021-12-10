using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidade.StatusPedido
{
    public class StatusDiferente
    {
        public string Status { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string Message { get; set; }
    }
}
