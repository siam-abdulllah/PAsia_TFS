using PAsia_Dashboard.Areas.Requisition.Models.BEL;
using PAsia_Dashboard.Areas.Security.DAO;
using PAsia_Dashboard.Universal.Gateway;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Globalization;
using System.Linq;
using System.Web;
using static PAsia_Dashboard.Areas.Requisition.Models.BEL.ExpRequisitionPrepareBEL;

namespace PAsia_Dashboard.Areas.Requisition.Models.DAL
{
    public class ExpRequisitionVerifyDAL : ReturnData
    {
        DBHelper dbHelper = new DBHelper();
        DBConnection dbConnection = new DBConnection();
        IDGenerated idGenerated = new IDGenerated();
        AuditTrailDAO _auditTrailDAO = new AuditTrailDAO();

        internal object GetExpReqMstList(string param)
        {
            try
            {
                string empCode = HttpContext.Current.Session["EMPLOYEE_CODE"].ToString();
                string qry =   "SELECT             MST_ID," +
                                "                  REQUISITION_NO," +
                                "                  REQUISITION_TYPE," +
                                "                  EXPENDITURE_MONTH," +
                                "                  PAY_TO_NAME," +
                                "                  PAY_TO_DESIG," +
                                "                  PAYMENT_PLACE," +
                                "                  PREPARED_BY," +
                                "                  PREPARED_BY_NAME," +
                                "                  PREPARED_BY_DESIG," +
                                "                  PREPARED_DATE," +
                                "                  PREPARED_REMARKS," +
                                "                  CHECKED_BY_NAME," +
                                "                  CHECKED_BY_DESIG," +
                                "                  CHECKED_REMARKS," +
                                "                  CHECKED_STATUS," +
                                "                  DIVISIONAL_BY_NAME," +
                                "                  DIVISIONAL_BY_DESIG," +
                                "                  DIVISIONAL_DATE," +
                                "                  DIVISIONAL_REMARKS," +
                                "                  DIVISIONAL_STATUS," +
                                "                  VERIFIED_STATUS," +
                                "                  RECOMMENDED_STATUS," +
                                "                  APPROVED_STATUS," +
                                "                  CHECKED_DATE," +
                                "                  VERIFIED_DATE," +
                                "                  RECOMMENDED_DATE," +
                                "                  APPROVED_DATE," +
                                "                  PREPARED_BY_CONFIRM" +
                                "  FROM " +
                                "          (                " +
                                "          SELECT DISTINCT MST_ID," +
                                "                          REQUISITION_NO," +
                                "                          REQUISITION_TYPE," +
                                "                          EXPENDITURE_MONTH," +
                                "                          PAY_TO_NAME," +
                                "                          PAY_TO_DESIG," +
                                "                          PAYMENT_PLACE," +
                                "                          PREPARED_BY," +
                                "                          PREPARED_BY_NAME," +
                                "                          PREPARED_BY_DESIG," +
                                "                          PREPARED_DATE," +
                                "                          PREPARED_REMARKS," +
                                "                          CHECKED_BY_NAME," +
                                "                          CHECKED_BY_DESIG," +
                                "                          CHECKED_REMARKS," +
                                "                          NVL (CHECKED_STATUS, 'Pending') CHECKED_STATUS," +
                                "                          DIVISIONAL_BY_NAME," +
                                "                          DIVISIONAL_BY_DESIG," +
                                "                          DIVISIONAL_DATE," +
                                "                          DIVISIONAL_REMARKS," +
                                "                          NVL (DIVISIONAL_STATUS, 'Pending') DIVISIONAL_STATUS," +
                                "                          NVL (VERIFIED_STATUS, 'Pending') VERIFIED_STATUS," +
                                "                          NVL (RECOMMENDED_STATUS, 'Pending') RECOMMENDED_STATUS," +
                                "                          NVL (APPROVED_STATUS, 'Pending') APPROVED_STATUS," +
                                "                          CHECKED_DATE," +
                                "                          VERIFIED_DATE," +
                                "                          RECOMMENDED_DATE," +
                                "                          APPROVED_DATE," +
                                "                          NVL (PREPARED_BY_CONFIRM, 'No') PREPARED_BY_CONFIRM" +
                                "            FROM          VW_EXP_REQUISITION" +
                                "           WHERE     1 = 1" +
                                "                 AND PREPARED_BY IS NOT NULL" +
                                "                 AND PREPARED_BY_CONFIRM IS NOT NULL" +
                                "                 AND PREPARED_BY_DEPARTMENT IN ('19','23','31','32','22')        " +
                                "                 AND DIVISIONAL_STATUS = 'Approved'" +
                                "                 AND VERIFIED_STATUS IS NULL" +
                                "        UNION ALL" +
                                "          SELECT DISTINCT MST_ID," +
                                "                          REQUISITION_NO," +
                                "                          REQUISITION_TYPE," +
                                "                          EXPENDITURE_MONTH," +
                                "                          PAY_TO_NAME," +
                                "                          PAY_TO_DESIG," +
                                "                          PAYMENT_PLACE," +
                                "                          PREPARED_BY," +
                                "                          PREPARED_BY_NAME," +
                                "                          PREPARED_BY_DESIG," +
                                "                          PREPARED_DATE," +
                                "                          PREPARED_REMARKS," +
                                "                          CHECKED_BY_NAME," +
                                "                          CHECKED_BY_DESIG," +
                                "                          CHECKED_REMARKS," +
                                "                          NVL (CHECKED_STATUS, 'Pending') CHECKED_STATUS," +
                                "                          DIVISIONAL_BY_NAME," +
                                "                          DIVISIONAL_BY_DESIG," +
                                "                          DIVISIONAL_DATE," +
                                "                          DIVISIONAL_REMARKS," +
                                "                          NVL (DIVISIONAL_STATUS, 'Pending') DIVISIONAL_STATUS," +
                                "                          NVL (VERIFIED_STATUS, 'Pending') VERIFIED_STATUS," +
                                "                          NVL (RECOMMENDED_STATUS, 'Pending') RECOMMENDED_STATUS," +
                                "                          NVL (APPROVED_STATUS, 'Pending') APPROVED_STATUS," +
                                "                          CHECKED_DATE," +
                                "                          VERIFIED_DATE," +
                                "                          RECOMMENDED_DATE," +
                                "                          APPROVED_DATE," +
                                "                          NVL (PREPARED_BY_CONFIRM, 'No') PREPARED_BY_CONFIRM" +
                                "            FROM          VW_EXP_REQUISITION" +
                                "           WHERE     1 = 1" +
                                "                 AND PREPARED_BY IS NOT NULL" +
                                "                 AND PREPARED_BY_CONFIRM IS NOT NULL" +
                                "                 AND PREPARED_BY_DEPARTMENT NOT IN ('19','23','31','32') " +
                                "                 AND CHECKED_STATUS = 'Approved'" +
                                "                 AND VERIFIED_STATUS IS NULL" +
                                "        ) " +
                                "ORDER BY REQUISITION_NO DESC ";




                DataTable dt = dbHelper.GetDataTable(dbConnection.SAConnStrReader("Sales"), qry);
                var item = (from DataRow row in dt.Rows
                            select new ExpReqPrepareMst
                            {
                                MstId = Convert.ToInt32(row["MST_ID"]),
                                RequisitionNo = row["REQUISITION_NO"].ToString(),
                                ReqTypeName = row["REQUISITION_TYPE"].ToString(),
                                ExpenditureMonth = row["EXPENDITURE_MONTH"].ToString(),
                                PayToName = row["PAY_TO_NAME"].ToString(),
                                PayToDesig = row["PAY_TO_DESIG"].ToString(),
                                PaymentPlace = row["PAYMENT_PLACE"].ToString(),

                                PrepareBy = row["PREPARED_BY"].ToString(),
                                PrepareName = row["PREPARED_BY_NAME"].ToString(),
                                PrepareDesig = row["PREPARED_BY_DESIG"].ToString(),
                                PrepareDate = row["PREPARED_DATE"].ToString(),
                                PreparedByConfirm = row["PREPARED_BY_CONFIRM"].ToString(),
                                PrepareRemarks = row["PREPARED_REMARKS"].ToString(),

                                CheckedName = row["CHECKED_BY_NAME"].ToString(),
                                CheckedDesig = row["CHECKED_BY_DESIG"].ToString(),
                                CheckedDate = row["CHECKED_DATE"].ToString(),
                                CheckedStatus = row["CHECKED_STATUS"].ToString(),
                                CheckedRemarks = row["CHECKED_REMARKS"].ToString(),

                                DivisionalName = row["DIVISIONAL_BY_NAME"].ToString(),
                                DivisionalDesig = row["DIVISIONAL_BY_DESIG"].ToString(),
                                DivisionalDate = row["DIVISIONAL_DATE"].ToString(),
                                DivisionalStatus = row["DIVISIONAL_STATUS"].ToString(),
                                DivisionalRemarks = row["DIVISIONAL_REMARKS"].ToString(),


                                VerifiedDate = row["VERIFIED_DATE"].ToString(),
                                VerifiedStatus = row["VERIFIED_STATUS"].ToString(),
                                RecommendedStatus = row["RECOMMENDED_STATUS"].ToString(),
                                ApprovedDate = row["APPROVED_DATE"].ToString(),
                                ApprovedStatus = row["APPROVED_STATUS"].ToString()

                            }).ToList();
               // _auditTrailDAO.InsertAudit("frmExpRequisitionApprove", "EXP_REQUISITION_MST", IUMode, MaxID, "Done", "Test", "tapu.it@pharmasia.com.bd");
                return item;

            }
            catch (Exception e)
            {
                throw;
            }
        }

