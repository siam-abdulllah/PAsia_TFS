using PAsia_Dashboard.Areas.FSM.Models.BEL;
using PAsia_Dashboard.Universal.Gateway;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace PAsia_Dashboard.Areas.FSM.Models.DAL
{
    public class DashboardNationalPrescriptionDAO
    {
        readonly DBHelper _dbHelper = new DBHelper();
        readonly DBConnection _dbConnection = new DBConnection();

        readonly string _cntDate = DateTime.Now.ToString("dd-MM-yyyy", CultureInfo.CurrentCulture);
        string _cntDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture);
        readonly string _cntMonthYear = DateTime.Now.ToString("MM-yyyy", CultureInfo.CurrentCulture);
        readonly string _lastMonthYear = DateTime.Now.AddMonths(-1).ToString("MM-yyyy", CultureInfo.CurrentCulture);


        public DashboardNationalPrescriptionBEO GetNationalPrescriptionData()
        {
            try
            {
                DashboardNationalPrescriptionBEO model = new DashboardNationalPrescriptionBEO();

                string MPOTargetQry = "Select SUM(PRESCRIPTION_QTY) PRESCRIPTION_QTY from FSM_PRESCRIPTION_TARGET";
                string TodayPrescriptionQry = "Select COUNT(MST_SL) PRESCRIPTION_QTY from FSM_PRESCRIPTION_MST Where TO_CHAR(SET_DATE,'dd-mm-yyyy')='" + _cntDate + "'" +
                                              " AND USER_ID NOT IN ('33333') AND MST_SL IN (SELECT DISTINCT MST_SL FROM FSM_PRESCRIPTION_DTL)";

                string CumulativeQry = "Select COUNT(MST_SL) PRESCRIPTION_QTY from FSM_PRESCRIPTION_MST Where SET_DATE BETWEEN To_Date('" + "01-" + _cntMonthYear + "','dd-mm-yyyy') AND " +
                                       " To_Date('" + _cntDate + "','dd-mm-yyyy')" +
                                       " AND USER_ID NOT IN ('33333') AND MST_SL IN (SELECT DISTINCT MST_SL FROM FSM_PRESCRIPTION_DTL)";//as per tapan's requirement 
                //string CumulativeQry = "Select COUNT(MST_SL) PRESCRIPTION_QTY from FSM_PRESCRIPTION_MST Where SET_DATE<=To_Date('" + _cntDate + "','dd-mm-yyyy')" +
                //                       " AND MST_SL IN (SELECT DISTINCT MST_SL FROM FSM_PRESCRIPTION_DTL)";
                //string LastMPSD = "Select COUNT(MST_SL) PRESCRIPTION_QTY from FSM_PRESCRIPTION_MST Where SET_DATE Between To_Date('" + "01-" + _lastMonthYear + "','dd-mm-yyyy') AND To_Date('" + _cntDate + "','dd-mm-yyyy') " +
                //                  " AND MST_SL IN (SELECT DISTINCT MST_SL FROM FSM_PRESCRIPTION_DTL)";
                string LastMPSD = "Select COUNT(MST_SL) PRESCRIPTION_QTY from FSM_PRESCRIPTION_MST " +
                                  "Where TO_DATE (SET_DATE, 'DD/MM/RRRR') Between TO_DATE (ADD_MONTHS (TRUNC ( SYSDATE,'MONTH'), -1),'DD/MM/RRRR') AND TO_DATE (ADD_MONTHS (SYSDATE,-1),'DD/MM/RRRR') " +
                                  " AND USER_ID NOT IN ('33333') AND MST_SL IN (SELECT DISTINCT MST_SL FROM FSM_PRESCRIPTION_DTL)";

                string LastMonth = "Select COUNT(MST_SL) PRESCRIPTION_QTY from FSM_PRESCRIPTION_MST Where TO_CHAR(SET_DATE,'MM-YYYY')='" + _lastMonthYear + "'" +
                                   " AND USER_ID NOT IN ('33333') AND MST_SL IN (SELECT DISTINCT MST_SL FROM FSM_PRESCRIPTION_DTL)";




                model.MPOTarget = _dbHelper.GetValue(_dbConnection.SAConnStrReader("Sales"), MPOTargetQry);
                model.TodayPrescription = _dbHelper.GetValue(_dbConnection.SAConnStrReader("Sales"), TodayPrescriptionQry);
                model.Cumulative = _dbHelper.GetValue(_dbConnection.SAConnStrReader("Sales"), CumulativeQry); ;
                model.Achievement = (Convert.ToDecimal(model.Cumulative) * 100 / Convert.ToDecimal(model.MPOTarget)).ToString("0.##");


                model.LastMPSD = _dbHelper.GetValue(_dbConnection.SAConnStrReader("Sales"), LastMPSD); ;
                model.LastMonth = _dbHelper.GetValue(_dbConnection.SAConnStrReader("Sales"), LastMonth); ;
                model.Growth = (Convert.ToDecimal(model.Cumulative)-Convert.ToDecimal(model.LastMPSD)).ToString("0.##");
                if (model.LastMPSD == "0")
                {
                    model.GrowthPercentage = "0";
                }
                else
                {
                    model.GrowthPercentage = (Convert.ToDecimal(model.Growth) * 100 / Convert.ToDecimal(model.LastMPSD)).ToString("0.##"); ;
                }

                //Monthly

                //string TotalMPOQry = "Select Count(distinct MIO_CODE) from VW_PAL_FIELD_FORCE_CUST_ESO";
                string TotalMPOQry = "Select Count(distinct MIO_CODE) from VW_PAL_FIELD_FORCE_MIO";//due to change of view
                                                                                                   //string CumulativeSenderMPO = "Select COUNT(MST_SL) PRESCRIPTION_QTY from FSM_PRESCRIPTION_MST Where TO_CHAR(SET_DATE,'MM-YYYY')='" + _cntMonthYear + "'";
                string CumulativeSenderMPO = "Select COUNT(distinct USER_ID) PRESCRIPTION_QTY from FSM_PRESCRIPTION_MST Where SET_DATE BETWEEN To_Date('" + "01-" + _cntMonthYear + "','dd-mm-yyyy') AND " +
                                             " To_Date('" + _cntDate + "','dd-mm-yyyy')" +
                                             " AND MST_SL IN (SELECT DISTINCT MST_SL FROM FSM_PRESCRIPTION_DTL)";//

                string LastMPSDSenderMPO = "Select COUNT(distinct USER_ID) PRESCRIPTION_QTY from FSM_PRESCRIPTION_MST Where TO_DATE (SET_DATE, 'DD/MM/RRRR') Between TO_DATE (ADD_MONTHS (TRUNC ( SYSDATE,'MONTH'), -1),'DD/MM/RRRR') AND TO_DATE (ADD_MONTHS (SYSDATE,-1),'DD/MM/RRRR') " +
                                           " AND MST_SL IN (SELECT DISTINCT MST_SL FROM FSM_PRESCRIPTION_DTL)";
                string noOfWhoSendQry = "Select COUNT(distinct USER_ID) PRESCRIPTION_QTY from FSM_PRESCRIPTION_MST Where TO_CHAR(SET_DATE,'dd-mm-yyyy')='" + _cntDate + "'" +
                                        " AND MST_SL IN (SELECT DISTINCT MST_SL FROM FSM_PRESCRIPTION_DTL)";//
                model.TotalMPO = _dbHelper.GetValue(_dbConnection.SAConnStrReader("Sales"), TotalMPOQry);
                model.CumulativeSenderMPO = _dbHelper.GetValue(_dbConnection.SAConnStrReader("Sales"), CumulativeSenderMPO);
                model.LastMPSDSenderMPO = _dbHelper.GetValue(_dbConnection.SAConnStrReader("Sales"), LastMPSDSenderMPO);
                model.NoOfWhoSend = _dbHelper.GetValue(_dbConnection.SAConnStrReader("Sales"), noOfWhoSendQry);
                model.NoOfWhoDidntSend = (Convert.ToDecimal(model.TotalMPO) - Convert.ToDecimal(model.NoOfWhoSend)).ToString("0.##");


                return model;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<DashboardNationalPrescriptionBEO> GetGridData()
        {
            try
            {
                //string Qry = "Select 'r002' Region_Code,'Regon' Region_Name,'100' Total_MPO,'50' NoOf_Prescription,'125' Today_Prescription,'15' Region_Wise_Target," +
                //    "'2' Current_Month_Prescription,'5' Achievement,'10' Last_MPSD,'2' Deficit  from Dual";


                //DataTable dt = _dbHelper.ReturnCursorF1(_dbConnection.SAConnStrReader("Sales"), "FN_REGION_WISE_PRES_SUM_DASH", "P_DATE ", "07-Apr-2029");
                using (OracleConnection objConn = new OracleConnection(_dbConnection.SAConnStrReader("Sales")))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_REGION_WISE_PRES_SUM_DASH";
                        objCmd.CommandType = CommandType.StoredProcedure;
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
                            select new DashboardNationalPrescriptionBEO
                            {
                                RegionCode = row["Region_Code"].ToString(),
                                RegionName = row["Region_Name"].ToString(),
                                MPOTarget = row["TARGET"].ToString(),
                                TotalMPO = row["TOTAL_EMPLOYEE"].ToString(),
                                NoOfPrescription = row["NO_OF_PRES"].ToString(),
                                TodayPrescription = row["TODAY_PRES"].ToString(),
                                RegionWiseTarget = row["REGION_WISE_TARGET"].ToString(),

                                CurrentMonthPrescription = row["CURRENT_MONTH_TOT_PRES"].ToString(),
                                LastMPSD = row["PREV_MONTH_TOT_PRES"].ToString(),
                                Achievement = row["TARGET"].ToString() == "1" ? "N/A" : row["Achievement"].ToString(),
                                Deficit = row["Deficit"].ToString(),

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