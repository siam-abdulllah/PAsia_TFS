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
    public class ExpRequisitionCheckController : Controller
    {
        ExpRequisitionCheckDAL _expRequisitionCheckDAL = new ExpRequisitionCheckDAL();
        public ActionResult frmExpRequisitionCheck()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetExpReqMstList(string param)
        {
            var data = _expRequisitionCheckDAL.GetExpReqMstList(param);
            return Json(new { Data = data, Status = "Ok" });
        }
        [HttpPost]
        public ActionResult GetExpCheckedReqMstList(string param)
        {
            var listData = _expRequisitionCheckDAL.GetExpCheckedReqMstList(param);
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
            var data = _expRequisitionCheckDAL.GetExpReqDtlList(mstId);
            return Json(new { Data = data, Status = "Ok" });
        }
        [HttpPost]
        public ActionResult InsertExpReqCheckInfo(ExpReqPrepareMst expReqPrepareMstInfo, List<ExpReqPrepareDtl> expReqPrepareDtlData)
        {
            return _expRequisitionCheckDAL.InsertExpReqCheckInfo(expReqPrepareMstInfo, expReqPrepareDtlData) ? Json(new { Status = "Ok"}) : Json(new { Status = _expRequisitionCheckDAL.ExceptionReturn });

        }
        [HttpPost]
        public ActionResult UpdateExpReqCheckInfo(ExpReqPrepareMst expReqPrepareMstInfo, List<ExpReqPrepareDtl> expReqPrepareDtlData)
        {
            return _expRequisitionCheckDAL.UpdateExpReqCheckInfo(expReqPrepareMstInfo, expReqPrepareDtlData) ? Json(new { Status = "Ok"}) : Json(new { Status = _expRequisitionCheckDAL.ExceptionReturn });

        }
    }
}