        internal object GetExpVerifiedReqMstList(string param)
        {
            try
            {
                string empCode = HttpContext.Current.Session["EMPLOYEE_CODE"].ToString();
                string qry = "SELECT  DISTINCT MST_ID,  REQUISITION_NO,  REQUISITION_TYPE,  EXPENDITURE_MONTH, " +
                     " PAY_TO_NAME, PAY_TO_DESIG, PAYMENT_PLACE,PREPARED_BY,PREPARED_BY_NAME,PREPARED_BY_DESIG,PREPARED_DATE,PREPARED_REMARKS,CHECKED_BY_NAME,CHECKED_BY_DESIG,CHECKED_REMARKS," +
                    " NVL( CHECKED_STATUS,'Pending') CHECKED_STATUS,DIVISIONAL_BY_NAME,DIVISIONAL_BY_DESIG,DIVISIONAL_DATE,DIVISIONAL_REMARKS,NVL(DIVISIONAL_STATUS,'Pending')DIVISIONAL_STATUS,VERIFIED_REMARKS,NVL( VERIFIED_STATUS,'Pending') VERIFIED_STATUS,NVL( RECOMMENDED_STATUS,'Pending') RECOMMENDED_STATUS," +
                    " NVL( APPROVED_STATUS,'Pending') APPROVED_STATUS, CHECKED_DATE, VERIFIED_DATE," +
                    " RECOMMENDED_DATE,APPROVED_DATE," +
                    " NVL( PREPARED_BY_CONFIRM,'No') PREPARED_BY_CONFIRM FROM VW_EXP_REQUISITION WHERE 1=1 AND VERIFIED_STATUS IS NOT NULL " + param + " " +
                    " ORDER BY    REQUISITION_NO DESC";
                DataTable dt = dbHelper.GetDataTable(dbConnection.SAConnStrReader("Sales"), qry);
                var item = (from DataRow row in dt.Rows
                            select new ExpReqPrepareMst
                            {
                                MstId = Convert.ToInt32(row["MST_ID"]),
                                RequisitionNo = row["REQUISITION_NO"].ToString(),
                                ReqTypeName = row["REQUISITION_TYPE"].ToString(),
                                ExpenditureMonth = row["EXPENDITURE_MONTH"].ToString(),
                                PayToName = row["PAY_TO_NAME"].ToString(),
                                PayToDesig = row["PAY_TO_DESIG"].ToString(),
                                PaymentPlace = row["PAYMENT_PLACE"].ToString(),

                                PrepareBy = row["PREPARED_BY"].ToString(),
                                PrepareName = row["PREPARED_BY_NAME"].ToString(),
                                PrepareDesig = row["PREPARED_BY_DESIG"].ToString(),
                                PrepareDate = row["PREPARED_DATE"].ToString(),
                                PreparedByConfirm = row["PREPARED_BY_CONFIRM"].ToString(),
                                PrepareRemarks = row["PREPARED_REMARKS"].ToString(),

                                CheckedName = row["CHECKED_BY_NAME"].ToString(),
                                CheckedDesig = row["CHECKED_BY_DESIG"].ToString(),
                                CheckedDate = row["CHECKED_DATE"].ToString(),
                                CheckedStatus = row["CHECKED_STATUS"].ToString(),
                                CheckedRemarks = row["CHECKED_REMARKS"].ToString(),

                                DivisionalName = row["DIVISIONAL_BY_NAME"].ToString(),
                                DivisionalDesig = row["DIVISIONAL_BY_DESIG"].ToString(),
                                DivisionalDate = row["DIVISIONAL_DATE"].ToString(),
                                DivisionalStatus = row["DIVISIONAL_STATUS"].ToString(),
                                DivisionalRemarks = row["DIVISIONAL_REMARKS"].ToString(),

                                VerifiedDate = row["VERIFIED_DATE"].ToString(),
                                VerifiedStatus = row["VERIFIED_STATUS"].ToString(),
                                VerifiedRemarks = row["VERIFIED_REMARKS"].ToString(),
                                RecommendedStatus = row["RECOMMENDED_STATUS"].ToString(),
                                ApprovedDate = row["APPROVED_DATE"].ToString(),
                                ApprovedStatus = row["APPROVED_STATUS"].ToString()

                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                throw;
            }
        }

        internal object GetExpReqDtlList(string mstId)
        {
            try
            {
                string qry = "SELECT DTL_ID, MST_ID, MOP,NVL(PREPARED_VALUE,0)PREPARED_VALUE,NVL(CHECKED_VALUE,0)CHECKED_VALUE,NVL(DIVISIONAL_VALUE,0)DIVISIONAL_VALUE,NVL(VERIFIED_VALUE,0) VERIFIED_VALUE,PURPOSE, " +
                    " TO_CHAR(FROM_DATE,'dd/MM/YYYY') FROM_DATE, TO_CHAR(TO_DATE,'dd/MM/YYYY') TO_DATE,TO_CHAR(REQUIRED_DATE,'dd/MM/YYYY') REQUIRED_DATE,REMARKS, TOTAL_DAYS FROM EXP_REQUISITION_DTL WHERE MST_ID=" + mstId;
                DataTable dt = dbHelper.GetDataTable(dbConnection.SAConnStrReader("Sales"), qry);
                var item = (from DataRow row in dt.Rows
                            select new ExpReqPrepareDtl
                            {
                                MstId = Convert.ToInt32(row["MST_ID"]),
                                DtlId = Convert.ToInt32(row["DTL_ID"]),
                                Mop = row["MOP"].ToString(),
                                PrepareValue = Convert.ToInt32(row["PREPARED_VALUE"]),
                                CheckedValue = Convert.ToInt32(row["CHECKED_VALUE"]),
                                DivisionalValue = Convert.ToInt32(row["DIVISIONAL_VALUE"]),
                                VerifiedValue = Convert.ToInt32(row["CHECKED_VALUE"]) == 0 ? Convert.ToInt32(row["DIVISIONAL_VALUE"]) : Convert.ToInt32(row["CHECKED_VALUE"]),
                                Purpose = row["PURPOSE"].ToString(),
                                FromDate = row["FROM_DATE"].ToString(),
                                ToDate = row["TO_DATE"].ToString(),
                                RequiredDate = row["REQUIRED_DATE"].ToString(),
                                Remarks = row["REMARKS"].ToString(),
                                TotalDays = row["TOTAL_DAYS"].ToString()
                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                throw;
            }
        }

        internal bool InsertExpReqVerifiedInfo(ExpReqPrepareMst expReqPrepareMstInfo, List<ExpReqPrepareDtl> expReqPrepareDtlData)
        {
            bool isTrue = false;
            string empCode = HttpContext.Current.Session["EMPLOYEE_CODE"].ToString();
            string empName = HttpContext.Current.Session["EMPLOYEE_NAME"].ToString();
            string empDesig = HttpContext.Current.Session["DESIGNATION"].ToString();
            using (OracleConnection con = new OracleConnection(dbConnection.SAConnStrReader("Sales")))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = con;
                    con.Open();
                    OracleTransaction trans = con.BeginTransaction();
                    cmd.Transaction = trans;
                    try
                    {
                        MaxID = expReqPrepareMstInfo.MstId.ToString();
                        MaxCode = expReqPrepareMstInfo.RequisitionNo;
                        IUMode = "U";
                        string qryMst =
                         "UPDATE  EXP_REQUISITION_MST SET VERIFIED_BY=:VerifiedBy,VERIFIED_DATE=:VerifiedDate,VERIFIED_REMARKS=:VerifiedRemarks, " +
                         " VERIFIED_STATUS=:VerifiedStatus,TOTAL_APPROVED_VALUE=:TotalApproveAmt WHERE  MST_ID=:MaxID";

                        OracleParameter[] paramMst = new OracleParameter[]
                        {
                            new OracleParameter("MaxID",Convert.ToInt32(MaxID)),
                            new OracleParameter("VerifiedBy",empCode),
                            new OracleParameter("VerifiedDate",DateTime.Now),
                            new OracleParameter("VerifiedRemarks", expReqPrepareMstInfo.VerifiedRemarks==null?"":expReqPrepareMstInfo.VerifiedRemarks),
                            new OracleParameter("VerifiedStatus", expReqPrepareMstInfo.VerifiedStatus),
                            new OracleParameter("TotalApproveAmt", expReqPrepareMstInfo.TotalApprovedAmt)
                        };
                        cmd.CommandText = qryMst;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddRange(paramMst);
                        int noOfRowsMst = cmd.ExecuteNonQuery();

                        if (noOfRowsMst > 0)
                        {
                            foreach (var data in expReqPrepareDtlData)
                            {
                                data.MstId = Convert.ToInt32(MaxID);
                                string qryDtl =
                                    "UPDATE  EXP_REQUISITION_DTL SET  VERIFIED_VALUE=:VerifiedValue WHERE DTL_ID=:DtlId";

                                OracleParameter[] paramDtl = new OracleParameter[]
                                {
                                    new OracleParameter("DtlId", data.DtlId),
                                    new OracleParameter("VerifiedValue","0")
                                };
                                if (expReqPrepareMstInfo.VerifiedStatus == "Approved")
                                {
                                    paramDtl = new OracleParameter[]
                                   {
                                    new OracleParameter("DtlId", data.DtlId),
                                    new OracleParameter("VerifiedValue", data.VerifiedValue)
                                   };
                                }
                                cmd.CommandText = qryDtl;
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddRange(paramDtl);
                                isTrue = cmd.ExecuteNonQuery() > 0;
                                if (!isTrue)
                                {
                                    trans.Rollback();
                                    break;
                                }
                            }



                            trans.Commit();


                            var Totamountqry = "select sum(nvl(VERIFIED_VALUE,0)) from EXP_REQUISITION_DTL WHERE  MST_ID=" + MaxID + "";
                            var Totamount = dbHelper.GetValue(dbConnection.SAConnStrReader("Sales"), Totamountqry);
                            string qryMst2 = "UPDATE  EXP_REQUISITION_MST SET  TOTAL_APPROVED_VALUE=" + Totamount + "  WHERE MST_ID=" + MaxID + "";
                            var A = dbHelper.CmdExecute(dbConnection.SAConnStrReader("Sales"), qryMst2);


                            expReqPrepareMstInfo.TotalApprovedAmt = Convert.ToInt32(Totamount);

                            if (expReqPrepareMstInfo.VerifiedStatus == "Approved")
                            {
                                //if (expReqPrepareMstInfo.TotalApprovedAmt > 10000)
                                // if (expReqPrepareMstInfo.TotalApprovedAmt > 50000 && expReqPrepareMstInfo.ReqTypeName!= "Adjustment")
                                if (expReqPrepareMstInfo.ReqTypeName == "Adjustment Miscell" || expReqPrepareMstInfo.ReqTypeName == "Bill Miscell" || expReqPrepareMstInfo.ReqTypeName == "Advance Miscell")
                                {
                                    if (expReqPrepareMstInfo.TotalApprovedAmt <= 10000)
                                    {
                                        expReqPrepareMstInfo.PrepareBy = dbHelper.GetValue(dbConnection.SAConnStrReader("Sales"), "SELECT PREPARED_BY FROM  EXP_REQUISITION_MST  WHERE REQUISITION_NO='" + expReqPrepareMstInfo.RequisitionNo + "'");
                                        var mailTo = dbHelper.GetValue(dbConnection.SAConnStrReader("Sales"), "SELECT EMAIL FROM  EMPLOYEE_INFO  WHERE EMPLOYEE_CODE='" + expReqPrepareMstInfo.PrepareBy + "'");
                                        string tag = string.Format(@"<a href='http://" + HttpContext.Current.Request.Url.Authority + ("/Requisition/RequisitionReport/frmRequisitionReport") + "'> Requisition Report</a>");
                                        var mailBody = "A Requisition <b><u>(" + expReqPrepareMstInfo.RequisitionNo + ")</u></b> has been approved.</br></br>" +
                         "<table border='1' style='border: 1px solid black;'>" +
                                         "<tbody>" +
                                             "<tr>" +
                                                 "<th>Access</th>" +
                                                 "<th>Name</th>" +
                                                 "<th>Designation</th>" +
                                                 "<th>Date</th>" +
                                             "</tr>" +
                                             "<tr>" +
                                                 "<td>Approved by</td>" +
                                                  "<td>[" + empCode + "]" + empName + "</td>" +
                                                 "<td>" + empDesig + "</td>" +
                                                 "<td>" + DateTime.Now.ToString("dd/MM/yyyy") + "</td>" +
                                             "</tr>" +
                                             "<tr>" +
                                                 "<td>Forwarded by</td>" +
                                                  "<td>" + expReqPrepareMstInfo.DivisionalName + "</td>" +
                                                 "<td>" + expReqPrepareMstInfo.DivisionalDesig + "</td>" +
                                                 "<td>" + expReqPrepareMstInfo.DivisionalDate + "</td>" +
                                             "</tr>" +
                                             "<tr>" +
                                                 "<td>Checked by</td>" +
                                                  "<td>" + expReqPrepareMstInfo.CheckedName + "</td>" +
                                                 "<td>" + expReqPrepareMstInfo.CheckedDesig + "</td>" +
                                                 "<td>" + expReqPrepareMstInfo.CheckedDate + "</td>" +
                                             "</tr>" +
                                             "<tr>" +
                                                 "<td>Prepared by</td>" +
                                                 "<td>" + expReqPrepareMstInfo.PrepareName + "</td>" +
                                                 "<td>" + expReqPrepareMstInfo.PrepareDesig + "</td>" +
                                                 "<td>" + expReqPrepareMstInfo.PrepareDate + "</td>" +
                                             "</tr>" +
                                         "</tbody>" +
                                    "</table>"
                                    + "</br>Total Amount: " + expReqPrepareMstInfo.TotalApprovedAmt
                                    + "</br>Click Here: " + tag;
                                        var mailSub = "Requisition Approved";
                                        _auditTrailDAO.InsertAudit("frmExpRequisitionApprove", "EXP_REQUISITION_MST", IUMode, MaxID, mailBody, mailSub, mailTo);
                                        return isTrue;
                                    }
                                }
                                if (expReqPrepareMstInfo.TotalApprovedAmt > 50000)
                                {
                                    string tag = string.Format(@"<a href='http://" + HttpContext.Current.Request.Url.Authority + ("/Requisition/ExpRequisitionRecommend/frmExpRequisitionRecommend") + "'> Requisition Recommend</a>");
                                    var mailBody = "A Requisition <b><u>(" + expReqPrepareMstInfo.RequisitionNo + ")</u></b> has been verified.</br></br>" +
                                        "<table border='1' style='border: 1px solid black;'>" +
                                                        "<tbody>" +
                                                            "<tr>" +
                                                                "<th>Access</th>" +
                                                                "<th>Name</th>" +
                                                                "<th>Designation</th>" +
                                                                "<th>Date</th>" +
                                                            "</tr>" +
                                                            "<tr>" +
                                                                "<td>Verified by</td>" +
                                                                 "<td>[" + empCode + "]" + empName + "</td>" +
                                                                "<td>" + empDesig + "</td>" +
                                                                "<td>" + DateTime.Now.ToString("dd/MM/yyyy") + "</td>" +
                                                            "</tr>" +
                                                            "<tr>" +
                                                                "<td>Forworded by</td>" +
                                                                "<td>" + expReqPrepareMstInfo.DivisionalName + "</td>" +
                                                                "<td>" + expReqPrepareMstInfo.DivisionalDesig + "</td>" +
                                                                "<td>" + expReqPrepareMstInfo.DivisionalDate + "</td>" +
                                                            "</tr>" +
                                                            "<tr>" +
                                                                "<td>Checked by</td>" +
                                                                 "<td>" + expReqPrepareMstInfo.CheckedName + "</td>" +
                                                                "<td>" + expReqPrepareMstInfo.CheckedDesig + "</td>" +
                                                                "<td>" + expReqPrepareMstInfo.CheckedDate + "</td>" +
                                                            "</tr>" +
                                                            "<tr>" +
                                                                "<td>Prepared by</td>" +
                                                                "<td>" + expReqPrepareMstInfo.PrepareName + "</td>" +
                                                                "<td>" + expReqPrepareMstInfo.PrepareDesig + "</td>" +
                                                                "<td>" + expReqPrepareMstInfo.PrepareDate + "</td>" +
                                                            "</tr>" +
                                                        "</tbody>" +
                                                   "</table>"
                                                   + "</br>Requisition Detail: " + expReqPrepareMstInfo.PrepareRemarks
                                                   + "</br>"
                                                   + "</br>Total Amount: " + expReqPrepareMstInfo.TotalApprovedAmt
                                                   + "</br>Click Here: " + tag;
                                    var mailSub = "Requisition For Recommend";
                                    _auditTrailDAO.InsertAudit("frmExpRequisitionVerify", "EXP_REQUISITION_MST", IUMode, MaxID, mailBody, mailSub);
                                }
                                // else if (expReqPrepareMstInfo.TotalApprovedAmt <= 10000)
                                // else if (expReqPrepareMstInfo.TotalApprovedAmt <= 50000 || expReqPrepareMstInfo.ReqTypeName == "Adjustment")
                               // else if (expReqPrepareMstInfo.TotalApprovedAmt <= 50000)
                                else
                                {
                                    expReqPrepareMstInfo.PrepareBy = dbHelper.GetValue(dbConnection.SAConnStrReader("Sales"), "SELECT PREPARED_BY FROM  EXP_REQUISITION_MST  WHERE REQUISITION_NO='" + expReqPrepareMstInfo.RequisitionNo + "'");
                                    var mailTo = dbHelper.GetValue(dbConnection.SAConnStrReader("Sales"), "SELECT EMAIL FROM  EMPLOYEE_INFO  WHERE EMPLOYEE_CODE='" + expReqPrepareMstInfo.PrepareBy + "'");
                                    string tag = string.Format(@"<a href='http://" + HttpContext.Current.Request.Url.Authority + ("/Requisition/RequisitionReport/frmRequisitionReport") + "'> Requisition Report</a>");
                                    var mailBody = "A Requisition <b><u>(" + expReqPrepareMstInfo.RequisitionNo + ")</u></b> has been approved.</br></br>" +
                     "<table border='1' style='border: 1px solid black;'>" +
                                     "<tbody>" +
                                         "<tr>" +
                                             "<th>Access</th>" +
                                             "<th>Name</th>" +
                                             "<th>Designation</th>" +
                                             "<th>Date</th>" +
                                         "</tr>" +
                                         "<tr>" +
                                             "<td>Approved by</td>" +
                                              "<td>[" + empCode + "]" + empName + "</td>" +
                                             "<td>" + empDesig + "</td>" +
                                             "<td>" + DateTime.Now.ToString("dd/MM/yyyy") + "</td>" +
                                         "</tr>" +
                                        "<tr>" +
                                            "<td>Forworded by</td>" +
                                            "<td>" + expReqPrepareMstInfo.DivisionalName + "</td>" +
                                            "<td>" + expReqPrepareMstInfo.DivisionalDesig + "</td>" +
                                            "<td>" + expReqPrepareMstInfo.DivisionalDate + "</td>" +
                                        "</tr>" +
                                         "<tr>" +
                                             "<td>Checked by</td>" +
                                              "<td>" + expReqPrepareMstInfo.CheckedName + "</td>" +
                                             "<td>" + expReqPrepareMstInfo.CheckedDesig + "</td>" +
                                             "<td>" + expReqPrepareMstInfo.CheckedDate + "</td>" +
                                         "</tr>" +
                                         "<tr>" +
                                             "<td>Prepared by</td>" +
                                             "<td>" + expReqPrepareMstInfo.PrepareName + "</td>" +
                                             "<td>" + expReqPrepareMstInfo.PrepareDesig + "</td>" +
                                             "<td>" + expReqPrepareMstInfo.PrepareDate + "</td>" +
                                         "</tr>" +
                                     "</tbody>" +
                                "</table>"
                                + "</br>Total Amount: " + expReqPrepareMstInfo.TotalApprovedAmt
                                + "</br>Click Here: " + tag;
                                    var mailSub = "Requisition Approved";
                                    _auditTrailDAO.InsertAudit("frmExpRequisitionApprove", "EXP_REQUISITION_MST", IUMode, MaxID, mailBody, mailSub, mailTo);
                                }

                                return isTrue;
                            }
                            _auditTrailDAO.InsertAudit("frmExpRequisitionApprove", "EXP_REQUISITION_MST", IUMode, MaxID);

                            return isTrue;
                        }
                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                }
            }

            return false;
        }

        internal bool UpdateExpReqVerifiedInfo(ExpReqPrepareMst expReqPrepareMstInfo, List<ExpReqPrepareDtl> expReqPrepareDtlData)
        {
            bool isTrue = false;
            string empCode = HttpContext.Current.Session["EMPLOYEE_CODE"].ToString();
            string empName = HttpContext.Current.Session["EMPLOYEE_NAME"].ToString();
            string empDesig = HttpContext.Current.Session["DESIGNATION"].ToString();
            using (OracleConnection con = new OracleConnection(dbConnection.SAConnStrReader("Sales")))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = con;
                    con.Open();
                    OracleTransaction trans = con.BeginTransaction();
                    cmd.Transaction = trans;
                    try
                    {
                        MaxID = expReqPrepareMstInfo.MstId.ToString();
                        MaxCode = expReqPrepareMstInfo.RequisitionNo;
                        expReqPrepareMstInfo.PrepareBy = empCode;
                        expReqPrepareMstInfo.RequisitionBy = empCode;
                        IUMode = "U";
                        string qryMst =
                         "UPDATE  EXP_REQUISITION_MST SET  VERIFIED_REMARKS=:VerifiedRemarks, VERIFIED_STATUS=:VerifiedStatus,TOTAL_APPROVED_VALUE=:TotalApproveAmt WHERE  MST_ID=:MaxID";

                        OracleParameter[] paramMst = new OracleParameter[]
                        {
                            new OracleParameter("MaxID",Convert.ToInt32(MaxID)),
                            new OracleParameter("VerifiedRemarks", expReqPrepareMstInfo.VerifiedRemarks==null?"":expReqPrepareMstInfo.VerifiedRemarks),
                            new OracleParameter("VerifiedStatus", expReqPrepareMstInfo.VerifiedStatus),
                            new OracleParameter("TotalApproveAmt", expReqPrepareMstInfo.TotalApprovedAmt)
                        };
                        cmd.CommandText = qryMst;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddRange(paramMst);
                        //cmd.BindByName = true;
                        int noOfRowsMst = cmd.ExecuteNonQuery();
                        if (noOfRowsMst > 0)
                        {
                            foreach (var data in expReqPrepareDtlData)
                            {
                                data.MstId = Convert.ToInt32(MaxID);
                                string qryDtl =
                                    "UPDATE  EXP_REQUISITION_DTL SET  VERIFIED_VALUE=:VerifiedValue WHERE DTL_ID=:DtlId";



                                OracleParameter[] paramDtl = new OracleParameter[]
                                {
                                    new OracleParameter("DtlId", data.DtlId),
                                    new OracleParameter("VerifiedValue","0")
                                };
                                if (expReqPrepareMstInfo.VerifiedStatus == "Approved")
                                {
                                    paramDtl = new OracleParameter[]
                                   {
                                    new OracleParameter("DtlId", data.DtlId),
                                    new OracleParameter("VerifiedValue", data.VerifiedValue)
                                   };
                                }
                                cmd.CommandText = qryDtl;
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddRange(paramDtl);
                                isTrue = cmd.ExecuteNonQuery() > 0;
                                if (!isTrue)
                                {
                                    trans.Rollback();
                                    break;
                                }
                            }

                            trans.Commit();

                            var Totamountqry = "select sum(nvl(VERIFIED_VALUE,0)) from EXP_REQUISITION_DTL WHERE  MST_ID=" + MaxID + "";
                            var Totamount = dbHelper.GetValue(dbConnection.SAConnStrReader("Sales"), Totamountqry);
                            string qryMst2 ="UPDATE  EXP_REQUISITION_MST SET  TOTAL_APPROVED_VALUE="+ Totamount + " WHERE MST_ID=" + MaxID + "";
                            var A = dbHelper.CmdExecute(dbConnection.SAConnStrReader("Sales"), qryMst2);





                            expReqPrepareMstInfo.TotalApprovedAmt = Convert.ToInt32(Totamount);


                            if (expReqPrepareMstInfo.VerifiedStatus == "Approved")
                            {
                                //if (expReqPrepareMstInfo.TotalApprovedAmt > 10000)
                                // if (expReqPrepareMstInfo.TotalApprovedAmt > 50000 && expReqPrepareMstInfo.ReqTypeName!= "Adjustment")
                                if (expReqPrepareMstInfo.ReqTypeName == "Adjustment Miscell" || expReqPrepareMstInfo.ReqTypeName == "Bill Miscell" || expReqPrepareMstInfo.ReqTypeName == "Advance Miscell")
                                {
                                    if (expReqPrepareMstInfo.TotalApprovedAmt <= 10000)
                                    {
                                        expReqPrepareMstInfo.PrepareBy = dbHelper.GetValue(dbConnection.SAConnStrReader("Sales"), "SELECT PREPARED_BY FROM  EXP_REQUISITION_MST  WHERE REQUISITION_NO='" + expReqPrepareMstInfo.RequisitionNo + "'");
                                        var mailTo = dbHelper.GetValue(dbConnection.SAConnStrReader("Sales"), "SELECT EMAIL FROM  EMPLOYEE_INFO  WHERE EMPLOYEE_CODE='" + expReqPrepareMstInfo.PrepareBy + "'");
                                        string tag = string.Format(@"<a href='http://" + HttpContext.Current.Request.Url.Authority + ("/Requisition/RequisitionReport/frmRequisitionReport") + "'> Requisition Report</a>");
                                        var mailBody = "A Requisition <b><u>(" + expReqPrepareMstInfo.RequisitionNo + ")</u></b> has been approved.</br></br>" +
                         "<table border='1' style='border: 1px solid black;'>" +
                                         "<tbody>" +
                                             "<tr>" +
                                                 "<th>Access</th>" +
                                                 "<th>Name</th>" +
                                                 "<th>Designation</th>" +
                                                 "<th>Date</th>" +
                                             "</tr>" +
                                             "<tr>" +
                                                 "<td>Approved by</td>" +
                                                  "<td>[" + empCode + "]" + empName + "</td>" +
                                                 "<td>" + empDesig + "</td>" +
                                                 "<td>" + DateTime.Now.ToString("dd/MM/yyyy") + "</td>" +
                                             "</tr>" +
                                             "<tr>" +
                                                 "<td> Forworded by</td>" +
                                                  "<td>" + expReqPrepareMstInfo.DivisionalName + "</td>" +
                                                 "<td>" + expReqPrepareMstInfo.DivisionalDesig + "</td>" +
                                                 "<td>" + expReqPrepareMstInfo.DivisionalDate + "</td>" +
                                             "</tr>" +
                                             "<tr>" +
                                                 "<td>Checked By</td>" +
                                                  "<td>" + expReqPrepareMstInfo.CheckedName + "</td>" +
                                                 "<td>" + expReqPrepareMstInfo.CheckedDesig + "</td>" +
                                                 "<td>" + expReqPrepareMstInfo.CheckedDate + "</td>" +
                                             "</tr>" +
                                             "<tr>" +
                                                 "<td>Prepared by</td>" +
                                                 "<td>" + expReqPrepareMstInfo.PrepareName + "</td>" +
                                                 "<td>" + expReqPrepareMstInfo.PrepareDesig + "</td>" +
                                                 "<td>" + expReqPrepareMstInfo.PrepareDate + "</td>" +
                                             "</tr>" +
                                         "</tbody>" +
                                    "</table>"
                                    + "</br>Total Amount: " + expReqPrepareMstInfo.TotalApprovedAmt
                                    + "</br>Click Here: " + tag;
                                        var mailSub = "Requisition Approved";
                                        _auditTrailDAO.InsertAudit("frmExpRequisitionApprove", "EXP_REQUISITION_MST", IUMode, MaxID, mailBody, mailSub, mailTo);
                                        return isTrue;
                                    }
                                }
                                if (expReqPrepareMstInfo.TotalApprovedAmt > 50000)
                                {
                                    string tag = string.Format(@"<a href='http://" + HttpContext.Current.Request.Url.Authority + ("/Requisition/ExpRequisitionRecommend/frmExpRequisitionRecommend") + "'> Requisition Recommend</a>");
                                    var mailBody = "A Requisition <b><u>(" + expReqPrepareMstInfo.RequisitionNo + ")</u></b> has been verified.</br></br>" +
                                        "<table border='1' style='border: 1px solid black;'>" +
                                                        "<tbody>" +
                                                            "<tr>" +
                                                                "<th>Access</th>" +
                                                                "<th>Name</th>" +
                                                                "<th>Designation</th>" +
                                                                "<th>Date</th>" +
                                                            "</tr>" +
                                                            "<tr>" +
                                                                "<td>Verified by</td>" +
                                                                 "<td>[" + empCode + "]" + empName + "</td>" +
                                                                "<td>" + empDesig + "</td>" +
                                                                "<td>" + DateTime.Now.ToString("dd/MM/yyyy") + "</td>" +
                                                            "</tr>" +
                                                             "<tr>" +
                                                                 "<td>Forwarded by</td>" +
                                                                  "<td>" + expReqPrepareMstInfo.DivisionalName + "</td>" +
                                                                 "<td>" + expReqPrepareMstInfo.DivisionalDesig + "</td>" +
                                                                 "<td>" + expReqPrepareMstInfo.DivisionalDate + "</td>" +
                                                             "</tr>" +
                                                            "<tr>" +
                                                                "<td>Checked by</td>" +
                                                                 "<td>" + expReqPrepareMstInfo.CheckedName + "</td>" +
                                                                "<td>" + expReqPrepareMstInfo.CheckedDesig + "</td>" +
                                                                "<td>" + expReqPrepareMstInfo.CheckedDate + "</td>" +
                                                            "</tr>" +
                                                            "<tr>" +
                                                                "<td>Prepared by</td>" +
                                                                "<td>" + expReqPrepareMstInfo.PrepareName + "</td>" +
                                                                "<td>" + expReqPrepareMstInfo.PrepareDesig + "</td>" +
                                                                "<td>" + expReqPrepareMstInfo.PrepareDate + "</td>" +
                                                            "</tr>" +
                                                        "</tbody>" +
                                                   "</table>"
                                                   + "</br>Requisition Detail: " + expReqPrepareMstInfo.PrepareRemarks
                                                   + "</br>"
                                                   + "</br>Total Amount: " + expReqPrepareMstInfo.TotalApprovedAmt
                                                   + "</br>Click Here: " + tag;
                                    var mailSub = "Requisition For Recommend";
                                    _auditTrailDAO.InsertAudit("frmExpRequisitionVerify", "EXP_REQUISITION_MST", IUMode, MaxID, mailBody, mailSub);
                                }
                                // else if (expReqPrepareMstInfo.TotalApprovedAmt <= 10000)
                                // else if (expReqPrepareMstInfo.TotalApprovedAmt <= 50000 || expReqPrepareMstInfo.ReqTypeName == "Adjustment")
                                // else if (expReqPrepareMstInfo.TotalApprovedAmt <= 50000)
                                else
                                {
                                    expReqPrepareMstInfo.PrepareBy = dbHelper.GetValue(dbConnection.SAConnStrReader("Sales"), "SELECT PREPARED_BY FROM  EXP_REQUISITION_MST  WHERE REQUISITION_NO='" + expReqPrepareMstInfo.RequisitionNo + "'");
                                    var mailTo = dbHelper.GetValue(dbConnection.SAConnStrReader("Sales"), "SELECT EMAIL FROM  EMPLOYEE_INFO  WHERE EMPLOYEE_CODE='" + expReqPrepareMstInfo.PrepareBy + "'");
                                    string tag = string.Format(@"<a href='http://" + HttpContext.Current.Request.Url.Authority + ("/Requisition/RequisitionReport/frmRequisitionReport") + "'> Requisition Report</a>");
                                    var mailBody = "A Requisition <b><u>(" + expReqPrepareMstInfo.RequisitionNo + ")</u></b> has been approved.</br></br>" +
                     "<table border='1' style='border: 1px solid black;'>" +
                                     "<tbody>" +
                                         "<tr>" +
                                             "<th>Access</th>" +
                                             "<th>Name</th>" +
                                             "<th>Designation</th>" +
                                             "<th>Date</th>" +
                                         "</tr>" +
                                         "<tr>" +
                                             "<td>Approved by</td>" +
                                              "<td>[" + empCode + "]" + empName + "</td>" +
                                             "<td>" + empDesig + "</td>" +
                                             "<td>" + DateTime.Now.ToString("dd/MM/yyyy") + "</td>" +
                                         "</tr>" +
                                             "<tr>" +
                                                 "<td>Forwarded by</td>" +
                                                  "<td>" + expReqPrepareMstInfo.DivisionalName + "</td>" +
                                                 "<td>" + expReqPrepareMstInfo.DivisionalDesig + "</td>" +
                                                 "<td>" + expReqPrepareMstInfo.DivisionalDate + "</td>" +
                                             "</tr>" +
                                         "<tr>" +
                                             "<td>Checked by</td>" +
                                              "<td>" + expReqPrepareMstInfo.CheckedName + "</td>" +
                                             "<td>" + expReqPrepareMstInfo.CheckedDesig + "</td>" +
                                             "<td>" + expReqPrepareMstInfo.CheckedDate + "</td>" +
                                         "</tr>" +
                                         "<tr>" +
                                             "<td>Prepared by</td>" +
                                             "<td>" + expReqPrepareMstInfo.PrepareName + "</td>" +
                                             "<td>" + expReqPrepareMstInfo.PrepareDesig + "</td>" +
                                             "<td>" + expReqPrepareMstInfo.PrepareDate + "</td>" +
                                         "</tr>" +
                                     "</tbody>" +
                                "</table>"
                                + "</br>Total Amount: " + expReqPrepareMstInfo.TotalApprovedAmt
                                + "</br>Click Here: " + tag;
                                    var mailSub = "Requisition Approved";
                                    _auditTrailDAO.InsertAudit("frmExpRequisitionApprove", "EXP_REQUISITION_MST", IUMode, MaxID, mailBody, mailSub, mailTo);
                                }

                                return isTrue;
                            }
                            _auditTrailDAO.InsertAudit("frmExpRequisitionApprove", "EXP_REQUISITION_MST", IUMode, MaxID);

                            return isTrue;
                        }
                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                }
            }
            return false;
        }

    }
}