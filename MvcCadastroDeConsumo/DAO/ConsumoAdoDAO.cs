using MvcCadastroDeConsumo.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MvcCadastroDeConsumo.DAO
{
    public class ConsumoAdoDAO : BaseDAO
    {
        public void Inserir(Consumo obj)
        {
            string sql = $"insert into consumo(descricao) values(@descricao);";

            MySqlCommand cmd = new MySqlCommand(sql);

            cmd.Parameters.Add(new MySqlParameter("@descricao", obj.Descricao));

            ExecutarComando(cmd);
        }

        public void Alterar(Consumo obj)
        {
            string sql = $"update consumo set descricao=@descricao where id=@id;";

            MySqlCommand cmd = new MySqlCommand(sql);

            cmd.Parameters.Add(new MySqlParameter("@descricao", obj.Descricao));
            cmd.Parameters.Add(new MySqlParameter("@id", obj.Id));

            ExecutarComando(cmd);
        }

        public void Excluir(Consumo obj)
        {
            string sql = $"delete from Consumo where id=@id;";

            MySqlCommand cmd = new MySqlCommand(sql);

            cmd.Parameters.Add(new MySqlParameter("@id", obj.Id));

            ExecutarComando(cmd);
        }

        public void RetornarMaiorId()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Consumo> RetornarTodos()
        {
            List<Consumo> lista = new List<Consumo>();

            string sql = $"select * from consumo order by descricao;";

            MySqlCommand cmd = new MySqlCommand(sql);

            DataSet ds = RetornarDataSet(cmd);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                foreach (DataRow item in ds.Tables[0].Rows)
                    lista.Add(RetornarConsumoSemItens(item));

            return lista;
        }

        // Retorna um obj completo, inclusive a lista de itens de consumo
        public Consumo RetornarPorId(int id)
        {
            Consumo obj = new Consumo();

            string sql = $"select* from consumo where id = @id;";
            
            MySqlCommand cmd = new MySqlCommand(sql);
            cmd.Parameters.Add(new MySqlParameter("@id", id));
            
            DataSet ds = RetornarDataSet(cmd);

            if (ds.Tables[0].Rows.Count == 0)
                return null;
            else
            {
                obj = RetornarConsumoSemItens(ds.Tables[0].Rows[0]);
                obj.ItensConsumo = RetornarItensConsumo(id);
            }

            return obj;
        }
        #region ItensConsumo
        public List<ItemConsumo> RetornarItensConsumo(int idConsumo)
        {
            List<ItemConsumo> objs = new List<ItemConsumo>();

            string sql = "select itensconsumo.idproduto, itensconsumo.quantidade, itensconsumo.idconsumo " +
                            "from itensconsumo inner join produto on itensconsumo.idproduto = produto.id " +
                            "where itensconsumo.idconsumo = @id " +
                            "order by produto.descricao; ";


            MySqlCommand cmd = new MySqlCommand(sql);

            cmd.Parameters.Add(new MySqlParameter("@id", idConsumo ));

            DataSet ds = RetornarDataSet(cmd);
            
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                foreach (DataRow item in ds.Tables[0].Rows)
                    objs.Add(RetornarItemConsumo(item));
            
            return objs;
        }


        
        #endregion
        private Consumo RetornarConsumoSemItens(DataRow dr)
        {
            Consumo obj = new Consumo();
            obj.Id = Convert.ToInt32(dr["id"]);
            obj.Descricao = dr["descricao"].ToString();

            return obj;
        }

        private ItemConsumo RetornarItemConsumo(DataRow dr)
        {
            ItemConsumo obj = new ItemConsumo();

            obj.IdConsumo = Convert.ToInt32(dr["idconsumo"]);
            obj.Quantidade = Convert.ToInt32(dr["quantidade"]);

            obj.Prod = new ProdutoDAO().RetornarPorId(Convert.ToInt32(dr["idproduto"]));

            return obj;
        }


    }
}