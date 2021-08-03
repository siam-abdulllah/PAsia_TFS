using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PAsia_Dashboard.Areas.FSM.Models.DAL.DAO;

namespace PAsia_Dashboard.Areas.FSM.Controllers
{
    public class ReportAMWisePrescriptionController : Controller
    {
        readonly ReportAMWisePrescriptionDAO _reportAmWisePrescriptionDao=new ReportAMWisePrescriptionDAO();
        public ActionResult frmReportAMWisePrescription()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetAMWisePrescriptionData(string fromDate, string toDate)
        {
            var listData = _reportAmWisePrescriptionDao.GetAMWisePrescriptionData(fromDate, toDate);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }
    }
}