using PAsia_Dashboard.Areas.Requisition.Models.DAL;
using PAsia_Dashboard.Universal.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static PAsia_Dashboard.Areas.Requisition.Models.BEL.ExpRequisitionPrepareBEL;

namespace PAsia_Dashboard.Areas.Requisition.Controllers
{
    [LogInChecker]
    public class ExpRequisitionApproveController : Controller
    {
        ExpRequisitionApproveDAL _expRequisitionApproveDAL = new ExpRequisitionApproveDAL();
        public ActionResult frmExpRequisitionApprove()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetExpReqMstList(string param)
        {
            var data = _expRequisitionApproveDAL.GetExpReqMstList(param);
            return Json(new { Data = data, Status = "Ok" });
        }
        [HttpPost]
        public ActionResult GetExpApprovedReqMstList(string param)
        {
            var listData = _expRequisitionApproveDAL.GetExpApprovedReqMstList(param);
            if (listData != null)
            {
                var data = Json(listData, JsonRequestBehavior.AllowGet);
                data.MaxJsonLength = int.MaxValue;
                return data;
            }
            return Json(new { Data = listData, Status = "Ok" });
        }
        [HttpPost]
        public ActionResult GetExpReqDtlList(string mstId)
        {
            var data = _expRequisitionApproveDAL.GetExpReqDtlList(mstId);
            return Json(new { Data = data, Status = "Ok" });
        }
        [HttpPost]
        public ActionResult InsertExpReqApprovedInfo(ExpReqPrepareMst expReqPrepareMstInfo, List<ExpReqPrepareDtl> expReqPrepareDtlData)
        {
            return _expRequisitionApproveDAL.InsertExpReqApprovedInfo(expReqPrepareMstInfo, expReqPrepareDtlData) ? Json(new { Status = "Ok" }) : Json(new { Status = _expRequisitionApproveDAL.ExceptionReturn });

        }
        [HttpPost]
        public ActionResult UpdateExpReqApprovedInfo(ExpReqPrepareMst expReqPrepareMstInfo, List<ExpReqPrepareDtl> expReqPrepareDtlData)
        {
            return _expRequisitionApproveDAL.UpdateExpReqApprovedInfo(expReqPrepareMstInfo, expReqPrepareDtlData) ? Json(new { Status = "Ok" }) : Json(new { Status = _expRequisitionApproveDAL.ExceptionReturn });

        }
    }
}