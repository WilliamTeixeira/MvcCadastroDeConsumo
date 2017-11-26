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

                return RedirectToAction("IndexConsumo");
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
            return View(new ConsumoDapperDAO().RetornarPorId(id));
        }

        // POST: Consumo/Delete/5
        [HttpPost]
        public ActionResult DeleteConsumo(int id, FormCollection collection)
        {
            try
            {
                Consumo obj = new ConsumoDapperDAO().RetornarPorId(id);

                new ConsumoDapperDAO().Excluir(obj);

                return RedirectToAction("IndexConsumo");
            }
            catch
            {
                return View();
            }
        }
    }
}
