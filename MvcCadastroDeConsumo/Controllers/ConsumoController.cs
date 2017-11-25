using MvcCadastroDeConsumo.DAO;
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
        //public ActionResult Index()
        //{
        //    return View(new ConsumoDAO().RetornarTodos());
        //}

        public ActionResult IndexConsumo()
        {
            return View(new ConsumoDAO().RetornarTodos());
        }
        // GET: Consumo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Consumo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Consumo/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Consumo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Consumo/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Consumo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Consumo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
