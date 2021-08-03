using PAsia_Dashboard.Areas.FSM.Models.DAL;
using PAsia_Dashboard.Universal.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PAsia_Dashboard.Areas.FSM.Controllers
{
    [LogInChecker]
    public class DashboardRegionalPrescriptionController : Controller
    {
        //
        // GET: /FSM/RegionalPrescriptionDashboard/

        DashboardRegionalPrescriptionDAO PrimaryDAO = new DashboardRegionalPrescriptionDAO();
        public ActionResult frmDashboardRegionalPrescription()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetRegionalPrescriptionData(string prodType)
        {
            var data = PrimaryDAO.GetRegionalPrescriptionData( prodType);
            return Json(new { Data = data, Status = "Ok" });
        }


        [HttpPost]
        public ActionResult GetGridData(string prodType)
        {
            var data = PrimaryDAO.GetGridData( prodType);
            return Json(new { Data = data, Status = "Ok" });
        }
	}
}