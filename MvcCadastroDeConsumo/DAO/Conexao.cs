using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace MvcCadastroDeConsumo.DAO
{
    public class Conexao
    {
        private string Server = "localhost";
        private string Database = "bdcadastrodeconsumo";
        private string Uid = "root";
        private string Password = "160400";


        public string ConnString()
        {
            return $"SERVER={Server}; DATABASE={Database}; UID={Uid}; PASSWORD={Password};";
        }

    }
}