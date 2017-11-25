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

        // GET: Home
        public ActionResult IndexProduto()
        {
            List<Produto> lista = new List<Produto>(); //teste
            lista.Add(new Produto { Id = 1, Descricao = "Mamao", Estoque = 10 });
            lista.Add(new Produto { Id = 1, Descricao = "Laranja", Estoque = 10 });
            lista.Add(new Produto { Id = 1, Descricao = "Abacate", Estoque = 10 });

            return View(lista);
        }

        // GET: Home
        public ActionResult IndexConsumo()
        {
            List<Consumo> lista = new List<Consumo>();
            lista.Add(new Consumo { Id = 1, Descricao = "Consumo 1", ItensConsumo = new List<ItemConsumo>() });
            lista.Add(new Consumo { Id = 1, Descricao = "Consumo 1", ItensConsumo = new List<ItemConsumo>() });
            lista.Add(new Consumo { Id = 1, Descricao = "Consumo 1", ItensConsumo = new List<ItemConsumo>() });

            return View(lista);
        }
    }
}