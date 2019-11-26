﻿using EIST.Entities;
using EIST.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EIST.Web.Controllers
{
    [Authorize]
    
    public class IssueLabelController : Controller
    {
        [Roles("Global_SupAdmin,IssueLabel_Configuration")]
        // GET: Issuelabel
        public ActionResult Index(IssueLabel model)
        {
            return View((new IssueLabelModel().GetAllIssueLabel()));
        }
        [Roles("Global_SupAdmin,IssueLabel_Configuration")]
        public ActionResult Add()
        {
            return View(new IssueLabelModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(IssueLabelModel model)
        {
            if (ModelState.IsValid)
            {
                model.AddIssueLabel();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public JsonResult GetIssueLabelDetailsById(int id)
        {
            var model = new IssueLabelModel(id);
            return Json(new { Id = model.Id, LabelTitle = model.LabelTitle, ColorCode = model.ColorCode });
        }
        public ActionResult Details(int id)
        {
            return View(new IssueLabelModel(id));
        }
        [Roles("Global_SupAdmin,IssueLabel_Configuration")]
        public ActionResult Edit(int id)
        {
            return View(new IssueLabelModel(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IssueLabelModel model)
        {
            if (ModelState.IsValid)
            {
                model.EditIssueLabel();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [Roles("Global_SupAdmin,IssueLabel_Configuration")]
        public ActionResult Delete(int id)
        {
            new IssueLabelModel().DeleteIssueLabel(id);
            return Json(new { meg = "success" });

        }
        public JsonResult IsIssueLabelExist(string LabelTitle, string InitialLabelTitle)
        {
            bool isNotExist = new IssueLabelModel().IsIssueLabelExist(LabelTitle, InitialLabelTitle);
            return Json(isNotExist, JsonRequestBehavior.AllowGet);
        }
    }
}