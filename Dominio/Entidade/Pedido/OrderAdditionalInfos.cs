using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entidade
{
    public class OrderAdditionalInfos
    {
        public DateTime TransferDate { get; set; }
        public float TransferAmount { get; set; }
        public string Fee { get; set; }
    }
}
