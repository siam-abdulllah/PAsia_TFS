using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using PAsia_Dashboard.Areas.Dashboard.Models.BEL;
using PAsia_Dashboard.Areas.Dashboard.Models.DAO;
using PAsia_Dashboard.Areas.Reports.Models.BEl;
using PAsia_Dashboard.Universal.Gateway;

namespace PAsia_Dashboard.Areas.Reports.Models.DAO
{
    public class RSMWiseSalesAchievementDAO
    {
        HomeDashboardDAO homeDashboardDao = new HomeDashboardDAO();
        DBHelper dbHelper = new DBHelper();
        DBConnection dbConn = new DBConnection();
        public object GetRSMeWiseSalesProductAchievement(string fromDate, string toDate)
        {
            throw new NotImplementedException();
        }

        public List<RSMWiseSalesAchievementValue> GetRSMWiseSalesValueAchievement(string fromDate, string toDate)
        {
            string CODE = HttpContext.Current.Session["CODE"].ToString();
            string ACCESS_LEVEL = HttpContext.Current.Session["ACCESS_LEVEL"].ToString();
            string accessLevelParam = " ";
            if (ACCESS_LEVEL == "N" || ACCESS_LEVEL == null)
            {
                accessLevelParam = " ";
                ///homeDashboard.ACCESS_LEVEL = " National" ;
            }
            else if (ACCESS_LEVEL == "Z")
            {
                accessLevelParam = " AND DSM_CODE = '" + CODE + "'";
            }
            else if (ACCESS_LEVEL == "R")
            {
                accessLevelParam = " AND RSM_CODE = '" + CODE + "'";
            }

            #region MyRegion

            string RValueQry = " SELECT row_number() OVER (ORDER BY ZONE_CODE) SL_NO, " +
            //"       FN_DEPOT_NAME(DEPOT_CODE)DEPOT_NAME," +
           "       (SELECT DISTINCT EMP_CODE DSM_CODE FROM VW_FIELD_FORCE WHERE LOC_CODE=A.ZONE_CODE AND POSTING_LOCATION='Z') DSM_CODE," +
            "       FN_EMPLOYEE_NAME((SELECT DISTINCT EMP_CODE DSM_CODE FROM VW_FIELD_FORCE WHERE LOC_CODE=A.ZONE_CODE AND POSTING_LOCATION='Z')) DSM_NAME," +
            //"       DSM_CODE," +
            //"       FN_EMPLOYEE_NAME(DSM_CODE) DSM_NAME," +
            "       A.ZONE_CODE," +
            "       FN_ZONE_NAME(ZONE_CODE) ZONE_NAME," +
            "       (SELECT DISTINCT EMP_CODE DSM_CODE FROM VW_FIELD_FORCE WHERE LOC_CODE=A.REGION_CODE AND POSTING_LOCATION='R') RSM_CODE," +
            "       FN_EMPLOYEE_NAME((SELECT DISTINCT EMP_CODE DSM_CODE FROM VW_FIELD_FORCE WHERE LOC_CODE=A.REGION_CODE AND POSTING_LOCATION='R')) RSM_NAME," +
            //"       RSM_CODE," +
           // "       FN_EMPLOYEE_NAME(RSM_CODE) RSM_NAME," +
            "       REGION_CODE," +
            "       FN_AREA_NAME(REGION_CODE) REGION_NAME," +
            "       RETURN_VALUE," +
            "       TARGET_AMT," +
            "       TO_DAY_SALES," +
            "       UPTO_SALES," +
            "       ACH," +
            "       LM_UPTO_SALES," +
            "       GROWTH," +
            "       CM_MPO," +
            "       LM_MPO," +
            "       CM_CUST," +
            "       LM_CUST" +
            " FROM" +
            "   ( " +
            "    SELECT " +
            "           ZONE_CODE," +
            "           REGION_CODE," +
            //"DSM_CODE,RSM_CODE," +
            "           SUM (NVL (RETURN_VALUE, 0)) RETURN_VALUE," +
            "           SUM(NVL(TARGET_AMT,0)) TARGET_AMT ," +
            "           SUM(NVL(TO_DAY_SALES,0)) TO_DAY_SALES," +
            "           SUM(NVL(UPTO_SALES,0)) UPTO_SALES," +
            "           ROUND(DECODE(SUM(NVL(TARGET_AMT,0)),0,0,((SUM(NVL(UPTO_SALES,0)) * 100)/ SUM(NVL(TARGET_AMT,0)))),2) ACH," +
            "           ROUND(SUM(NVL(LM_UPTO_SALES,0))) LM_UPTO_SALES," +
            "           SUM(NVL(UPTO_SALES,0)) - SUM(NVL(LM_UPTO_SALES,0)) GROWTH," +
            "           SUM(NVL(CM_MPO,0)) CM_MPO," +
            "           SUM(NVL(LM_MPO,0)) LM_MPO,                     " +
            "           SUM(NVL(CM_CUST,0))CM_CUST," +
            "           SUM(NVL(LM_CUST,0))LM_CUST       " +
            "    FROM" +
            "       (       " +
            "    SELECT " +
            "           ZONE_CODE," +
            "           REGION_CODE," +
            //"DSM_CODE,RSM_CODE," +
            "          SUM (NVL(B.TOTAL_VALUE, 0) - NVL(B.INV_DISC_VALUE, 0)) RETURN_VALUE," +
            "           0 TARGET_AMT," +
            "           0 TO_DAY_SALES," +
            "           0 UPTO_SALES," +
            "           0 LM_UPTO_SALES," +
            "           0 CM_MPO ," +
            "           0 LM_MPO,           " +
            "           0 CM_CUST," +
            "           0 LM_CUST         " +
            "    FROM RETURN_RECEIVE_MST A,RETURN_RECEIVE_DTL B,INVOICE_MST C" +
            "    WHERE A.RET_RECV_MST_SLNO = B.RET_RECV_MST_SLNO AND A.INVOICE_NO = C.INVOICE_NO AND INV_TYPE_CODE IN ('INV001', 'INV002')" +
           "    AND   TO_DATE (INVOICE_DATE, 'DD/MM/RRRR') BETWEEN  TO_DATE('" + fromDate + "','DD/MM/RRRR') AND TO_DATE('" + toDate + "','DD/MM/RRRR') " + accessLevelParam + "" +
            "    GROUP BY ZONE_CODE,REGION_CODE" +
            //" ,DSM_CODE,RSM_CODE" +
            "    UNION ALL" +
            "    SELECT " +
            "           ZONE_CODE," +
            "           REGION_CODE," +
            //"DSM_CODE,RSM_CODE," +
            "          0 RETURN_VALUE," +
            "           SUM(NVL(TARGET_AMT,0)) TARGET_AMT," +
            "           0 TO_DAY_SALES," +
            "           0 UPTO_SALES," +
            "           0 LM_UPTO_SALES," +
            "           0 CM_MPO ," +
            "           0 LM_MPO,           " +
            "           0 CM_CUST," +
            "           0 LM_CUST         " +
            "    FROM TARGET_DTL_ALL" +
            "    WHERE YYYYMM BETWEEN TO_CHAR(TO_DATE('" + fromDate + "','DD/MM/RRRR'),'YYYYMM') AND TO_CHAR(TO_DATE('" + toDate + "','DD/MM/RRRR'),'YYYYMM') " + accessLevelParam + "" +
            "    GROUP BY ZONE_CODE,REGION_CODE" +
            //" ,DSM_CODE,RSM_CODE" +
            "    UNION ALL" +
            "    SELECT " +
            "           ZONE_CODE," +
            "           REGION_CODE," +
            // "  DSM_CODE,RSM_CODE," +
            "          0 RETURN_VALUE," +
            "           0 TARGET_AMT," +
            "           SUM(NVL(NET_INV_VALUE,0) - NVL(NET_RETURN_VALUE,0)) TO_DAY_SALES," +
            "           0 UPTO_SALES," +
            "           0 LM_UPTO_SALES," +
            "           0 CM_MPO ," +
            "           0 LM_MPO,           " +
            "           0 CM_CUST," +
            "           0 LM_CUST           " +
            "    FROM  INVOICE_MST A" +
            "    WHERE INV_TYPE_CODE IN ('INV001', 'INV002')" +
            "    AND   TO_DATE (INVOICE_DATE, 'DD/MM/RRRR') = TO_DATE('" + toDate + "','DD/MM/RRRR') " + accessLevelParam + "" +
            "    GROUP BY ZONE_CODE,REGION_CODE" +
           // " ,DSM_CODE,RSM_CODE" +
            "    UNION ALL" +
            "    SELECT " +
            "           ZONE_CODE," +
            "           REGION_CODE," +
            //" DSM_CODE,RSM_CODE," +
            "          0 RETURN_VALUE," +
            "           0 TARGET_AMT," +
            "           0 TO_DAY_SALES," +
            "           SUM(NVL(NET_INV_VALUE,0) - NVL(NET_RETURN_VALUE,0)) UPTO_SALES ," +
            "           0 LM_UPTO_SALES," +
            "           0 CM_MPO ," +
            "           0 LM_MPO,           " +
            "           0 CM_CUST," +
            "           0 LM_CUST                " +
            "    FROM  INVOICE_MST A" +
            "    WHERE INV_TYPE_CODE IN ('INV001', 'INV002')" +
            "    AND   TO_DATE (INVOICE_DATE, 'DD/MM/RRRR') BETWEEN  TO_DATE('" + fromDate + "','DD/MM/RRRR') AND TO_DATE('" + toDate + "','DD/MM/RRRR') " + accessLevelParam + "" +
            "    GROUP BY ZONE_CODE,REGION_CODE" +
           // " ,DSM_CODE,RSM_CODE" +
            "    UNION ALL" +
            "    SELECT " +
            "           ZONE_CODE," +
            "           REGION_CODE," +
            //" DSM_CODE,RSM_CODE," +
            "          0 RETURN_VALUE," +
            "           0 TARGET_AMT," +
            "           0 TO_DAY_SALES," +
            "           0 UPTO_SALES,          " +
            "           SUM(NVL(NET_INV_VALUE,0) - NVL(NET_RETURN_VALUE,0)) LM_UPTO_SALES, " +
            "           0 CM_MPO ," +
            "           0 LM_MPO,            " +
            "           0 CM_CUST," +
            "           0 LM_CUST                 " +
            "    FROM  INVOICE_MST A" +
            "    WHERE INV_TYPE_CODE IN ('INV001', 'INV002')" +
            "    AND   TO_DATE (INVOICE_DATE, 'DD/MM/RRRR') BETWEEN  ADD_MONTHS(TO_DATE('" + fromDate + "','DD/MM/RRRR'),-1) AND ADD_MONTHS(TO_DATE('" + toDate + "','DD/MM/RRRR'),-1) " + accessLevelParam + "" +
            "    GROUP BY ZONE_CODE,REGION_CODE" +
            //"   ,DSM_CODE,RSM_CODE" +
            "    UNION ALL" +
            "    SELECT " +
            "           ZONE_CODE," +
            "           REGION_CODE," +
            // " DSM_CODE,RSM_CODE," +
            "          0 RETURN_VALUE," +
            "           0 TARGET_AMT," +
            "           0 TO_DAY_SALES," +
            "           0 UPTO_SALES," +
            "           0 LM_UPTO_SALES," +
            "           0 CM_MPO ," +
            "           0 LM_MPO,           " +
            "           COUNT(DISTINCT CUSTOMER_CODE) CM_CUST," +
            "           0 LM_CUST                 " +
            "    FROM  INVOICE_MST A" +
            "    WHERE INV_TYPE_CODE IN ('INV001', 'INV002')" +
            "    AND   TO_DATE (INVOICE_DATE, 'DD/MM/RRRR') BETWEEN  TO_DATE('" + fromDate + "','DD/MM/RRRR') AND TO_DATE('" + toDate + "','DD/MM/RRRR') " + accessLevelParam + "" +
            "    GROUP BY ZONE_CODE,REGION_CODE" +
            //" ,DSM_CODE,RSM_CODE" +
            "    UNION ALL" +
            "    SELECT " +
            "           ZONE_CODE," +
            "           REGION_CODE," +
            //"DSM_CODE,RSM_CODE," +
            "          0 RETURN_VALUE," +
            "           0 TARGET_AMT," +
            "           0 TO_DAY_SALES," +
            "           0 UPTO_SALES," +
            "           0 LM_UPTO_SALES," +
            "           0 CM_MPO ," +
            "           0 LM_MPO,           " +
            "           0 CM_CUST," +
            "           COUNT(DISTINCT CUSTOMER_CODE) LM_CUST    " +
            "    FROM  INVOICE_MST A" +
            "    WHERE INV_TYPE_CODE IN ('INV001', 'INV002')" +
            "    AND   TO_DATE (INVOICE_DATE, 'DD/MM/RRRR') BETWEEN  ADD_MONTHS(TO_DATE('" + fromDate + "','DD/MM/RRRR'),-1) AND ADD_MONTHS(TO_DATE('" + toDate + "','DD/MM/RRRR'),-1) " + accessLevelParam + "" +
            "    GROUP BY ZONE_CODE,REGION_CODE" +
            //",DSM_CODE,RSM_CODE" +
            " UNION ALL" +
            " SELECT A.ZONE_CODE," +
            "                           A.REGION_CODE," +
            // " A.DSM_CODE,A.RSM_CODE," +
            "                           0 RETURN_VALUE," +
            "                           0 TARGET_AMT," +
            "                           0 TO_DAY_SALES," +
            "                           0 UPTO_SALES," +
            "                           0 LM_UPTO_SALES," +
            "                           COUNT (DISTINCT A.MPO_CODE) CM_MPO," +
            "                           0 LM_MPO," +
            "                           0 CM_CUST," +
            "                           0 LM_CUST" +
            "                      FROM INVOICE_MST A,EMPLOYEE_INFO B" +
            "                     WHERE   A.MPO_CODE=B.EMPLOYEE_CODE" +
            "                     AND B.STATUS='A'  " +
            "                     AND A.INV_TYPE_CODE IN ('INV001', 'INV002')" +
            "                           AND TO_DATE (A.INVOICE_DATE, 'DD/MM/RRRR') BETWEEN TO_DATE('" + fromDate + "','DD/MM/RRRR') AND TO_DATE('" + toDate + "','DD/MM/RRRR') " + accessLevelParam + "" +
            "                           AND A.MPO_CODE IN (SELECT EMPLOYEE_CODE" +
            "                                              FROM EMPLOYEE_INFO" +
            "                                             WHERE DESIGNATION IN ('003'," +
            "                                                                   '004'," +
            "                                                                   '014'," +
            "                                                                   '015'," +
            "                                                                   '017'))" +
            "                  GROUP BY A.ZONE_CODE, A.REGION_CODE" +
           // " ,A.DSM_CODE,A.RSM_CODE" +
            "                  UNION ALL" +
            " SELECT " +
            "       ZONE_CODE," +
            "       REGION_CODE," +
            //" DSM_CODE,RSM_CODE," +
            "          0 RETURN_VALUE," +
            "       0 TARGET_AMT," +
            "       0 TO_DAY_SALES," +
            "       0 UPTO_SALES," +
            "       0 LM_UPTO_SALES," +
            "       0 CM_MPO," +
            "       COUNT(DISTINCT MPO_CODE) LM_MPO," +
            "       0 CM_CUST," +
            "       0 LM_CUST                  " +
            " FROM  INVOICE_MST A" +
            " WHERE INV_TYPE_CODE IN ('INV001', 'INV002')" +
            " AND   TO_DATE (INVOICE_DATE, 'DD/MM/RRRR') BETWEEN  ADD_MONTHS(TO_DATE('" + fromDate + "','DD/MM/RRRR'),-1) AND ADD_MONTHS(TO_DATE('" + toDate + "','DD/MM/RRRR'),-1) " + accessLevelParam + "" +
            " AND MPO_CODE IN (SELECT EMPLOYEE_CODE" +
            "                                     FROM EMPLOYEE_INFO" +
            "                                    WHERE DESIGNATION IN ('003'," +
            "                                                          '004'," +
            "                                                          '014'," +
            "                                                          '015'," +
            "                                                          '017'))" +
            " GROUP BY ZONE_CODE,REGION_CODE" +
            //" ,DSM_CODE,RSM_CODE" +
            "  )" +
            "  GROUP BY  ZONE_CODE,REGION_CODE" +
           // " ,DSM_CODE,RSM_CODE" +
            " ) A";

            #endregion
            DataTable DCSdt = dbHelper.GetDataTable(dbConn.SAConnStrReader("Dashboard"), RValueQry);
            var rsmWiseSalesAchievementValue = (from DataRow row in DCSdt.Rows
                                                select new RSMWiseSalesAchievementValue()
                                                {
                                                    SL_No = row["SL_NO"].ToString(),
                                                    //DEPOT_CODE = row["DEPOT_CODE"].ToString(),
                                                    //DEPOT_NAME = row["DEPOT_NAME"].ToString(),
                                                    REGION_CODE = row["REGION_CODE"].ToString(),
                                                    REGION_NAME = row["REGION_NAME"].ToString(),
                                                    RSM_CODE = row["RSM_CODE"].ToString(),
                                                    RSM_NAME = row["RSM_NAME"].ToString(),
                                                    DSM_CODE = row["DSM_CODE"].ToString(),
                                                    DSM_NAME = row["DSM_NAME"].ToString(),
                                                    ZONE_CODE = row["ZONE_CODE"].ToString(),
                                                    //ZONE_NAME = row["ZONE_NAME"].ToString(),
                                                    RETURN_VALUE = row["RETURN_VALUE"].ToString(),
                                                    TARGET_AMT = row["TARGET_AMT"].ToString(),
                                                    TO_DAY_SALES = row["TO_DAY_SALES"].ToString(),
                                                    UPTO_SALES = row["UPTO_SALES"].ToString(),
                                                    ACH = row["ACH"].ToString(),
                                                    LM_UPTO_SALES = row["LM_UPTO_SALES"].ToString(),
                                                    GROWTH = row["GROWTH"].ToString(),
                                                    CM_MPO = row["CM_MPO"].ToString(),
                                                    LM_MPO = row["LM_MPO"].ToString(),
                                                    CM_CUST = row["CM_CUST"].ToString(),
                                                    LM_CUST = row["LM_CUST"].ToString()
                                                }).ToList();
            return rsmWiseSalesAchievementValue;
        }
        public HomeDashboard GetDashboardData()
        {
            string CODE = HttpContext.Current.Session["CODE"].ToString();
            string ACCESS_LEVEL = HttpContext.Current.Session["ACCESS_LEVEL"].ToString();
            string accessLevelParam = " ";
            if (ACCESS_LEVEL == "N" || ACCESS_LEVEL == null)
            {
                accessLevelParam = " ";


                ///homeDashboard.ACCESS_LEVEL = " National" ;
            }

            else if (ACCESS_LEVEL == "R")
            {
                accessLevelParam = "AND RSM_CODE = '" + CODE + "'";
            }
            HomeDashboard homeDashboard = new HomeDashboard
            {
                Commercial_Stock_Valuation = homeDashboardDao.GetCommercialStockValuation(accessLevelParam),
                Sample_Stock_Valuation = homeDashboardDao.GetSampleStockValuation(accessLevelParam),
                PPM_Stock_Valuation = homeDashboardDao.GetPPMStockValuation(accessLevelParam),
                Gift_Stock_Valuation = homeDashboardDao.GetGiftStockValuation(accessLevelParam)
            };
            return homeDashboard;
        }
    }
}