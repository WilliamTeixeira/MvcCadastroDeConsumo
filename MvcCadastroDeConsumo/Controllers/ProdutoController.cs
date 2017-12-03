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
                //Cria o produto e atualiza o estoque atual com o valor do estoque inicial
                Produto obj = new Produto();
                UpdateModel(obj);
                obj.EstoqueAtual = obj.EstoqueInicial;

                //Insere o produto na base
                if(new ProdutoDAO().RetornarPorId(obj.Id) == null)
                {
                    new ProdutoDAO().Inserir(obj);
                    return RedirectToAction("IndexProduto");
                }
                else
                {
                    TempData["msg"] = String.Format("Id indisponível. Escolha outro Id.");
                    return View("CreateProduto",obj);
                }

                
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
                // Cria o objeto e atualiza com os dados da model. O atributo EstoqueAtual não pode ser alterado pelo usuário
                Produto objProd = new Produto();
                UpdateModel(objProd);

                //Atualiza o objeto produto após ter alterado o estoque inicial
                int totalConsumido = new ItemConsumoAdoDAO().RetornarTotalConsumidoProd(objProd.Id);
                objProd.AtualizaEstoque(totalConsumido);
                
                //Altera o produto na base
                new ProdutoDAO().Alterar(objProd);
                
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
                //Cria um obj produto
                Produto objProd = new ProdutoDAO().RetornarPorId(id);

                //Verifica se existe algum consumo utilizando este produto.
                //Se existir apresenta uma mensagem. Senão, efetua a exclusão. 
                int cont = new ProdutoDAO().RetornarUtilizacao(objProd.Id);
                if (cont > 0)
                {
                    TempData["mensagemErro"] = String.Format("Alerta! " +
                                                             "Existem pelomenos {0} itens de consumo relacionados ao produto {1}. " +
                                                             "Exclua-os antes de excluir este produto.", cont, objProd.Descricao.ToUpper());
                    
                    return RedirectToAction("IndexProduto");
                }
                else
                {
                    new ProdutoDAO().Excluir(objProd);

                    return RedirectToAction("IndexProduto");
                }
    
            }
            catch
            {
                return View();
            }
        }
    }
}
