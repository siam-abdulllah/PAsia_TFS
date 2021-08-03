using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PAsia_Dashboard.Areas.FSM.Models.DAL.DAO;
using PAsia_Dashboard.Universal.Gateway;

namespace PAsia_Dashboard.Areas.FSM.Controllers
{
    [LogInChecker]
    public class ReportNonSenderMPOController : Controller
    {
        readonly ReportNonSenderMPODAO _reportNonSenderMPODao=new ReportNonSenderMPODAO();
        public ActionResult frmReportNonSenderMPO()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetNonSenderMPOData(string depotCode, string zoneCode, string regionCode, string areaCode, string territoryCode, string fromDate, string toDate)
        {
            var listData = _reportNonSenderMPODao.GetNonSenderMPOData(depotCode, zoneCode, regionCode, areaCode, territoryCode, fromDate, toDate);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }
    }
}