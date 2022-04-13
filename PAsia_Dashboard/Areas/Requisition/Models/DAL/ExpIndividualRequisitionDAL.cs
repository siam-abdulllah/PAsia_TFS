using PAsia_Dashboard.Universal.Gateway;
using System;
using System.Data;
using System.Linq;
using System.Web;
using static PAsia_Dashboard.Areas.Requisition.Models.BEL.ExpRequisitionPrepareBEL;

namespace PAsia_Dashboard.Areas.Requisition.Models.DAL
{
    public class ExpIndividualRequisitionDAL : ReturnData
    {
        DBHelper dbHelper = new DBHelper();
        DBConnection dbConnection = new DBConnection();
        IDGenerated idGenerated = new IDGenerated();
        

        internal object GetExpAllReqMstList(string param)
        {
            try
            {
                string empCode = HttpContext.Current.Session["EMPLOYEE_CODE"].ToString();
                string deptCode = System.Web.HttpContext.Current.Session["DEPARTMENT_CODE"].ToString();
                string qry = "SELECT  DISTINCT MST_ID,  REQUISITION_NO,  REQUISITION_TYPE,  EXPENDITURE_MONTH, " +
                    "  PAY_TO_NAME, PAY_TO_DESIG, PAYMENT_PLACE, " +
                    "  PREPARED_BY_NAME,PREPARED_BY_DESIG,  PREPARED_DATE, CHECKED_BY_NAME,CHECKED_BY_DESIG,DIVISIONAL_BY_NAME,DIVISIONAL_BY_DESIG," +
                    "  VERIFIED_BY_NAME,VERIFIED_BY_DESIG,RECOMMENDED_BY_NAME,RECOMMENDED_BY_DESIG," +
                    "  APPROVED_BY_NAME,APPROVED_BY_DESIG," +
                    "  NVL( CHECKED_STATUS,'Pending') CHECKED_STATUS,NVL( DIVISIONAL_STATUS,'Pending') DIVISIONAL_STATUS,NVL( VERIFIED_STATUS,'Pending') VERIFIED_STATUS,NVL( RECOMMENDED_STATUS,'Pending') RECOMMENDED_STATUS,RECOMMENDED_REMARKS," +
                    "  NVL( APPROVED_STATUS,'Pending') APPROVED_STATUS, CHECKED_DATE, DIVISIONAL_DATE,VERIFIED_DATE," +
                    "  RECOMMENDED_DATE,APPROVED_DATE,APPROVED_REMARKS," +
                    "  NVL( PREPARED_BY_CONFIRM,'No') PREPARED_BY_CONFIRM FROM VW_EXP_REQUISITION WHERE 1=1   AND PREPARED_BY='" + empCode + "' " + param + " " +
                    " UNION ALL" +
                    " SELECT DISTINCT MST_ID," +
                    "                REQUISITION_NO," +
                    "                REQUISITION_TYPE," +
                    "                EXPENDITURE_MONTH," +
                    "                PAY_TO_NAME," +
                    "                PAY_TO_DESIG," +
                    "                PAYMENT_PLACE," +
                    "                PREPARED_BY_NAME," +
                    "                PREPARED_BY_DESIG," +
                    "                PREPARED_DATE," +
                    "                CHECKED_BY_NAME," +
                    "                CHECKED_BY_DESIG," +
                    "                DIVISIONAL_BY_NAME," +
                    "                DIVISIONAL_BY_DESIG," +
                    "                VERIFIED_BY_NAME," +
                    "                VERIFIED_BY_DESIG," +
                    "                RECOMMENDED_BY_NAME," +
                    "                RECOMMENDED_BY_DESIG," +
                    "                APPROVED_BY_NAME," +
                    "                APPROVED_BY_DESIG," +
                    "                NVL (CHECKED_STATUS, 'Pending') CHECKED_STATUS," +
                    "                NVL (DIVISIONAL_STATUS, 'Pending') CHECKED_STATUS," +
                    "                NVL (VERIFIED_STATUS, 'Pending') VERIFIED_STATUS," +
                    "                NVL (RECOMMENDED_STATUS, 'Pending') RECOMMENDED_STATUS," +
                    "                RECOMMENDED_REMARKS," +
                    "                NVL (APPROVED_STATUS, 'Pending') APPROVED_STATUS," +
                    "                CHECKED_DATE," +
                    "                DIVISIONAL_DATE," +
                    "                VERIFIED_DATE," +
                    "                RECOMMENDED_DATE," +
                    "                APPROVED_DATE," +
                    "                APPROVED_REMARKS," +
                    "                NVL (PREPARED_BY_CONFIRM, 'No') PREPARED_BY_CONFIRM" +
                    "  FROM VW_EXP_REQUISITION A INNER JOIN SC_EMPLOYEE B ON A.PREPARED_BY=B.EMP_CODE " +
                    "  WHERE 1=1 AND B.DEPT_CODE='" + deptCode + "'   AND '" + empCode + "' =(SELECT DISTINCT EMP_CODE FROM SA_ML_CONF WHERE SM_URL='frmExpRequisitionPrepare' AND EMP_CODE='" + empCode + "'  " + param + " ) ";

                //" NVL( PREPARED_BY_CONFIRM,'No') PREPARED_BY_CONFIRM FROM VW_EXP_REQUISITION WHERE 1=1  " + param + " ";
                DataTable dt = dbHelper.GetDataTable(dbConnection.SAConnStrReader("Sales"), qry);
                var item = (from DataRow row in dt.Rows
                            select new ExpReqPrepareMst
                            {
                                MstId = Convert.ToInt32(row["MST_ID"]),
                                RequisitionNo = row["REQUISITION_NO"].ToString(),
                                ReqTypeName = row["REQUISITION_TYPE"].ToString(),
                                ExpenditureMonth = row["EXPENDITURE_MONTH"].ToString(),
                                AdjustmentByName = row["REQUISITION_TYPE"].ToString() == "Adjustment" ? row["PAY_TO_NAME"].ToString() : "",
                                PayToName = row["REQUISITION_TYPE"].ToString() == "Adjustment" ? "" : row["PAY_TO_NAME"].ToString(),
                                PayToDesig = row["PAY_TO_DESIG"].ToString(),
                                PaymentPlace = row["PAYMENT_PLACE"].ToString(),
                                PrepareName = row["PREPARED_BY_NAME"].ToString(),
                                PrepareDesig = row["PREPARED_BY_DESIG"].ToString(),
                                PrepareDate = row["PREPARED_DATE"].ToString(),

                                CheckedName = row["CHECKED_BY_NAME"].ToString(),
                                CheckedDesig = row["CHECKED_BY_DESIG"].ToString(),


                                DivisionalName = row["DIVISIONAL_BY_NAME"].ToString(),
                                DivisionalDesig = row["DIVISIONAL_BY_DESIG"].ToString(),

                                VerifiedName = row["VERIFIED_BY_NAME"].ToString(),
                                VerifiedDesig = row["VERIFIED_BY_DESIG"].ToString(),
                                RecommendedName = row["RECOMMENDED_BY_NAME"].ToString(),
                                RecommendedDesig = row["RECOMMENDED_BY_DESIG"].ToString(),
                                ApprovedName = row["APPROVED_BY_NAME"].ToString(),
                                ApprovedDesig = row["APPROVED_BY_DESIG"].ToString(),
                                ApprovedRemarks = row["APPROVED_REMARKS"].ToString(),
                                CheckedStatus = row["CHECKED_STATUS"].ToString(),
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
                string qry = "SELECT DTL_ID, MST_ID, MOP,NVL(PREPARED_VALUE,0) PREPARED_VALUE,NVL(CHECKED_VALUE,0) CHECKED_VALUE,NVL(DIVISIONAL_VALUE,0) DIVISIONAL_VALUE,NVL(VERIFIED_VALUE,0) VERIFIED_VALUE," +
                    " NVL(RECOMMENDED_VALUE,0) RECOMMENDED_VALUE,NVL(APPROVED_VALUE,0) APPROVED_VALUE,PURPOSE, " +
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
                                VerifiedValue = Convert.ToInt32(row["VERIFIED_VALUE"]),
                                RecommendedValue = Convert.ToInt32(row["RECOMMENDED_VALUE"]),
                                ApprovedValue = Convert.ToInt32(row["APPROVED_VALUE"]) == 0 ? Convert.ToInt32(row["RECOMMENDED_VALUE"]) : Convert.ToInt32(row["APPROVED_VALUE"]),
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

        


    }
}