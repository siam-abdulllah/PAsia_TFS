using PAsia_Dashboard.Areas.Requisition.Models.BEL;
using PAsia_Dashboard.Universal.Gateway;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PAsia_Dashboard.Areas.Requisition.Models.DAL
{
    public class RequisitionReportDAL
    {
        DBConnection dbConnection = new DBConnection();
        DBHelper dbHelper = new DBHelper();



        public DataTable GetDateWiseRequisition(RequisitionReportBEL model)
        {

            string roleName = HttpContext.Current.Session["ROLE_NAME"].ToString();
            string employeeCode = HttpContext.Current.Session["EMPLOYEE_CODE"].ToString();
            string Qry = "SELECT MST_ID,REQUISITION_NO, REQUISITION_DATE,REQUISITION_TYPE,EXPENDITURE_MONTH,PAY_TO," +
                "PAY_TO_NAME,PAY_TO_DESIG,PAYMENT_PLACE,PREPARED_BY,PREPARED_BY_NAME,PREPARED_BY_DESIG,PREPARED_DATE,PREPARED_REMARKS, " +
                "CHECKED_BY,CHECKED_BY_NAME,CHECKED_BY_DESIG,CHECKED_DATE,CHECKED_REMARKS,CHECKED_STATUS, VERIFIED_BY,VERIFIED_BY_NAME,VERIFIED_BY_DESIG," +
                "VERIFIED_DATE,VERIFIED_REMARKS,VERIFIED_STATUS,RECOMMENDED_BY RECOMMMENDED_BY,RECOMMENDED_BY_NAME RECOMMMENDED_BY_NAME,RECOMMENDED_BY_DESIG RECOMMMENDED_BY_DESIG," +
                "RECOMMENDED_DATE RECOMMMENDED_DATE,RECOMMENDED_REMARKS RECOMMMENDED_REMARKS,RECOMMENDED_STATUS,APPROVED_BY,APPROVED_BY_NAME,APPROVED_BY_DESIG,APPROVED_DATE APPROVED_BY_DATE,APPROVED_REMARKS,APPROVED_STATUS, TOTAL_APPROVED_VALUE," +
                "MOP,NVL(PREPARED_VALUE,0) PREPARED_VALUE,NVL(CHECKED_VALUE,0) CHECKED_VALUE,NVL(VERIFIED_VALUE,0) VERIFIED_VALUE,NVL(RECOMMENDED_VALUE,0) RECOMMMENDED_VALUE,NVL(APPROVED_VALUE,0) APPROVED_VALUE,TO_CHAR (REQUIRED_DATE, 'dd/mm/yyyy') REQUIRED_DATE," +
                "PURPOSE,TO_CHAR (FROM_DATE, 'dd/mm/yyyy') FROM_DATE,TO_CHAR (TO_DATE, 'dd/mm/yyyy') TO_DATE,TOTAL_DAYS " +
                "from VW_EXP_REQUISITION WHERE 1=1 ";
            if (employeeCode != "04626" && employeeCode != "04521" && employeeCode != "99999" && roleName != "CEO")
            {
                Qry = Qry + " AND APPROVED_STATUS='Approved' ";
            }
            if (!string.IsNullOrEmpty(model.PrepareFromDate) && !string.IsNullOrEmpty(model.PrepareToDate))
            {
                Qry = Qry + "AND TO_DATE(REQUISITION_DATE,'dd/MM/YYYY') BETWEEN TO_DATE('" + model.PrepareFromDate + "','dd/MM/YYYY') AND TO_DATE('" + model.PrepareToDate + "','dd/MM/YYYY')";
            }
            if (!string.IsNullOrEmpty(model.ApprovedFromDate) && !string.IsNullOrEmpty(model.ApprovedToDate))
            {
                Qry = Qry + " AND  TO_DATE(APPROVED_DATE,'dd/MM/YYYY') BETWEEN TO_DATE('" + model.ApprovedFromDate + "','dd/MM/YYYY') AND TO_DATE('" + model.ApprovedToDate + "','dd/MM/YYYY')";
            }
            if (model.PreparedBy != null)
            {
                Qry = Qry + " AND PREPARED_BY='" + model.PreparedBy + "' ";
            }
            if (model.RequisitionNo != null)
            {
                Qry = Qry + "AND REQUISITION_NO='" + model.RequisitionNo + "' ";
            }
            Qry = Qry + "UNION SELECT MST_ID,REQUISITION_NO, REQUISITION_DATE,REQUISITION_TYPE,EXPENDITURE_MONTH,PAY_TO," +
                " PAY_TO_NAME,PAY_TO_DESIG,PAYMENT_PLACE,PREPARED_BY,PREPARED_BY_NAME,PREPARED_BY_DESIG,PREPARED_DATE,PREPARED_REMARKS, " +
                " CHECKED_BY,CHECKED_BY_NAME,CHECKED_BY_DESIG,CHECKED_DATE,CHECKED_REMARKS,CHECKED_STATUS, VERIFIED_BY,VERIFIED_BY_NAME,VERIFIED_BY_DESIG," +
                " VERIFIED_DATE,VERIFIED_REMARKS,VERIFIED_STATUS,RECOMMENDED_BY RECOMMMENDED_BY,RECOMMENDED_BY_NAME RECOMMMENDED_BY_NAME,RECOMMENDED_BY_DESIG RECOMMMENDED_BY_DESIG," +
                " RECOMMENDED_DATE RECOMMMENDED_DATE,RECOMMENDED_REMARKS RECOMMMENDED_REMARKS,RECOMMENDED_STATUS,APPROVED_BY,APPROVED_BY_NAME,APPROVED_BY_DESIG,APPROVED_DATE APPROVED_BY_DATE,APPROVED_REMARKS,APPROVED_STATUS, TOTAL_APPROVED_VALUE," +
                " MOP,NVL(PREPARED_VALUE,0) PREPARED_VALUE,NVL(CHECKED_VALUE,0) CHECKED_VALUE,NVL(VERIFIED_VALUE,0) VERIFIED_VALUE,NVL(RECOMMENDED_VALUE,0) RECOMMMENDED_VALUE,NVL(APPROVED_VALUE,0) APPROVED_VALUE,TO_CHAR (REQUIRED_DATE, 'dd/mm/yyyy') REQUIRED_DATE," +
                " PURPOSE,TO_CHAR (FROM_DATE, 'dd/mm/yyyy') FROM_DATE,TO_CHAR (TO_DATE, 'dd/mm/yyyy') TO_DATE,TOTAL_DAYS " +
                " from VW_EXP_REQUISITION WHERE 1=1 ";
            if (model.PreparedBy != null)
            {
                Qry = Qry + " AND PREPARED_BY='" + model.PreparedBy + "' ";
            }
            if (model.RequisitionNo != null)
            {
                Qry = Qry + "AND REQUISITION_NO='" + model.RequisitionNo + "' ";
            }
            if (!string.IsNullOrEmpty(model.PrepareFromDate) && !string.IsNullOrEmpty(model.PrepareToDate))
            {
                Qry = Qry + "AND TO_DATE(REQUISITION_DATE,'dd/MM/YYYY') BETWEEN TO_DATE('" + model.PrepareFromDate + "','dd/MM/YYYY') AND TO_DATE('" + model.PrepareToDate + "','dd/MM/YYYY')";
            }
            if (!string.IsNullOrEmpty(model.ApprovedFromDate) && !string.IsNullOrEmpty(model.ApprovedToDate))
            {
                Qry = Qry + " AND TO_DATE(VERIFIED_DATE,'dd/MM/YYYY') BETWEEN TO_DATE('" + model.ApprovedFromDate + "','dd/MM/YYYY') AND TO_DATE('" + model.ApprovedToDate + "','dd/MM/YYYY') ";
            }
           // Qry = Qry + " AND VERIFIED_STATUS='Approved' AND TOTAL_APPROVED_VALUE<=10000";
            Qry = Qry + " AND VERIFIED_STATUS='Approved' AND TOTAL_APPROVED_VALUE<=50000";
            Qry = Qry + "UNION SELECT MST_ID,REQUISITION_NO, REQUISITION_DATE,REQUISITION_TYPE,EXPENDITURE_MONTH,PAY_TO," +
                " PAY_TO_NAME,PAY_TO_DESIG,PAYMENT_PLACE,PREPARED_BY,PREPARED_BY_NAME,PREPARED_BY_DESIG,PREPARED_DATE,PREPARED_REMARKS, " +
                " CHECKED_BY,CHECKED_BY_NAME,CHECKED_BY_DESIG,CHECKED_DATE,CHECKED_REMARKS,CHECKED_STATUS, VERIFIED_BY,VERIFIED_BY_NAME,VERIFIED_BY_DESIG," +
                " VERIFIED_DATE,VERIFIED_REMARKS,VERIFIED_STATUS,RECOMMENDED_BY RECOMMMENDED_BY,RECOMMENDED_BY_NAME RECOMMMENDED_BY_NAME,RECOMMENDED_BY_DESIG RECOMMMENDED_BY_DESIG," +
                " RECOMMENDED_DATE RECOMMMENDED_DATE,RECOMMENDED_REMARKS RECOMMMENDED_REMARKS,RECOMMENDED_STATUS,APPROVED_BY,APPROVED_BY_NAME,APPROVED_BY_DESIG,APPROVED_DATE APPROVED_BY_DATE,APPROVED_REMARKS,APPROVED_STATUS, TOTAL_APPROVED_VALUE," +
                " MOP,NVL(PREPARED_VALUE,0) PREPARED_VALUE,NVL(CHECKED_VALUE,0) CHECKED_VALUE,NVL(VERIFIED_VALUE,0) VERIFIED_VALUE,NVL(RECOMMENDED_VALUE,0) RECOMMMENDED_VALUE,NVL(APPROVED_VALUE,0) APPROVED_VALUE,TO_CHAR (REQUIRED_DATE, 'dd/mm/yyyy') REQUIRED_DATE," +
                " PURPOSE,TO_CHAR (FROM_DATE, 'dd/mm/yyyy') FROM_DATE,TO_CHAR (TO_DATE, 'dd/mm/yyyy') TO_DATE,TOTAL_DAYS " +
                " from VW_EXP_REQUISITION WHERE 1=1 ";
            if (model.PreparedBy != null)
            {
                Qry = Qry + " AND PREPARED_BY='" + model.PreparedBy + "' ";
            }
            if (model.RequisitionNo != null)
            {
                Qry = Qry + "AND REQUISITION_NO='" + model.RequisitionNo + "' ";
            }
            if (!string.IsNullOrEmpty(model.PrepareFromDate) && !string.IsNullOrEmpty(model.PrepareToDate))
            {
                Qry = Qry + "AND TO_DATE(REQUISITION_DATE,'dd/MM/YYYY') BETWEEN TO_DATE('" + model.PrepareFromDate + "','dd/MM/YYYY') AND TO_DATE('" + model.PrepareToDate + "','dd/MM/YYYY')";
            }
            if (!string.IsNullOrEmpty(model.ApprovedFromDate) && !string.IsNullOrEmpty(model.ApprovedToDate))
            {
                Qry = Qry + " AND TO_DATE(VERIFIED_DATE,'dd/MM/YYYY') BETWEEN TO_DATE('" + model.ApprovedFromDate + "','dd/MM/YYYY') AND TO_DATE('" + model.ApprovedToDate + "','dd/MM/YYYY') ";
            }
            // Qry = Qry + " AND VERIFIED_STATUS='Approved' AND TOTAL_APPROVED_VALUE<=10000";
            Qry = Qry + " AND VERIFIED_STATUS='Approved' AND TOTAL_APPROVED_VALUE>10000  AND REQUISITION_TYPE IN (SELECT TYPE_NAME FROM REQ_TYPE_LIMIT WHERE STATUS='A') ";
            Qry = Qry + " order by REQUISITION_NO DESC";

            DataTable dt = dbHelper.GetDataTable(dbConnection.SAConnStrReader("Sales"), Qry);
            return dt;
        }

        public List<RequisitionReportBEL> GetEmployees(string PrepareFromDate, string PrepareToDate, string ApprovedFromDate, string ApprovedToDate)
        {
            string roleName = HttpContext.Current.Session["ROLE_NAME"].ToString();
            string employeeCode = HttpContext.Current.Session["EMPLOYEE_CODE"].ToString();
            string Qry = "SELECT DISTINCT PREPARED_BY,PREPARED_BY_NAME FROM VW_EXP_REQUISITION" +
                " WHERE  1=1  ";
            if (!string.IsNullOrEmpty(PrepareFromDate) && !string.IsNullOrEmpty(PrepareToDate))
            {
                Qry = Qry + "AND REQUISITION_DATE BETWEEN '" + PrepareFromDate + "' AND '" + PrepareToDate + "'";
            }
            if (!string.IsNullOrEmpty(ApprovedFromDate) && !string.IsNullOrEmpty(ApprovedToDate))
            {
                Qry = Qry + " AND  APPROVED_DATE BETWEEN '" + ApprovedFromDate + "' AND '" + ApprovedToDate + "'";
            }
            if (employeeCode != "04626" && employeeCode != "04521" && employeeCode != "99999" && roleName != "CEO")
            {
                Qry = Qry + " AND APPROVED_STATUS='Approved' ";
            }
            Qry = Qry + " UNION SELECT DISTINCT PREPARED_BY, PREPARED_BY_NAME FROM " +
                " VW_EXP_REQUISITION WHERE 1=1";
            if (!string.IsNullOrEmpty(PrepareFromDate) && !string.IsNullOrEmpty(PrepareToDate))
            {
                Qry = Qry + "AND REQUISITION_DATE BETWEEN '" + PrepareFromDate + "' AND '" + PrepareToDate + "'";
            }
            if (!string.IsNullOrEmpty(ApprovedFromDate) && !string.IsNullOrEmpty(ApprovedToDate))
            {
                Qry = Qry + " AND VERIFIED_DATE BETWEEN '" + ApprovedFromDate + "' AND '" + ApprovedToDate + "' ";
            }

            //Qry = Qry + " AND VERIFIED_STATUS='Approved' AND TOTAL_APPROVED_VALUE<=10000";
           // Qry = Qry + " AND VERIFIED_STATUS='Approved' AND (TOTAL_APPROVED_VALUE<=50000 OR REQUISITION_TYPE='Adjustment' ) ";
            Qry = Qry + " AND VERIFIED_STATUS='Approved' AND TOTAL_APPROVED_VALUE<=50000 ";

            Qry = Qry + " UNION SELECT DISTINCT PREPARED_BY, PREPARED_BY_NAME FROM " +
                " VW_EXP_REQUISITION WHERE 1=1";
            if (!string.IsNullOrEmpty(PrepareFromDate) && !string.IsNullOrEmpty(PrepareToDate))
            {
                Qry = Qry + "AND REQUISITION_DATE BETWEEN '" + PrepareFromDate + "' AND '" + PrepareToDate + "'";
            }
            if (!string.IsNullOrEmpty(ApprovedFromDate) && !string.IsNullOrEmpty(ApprovedToDate))
            {
                Qry = Qry + " AND VERIFIED_DATE BETWEEN '" + ApprovedFromDate + "' AND '" + ApprovedToDate + "' ";
            }

            //Qry = Qry + " AND VERIFIED_STATUS='Approved' AND TOTAL_APPROVED_VALUE<=10000";
            // Qry = Qry + " AND VERIFIED_STATUS='Approved' AND (TOTAL_APPROVED_VALUE<=50000 OR REQUISITION_TYPE='Adjustment' ) ";
            Qry = Qry + " AND VERIFIED_STATUS='Approved' AND TOTAL_APPROVED_VALUE>10000  AND REQUISITION_TYPE IN (SELECT TYPE_NAME FROM REQ_TYPE_LIMIT WHERE STATUS='A') ";

            DataTable dt = dbHelper.GetDataTable(dbConnection.SAConnStrReader("Sales"), Qry);

            List<RequisitionReportBEL> item;
            item = (from DataRow row in dt.Rows
                    select new RequisitionReportBEL
                    {
                        PreparedBy = row["PREPARED_BY"].ToString(),
                        PreparedByName = row["PREPARED_BY_NAME"].ToString(),

                    }).ToList();

            return item;
        }

        public List<RequisitionReportBEL> GetAllRequisition(string PrepareFromDate, string PrepareToDate, string ApprovedFromDate, string ApprovedToDate, string empCode)
        {
            string roleName = HttpContext.Current.Session["ROLE_NAME"].ToString();
            string employeeCode = HttpContext.Current.Session["EMPLOYEE_CODE"].ToString();
            string Qry = "SELECT DISTINCT REQUISITION_NO FROM VW_EXP_REQUISITION WHERE 1=1 ";
            if (employeeCode != "04626" && employeeCode != "04521" && employeeCode != "99999" && roleName != "CEO")
            {
                Qry = Qry + " AND APPROVED_STATUS='Approved' ";
            }
            if (!string.IsNullOrEmpty(PrepareFromDate) && !string.IsNullOrEmpty(PrepareToDate))
            {
                Qry = Qry + " AND TO_DATE(REQUISITION_DATE,'dd/MM/YYYY') BETWEEN TO_DATE('" + PrepareFromDate + "','dd/MM/YYYY') AND TO_DATE('" + PrepareToDate + "','dd/MM/YYYY')";
            }
            if (!string.IsNullOrEmpty(ApprovedFromDate) && !string.IsNullOrEmpty(ApprovedToDate))
            {
                Qry = Qry + " AND  TO_DATE(APPROVED_DATE,'dd/MM/YYYY') BETWEEN TO_DATE('" + ApprovedFromDate + "','dd/MM/YYYY') AND TO_DATE('" + ApprovedToDate + "','dd/MM/YYYY')";
            }
            if (empCode != null)
            {
                Qry = Qry + " AND PREPARED_BY='" + empCode + "'";
            }
            Qry = Qry + " UNION SELECT DISTINCT REQUISITION_NO FROM " +
               " VW_EXP_REQUISITION WHERE 1=1";
            if (!string.IsNullOrEmpty(PrepareFromDate) && !string.IsNullOrEmpty(PrepareToDate))
            {
                Qry = Qry + " AND TO_DATE(REQUISITION_DATE,'dd/MM/YYYY') BETWEEN TO_DATE('" + PrepareFromDate + "','dd/MM/YYYY') AND TO_DATE('" + PrepareToDate + "','dd/MM/YYYY')";
            }
            if (!string.IsNullOrEmpty(ApprovedFromDate) && !string.IsNullOrEmpty(ApprovedToDate))
            {
                Qry = Qry + " AND TO_DATE(VERIFIED_DATE,'dd/MM/YYYY') BETWEEN TO_DATE('" + ApprovedFromDate + "','dd/MM/YYYY') AND TO_DATE('" + ApprovedToDate + "','dd/MM/YYYY') ";
            }
            if (empCode != null)
            {
                Qry = Qry + " AND PREPARED_BY='" + empCode + "'";
            }
           // Qry = Qry + " AND VERIFIED_STATUS='Approved'  AND TOTAL_APPROVED_VALUE<=10000 order by REQUISITION_NO DESC";
            //Qry = Qry + " AND VERIFIED_STATUS='Approved' AND (TOTAL_APPROVED_VALUE<=50000 OR REQUISITION_TYPE='Adjustment' )  order by REQUISITION_NO DESC";
            Qry = Qry + " AND VERIFIED_STATUS='Approved' AND TOTAL_APPROVED_VALUE<=50000 ";
            Qry = Qry + " UNION SELECT DISTINCT REQUISITION_NO FROM " +
               " VW_EXP_REQUISITION WHERE 1=1";
            if (!string.IsNullOrEmpty(PrepareFromDate) && !string.IsNullOrEmpty(PrepareToDate))
            {
                Qry = Qry + " AND TO_DATE(REQUISITION_DATE,'dd/MM/YYYY') BETWEEN TO_DATE('" + PrepareFromDate + "','dd/MM/YYYY') AND TO_DATE('" + PrepareToDate + "','dd/MM/YYYY')";
            }
            if (!string.IsNullOrEmpty(ApprovedFromDate) && !string.IsNullOrEmpty(ApprovedToDate))
            {
                Qry = Qry + " AND TO_DATE(VERIFIED_DATE,'dd/MM/YYYY') BETWEEN TO_DATE('" + ApprovedFromDate + "','dd/MM/YYYY') AND TO_DATE('" + ApprovedToDate + "','dd/MM/YYYY') ";
            }
            if (empCode != null)
            {
                Qry = Qry + " AND PREPARED_BY='" + empCode + "'";
            }
             Qry = Qry + " AND VERIFIED_STATUS='Approved' AND TOTAL_APPROVED_VALUE>10000  AND REQUISITION_TYPE IN (SELECT TYPE_NAME FROM REQ_TYPE_LIMIT WHERE STATUS='A')  order by REQUISITION_NO DESC";



            DataTable dt = dbHelper.GetDataTable(dbConnection.SAConnStrReader("Sales"), Qry);

            List<RequisitionReportBEL> item;
            item = (from DataRow row in dt.Rows
                    select new RequisitionReportBEL
                    {
                        RequisitionNo = row["REQUISITION_NO"].ToString()

                    }).ToList();

            return item;
        }

        public CompanyInfoBEL GetCompanyInfo()
        {
            CompanyInfoBEL companyInfoBEL = new CompanyInfoBEL();
            companyInfoBEL.ComName = dbHelper.GetValue(dbConnection.SAConnStrReader("Sales"), "Select COMP_NAME from PARAM WHERE COMP_CODE = '001'");
            companyInfoBEL.ComAddress = dbHelper.GetValue(dbConnection.SAConnStrReader("Sales"), "Select ADD1 from PARAM WHERE COMP_CODE = '001'");
            return companyInfoBEL;
        }
        public HeadingBEL GetHeading()
        {
            HeadingBEL headingBEL = new HeadingBEL();
            headingBEL.PreparedBy = dbHelper.GetValue(dbConnection.SAConnStrReader("Sales"), "Select PREPARE_VAR from HEADING WHERE MST_ID = '1'");
            headingBEL.CheckedBy = dbHelper.GetValue(dbConnection.SAConnStrReader("Sales"), "Select CHECK_VAR from HEADING WHERE MST_ID = '1'");
            headingBEL.VerifiedBy = dbHelper.GetValue(dbConnection.SAConnStrReader("Sales"), "Select VERIFY_VAR from HEADING WHERE MST_ID = '1'");
            headingBEL.RecommendBy = dbHelper.GetValue(dbConnection.SAConnStrReader("Sales"), "Select RECOMMEND_VAR from HEADING WHERE MST_ID = '1'");
            headingBEL.ApprovedBy = dbHelper.GetValue(dbConnection.SAConnStrReader("Sales"), "Select APPROVE_VAR from HEADING WHERE MST_ID = '1'");
            return headingBEL;
        }
    }
}