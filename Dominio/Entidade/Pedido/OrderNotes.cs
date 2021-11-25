using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entidade
{
    public class OrderNotes
    {
        public int IdUser { get; set; }
        public DateTime CreateDate { get; set; }
        public string Message { get; set; }
    }
}
