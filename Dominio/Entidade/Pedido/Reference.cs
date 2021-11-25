using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entidade
{
    public class Reference
    {
        public int IdTenant { get; set; }
        public string Store { get; set; }
        public int Id { get; set; }
        public string Virtual { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public System System { get; set; }
    }

    public class System
    {
        public string Source { get; set; }
        public string Destination { get; set; }
    }
}
