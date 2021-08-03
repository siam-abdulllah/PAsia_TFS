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
    public class ExpRequisitionVerifyController : Controller
    {
        ExpRequisitionVerifyDAL _expRequisitionVerifyDAL = new ExpRequisitionVerifyDAL();
        public ActionResult frmExpRequisitionVerify()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetExpReqMstList(string param)
        {
            var data = _expRequisitionVerifyDAL.GetExpReqMstList(param);
            return Json(new { Data = data, Status = "Ok" });
        }
        [HttpPost]
        public ActionResult GetExpVerifiedReqMstList(string param)
        {
            var listData = _expRequisitionVerifyDAL.GetExpVerifiedReqMstList(param);
            if (listData != null)
            {
                var data = Json(listData, JsonRequestBehavior.AllowGet);
                data.MaxJsonLength = int.MaxValue;
                return data;
                //return Json(new { Data = data, Status = "Ok" });
            }
            else { return Json(new { Data = listData, Status = "Ok" }); }
        }
        [HttpPost]
        public ActionResult GetExpReqDtlList(string mstId)
        {
            var data = _expRequisitionVerifyDAL.GetExpReqDtlList(mstId);
            return Json(new { Data = data, Status = "Ok" });
        }
        [HttpPost]
        public ActionResult InsertExpReqVerifiedInfo(ExpReqPrepareMst expReqPrepareMstInfo, List<ExpReqPrepareDtl> expReqPrepareDtlData)
        {
            return _expRequisitionVerifyDAL.InsertExpReqVerifiedInfo(expReqPrepareMstInfo, expReqPrepareDtlData) ? Json(new { Status = "Ok" }) : Json(new { Status = _expRequisitionVerifyDAL.ExceptionReturn });

        }
        [HttpPost]
        public ActionResult UpdateExpReqVerifiedInfo(ExpReqPrepareMst expReqPrepareMstInfo, List<ExpReqPrepareDtl> expReqPrepareDtlData)
        {
            return _expRequisitionVerifyDAL.UpdateExpReqVerifiedInfo(expReqPrepareMstInfo, expReqPrepareDtlData) ? Json(new { Status = "Ok" }) : Json(new { Status = _expRequisitionVerifyDAL.ExceptionReturn });

        }
    }
}