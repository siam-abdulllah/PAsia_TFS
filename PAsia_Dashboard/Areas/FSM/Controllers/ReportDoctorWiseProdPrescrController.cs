using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PAsia_Dashboard.Areas.FSM.Models.DAL;
using PAsia_Dashboard.Universal.Gateway;

namespace PAsia_Dashboard.Areas.FSM.Controllers
{
    [LogInChecker]
    public class ReportDoctorWiseProdPrescrController : Controller
    {
        ReportDoctorWiseProdPrescrDAO reportDoctorWiseProdPrescrDAO=new ReportDoctorWiseProdPrescrDAO();
        public ActionResult frmReportDoctorWiseProdPrescr()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetDoctorWiseProdPrescrData(string depotCode, string zoneCode, string regionCode, string areaCode, string territoryCode, string fromDate, string toDate, string prodType, string doctorType)
        {
            var listData = reportDoctorWiseProdPrescrDAO.GetDoctorWiseProdPrescrData(depotCode, zoneCode, regionCode, areaCode, territoryCode, fromDate, toDate, prodType, doctorType);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }
        [HttpPost]
        public ActionResult GetMPOWisePrescriptionData(string depotCode, string zoneCode, string regionCode, string areaCode, string territoryCode, string fromDate, string toDate, string doctorCode, string ProdType)
        {
            var listData = reportDoctorWiseProdPrescrDAO.GetMPOWisePrescriptionData(depotCode, zoneCode, regionCode, areaCode, territoryCode, fromDate, toDate, doctorCode, ProdType);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }
    }
}