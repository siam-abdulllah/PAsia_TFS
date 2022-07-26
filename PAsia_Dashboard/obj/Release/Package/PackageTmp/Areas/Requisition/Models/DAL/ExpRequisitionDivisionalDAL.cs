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
    public class ExpRequisitionDivisionalDAL : ReturnData
    {

        DBHelper dbHelper = new DBHelper();
        DBConnection dbConnection = new DBConnection();
        IDGenerated idGenerated = new IDGenerated();
        AuditTrailDAO _auditTrailDAO = new AuditTrailDAO();

        internal object GetExpReqMstList(string param)
        {
            try
            {
                string deptCode = System.Web.HttpContext.Current.Session["DEPARTMENT_CODE"].ToString();
                string empCode = HttpContext.Current.Session["EMPLOYEE_CODE"].ToString();
                string qry = "  SELECT DISTINCT MST_ID," +
                                "                  REQUISITION_NO," +
                                "                  REQUISITION_TYPE," +
                                "                  EXPENDITURE_MONTH," +
                                "                  PAY_TO_NAME," +
                                "                  PAY_TO_DESIG," +
                                "                  PAYMENT_PLACE," +
                                "                  PREPARED_BY_NAME," +
                                "                  PREPARED_BY_DESIG," +
                                "                  PREPARED_DATE," +
                                "                  PREPARED_REMARKS," +
                                "                  CHECKED_BY_NAME,"+
                                "                  CHECKED_BY_DESIG,"+
                                "                  NVL (CHECKED_STATUS, 'Pending') CHECKED_STATUS," +
                                "                  NVL (DIVISIONAL_STATUS, 'Pending') DIVISIONAL_STATUS," +
                                "                  NVL (VERIFIED_STATUS, 'Pending') VERIFIED_STATUS," +
                                "                  NVL (RECOMMENDED_STATUS, 'Pending') RECOMMENDED_STATUS," +
                                "                  NVL (APPROVED_STATUS, 'Pending') APPROVED_STATUS," +
                                "                  CHECKED_DATE," +
                                "                  VERIFIED_DATE," +
                                "                  RECOMMENDED_DATE," +
                                "                  APPROVED_DATE," +
                                "                  NVL (PREPARED_BY_CONFIRM, 'No') PREPARED_BY_CONFIRM" +
                                "    FROM VW_EXP_REQUISITION A" +
                                "         INNER JOIN SC_EMPLOYEE B ON A.PREPARED_BY = B.EMP_CODE" +
                                "   WHERE     1 = 1" +
                                "         AND B.DEPT_CODE IN ('19','23', '31', '32')" +
                                "         AND PREPARED_BY IS NOT NULL" +
                                "         AND PREPARED_BY_CONFIRM IS NOT NULL" +
                                "         AND PREPARED_BY_DEPARTMENT IN ('19', '23', '31', '32')" +
                                "         AND DIVISIONAL_STATUS IS NULL" +
                                "         AND VERIFIED_STATUS IS NULL" +
                                " UNION ALL " +
                                " SELECT DISTINCT MST_ID," +
                                "                  REQUISITION_NO," +
                                "                  REQUISITION_TYPE," +
                                "                  EXPENDITURE_MONTH," +
                                "                  PAY_TO_NAME," +
                                "                  PAY_TO_DESIG," +
                                "                  PAYMENT_PLACE," +
                                "                  PREPARED_BY_NAME," +
                                "                  PREPARED_BY_DESIG," +
                                "                  PREPARED_DATE," +
                                "                  PREPARED_REMARKS," +
                                "                  CHECKED_BY_NAME," +
                                "                  CHECKED_BY_DESIG," +
                                "                  NVL (CHECKED_STATUS, 'Pending') CHECKED_STATUS," +
                                "                  NVL (DIVISIONAL_STATUS, 'Pending') DIVISIONAL_STATUS," +
                                "                  NVL (VERIFIED_STATUS, 'Pending') VERIFIED_STATUS," +
                                "                  NVL (RECOMMENDED_STATUS, 'Pending') RECOMMENDED_STATUS," +
                                "                  NVL (APPROVED_STATUS, 'Pending') APPROVED_STATUS," +
                                "                  CHECKED_DATE," +
                                "                  VERIFIED_DATE," +
                                "                  RECOMMENDED_DATE," +
                                "                  APPROVED_DATE," +
                                "                  NVL (PREPARED_BY_CONFIRM, 'No') PREPARED_BY_CONFIRM" +
                                "    FROM VW_EXP_REQUISITION A" +
                                "         INNER JOIN SC_EMPLOYEE B ON A.PREPARED_BY = B.EMP_CODE" +
                                "   WHERE     1 = 1" +
                                "         AND B.DEPT_CODE IN ('22')" +
                                "         AND PREPARED_BY IS NOT NULL" +
                                "         AND PREPARED_BY_CONFIRM IS NOT NULL" +
                                "         AND PREPARED_BY_DEPARTMENT IN ('22')" +
                                "         AND DIVISIONAL_STATUS IS NULL" +
                                "         AND VERIFIED_STATUS IS NULL" +
                                "         AND CHECKED_STATUS='Approved'" +
                                " ORDER BY REQUISITION_NO DESC";
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
                                PrepareName = row["PREPARED_BY_NAME"].ToString(),
                                PrepareDesig = row["PREPARED_BY_DESIG"].ToString(),
                                PrepareDate = row["PREPARED_DATE"].ToString(),

                                CheckedDesig = row["CHECKED_BY_DESIG"].ToString(),
                                CheckedName = row["CHECKED_BY_NAME"].ToString(),

                                CheckedStatus = row["CHECKED_STATUS"].ToString(),
                                CheckedDate = row["CHECKED_DATE"].ToString(),

                                DivisionalStatus = row["DIVISIONAL_STATUS"].ToString(),

                                VerifiedStatus = row["VERIFIED_STATUS"].ToString(),
                                VerifiedDate = row["VERIFIED_DATE"].ToString(),
                                RecommendedStatus = row["RECOMMENDED_STATUS"].ToString(),
                                RecommendedDate = row["RECOMMENDED_DATE"].ToString(),
                                ApprovedStatus = row["APPROVED_STATUS"].ToString(),
                                ApprovedDate = row["APPROVED_DATE"].ToString(),
                                PrepareRemarks = row["PREPARED_REMARKS"].ToString(),
                                PreparedByConfirm = row["PREPARED_BY_CONFIRM"].ToString()
                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                throw;
            }
        }

        internal object GetExpDivisionalReqMstList(string param)
        {
            try
            {
                string deptCode = System.Web.HttpContext.Current.Session["DEPARTMENT_CODE"].ToString();
                string empCode = HttpContext.Current.Session["EMPLOYEE_CODE"].ToString();
                string qry = "SELECT  DISTINCT MST_ID,  REQUISITION_NO,  REQUISITION_TYPE,  EXPENDITURE_MONTH, " +
                    "  PAY_TO_NAME, PAY_TO_DESIG, PAYMENT_PLACE, " +
                    "  PREPARED_BY_NAME,PREPARED_BY_DESIG,  PREPARED_DATE, PREPARED_REMARKS,CHECKED_BY_NAME,CHECKED_BY_DESIG, " +
                    " NVL( CHECKED_STATUS,'Pending') CHECKED_STATUS,CHECKED_REMARKS,NVL( DIVISIONAL_STATUS,'Pending') DIVISIONAL_STATUS,NVL( VERIFIED_STATUS,'Pending') VERIFIED_STATUS,NVL( RECOMMENDED_STATUS,'Pending') RECOMMENDED_STATUS," +
                    " NVL( APPROVED_STATUS,'Pending') APPROVED_STATUS, CHECKED_DATE, DIVISIONAL_DATE, VERIFIED_DATE," +
                    " RECOMMENDED_DATE,APPROVED_DATE," +
                    " NVL( PREPARED_BY_CONFIRM,'No') PREPARED_BY_CONFIRM FROM VW_EXP_REQUISITION " +
                     " A INNER JOIN SC_EMPLOYEE B ON A.PREPARED_BY=B.EMP_CODE " +
                    " WHERE 1=1  AND B.DEPT_CODE IN ('19','22','23','31','32') AND PREPARED_BY_DEPARTMENT IN ('19','22','23','31','32') AND DIVISIONAL_STATUS IS NOT NULL  " + param + " " +
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

                                PrepareName = row["PREPARED_BY_NAME"].ToString(),
                                PrepareDesig = row["PREPARED_BY_DESIG"].ToString(),
                                PrepareDate = row["PREPARED_DATE"].ToString(),
                                PrepareRemarks = row["PREPARED_REMARKS"].ToString(),

                                CheckedName = row["CHECKED_BY_NAME"].ToString(),
                                CheckedDesig = row["CHECKED_BY_DESIG"].ToString(),


                                CheckedStatus = row["CHECKED_STATUS"].ToString(),
                                CheckedRemarks = row["CHECKED_REMARKS"].ToString(),
                                CheckedDate = row["CHECKED_DATE"].ToString(),

                                DivisionalStatus = row["DIVISIONAL_STATUS"].ToString(),
                                DivisionalDate = row["DIVISIONAL_DATE"].ToString(),

                                VerifiedStatus = row["VERIFIED_STATUS"].ToString(),
                                VerifiedDate = row["VERIFIED_DATE"].ToString(),
                                RecommendedStatus = row["RECOMMENDED_STATUS"].ToString(),
                                RecommendedDate = row["RECOMMENDED_DATE"].ToString(),
                                ApprovedStatus = row["APPROVED_STATUS"].ToString(),
                                ApprovedDate = row["APPROVED_DATE"].ToString(),
                                PreparedByConfirm = row["PREPARED_BY_CONFIRM"].ToString()
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
                string qry = "SELECT DTL_ID, MST_ID, MOP,NVL(PREPARED_VALUE,0)PREPARED_VALUE,NVL(CHECKED_VALUE,0) CHECKED_VALUE,NVL(DIVISIONAL_VALUE,0) DIVISIONAL_VALUE,PURPOSE, " +
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
                                DivisionalValue = Convert.ToInt32(row["CHECKED_VALUE"]) == 0 ? Convert.ToInt32(row["PREPARED_VALUE"]) : Convert.ToInt32(row["DIVISIONAL_VALUE"]),
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


        //-------------------- Insert ----------------------------//
        internal bool InsertExpReqDivisionalInfo(ExpReqPrepareMst expReqPrepareMstInfo, List<ExpReqPrepareDtl> expReqPrepareDtlData)
        {

            //if (expReqPrepareMstInfo.PreparedByConfirm != "Yes")
            //{
            //    ExceptionReturn = "Requistion No: " + expReqPrepareMstInfo.RequisitionNo + " is not submitted yet";
            //    return false;
            //}

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
                           "UPDATE  EXP_REQUISITION_MST SET DIVISIONAL_BY=:DivisionalBy,DIVISIONAL_DATE=:DivisionalDate,DIVISIONAL_REMARKS=:DivisionalRemarks, " +
                           " DIVISIONAL_STATUS=:DivisionalStatus,TOTAL_APPROVED_VALUE=:TotalApproveAmt WHERE  MST_ID=:MaxID";

                        OracleParameter[] paramMst = new OracleParameter[]
                        {
                            new OracleParameter("MaxID",Convert.ToInt32(MaxID)),
                            new OracleParameter("DivisionalBy",empCode),
                            new OracleParameter("DivisionalDate",DateTime.Now),
                            new OracleParameter("DivisionalRemarks", expReqPrepareMstInfo.DivisionalRemarks==null?"":expReqPrepareMstInfo.DivisionalRemarks),
                            new OracleParameter("DivisionalStatus", expReqPrepareMstInfo.DivisionalStatus),
                            new OracleParameter("TotalApproveAmt", expReqPrepareMstInfo.TotalApprovedAmt)
                        };


                        //if (expReqPrepareMstInfo.Status == "On Hold")
                        //{
                        //    qryMst =
                        //   "UPDATE  EXP_REQUISITION_MST SET CHECKED_BY=:CheckedBy,CHECKED_DATE=:CheckedDate,CHECKED_REMARKS=:CheckedRemarks, " +
                        //   " CHECKED_STATUS=:CheckedStatus,PREPARED_BY_CONFIRM=:PreparedByConfirm,TOTAL_APPROVED_VALUE=:TotalApproveAmt WHERE  MST_ID=:MaxID";

                        //    paramMst = new OracleParameter[]
                        //    {
                        //    new OracleParameter("MaxID",Convert.ToInt32(MaxID)),
                        //    new OracleParameter("CheckedBy",empCode),
                        //    new OracleParameter("CheckedDate",DateTime.Now),
                        //    new OracleParameter("CheckedRemarks", expReqPrepareMstInfo.CheckedRemarks==null?"":expReqPrepareMstInfo.CheckedRemarks),
                        //    new OracleParameter("CheckedStatus", expReqPrepareMstInfo.CheckedStatus),
                        //    new OracleParameter("PreparedByConfirm", ""),
                        //    new OracleParameter("TotalApproveAmt", expReqPrepareMstInfo.TotalApprovedAmt)
                        //    };
                        //}

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
                                    "UPDATE  EXP_REQUISITION_DTL SET  DIVISIONAL_VALUE=:DivisionalValue WHERE DTL_ID=:DtlId";
                                OracleParameter[] paramDtl = new OracleParameter[]
                                {
                                    new OracleParameter("DtlId", data.DtlId),
                                    new OracleParameter("DivisionalValue","0")
                                };
                                if (expReqPrepareMstInfo.DivisionalStatus == "Approved")
                                {
                                    paramDtl = new OracleParameter[]
                                   {
                                    new OracleParameter("DtlId", data.DtlId),
                                    new OracleParameter("DivisionalValue", data.DivisionalValue)
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

                            if (expReqPrepareMstInfo.DivisionalStatus == "Approved")
                            {
                                string tag = string.Format(@"<a href='http://" + HttpContext.Current.Request.Url.Authority + ("/Requisition/ExpRequisitionVerify/frmExpRequisitionVerify") + "'> Requisition Verify</a>");
                                var mailBody = "A Requisition <b><u>(" + expReqPrepareMstInfo.RequisitionNo + ")</u></b> has been checked.</br></br>" +
                                    "<table border='1' style='border: 1px solid black;'>" +
                                                    "<tbody>" +
                                                        "<tr>" +
                                                            "<th>Access</th>" +
                                                            "<th>Name</th>" +
                                                            "<th>Designation</th>" +
                                                            "<th>Date</th>" +
                                                        "</tr>" +
                                                        "<tr>" +
                                                            "<td>Forwarded by</td>" +
                                                             "<td>[" + empCode + "]" + empName + "</td>" +
                                                            "<td>" + empDesig + "</td>" +
                                                            "<td>" + DateTime.Now.ToString("dd/MM/yyyy") + "</td>" +
                                                        "</tr>                                            " +
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

                                //var mailBody = "A Requisition (" + expReqPrepareMstInfo.RequisitionNo + ") has been generated." +
                                //    "</br>Preapared By:"+expReqPrepareMstInfo.PrepareName
                                //      + "</br>Designation: " + expReqPrepareMstInfo.PrepareDesig
                                //     + "</br>Total Amount: " + expReqPrepareMstInfo.TotalApprovedAmt
                                //      + "</br>Prepare Date: " + expReqPrepareMstInfo.PrepareDate
                                //      + "</br>Click Here: " + tag;
                                //+ Environment.NewLine + Environment.NewLine;
                                var mailSub = "Requisition For Verify";
                                //var mailTo = "maaz@squaregroup.com";
                                _auditTrailDAO.InsertAudit("frmExpRequisitionDivisional", "EXP_REQUISITION_MST", IUMode, MaxID, mailBody, mailSub);
                                return isTrue;
                            }
                            _auditTrailDAO.InsertAudit("frmExpRequisitionDivisional", "EXP_REQUISITION_MST", IUMode, MaxID);
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

        internal bool UpdateExpReqDivisionalInfo(ExpReqPrepareMst expReqPrepareMstInfo, List<ExpReqPrepareDtl> expReqPrepareDtlData)
        {
            //if (expReqPrepareMstInfo.PreparedByConfirm != "Yes")
            //{
            //    ExceptionReturn = "Requistion No: " + expReqPrepareMstInfo.RequisitionNo + " is not submitted yet";
            //    return false;
            //}
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
                         "UPDATE  EXP_REQUISITION_MST SET  DIVISIONAL_REMARKS=:DivisionalRemarks, DIVISIONAL_STATUS=:DivisionalStatus,TOTAL_APPROVED_VALUE=:TotalApproveAmt WHERE  MST_ID=:MaxID";

                        OracleParameter[] paramMst = new OracleParameter[]
                        {
                            new OracleParameter("MaxID",Convert.ToInt32(MaxID)),
                            new OracleParameter("DivisionalRemarks", expReqPrepareMstInfo.DivisionalRemarks==null?"":expReqPrepareMstInfo.DivisionalRemarks),
                            new OracleParameter("DivisionalStatus", expReqPrepareMstInfo.DivisionalStatus),
                            new OracleParameter("TotalApproveAmt", expReqPrepareMstInfo.TotalApprovedAmt)
                        };


                        //if (expReqPrepareMstInfo.CheckedStatus == "On Hold")
                        //{
                        //    qryMst =
                        //   "UPDATE  EXP_REQUISITION_MST SET CHECKED_REMARKS=:CheckedRemarks, " +
                        //   " CHECKED_STATUS=:CheckedStatus,PREPARED_BY_CONFIRM=:PreparedByConfirm WHERE  MST_ID=:MaxID";

                        //    paramMst = new OracleParameter[]
                        //    {
                        //    new OracleParameter("MaxID",Convert.ToInt32(MaxID)),
                        //    new OracleParameter("CheckedRemarks", expReqPrepareMstInfo.CheckedRemarks==null?"":expReqPrepareMstInfo.CheckedRemarks),
                        //    new OracleParameter("CheckedStatus", expReqPrepareMstInfo.CheckedStatus),
                        //    new OracleParameter("PreparedByConfirm", "")
                        //    };
                        //}


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
                                    "UPDATE  EXP_REQUISITION_DTL SET  DIVISIONAL_VALUE=:DivisionalValue WHERE DTL_ID=:DtlId";

                                OracleParameter[] paramDtl = new OracleParameter[]
                                    {
                                    new OracleParameter("DtlId", data.DtlId),
                                    new OracleParameter("DivisionalValue", "0")
                                    };
                                if (expReqPrepareMstInfo.DivisionalStatus == "Approved")
                                {
                                    paramDtl = new OracleParameter[]
                                   {
                                    new OracleParameter("DtlId", data.DtlId),
                                    new OracleParameter("DivisionalValue", data.DivisionalValue)
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

                            if (expReqPrepareMstInfo.DivisionalStatus == "Approved")
                            {
                                string tag = string.Format(@"<a href='http://" + HttpContext.Current.Request.Url.Authority + ("/Requisition/ExpRequisitionVerify/frmExpRequisitionVerify") + "'> Requisition Verify</a>");
                                var mailBody = "A Requisition <b><u>(" + expReqPrepareMstInfo.RequisitionNo + ")</u></b> has been checked.</br></br>" +
                                    "<table border='1' style='border: 1px solid black;'>" +
                                                    "<tbody>" +
                                                        "<tr>" +
                                                            "<th>Access</th>" +
                                                            "<th>Name</th>" +
                                                            "<th>Designation</th>" +
                                                            "<th>Date</th>" +
                                                        "</tr>" +
                                                        "<tr>" +
                                                            "<td>Forwarded by</td>" +
                                                             "<td>[" + empCode + "]" + empName + "</td>" +
                                                            "<td>" + empDesig + "</td>" +
                                                            "<td>" + DateTime.Now.ToString("dd/MM/yyyy") + "</td>" +
                                                        "</tr>                                            " +

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
                                var mailSub = "Requisition For Verify";
                                _auditTrailDAO.InsertAudit("frmExpRequisitionDivisional", "EXP_REQUISITION_MST", IUMode, MaxID, mailBody, mailSub);
                                return isTrue;
                            }
                            _auditTrailDAO.InsertAudit("frmExpRequisitionDivisional", "EXP_REQUISITION_MST", IUMode, MaxID);
                            return isTrue;
                        }
                        return isTrue;
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