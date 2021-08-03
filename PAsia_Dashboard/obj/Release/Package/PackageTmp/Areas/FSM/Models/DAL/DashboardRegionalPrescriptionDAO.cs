using PAsia_Dashboard.Areas.FSM.Models.BEL;
using PAsia_Dashboard.Universal.Gateway;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Globalization;
using System.Linq;

namespace PAsia_Dashboard.Areas.FSM.Models.DAL
{
    public class DashboardRegionalPrescriptionDAO
    {
        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly DBConnection _dbConnection = new DBConnection();

        private readonly string _cntDate = DateTime.Now.ToString("dd-MM-yyyy", CultureInfo.CurrentCulture);
        private string _cntDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture);
        private string _cntMonthYear = DateTime.Now.ToString("MM-yyyy", CultureInfo.CurrentCulture);
        private readonly string _lastMonthYear = DateTime.Now.AddMonths(-1).ToString("MM-yyyy", CultureInfo.CurrentCulture);

        public DashboardRegionalPrescriptionBEO GetRegionalPrescriptionData(string prodType)
        {
            DashboardRegionalPrescriptionBEO model = new DashboardRegionalPrescriptionBEO();

            try
            {
                string commitmentQry = "Select NVL(SUM(PRACTICING_DAY*PRESCRIPTION_PER_DAY),0) Total  from FSM_DOC_HONORARIUM ";

                //string todayPrescriptionQry = "Select COUNT(MST_SL) PRESCRIPTION_QTY from FSM_PRESCRIPTION_MST Where TO_CHAR(SET_DATE,'dd-mm-yyyy')='" + _cntDate + "' " +
                //                              " AND MST_SL IN (SELECT DISTINCT MST_SL FROM FSM_PRESCRIPTION_DTL) AND DOCTOR_CODE IN (Select distinct DOCTOR_CODE from FSM_DOC_HONORARIUM) ";

                //string cumulativeQry = "Select COUNT(MST_SL) PRESCRIPTION_QTY from FSM_PRESCRIPTION_MST Where SET_DATE<=To_Date('" + _cntDate + "','dd-mm-yyyy') " +
                //                       "AND MST_SL IN (SELECT DISTINCT MST_SL FROM FSM_PRESCRIPTION_DTL) AND DOCTOR_CODE IN (Select distinct DOCTOR_CODE from FSM_DOC_HONORARIUM)";

                //string lastMpSd = "Select COUNT(MST_SL) PRESCRIPTION_QTY from FSM_PRESCRIPTION_MST Where SET_DATE Between To_Date('" + "01-" + _lastMonthYear + "','dd-mm-yyyy') AND To_Date('" + _cntDate + "','dd-mm-yyyy') " +
                //                  "AND MST_SL IN (SELECT DISTINCT MST_SL FROM FSM_PRESCRIPTION_DTL) AND DOCTOR_CODE IN (Select distinct DOCTOR_CODE from FSM_DOC_HONORARIUM)";

                //string lastMonth = "Select COUNT(MST_SL) PRESCRIPTION_QTY from FSM_PRESCRIPTION_MST Where TO_CHAR(SET_DATE,'MM-YYYY')='" + _lastMonthYear + "'" +
                //                   "AND MST_SL IN (SELECT DISTINCT MST_SL FROM FSM_PRESCRIPTION_DTL) AND DOCTOR_CODE IN (Select distinct DOCTOR_CODE from FSM_DOC_HONORARIUM)";

                string todayPrescriptionQry = "Select COUNT(MST_SL) PRESCRIPTION_QTY from FSM_PRESCRIPTION_MST Where TO_CHAR(SET_DATE,'dd-mm-yyyy')='" + _cntDate + "' " +
                                                             " AND MST_SL IN (SELECT DISTINCT MST_SL FROM FSM_PRESCRIPTION_DTL WHERE PRODUCT_CODE IN " +
                                                             " (SELECT PRODUCT_CODE FROM FSM_PROD_DETAIL WHERE TYPE='" + prodType + "')) ";

                string cumulativeQry = "Select COUNT(MST_SL) PRESCRIPTION_QTY from FSM_PRESCRIPTION_MST Where SET_DATE BETWEEN To_Date('" + "01-" + _cntMonthYear + "','dd-mm-yyyy') AND " +
                                       " To_Date('" + _cntDate + "','dd-mm-yyyy') AND MST_SL IN (SELECT DISTINCT MST_SL FROM FSM_PRESCRIPTION_DTL WHERE PRODUCT_CODE IN " +
                                       " (SELECT DISTINCT PRODUCT_CODE FROM FSM_PROD_DETAIL WHERE TYPE='" + prodType + "')) ";

                string lastMpSd = "Select COUNT(MST_SL) PRESCRIPTION_QTY from FSM_PRESCRIPTION_MST Where SET_DATE Between TO_DATE (ADD_MONTHS (TRUNC ( SYSDATE,'MONTH'), -1),'DD/MM/RRRR') AND TO_DATE (ADD_MONTHS (SYSDATE,-1),'DD/MM/RRRR')  " +
                                  "AND MST_SL IN (SELECT DISTINCT MST_SL FROM FSM_PRESCRIPTION_DTL WHERE PRODUCT_CODE IN " +
                                  " (SELECT DISTINCT PRODUCT_CODE FROM FSM_PROD_DETAIL WHERE TYPE='" + prodType + "')) ";

                string lastMonth = "Select COUNT(MST_SL) PRESCRIPTION_QTY from FSM_PRESCRIPTION_MST Where TO_CHAR(SET_DATE,'MM-YYYY')='" + _lastMonthYear + "'" +
                                   "AND MST_SL IN (SELECT DISTINCT MST_SL FROM FSM_PRESCRIPTION_DTL WHERE PRODUCT_CODE IN " +
                                   " (SELECT DISTINCT PRODUCT_CODE FROM FSM_PROD_DETAIL WHERE TYPE='" + prodType + "')) ";


                model.Commitment = _dbHelper.GetValue(_dbConnection.SAConnStrReader("Sales"), commitmentQry);
               // model.Commitment = "0";
                model.TodayPrescription = _dbHelper.GetValue(_dbConnection.SAConnStrReader("Sales"), todayPrescriptionQry);
                model.Cumulative = _dbHelper.GetValue(_dbConnection.SAConnStrReader("Sales"), cumulativeQry);
                model.Achievement = model.Commitment == "0" ? "0" : (Convert.ToDecimal(model.Cumulative) * 100 / Convert.ToDecimal(model.Commitment)).ToString("0.##");
                model.LastMPSD = _dbHelper.GetValue(_dbConnection.SAConnStrReader("Sales"), lastMpSd);
                model.LastMonth = _dbHelper.GetValue(_dbConnection.SAConnStrReader("Sales"), lastMonth);
                model.Growth = (Convert.ToDecimal(model.Cumulative) - Convert.ToDecimal(model.LastMPSD)).ToString("0.##");
                model.GrowthPercentage = model.LastMPSD == "0" ? "0" : (Convert.ToDecimal(model.Growth) * 100 / Convert.ToDecimal(model.LastMPSD)).ToString("0.##");
                return model;
            }
            catch (Exception e)
            {
                throw;
            }

        }




