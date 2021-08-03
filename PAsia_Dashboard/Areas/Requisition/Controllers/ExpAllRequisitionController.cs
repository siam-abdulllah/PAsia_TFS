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
    public class ExpAllRequisitionController : Controller
    {
        ExpAllRequisitionDAL _expAllRequisitionDAL = new ExpAllRequisitionDAL();
        public ActionResult frmExpAllRequisition()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetExpAllReqMstList(string param)
        {
            var data = _expAllRequisitionDAL.GetExpAllReqMstList(param);
            return Json(new { Data = data, Status = "Ok" });
        }
        [HttpPost]
        public ActionResult GetExpReqDtlList(string mstId)
        {
            var data = _expAllRequisitionDAL.GetExpReqDtlList(mstId);
            return Json(new { Data = data, Status = "Ok" });
        }
    }
}