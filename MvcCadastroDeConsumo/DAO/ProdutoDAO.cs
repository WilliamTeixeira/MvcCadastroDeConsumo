using Dapper;
using MvcCadastroDeConsumo.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcCadastroDeConsumo.DAO
{
    public class ProdutoDAO : Conexao
    {
        public void Inserir(Produto obj)
        {
            using (MySqlConnection conexao = new MySqlConnection(ConnString()))
            {
                conexao.Execute($"insert into produto (id, descricao, estoqueinicial, estoqueatual) values (@id, @descricao, @estoqueinicial, @estoqueatual)", obj);
            }
        }

        public void Alterar(Produto obj)
        {
            using (MySqlConnection conexao = new MySqlConnection(ConnString()))
            {
                conexao.Execute($"update produto set descricao=@descricao, estoqueinicial=@estoqueinicial, @estoqueatual=estoqueatual where id=@id", obj);
            }
        }

        public void Excluir(Produto obj)
        {
            using (MySqlConnection conexao = new MySqlConnection(ConnString()))
            {
                conexao.Execute($"delete from produto where id=@id", obj);
            }
        }

        public IEnumerable<Produto> RetornarTodos()
        {
            using (MySqlConnection conexao = new MySqlConnection(ConnString()))
            {
                return conexao.Query<Produto>($"select * from produto order by descricao");
            }
        }

        public Produto RetornarPorId(int id)
        {
            
            using (MySqlConnection conexao = new MySqlConnection(ConnString()))
            {
                return conexao.Query<Produto>($"SELECT * FROM produto WHERE id = @id;", new { id=@id }).FirstOrDefault();
            }
        }
    }
}