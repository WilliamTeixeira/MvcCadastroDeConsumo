using Dapper;
using MvcCadastroDeConsumo.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcCadastroDeConsumo.DAO
{
    public class ConsumoDAO
    {
        public void Inserir(Consumo obj)
        {
            using (MySqlConnection conexao = new MySqlConnection(new Conexao().ConnString()))
            {
                conexao.Execute($"insert into consumo (descricao) values (@descricao)", obj);
            }
        }

        public void Alterar(Consumo obj)
        {
            using (MySqlConnection conexao = new MySqlConnection(new Conexao().ConnString()))
            {
                conexao.Execute($"update consumo set descricao=@descricao where id=@id", obj);
            }
        }

        public void Excluir(Consumo obj)
        {
            using (MySqlConnection conexao = new MySqlConnection(new Conexao().ConnString()))
            {
                conexao.Execute($"delete from Consumo where id=@id", obj);
            }
        }

        public IEnumerable<Consumo> RetornarTodos()
        {
            using (MySqlConnection conexao = new MySqlConnection(new Conexao().ConnString()))
            {
                return conexao.Query<Consumo>($"select * from consumo order by descricao");
            }
        }

        public Consumo RetornarPorId(int id)
        {
            using (MySqlConnection conexao = new MySqlConnection(new Conexao().ConnString()))
            {
                return conexao.Query<Consumo>($"select * from consumo where id=@id ", new { id = @id }).FirstOrDefault();
            }
        }
    }
}