﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidade.Pedido
{
    public class Webhook
    {
        public int IdTenant { get; set; }
        public int IdOrder { get; set; }
        public string OrderStatus { get; set; }
    }
}
