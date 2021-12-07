using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidade.Nf_e
{
    public class NotaFiscal
    {
        public string Xml { get; set; }
        public string Key { get; set; }
        public string Number { get; set; }
        public string Cfop { get; set; }
        public string Series { get; set; }
        public string TotalAmount { get; set; }
        public string IssueDate { get; set; }
        public string XmlReference { get; set; }
        public string Packages { get; set; }
    }
}
