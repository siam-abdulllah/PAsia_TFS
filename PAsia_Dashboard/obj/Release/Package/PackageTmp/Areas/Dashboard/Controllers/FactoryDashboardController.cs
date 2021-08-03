using PAsia_Dashboard.Areas.Dashboard.Models.DAO;
using PAsia_Dashboard.Universal.Gateway;
using System.Web.Mvc;

namespace PAsia_Dashboard.Areas.Dashboard.Controllers
{
    [LogInChecker]
    public class FactoryDashboardController : Controller
    {
        FactoryDashboardDAO factoryDashboardDao = new FactoryDashboardDAO();
        public ActionResult frmFactoryDashboard()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetDashboardData()
        {
            var data = factoryDashboardDao.GetDashboardData();
            if (string.IsNullOrEmpty(factoryDashboardDao.ExceptionReturn))
            {
                return Json(new { Data = data, Status = "Ok" });
            }
            return Json(new { ExceptionMessage = factoryDashboardDao.ExceptionReturn });
        }
    }
}