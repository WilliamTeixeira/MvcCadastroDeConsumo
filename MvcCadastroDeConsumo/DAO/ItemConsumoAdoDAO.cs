using MvcCadastroDeConsumo.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MvcCadastroDeConsumo.DAO
{
    public class ItemConsumoAdoDAO : BaseDAO
    {
        public void InserirItemConsumo(ItemConsumo obj)
        {
            string sql = $"insert into itensconsumo(idconsumo, idproduto, quantidade) values(@idconsumo, @idproduto, @quantidade);";

            MySqlCommand cmd = new MySqlCommand(sql);

            cmd.Parameters.Add(new MySqlParameter("@idconsumo", obj.IdConsumo));
            cmd.Parameters.Add(new MySqlParameter("@idproduto", obj.Prod.Id));
            cmd.Parameters.Add(new MySqlParameter("@quantidade", obj.Quantidade));

            ExecutarComando(cmd);
        }

        public void AlterarItemConsumo(ItemConsumo obj)
        {
            string sql = $"update itensconsumo set quantidade = @quantidade where idconsumo=@idconsumo and idproduto=@idproduto;";
            MySqlCommand cmd = new MySqlCommand(sql);

            cmd.Parameters.Add(new MySqlParameter("@idconsumo", obj.IdConsumo));
            cmd.Parameters.Add(new MySqlParameter("@idproduto", obj.Prod.Id));
            cmd.Parameters.Add(new MySqlParameter("@quantidade", obj.Quantidade));

            ExecutarComando(cmd);
        }

        public void ExcluirItemConsumo(ItemConsumo obj)
        {
            string sql = $"delete from itensconsumo where idconsumo=@idconsumo and idproduto=@idproduto;";

            MySqlCommand cmd = new MySqlCommand(sql);

            cmd.Parameters.Add(new MySqlParameter("@idconsumo", obj.IdConsumo));
            cmd.Parameters.Add(new MySqlParameter("@idproduto", obj.Prod.Id));

            ExecutarComando(cmd);
        }

        public ItemConsumo RetornarItemConsumo(int idConsumo, int idProduto)
        {

            string sql = "select * from itensconsumo where idconsumo = @idconsumo and idproduto = @idproduto;";

            MySqlCommand cmd = new MySqlCommand(sql);

            cmd.Parameters.Add(new MySqlParameter("@idconsumo", idConsumo));
            cmd.Parameters.Add(new MySqlParameter("@idproduto", idProduto));

            DataSet ds = RetornarDataSet(cmd);

            if (ds.Tables[0].Rows.Count == 0)
                return null;
            else
                return RetornarItemConsumo(ds.Tables[0].Rows[0]);
             
        }

        public int RetornarTotalConsumidoProd(int idProduto)
        {

            string sql = "SELECT sum(quantidade) as total FROM itensconsumo where idproduto @idproduto;";

            MySqlCommand cmd = new MySqlCommand(sql);

            cmd.Parameters.Add(new MySqlParameter("@idproduto", idProduto));

            DataSet ds = RetornarDataSet(cmd);

            if (ds.Tables[0].Rows.Count == 0)
                return 0;
            else
                return TotalConsumo(ds.Tables[0].Rows[0]);

        }

        private int TotalConsumo(DataRow dr)
        {
            return Convert.ToInt32(dr["total"]);
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