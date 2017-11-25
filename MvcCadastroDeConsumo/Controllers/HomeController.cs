using MvcCadastroDeConsumo.DAO;
using MvcCadastroDeConsumo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcCadastroDeConsumo.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        #region IndexProduto
        // GET: Home
        public ActionResult IndexProduto()
        {
            return View(new ProdutoDAO().RetornarTodos());
        }

        #endregion

        #region IndexConsumo
        // GET: Home
        public ActionResult IndexConsumo()
        {
            return View(new ConsumoDAO().RetornarTodos());
        }
        #endregion
    }
}