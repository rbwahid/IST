using IST.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IST.Web.Controllers
{
    [Authorize]
    [Roles("Company_Project_Configuration,Global_SupAdmin")]
    public class CompanyProjectController : Controller
    {
        // GET: Product
        public ActionResult Index(CompanyProjectModel model)
        {
            return View((new CompanyProjectModel().GetAllCompanyProjects()));
        }
        public ActionResult Add()
        {
            return View(new CompanyProjectModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(CompanyProjectModel model)
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
            var model = new CompanyProjectModel(id);
            return Json(new { Id = model.Id, Name = model.Name, Description = model.Description, CompanyId = model.CompanyId });
        }
        public ActionResult Details(int id)
        {
            return View(new CompanyProjectModel(id));
        }
        public ActionResult Edit(int id)
        {
            return View(new CompanyProjectModel(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CompanyProjectModel model)
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
            new CompanyProjectModel().DeleteCompanyProject(id);
            return Json(new { meg = "success" });

        }
        public JsonResult IsCompanyProjectNameExist(string Name, string initialName)
        {
            bool isNotExist = new CompanyProjectModel().IsCompanyProjectNameExist(Name, initialName);
            return Json(isNotExist, JsonRequestBehavior.AllowGet);
        }
    }
}