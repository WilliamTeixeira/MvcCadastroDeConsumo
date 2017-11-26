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
            return View(new ConsumoDAO().RetornarTodos());
        }

        // GET: Consumo/Details/5
        public ActionResult DetailsConsumo(int id)
        {
            return View(new ConsumoDAO().RetornarPorId(id));
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

                new ConsumoDAO().Inserir(obj);

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
            return View(new ConsumoDAO().RetornarPorId(id));
        }

        // POST: Consumo/Edit/5
        [HttpPost]
        public ActionResult EditConsumo(int id, FormCollection collection)
        {
            try
            {
                Consumo obj = new Consumo();
                UpdateModel(obj);

                new ConsumoDAO().Alterar(obj);

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
            return View(new ConsumoDAO().RetornarPorId(id));
        }

        // POST: Consumo/Delete/5
        [HttpPost]
        public ActionResult DeleteConsumo(int id, FormCollection collection)
        {
            try
            {
                Consumo obj = new ConsumoDAO().RetornarPorId(id);

                new ConsumoDAO().Excluir(obj);

                return RedirectToAction("IndexConsumo");
            }
            catch
            {
                return View();
            }
        }
    }
}
