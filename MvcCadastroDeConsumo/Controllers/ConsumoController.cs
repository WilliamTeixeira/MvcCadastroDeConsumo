using MvcCadastroDeConsumo.DAO;
using MvcCadastroDeConsumo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcCadastroDeConsumo.Controllers
{
    public class ConsumoController : Controller
    {
        #region Consumo
        // GET: Consumo
        public ActionResult IndexConsumo()
        {
            return View(new ConsumoDapperDAO().RetornarTodos());
        }

        // GET: Consumo/Details/5
        public ActionResult DetailsConsumo(int id)
        {
            return View(new ConsumoAdoDAO().RetornarPorId(id));

        }

        // GET: Consumo/Create
        public ActionResult CreateConsumo()
        {
            return View();
        }

        // POST: Consumo/Create
        [HttpPost]
        public ActionResult CreateConsumo(FormCollection collection)
        {
            try
            {
                Consumo obj = new Consumo();
                UpdateModel(obj);

                new ConsumoDapperDAO().Inserir(obj);

                obj.Id = new ConsumoDapperDAO().RetornarMaiorId();

                return View("DetailsConsumoCreateItens", new ConsumoAdoDAO().RetornarPorId(obj.Id));

               
            }
            catch
            {
                return View();
            }
        }

        // GET: Consumo/Edit/5
        public ActionResult EditConsumo(int id)
        {
            return View(new ConsumoAdoDAO().RetornarPorId(id));
        }

        // POST: Consumo/Edit/5
        [HttpPost]
        public ActionResult EditConsumo(int id, FormCollection collection)
        {
            try
            {
                Consumo obj = new Consumo();
                UpdateModel(obj);

                //Como neste momento não altera-se o item consumo, não há tratamentos para o estoque.
                new ConsumoDapperDAO().Alterar(obj);

                return RedirectToAction("IndexConsumo");
            }
            catch
            {
                return View();
            }
        }

        // GET: Consumo/Delete/5
        public ActionResult DeleteConsumo(int id)
        {
            return View(new ConsumoAdoDAO().RetornarPorId(id));
        }

        // POST: Consumo/Delete/5
        [HttpPost]
        public ActionResult DeleteConsumo(int id, FormCollection collection)
        {
            try
            {
                //Retornar o consumo
                Consumo obj = new ConsumoAdoDAO().RetornarPorId(id);

                //Verifica se existem itens de consumo, deleta se existir e atualiza o estoque atual de cada produto
                if (obj.ItensConsumo.Count > 0)
                    foreach (var objItemConsumo in obj.ItensConsumo)
                    {
                        //Exclui o item
                        new ItemConsumoAdoDAO().ExcluirItemConsumo(objItemConsumo);

                        //Atualiza produto após ter excluido o consumo
                        int totalConsumido = new ItemConsumoAdoDAO().RetornarTotalConsumidoProd(objItemConsumo.Prod.Id);
                        objItemConsumo.Prod.AtualizaEstoque(totalConsumido);
                        new ProdutoDAO().Alterar(objItemConsumo.Prod);
                    }
                
                //Exclui o consumo apos excluir os itens e atualizar o produto
                new ConsumoAdoDAO().Excluir(obj);


                return RedirectToAction("IndexConsumo");
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region ItensConsumo

        // GET: Consumo/Details/5
        public ActionResult DetailsConsumoCreateItens(int id)
        {
            return View(new ConsumoAdoDAO().RetornarPorId(id));

        }

        // GET: Consumo/CreateItemConsumo
        public ActionResult CreateItemConsumo(int idConsumo)
        {
            ViewBag.ListaDropProdutos = new SelectList(new ProdutoDAO().RetornarTodos(),"Id", "Descricao");

            return View(new ItemConsumo(idConsumo));
        }

        // POST: Consumo/CreateItemConsumo
        [HttpPost]
        public ActionResult CreateItemConsumo(FormCollection collection)
        {
            try
            {
                //Pega o id do item selecionado no dropdownlist: o indice "ListaDropProdutos" da collection é o item selecionado 
                int idProduto = Convert.ToInt32(collection["ListaDropProdutos"].ToString()); 
                
                //Retornar o produto
                Produto objProd = new ProdutoDAO().RetornarPorId(idProduto);

                //Cria e atualiza o Item de Consumo
                ItemConsumo objItemConsumo = new ItemConsumo();
                UpdateModel(objItemConsumo);
                objItemConsumo.Prod = objProd;
                
                /* Verifica se a chave IdConsumo + IdProduto já existe na relação de itens de consumo da base
                 * -> Se não existir, incluir o item de consumo. -> Se existir, apresenta uma mensagem de erro.
                 */
                if (new ItemConsumoAdoDAO().RetornarItemConsumo(objItemConsumo.IdConsumo,objItemConsumo.Prod.Id) == null)
                {
                    //Insere o item consumo na base
                    new ItemConsumoAdoDAO().InserirItemConsumo(objItemConsumo);

                    //Atualiza o produto após ter inserido o consumo
                    int totalConsumido = new ItemConsumoAdoDAO().RetornarTotalConsumidoProd(objProd.Id);
                    objProd.AtualizaEstoque(totalConsumido);
                    new ProdutoDAO().Alterar(objProd);

                    return View("DetailsConsumoCreateItens", new ConsumoAdoDAO().RetornarPorId(objItemConsumo.IdConsumo));
                }
                else
                {
                    TempData["msgErroItemKey"] = String.Format("Produto já utilizado para este consumo. Escolha outro Produto.");
                    ViewBag.ListaDropProdutos = new SelectList(new ProdutoDAO().RetornarTodos(), "Id", "Descricao", idProduto);
                    return View("CreateItemConsumo", objItemConsumo);
                }
                
            }
            catch
            {
                return View();
            }
        }

        // GET: Consumo/EditItemConsumo
        public ActionResult EditItemConsumo(int idConsumo, int idProduto)
        {
            ViewBag.ListaDropProdutos = new SelectList(new ProdutoDAO().RetornarTodos(), "Id", "Descricao",idProduto);

            return View(new ItemConsumoAdoDAO().RetornarItemConsumo(idConsumo, idProduto));
        }

        // POST: Consumo/EditItemConsumo
        [HttpPost]
        public ActionResult EditItemConsumo(FormCollection collection)
        {
            try
            {
                //Pega o id do item selecionado no dropdownlist:  O indice "ListaDropProdutos" da collection é o item selecionado 
                int idProduto = Convert.ToInt32(collection["ListaDropProdutos"].ToString());

                //Retornar o produto selecionado
                Produto objNovoProd = new ProdutoDAO().RetornarPorId(idProduto);
                
                //Cria e atualiza o itemConsumo a ser editado
                ItemConsumo objItemConsumo = new ItemConsumo();
                UpdateModel(objItemConsumo);

                if (objNovoProd.Id == objItemConsumo.Prod.Id)
                {
                    //Se o produto não foi alterado: Apenas atualizo a quantidade
                    new ItemConsumoAdoDAO().AlterarItemConsumo(objItemConsumo);

                    //Atualiza produto 
                    int totalConsumido = new ItemConsumoAdoDAO().RetornarTotalConsumidoProd(objItemConsumo.Prod.Id);
                    Produto objProd = new ProdutoDAO().RetornarPorId(objItemConsumo.Prod.Id);
                    objProd.AtualizaEstoque(totalConsumido);
                    new ProdutoDAO().Alterar(objProd);
                }
                else
                {
                    //Guardadno o produto antigo    
                    Produto prodAntigo = new ProdutoDAO().RetornarPorId(objItemConsumo.Prod.Id);

                    //Atualizando o item com o novo produto
                    objItemConsumo.Prod = objNovoProd;

                    //Atualizo o item consumo na base
                    new ItemConsumoAdoDAO().AlterarItemConsumo(prodAntigo, objItemConsumo);

                    //Atualizo o novo produto
                    int totalConsumidoNovoProd = new ItemConsumoAdoDAO().RetornarTotalConsumidoProd(objItemConsumo.Prod.Id);
                    objNovoProd.AtualizaEstoque(totalConsumidoNovoProd);
                    new ProdutoDAO().Alterar(objNovoProd);

                    //Atualizo o produto antigo
                    int totalConsumidoAntigoProd = new ItemConsumoAdoDAO().RetornarTotalConsumidoProd(prodAntigo.Id);
                    prodAntigo.AtualizaEstoque(totalConsumidoAntigoProd);
                    new ProdutoDAO().Alterar(prodAntigo);
                }

                return View("DetailsConsumoCreateItens", new ConsumoAdoDAO().RetornarPorId(objItemConsumo.IdConsumo));
            }
            catch
            {
                return View();
            }
        }

        // GET: Consumo/DeleteItemConsumo
        public ActionResult DeleteItemConsumo(int idConsumo, int idProduto)
        {
            return View(new ItemConsumoAdoDAO().RetornarItemConsumo(idConsumo, idProduto));
        }

        // POST: Consumo/DeleteItemConsumo
        [HttpPost]
        public ActionResult DeleteItemConsumo(int idConsumo, int idProduto, FormCollection collection)
        {
            try
            {
                //Cria obj Item Consumo
                ItemConsumo objItemConsumo = new ItemConsumoAdoDAO().RetornarItemConsumo(idConsumo, idProduto);

                //Excluir item consumo
                new ItemConsumoAdoDAO().ExcluirItemConsumo(objItemConsumo);

                //Atualiza produto após ter excluido o item de consumo
                int totalConsumido = new ItemConsumoAdoDAO().RetornarTotalConsumidoProd(idProduto);
                Produto objProd = new ProdutoDAO().RetornarPorId(idProduto);
                objProd.AtualizaEstoque(totalConsumido);
                new ProdutoDAO().Alterar(objProd);
                                
                return View("DetailsConsumoCreateItens", new ConsumoAdoDAO().RetornarPorId(objItemConsumo.IdConsumo));
            }
            catch
            {
                return View();
            }
        }

        #endregion
    }
}
