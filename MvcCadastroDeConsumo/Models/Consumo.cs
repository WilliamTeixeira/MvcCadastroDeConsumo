using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcCadastroDeConsumo.Models
{
    public class Consumo
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        private List<ItemConsumo> itensConsumo = new List<ItemConsumo>();

        public List<ItemConsumo> ItensConsumo
        {
            get { return itensConsumo; }
            set { itensConsumo = value; }
        }
        
        
    }
    
}