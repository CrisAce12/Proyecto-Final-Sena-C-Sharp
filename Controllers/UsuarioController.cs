﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;
using System.Web.Security;
using System.IO;

namespace Proyecto.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        [Authorize]
        public ActionResult Index()
        {

            using (var db = new inventarioEntities1()) 
            {

                return View(db.usuario.ToList());

            }
                
        }

        public ActionResult Create()
        {

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(usuario usuario)
        {

            if (!ModelState.IsValid)
                return View();

            try
            {

                using (var db = new inventarioEntities1())
                {

                    usuario.password = UsuarioController.HashSHA1(usuario.password);
                    db.usuario.Add(usuario);
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

        public static string HashSHA1(string value)
        {

            var sha1 = System.Security.Cryptography.SHA1.Create();
            var inputBytes = Encoding.ASCII.GetBytes(value);
            var hash = sha1.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();

        }

        public ActionResult Edit(int id)
        {
            try
            {

                using (var db = new inventarioEntities1())
                {

                    usuario findUser = db.usuario.Where(a => a.id == id).FirstOrDefault();
                    return View(findUser);

                }

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("","error"+ex);
                return View();

            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(usuario editUser)
        {

            try
            {

                using (var db = new inventarioEntities1())
                {

                    usuario user = db.usuario.Find(editUser.id);

                    user.nombre = editUser.nombre;

                    user.apellido = editUser.apellido;

                    user.email = editUser.email;

                    user.fecha_nacimiento = editUser.fecha_nacimiento;

                    user.password = editUser.password;

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

        public ActionResult Delete(int id)
        {

            try
            {

                using (var db = new inventarioEntities1())
                {

                    var findUser = db.usuario.Find(id);
                    db.usuario.Remove(findUser);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("","error"+ex);
                return RedirectToAction("Index");

            }

        }

        public ActionResult Details(int id)
        {

            using (var db = new inventarioEntities1())
            {

                var finduser = db.usuario.Find(id);
                return View(finduser);

            }

        }

        public ActionResult Login(string message = "")
        {

            ViewBag.Message = message;
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string user, string password)
        {

            string passwordEncrip = UsuarioController.HashSHA1(password);
            using (var db = new inventarioEntities1())
            {

                var userLogin = db.usuario.FirstOrDefault(e => e.email == user && e.password == passwordEncrip);
                if (userLogin != null)
                {

                    FormsAuthentication.SetAuthCookie(userLogin.email, true);
                    Session["User"] = userLogin;
                    return RedirectToAction("Index");

                }
                else
                {

                    return Login("Verifique sus datos");

                }
            }

        }

        [Authorize]
        public ActionResult CloseSession()
        {

            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");

        }

        public ActionResult uploadCSV()
        {

            return View();

        }

        [HttpPost]
        public ActionResult uploadCSV(HttpPostedFileBase fileForm)
        {

            string filePath = string.Empty;

            if (fileForm != null)
            {

                string path = Server.MapPath("~/Uploads/");

                if (!Directory.Exists(path))
                {

                    Directory.CreateDirectory(path);

                }

                filePath = path + Path.GetFileName(fileForm.FileName);
                string extension = Path.GetExtension(fileForm.FileName);

                fileForm.SaveAs(filePath);

                string csvData = System.IO.File.ReadAllText(filePath);

                foreach (string row in csvData.Split('\n'))
                {

                    if (!string.IsNullOrEmpty(row))
                    {

                        var newUsuario = new usuario
                        {

                            nombre = row.Split(';')[0],
                            apellido = row.Split(';')[1],
                            fecha_nacimiento = DateTime.Parse(row.Split(';')[2]),
                            email = row.Split(';')[3],
                            password = row.Split(';')[4],

                        };

                        using (var db = new inventarioEntities1())
                        {

                            db.usuario.Add(newUsuario);
                            db.SaveChanges();

                        }

                    }

                }

                return RedirectToAction("Index");

            }

            return View("");

        }

    }
}