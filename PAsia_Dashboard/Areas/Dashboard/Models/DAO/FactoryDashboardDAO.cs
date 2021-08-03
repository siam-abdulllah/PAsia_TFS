using PAsia_Dashboard.Universal.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PAsia_Dashboard.Areas.Dashboard.Models.BEL;
using System.Data;
using System.Data.OracleClient;
using System.Diagnostics;

namespace PAsia_Dashboard.Areas.Dashboard.Models.DAO
{
    public class FactoryDashboardDAO : ReturnData
    {
        DBConnection _dbConn = new DBConnection();
        readonly DBHelper _dbHelper = new DBHelper();
        readonly DBConnection _dbConnection = new DBConnection();
        string toDayDate = DateTime.Now.ToString("dd/MM/yyyy");
        private DataRow _row;
        private DataTable _dt;

        public FactoryDashboard GetDashboardData()
        {
            try
            {
                string CODE = HttpContext.Current.Session["CODE"].ToString();
                string ACCESS_LEVEL = HttpContext.Current.Session["ACCESS_LEVEL"].ToString();
                string accessLevelParam = "";

                FactoryDashboard factoryDashboard = new FactoryDashboard();
                //if (ACCESS_LEVEL == "N" || ACCESS_LEVEL == null)
                //{
                //    accessLevelParam = "";
                //    //factoryDashboard.ACCESS_LEVEL = "National";
                //    factoryDashboard.POSTING_LOCATION = "National";
                //}
                //else if (ACCESS_LEVEL == "Z")
                //{
                //    accessLevelParam = " AND DSM_CODE = '" + CODE + "'";
                //}
                //else if (ACCESS_LEVEL == "D")
                //{
                //    accessLevelParam = " AND DEPOT_CODE = '" + CODE + "'";
                //    string depot_Name_Qry =
                //        "SELECT UNIT_NAME FROM SC_COMP_UNIT WHERE UNIT_CODE NOT IN ('01','02') AND UNIT_CODE=" + CODE + " ";
                //    _row = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), depot_Name_Qry).Rows[0];
                //    factoryDashboard.POSTING_LOCATION = _row["UNIT_NAME"].ToString();
                //}
                //else if (ACCESS_LEVEL == "R")
                //{
                //    accessLevelParam = " AND RSM_CODE = '" + CODE + "'";
                //    string region_Name_Qry =
                //        "SELECT REGION_NAME FROM VW_PAL_FIELD_FORCE_MIO WHERE  RSM_CODE=" + CODE + " ";
                //    _row = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), region_Name_Qry).Rows[0];
                //    factoryDashboard.POSTING_LOCATION = _row["REGION_NAME"].ToString();
                //}
                //else if (ACCESS_LEVEL == "A")
                //{
                //    accessLevelParam = " AND AM_CODE = '" + CODE + "'";

                //    string area_Name_Qry =
                //        "SELECT AREA_NAME FROM VW_PAL_FIELD_FORCE_MIO WHERE AM_CODE=" + CODE + " ";
                //    _row = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), area_Name_Qry).Rows[0];
                //    factoryDashboard.POSTING_LOCATION = _row["AREA_NAME"].ToString();
                //}
                //else if (ACCESS_LEVEL == "T")
                //{
                //    accessLevelParam = " AND MPO_CODE = '" + CODE + "'";

                //}

                factoryDashboard.NoOfBatch = GetNoOfBatch(accessLevelParam);
                factoryDashboard.RawMatIssueVal = GetRawMatIssueVal(accessLevelParam);
                factoryDashboard.RawMatRcvVal = GetRawMatRcvVal(accessLevelParam);
                factoryDashboard.RawMatRelVal = GetRawMatRelVal(accessLevelParam);
                factoryDashboard.PackMatIssueVal = GetPackMatIssueVal(accessLevelParam);
                factoryDashboard.PackMatRcvVal = GetPackMatRcvVal(accessLevelParam);
                factoryDashboard.PackMatRelVal = GetPackMatRelVal(accessLevelParam);
                //factoryDashboard.WipTotalVal = GetWipTotalVal(accessLevelParam);
                //factoryDashboard.Growth = "";
                factoryDashboard.ProdToWarBox = GetProdToWarBox(accessLevelParam);
                factoryDashboard.WarToCWHBox = GetWarToCWHBox(accessLevelParam);
                factoryDashboard.NoOfQCTestToday = GetNoOfQCTestToday(accessLevelParam);
                factoryDashboard.NoOfPMReqToday = GetNoOfPMReqToday(accessLevelParam);
                factoryDashboard.NoOfRMReqTotal = GetNoOfRMReqTotal(accessLevelParam);
                factoryDashboard.NoOfPMReqTotal = GetNoOfPMReqTotal(accessLevelParam);
                factoryDashboard.NoOfRMReqThirtyForty = GetNoOfRMReqThirtyForty(accessLevelParam);
                factoryDashboard.NoOfPMReqFiftyHundred = GetNoOfPMReqFiftyHundred(accessLevelParam);
                factoryDashboard.NoOfRMTest = GetNoOfRMTest(accessLevelParam);
                factoryDashboard.NoOfPMTest = GetNoOfPMTest(accessLevelParam);
                factoryDashboard.NoOfMicrobiologyTest = GetNoOfMicrobiologyTest(accessLevelParam);
                factoryDashboard.NoOfLOPToday = GetNoOfLOPToday(accessLevelParam);
                factoryDashboard.NoOfLOPUpTo = GetNoOfLOPUpTo(accessLevelParam);
                factoryDashboard.NoOfLCUpTo = GetNoOfLCUpTo(accessLevelParam);
                factoryDashboard.NoOfCSToday = GetNoOfCSToday(accessLevelParam);
                factoryDashboard.NoOfCSTotal = GetNoOfCSTotal(accessLevelParam);
                factoryDashboard.NoOfCommStockCurr = GetNoOfCommStockCurr(accessLevelParam);
                factoryDashboard.NoOfSampleStockCurr = GetNoOfSampleStockCurr(accessLevelParam);
                factoryDashboard.NoOfRMOtherRcvReqToday = GetNoOfRMOtherRcvReqToday(accessLevelParam);
                factoryDashboard.NoOfRMOtherRcvReqTotal = GetNoOfRMOtherRcvReqTotal(accessLevelParam);
                factoryDashboard.NoOfPMOtherRcvReqToday = GetNoOfPMOtherRcvReqToday(accessLevelParam);
                factoryDashboard.NoOfPMOtherRcvReqTotal = GetNoOfPMOtherRcvReqTotal(accessLevelParam);
                //factoryDashboard.ACCESS_LEVEL = ACCESS_LEVEL;
                //    if (ACCESS_LEVEL == "N" || ACCESS_LEVEL == "D")
                //{
                //    factoryDashboard.Commercial_Stock_Valuation = GetCommercialStockValuation(accessLevelParam);

                //}
                //factoryDashboard.Growth = "";
                return factoryDashboard;
            }
            catch (Exception e)
            {
                ExceptionReturn = e.Message;
                var linenum = Convert.ToInt32(e.StackTrace.Substring(e.StackTrace.LastIndexOf(' ')));
                throw;
            }
        }
        public string GetNoOfBatch(string param)
        {
            string noOfBatch_Qry =
                "SELECT FN_NO_OF_BATCH('" + toDayDate + "') NO_OF_BATCH FROM DUAL" + param;
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), noOfBatch_Qry);
            string noOfBatch = "";
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                noOfBatch = _row["NO_OF_BATCH"].ToString();
            }


            return noOfBatch;
        }
        private string GetRawMatIssueVal(string param)
        {
            string rawMatIssueVal_Qry =
                "SELECT FN_RM_ISSUED_AMOUNT('" + toDayDate + "') RAW_MAT_ISSUE_VAL FROM DUAL"+param;
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), rawMatIssueVal_Qry);
            string rawMatIssueVal = "";
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                rawMatIssueVal = _row["RAW_MAT_ISSUE_VAL"].ToString();
            }
            
            return rawMatIssueVal;
        }
        private string GetRawMatRcvVal(string param)
        {
            string rawMatRcvVal_Qry =
                 "SELECT FN_RM_RECEIVED_AMOUNT('" + toDayDate + "') RAW_MAT_RCV_VAL FROM DUAL";
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), rawMatRcvVal_Qry);
            string rawMatRcvVal = "";
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                rawMatRcvVal = _row["RAW_MAT_RCV_VAL"].ToString();
            }

            return rawMatRcvVal;
        }

        public string GetRawMatRelVal(string param)
        {
            string rawMatRelVal_Qry =
                 "SELECT FN_RM_RELEASED_AMOUNT('" + toDayDate + "') RAW_MAT_REL_VAL FROM DUAL";
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), rawMatRelVal_Qry);
            string rawMatRelVal = "";
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                rawMatRelVal = _row["RAW_MAT_REL_VAL"].ToString();
            }

            return rawMatRelVal;
        }

        public string GetPackMatIssueVal(string param)
        {
            string packMatIssueVal_Qry =
                 "SELECT FN_PM_ISSUED_AMOUNT('" + toDayDate + "') PACK_MAT_ISSUE_VAL FROM DUAL";
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), packMatIssueVal_Qry);
            string packMatIssue = "";
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                packMatIssue = _row["PACK_MAT_ISSUE_VAL"].ToString();
            }

            return packMatIssue;
        }
        public string GetPackMatRcvVal(string param)
        {
            string PackMatRcvVal_Qry =
                 "SELECT FN_PM_RECEIVED_AMOUNT ('" + toDayDate + "') PACK_MAT_RCV_VAL FROM DUAL";
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), PackMatRcvVal_Qry);
            string PackMatRcvVal = "";
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                PackMatRcvVal = _row["PACK_MAT_RCV_VAL"].ToString();
            }

            return PackMatRcvVal;
        }
        public string GetPackMatRelVal(string param)
        {
            string packMatRelVal_Qry =
                 "SELECT FN_PM_RELEASED_AMOUNT ('" + toDayDate + "') PACK_MAT_REL_VAL FROM DUAL";
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), packMatRelVal_Qry);
            string packMatRelVal = "";
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                packMatRelVal = _row["PACK_MAT_REL_VAL"].ToString();
            }

            return packMatRelVal;
        }
        public string GetWipTotalVal(string param)
        {
            string wipTotalVal_Qry =
                  "SELECT FN_PM_RELEASED_AMOUNT ('" + toDayDate + "') PACK_MAT_REL_VAL FROM DUAL";
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), wipTotalVal_Qry);
            string wipTotalVal = "";
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                wipTotalVal = _row["PACK_MAT_REL_VAL"].ToString();
            }

            return wipTotalVal;
        }

        public string GetProdToWarBox(string param)
        {
            string prodToWarBox_Qry =
                "SELECT FN_PROD_TO_FG_TRAN ('" + toDayDate + "') PROD_TO_WAR_BOX FROM DUAL";
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), prodToWarBox_Qry);
            string prodToWarBoxVal = "";
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                prodToWarBoxVal = _row["PROD_TO_WAR_BOX"].ToString();
            }

            return prodToWarBoxVal;
        }

        public string GetWarToCWHBox(string param)
        {

            string warToCWHBox_Qry =
                "SELECT FN_FG_TO_CWH_TRAN ('" + toDayDate + "') WAR_TO_CWH_BOX FROM DUAL";
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), warToCWHBox_Qry);
            string warToCWHBoxVal = "";
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                warToCWHBoxVal = _row["WAR_TO_CWH_BOX"].ToString();
            }

            return warToCWHBoxVal;
        }

        public string GetNoOfQCTestToday(string param)
        {
            
            string NoOfQCTestToday_Qry =
                "SELECT FN_FG_NO_OF_QCTR ('" + toDayDate + "') VALUE FROM DUAL";
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), NoOfQCTestToday_Qry);
            string NoOfQCTestToday = "";
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                NoOfQCTestToday = _row["VALUE"].ToString();
            }

            return NoOfQCTestToday;
        }

        public string GetNoOfPMReqToday(string param)
        {
            string NoOfPMReqToday_Qry =
                "SELECT FN_TODAY_PMREQ ('" + toDayDate + "') VALUE FROM DUAL";
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), NoOfPMReqToday_Qry);
            string NoOfPMReqToday = "";
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                NoOfPMReqToday = _row["VALUE"].ToString();
            }

            return NoOfPMReqToday;
        }

        public string GetNoOfRMReqTotal(string param)
        {
            string NoOfRMReqTotal_Qry =
                "SELECT FN_UPTO_RMREQ ('" + toDayDate + "') VALUE FROM DUAL";
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), NoOfRMReqTotal_Qry);
            string NoOfRMReqTotal = "";
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                NoOfRMReqTotal = _row["VALUE"].ToString();
            }

            return NoOfRMReqTotal;
        }
        public string GetNoOfPMReqTotal(string param)
        {
            string NoOfPMReqTotal_Qry =
                "SELECT FN_UPTO_PMREQ ('" + toDayDate + "') VALUE FROM DUAL";
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), NoOfPMReqTotal_Qry);
            string NoOfPMReqTotal = "";
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                NoOfPMReqTotal = _row["VALUE"].ToString();
            }

            return NoOfPMReqTotal;
        }

        public string GetNoOfRMReqThirtyForty(string param)
        {
            string NoOfRMReqThirtyForty_Qry =
               "SELECT FN_UPTO_RMREQ_30_40 ('" + toDayDate + "') VALUE FROM DUAL";
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), NoOfRMReqThirtyForty_Qry);
            string NoOfRMReqThirtyForty = "";
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                NoOfRMReqThirtyForty = _row["VALUE"].ToString();
            }

            return NoOfRMReqThirtyForty;
        }
        public string GetNoOfPMReqFiftyHundred(string param)
        {
            string NoOfPMReqFiftyHundred_Qry =
               "SELECT FN_UPTO_PMREQ_50_100 ('" + toDayDate + "') VALUE FROM DUAL";
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), NoOfPMReqFiftyHundred_Qry);
            string NoOfPMReqFiftyHundred = "";
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                NoOfPMReqFiftyHundred = _row["VALUE"].ToString();
            }

            return NoOfPMReqFiftyHundred;
        }
        public string GetNoOfRMTest(string param)
        {
            string NoOfRMTest_Qry =
               "SELECT FN_RM_TEST_TODAY ('" + toDayDate + "') VALUE FROM DUAL";
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), NoOfRMTest_Qry);
            string NoOfRMTest = "";
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                NoOfRMTest = _row["VALUE"].ToString();
            }

            return NoOfRMTest;
        }
        public string GetNoOfPMTest(string param)
        {
            string NoOfPMTest_Qry =
               "SELECT FN_PM_TEST_TODAY ('" + toDayDate + "') VALUE FROM DUAL";
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), NoOfPMTest_Qry);
            string NoOfPMTest = "";
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                NoOfPMTest = _row["VALUE"].ToString();
            }

            return NoOfPMTest;
        }
        public string GetNoOfMicrobiologyTest(string param)
        {
            string NoOfMicrobiologyTest_Qry =
               "SELECT FN_MICRO_PACK_QCTEST_TODAY ('" + toDayDate + "') VALUE FROM DUAL";
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), NoOfMicrobiologyTest_Qry);
            string NoOfMicrobiologyTest = "";
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                NoOfMicrobiologyTest = _row["VALUE"].ToString();
            }

            return NoOfMicrobiologyTest;
        }
        public string GetNoOfLOPToday(string param)
        {
            string NoOfLOPToday_Qry =
               "SELECT FN_TODAY_LPO ('" + toDayDate + "') VALUE FROM DUAL";
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), NoOfLOPToday_Qry);
            string NoOfLOPToday = "";
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                NoOfLOPToday = _row["VALUE"].ToString();
            }

            return NoOfLOPToday;
        }
        public string GetNoOfLOPUpTo(string param)
        {
            string NoOfLOPUpTo_Qry =
               "SELECT FN_UPTO_LPO ('" + toDayDate + "') VALUE FROM DUAL";
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), NoOfLOPUpTo_Qry);
            string NoOfLOPUpTo = "";
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                NoOfLOPUpTo = _row["VALUE"].ToString();
            }

            return NoOfLOPUpTo;
        }
        public string GetNoOfLCUpTo(string param)
        {
            string NoOfLCUpTo_Qry =
               "SELECT FN_UPTO_LC ('" + toDayDate + "') VALUE FROM DUAL";
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), NoOfLCUpTo_Qry);
            string NoOfLCUpTo = "";
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                NoOfLCUpTo = _row["VALUE"].ToString();
            }

            return NoOfLCUpTo;
        }
        public string GetNoOfCSToday(string param)
        {
            string NoOfCSToday_Qry =
               "SELECT FN_TODAY_CS ('" + toDayDate + "') VALUE FROM DUAL";
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), NoOfCSToday_Qry);
            string NoOfCSToday = "";
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                NoOfCSToday = _row["VALUE"].ToString();
            }

            return NoOfCSToday;
        }
        public string GetNoOfCSTotal(string param)
        {
            string NoOfCSTotal_Qry =
               "SELECT FN_UPTO_CS ('" + toDayDate + "') VALUE FROM DUAL";
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), NoOfCSTotal_Qry);
            string NoOfCSTotal = "";
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                NoOfCSTotal = _row["VALUE"].ToString();
            }

            return NoOfCSTotal;
        }
        public string GetNoOfCommStockCurr(string param)
        {
            string NoOfCommStockCurr_Qry =
               "SELECT FN_CURRENT_COMMERCIAL_STK VALUE FROM DUAL";
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), NoOfCommStockCurr_Qry);
            string NoOfCommStockCurr = "";
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                NoOfCommStockCurr = _row["VALUE"].ToString();
            }

            return NoOfCommStockCurr;
        }
        public string GetNoOfSampleStockCurr(string param)
        {
            string NoOfSampleStockCurr_Qry =
               "SELECT FN_CURRENT_SAMPLE_STK VALUE FROM DUAL";
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), NoOfSampleStockCurr_Qry);
            string NoOfSampleStockCurr = "";
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                NoOfSampleStockCurr = _row["VALUE"].ToString();
            }

            return NoOfSampleStockCurr;
        }
        public string GetNoOfRMOtherRcvReqToday(string param)
        {
            string NoOfRMOtherRcvReqToday_Qry =
               "SELECT FN_OTHERS_RCVRM_TODAY ('" + toDayDate + "')  VALUE FROM DUAL";
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), NoOfRMOtherRcvReqToday_Qry);
            string NoOfRMOtherRcvReqToday = "";
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                NoOfRMOtherRcvReqToday = _row["VALUE"].ToString();
            }

            return NoOfRMOtherRcvReqToday;
        }
        public string GetNoOfRMOtherRcvReqTotal(string param)
        {
            string NoOfRMOtherRcvReqTotal_Qry =
               "SELECT FN_OTHERS_RCVRM_UPTO ('" + toDayDate + "')  VALUE FROM DUAL";
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), NoOfRMOtherRcvReqTotal_Qry);
            string NoOfRMOtherRcvReqTotal = "";
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                NoOfRMOtherRcvReqTotal = _row["VALUE"].ToString();
            }

            return NoOfRMOtherRcvReqTotal;
        }
        public string GetNoOfPMOtherRcvReqToday(string param)
        {
            string NoOfPMOtherRcvReqToday_Qry =
               "SELECT FN_OTHERS_RCVPM_TODAY ('" + toDayDate + "')  VALUE FROM DUAL";
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), NoOfPMOtherRcvReqToday_Qry);
            string NoOfPMOtherRcvReqToday = "";
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                NoOfPMOtherRcvReqToday = _row["VALUE"].ToString();
            }

            return NoOfPMOtherRcvReqToday;
        }  public string GetNoOfPMOtherRcvReqTotal(string param)
        {
            string NoOfPMOtherRcvReqTotal_Qry =
               "SELECT FN_OTHERS_RCVPM_UPTO ('" + toDayDate + "')  VALUE FROM DUAL";
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), NoOfPMOtherRcvReqTotal_Qry);
            string NoOfPMOtherRcvReqTotal = "";
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                NoOfPMOtherRcvReqTotal = _row["VALUE"].ToString();
            }

            return NoOfPMOtherRcvReqTotal;
        }

        public DiscontinueMaturedDue GetDiscontinueMaturedDues(string param)
        {
            string Discontinue_Matured_Dues_Qry =
                "select ROUND(sum(MATURE_DUES_AMT_CA)/100000,2) DIS_MATURE_DUES_AMT_CA, ROUND(sum(MATURE_DUES_AMT_CR)/100000,2) DIS_MATURE_DUES_AMT_CR, ROUND((sum(MATURE_DUES_AMT_CA) + sum(MATURE_DUES_AMT_CR))/100000,2) DIS_TOTAL_MATURE_DUES_AMT " +
                " from ( select MODE_OF_PAYMENT, SUM(MATURE_DUES_AMT) MATURE_DUES_AMT_CA, 0 MATURE_DUES_AMT_CR  from DISCONTINUE_MATURE_DUES where MODE_OF_PAYMENT = 'Cash' AND SUBSTR(yyyymm,1,4)=TO_CHAR(sysdate,'yyyy') " +
                param + " " +
                " GROUP BY  MODE_OF_PAYMENT UNION ALL select MODE_OF_PAYMENT, 0 MATURE_DUES_AMT_CA, SUM(MATURE_DUES_AMT) MATURE_DUES_AMT_CR  from DISCONTINUE_MATURE_DUES where MODE_OF_PAYMENT = 'Credit' AND SUBSTR(yyyymm,1,4)=TO_CHAR(sysdate,'yyyy') " +
                param + " " +
                " GROUP BY  MODE_OF_PAYMENT)  ";
            //
            DiscontinueMaturedDue discontinueMaturedDue = new DiscontinueMaturedDue();
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), Discontinue_Matured_Dues_Qry);
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                //row = dbHelper.GetDataTable(dbConnection.SAConnStrReader("Dashboard"), Discontinue_Matured_Dues_Qry).Rows[0];
                discontinueMaturedDue.Discontinue_Matured_Dues = _row["DIS_TOTAL_MATURE_DUES_AMT"].ToString();
                discontinueMaturedDue.Discontinue_Matured_Dues_CA = _row["DIS_MATURE_DUES_AMT_CA"].ToString();
                discontinueMaturedDue.Discontinue_Matured_Dues_CR = _row["DIS_MATURE_DUES_AMT_CR"].ToString();
            }

            return discontinueMaturedDue;
        }

        public ImmaturedDue GetImmaturedDue(string param)
        {
            string IMMatured_Dues_Qry =
                "select ROUND(sum(MATURE_DUES_AMT_CA)/100000,2) IMMATURE_DUES_AMT_CA, ROUND(sum(MATURE_DUES_AMT_CR)/100000,2) IMMATURE_DUES_AMT_CR, ROUND((sum(MATURE_DUES_AMT_CA) + sum(MATURE_DUES_AMT_CR))/100000,2) TOTAL_IMMATURE_DUES_AMT " +
                " from ( select MODE_OF_PAYMENT, SUM(MATURE_DUES_AMT) MATURE_DUES_AMT_CA, 0 MATURE_DUES_AMT_CR  from IMMATURE_DUES where MODE_OF_PAYMENT = 'Cash' " +
                param + " " +
                " GROUP BY  MODE_OF_PAYMENT UNION ALL select MODE_OF_PAYMENT, 0 MATURE_DUES_AMT_CA, SUM(MATURE_DUES_AMT) MATURE_DUES_AMT_CR  from IMMATURE_DUES where MODE_OF_PAYMENT = 'Credit' " +
                param + " " +
                " GROUP BY  MODE_OF_PAYMENT)  ";
            //
            ImmaturedDue immaturedDue = new ImmaturedDue();
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), IMMatured_Dues_Qry);
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                //row = dbHelper.GetDataTable(dbConnection.SAConnStrReader("Dashboard"), IMMatured_Dues_Qry).Rows[0];
                immaturedDue.Immatured_Dues = _row["TOTAL_IMMATURE_DUES_AMT"].ToString();
                immaturedDue.Immatured_Dues_CA = _row["IMMATURE_DUES_AMT_CA"].ToString();
                immaturedDue.Immatured_Dues_CR = _row["IMMATURE_DUES_AMT_CR"].ToString();
            }

            return immaturedDue;
        }

        public ProductValueSale GetProductSalesValue(string param, string tableName)
        {
            //string CM_Product_Value_Sales_Qry =
            //    "SELECT ROUND(SUM(NET_VALUE)/100000,2) CM_Product_Value_Sales FROM DATE_WISE_XELPRO_SALES  WHERE 1=1 " +
            //    param + " ";
            string Product_value_sales_qry = "SELECT SUM(CM_Product_Value_Sales) CM_Total_Product_Value_Sales,SUM(LM_Product_Value_Sales) " +
            " LM_Total_Product_Value_Sales FROM" +
            " (SELECT ROUND(SUM(NET_VALUE) / 100000, 2) CM_Product_Value_Sales, 0 LM_Product_Value_Sales FROM " + tableName +
            " WHERE TO_CHAR(INVOICE_DATE, 'YYYYMM') = TO_CHAR(SYSDATE, 'YYYYMM') " + param + " " +
            "  UNION ALL" +
            " SELECT 0 CM_Product_Value_Sales, ROUND(SUM(NET_VALUE) / 100000, 2) LM_Product_Value_Sales FROM " + tableName +
            " WHERE TO_DATE (INVOICE_DATE, 'DD/MM/RRRR') BETWEEN TO_DATE (ADD_MONTHS (TRUNC ( SYSDATE,'MONTH'), -1),'DD/MM/RRRR') AND TO_DATE (ADD_MONTHS (SYSDATE,-1),'DD/MM/RRRR') " + param + " )";
            ProductValueSale productValueSale = new ProductValueSale();
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), Product_value_sales_qry);
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                //row = dbHelper.GetDataTable(dbConnection.SAConnStrReader("Dashboard"), CM_Product_Value_Sales_Qry).Rows[0];
                productValueSale.CM_Product_Value_Sales = _row["CM_Total_Product_Value_Sales"].ToString();
                productValueSale.LM_Product_Value_Sales = _row["LM_Total_Product_Value_Sales"].ToString();
            }

            return productValueSale;
        }
        public ProductQTYSale GetProductSalesQTY(string param, string tableName)
        {
            string Product_qty_sales_qry = "SELECT SUM (CM_Product_QTY) CM_Total_Product_QTY,SUM(LM_Product_QTY) LM_Total_Product_QTY " +
                " FROM(SELECT SUM(NET_QTY) CM_Product_QTY,0 LM_Product_QTY FROM " + tableName +
                " WHERE TO_CHAR(INVOICE_DATE, 'YYYYMM') = TO_CHAR(SYSDATE, 'YYYYMM') " + param + " " +
                " UNION ALL SELECT 0 CM_Product_QTY, SUM(NET_QTY) LM_Product_QTY FROM " + tableName +
                " WHERE TO_DATE(INVOICE_DATE, 'DD/MM/RRRR') BETWEEN TO_DATE(ADD_MONTHS(TRUNC(SYSDATE,'MONTH'),-1),'DD/MM/RRRR') " +
                " AND TO_DATE(ADD_MONTHS( SYSDATE,-1),'DD/MM/RRRR')  " + param + " )";

            ProductQTYSale productQTYSale = new ProductQTYSale();
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), Product_qty_sales_qry);
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                //row = dbHelper.GetDataTable(dbConnection.SAConnStrReader("Dashboard"), CM_Product_Value_Sales_Qry).Rows[0];
                productQTYSale.CM_Product_QTY_Sales = _row["CM_Total_Product_QTY"].ToString();
                productQTYSale.LM_Product_QTY_Sales = _row["LM_Total_Product_QTY"].ToString();
            }

            return productQTYSale;
        }

        public string GetCommercialStockValuation(string param)
        {
            string COMMERCIAL_STOCK_VAL = "";
            string Commercial_Stock_Valuation_Qry =
                "SELECT ROUND(SUM(STOCK_VAl)/100000,2) COMMERCIAL_STOCK_VAL FROM (SELECT SUM(STOCK_VAl) STOCK_VAl  FROM PRODUCT_STOCK_QTY_VAL WHERE 1=1  " +
                param + "" +
                " UNION ALL SELECT SUM(STOCK_VAl) STOCK_VAl FROM PPM_STOCK_QTY_VAL WHERE PPM_TYPE = '001' " +
                param + " ) ";
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), Commercial_Stock_Valuation_Qry);
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                //row = dbHelper.GetDataTable(dbConnection.SAConnStrReader("Dashboard"), Commercial_Stock_Valuation_Qry).Rows[0];
                COMMERCIAL_STOCK_VAL = _row["COMMERCIAL_STOCK_VAL"].ToString();
            }

            //
            return COMMERCIAL_STOCK_VAL;
        }

        public string GetSampleStockValuation(string param)
        {
            string SAMPLE_STOCK_VAl = "";
            string Sample_Stock_Valuation_Qry =
                "SELECT ROUND(SUM(STOCK_VAl)/100000,2) SAMPLE_STOCK_VAl FROM PPM_STOCK_QTY_VAL WHERE PPM_TYPE = '002' " +
                param + " ";
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), Sample_Stock_Valuation_Qry);
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                //row = dbHelper.GetDataTable(dbConnection.SAConnStrReader("Dashboard"), Sample_Stock_Valuation_Qry).Rows[0];
                SAMPLE_STOCK_VAl = _row["SAMPLE_STOCK_VAl"].ToString();
            }

            //
            return SAMPLE_STOCK_VAl;
        }

        public string GetPPMStockValuation(string param)
        {
            string SAMPLE_STOCK_VAl = "";
            string PPM_Stock_Valuation_Qry =
                "SELECT ROUND(SUM(STOCK_VAl)/100000,2) PPM_STOCK_VAl FROM PPM_STOCK_QTY_VAL WHERE PPM_TYPE = '003' " +
                param + " ";
            //
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), PPM_Stock_Valuation_Qry);
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                //row = dbHelper.GetDataTable(dbConnection.SAConnStrReader("Dashboard"), PPM_Stock_Valuation_Qry).Rows[0];
                SAMPLE_STOCK_VAl = _row["PPM_STOCK_VAl"].ToString();
            }

            //
            return SAMPLE_STOCK_VAl;
        }

        public string GetGiftStockValuation(string param)
        {
            string SAMPLE_STOCK_VAl = "";
            string Gift_Stock_Valuation_Qry =
                "SELECT ROUND(SUM(STOCK_VAl)/100000,2) Gift_STOCK_VAl FROM PPM_STOCK_QTY_VAL WHERE PPM_TYPE = '004' " +
                param + " ";
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), Gift_Stock_Valuation_Qry);
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                //row = dbHelper.GetDataTable(dbConnection.SAConnStrReader("Dashboard"), Gift_Stock_Valuation_Qry).Rows[0];
                SAMPLE_STOCK_VAl = _row["Gift_STOCK_VAl"].ToString();
            }

            //
            return SAMPLE_STOCK_VAl;
        }
        public DCCTotalSale GetDCC_Sale(string param)
        {
            string DCC_Sales_Qry =
            "SELECT ROUND(SUM(DCC_SALES_LM)/100000,2) DCC_TOTAL_SALES_LM,ROUND(SUM(DCC_SALES_CM)/100000,2) DCC_TOTAL_SALES_CM FROM ( SELECT 0 DCC_SALES_LM,SUM(NET_INV_AMT) DCC_SALES_CM FROM DATE_WISE_DCC_SALES " +
            " WHERE TO_CHAR (INVOICE_DATE, 'YYYYMM') = TO_CHAR(SYSDATE, 'YYYYMM') " + param + " " +
            " UNION ALL SELECT SUM(NET_INV_AMT) DCC_SALES_LM,0 DCC_SALES_CM FROM DATE_WISE_DCC_SALES" +
            " WHERE TO_DATE (INVOICE_DATE, 'DD/MM/RRRR') BETWEEN TO_DATE (ADD_MONTHS (TRUNC ( SYSDATE,'MONTH'), -1),'DD/MM/RRRR') AND TO_DATE (ADD_MONTHS (SYSDATE,-1),'DD/MM/RRRR') " + param + " )";
            DCCTotalSale dccTotalSales = new DCCTotalSale();
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), DCC_Sales_Qry);
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                //row = dbHelper.GetDataTable(dbConnection.SAConnStrReader("Dashboard"), DCC_Sales_Qry).Rows[0];
                dccTotalSales.DCC_TOTAL_SALES_CM = _row["DCC_TOTAL_SALES_CM"].ToString();
                dccTotalSales.DCC_TOTAL_SALES_LM = _row["DCC_TOTAL_SALES_LM"].ToString();
            }

            //
            return dccTotalSales;
        }
        public List<DashboardChart> GetBarChartData()
        {
            string CODE = HttpContext.Current.Session["CODE"].ToString();
            string ACCESS_LEVEL = HttpContext.Current.Session["ACCESS_LEVEL"].ToString();
            string accessLevelParam = "";
            string accessLevelParamStock = "";
            if (ACCESS_LEVEL == "N" || ACCESS_LEVEL == null)
            {
                accessLevelParam = "";
            }
            else if (ACCESS_LEVEL == "Z")
            {
                accessLevelParam = "AND DSM_CODE = '" + CODE + "'";

            }
            else if (ACCESS_LEVEL == "D")
            {
                accessLevelParam = "AND DEPOT_CODE = '" + CODE + "'";
            }
            else if (ACCESS_LEVEL == "R")
            {
                accessLevelParam = "AND RSM_CODE = '" + CODE + "'";
            }
            else if (ACCESS_LEVEL == "A")
            {
                accessLevelParam = "AND AM_CODE = '" + CODE + "'";
            }
            else if (ACCESS_LEVEL == "T")
            {
                accessLevelParam = "AND MPO_CODE = '" + CODE + "'";
            }
            string barChart_Qry = "SELECT DISTINCT RSM_NAME,DWS.RSM_CODE, NET_INV_AMT,REGION_NAME,NULL COLOR FROM (SELECT RSM_CODE, ROUND(SUM (NET_INV_AMT)/ 100000,2) NET_INV_AMT FROM DATE_WISE_SALES " +
                                    " WHERE INVOICE_DATE BETWEEN TRUNC(SYSDATE, 'MM') AND TRUNC(SYSDATE)  " + accessLevelParam + " GROUP BY RSM_CODE ) DWS,VW_PAL_FIELD_FORCE_MIO VPFFM " +
                                    " WHERE DWS.RSM_CODE = VPFFM.RSM_CODE ORDER BY REGION_NAME";
            DataTable barChart = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), barChart_Qry);
            List<DashboardChart> barChartList;
            barChartList = (from DataRow row in barChart.Rows
                            select new DashboardChart
                            {
                                Level = row["REGION_NAME"].ToString(),
                                Data = row["NET_INV_AMT"].ToString(),
                                Color = row["COLOR"].ToString(),
                                BaloonText = row["RSM_NAME"].ToString()
                            }).ToList();
            return barChartList;
        }
        public ProdSale GetProdSalesData(string param, string prodCode)
        {

            string Prod_Sales_Qry = "SELECT SUM(NET_QTY) PROD_TOTAL_QTY,ROUND(SUM(NET_VALUE)/100000, 2) PROD_TOTAL_VALUE,PRODUCT_CODE FROM DATE_WISE_PROD_SALES WHERE INVOICE_DATE BETWEEN " +
                                  "TO_DATE(TRUNC((SYSDATE), 'MONTH'), 'DD/MM/RRRR') AND TO_DATE(SYSDATE,'DD/MM/RRRR') AND PRODUCT_CODE='" + prodCode + "' " + param + "  GROUP BY PRODUCT_CODE";
            ProdSale prodSales = new ProdSale();
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), Prod_Sales_Qry);
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                prodSales.PROD_TOTAL_QTY = _row["PROD_TOTAL_QTY"].ToString();
                prodSales.PROD_TOTAL_VALUE = _row["PROD_TOTAL_VALUE"].ToString();
            }
            return prodSales;
        }
        public WorldCupOffer GetWorldCupOffer(string param)
        {

            string World_Cup_offer_Qry = "select sum(nvl(qty,0)) NET_ISSUED_QTY,ROUND(sum(nvl(NET_INV_VALUE,0))/100000, 2) NET_INV_VALUE from (" +
                                         " select Distinct invoice_no, (count(ISSUED_QTY) / 4) qty, a.NET_INV_VALUE" +
                                         " from invoice_mst a, invoice_dtl b" +
                                         " where a.inv_mst_slno = b.inv_mst_slno" +
                                         " and   inv_type_code = 'INV001'" +
                                         " and   nvl(offer_type, 'N') = 'WorldCup'  " + param + "  " +
                                         " group by invoice_no, a.NET_INV_VALUE)";
            WorldCupOffer worldCupOffer = new WorldCupOffer();
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), World_Cup_offer_Qry);
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                worldCupOffer.NET_ISSUED_QTY = _row["NET_ISSUED_QTY"].ToString();
                worldCupOffer.NET_INV_VALUE = _row["NET_INV_VALUE"].ToString();
            }
            return worldCupOffer;
        }
        public EzylifeNoOfCustomer GetEzylifeNoOfCustomer(string param)
        {

            //string EzylifeNoOfCustomerQry = "SELECT SUM(CUSTOMER_CODE) CM_NO_OF_CUSTOMER FROM VW_EZYLIFE_NO_OF_CUSTOMER WHERE YYYYMM=to_char(SYSDATE,'YYYYMM') " + param + " ";
            string EzylifeNoOfCustomerQry = "SELECT SUM (CM_NO_OF_CUSTOMER) CM_NO_OF_CUSTOMER," +
                                            "       SUM (LM_NO_OF_CUSTOMER) LM_NO_OF_CUSTOMER" +
                                            "  FROM (SELECT COUNT(DISTINCT CUSTOMER_CODE) CM_NO_OF_CUSTOMER, 0 LM_NO_OF_CUSTOMER" +
                                            "          FROM VW_EZYLIFE_CM_NO_OF_CUSTOMER" +
                                            "         WHERE 1=1" +
                                            //" AND YYYYMM = TO_CHAR (SYSDATE, 'YYYYMM')" +
                                            "        UNION ALL" +
                                            "        SELECT 0 CM_NO_OF_CUSTOMER, COUNT(DISTINCT CUSTOMER_CODE) LM_NO_OF_CUSTOMER" +
                                            "          FROM VW_EZYLIFE_LM_NO_OF_CUSTOMER" +
                                            "         WHERE 1=1 " +
                                            //" AND YYYYMM =" +
                                            //"                  TO_CHAR (ADD_MONTHS (TRUNC (SYSDATE, 'MONTH'), -1)," +
                                            //"                           'YYYYMM')" +
                                            ")";
            EzylifeNoOfCustomer EzylifeNoOfCustomer = new EzylifeNoOfCustomer();
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), EzylifeNoOfCustomerQry);
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                EzylifeNoOfCustomer.CM_EZYLIFE_NO_OF_CUSTOMER = _row["CM_NO_OF_CUSTOMER"].ToString();
                EzylifeNoOfCustomer.LM_EZYLIFE_NO_OF_CUSTOMER = _row["LM_NO_OF_CUSTOMER"].ToString();

            }
            return EzylifeNoOfCustomer;
        }
        public XelproNoOfCustomer GetXelproNoOfCustomer(string param)
        {

            // string EzylifeNoOfCustomerQry = "SELECT SUM(CUSTOMER_CODE) CUSTOMER_CODE FROM VW_XELPRO_NO_OF_CUSTOMER WHERE YYYYMM=to_char(SYSDATE,'YYYYMM') " + param + " ";
            string XelproNoOfCustomerQry = "SELECT SUM (CM_NO_OF_CUSTOMER) CM_NO_OF_CUSTOMER," +
                                            "       SUM (LM_NO_OF_CUSTOMER) LM_NO_OF_CUSTOMER" +
                                            "  FROM (SELECT COUNT(DISTINCT CUSTOMER_CODE) CM_NO_OF_CUSTOMER, 0 LM_NO_OF_CUSTOMER" +
                                            "          FROM VW_XELPRO_CM_NO_OF_CUSTOMER" +
                                            "         WHERE 1=1 " +
                                            // " AND YYYYMM = TO_CHAR (SYSDATE, 'YYYYMM')" +
                                            "        UNION ALL" +
                                            "        SELECT 0 CM_NO_OF_CUSTOMER, COUNT(DISTINCT CUSTOMER_CODE) LM_NO_OF_CUSTOMER" +
                                            "          FROM VW_XELPRO_LM_NO_OF_CUSTOMER" +
                                            "         WHERE 1=1 " +
                                            //" AND YYYYMM =" +
                                            //"                  TO_CHAR (ADD_MONTHS (TRUNC (SYSDATE, 'MONTH'), -1)," +
                                            //"                           'YYYYMM')" +
                                            ")";
            XelproNoOfCustomer XelproNoOfCustomer = new XelproNoOfCustomer();
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), XelproNoOfCustomerQry);
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                XelproNoOfCustomer.CM_XELPRO_NO_OF_CUSTOMER = _row["CM_NO_OF_CUSTOMER"].ToString();
                XelproNoOfCustomer.LM_XELPRO_NO_OF_CUSTOMER = _row["LM_NO_OF_CUSTOMER"].ToString();

            }
            return XelproNoOfCustomer;
        }
        public FuxtilNoOfCustomer GetFuxtilNoOfCustomer(string param)
        {

            // string FuxtilNoOfCustomerQry = "SELECT SUM(CUSTOMER_CODE) CUSTOMER_CODE FROM VW_FUXTIL_NO_OF_CUSTOMER WHERE YYYYMM=to_char(SYSDATE,'YYYYMM') " + param + " ";
            string FuxtilNoOfCustomerQry = "SELECT SUM (CM_NO_OF_CUSTOMER) CM_NO_OF_CUSTOMER," +
                                            "       SUM (LM_NO_OF_CUSTOMER) LM_NO_OF_CUSTOMER" +
                                            "  FROM (SELECT COUNT(DISTINCT CUSTOMER_CODE) CM_NO_OF_CUSTOMER, 0 LM_NO_OF_CUSTOMER" +
                                            "          FROM VW_FUXTIL_CM_NO_OF_CUSTOMER" +
                                            "         WHERE 1=1 " +
                                            " AND YYYYMM = TO_CHAR (SYSDATE, 'YYYYMM')" +
                                            "        UNION ALL" +
                                            "        SELECT 0 CM_NO_OF_CUSTOMER, COUNT(DISTINCT CUSTOMER_CODE) LM_NO_OF_CUSTOMER" +
                                            "          FROM VW_FUXTIL_LM_NO_OF_CUSTOMER" +
                                            "         WHERE 1=1" +
                                            //" AND YYYYMM =" +
                                            //"                  TO_CHAR (ADD_MONTHS (TRUNC (SYSDATE, 'MONTH'), -1)," +
                                            //"                           'YYYYMM')" +
                                            ")";
            FuxtilNoOfCustomer FuxtilNoOfCustomer = new FuxtilNoOfCustomer();
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), FuxtilNoOfCustomerQry);
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                FuxtilNoOfCustomer.CM_FUXTIL_NO_OF_CUSTOMER = _row["CM_NO_OF_CUSTOMER"].ToString();
                FuxtilNoOfCustomer.LM_FUXTIL_NO_OF_CUSTOMER = _row["LM_NO_OF_CUSTOMER"].ToString();

            }
            return FuxtilNoOfCustomer;
        }
        public SweetDropsNoOfCustomer GetSweetDropsNoOfCustomer(string param)
        {
            string sweetDropsNoOfCustomerQry = "SELECT SUM (CM_NO_OF_CUSTOMER) CM_NO_OF_CUSTOMER," +
                                            "       SUM (LM_NO_OF_CUSTOMER) LM_NO_OF_CUSTOMER" +
                                            "  FROM (SELECT COUNT(DISTINCT CUSTOMER_CODE) CM_NO_OF_CUSTOMER, 0 LM_NO_OF_CUSTOMER" +
                                            "          FROM VW_SWTDROPS_CM_NO_OF_CUSTOMER" +
                                            "         WHERE 1=1 " +
                                            " AND YYYYMM = TO_CHAR (SYSDATE, 'YYYYMM')" +
                                            "        UNION ALL" +
                                            "        SELECT 0 CM_NO_OF_CUSTOMER, COUNT(DISTINCT CUSTOMER_CODE) LM_NO_OF_CUSTOMER" +
                                            "          FROM VW_SWTDROPS_LM_NO_OF_CUSTOMER" +
                                            "         WHERE 1=1" +
                                            //" AND YYYYMM =" +
                                            //"                  TO_CHAR (ADD_MONTHS (TRUNC (SYSDATE, 'MONTH'), -1)," +
                                            //"                           'YYYYMM')" +
                                            ")";
            SweetDropsNoOfCustomer sweetDropsNoOfCustomer = new SweetDropsNoOfCustomer();
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), sweetDropsNoOfCustomerQry);
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                sweetDropsNoOfCustomer.CM_SWEETDROPS_NO_OF_CUSTOMER = _row["CM_NO_OF_CUSTOMER"].ToString();
                sweetDropsNoOfCustomer.LM_SWEETDROPS_NO_OF_CUSTOMER = _row["LM_NO_OF_CUSTOMER"].ToString();

            }
            return sweetDropsNoOfCustomer;
        }
        public NewChemist GetNewChemist(string param)
        {

            string NewChemistQry = "SELECT SUM(TODAY_NEW_CHEMIST) TODAY_NEW_CHEMIST,SUM(TOTAL_NEW_CHEMIST_CM) TOTAL_NEW_CHEMIST_CM FROM (SELECT COUNT(DISTINCT CUSTOMER_CODE) TODAY_NEW_CHEMIST,0 TOTAL_NEW_CHEMIST_CM " +
                                            " FROM INVOICE_MST" +
                                            " WHERE TO_CHAR(INVOICE_DATE,'YYYYMMDD')=TO_CHAR(SYSDATE,'YYYYMMDD')" +
                                            " " + param + " AND CUSTOMER_CODE NOT IN (" +
                                            "                            SELECT DISTINCT CUSTOMER_CODE" +
                                            "                            FROM INVOICE_MST" +
                                            "                            WHERE TO_CHAR(INVOICE_DATE,'YYYYMMDD')<>TO_CHAR(SYSDATE,'YYYYMMDD')" +
                                            "                            )" +
                                            "                            " +
                                            " UNION ALL                      " +
                                            " SELECT 0 TODAY_NEW_CHEMIST,COUNT(DISTINCT CUSTOMER_CODE) TOTAL_NEW_CHEMIST_CM" +
                                            " FROM INVOICE_MST" +
                                            " WHERE TO_CHAR(INVOICE_DATE,'YYYYMM')=TO_CHAR(SYSDATE,'YYYYMM')" +
                                            "  " + param + " AND CUSTOMER_CODE NOT IN (" +
                                            "                            SELECT DISTINCT CUSTOMER_CODE" +
                                            "                            FROM INVOICE_MST" +
                                            "                            WHERE TO_CHAR(INVOICE_DATE,'YYYYMM')<>TO_CHAR(SYSDATE,'YYYYMM')))"; ;
            NewChemist newChemist = new NewChemist();
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), NewChemistQry);
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                newChemist.TOTAL_NEW_CHEMIST_CM = _row["TOTAL_NEW_CHEMIST_CM"].ToString();
                newChemist.TODAY_NEW_CHEMIST = _row["TODAY_NEW_CHEMIST"].ToString();

            }
            return newChemist;
        }
        public MPOCreditLimit GetMPOCreditLimit(string param)
        {

            string MPO_Credit_Limit_Qry = "SELECT ROUND(SUM(LIMIT_AMOUNT)/100000, 2)  TOTAL_LIMIT_AMOUNT FROM MPO_CREDIT_LIMIT WHERE LIMIT_YYYYMM = TO_CHAR(SYSDATE, 'YYYYMM')";
            MPOCreditLimit mPOCreditLimit = new MPOCreditLimit();
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), MPO_Credit_Limit_Qry);
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                mPOCreditLimit.TOTAL_LIMIT_AMOUNT = _row["TOTAL_LIMIT_AMOUNT"].ToString();

            }
            return mPOCreditLimit;
        }
    }


}