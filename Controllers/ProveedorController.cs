using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;
using System.IO;

namespace Proyecto.Controllers
{
    public class ProveedorController : Controller
    {
        // GET: Proveedor
        public ActionResult Index()
        {

            using(var db = new inventarioEntities1())
            {

                return View(db.proveedor.ToList());

            }

        }

        public ActionResult Details(int id)
        {

            using (var db = new inventarioEntities1())
            {

                var findUser = db.proveedor.Find(id);
                return View(findUser);

            }

        }

        public ActionResult Create()
        {

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(proveedor proveedor)
        {

            if (!ModelState.IsValid)
            {

                return View();

            }

            try
            {

                using (var db = new inventarioEntities1())
                {

                    db.proveedor.Add(proveedor);
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

                    var findUser = db.proveedor.Find(id);
                    db.proveedor.Remove(findUser);
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

        public ActionResult Edit(int id)
        {

            try
            {

                using (var db = new inventarioEntities1())
                {

                    proveedor findUser = db.proveedor.Where(a => a.id == id).FirstOrDefault();
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
        public ActionResult Edit(proveedor editProveedor)
        {

            try
            {

                using (var db = new inventarioEntities1())
                {

                    proveedor provider = db.proveedor.Find(editProveedor.id);

                    provider.nombre = editProveedor.nombre;

                    provider.direccion = editProveedor.direccion;

                    provider.telefono = editProveedor.telefono;

                    provider.nombre_contacto = editProveedor.nombre_contacto;

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

        public ActionResult uploadCSV()
        {

            return View();

        }

        [HttpPost]
        public ActionResult returnCSV(HttpPostedFile fileForm)
        {

            string filePath = string.Empty;

            if(fileForm != null)
            {

                string path = Server.MapPath("~/Uploads/");

                if (Directory.Exists(path))
                {

                    Directory.CreateDirectory(path);

                }

                filePath = path + Path.GetFileName(fileForm.FileName);
                string extension = Path.GetExtension(fileForm.FileName);

                fileForm.SaveAs(filePath);

                string csvData = System.IO.File.ReadAllText(filePath);

                foreach(string row in csvData.Split('\n'))
                {

                    if (!string.IsNullOrEmpty(row))
                    {

                        var newProveedor = new proveedor
                        {

                            nombre = row.Split(';')[0],
                            nombre_contacto = row.Split(';')[1],
                            direccion = row.Split(';')[2],
                            telefono = row.Split(';')[3]

                        };

                        using (var db = new inventarioEntities1())
                        {

                            db.proveedor.Add(newProveedor);
                            db.SaveChanges();

                        }

                    }

                }

            }

            return View("Index");

        }

    }
}