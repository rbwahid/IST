using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EIST.Web.Models;

namespace EIST.Web.Controllers
{
    public class ErrorHandlerController : Controller
    {
        public ActionResult UnAuthenticError()
        {
            return View();
        }
        
        public ActionResult ExceptionError(ExceptionLogger logger)
        {
            return View(logger);
        }
    }
}