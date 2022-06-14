
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
    public class ExpRequisitionPrepareController : Controller
    {
        private readonly CommonMailer _cmn = new CommonMailer();
        ExpRequisitionPrepareDAL _expRequisitionPrepareDAL = new ExpRequisitionPrepareDAL();
        public ActionResult frmExpRequisitionPrepare()
        {
            return View();
        }

        //----------Requisition Type-----------//

        [HttpPost]
        public ActionResult GetRequisitionType(string param)
        {
            var data = _expRequisitionPrepareDAL.GetRequisitionType(param);
            return Json(new { Data = data, Status = "Ok" });
        }
         [HttpPost]
        public ActionResult SendMail(string param)
        {
            var data = _cmn.SendMail("tapu.it@pharmasia.com.bd", "Mail Check", "It is a demo mail for mail service checking.");
            return Json(new { Data = data, Status = "Ok" });
        }

        //----------Pay To-----------//

        [HttpPost]
        public ActionResult GetPayTo(string param)
        {
            var listData = _expRequisitionPrepareDAL.GetPayTo(param);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
            //return Json(new { Data = data, Status = "Ok" });
        } 
        [HttpPost]
        public ActionResult GetPaymentPlace(string param)
        {
            var data = _expRequisitionPrepareDAL.GetPaymentPlace(param);
            return Json(new { Data = data, Status = "Ok" });
        }
        [HttpPost]
        public ActionResult GetExpReqMstList(string param)
        {
            var data = _expRequisitionPrepareDAL.GetExpReqMstList(param);
            return Json(new { Data = data, Status = "Ok" });
        }
        
        [HttpPost]
        public ActionResult GetExpReqDtlList(string mstId)
        {
            var data = _expRequisitionPrepareDAL.GetExpReqDtlList(mstId);
            return Json(new { Data = data, Status = "Ok" });
        }


        //------INSERT -------//

        [HttpPost]
        public ActionResult InsertExpReqPrepareInfo(ExpReqPrepareMst expReqPrepareMstInfo, List<ExpReqPrepareDtl> expReqPrepareDtlData)
        {
            return _expRequisitionPrepareDAL.InsertExpReqPrepareInfo(expReqPrepareMstInfo, expReqPrepareDtlData) ? Json(new { Status = "Ok", Id = _expRequisitionPrepareDAL.MaxID, Code = _expRequisitionPrepareDAL.MaxCode }) : Json(new { Status = _expRequisitionPrepareDAL.ExceptionReturn });
        }
        [HttpPost]
        public ActionResult UpdateExpReqPrepareInfo(ExpReqPrepareMst expReqPrepareMstInfo, List<ExpReqPrepareDtl> expReqPrepareDtlData)
        {
            return _expRequisitionPrepareDAL.UpdateExpReqPrepareInfo(expReqPrepareMstInfo, expReqPrepareDtlData) ? Json(new { Status = "Ok", Id = _expRequisitionPrepareDAL.MaxID, Code = _expRequisitionPrepareDAL.MaxCode }) : Json(new { Status = _expRequisitionPrepareDAL.ExceptionReturn });

        }
        [HttpPost]
        public ActionResult SubmitExpReqPrepareInfo(ExpReqPrepareMst expReqPrepareMstInfo)
        {
            return _expRequisitionPrepareDAL.SubmitExpReqPrepareInfo(expReqPrepareMstInfo) ? Json(new { Status = "Ok"}) : Json(new { Status = _expRequisitionPrepareDAL.ExceptionReturn });
        }
         [HttpPost]
        public ActionResult DeleteExpReqMst(string mstId)
        {
            return _expRequisitionPrepareDAL.DeleteExpReqMst(mstId) ? Json(new { Status = "Ok"}) : Json(new { Status = _expRequisitionPrepareDAL.ExceptionReturn });
        }
         [HttpPost]
        public ActionResult DeleteExpReqDtl(string dtlId)
        {
            return _expRequisitionPrepareDAL.DeleteExpReqDtl(dtlId) ? Json(new { Status = "Ok"}) : Json(new { Status = _expRequisitionPrepareDAL.ExceptionReturn });
        }






    }
}