        public List<DashboardRegionalPrescriptionBEO> GetGridData(string prodType)
        {
            //string Qry = "Select 'r002' Region_Code,'Regon' Region_Name,'p002' Product_Code,'Product' Product_Name,'125' Today_Prescription,'15' Cumulative," +
            //    "'5' Achievement,'10' Last_MPSD,'2' Growth,'2' Growth_Percentage,'10' Existing_MPO,'5' Sending_MPO  from Dual";


            //var dt = _dbHelper.ReturnCursorF1(_dbConnection.SAConnStrReader("Sales"), "FN_REGION_WISE_XELPRO_SUM", "P_DATE ", "07-Apr-2029");
            try
            {
                using (OracleConnection objConn = new OracleConnection(_dbConnection.SAConnStrReader("Sales")))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_REGION_WISE_XELPRO_SUM";
                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.Parameters.Add("P_TYPE", OracleType.VarChar).Value = prodType;
                        objCmd.Parameters.Add("return_value", OracleType.Cursor).Direction =
                            ParameterDirection.ReturnValue;
                        objConn.Open();
                        objCmd.ExecuteNonQuery();
                        OracleDataReader rdr = objCmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        if (rdr.HasRows)
                        {
                            dt.Load(rdr);
                        }

                        int count = 0;
                        var item = (from DataRow row in dt.Rows
                                    select new DashboardRegionalPrescriptionBEO
                                    {
                                        RegionCode = row["Region_Code"].ToString(),
                                        RegionName = row["Region_Name"].ToString(),
                                        Commitment = row["XELPRO_COMMITMENT"].ToString(),
                                        TodayPrescription = row["TODAY_PRES"].ToString(),
                                        Cumulative = row["CUMMULATIVE_PRES"].ToString(),
                                        Achievement = row["XELPRO_COMMITMENT"].ToString() == "0"
                                            ? "0"
                                            : row["Achievement"].ToString(),
                                        LastMPSD = row["LAST_MSDP"].ToString(),
                                        Growth = row["growth"].ToString(),
                                        HonorariumAmount = row["HONORARIUM_AMOUNT"].ToString(),
                                        ExistingMPO = row["NO_OF_MIO"].ToString(),
                                        SendingMPO = row["NO_OF_SEND_MIO"].ToString()
                                    }).ToList();
                        return item;
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
