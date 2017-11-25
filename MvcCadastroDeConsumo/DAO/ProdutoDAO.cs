using Dapper;
using MvcCadastroDeConsumo.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcCadastroDeConsumo.DAO
{
    public class ProdutoDAO
    {
        public void Inserir(Produto obj)
        {
            using (MySqlConnection conexao = new MySqlConnection(new Conexao().ConnString()))
            {
                obj.Id = 1 + conexao.Query<int>("select max(id) from produto").FirstOrDefault();
                
                conexao.Execute($"insert into produto (id, descricao) values (@id, @descricao)", obj);
            }
        }

        public void Alterar(Produto obj)
        {
            using (MySqlConnection conexao = new MySqlConnection(new Conexao().ConnString()))
            {
                conexao.Execute($"update produto set descricao=@descricao where id=@id", obj);
            }
        }

        public void Excluir(Produto obj)
        {
            using (MySqlConnection conexao = new MySqlConnection(new Conexao().ConnString()))
            {
                conexao.Execute($"delete from produto where id=@id", obj);
            }
        }

        public IEnumerable<Produto> RetornarTodos()
        {
            using (MySqlConnection conexao = new MySqlConnection(new Conexao().ConnString()))
            {
                return conexao.Query<Produto>($"select * from produto order by descricao");
            }
        }

        public Produto RetornarPorId(int id)
        {
            using (MySqlConnection conexao = new MySqlConnection(new Conexao().ConnString()))
            {
                return conexao.Query<Produto>($"select * from produto where id=@id ").FirstOrDefault();
            }
        }
    }
}