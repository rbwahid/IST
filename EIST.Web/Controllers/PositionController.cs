using EIST.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EIST.Web.Controllers
{
    [Authorize]
    [Roles("Global_SupAdmin,Position_Configuration")]
    public class PositionController : Controller
    {
        // GET: Product
        public ActionResult Index(PositionModel model)
        {
            return View((new PositionModel().GetAllPositions()));
        }
        public ActionResult Add()
        {
            return View(new PositionModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(PositionModel model)
        {
            if (ModelState.IsValid)
            {
                model.AddPosition();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public JsonResult GetPositionDetailsById(int id)
        {
            var model = new PositionModel(id);
            return Json(new { Id = model.Id, PositionName = model.PositionName, ShortName = model.ShortName });
        }
        public ActionResult Details(int id)
        {
            return View(new PositionModel(id));
        }
        public ActionResult Edit(int id)
        {
            return View(new PositionModel(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PositionModel model)
        {
            if (ModelState.IsValid)
            {
                model.EditPosition();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            new PositionModel().DeletePosition(id);
            return Json(new { meg = "success" });

        }
        public JsonResult IsPositionNameExist(string PositionName, string InitialName)
        {
            bool isNotExist = new PositionModel().IsPositionNameExist(PositionName, InitialName);
            return Json(isNotExist, JsonRequestBehavior.AllowGet);
        }
    }
}