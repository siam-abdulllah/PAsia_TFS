using PAsia_Dashboard.Areas.Reports.Models.DAO;
using PAsia_Dashboard.Universal.Gateway;
using System.Web.Mvc;

namespace PAsia_Dashboard.Areas.Reports.Controllers
{
    [LogInChecker]
    public class StockProdSalesController : Controller
    {
        StockProdSalesDAO stockProdSalesDao = new StockProdSalesDAO();
        public ActionResult frmStockProdSales()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetStockProdSalesValue(string fromDate, string toDate)
        {
            var listData = stockProdSalesDao.GetStockProdSalesValue(fromDate, toDate);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }
    }
}