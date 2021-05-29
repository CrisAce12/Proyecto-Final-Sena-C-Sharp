using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    public class Producto_CompraController : Controller
    {
        public ActionResult Index()
        {

            using (var db = new inventarioEntities1())
            {

                return View(db.producto_compra.ToList());

            }

        }

        public static string NombreProducto(int? idProducto)
        {

            using (var db = new inventarioEntities1())
            {

                return db.producto.Find(idProducto).nombre;

            }

        }

        public ActionResult ListarProductos(int? idProducto)
        {

            using (var db = new inventarioEntities1())
            {

                return PartialView(db.producto.ToList());

            }

        }

        public ActionResult Create()
        {

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(producto_compra producto_Compra)
        {

            if (!ModelState.IsValid)
                return View();

            try
            {

                using (var db = new inventarioEntities1())
                {

                    db.producto_compra.Add(producto_Compra);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "error" + ex);
                return View();

            }

        }

        public ActionResult Edit(int id)
        {

            using (var db = new inventarioEntities1())
            {

                producto_compra producto_compraEdit = db.producto_compra.Where(a => a.id == id).FirstOrDefault();
                return View(producto_compraEdit);

            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(producto_compra producto_compraEdit)
        {

            try
            {

                using (var db = new inventarioEntities1())
                {

                    var oldProducto_Compra = db.producto_compra.Find(producto_compraEdit.id);
                    oldProducto_Compra.id_compra = producto_compraEdit.id_compra;
                    oldProducto_Compra.id_producto = producto_compraEdit.id_producto;
                    oldProducto_Compra.cantidad = producto_compraEdit.cantidad;
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "Error" + ex);
                return View();

            }

        }

        public ActionResult Delete(int id)
        {

            try
            {

                using (var db = new inventarioEntities1())
                {

                    producto_compra producto_Compra = db.producto_compra.Find(id);
                    db.producto_compra.Remove(producto_Compra);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "" + ex);
                return View();

            }

        }

        public ActionResult Details(int id)
        {

            using (var db = new inventarioEntities1())
            {

                return View(db.producto_compra.Find(id));

            }

        }
    }
}