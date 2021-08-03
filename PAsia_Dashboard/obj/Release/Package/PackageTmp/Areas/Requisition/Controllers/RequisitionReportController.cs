using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using PAsia_Dashboard.Areas.Requisition.Models.BEL;
using PAsia_Dashboard.Areas.Requisition.Models.DAL;
using PAsia_Dashboard.Universal.Gateway;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PAsia_Dashboard.Areas.Requisition.Controllers
{
    [LogInChecker]
    public class RequisitionReportController : Controller
    {
        RequisitionReportDAL requisitionReportDAL = new RequisitionReportDAL();
        // GET: Requisition/RequisitionReport
        public ActionResult frmRequisitionReport()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetRequisitionNo(string PrepareFromDate, string PrepareToDate, string ApprovedFromDate, string ApprovedToDate, string empCode)
        {
            List<RequisitionReportBEL> allReq = requisitionReportDAL.GetAllRequisition(PrepareFromDate, PrepareToDate, ApprovedFromDate, ApprovedToDate, empCode);
            return Json(allReq, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetEmployees(string PrepareFromDate, string PrepareToDate, string ApprovedFromDate, string ApprovedToDate)
        {
            List<RequisitionReportBEL> allEmployee = requisitionReportDAL.GetEmployees(PrepareFromDate, PrepareToDate, ApprovedFromDate, ApprovedToDate);
            return Json(allEmployee, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult RV(RequisitionReportBEL model)
        {
            ReportDocument rd = new ReportDocument();
            DataTable dt = new DataTable();
            string ReportPath = Server.MapPath("~/Areas/Requisition/Reports");
            string imagePath = Server.MapPath("~/Image/logo.JPG");

            string day = DateTime.Now.Day.ToString().Length == 1 ? "0" + DateTime.Now.Day.ToString() : DateTime.Now.Day.ToString();
            string month = DateTime.Now.Month.ToString().Length == 1 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString();
            string rptName = day + month + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString();

            rptName = "Requisition" + rptName;
            dt = requisitionReportDAL.GetDateWiseRequisition(model);
            ReportPath = ReportPath + "/rptRequisition.rpt";
            rd.Load(ReportPath);
            rd.SetDataSource(dt);
            //var x= dt.PREPARED_BY;

            string preparedBySignaturePath = Server.MapPath("~/Image/Signature/");
            string checkedBySignaturePath = Server.MapPath("~/Image/Signature/");
            string verifiedBySignaturePath = Server.MapPath("~/Image/Signature/");
            string recommendedBySignaturePath = Server.MapPath("~/Image/Signature/");
            string approvedBySignaturePath = Server.MapPath("~/Image/Signature/");
            string signaturePath = Server.MapPath("~/Image/Signature/");

            rd.SetParameterValue("imageUrl", imagePath);
            rd.SetParameterValue("PreparedBySignatureUrl", signaturePath);
            rd.SetParameterValue("CheckedBySignatureUrl", signaturePath);
            rd.SetParameterValue("VerifiedBySignatureUrl", signaturePath);
            rd.SetParameterValue("RecommendedBySignatureUrl", signaturePath);
            rd.SetParameterValue("ApprovedBySignatureUrl", signaturePath);

            CompanyInfoBEL companyInfoBEL = new CompanyInfoBEL();
            companyInfoBEL = requisitionReportDAL.GetCompanyInfo();

            TextObject vCompanyName;
            vCompanyName = (TextObject)rd.ReportDefinition.ReportObjects["txtComName"];
            vCompanyName.Text = companyInfoBEL.ComName;

            TextObject vAddress;
            vAddress = (TextObject)rd.ReportDefinition.ReportObjects["txtComAddress"];
            vAddress.Text = companyInfoBEL.ComAddress;

            //TextObject vDevBy;
            //vDevBy = (TextObject)rd.ReportDefinition.ReportObjects["txtDevBy"];
            // vDevBy.Text = reportDataHeader.DevCompany;

            HeadingBEL headingBEL = new HeadingBEL();
            headingBEL = requisitionReportDAL.GetHeading();

            TextObject vPreparedBy;
            vPreparedBy = (TextObject)rd.ReportDefinition.ReportObjects["txtPreparedBy"];
            vPreparedBy.Text = headingBEL.PreparedBy;

            TextObject vPreparedBySign;
            vPreparedBySign = (TextObject)rd.ReportDefinition.ReportObjects["txtPreparedBySign"];
            vPreparedBySign.Text = headingBEL.PreparedBy;

            TextObject vCheckedBy;
            vCheckedBy = (TextObject)rd.ReportDefinition.ReportObjects["txtCheckedBy"];
            vCheckedBy.Text = headingBEL.CheckedBy;

            TextObject vCheckedByForStatus;
            vCheckedByForStatus = (TextObject)rd.ReportDefinition.ReportObjects["txtCheckedByForStatus"];
            vCheckedByForStatus.Text = headingBEL.CheckedBy;

            TextObject vCheckedBySign;
            vCheckedBySign = (TextObject)rd.ReportDefinition.ReportObjects["txtCheckedBySign"];
            vCheckedBySign.Text = headingBEL.CheckedBy;

            TextObject vVerifiedBy;
            vVerifiedBy = (TextObject)rd.ReportDefinition.ReportObjects["txtVerifiedBy"];
            vVerifiedBy.Text = headingBEL.VerifiedBy;

            TextObject vVerifiedByForStatus;
            vVerifiedByForStatus = (TextObject)rd.ReportDefinition.ReportObjects["txtVerifiedByForStatus"];
            vVerifiedByForStatus.Text = headingBEL.VerifiedBy;


            TextObject vVerifiedBySign;
            vVerifiedBySign = (TextObject)rd.ReportDefinition.ReportObjects["txtVerifiedBySign"];
            vVerifiedBySign.Text = headingBEL.VerifiedBy;

            TextObject vRecommendBy;
            vRecommendBy = (TextObject)rd.ReportDefinition.ReportObjects["txtRecommendBy"];
            vRecommendBy.Text = headingBEL.RecommendBy;

            TextObject vRecommendByForStatus;
            vRecommendByForStatus = (TextObject)rd.ReportDefinition.ReportObjects["txtRecommendByForStatus"];
            vRecommendByForStatus.Text = headingBEL.RecommendBy;


            TextObject vRecommendBySign;
            vRecommendBySign = (TextObject)rd.ReportDefinition.ReportObjects["txtRecommendBySign"];
            vRecommendBySign.Text = headingBEL.RecommendBy;

            TextObject vApprovedBy;
            vApprovedBy = (TextObject)rd.ReportDefinition.ReportObjects["txtApprovedBy"];
            vApprovedBy.Text = headingBEL.ApprovedBy;

            TextObject vApprovedByForStatus;
            vApprovedByForStatus = (TextObject)rd.ReportDefinition.ReportObjects["txtApprovedByForStatus"];
            vApprovedByForStatus.Text = headingBEL.ApprovedBy;

            TextObject vApprovedBySign;
            vApprovedBySign = (TextObject)rd.ReportDefinition.ReportObjects["txtApprovedBySign"];
            vApprovedBySign.Text = headingBEL.ApprovedBy;

            if (dt.Rows.Count > 0)
            {
                rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, rptName);
                rd.Close();
                rd.Dispose();
                return View();
            }
            else
            {
                rd.Close();
                rd.Dispose();
                return Content("<script language='javascript' type='text/javascript'>alert('Data not found contact with vendor ');</script>");

                // return RedirectToAction("RequisitionReport", "RV");
            }
        }
    }
}