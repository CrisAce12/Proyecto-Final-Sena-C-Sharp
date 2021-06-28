using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {

            using (var db = new inventarioEntities1())
            {

                return View(db.cliente.ToList());

            }
                
        }

        public ActionResult Details(int id)
        {

            using (var db = new inventarioEntities1())
            {

                var findUser = db.cliente.Find(id);
                return View(findUser);

            }

        }

        public ActionResult Create()
        {

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(cliente cliente)
        {

            if (!ModelState.IsValid)
            {

                return View();

            }

            try
            {

                using (var db = new inventarioEntities1())
                {

                    db.cliente.Add(cliente);
                    db.SaveChanges();
                    return RedirectToAction("index");

                }

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "error " + ex);
                return View();

            }

        }

        public ActionResult Delete(int id)
        {

            try
            {

                using (var db = new inventarioEntities1())
                {

                    var findUser = db.cliente.Find(id);
                    db.cliente.Remove(findUser);
                    db.SaveChanges();
                    return RedirectToAction("index");

                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("","error "+ex);
                return View();
            }

        }

        public ActionResult Edit(int id)
        {

            try
            {

                using (var db = new inventarioEntities1())
                {

                    cliente findUser = db.cliente.Where(a => a.id == id).FirstOrDefault();
                    return View(findUser);

                }

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "error " + ex);
                return View();

            }           

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(cliente editCliente)
        {

            try
            {

                using (var db = new inventarioEntities1())
                {

                    cliente client = db.cliente.Find(editCliente.id);

                    client.nombre = editCliente.nombre;

                    client.documento = editCliente.documento;

                    client.email = editCliente.email;

                    db.SaveChanges();
                    return RedirectToAction("index");

                }

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("","error "+ex);
                return View();

            }

        }

        public ActionResult Reporte()
        {
            var db = new inventarioEntities1();

            var query = from tabCompra in db.compra
                        join tabCliente in db.cliente on tabCompra.id_cliente equals tabCliente.id
                        select new Reporte
                        {
                            nombreCliente = tabCliente.nombre,
                            emailCliente = tabCliente.email,
                            idCompra = tabCompra.id,
                            totalCompra = tabCompra.total,

                        };
            return View(query);

        }

    }
}