using PAsia_Dashboard.Areas.Requisition.Models.DAL;
using PAsia_Dashboard.Universal.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PAsia_Dashboard.Areas.Requisition.Controllers
{
    [LogInChecker]
    public class ExpIndividualRequisitionController : Controller
    {
        ExpIndividualRequisitionDAL _ExpIndividualRequisitionDAL = new ExpIndividualRequisitionDAL();
        public ActionResult frmExpIndividualRequisition()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetExpAllReqMstList(string param)
        {
            var data = _ExpIndividualRequisitionDAL.GetExpAllReqMstList(param);
            return Json(new { Data = data, Status = "Ok" });
        }
        [HttpPost]
        public ActionResult GetExpReqDtlList(string mstId)
        {
            var data = _ExpIndividualRequisitionDAL.GetExpReqDtlList(mstId);
            return Json(new { Data = data, Status = "Ok" });
        }
    }
}