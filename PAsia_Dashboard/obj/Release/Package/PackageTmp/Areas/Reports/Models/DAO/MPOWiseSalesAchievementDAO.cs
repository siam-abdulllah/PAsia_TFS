﻿using System;
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
    public class MPOWiseSalesAchievementDAO
    {

        HomeDashboardDAO homeDashboardDao = new HomeDashboardDAO();
        DBHelper dbHelper = new DBHelper();
        DBConnection dbConn = new DBConnection();
        public object GetAMeWiseSalesProductAchievement(string fromDate, string toDate)
        {
            throw new NotImplementedException();
        }

        public List<MPOWiseSalesAchievementValue> GetMPOWiseSalesValueAchievement(string fromDate, string toDate)
        {
            try
            {
            string CODE = HttpContext.Current.Session["CODE"].ToString();
            string ACCESS_LEVEL = HttpContext.Current.Session["ACCESS_LEVEL"].ToString();
            string accessLevelParam = " ";
            if (ACCESS_LEVEL == "N" || ACCESS_LEVEL == null)
            {
                accessLevelParam = " ";
            }
            else if (ACCESS_LEVEL == "Z")
            {
                accessLevelParam = " AND DSM_CODE = '" + CODE + "'";
            }
            else if (ACCESS_LEVEL == "R")
            {
                accessLevelParam = " AND RSM_CODE = '" + CODE + "'";
            }
            else if (ACCESS_LEVEL == "A")
            {
                accessLevelParam = " AND AM_CODE = '" + CODE + "'";
            }
            else if (ACCESS_LEVEL == "T")
            {
                accessLevelParam = " AND MPO_CODE = '" + CODE + "'";
            }
           
            
            
            #region MyRegion

            string MValueQry = " SELECT row_number() OVER (ORDER BY TERRITORY_CODE) SL_NO," +
            //"DEPOT_CODE," +
            //"       FN_DEPOT_NAME(DEPOT_CODE)DEPOT_NAME," +
            // "       (SELECT DISTINCT EMP_CODE DSM_CODE FROM VW_FIELD_FORCE WHERE LOC_CODE=A.ZONE_CODE AND POSTING_LOCATION='Z') DSM_CODE," +
            //// "       FN_EMPLOYEE_NAME((SELECT DISTINCT EMP_CODE DSM_CODE FROM VW_FIELD_FORCE WHERE LOC_CODE=A.ZONE_CODE AND POSTING_LOCATION='Z')) DSM_NAME," +
            //"       DSM_CODE," +
            // "       FN_EMPLOYEE_NAME(DSM_CODE) DSM_NAME," +
            // "       A.ZONE_CODE,       " +
            //  "       FN_ZONE_NAME(ZONE_CODE) ZONE_NAME," +
            //"       (SELECT DISTINCT EMP_CODE DSM_CODE FROM VW_FIELD_FORCE WHERE LOC_CODE=A.REGION_CODE AND POSTING_LOCATION='R') RSM_CODE," +
            //"       FN_EMPLOYEE_NAME((SELECT DISTINCT EMP_CODE DSM_CODE FROM VW_FIELD_FORCE WHERE LOC_CODE=A.REGION_CODE AND POSTING_LOCATION='R')) RSM_NAME," +
            // "       RSM_CODE," +
            // "       FN_EMPLOYEE_NAME(RSM_CODE) RSM_NAME," +
            // "       REGION_CODE,       " +
            // "       FN_AREA_NAME(REGION_CODE) REGION_NAME," +
            // "       AM_CODE," +
            //"       FN_EMPLOYEE_NAME(AM_CODE) AM_NAME," +
            // "       AREA_CODE, " +
            //  "       FN_BELT_NAME(AREA_CODE) AREA_NAME,      " +
            "       (SELECT DISTINCT EMP_CODE MPO_CODE FROM VW_FIELD_FORCE WHERE LOC_CODE=A.TERRITORY_CODE AND POSTING_LOCATION='T') MPO_CODE," +
            "       FN_EMPLOYEE_NAME((SELECT DISTINCT EMP_CODE MPO_CODE FROM VW_FIELD_FORCE WHERE LOC_CODE=A.TERRITORY_CODE AND POSTING_LOCATION='T')) MPO_NAME," +

            //"       MPO_CODE," +
            //"       FN_EMPLOYEE_NAME(MPO_CODE) MPO_NAME," +
            "       TERRITORY_CODE," +
            "       FN_TERRITORY_NAME(TERRITORY_CODE) TERRITORY_NAME,        " +
            "       TARGET_AMT," +
            "       TO_DAY_SALES," +
            "       UPTO_SALES," +
            "       ACH," +
            "       LM_UPTO_SALES," +
            "       GROWTH," +
            "       CM_CUST," +
            "       LM_CUST       " +
            "   FROM" +
            "   ( " +
            "    SELECT " +
            // " DEPOT_CODE," +
            // "           ZONE_CODE," +
            // "           REGION_CODE," +
            // "           AREA_CODE,DSM_CODE,RSM_CODE,AM_CODE," +
            "           TERRITORY_CODE," +
            //" MPO_CODE," +
            "           SUM(NVL(TARGET_AMT,0)) TARGET_AMT ," +
            "           SUM(NVL(TO_DAY_SALES,0)) TO_DAY_SALES," +
            "           SUM(NVL(UPTO_SALES,0)) UPTO_SALES," +
            "           ROUND(DECODE(SUM(NVL(TARGET_AMT,0)),0,0,((SUM(NVL(UPTO_SALES,0)) * 100)/ SUM(NVL(TARGET_AMT,0)))),2) ACH," +
            "           SUM(NVL(LM_UPTO_SALES,0)) LM_UPTO_SALES," +
            "           SUM(NVL(UPTO_SALES,0)) - SUM(NVL(LM_UPTO_SALES,0)) GROWTH," +
            "           SUM(NVL(CM_CUST,0))CM_CUST," +
            "           SUM(NVL(LM_CUST,0))LM_CUST       " +
            "    FROM" +
            "       (       " +
            "    SELECT " +
            // " DEPOT_CODE," +
            //"           ZONE_CODE," +
            //"           REGION_CODE," +
            //"           AREA_CODE,DSM_CODE,RSM_CODE,AM_CODE," +
            "           TERRITORY_CODE," +
            //" MPO_CODE," +
            "           SUM(NVL(TARGET_AMT,0)) TARGET_AMT," +
            "           0 TO_DAY_SALES," +
            "           0 UPTO_SALES," +
            "           0 LM_UPTO_SALES," +
            "           0 CM_CUST," +
            "           0 LM_CUST         " +
            "    FROM TARGET_DTL_ALL" +
            "    WHERE YYYYMM BETWEEN TO_CHAR(TO_DATE('"+ fromDate +"','DD/MM/RRRR'),'YYYYMM') AND TO_CHAR(TO_DATE('"+toDate + "','DD/MM/RRRR'),'YYYYMM') " + accessLevelParam + "" +
            "    GROUP BY " +
            //"    DEPOT_CODE,ZONE_CODE,REGION_CODE,AREA_CODE,DSM_CODE,RSM_CODE,AM_CODE," +
            "    TERRITORY_CODE" +
            //" ,MPO_CODE" +
            "    UNION ALL" +
            "    SELECT " +
            // "           DEPOT_CODE,ZONE_CODE,REGION_CODE,AREA_CODE,DSM_CODE,RSM_CODE,AM_CODE," +
            "           TERRITORY_CODE," +
            //" MPO_CODE," +
            "           0 TARGET_AMT," +
            "           SUM(NVL(NET_INV_VALUE,0) - NVL(NET_RETURN_VALUE,0)) TO_DAY_SALES," +
            "           0 UPTO_SALES," +
            "           0 LM_UPTO_SALES," +
            "           0 CM_CUST," +
            "           0 LM_CUST           " +
            "    FROM  INVOICE_MST A" +
            "    WHERE INV_TYPE_CODE IN ('INV001', 'INV002')" +
            "    AND   TO_DATE (INVOICE_DATE, 'DD/MM/RRRR') = TO_DATE('"+toDate + "','DD/MM/RRRR') " + accessLevelParam + "" +
            "    GROUP BY " +
            //"    DEPOT_CODE,ZONE_CODE,REGION_CODE,AREA_CODE,DSM_CODE,RSM_CODE,AM_CODE," +
            "    TERRITORY_CODE" +
            //" ,MPO_CODE" +
            "    UNION ALL" +
            "    SELECT " +
            //"           DEPOT_CODE,ZONE_CODE,REGION_CODE,AREA_CODE,DSM_CODE,RSM_CODE,AM_CODE," +
            "           TERRITORY_CODE," +
           // " MPO_CODE," +
            "           0 TARGET_AMT," +
            "           0 TO_DAY_SALES," +
            "           SUM(NVL(NET_INV_VALUE,0) - NVL(NET_RETURN_VALUE,0)) UPTO_SALES ," +
            "           0 LM_UPTO_SALES," +
            "           0 CM_CUST," +
            "           0 LM_CUST                " +
            "    FROM  INVOICE_MST A" +
            "    WHERE INV_TYPE_CODE IN ('INV001', 'INV002')" +
            "    AND   TO_DATE (INVOICE_DATE, 'DD/MM/RRRR') BETWEEN  TO_DATE('"+ fromDate +"','DD/MM/RRRR') AND TO_DATE('"+toDate + "','DD/MM/RRRR') " + accessLevelParam + "" +
            "    GROUP BY " +
            //"    DEPOT_CODE,ZONE_CODE,REGION_CODE,AREA_CODE,DSM_CODE,RSM_CODE,AM_CODE," +
            "    TERRITORY_CODE" +
            //" ,MPO_CODE" +
            "    UNION ALL" +
            "    SELECT " +
            //"    DEPOT_CODE,ZONE_CODE,REGION_CODE,AREA_CODE,DSM_CODE,RSM_CODE,AM_CODE," +
            "           TERRITORY_CODE," +
            //" MPO_CODE," +
            "           0 TARGET_AMT," +
            "           0 TO_DAY_SALES," +
            "           0 UPTO_SALES," +
            "           SUM(NVL(NET_INV_VALUE,0) - NVL(NET_RETURN_VALUE,0)) LM_UPTO_SALES, " +
            "           0 CM_CUST," +
            "           0 LM_CUST                 " +
            "    FROM  INVOICE_MST A" +
            "    WHERE INV_TYPE_CODE IN ('INV001', 'INV002')" +
            "    AND   TO_DATE (INVOICE_DATE, 'DD/MM/RRRR') BETWEEN  ADD_MONTHS(TO_DATE('"+ fromDate +"','DD/MM/RRRR'),-1) AND ADD_MONTHS(TO_DATE('"+toDate + "','DD/MM/RRRR'),-1) " + accessLevelParam + "" +
            "    GROUP BY " +
            //"    DEPOT_CODE,ZONE_CODE,REGION_CODE,AREA_CODE,DSM_CODE,RSM_CODE,AM_CODE," +
            "    TERRITORY_CODE" +
            //" ,MPO_CODE" +
            "    UNION ALL" +
            "    SELECT " +
            //"           DEPOT_CODE,ZONE_CODE,REGION_CODE,AREA_CODE,DSM_CODE,RSM_CODE,AM_CODE," +
            "           TERRITORY_CODE," +
            //" MPO_CODE," +
            "           0 TARGET_AMT," +
            "           0 TO_DAY_SALES," +
            "           0 UPTO_SALES," +
            "           0 LM_UPTO_SALES," +
            "           COUNT(DISTINCT CUSTOMER_CODE) CM_CUST," +
            "           0 LM_CUST                 " +
            "    FROM  INVOICE_MST A" +
            "    WHERE INV_TYPE_CODE IN ('INV001', 'INV002')" +
            "    AND   TO_DATE (INVOICE_DATE, 'DD/MM/RRRR') BETWEEN  TO_DATE('"+ fromDate +"','DD/MM/RRRR') AND TO_DATE('"+toDate + "','DD/MM/RRRR') " + accessLevelParam + "" +
            "    GROUP BY " +
            //"     DEPOT_CODE,ZONE_CODE,REGION_CODE,AREA_CODE,DSM_CODE,RSM_CODE,AM_CODE," +
            "    TERRITORY_CODE" +
            //" ,MPO_CODE" +
            "          " +
            "    UNION ALL" +
            "    SELECT " +
            //"    DEPOT_CODE,ZONE_CODE,REGION_CODE,AREA_CODE,DSM_CODE,RSM_CODE,AM_CODE," +
            "           TERRITORY_CODE," +
            //" MPO_CODE," +
            "           0 TARGET_AMT," +
            "           0 TO_DAY_SALES," +
            "           0 UPTO_SALES," +
            "           0 LM_UPTO_SALES," +
            "           0 CM_CUST," +
            "           COUNT(DISTINCT CUSTOMER_CODE) LM_CUST                  " +
            "    FROM  INVOICE_MST A" +
            "    WHERE INV_TYPE_CODE IN ('INV001', 'INV002')" +
            "    AND   TO_DATE (INVOICE_DATE, 'DD/MM/RRRR') BETWEEN  ADD_MONTHS(TO_DATE('"+ fromDate +"','DD/MM/RRRR'),-1) AND ADD_MONTHS(TO_DATE('"+toDate + "','DD/MM/RRRR'),-1) " + accessLevelParam + "" +
            "    GROUP BY " +
            //"   DEPOT_CODE,ZONE_CODE,REGION_CODE,AREA_CODE,DSM_CODE,RSM_CODE,AM_CODE," +
            "  TERRITORY_CODE" +
            //" ,MPO_CODE" +
            "  )" +
            "  GROUP BY  " +
            //"  DEPOT_CODE,ZONE_CODE,REGION_CODE,AREA_CODE,DSM_CODE,RSM_CODE,AM_CODE," +
            "  TERRITORY_CODE" +
            //" ,MPO_CODE" +
            "  ) A";


            #endregion
            DataTable DCSdt = dbHelper.GetDataTable(dbConn.SAConnStrReader("Dashboard"), MValueQry);
            var mpoWiseSalesAchievementValue = (from DataRow row in DCSdt.Rows
                                                select new MPOWiseSalesAchievementValue()
                                                {
                                                    SL_No = row["SL_NO"].ToString(),
                                                    MPO_CODE = row["MPO_CODE"].ToString(),
                                                    MPO_NAME = row["MPO_NAME"].ToString(),
                                                    TERRITORY_CODE = row["TERRITORY_CODE"].ToString(),
                                                    TERRITORY_NAME = row["TERRITORY_NAME"].ToString(),
                                                    //DEPOT_CODE = row["DEPOT_CODE"].ToString(),
                                                    //DEPOT_NAME = row["DEPOT_NAME"].ToString(),
                                                    //DSM_CODE = row["DSM_CODE"].ToString(),
                                                    //DSM_NAME = row["DSM_NAME"].ToString(),
                                                    //ZONE_CODE = row["ZONE_CODE"].ToString(),
                                                    //ZONE_NAME = row["ZONE_NAME"].ToString(),
                                                    //RSM_CODE = row["RSM_CODE"].ToString(),
                                                    //RSM_NAME = row["RSM_NAME"].ToString(),
                                                    //REGION_CODE = row["REGION_CODE"].ToString(),
                                                    //REGION_NAME = row["REGION_NAME"].ToString(),
                                                    //AM_CODE = row["AM_CODE"].ToString(),
                                                    //AM_NAME = row["AM_NAME"].ToString(),
                                                    //AREA_CODE = row["AREA_CODE"].ToString(),
                                                    //AREA_NAME = row["AREA_NAME"].ToString(),
                                                    TARGET_AMT = row["TARGET_AMT"].ToString(),
                                                    TO_DAY_SALES = row["TO_DAY_SALES"].ToString(),
                                                    UPTO_SALES = row["UPTO_SALES"].ToString(),
                                                    ACH = row["ACH"].ToString(),
                                                    LM_UPTO_SALES = row["LM_UPTO_SALES"].ToString(),
                                                    GROWTH = row["GROWTH"].ToString(),
                                                    CM_CUST = row["CM_CUST"].ToString(),
                                                    LM_CUST = row["LM_CUST"].ToString()


                                                }).ToList();
            return mpoWiseSalesAchievementValue;
            }
            catch (Exception e)
            {

                throw;
            }
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
