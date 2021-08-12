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
    public class ExpRequisitionPrepareDAL : ReturnData
    {
        DBHelper dbHelper = new DBHelper();
        DBConnection dbConnection = new DBConnection();
        IDGenerated idGenerated = new IDGenerated();
        AuditTrailDAO _auditTrailDAO = new AuditTrailDAO();
        //-------Requisition Type ------//

        public object GetRequisitionType(string param)
        {
            try
            {
                string qry = "SELECT TYPE_NAME FROM EXP_REQUISITION_TYPE WHERE STATUS='A' ORDER BY TYPE_NAME";
                DataTable dt = dbHelper.GetDataTable(dbConnection.SAConnStrReader("Sales"), qry);
                var item = (from DataRow row in dt.Rows
                            select new ExpRequisitionPrepareBEL.ExpReqPrepareMst
                            {
                                ReqTypeName = row["TYPE_NAME"].ToString()
                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                throw;
            }
        }

        internal object GetExpReqMstList(string param)
        {
            try
            {
                string empCode = HttpContext.Current.Session["EMPLOYEE_CODE"].ToString();
                string qry = "SELECT A.MST_ID, A.REQUISITION_NO, TO_CHAR(A.REQUISITION_DATE,'dd/MM/YYYY') REQUISITION_DATE,A.REQUISITION_TYPE, A.EXPENDITURE_MONTH, " +
                    " A.PAY_TO,B.EMPLOYEE_NAME,C.DESIG_NAME,A.PAYMENT_PLACE, " +
                    " A.PREPARED_BY, TO_CHAR(A.PREPARED_DATE,'dd/MM/YYYY') PREPARED_DATE,A.PREPARED_REMARKS," +
                    " NVL(A.CHECKED_STATUS,'Pending') CHECKED_STATUS,NVL(A.VERIFIED_STATUS,'Pending') VERIFIED_STATUS,NVL(A.RECOMMENDED_STATUS,'Pending') RECOMMENDED_STATUS," +
                    " NVL(A.APPROVED_STATUS,'Pending') APPROVED_STATUS,TO_CHAR(A.CHECKED_DATE,'dd/MM/YYYY') CHECKED_DATE,TO_CHAR(A.VERIFIED_DATE,'dd/MM/YYYY') VERIFIED_DATE," +
                    " TO_CHAR(A.RECOMMENDED_DATE,'dd/MM/YYYY') RECOMMENDED_DATE,TO_CHAR(A.APPROVED_DATE,'dd/MM/YYYY') APPROVED_DATE," +
                    " NVL(A.PREPARED_BY_CONFIRM,'No') PREPARED_BY_CONFIRM FROM EXP_REQUISITION_MST A INNER JOIN EMPLOYEE_INFO B " +
                    " ON A.PAY_TO=B.EMPLOYEE_CODE INNER JOIN SC_DESIGNATION C ON B.DESIGNATION=C.DESIG_CODE WHERE 1=1 AND A.PREPARED_BY=" + empCode + " " + param + " " +
                    " ORDER BY    REQUISITION_NO DESC";
                DataTable dt = dbHelper.GetDataTable(dbConnection.SAConnStrReader("Sales"), qry);
                var item = (from DataRow row in dt.Rows
                            select new ExpReqPrepareMst
                            {
                                MstId = Convert.ToInt32(row["MST_ID"]),
                                RequisitionNo = row["REQUISITION_NO"].ToString(),
                                RequisitionDate = row["REQUISITION_DATE"].ToString(),
                                ReqTypeName = row["REQUISITION_TYPE"].ToString(),
                                ExpenditureMonth = row["EXPENDITURE_MONTH"].ToString(),
                                PayToCode = row["PAY_TO"].ToString(),
                                PayToName = row["EMPLOYEE_NAME"].ToString(),
                                PayToDesig = row["DESIG_NAME"].ToString(),
                                PaymentPlace = row["PAYMENT_PLACE"].ToString(),
                                PrepareBy = row["PREPARED_BY"].ToString(),
                                PrepareDate = row["PREPARED_DATE"].ToString(),
                                CheckedStatus = row["CHECKED_STATUS"].ToString(),
                                CheckedDate = row["CHECKED_DATE"].ToString(),
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
        internal object GetExpReqDtlList(string mstId)
        {
            try
            {
                string qry = "SELECT DTL_ID, MST_ID, MOP,PREPARED_VALUE,PURPOSE, " +
                    " TO_CHAR(FROM_DATE,'dd/MM/YYYY') FROM_DATE, TO_CHAR(TO_DATE,'dd/MM/YYYY') TO_DATE,TO_CHAR(REQUIRED_DATE,'dd/MM/YYYY') REQUIRED_DATE,REMARKS, TOTAL_DAYS FROM EXP_REQUISITION_DTL WHERE MST_ID=" + mstId;
                DataTable dt = dbHelper.GetDataTable(dbConnection.SAConnStrReader("Sales"), qry);
                var item = (from DataRow row in dt.Rows
                            select new ExpReqPrepareDtl
                            {
                                MstId = Convert.ToInt32(row["MST_ID"]),
                                DtlId = Convert.ToInt32(row["DTL_ID"]),
                                Mop = row["MOP"].ToString(),
                                PrepareValue = Convert.ToInt32(row["PREPARED_VALUE"]),
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
        internal bool DeleteExpReqDtl(string dtlId)
        {
            string mstQry = "DELETE from EXP_REQUISITION_DTL where DTL_ID='" + dtlId + "'";
            if (dbHelper.CmdExecute(dbConnection.SAConnStrReader("Dashboard"), mstQry))
            {
                return true;
            }
            return false;
        }
        internal bool DeleteExpReqMst(string mstId)
        {
            string mstQry = "DELETE from EXP_REQUISITION_MST where MST_ID='" + mstId + "'";
            if (dbHelper.CmdExecute(dbConnection.SAConnStrReader("Dashboard"), mstQry))
            {
                return true;
            }
            return false;
        }
        //-------Pay To ------//
        public object GetPayTo(string param)
        {
            try
            {
                string qry = "SELECT A.EMPLOYEE_NAME,A.EMPLOYEE_CODE,B.DESIG_NAME FROM EMPLOYEE_INFO A,SC_DESIGNATION B WHERE A.DESIGNATION=B.DESIG_CODE AND A.STATUS='A' " +
                    " AND A.POSTING_LOCATION IN ('D','H','F','R','Z') ORDER BY A.EMPLOYEE_NAME";
                DataTable dt = dbHelper.GetDataTable(dbConnection.SAConnStrReader("Sales"), qry);
                var item = (from DataRow row in dt.Rows
                            select new ExpReqPrepareMst
                            {
                                PayToName = row["EMPLOYEE_NAME"].ToString(),
                                PayToCode = row["EMPLOYEE_CODE"].ToString(),
                                PayToDesig = row["DESIG_NAME"].ToString()
                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                throw;
            }
        }
        //-------------------- Insert ----------------------------//
        internal bool InsertExpReqPrepareInfo(ExpReqPrepareMst expReqPrepareMstInfo, List<ExpReqPrepareDtl> expReqPrepareDtlData)
        {
            bool isTrue = false;
            int DtlCode = 0;
            string empCode = HttpContext.Current.Session["EMPLOYEE_CODE"].ToString();
            long id = 0;
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
                        MaxID = idGenerated.getMAXSL("EXP_REQUISITION_MST", "MST_ID", dbConnection.SAConnStrReader("Sales")).ToString();
                        //MaxCode = dbHelper.GetNo("EXP_REQUISITION_MST", "REQUISITION_NO");
                        //string qry = "SELECT  NVL(COUNT(MST_ID),0)+1 id FROM EXP_REQUISITION_MST WHERE TO_CHAR(REQUISITION_DATE,'YYYYYMM')=TO_CHAR(SYSDATE,'YYYYYMM')";
                        string qry = "SELECT  TO_CHAR(SYSDATE,'YYYY')||TO_CHAR(SYSDATE,'MM')||LPAD(TO_CHAR(NVL(MAX(TO_NUMBER(SUBSTR(REQUISITION_NO,7,5))),0)+1),5,0) id " +
                            " FROM EXP_REQUISITION_MST WHERE SUBSTR(REQUISITION_NO,1,4)= TO_CHAR(SYSDATE, 'YYYY') AND SUBSTR(REQUISITION_NO,5,2)= TO_CHAR(SYSDATE, 'MM') ";
                        var _dt = dbHelper.GetDataTable(dbConnection.SAConnStrReader("Dashboard"), qry);
                        //if (_dt.Rows.Count > 0)
                        //{
                        //    var _row = _dt.Rows[0];

                        //    id = Convert.ToInt32(_row["id"]);
                        //}
                        var _row = _dt.Rows[0];
                        MaxCode = _row["id"].ToString();
                        //MaxCode = DateTime.Now.ToString("yyyyMM") + id.ToString("00000");
                        expReqPrepareMstInfo.PrepareBy = empCode;
                        expReqPrepareMstInfo.RequisitionBy = empCode;
                        IUMode = "I";
                        DateTime? requisitionDate = string.IsNullOrEmpty(expReqPrepareMstInfo.RequisitionDate) ? (DateTime?)null : DateTime.ParseExact(expReqPrepareMstInfo.RequisitionDate,
                                    "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        var prepareDate = DateTime.Now;
                        string qryMst =
                         "INSERT INTO EXP_REQUISITION_MST (MST_ID,REQUISITION_NO,REQUISITION_DATE,REQUISITION_TYPE,EXPENDITURE_MONTH, PAY_TO, " +
                         " PAYMENT_PLACE, PREPARED_BY, PREPARED_DATE,PREPARED_REMARKS,TOTAL_APPROVED_VALUE) " +
                         " Values (:MaxID,:MaxCode,:RequisitionDate,:ReqTypeName,:ExpenditureMonth,:PayTo," +
                         " :PaymentPlace,:PrepareBy,:PrepareDate,:PrepareRemarks,:TotalApproveAmt)";

                        OracleParameter[] paramMst = new OracleParameter[]
                        {
                            new OracleParameter("MaxID",Convert.ToInt32(MaxID)),
                            new OracleParameter("MaxCode",MaxCode),
                            new OracleParameter("RequisitionDate",requisitionDate),
                            new OracleParameter("ReqTypeName",expReqPrepareMstInfo.ReqTypeName),
                            new OracleParameter("ExpenditureMonth",expReqPrepareMstInfo.ExpenditureMonth),
                            new OracleParameter("PayTo", expReqPrepareMstInfo.PayToCode),
                            new OracleParameter("PaymentPlace", expReqPrepareMstInfo.PaymentPlace),
                            new OracleParameter("PrepareBy", expReqPrepareMstInfo.PrepareBy),
                            new OracleParameter("PrepareDate", prepareDate),
                            new OracleParameter("PrepareRemarks", expReqPrepareMstInfo.PrepareRemarks==null?"":expReqPrepareMstInfo.PrepareRemarks),
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
                                data.DtlId = Convert.ToInt32(idGenerated.getMAXSL("EXP_REQUISITION_DTL", "DTL_ID", dbConnection.SAConnStrReader("Sales"))) + DtlCode;
                                data.MstId = Convert.ToInt32(MaxID);
                                DateTime? fromDate = string.IsNullOrEmpty(data.FromDate) ? (DateTime?)null : DateTime.ParseExact(data.FromDate,
                                  "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                DateTime? toDate = string.IsNullOrEmpty(data.ToDate) ? (DateTime?)null : DateTime.ParseExact(data.ToDate,
                                            "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                DateTime? requiredDate = string.IsNullOrEmpty(data.RequiredDate) ? (DateTime?)null : DateTime.ParseExact(data.RequiredDate,
                                            "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                DateTime enteredDateDtl = DateTime.Now;
                                string qryDtl =
                                    "INSERT INTO EXP_REQUISITION_DTL (DTL_ID,MST_ID, MOP,PREPARED_VALUE,PURPOSE,FROM_DATE,TO_DATE,REQUIRED_DATE,REMARKS,TOTAL_DAYS)" +
                                                           " Values (:DtlId,:MstId,:Mop,:PrepareValue,:Purpose,:FromDate,:ToDate,:RequiredDate,:Remarks,:TotalDays)";

                                OracleParameter[] paramDtl = new OracleParameter[]
                                {
                                    new OracleParameter("DtlId", data.DtlId),
                                    new OracleParameter("MstId", data.MstId),
                                    new OracleParameter("Mop", data.Mop),
                                    new OracleParameter("PrepareValue", data.PrepareValue),
                                    new OracleParameter("Purpose", data.Purpose),
                                    new OracleParameter("FromDate", fromDate),
                                    new OracleParameter("ToDate", toDate),
                                    new OracleParameter("RequiredDate", requiredDate),
                                    new OracleParameter("Remarks", data.Remarks==null?"":data.Remarks),
                                    new OracleParameter("TotalDays", data.TotalDays==null?"":data.TotalDays),
                                };
                                cmd.CommandText = qryDtl;
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddRange(paramDtl);
                                isTrue = cmd.ExecuteNonQuery() > 0;
                                if (!isTrue)
                                {
                                    trans.Rollback();
                                    break;
                                }
                                DtlCode += 1;
                            }
                            trans.Commit();
                            _auditTrailDAO.InsertAudit("frmExpRequisitionPrepare", "EXP_REQUISITION_MST", IUMode, MaxID);
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

        internal bool UpdateExpReqPrepareInfo(ExpReqPrepareMst expReqPrepareMstInfo, List<ExpReqPrepareDtl> expReqPrepareDtlData)
        {
            bool isTrue = false;
            int DtlCode = 0;
            string empCode = HttpContext.Current.Session["EMPLOYEE_CODE"].ToString();
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
                         "UPDATE  EXP_REQUISITION_MST SET REQUISITION_TYPE=:ReqTypeName,EXPENDITURE_MONTH=:ExpenditureMonth, PAY_TO=:PayTo, " +
                         " PAYMENT_PLACE=:PaymentPlace, PREPARED_REMARKS=:PrepareRemarks,TOTAL_APPROVED_VALUE=:TotalApproveAmt WHERE  MST_ID=:MaxID";

                        OracleParameter[] paramMst = new OracleParameter[]
                        {
                            new OracleParameter("MaxID",Convert.ToInt32(MaxID)),
                            new OracleParameter("ReqTypeName",expReqPrepareMstInfo.ReqTypeName),
                            new OracleParameter("ExpenditureMonth",expReqPrepareMstInfo.ExpenditureMonth),
                            new OracleParameter("PayTo", expReqPrepareMstInfo.PayToCode),
                            new OracleParameter("PaymentPlace", expReqPrepareMstInfo.PaymentPlace),
                            new OracleParameter("PrepareRemarks", expReqPrepareMstInfo.PrepareRemarks==null?"":expReqPrepareMstInfo.PrepareRemarks),
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
                                if (data.DtlId == 0)
                                {
                                    data.DtlId = Convert.ToInt32(idGenerated.getMAXSL("EXP_REQUISITION_DTL", "DTL_ID", dbConnection.SAConnStrReader("Sales"))) + DtlCode;
                                    data.MstId = Convert.ToInt32(MaxID);
                                    DateTime? fromDate = string.IsNullOrEmpty(data.FromDate) ? (DateTime?)null : DateTime.ParseExact(data.FromDate,
                                      "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                    DateTime? toDate = string.IsNullOrEmpty(data.ToDate) ? (DateTime?)null : DateTime.ParseExact(data.ToDate,
                                                "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                    DateTime? requiredDate = string.IsNullOrEmpty(data.RequiredDate) ? (DateTime?)null : DateTime.ParseExact(data.RequiredDate,
                                           "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                    DateTime enteredDateDtl = DateTime.Now;
                                    string qryDtl =
                                    "INSERT INTO EXP_REQUISITION_DTL (DTL_ID,MST_ID, MOP,PREPARED_VALUE,PURPOSE,FROM_DATE,TO_DATE,REQUIRED_DATE,REMARKS,TOTAL_DAYS)" +
                                                           " Values (:DtlId,:MstId,:Mop,:PrepareValue,:Purpose,:FromDate,:ToDate,:RequiredDate,:Remarks,:TotalDays)";

                                    OracleParameter[] paramDtl = new OracleParameter[]
                                    {
                                    new OracleParameter("DtlId", data.DtlId),
                                    new OracleParameter("MstId", data.MstId),
                                    new OracleParameter("Mop", data.Mop),
                                    new OracleParameter("PrepareValue", data.PrepareValue),
                                    new OracleParameter("Purpose", data.Purpose),
                                    new OracleParameter("FromDate", fromDate),
                                    new OracleParameter("ToDate", toDate),
                                    new OracleParameter("RequiredDate", requiredDate),
                                    new OracleParameter("Remarks", data.Remarks==null?"":data.Remarks),
                                    new OracleParameter("TotalDays", data.TotalDays==null?"":data.TotalDays),
                                    };
                                    cmd.CommandText = qryDtl;
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddRange(paramDtl);
                                    isTrue = cmd.ExecuteNonQuery() > 0;
                                    if (!isTrue)
                                    {
                                        trans.Rollback();
                                        break;
                                    }
                                    DtlCode += 1;
                                }
                                else
                                {

                                    data.MstId = Convert.ToInt32(MaxID);
                                    DateTime? fromDate = string.IsNullOrEmpty(data.FromDate) ? (DateTime?)null : DateTime.ParseExact(data.FromDate,
                                      "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                    DateTime? toDate = string.IsNullOrEmpty(data.ToDate) ? (DateTime?)null : DateTime.ParseExact(data.ToDate,
                                                "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                    DateTime? requiredDate = string.IsNullOrEmpty(data.RequiredDate) ? (DateTime?)null : DateTime.ParseExact(data.RequiredDate,
                                         "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                    DateTime enteredDateDtl = DateTime.Now;
                                    string qryDtl =
                                        "UPDATE  EXP_REQUISITION_DTL SET  MOP=:Mop,PREPARED_VALUE=:PrepareValue,PURPOSE=:Purpose,FROM_DATE=:FromDate," +
                                        " TO_DATE=:ToDate,REQUIRED_DATE=:RequiredDate, REMARKS=:Remarks,TOTAL_DAYS=:TotalDays WHERE DTL_ID=:DtlId";

                                    OracleParameter[] paramDtl = new OracleParameter[]
                                    {
                                    new OracleParameter("DtlId", data.DtlId),
                                    new OracleParameter("Mop", data.Mop),
                                    new OracleParameter("PrepareValue", data.PrepareValue),
                                    new OracleParameter("Purpose", data.Purpose),
                                    new OracleParameter("FromDate", fromDate),
                                    new OracleParameter("ToDate", toDate),
                                    new OracleParameter("RequiredDate", requiredDate),
                                    new OracleParameter("Remarks", data.Remarks==null?"":data.Remarks),
                                    new OracleParameter("TotalDays", data.TotalDays==null?"":data.TotalDays),
                                    };
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
                            }
                            trans.Commit();
                            _auditTrailDAO.InsertAudit("frmExpRequisitionPrepare", "EXP_REQUISITION_MST", IUMode, MaxID);
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


        internal bool SubmitExpReqPrepareInfo(ExpReqPrepareMst expReqPrepareMstInfo)
        {
            // string empCode = HttpContext.Current.Session["EMPLOYEE_CODE"].ToString();
            using (OracleConnection con = new OracleConnection(dbConnection.SAConnStrReader("Sales")))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    string empCode = HttpContext.Current.Session["EMPLOYEE_CODE"].ToString();
                    string empName = HttpContext.Current.Session["EMPLOYEE_NAME"].ToString();
                    string empDesig = HttpContext.Current.Session["DESIGNATION"].ToString();
                    cmd.Connection = con;
                    con.Open();
                    OracleTransaction trans = con.BeginTransaction();
                    cmd.Transaction = trans;
                    try
                    {
                        IUMode = "S";
                        string qryMst =
                         "UPDATE  EXP_REQUISITION_MST SET PREPARED_BY_CONFIRM=:PreapredByConfirm WHERE  MST_ID=:MstId";
                        OracleParameter[] paramMst = new OracleParameter[]
                        {
                            new OracleParameter("MstId",expReqPrepareMstInfo.MstId),
                            new OracleParameter("PreapredByConfirm","Yes")
                        };
                        cmd.CommandText = qryMst;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddRange(paramMst);
                        //cmd.BindByName = true;
                        int noOfRowsMst = cmd.ExecuteNonQuery();
                        if (noOfRowsMst > 0)
                        {
                            trans.Commit();
                            string tag = string.Format(@"<a href='http://"+HttpContext.Current.Request.Url.Authority+("/Requisition/ExpRequisitionCheck/frmExpRequisitionCheck")+"'>Requisition Check</a>");
                            var mailBody = "A Requisition <b><u>(" + expReqPrepareMstInfo.RequisitionNo + ")</u></b> has been generated.</br></br>" +
                                "<table border='1' style='border: 1px solid black;'>" +
                                                    "<tbody>" +
                                                        "<tr>" +
                                                            "<th>Access</th>" +
                                                            "<th>Name</th>" +
                                                            "<th>Designation</th>" +
                                                            "<th>Date</th>" +
                                                        "</tr>" +
                                                        "<tr>" +
                                                            "<td>Prepared By</td>" +
                                                            "<td>[" + empCode + "] " + empName+ "</td>" +
                                                            "<td>" + empDesig + "</td>" +
                                                            "<td>" + expReqPrepareMstInfo.PrepareDate + "</td>" +
                                                        "</tr>" +
                                                    "</tbody>" +
                                               "</table>" 
                                               + "</br>Requisition Detail: " + expReqPrepareMstInfo.PrepareRemarks
                                               + "</br>"
                                               + "</br>Total Amount: " + expReqPrepareMstInfo.TotalApprovedAmt
                                               + "</br>Click Here: " + tag;
                            //"</br>Preapared By: [" + empCode + "] " + empName
                            //  //+ Environment.NewLine + Environment.NewLine +
                            //  +"</br>Designation: " + empDesig
                            //  //+ Environment.NewLine + Environment.NewLine +
                            // + "</br>Total Amount: " + expReqPrepareMstInfo.TotalApprovedAmt
                            //  //+ Environment.NewLine + Environment.NewLine +
                            //  +"</br>Prepare Date: " + expReqPrepareMstInfo.PrepareDate
                            //  //+ Environment.NewLine + Environment.NewLine
                            //  //+"Click Here: " + <asp:HyperLink id = "hyperlink1" ImageUrl = "images/pict.jpg" NavigateUrl = "http://www.microsoft.com"Text = "Microsoft Official Site" Target = "_new" runat = "server" />
                            //  //+"Click Here: " + "<a href='http://202.84.32.118:8998/Requisition/ExpRequisitionCheck/frmExpRequisitionCheck'>Requisition Check</a>"
                            //  + "</br>Click Here: " + tag;
                            //  //+ Environment.NewLine + Environment.NewLine;
                            var mailSub = "Requisition For Checking";
                            _auditTrailDAO.InsertAudit("frmExpRequisitionPrepare", "EXP_REQUISITION_MST", IUMode, expReqPrepareMstInfo.MstId.ToString(), mailBody, mailSub);
                            return true;
                        }
                        else { trans.Rollback(); }
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