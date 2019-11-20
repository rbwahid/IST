﻿using EIST.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EIST.Web.Controllers
{
    [Authorize]
    [Roles("Company_Project_Configuration,Global_SupAdmin")]
    public class ProjectController : Controller
    {
        // GET: Product
        public ActionResult Index(ProjectModel model)
        {
            return View((new ProjectModel().GetAllCompanyProjects()));
        }
        public ActionResult Add()
        {
            return View(new ProjectModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ProjectModel model)
        {
            if (ModelState.IsValid)
            {
                model.AddCompanyProject();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public JsonResult GetCompanyProjectDetailsById(int id)
        {
            var model = new ProjectModel(id);
            return Json(new { Id = model.Id, Name = model.Name, Description = model.Description, CompanyId = model.CompanyId });
        }
        public ActionResult Details(int id)
        {
            return View(new ProjectModel(id));
        }
        public ActionResult Edit(int id)
        {
            return View(new ProjectModel(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectModel model)
        {
            if (ModelState.IsValid)
            {
                model.EditCompanyProject();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            new ProjectModel().DeleteCompanyProject(id);
            return Json(new { meg = "success" });

        }
        public JsonResult IsCompanyProjectNameExist(string Name, string initialName)
        {
            bool isNotExist = new ProjectModel().IsCompanyProjectNameExist(Name, initialName);
            return Json(isNotExist, JsonRequestBehavior.AllowGet);
        }
    }
}