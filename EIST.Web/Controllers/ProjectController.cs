using EIST.Web.Models;
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
            
                model.AddCompanyProject();
                return RedirectToAction("Index");
        
           
        }
        public JsonResult GetCompanyProjectDetailsById(int id)
        {
            var model = new ProjectModel(id);
            return Json(new { Id = model.Id, Name = model.Name, Description = model.Description, CompanyId = model.CompanyId,PmId =model.PmId, SuperVisorId = model.SuperVisorId });
        }
        
        public ActionResult Details(int id)
        {
            var model = new ProjectModel().GetCompanyProjectById(id);
            return PartialView("_Details", model);
        }
        public ActionResult Edit(int id)
        {
            return View(new ProjectModel(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectModel model)
        {
                model.EditCompanyProject();
                return RedirectToAction("Index");      
        }

        public ActionResult Delete(int id)
        {
            new ProjectModel().DeleteCompanyProject(id);
            return Json(new { meg = "success" });

        }
        public JsonResult IsCompanyProjectNameExist(string Name, string InitialName)
        {
            bool isNotExist = new ProjectModel().IsCompanyProjectNameExist(Name, InitialName);
            return Json(isNotExist, JsonRequestBehavior.AllowGet);
        }
    }
}