using PAsia_Dashboard.Areas.Reports.Models.DAO;
using PAsia_Dashboard.Universal.Gateway;
using System.Web.Mvc;

namespace PAsia_Dashboard.Areas.Reports.Controllers
{
    [LogInChecker]
    public class TargetSalesController : Controller
    {

        TargetSalesDAO TargetSalesDAO = new TargetSalesDAO();
        // GET: Reports/TargetSales
        public ActionResult frmTargetSales()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetTargetSalesValue(string fromDate, string toDate)
        {
            var listData = TargetSalesDAO.GetTargetSalesValue(fromDate, toDate);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }

    }
}