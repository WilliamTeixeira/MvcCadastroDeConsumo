using MvcCadastroDeConsumo.DAO;
using MvcCadastroDeConsumo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcCadastroDeConsumo.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Produto
        public ActionResult IndexProduto()
        {
            return View(new ProdutoDAO().RetornarTodos());
        }

        // GET: Produto/DetailsProduto/5
        public ActionResult DetailsProduto(int id)
        {
            return View(new ProdutoDAO().RetornarPorId(id));
        }

        // GET: Produto/Create
        public ActionResult CreateProduto()
        {
            return View();
        }

        // POST: Produto/Create
        [HttpPost]
        public ActionResult CreateProduto(FormCollection collection)
        {
            try
            {
                Produto novoObj = new Produto();
                UpdateModel(novoObj);

                new ProdutoDAO().Inserir(novoObj);

                return RedirectToAction("IndexProduto");
            }
            catch
            {
                return View();
            }
        }

        // GET: Produto/Edit/5
        public ActionResult EditProduto(int id)
        {
            return View(new ProdutoDAO().RetornarPorId(id));
        }

        // POST: Produto/Edit/5
        [HttpPost]
        public ActionResult EditProduto(int id, FormCollection collection)
        {
            try
            {
                Produto novoObj = new Produto();
                UpdateModel(novoObj);

                new ProdutoDAO().Alterar(novoObj);
                
                return RedirectToAction("IndexProduto");
            }
            catch
            {
                return View();
            }
        }

        // GET: Produto/Delete/5
        public ActionResult DeleteProduto(int id)
        {
            return View(new ProdutoDAO().RetornarPorId(id));
        }

        // POST: Produto/Delete/5
        [HttpPost]
        public ActionResult DeleteProduto(int id, FormCollection collection)
        {
            try
            {
                Produto novoObj = new ProdutoDAO().RetornarPorId(id);

                new ProdutoDAO().Excluir(novoObj);
                
                return RedirectToAction("IndexProduto");
            }
            catch
            {
                return View();
            }
        }
    }
}
