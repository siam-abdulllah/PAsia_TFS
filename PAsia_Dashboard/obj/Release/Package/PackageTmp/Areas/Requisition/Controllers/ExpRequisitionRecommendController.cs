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
    public class ExpRequisitionRecommendController : Controller
    {
        ExpRequisitionRecommendDAL _expRequisitionRecommendDAL = new ExpRequisitionRecommendDAL();
        public ActionResult frmExpRequisitionRecommend()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetExpReqMstList(string param)
        {
            var data = _expRequisitionRecommendDAL.GetExpReqMstList(param);
            return Json(new { Data = data, Status = "Ok" });
        }
        [HttpPost]
        public ActionResult GetExpRecommendedReqMstList(string param)
        {
            var listData = _expRequisitionRecommendDAL.GetExpRecommendedReqMstList(param);
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
            var data = _expRequisitionRecommendDAL.GetExpReqDtlList(mstId);
            return Json(new { Data = data, Status = "Ok" });
        }
        [HttpPost]
        public ActionResult InsertExpReqRecommendedInfo(ExpReqPrepareMst expReqPrepareMstInfo, List<ExpReqPrepareDtl> expReqPrepareDtlData)
        {
            return _expRequisitionRecommendDAL.InsertExpReqRecommendedInfo(expReqPrepareMstInfo, expReqPrepareDtlData) ? Json(new { Status = "Ok" }) : Json(new { Status = _expRequisitionRecommendDAL.ExceptionReturn });

        }
        [HttpPost]
        public ActionResult UpdateExpReqRecommendedInfo(ExpReqPrepareMst expReqPrepareMstInfo, List<ExpReqPrepareDtl> expReqPrepareDtlData)
        {
            return _expRequisitionRecommendDAL.UpdateExpReqRecommendedInfo(expReqPrepareMstInfo, expReqPrepareDtlData) ? Json(new { Status = "Ok" }) : Json(new { Status = _expRequisitionRecommendDAL.ExceptionReturn });

        }
    }
}