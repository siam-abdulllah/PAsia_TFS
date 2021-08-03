﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PAsia_Dashboard.Areas.FSM.Models.BEL.BEO;
using PAsia_Dashboard.Areas.FSM.Models.DAL.DAO;
using PAsia_Dashboard.Universal.Gateway;

namespace PAsia_Dashboard.Areas.FSM.Controllers
{
    [LogInChecker]
    public class HonorariumDoctorDataUploadController : Controller
    {
        
        readonly HonorariumDoctorDataUploadDAO honorariumDoctorDataUploadDAO=new HonorariumDoctorDataUploadDAO();
       DefaultDAO defaultDAO=new DefaultDAO();
        public ActionResult frmHonorariumDoctorDataUpload()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult LoadExcelFile(string fileName, string physicalPath)
        {
            try
            {
                var excelData = honorariumDoctorDataUploadDAO.LoadExcelFileHonorariumDoctorData(fileName, physicalPath);
                if (excelData != null && honorariumDoctorDataUploadDAO.ExceptionReturn == null)
                {
                    var data = Json(excelData, JsonRequestBehavior.AllowGet);
                    data.MaxJsonLength = int.MaxValue;
                    return data;
                }
                else
                    return Json(new { Status = honorariumDoctorDataUploadDAO.ExceptionReturn });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new { Status = e.Message });
            }
            // return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult OperationsMode(List<HonorariumDoctorDataUploadInfo> model)
        {
            try
            {
               var message = honorariumDoctorDataUploadDAO.SaveUpdate(model);
               return Json(new { Status = message });
            }

            catch (Exception e)
            {
                if (e.Message.Substring(0, 9) == "ORA-00001")
                    return Json(new { Status = "Error:ORA-00001,Data already exists!" });//Unique Identifier.
                else if (e.Message.Substring(0, 9) == "ORA-02292")
                    return Json(new { Status = "Error:ORA-02292,Data already exists!" });//Child Record Found.
                else if (e.Message.Substring(0, 9) == "ORA-12899")
                    return Json(new { Status = "Error:ORA-12899,Data Value Too Large!" });//Value Too Large.
                else
                    return Json(new { Status = "! Error : Error Code:" + e.Message.Substring(0, 9) });//Other Wise Error Found

            }
        }
        [HttpPost]
        public ActionResult GetDoctorGridData(string depotCode, string zoneCode, string regionCode, string areaCode, string territoryCode, string fromDate, string toDate)
        {
            try
            {

           
            var listData = honorariumDoctorDataUploadDAO.GetDoctorGridData(depotCode, zoneCode, regionCode, areaCode, territoryCode, fromDate,  toDate);
            if (listData != null && honorariumDoctorDataUploadDAO.ExceptionReturn == null)
            {
                var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
            }
            else
                return Json(new { Status = honorariumDoctorDataUploadDAO.ExceptionReturn });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new { Status = e.Message });
            }

        }
    }
}