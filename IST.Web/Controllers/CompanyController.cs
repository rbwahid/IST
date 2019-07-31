using IST.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IST.Web.Controllers
{
    [Authorize]
    [Roles("Global_SupAdmin,Configuration")]
    public class CompanyController : Controller
    {
        
        // GET: Product
        public ActionResult Index(CompanyModel model)
        {
            return View((new CompanyModel().GetAllCompanies()));           
        }
        public ActionResult Add()
        {
            return View(new CompanyModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(CompanyModel model)
        {
            if (ModelState.IsValid)
            {
                model.AddCompany();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public JsonResult GetCompanyDetailsById(int id)
        {
            var model = new CompanyModel(id);
            return Json(new { Id = model.Id, Name = model.Name, Address = model.Address, Phone = model.Phone, FaxNo = model.FaxNo, Email=model.Email });
        }
        public ActionResult Details(int id)
        {
            return View(new CompanyModel(id));
        }
        public ActionResult Edit(int id)
        {
            return View(new CompanyModel(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CompanyModel model)
        {
            if (ModelState.IsValid)
            {
                model.EditCompany();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            new CompanyModel().DeleteCompany(id);
            return Json(new { meg = "success" });

        }
        public JsonResult IsCompanyNameExist(string Name, string initialName)
        {
            bool isNotExist = new CompanyModel().IsCompanyNameExist(Name, initialName);
            return Json(isNotExist, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult Import()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Import(HttpPostedFileBase ProductExcel)
        //{
        //    string executeMsg = "";
        //    if (ModelState.IsValid)
        //    {
        //        //try
        //        //{
        //        if (ProductExcel != null)
        //        {
        //            new ProductModel().ImportProduct(ProductExcel, null, null, null, null);
        //            return RedirectToAction("Index", "Product");
        //        }
        //        //}
        //        //catch (Exception e)
        //        //{
        //        //    Console.WriteLine(e.Message);
        //        //    executeMsg = e.Message;
        //        //}
        //        //ViewBag.ExcuteMsg = executeMsg;
        //    }
        //    return View();
        //}



    }
}