using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCCrud.Models;
using MVCCrud.Models.ViewModels;
using RestSharp;

namespace MVCCrud.Controllers
{
    public class CrudController : Controller
    {
        //// GET: Crud
        public ActionResult index()
        {
            List<ListCrudViewModel> lst;
            using (PruebaEntities db = new PruebaEntities())
            {
                lst = (from d in db.crud
                       select new ListCrudViewModel
                       {
                           Id = d.Id,
                           Nombre = d.Nombre,
                           Correo = d.Correo
                       }).ToList();

                //lst = db.crud.select(c => new listcrudviewmodel() { correo = c.correo, id = c.id, nombre = c.nombre }).tolist();
            }

            return View(lst);
        }

        public ActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Nuevo(CrudViewModel model)
        {
  
            if (ModelState.IsValid)
            {
                using (PruebaEntities db = new PruebaEntities())
                {
                    var oCrud = new crud();
                    oCrud.Id = model.Id;
                    oCrud.Nombre = model.Nombre;
                    oCrud.Correo = model.Correo;
                    oCrud.Fecha_nacimiento = model.Fecha_Nacimiento;

                    db.crud.Add(oCrud);
                    db.SaveChanges();
                }
                return Redirect("~/Crud/");
            }

            return View(model);
        }

        public ActionResult Editar(int Id)
        {
            CrudViewModel model = new CrudViewModel();
            using (PruebaEntities db = new PruebaEntities())
            {
                var oCrud = db.crud.Find(Id);
                model.Nombre = oCrud.Nombre;
                model.Correo = oCrud.Correo;
                model.Fecha_Nacimiento = oCrud.Fecha_nacimiento;
                model.Id = oCrud.Id;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(CrudViewModel model)
        {

            if (ModelState.IsValid)
            {
                using (PruebaEntities db = new PruebaEntities())
                {
                    var oCrud = db.crud.Find(model.Id);
                    oCrud.Id = model.Id;
                    oCrud.Nombre = model.Nombre;
                    oCrud.Correo = model.Correo;
                    oCrud.Fecha_nacimiento = model.Fecha_Nacimiento;

                    db.Entry(oCrud).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return Redirect("~/Crud/");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Eliminar(int Id)
        {
            using (PruebaEntities db = new PruebaEntities())
            {
                var oCrud = db.crud.Find(Id);
                db.crud.Remove(oCrud);
                db.SaveChanges();
            }

            return Redirect("~/Crud/");
        }
    }
}