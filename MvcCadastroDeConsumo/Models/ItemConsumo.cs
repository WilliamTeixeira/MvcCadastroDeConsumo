﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcCadastroDeConsumo.Models
{
    public class ItemConsumo
    {
        public Produto Prod { get; set; }
        public int Quantidade { get; set; }

    }
}