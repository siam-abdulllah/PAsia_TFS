using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PAsia_Dashboard.Areas.FSM.Models.DAL;
using PAsia_Dashboard.Universal.Gateway;

namespace PAsia_Dashboard.Areas.FSM.Controllers
{
    //[LogInChecker]
    public class ReportDoctorWiseSpecialProdPrescrController : Controller
    {
        ReportDoctorWiseSpecialProdPrescrDAO ReportDoctorWiseSpecialProdPrescrDAO=new ReportDoctorWiseSpecialProdPrescrDAO();
        public ActionResult frmReportDoctorWiseSpecialProdPrescr()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetDoctorWiseProdPrescrData(string depotCode, string zoneCode, string regionCode, string areaCode, string territoryCode, string fromDate, string toDate, string prodType)
        {
            var listData = ReportDoctorWiseSpecialProdPrescrDAO.GetDoctorWiseProdPrescrData(depotCode, zoneCode, regionCode, areaCode, territoryCode, fromDate, toDate, prodType);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }
    }
}