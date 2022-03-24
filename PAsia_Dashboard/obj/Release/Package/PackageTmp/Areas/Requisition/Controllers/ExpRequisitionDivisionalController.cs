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
    public class ExpRequisitionDivisionalController : Controller
    {

        ExpRequisitionDivisionalDAL _expRequisitionDivisionalDAL = new ExpRequisitionDivisionalDAL();
        // GET: Requisition/ExpRequisitionDivisional
        public ActionResult frmExpRequisitionDivisional()
        {
            return View();
        }


        [HttpPost]
        public ActionResult GetExpReqMstList(string param)
        {
            var data = _expRequisitionDivisionalDAL.GetExpReqMstList(param);
            return Json(new { Data = data, Status = "Ok" });
        }
        [HttpPost]
        public ActionResult GetExpDivisionalReqMstList(string param)
        {
            var listData = _expRequisitionDivisionalDAL.GetExpDivisionalReqMstList(param);
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
            var data = _expRequisitionDivisionalDAL.GetExpReqDtlList(mstId);
            return Json(new { Data = data, Status = "Ok" });
        }
        [HttpPost]
        public ActionResult InsertExpReqDivisionalInfo(ExpReqPrepareMst expReqPrepareMstInfo, List<ExpReqPrepareDtl> expReqPrepareDtlData)
        {
            return _expRequisitionDivisionalDAL.InsertExpReqDivisionalInfo(expReqPrepareMstInfo, expReqPrepareDtlData) ? Json(new { Status = "Ok" }) : Json(new { Status = _expRequisitionDivisionalDAL.ExceptionReturn });

        }
        [HttpPost]
        public ActionResult UpdateExpReqDivisionalInfo(ExpReqPrepareMst expReqPrepareMstInfo, List<ExpReqPrepareDtl> expReqPrepareDtlData)
        {
            return _expRequisitionDivisionalDAL.UpdateExpReqDivisionalInfo(expReqPrepareMstInfo, expReqPrepareDtlData) ? Json(new { Status = "Ok" }) : Json(new { Status = _expRequisitionDivisionalDAL.ExceptionReturn });

        }


    }
}