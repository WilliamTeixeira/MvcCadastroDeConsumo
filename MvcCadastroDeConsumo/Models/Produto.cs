using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcCadastroDeConsumo.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int EstoqueInicial { get; set; } // Inicial
        public int EstoqueAtual { get; set; } // Atual

        //O valor totalConsumido deve ser retornado do total de consumo presente no banco para este produto
        public void AtualizaEstoque(int totalConsumido)
        {
            EstoqueAtual = EstoqueInicial - totalConsumido;
        }
    }
}