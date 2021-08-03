using PAsia_Dashboard.Areas.Dashboard.Models.DAO;
using PAsia_Dashboard.Universal.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PAsia_Dashboard.Areas.Dashboard.Controllers
{
    [LogInChecker]
    public class HomeDashboardController : Controller
    {
        
        HomeDashboardDAO homeDashboardDao = new HomeDashboardDAO();
        public ActionResult frmHomeDashboard()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetDashboardData()
        {
            var data = homeDashboardDao.GetDashboardData();
            if (string.IsNullOrEmpty(homeDashboardDao.ExceptionReturn))
            {
                return Json(new { Data = data, Status = "Ok" });
            }
            return Json(new { ExceptionMessage=homeDashboardDao.ExceptionReturn });
        }
        public ActionResult GetBarChartData()
        {
            var data = homeDashboardDao.GetBarChartData();
            return Json(new { Data = data, Status= "Ok" });
        }
        //[HttpGet]
        //public ActionResult GetBarChartData()
        //{
        //    var data = homeDashboardDao.GetBarChartData();
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}
        
    }
}