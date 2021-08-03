using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PAsia_Dashboard.Areas.FSM.Models.DAL.DAO;
using PAsia_Dashboard.Universal.Gateway;

namespace PAsia_Dashboard.Areas.FSM.Controllers
{
    [LogInChecker]
    public class ReportMPOWisePrescriptionController : Controller
    {
        ReportMPOWisePrescriptionDAO reportMpoWisePrescriptionDAO = new ReportMPOWisePrescriptionDAO();
        public ActionResult frmReportMPOWisePrescription()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetMPOWisePrescriptionData(string depotCode, string zoneCode, string regionCode, string areaCode, string territoryCode, string fromDate, string toDate, string doctorCode, string ProdType)
        {
            var listData = reportMpoWisePrescriptionDAO.GetMPOWisePrescriptionData(depotCode, zoneCode, regionCode, areaCode, territoryCode, fromDate, toDate, doctorCode, ProdType);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }
        [HttpPost]
        public JsonResult DeletePrescription(string mstSl, string prescriptionUrl)
        {
            try
            {
                var message = " ";
                string root = Server.MapPath("~");
                string parent = Path.GetDirectoryName(root);
                string grandParent = Path.GetDirectoryName(parent);
                //string serverIpAdd = "http://202.84.32.118:8999";
                // string filePath = Server.MapPath("~//Image/20190512-114444-1579.jpg" + FolderName + "/" + FileName);
                // string filePath = Server.MapPath("~/Image/20190512-114444-1579.jpg");
                string filePath = grandParent + "/ESOPharmaAsia" + prescriptionUrl;
               // string filePath = grandParent + "/ESOPharmaAsia/Image/20190512-114444-1579.jpg";
               
                if (reportMpoWisePrescriptionDAO.DeletePrescription(mstSl, filePath))
                {
                    message = "Delete";
                    return Json(new { Message = message });
                }

                return Json(new { Message = message });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new { Message = e.Message });
            }
        }

    }
}
