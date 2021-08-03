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
    public class HomeDashboardDAO : ReturnData
    {
        DBConnection _dbConn = new DBConnection();
        readonly DBHelper _dbHelper = new DBHelper();
        readonly DBConnection _dbConnection = new DBConnection();

        private DataRow _row;
        private DataTable _dt;

        public HomeDashboard GetDashboardData()
        {
            try
            {
                string CODE = HttpContext.Current.Session["CODE"].ToString();
                string ACCESS_LEVEL = HttpContext.Current.Session["ACCESS_LEVEL"].ToString();
                string accessLevelParam = "";
                HomeDashboard homeDashboard = new HomeDashboard();
                if (ACCESS_LEVEL == "N" || ACCESS_LEVEL == null)
                {
                    accessLevelParam = "";
                    //homeDashboard.ACCESS_LEVEL = "National";
                    homeDashboard.POSTING_LOCATION = "National";
                }
                else if (ACCESS_LEVEL == "Z")
                {
                    accessLevelParam = " AND DSM_CODE = '" + CODE + "'";
                }
                else if (ACCESS_LEVEL == "D")
                {
                    accessLevelParam = " AND DEPOT_CODE = '" + CODE + "'";
                    //accessLevelParam = "WHERE DEPOT_CODE = '" + CODE + "'";
                    //accessLevelParamStock = "AND DEPOT_CODE = '" + CODE + "'";
                    //homeDashboard.ACCESS_LEVEL = "Depot";
                    //
                    string depot_Name_Qry =
                        "SELECT UNIT_NAME FROM SC_COMP_UNIT WHERE UNIT_CODE NOT IN ('01','02') AND UNIT_CODE=" + CODE + " ";
                    //
                    _row = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), depot_Name_Qry).Rows[0];
                    homeDashboard.POSTING_LOCATION = _row["UNIT_NAME"].ToString();
                }
                else if (ACCESS_LEVEL == "R")
                {
                    accessLevelParam = " AND RSM_CODE = '" + CODE + "'";
                    //accessLevelParam = "WHERE RSM_CODE = '" + CODE + "'";
                    //homeDashboard.ACCESS_LEVEL = "Region";
                    string region_Name_Qry =
                        "SELECT REGION_NAME FROM VW_PAL_FIELD_FORCE_MIO WHERE  RSM_CODE=" + CODE + " ";
                    //
                    _row = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), region_Name_Qry).Rows[0];
                    homeDashboard.POSTING_LOCATION = _row["REGION_NAME"].ToString();
                }
                else if (ACCESS_LEVEL == "A")
                {
                    accessLevelParam = " AND AM_CODE = '" + CODE + "'";
                    //accessLevelParam = "WHERE AM_CODE = '" + CODE + "'";
                    //homeDashboard.ACCESS_LEVEL = "Area";
                    string area_Name_Qry =
                        "SELECT AREA_NAME FROM VW_PAL_FIELD_FORCE_MIO WHERE AM_CODE=" + CODE + " ";
                    //
                    _row = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), area_Name_Qry).Rows[0];
                    homeDashboard.POSTING_LOCATION = _row["AREA_NAME"].ToString();
                }
                else if (ACCESS_LEVEL == "T")
                {
                    accessLevelParam = " AND MPO_CODE = '" + CODE + "'";
                    //accessLevelParam = "WHERE MPO_CODE = '" + CODE + "'";
                    //homeDashboard.ACCESS_LEVEL = "Teritory";
                }

                homeDashboard.Target = GetTargetCurrentMonth(accessLevelParam);
                //if (CODE == "04256")
                //{
                    homeDashboard.YearlySales = GetYearlySales(accessLevelParam);
               // }
                homeDashboard.TodaySale = GetTodaySales(accessLevelParam);
                homeDashboard.UpToMonthSale = GetUpToMonthSales(accessLevelParam);
                homeDashboard.LMUpToDate = GetLMUpToDateSales(accessLevelParam);
                homeDashboard.TotalMpo = GetTotalMpo(accessLevelParam);
                homeDashboard.Growth = "";
                homeDashboard.Today_Collection_Amount = GetToDayCollection(accessLevelParam);
                homeDashboard.UpTo_Month_Collection = GetUpToMonthCollection(accessLevelParam);
                homeDashboard.TotalCustomer = GetTotalCustomer(accessLevelParam);
                homeDashboard.MaturedDue = GetMaturedDues(accessLevelParam);
                homeDashboard.ActiveMaturedDue = GetActiveMaturedDues(accessLevelParam);
                homeDashboard.DiscontinueMaturedDue = GetDiscontinueMaturedDues(accessLevelParam);
                homeDashboard.ImmaturedDue = GetImmaturedDue(accessLevelParam);
                homeDashboard.XelproSale = GetProductSalesValue(accessLevelParam, "DATE_WISE_XELPRO_SALES");
                homeDashboard.EzylifeVal = GetProductSalesValue(accessLevelParam + " AND PRODUCT_CODE IN ('C0170','C0171','C0175')", "DATE_WISE_PROD_SALES");
                //homeDashboard.FuxtilVal = GetProductSalesValue(accessLevelParam + " AND PRODUCT_CODE IN ('C0055','C0056','C0057','C0058','C0059','C0060','C0061','C0062')", "DATE_WISE_PROD_SALES");
                homeDashboard.SweetDropsVal = GetProductSalesValue(accessLevelParam + " AND PRODUCT_CODE IN ('C0191')", "DATE_WISE_PROD_SALES");
                homeDashboard.SweetDropsQTY = GetProductSalesQTY(accessLevelParam + " AND PRODUCT_CODE IN ('C0191')", "DATE_WISE_PROD_SALES");
                //homeDashboard.SweetDropsRtrn = GetProductRtrnQTY(accessLevelParam + " AND PRODUCT_CODE IN ('C0191')");
                homeDashboard.AllProdRtrn = GetAllProductRtrnQTY(accessLevelParam);
                homeDashboard.DCCTotalSale = GetDCC_Sale(accessLevelParam);
                //homeDashboard.C0165Sale = GetProdSalesData(accessLevelParam, "C0165");
                //homeDashboard.C0166Sale = GetProdSalesData(accessLevelParam, "C0166");
                //homeDashboard.C0170Sale = GetProdSalesData(accessLevelParam, "C0170");
                //homeDashboard.C0171Sale = GetProdSalesData(accessLevelParam, "C0171");
               // homeDashboard.C0191Sale = GetProdSalesData(accessLevelParam, "C0191");

                homeDashboard.MPOCreditLimit = GetMPOCreditLimit(accessLevelParam);
                homeDashboard.EzylifeNoOfCustomer = GetEzylifeNoOfCustomer(accessLevelParam);
                homeDashboard.XelproNoOfCustomer = GetXelproNoOfCustomer(accessLevelParam);
                //homeDashboard.FuxtilNoOfCustomer = GetFuxtilNoOfCustomer(accessLevelParam);
               // homeDashboard.SweetDropsNoOfCustomer = GetSweetDropsNoOfCustomer(accessLevelParam);
                homeDashboard.NewChemist = GetNewChemist(accessLevelParam);
                homeDashboard.ACCESS_LEVEL = ACCESS_LEVEL;
                //homeDashboard.WorldCupOffer = GetWorldCupOffer(accessLevelParam);
                //homeDashboard.EzylifeNoOfCustomer = GetEzylifeNoOfCustomer(accessLevelParam);
                if (ACCESS_LEVEL == "N" || ACCESS_LEVEL == "D")
                {
                    homeDashboard.Commercial_Stock_Valuation = GetCommercialStockValuation(accessLevelParam);
                    //homeDashboard.Sample_Stock_Valuation = GetSampleStockValuation(accessLevelParam);
                    //homeDashboard.PPM_Stock_Valuation = GetPPMStockValuation(accessLevelParam);

                    //homeDashboard.Gift_Stock_Valuation = GetGiftStockValuation(accessLevelParam);
                }
                homeDashboard.Growth = "";
                // ListReturn = GetBarChartData(accessLevelParam);
                return homeDashboard;
            }
            catch (Exception e)
            {
                ExceptionReturn = e.Message;
                var linenum = Convert.ToInt32(e.StackTrace.Substring(e.StackTrace.LastIndexOf(' ')));
                throw;
            }
        }

        private SweetDropsRtrn GetProductRtrnQTY(string param)
        {
            string target_Current_Month_Qry =
               "SELECT SUM (NVL (SALES_RETURN_QTY, 0)) RETURN_QTY," +
                "       ROUND(SUM (NVL (B.TOTAL_VALUE, 0) - NVL (B.INV_DISC_VALUE, 0)) / 100000, 2) RETURN_VALUE" +
                "  FROM RETURN_RECEIVE_MST A, RETURN_RECEIVE_DTL B, INVOICE_MST C" +
                "  WHERE     A.RET_RECV_MST_SLNO = B.RET_RECV_MST_SLNO" +
                "       AND A.INVOICE_NO = C.INVOICE_NO" +
                // "       AND B.PRODUCT_CODE IN ('C0191')" +
               param +
                "       AND INVOICE_DATE BETWEEN TRUNC (SYSDATE, 'MM') AND TRUNC (SYSDATE)";
            SweetDropsRtrn sweetDropsRtrn = new SweetDropsRtrn();
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), target_Current_Month_Qry);
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                sweetDropsRtrn.SWEET_DROPS_RETURN_QTY = _row["RETURN_QTY"].ToString();
                sweetDropsRtrn.SWEET_DROPS_RETURN_VALUE = _row["RETURN_VALUE"].ToString();
            }
            return sweetDropsRtrn;
        }
        private AllProdRtrn GetAllProductRtrnQTY(string param)
        {
            string target_Current_Month_Qry =
               "SELECT SUM (NVL (SALES_RETURN_QTY, 0)) RETURN_QTY," +
                "       ROUND(SUM (NVL (B.TOTAL_VALUE, 0) - NVL (B.INV_DISC_VALUE, 0)) / 100000, 2) RETURN_VALUE" +
                "  FROM RETURN_RECEIVE_MST A, RETURN_RECEIVE_DTL B, INVOICE_MST C" +
                "  WHERE     A.RET_RECV_MST_SLNO = B.RET_RECV_MST_SLNO" +
                "       AND A.INVOICE_NO = C.INVOICE_NO" +
                // "       AND B.PRODUCT_CODE IN ('C0191')" +
               param +
                "       AND INVOICE_DATE BETWEEN TRUNC (SYSDATE, 'MM') AND TRUNC (SYSDATE)";
            AllProdRtrn allProdRtrn = new AllProdRtrn();
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), target_Current_Month_Qry);
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                allProdRtrn.ALL_PROD_RETURN_QTY = _row["RETURN_QTY"].ToString();
                allProdRtrn.ALL_PROD_RETURN_VALUE = _row["RETURN_VALUE"].ToString();
            }
            return allProdRtrn;
        }

        public Target GetTargetCurrentMonth(string param)
        {
            string target_Current_Month_Qry =
                "SELECT INITCAP(TO_CHAR(TO_DATE(MONTH_CODE, 'MM'), 'MONTH')) MONTH_NAME,ROUND(SUM(TARGET_AMT)/100000,2) TARGET_CURRENT_MONTH FROM TARGET_DTL WHERE TO_CHAR(TRUNC(SYSDATE),'yyyyMM')=YYYYMM  " +
                param + "  GROUP BY MONTH_CODE  ";
            Target target = new Target();
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), target_Current_Month_Qry);
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                target.Target_Current_Month = _row["TARGET_CURRENT_MONTH"].ToString();
                target.Target_Current_Month_Name = _row["MONTH_NAME"].ToString();
            }
            //else
            //{
            //    target.Target_Current_Month = "";
            //    target.Target_Current_Month_Name = "";
            //}


            return target;
        }

        public TodaySale GetTodaySales(string param)
        {
            string today_Sales_Qry =
                "select ROUND(SUM(NET_INV_AMT_CA)/100000,2) TODAY_NET_INV_AMT_CA, ROUND(SUM(NET_INV_AMT_CR)/100000,2) TODAY_NET_INV_AMT_CR, ROUND((SUM(NET_INV_AMT_CA) + SUM(NET_INV_AMT_CR))/100000,2) TODAY_TOTAL_SALES" +
                " from ( select MOP, SUM(NET_INV_AMT) NET_INV_AMT_CA, 0 NET_INV_AMT_CR  from DATE_WISE_SALES where INVOICE_DATE = TRUNC(SYSDATE) AND MOP = 'CA'  " +
                param + " " +
                " GROUP BY  MOP  UNION ALL select MOP, 0 NET_INV_AMT_CA, SUM(NET_INV_AMT) NET_INV_AMT_CR  from DATE_WISE_SALES where INVOICE_DATE = TRUNC(SYSDATE) AND MOP = 'CR' " +
                param + " " +
                " GROUP BY  MOP )";
            TodaySale todaySale = new TodaySale();
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), today_Sales_Qry);
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];

                todaySale.Today_Sales_CA = _row["TODAY_NET_INV_AMT_CA"].ToString();
                todaySale.Today_Sales_CR = _row["TODAY_NET_INV_AMT_CR"].ToString();
                todaySale.Today_Sales = _row["TODAY_TOTAL_SALES"].ToString();
            }

            return todaySale;
        }

        public UpToMonthSale GetUpToMonthSales(string param)
        {
            string upToMonth_Sales_Amnt_Qry =
                "select ROUND(SUM(NET_INV_AMT_CA)/100000,2) UPTO_NET_INV_AMT_CA, ROUND(SUM(NET_INV_AMT_CR)/100000,2) UPTO_NET_INV_AMT_CR, ROUND((SUM(NET_INV_AMT_CA) + SUM(NET_INV_AMT_CR))/100000,2) UPTO_TOTAL_SALES" +
                " from ( select MOP, SUM(NET_INV_AMT) NET_INV_AMT_CA, 0 NET_INV_AMT_CR  from DATE_WISE_SALES where INVOICE_DATE between TRUNC (SYSDATE, 'MM') AND TRUNC (SYSDATE)  AND MOP = 'CA' " +
                param + " " +
                " GROUP BY  MOP  UNION ALL select MOP, 0 NET_INV_AMT_CA, SUM(NET_INV_AMT) NET_INV_AMT_CR  from DATE_WISE_SALES where INVOICE_DATE between TRUNC (SYSDATE, 'MM') AND TRUNC (SYSDATE)  AND MOP = 'CR' " +
                param + " " +
                " GROUP BY  MOP )";

            UpToMonthSale upToMonthSale = new UpToMonthSale();
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), upToMonth_Sales_Amnt_Qry);
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                //row = dbHelper.GetDataTable(dbConnection.SAConnStrReader("Dashboard"), upToMonth_Sales_Amnt_Qry).Rows[0];
                upToMonthSale.UpTo_Month_Total_Sales_CA = _row["UPTO_NET_INV_AMT_CA"].ToString();
                upToMonthSale.UpTo_Month_Total_Sales_CR = _row["UPTO_NET_INV_AMT_CR"].ToString();
                upToMonthSale.UpTo_Month_Total_Sales = _row["UPTO_TOTAL_SALES"].ToString();
            }

            return upToMonthSale;
        }
        public LMUpToDate GetLMUpToDateSales(string param)
        {
            string upToMonth_Sales_Amnt_Qry =
                "select ROUND(SUM(NET_INV_AMT_CA)/100000,2) LMTO_NET_INV_AMT_CA, ROUND(SUM(NET_INV_AMT_CR)/100000,2) LMTO_NET_INV_AMT_CR, ROUND((SUM(NET_INV_AMT_CA) + SUM(NET_INV_AMT_CR))/100000,2) LMTO_TOTAL_SALES" +
                " from ( select MOP, SUM(NET_INV_AMT) NET_INV_AMT_CA, 0 NET_INV_AMT_CR  from DATE_WISE_SALES where INVOICE_DATE between TRUNC(ADD_MONTHS(SYSDATE, -1),'MM') AND TRUNC(add_months(sysdate, -1))  AND MOP = 'CA' " +
                param + " " +
                " GROUP BY  MOP  UNION ALL select MOP, 0 NET_INV_AMT_CA, SUM(NET_INV_AMT) NET_INV_AMT_CR  from DATE_WISE_SALES where INVOICE_DATE between TRUNC(ADD_MONTHS(SYSDATE, -1),'MM') AND TRUNC(add_months(sysdate, -1))  AND MOP = 'CR' " +
                param + " " +
                " GROUP BY  MOP )";

            LMUpToDate lmUpToDate = new LMUpToDate();
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), upToMonth_Sales_Amnt_Qry);
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                //row = dbHelper.GetDataTable(dbConnection.SAConnStrReader("Dashboard"), upToMonth_Sales_Amnt_Qry).Rows[0];
                lmUpToDate.LM_UP_ToDate_Total_Sales_CA = _row["LMTO_NET_INV_AMT_CA"].ToString();
                lmUpToDate.LM_UP_ToDate_Total_Sales_CR = _row["LMTO_NET_INV_AMT_CR"].ToString();
                lmUpToDate.LM_UP_ToDate_Total_Sales = _row["LMTO_TOTAL_SALES"].ToString();
            }

            return lmUpToDate;
        }

        public TotalMPO GetTotalMpo(string param)
        {
            string Total_Mpo_Qry = "SELECT SUM(DISTINCT LM_MPO) TOTAL_LM_MPO,SUM(DISTINCT CM_MPO) TOTAL_CM_MPO FROM( " +
                                   " SELECT 0 LM_MPO,COUNT(DISTINCT MPO_CODE) CM_MPO FROM CM_TOTAL_MPO WHERE 1=1  " +
                                   param + " " +
                                   " UNION ALL " +
                                   " SELECT COUNT(DISTINCT MPO_CODE) LM_MPO,0 CM_MPO FROM LM_TOTAL_MPO WHERE 1=1  " +
                                   param + " )";


            TotalMPO totalMpo = new TotalMPO();
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), Total_Mpo_Qry);
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                //row = dbHelper.GetDataTable(dbConnection.SAConnStrReader("Dashboard"), Total_Mpo_Qry).Rows[0];
                totalMpo.CM_Total_MPO = _row["TOTAL_CM_MPO"].ToString();
                totalMpo.LM_Total_MPO = _row["TOTAL_LM_MPO"].ToString();
            }

            return totalMpo;
        }

        public string GetToDayCollection(string param)
        {
            string TODAY_COLLECTION_AMT = "";
            string today_Collection_Amnt_Qry =
                "select ROUND(SUM(COLLECTION_AMT)/100000,2) TODAY_COLLECTION_AMT from DATE_WISE_COLLECTION where COLLECT_DATE =TRUNC(SYSDATE) " +
                param + " ";
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), today_Collection_Amnt_Qry);
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                //row = dbHelper.GetDataTable(dbConnection.SAConnStrReader("Dashboard"), today_Collection_Amnt_Qry).Rows[0];
                TODAY_COLLECTION_AMT = _row["TODAY_COLLECTION_AMT"].ToString();
            }

            return TODAY_COLLECTION_AMT;
        }

        public string GetUpToMonthCollection(string param)
        {
            string UPTO_COLLECTION_AMT = "";
            string upToMonth_Collection_Amnt_Qry =
                "select ROUND(SUM(COLLECTION_AMT)/100000,2) UPTO_COLLECTION_AMT from DATE_WISE_COLLECTION" +
                " where COLLECT_DATE between TO_DATE (TRUNC((SYSDATE),'MONTH'),'DD/MM/RRRR') AND TRUNC(SYSDATE) " +
                //" where COLLECT_DATE between TO_DATE('01/' || to_char(trunc(sysdate), 'mm/yyyy'), 'dd/mm/yyyy') AND TRUNC(SYSDATE) " +
                param + " ";
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), upToMonth_Collection_Amnt_Qry);
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                //row = dbHelper.GetDataTable(dbConnection.SAConnStrReader("Dashboard"), upToMonth_Collection_Amnt_Qry).Rows[0];
                UPTO_COLLECTION_AMT = _row["UPTO_COLLECTION_AMT"].ToString();
            }

            return UPTO_COLLECTION_AMT;
        }

        public TotalCustomer GetTotalCustomer(string param)
        {
            string Total_Customer_Qry =
                " SELECT SUM(LM_Customer) LM_Total_Customer,SUM(CM_Customer) CM_Total_Customer FROM " +
                " (SELECT 0 LM_Customer, COUNT(DISTINCT CUSTOMER_CODE) CM_Customer FROM CM_TOTAL_CUSTOMER WHERE 1 = 1 " +
                param + " " +
                " UNION ALL SELECT COUNT(DISTINCT CUSTOMER_CODE) LM_Customer, 0 CM_Customer FROM LM_TOTAL_CUSTOMER WHERE 1 = 1 " +
                param + ") ";
            TotalCustomer totalCustomer = new TotalCustomer();
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), Total_Customer_Qry);
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                // row = dbHelper.GetDataTable(dbConnection.SAConnStrReader("Dashboard"), Total_Customer_Qry).Rows[0];
                totalCustomer.CM_Total_Customer = _row["CM_Total_Customer"].ToString();
                totalCustomer.LM_Total_Customer = _row["LM_Total_Customer"].ToString();
            }

            return totalCustomer;
        }

        public YearlySales GetYearlySales(string param)
        {
            string Matured_Dues_Qry =
                            "SELECT ROUND (" +
            "          SUM (NVL (NET_INV_VALUE, 0) - NVL (NET_RETURN_VALUE, 0)) / 100000," +
            "          2)" +
            "          NET_INV_AMT" +
            "  FROM INVOICE_MST" +
            " WHERE     INV_TYPE_CODE IN ('INV001', 'INV002', 'INV004')" +
            "       AND TO_CHAR (INVOICE_DATE, 'YYYY') = TO_CHAR (SYSDATE, 'YYYY') "+ param;

            YearlySales yearlySales = new YearlySales();
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), Matured_Dues_Qry);
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                //row = dbHelper.GetDataTable(dbConnection.SAConnStrReader("Dashboard"), Matured_Dues_Qry).Rows[0];
                yearlySales.Net_Yearly_Sales = _row["NET_INV_AMT"].ToString();
            }

            return yearlySales;
        }
        public MaturedDue GetMaturedDues(string param)
        {
            string Matured_Dues_Qry =
                "select ROUND(sum(MATURE_DUES_AMT_CA)/100000,2) MATURE_DUES_AMT_CA, ROUND(sum(MATURE_DUES_AMT_CR)/100000,2) MATURE_DUES_AMT_CR, ROUND((sum(MATURE_DUES_AMT_CA) + sum(MATURE_DUES_AMT_CR))/100000,2) TOTAL_MATURE_DUES_AMT " +
                " from ( select MODE_OF_PAYMENT, SUM(MATURE_DUES_AMT) MATURE_DUES_AMT_CA, 0 MATURE_DUES_AMT_CR  from MATURE_DUES where MODE_OF_PAYMENT = 'Cash' " +
                param + "" +
                " GROUP BY  MODE_OF_PAYMENT UNION ALL select MODE_OF_PAYMENT, 0 MATURE_DUES_AMT_CA, SUM(MATURE_DUES_AMT) MATURE_DUES_AMT_CR  from MATURE_DUES where MODE_OF_PAYMENT = 'Credit' " +
                param + "" +
                " GROUP BY  MODE_OF_PAYMENT)  ";
            MaturedDue maturedDue = new MaturedDue();
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), Matured_Dues_Qry);
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                //row = dbHelper.GetDataTable(dbConnection.SAConnStrReader("Dashboard"), Matured_Dues_Qry).Rows[0];
                maturedDue.Matured_Dues = _row["TOTAL_MATURE_DUES_AMT"].ToString();
                maturedDue.Matured_Dues_CA = _row["MATURE_DUES_AMT_CA"].ToString();
                maturedDue.Matured_Dues_CR = _row["MATURE_DUES_AMT_CR"].ToString();
            }

            return maturedDue;
        }

        public ActiveMaturedDue GetActiveMaturedDues(string param)
        {
            string Active_Matured_Dues_Qry =
                "select ROUND(sum(MATURE_DUES_AMT_CA)/100000,2) Active_MATURE_DUES_AMT_CA, ROUND(sum(MATURE_DUES_AMT_CR)/100000,2) Active_MATURE_DUES_AMT_CR, ROUND((sum(MATURE_DUES_AMT_CA) + sum(MATURE_DUES_AMT_CR))/100000,2) TOTAL_ACTIVE_MATURE_DUES_AMT " +
                " from ( select MODE_OF_PAYMENT, SUM(MATURE_DUES_AMT) MATURE_DUES_AMT_CA, 0 MATURE_DUES_AMT_CR  from ACTIVE_MATURE_DUES where MODE_OF_PAYMENT = 'Cash' " +
                param + "" +
                " GROUP BY  MODE_OF_PAYMENT UNION ALL select MODE_OF_PAYMENT, 0 MATURE_DUES_AMT_CA, SUM(MATURE_DUES_AMT) MATURE_DUES_AMT_CR  from ACTIVE_MATURE_DUES where MODE_OF_PAYMENT = 'Credit' " +
                param + "" +
                " GROUP BY  MODE_OF_PAYMENT)  ";
            //
            ActiveMaturedDue activeMaturedDue = new ActiveMaturedDue();
            _dt = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), Active_Matured_Dues_Qry);
            if (_dt.Rows.Count > 0)
            {
                _row = _dt.Rows[0];
                //row = dbHelper.GetDataTable(dbConnection.SAConnStrReader("Dashboard"), Active_Matured_Dues_Qry).Rows[0];
                activeMaturedDue.Active_Matured_Dues = _row["TOTAL_ACTIVE_MATURE_DUES_AMT"].ToString();
                activeMaturedDue.Active_Matured_Dues_CA = _row["Active_MATURE_DUES_AMT_CA"].ToString();
                activeMaturedDue.Active_Matured_Dues_CR = _row["Active_MATURE_DUES_AMT_CR"].ToString();
            }

            return activeMaturedDue;
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