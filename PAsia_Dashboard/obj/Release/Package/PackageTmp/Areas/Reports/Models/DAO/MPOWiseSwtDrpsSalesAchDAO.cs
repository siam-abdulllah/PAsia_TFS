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
    public class MPOWiseSwtDrpsSalesAchDAO
    {

        HomeDashboardDAO homeDashboardDao = new HomeDashboardDAO();
        DBHelper dbHelper = new DBHelper();
        DBConnection dbConn = new DBConnection();
        

        public List<MPOWiseSwtDrpsSalesAchValue> GetMPOWiseSwtDrpsSalesValueAch(string fromDate, string toDate)
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
            //string MSwtDrpValueQry = " SELECT ROW_NUMBER () OVER (ORDER BY ZONE_CODE) SL_NO," +
            //                    "       DEPOT_CODE," +
            //                    "       FN_DEPOT_NAME (DEPOT_CODE) DEPOT_NAME," +
            //                    "       (SELECT DISTINCT EMP_CODE DSM_CODE" +
            //                    "          FROM VW_FIELD_FORCE" +
            //                    "         WHERE LOC_CODE = A.ZONE_CODE AND POSTING_LOCATION = 'Z')" +
            //                    "          DSM_CODE," +
            //                    "       FN_EMPLOYEE_NAME (" +
            //                    "          (SELECT DISTINCT EMP_CODE DSM_CODE" +
            //                    "             FROM VW_FIELD_FORCE" +
            //                    "            WHERE LOC_CODE = A.ZONE_CODE AND POSTING_LOCATION = 'Z'))" +
            //                    "          DSM_NAME," +
            //                    "       A.ZONE_CODE," +
            //                    "       FN_ZONE_NAME (ZONE_CODE) ZONE_NAME," +
            //                    "       (SELECT DISTINCT EMP_CODE DSM_CODE" +
            //                    "          FROM VW_FIELD_FORCE" +
            //                    "         WHERE LOC_CODE = A.REGION_CODE AND POSTING_LOCATION = 'R')" +
            //                    "          RSM_CODE," +
            //                    "       FN_EMPLOYEE_NAME (" +
            //                    "          (SELECT DISTINCT EMP_CODE DSM_CODE" +
            //                    "             FROM VW_FIELD_FORCE" +
            //                    "            WHERE LOC_CODE = A.REGION_CODE AND POSTING_LOCATION = 'R'))" +
            //                    "          RSM_NAME," +
            //                    "       REGION_CODE," +
            //                    "       FN_AREA_NAME (REGION_CODE) REGION_NAME," +
            //                    "       (SELECT DISTINCT EMP_CODE DSM_CODE" +
            //                    "          FROM VW_FIELD_FORCE" +
            //                    "         WHERE LOC_CODE = A.AREA_CODE AND POSTING_LOCATION = 'A')" +
            //                    "          AM_CODE," +
            //                    "       FN_EMPLOYEE_NAME (" +
            //                    "          (SELECT DISTINCT EMP_CODE DSM_CODE" +
            //                    "             FROM VW_FIELD_FORCE" +
            //                    "            WHERE LOC_CODE = A.AREA_CODE AND POSTING_LOCATION = 'A'))" +
            //                    "          AM_NAME," +
            //                    "       AREA_CODE," +
            //                    "       FN_BELT_NAME (AREA_CODE) AREA_NAME," +
            //                    "       (SELECT DISTINCT EMP_CODE DSM_CODE" +
            //                    "          FROM VW_FIELD_FORCE" +
            //                    "         WHERE LOC_CODE = A.TERRITORY_CODE AND POSTING_LOCATION = 'T')" +
            //                    "          MPO_CODE," +
            //                    "       FN_EMPLOYEE_NAME (" +
            //                    "          (SELECT DISTINCT EMP_CODE DSM_CODE" +
            //                    "             FROM VW_FIELD_FORCE" +
            //                    "            WHERE LOC_CODE = A.TERRITORY_CODE AND POSTING_LOCATION = 'T'))" +
            //                    "          MPO_NAME," +
            //                    "       TERRITORY_CODE," +
            //                    "       FN_TERRITORY_NAME (TERRITORY_CODE) TERRITORY_NAME," +
            //                    "       TARGET_AMT," +
            //                    "       TO_DAY_SALES," +
            //                    "       UPTO_SALES," +
            //                    "       ACH," +
            //                    "       LM_UPTO_SALES," +
            //                    "       GROWTH," +
            //                    "       CM_CUST," +
            //                    "       LM_CUST" +
            //                    "  FROM (  SELECT DEPOT_CODE," +
            //                    "                 ZONE_CODE," +
            //                    "                 REGION_CODE," +
            //                    "                 AREA_CODE," +
            //                    "                 TERRITORY_CODE," +
            //                    "                 SUM (NVL (TARGET_AMT, 0)) TARGET_AMT," +
            //                    "                 SUM (NVL (TO_DAY_SALES, 0)) TO_DAY_SALES," +
            //                    "                 SUM (NVL (UPTO_SALES, 0)) UPTO_SALES," +
            //                    "                 ROUND (" +
            //                    "                    DECODE (" +
            //                    "                       SUM (NVL (TARGET_AMT, 0))," +
            //                    "                       0, 0," +
            //                    "                       (  (SUM (NVL (UPTO_SALES, 0)) * 100)" +
            //                    "                        / SUM (NVL (TARGET_AMT, 0))))," +
            //                    "                    2)" +
            //                    "                    ACH," +
            //                    "                 SUM (NVL (LM_UPTO_SALES, 0)) LM_UPTO_SALES," +
            //                    "                 SUM (NVL (UPTO_SALES, 0)) - SUM (NVL (LM_UPTO_SALES, 0))" +
            //                    "                    GROWTH," +
            //                    "                 SUM (NVL (CM_CUST, 0)) CM_CUST," +
            //                    "                 SUM (NVL (LM_CUST, 0)) LM_CUST" +
            //                    "            FROM (  SELECT A.DEPOT_CODE," +
            //                    "                           A.ZONE_CODE," +
            //                    "                           A.REGION_CODE," +
            //                    "                           A.AREA_CODE," +
            //                    "                           A.TERRITORY_CODE," +
            //                    "                           SUM (NVL (20000, 0)) TARGET_AMT," +
            //                    "                           0 TO_DAY_SALES," +
            //                    "                           0 UPTO_SALES," +
            //                    "                           0 LM_UPTO_SALES," +
            //                    "                           0 CM_CUST," +
            //                    "                           0 LM_CUST" +
            //                    "                      FROM TARGET_DTL_ALL A,TERRITORY_TARGET_MST B" +
            //                    "                     WHERE A.MST_SLNO=B.MST_SLNO AND A.YYYYMM BETWEEN TO_CHAR (" +
            //                    "                                             TO_DATE ('"+ fromDate +"', 'DD/MM/RRRR')," +
            //                    "                                             'YYYYMM')" +
            //                    "                                      AND TO_CHAR (" +
            //                    "                                             TO_DATE ('"+toDate + "', 'DD/MM/RRRR')," +
            //                    "                                             'YYYYMM') " + accessLevelParam + "" +
            //                    "                  GROUP BY A.DEPOT_CODE," +
            //                    "                           A.ZONE_CODE," +
            //                    "                           A.REGION_CODE," +
            //                    "                           A.AREA_CODE," +
            //                    "                           A.TERRITORY_CODE" +
            //                    "                  UNION ALL" +
            //                    "                    SELECT DEPOT_CODE," +
            //                    "                           ZONE_CODE," +
            //                    "                           REGION_CODE," +
            //                    "                           AREA_CODE," +
            //                    "                           TERRITORY_CODE," +
            //                    "                           0 TARGET_AMT," +
            //                    "                           SUM (" +
            //                    "                              NVL (NET_INV_VALUE, 0) - NVL (NET_RETURN_VALUE, 0))" +
            //                    "                              TO_DAY_SALES," +
            //                    "                           0 UPTO_SALES," +
            //                    "                           0 LM_UPTO_SALES," +
            //                    "                           0 CM_CUST," +
            //                    "                           0 LM_CUST" +
            //                    "                      FROM INVOICE_MST A, INVOICE_DTL B" +
            //                    "                      WHERE     A.INV_MST_SLNO = B.INV_MST_SLNO AND   INV_TYPE_CODE IN ('INV004')" +
            //                    "                           AND TO_DATE (INVOICE_DATE, 'DD/MM/RRRR') =" +
            //                    "                                  TO_DATE ('"+toDate + "', 'DD/MM/RRRR') " + accessLevelParam + "" +
            //                    "                  GROUP BY DEPOT_CODE," +
            //                    "                           ZONE_CODE," +
            //                    "                           REGION_CODE," +
            //                    "                           AREA_CODE," +
            //                    "                           TERRITORY_CODE" +
            //                    "                  UNION ALL" +
            //                    "                    SELECT DEPOT_CODE," +
            //                    "                           ZONE_CODE," +
            //                    "                           REGION_CODE," +
            //                    "                           AREA_CODE," +
            //                    "                           TERRITORY_CODE," +
            //                    "                           0 TARGET_AMT," +
            //                    "                           0 TO_DAY_SALES," +
            //                    "                           SUM (" +
            //                    "                              NVL (NET_INV_VALUE, 0) - NVL (NET_RETURN_VALUE, 0))" +
            //                    "                              UPTO_SALES," +
            //                    "                           0 LM_UPTO_SALES," +
            //                    "                           0 CM_CUST," +
            //                    "                           0 LM_CUST" +
            //                    "                      FROM INVOICE_MST A, INVOICE_DTL B" +
            //                    "                      WHERE     A.INV_MST_SLNO = B.INV_MST_SLNO AND    INV_TYPE_CODE IN ('INV004')" +
            //                    "                           AND TO_DATE (INVOICE_DATE, 'DD/MM/RRRR') BETWEEN TO_DATE (" +
            //                    "                                                                               '"+ fromDate +"'," +
            //                    "                                                                               'DD/MM/RRRR')" +
            //                    "                                                                        AND TO_DATE (" +
            //                    "                                                                               '"+toDate + "'," +
            //                    "                                                                               'DD/MM/RRRR') AND PRODUCT_CODE='C0191' " + accessLevelParam + "" +
            //                    "                  GROUP BY DEPOT_CODE," +
            //                    "                           ZONE_CODE," +
            //                    "                           REGION_CODE," +
            //                    "                           AREA_CODE," +
            //                    "                           TERRITORY_CODE" +
            //                    "                  UNION ALL" +
            //                    "                    SELECT DEPOT_CODE," +
            //                    "                           ZONE_CODE," +
            //                    "                           REGION_CODE," +
            //                    "                           AREA_CODE," +
            //                    "                           TERRITORY_CODE," +
            //                    "                           0 TARGET_AMT," +
            //                    "                           0 TO_DAY_SALES," +
            //                    "                           0 UPTO_SALES," +
            //                    "                           SUM (" +
            //                    "                              NVL (NET_INV_VALUE, 0) - NVL (NET_RETURN_VALUE, 0))" +
            //                    "                              LM_UPTO_SALES," +
            //                    "                           0 CM_CUST," +
            //                    "                           0 LM_CUST" +
            //                    "                      FROM INVOICE_MST A, INVOICE_DTL B" +
            //                    "                      WHERE     A.INV_MST_SLNO = B.INV_MST_SLNO AND     INV_TYPE_CODE IN ('INV004')" +
            //                    "                           AND TO_DATE (INVOICE_DATE, 'DD/MM/RRRR') BETWEEN ADD_MONTHS (" +
            //                    "                                                                               TO_DATE (" +
            //                    "                                                                                  '"+ fromDate +"'," +
            //                    "                                                                                  'DD/MM/RRRR')," +
            //                    "                                                                               -1)" +
            //                    "                                                                        AND ADD_MONTHS (" +
            //                    "                                                                               TO_DATE (" +
            //                    "                                                                                  '"+toDate + "'," +
            //                    "                                                                                  'DD/MM/RRRR')," +
            //                    "                                                                               -1) AND PRODUCT_CODE='C0191' " + accessLevelParam + "" +
            //                    "                  GROUP BY DEPOT_CODE," +
            //                    "                           ZONE_CODE," +
            //                    "                           REGION_CODE," +
            //                    "                           AREA_CODE," +
            //                    "                           TERRITORY_CODE" +
            //                    "                  UNION ALL" +
            //                    "                    SELECT DEPOT_CODE," +
            //                    "                           ZONE_CODE," +
            //                    "                           REGION_CODE," +
            //                    "                           AREA_CODE," +
            //                    "                           TERRITORY_CODE," +
            //                    "                           0 TARGET_AMT," +
            //                    "                           0 TO_DAY_SALES," +
            //                    "                           0 UPTO_SALES," +
            //                    "                           0 LM_UPTO_SALES," +
            //                    "                           COUNT (DISTINCT CUSTOMER_CODE) CM_CUST," +
            //                    "                           0 LM_CUST" +
            //                    "                      FROM INVOICE_MST A, INVOICE_DTL B" +
            //                    "                      WHERE     A.INV_MST_SLNO = B.INV_MST_SLNO AND    INV_TYPE_CODE IN ('INV004')" +
            //                    "                           AND TO_DATE (INVOICE_DATE, 'DD/MM/RRRR') BETWEEN TO_DATE (" +
            //                    "                                                                               '"+ fromDate +"'," +
            //                    "                                                                               'DD/MM/RRRR')" +
            //                    "                                                                        AND TO_DATE (" +
            //                    "                                                                               '"+toDate + "'," +
            //                    "                                                                               'DD/MM/RRRR') AND PRODUCT_CODE='C0191' " + accessLevelParam + "" +
            //                    "                  GROUP BY DEPOT_CODE," +
            //                    "                           ZONE_CODE," +
            //                    "                           REGION_CODE," +
            //                    "                           AREA_CODE," +
            //                    "                           TERRITORY_CODE" +
            //                    "                  UNION ALL" +
            //                    "                    SELECT DEPOT_CODE," +
            //                    "                           ZONE_CODE," +
            //                    "                           REGION_CODE," +
            //                    "                           AREA_CODE," +
            //                    "                           TERRITORY_CODE," +
            //                    "                           0 TARGET_AMT," +
            //                    "                           0 TO_DAY_SALES," +
            //                    "                           0 UPTO_SALES," +
            //                    "                           0 LM_UPTO_SALES," +
            //                    "                           0 CM_CUST," +
            //                    "                           COUNT (DISTINCT CUSTOMER_CODE) LM_CUST" +
            //                    "                      FROM INVOICE_MST A, INVOICE_DTL B" +
            //                    "                      WHERE     A.INV_MST_SLNO = B.INV_MST_SLNO AND   INV_TYPE_CODE IN ('INV004')" +
            //                    "                           AND TO_DATE (INVOICE_DATE, 'DD/MM/RRRR') BETWEEN ADD_MONTHS (" +
            //                    "                                                                               TO_DATE (" +
            //                    "                                                                                  '"+ fromDate +"'," +
            //                    "                                                                                  'DD/MM/RRRR')," +
            //                    "                                                                               -1)" +
            //                    "                                                                        AND ADD_MONTHS (" +
            //                    "                                                                               TO_DATE (" +
            //                    "                                                                                  '"+toDate + "'," +
            //                    "                                                                                  'DD/MM/RRRR')," +
            //                    "                                                                               -1) AND PRODUCT_CODE='C0191' " + accessLevelParam + "" +
            //                    "                  GROUP BY DEPOT_CODE," +
            //                    "                           ZONE_CODE," +
            //                    "                           REGION_CODE," +
            //                    "                           AREA_CODE," +
            //                    "                           TERRITORY_CODE)" +
            //                    "        GROUP BY DEPOT_CODE," +
            //                    "                 ZONE_CODE," +
            //                    "                 REGION_CODE," +
            //                    "                 AREA_CODE," +
            //                    "                 TERRITORY_CODE) A";
            string MSwtDrpValueQry = "SELECT ROW_NUMBER () OVER (ORDER BY ZONE_CODE) SL_NO," +
"       DEPOT_CODE," +
"       FN_DEPOT_NAME (DEPOT_CODE) DEPOT_NAME," +
"       (SELECT DISTINCT EMP_CODE DSM_CODE" +
"          FROM VW_FIELD_FORCE" +
"         WHERE LOC_CODE = A.ZONE_CODE AND POSTING_LOCATION = 'Z')" +
"          DSM_CODE," +
"       FN_EMPLOYEE_NAME (" +
"          (SELECT DISTINCT EMP_CODE DSM_CODE" +
"             FROM VW_FIELD_FORCE" +
"            WHERE LOC_CODE = A.ZONE_CODE AND POSTING_LOCATION = 'Z'))" +
"          DSM_NAME," +
"       A.ZONE_CODE," +
"       FN_ZONE_NAME (ZONE_CODE) ZONE_NAME," +
"       (SELECT DISTINCT EMP_CODE DSM_CODE" +
"          FROM VW_FIELD_FORCE" +
"         WHERE LOC_CODE = A.REGION_CODE AND POSTING_LOCATION = 'R')" +
"          RSM_CODE," +
"       FN_EMPLOYEE_NAME (" +
"          (SELECT DISTINCT EMP_CODE DSM_CODE" +
"             FROM VW_FIELD_FORCE" +
"            WHERE LOC_CODE = A.REGION_CODE AND POSTING_LOCATION = 'R'))" +
"          RSM_NAME," +
"       REGION_CODE," +
"       FN_AREA_NAME (REGION_CODE) REGION_NAME," +
"       (SELECT DISTINCT EMP_CODE DSM_CODE" +
"          FROM VW_FIELD_FORCE" +
"         WHERE LOC_CODE = A.AREA_CODE AND POSTING_LOCATION = 'A')" +
"          AM_CODE," +
"       FN_EMPLOYEE_NAME (" +
"          (SELECT DISTINCT EMP_CODE DSM_CODE" +
"             FROM VW_FIELD_FORCE" +
"            WHERE LOC_CODE = A.AREA_CODE AND POSTING_LOCATION = 'A'))" +
"          AM_NAME," +
"       AREA_CODE," +
"       FN_BELT_NAME (AREA_CODE) AREA_NAME," +
"       (SELECT DISTINCT EMP_CODE DSM_CODE" +
"          FROM VW_FIELD_FORCE" +
"         WHERE LOC_CODE = A.TERRITORY_CODE AND POSTING_LOCATION = 'T')" +
"          MPO_CODE," +
"       FN_EMPLOYEE_NAME (" +
"          (SELECT DISTINCT EMP_CODE DSM_CODE" +
"             FROM VW_FIELD_FORCE" +
"            WHERE LOC_CODE = A.TERRITORY_CODE AND POSTING_LOCATION = 'T'))" +
"          MPO_NAME," +
"       FN_MIO_DESIG_NAME( (SELECT DISTINCT EMP_CODE" +
"             FROM VW_FIELD_FORCE" +
"            WHERE LOC_CODE = A.TERRITORY_CODE AND POSTING_LOCATION = 'T') ) DESIGNATION," +
"       TERRITORY_CODE," +
"       FN_TERRITORY_NAME (TERRITORY_CODE) TERRITORY_NAME," +
"       TARGET_AMT," +
"       TO_DAY_SALES," +
"       TO_DAY_BOX," +
"       UPTO_SALES," +
"       UPTO_BOX," +
"       ACH," +
"       LM_UPTO_SALES," +
"       LM_UPTO_BOX," +
"       GROWTH," +
"       CM_CUST," +
"       LM_CUST" +
"  FROM (  SELECT DEPOT_CODE," +
"                 ZONE_CODE," +
"                 REGION_CODE," +
"                 AREA_CODE," +
"                 TERRITORY_CODE," +
"                 SUM (NVL (TARGET_AMT, 0)) TARGET_AMT," +
"                 SUM (NVL (TO_DAY_SALES, 0)) TO_DAY_SALES," +
"                 SUM (NVL (TO_DAY_BOX, 0)) TO_DAY_BOX," +
"                 SUM (NVL (UPTO_SALES, 0)) UPTO_SALES," +
"                 SUM (NVL (UPTO_BOX, 0)) UPTO_BOX," +
"                 ROUND (" +
"                    DECODE (" +
"                       SUM (NVL (TARGET_AMT, 0))," +
"                       0, 0," +
"                       (  (SUM (NVL (UPTO_SALES, 0)) * 100)" +
"                        / SUM (NVL (TARGET_AMT, 0))))," +
"                    2)" +
"                    ACH," +
"                 SUM (NVL (LM_UPTO_SALES, 0)) LM_UPTO_SALES," +
"                 SUM (NVL (LM_UPTO_BOX, 0)) LM_UPTO_BOX," +
"                 SUM (NVL (UPTO_SALES, 0)) - SUM (NVL (LM_UPTO_SALES, 0))" +
"                    GROWTH," +
"                 SUM (NVL (CM_CUST, 0)) CM_CUST," +
"                 SUM (NVL (LM_CUST, 0)) LM_CUST" +
"            FROM ( SELECT D.DEPOT_CODE," +
"                           D.ZONE_CODE," +
"                           D.REGION_CODE," +
"                           D.AREA_CODE," +
"                           D.TERRITORY_CODE," +
"                           ROUND(NVL (TARGET_QTY, 0)*(NVL(UNIT_TP,0)+NVL(UNIT_VAT,0))) TARGET_AMT," +
"                           0 TO_DAY_SALES," +
"                           0 TO_DAY_BOX," +
"                           0 UPTO_SALES," +
"                           0 UPTO_BOX," +
"                           0 LM_UPTO_SALES," +
"                           0 LM_UPTO_BOX," +
"                           0 CM_CUST," +
"                           0 LM_CUST" +
"                      FROM TERRITORY_TARGET_MST A, TERRITORY_TARGET_DTL B,PRODUCT_PRICE C,TARGET_DTL_ALL D" +
"                     WHERE     A.MST_SLNO = B.MST_SLNO" +
"                     AND A.MST_SLNO = D.MST_SLNO" +
"                     AND B.PRODUCT_CODE=C.PRODUCT_CODE" +
"                           AND A.YYYYMM BETWEEN TO_CHAR (" +
"                                                   TO_DATE ('" + fromDate + "'," +
"                                                            'DD/MM/RRRR')," +
"                                                   'YYYYMM')" +
"                                            AND TO_CHAR (" +
"                                                   TO_DATE ('" + toDate + "'," +
"                                                            'DD/MM/RRRR')," +
"                                                   'YYYYMM') " + accessLevelParam + "" +
"                                                   AND B.PRODUCT_CODE = 'C0191' " +

//"                  GROUP BY A.DEPOT_CODE," +
//"                           A.ZONE_CODE," +
//"                           A.REGION_CODE," +
//"                           A.AREA_CODE," +
//"                           A.TERRITORY_CODE" +
"                  UNION ALL" +
"                    SELECT DEPOT_CODE," +
"                           ZONE_CODE," +
"                           REGION_CODE," +
"                           AREA_CODE," +
"                           TERRITORY_CODE," +
"                           0 TARGET_AMT," +
"                           SUM (NVL (TO_DAY_SALES, 0) - NVL (TO_DAY_RETURN, 0))" +
"                              TO_DAY_SALES," +
"                           SUM (NVL (TO_DAY_BOX, 0) - NVL (TO_DAY_RETURN_BOX, 0))" +
"                              TO_DAY_BOX," +
"                           0 UPTO_SALES," +
"                           0 UPTO_BOX," +
"                           0 LM_TO_DAY_SALES," +
"                           0 LM_UPTO_BOX," +
"                           0 CM_CUST," +
"                           0 LM_CUST" +
"                      FROM (  SELECT DEPOT_CODE," +
"                                     ZONE_CODE," +
"                                     REGION_CODE," +
"                                     AREA_CODE," +
"                                     TERRITORY_CODE," +
"                                     SUM (" +
"                                          NVL (B.TOTAL_VALUE, 0)" +
"                                        - NVL (B.INV_DISC_VALUE, 0))" +
"                                        TO_DAY_SALES," +
"                                     SUM (ISSUED_QTY) TO_DAY_BOX," +
"                                     0 TO_DAY_RETURN," +
"                                     0 TO_DAY_RETURN_BOX" +
"                                FROM INVOICE_MST A, INVOICE_DTL B" +
"                               WHERE     A.INV_MST_SLNO = B.INV_MST_SLNO" +
"                                     AND INV_TYPE_CODE IN ('INV004')" +
"                                     AND TO_DATE (INVOICE_DATE, 'DD/MM/RRRR') =" +
"                                            TO_DATE ('"+toDate + "', 'DD/MM/RRRR')" +
"                                     AND PRODUCT_CODE = 'C0191' " + accessLevelParam + "" +
"                            GROUP BY DEPOT_CODE," +
"                                     ZONE_CODE," +
"                                     REGION_CODE," +
"                                     AREA_CODE," +
"                                     TERRITORY_CODE" +
"                            UNION ALL" +
"                              SELECT C.DEPOT_CODE," +
"                                     C.ZONE_CODE," +
"                                     C.REGION_CODE," +
"                                     C.AREA_CODE," +
"                                     C.TERRITORY_CODE," +
"                                     0 TO_DAY_SALES," +
"                                     0 TO_DAY_BOX," +
"                                     SUM (" +
"                                          NVL (B.TOTAL_VALUE, 0)" +
"                                        - NVL (B.INV_DISC_VALUE, 0))" +
"                                        TO_DAY_RETURN," +
"                                     SUM (B.SALES_RETURN_QTY) TO_DAY_RETURN_BOX" +
"                                FROM RETURN_RECEIVE_MST A," +
"                                     RETURN_RECEIVE_DTL B," +
"                                     INVOICE_MST C" +
"                               WHERE     A.RET_RECV_MST_SLNO = B.RET_RECV_MST_SLNO" +
"                                     AND A.INVOICE_NO = C.INVOICE_NO" +
"                                     AND INV_TYPE_CODE IN ('INV004')" +
"                                     AND TO_DATE (INVOICE_DATE, 'DD/MM/RRRR') =" +
"                                            TO_DATE ('"+toDate + "', 'DD/MM/RRRR')" +
"                                     AND PRODUCT_CODE = 'C0191' " + accessLevelParam + "" +
"                            GROUP BY C.DEPOT_CODE," +
"                                     ZONE_CODE," +
"                                     REGION_CODE," +
"                                     AREA_CODE," +
"                                     TERRITORY_CODE)" +
"                  GROUP BY DEPOT_CODE," +
"                           ZONE_CODE," +
"                           REGION_CODE," +
"                           AREA_CODE," +
"                           TERRITORY_CODE" +
"                  UNION ALL" +
"                    SELECT DEPOT_CODE," +
"                           ZONE_CODE," +
"                           REGION_CODE," +
"                           AREA_CODE," +
"                           TERRITORY_CODE," +
"                           0 TARGET_AMT," +
"                           0 TO_DAY_SALES," +
"                           0 TO_DAY_BOX," +
"                           SUM (NVL (UPTO_SALES, 0) - NVL (UPTO_RETURN, 0))" +
"                              UPTO_SALES," +
"                           SUM (NVL (UPTO_BOX, 0) - NVL (UPTO_RETURN_BOX, 0))" +
"                              UPTO_BOX," +
"                           0 LM_UPTO_SALES," +
"                           0 LM_UPTO_BOX," +
"                           0 CM_CUST," +
"                           0 LM_CUST" +
"                      FROM (  SELECT DEPOT_CODE," +
"                                     ZONE_CODE," +
"                                     REGION_CODE," +
"                                     AREA_CODE," +
"                                     TERRITORY_CODE," +
"                                     SUM (" +
"                                          NVL (B.TOTAL_VALUE, 0)" +
"                                        - NVL (B.INV_DISC_VALUE, 0))" +
"                                        UPTO_SALES," +
"                                     SUM (ISSUED_QTY) UPTO_BOX," +
"                                     0 UPTO_RETURN," +
"                                     0 UPTO_RETURN_BOX" +
"                                FROM INVOICE_MST A, INVOICE_DTL B" +
"                               WHERE     A.INV_MST_SLNO = B.INV_MST_SLNO" +
"                                     AND INV_TYPE_CODE IN ('INV004')" +
"                                     AND TO_DATE (INVOICE_DATE, 'DD/MM/RRRR') BETWEEN TO_DATE (" +
"                                                                                         '"+ fromDate +"'," +
"                                                                                         'DD/MM/RRRR')" +
"                                                                                  AND TO_DATE (" +
"                                                                                         '"+toDate + "'," +
"                                                                                         'DD/MM/RRRR')" +
"                                     AND PRODUCT_CODE = 'C0191' " + accessLevelParam + "" +
"                            GROUP BY DEPOT_CODE," +
"                                     ZONE_CODE," +
"                                     REGION_CODE," +
"                                     AREA_CODE," +
"                                     TERRITORY_CODE" +
"                            UNION ALL" +
"                              SELECT C.DEPOT_CODE," +
"                                     C.ZONE_CODE," +
"                                     C.REGION_CODE," +
"                                     C.AREA_CODE," +
"                                     C.TERRITORY_CODE," +
"                                     0 UPTO_SALES," +
"                                     0 UPTO_BOX," +
"                                     SUM (" +
"                                          NVL (B.TOTAL_VALUE, 0)" +
"                                        - NVL (B.INV_DISC_VALUE, 0))" +
"                                        UPTO_RETURN," +
"                                     SUM (B.SALES_RETURN_QTY) UPTO_RETURN_BOX" +
"                                FROM RETURN_RECEIVE_MST A," +
"                                     RETURN_RECEIVE_DTL B," +
"                                     INVOICE_MST C" +
"                               WHERE     A.RET_RECV_MST_SLNO = B.RET_RECV_MST_SLNO" +
"                                     AND A.INVOICE_NO = C.INVOICE_NO" +
"                                     AND INV_TYPE_CODE IN ('INV004')" +
"                                     AND TO_DATE (INVOICE_DATE, 'DD/MM/RRRR') BETWEEN TO_DATE (" +
"                                                                                         '"+ fromDate +"'," +
"                                                                                         'DD/MM/RRRR')" +
"                                                                                  AND TO_DATE (" +
"                                                                                         '"+toDate + "'," +
"                                                                                         'DD/MM/RRRR')" +
"                                     AND PRODUCT_CODE = 'C0191' " + accessLevelParam + "" +
"                            GROUP BY C.DEPOT_CODE," +
"                                     ZONE_CODE," +
"                                     REGION_CODE," +
"                                     AREA_CODE," +
"                                     TERRITORY_CODE)" +
"                  GROUP BY DEPOT_CODE," +
"                           ZONE_CODE," +
"                           REGION_CODE," +
"                           AREA_CODE," +
"                           TERRITORY_CODE" +
"                  UNION ALL" +
"                    SELECT DEPOT_CODE," +
"                           ZONE_CODE," +
"                           REGION_CODE," +
"                           AREA_CODE," +
"                           TERRITORY_CODE," +
"                           0 TARGET_AMT," +
"                           0 TO_DAY_SALES," +
"                           0 TO_DAY_BOX," +
"                           0 UPTO_SALES," +
"                           0 UPTO_BOX," +
"                           SUM (" +
"                                NVL (LM_UPTO_SALES, 0)" +
"                              - NVL (LM_UPTO_RETURN_SALES, 0))" +
"                              LM_UPTO_SALES," +
"                           SUM (" +
"                              NVL (LM_UPTO_BOX, 0) - NVL (LM_UPTO_RETURN_BOX, 0))" +
"                              LM_UPTO_BOX," +
"                           0 CM_CUST," +
"                           0 LM_CUST" +
"                      FROM (  SELECT DEPOT_CODE," +
"                                     ZONE_CODE," +
"                                     REGION_CODE," +
"                                     AREA_CODE," +
"                                     TERRITORY_CODE," +
"                                     SUM (" +
"                                          NVL (B.TOTAL_VALUE, 0)" +
"                                        - NVL (B.INV_DISC_VALUE, 0))" +
"                                        LM_UPTO_SALES," +
"                                     SUM (ISSUED_QTY) LM_UPTO_BOX," +
"                                     0 LM_UPTO_RETURN_SALES," +
"                                     0 LM_UPTO_RETURN_BOX," +
"                                     0 CM_CUST," +
"                                     0 LM_CUST" +
"                                FROM INVOICE_MST A, INVOICE_DTL B" +
"                               WHERE     A.INV_MST_SLNO = B.INV_MST_SLNO" +
"                                     AND INV_TYPE_CODE IN ('INV004')" +
"                                     AND TO_DATE (INVOICE_DATE, 'DD/MM/RRRR') BETWEEN ADD_MONTHS (" +
"                                                                                         TO_DATE (" +
"                                                                                            '"+ fromDate +"'," +
"                                                                                            'DD/MM/RRRR')," +
"                                                                                         -1)" +
"                                                                                  AND ADD_MONTHS (" +
"                                                                                         TO_DATE (" +
"                                                                                            '"+toDate + "'," +
"                                                                                            'DD/MM/RRRR')," +
"                                                                                         -1)" +
"                                     AND PRODUCT_CODE = 'C0191' " + accessLevelParam + "" +
"                            GROUP BY DEPOT_CODE," +
"                                     ZONE_CODE," +
"                                     REGION_CODE," +
"                                     AREA_CODE," +
"                                     TERRITORY_CODE" +
"                            UNION ALL" +
"                              SELECT DEPOT_CODE," +
"                                     ZONE_CODE," +
"                                     REGION_CODE," +
"                                     AREA_CODE," +
"                                     TERRITORY_CODE," +
"                                     0 LM_UPTO_SALES," +
"                                     0 LM_UPTO_BOX," +
"                                     SUM (" +
"                                          NVL (B.TOTAL_VALUE, 0)" +
"                                        - NVL (B.INV_DISC_VALUE, 0))" +
"                                        LM_UPTO_RETURN," +
"                                     SUM (B.SALES_RETURN_QTY) LM_UPTO_RETURN_BOX," +
"                                     0 CM_CUST," +
"                                     0 LM_CUST" +
"                                FROM INVOICE_MST A, INVOICE_DTL B" +
"                               WHERE     A.INV_MST_SLNO = B.INV_MST_SLNO" +
"                                     AND INV_TYPE_CODE IN ('INV004')" +
"                                     AND TO_DATE (INVOICE_DATE, 'DD/MM/RRRR') BETWEEN ADD_MONTHS (" +
"                                                                                         TO_DATE (" +
"                                                                                            '"+ fromDate +"'," +
"                                                                                            'DD/MM/RRRR')," +
"                                                                                         -1)" +
"                                                                                  AND ADD_MONTHS (" +
"                                                                                         TO_DATE (" +
"                                                                                            '"+toDate + "'," +
"                                                                                            'DD/MM/RRRR')," +
"                                                                                         -1)" +
"                                     AND PRODUCT_CODE = 'C0191' " + accessLevelParam + "" +
"                            GROUP BY DEPOT_CODE," +
"                                     ZONE_CODE," +
"                                     REGION_CODE," +
"                                     AREA_CODE," +
"                                     TERRITORY_CODE)" +
"                  GROUP BY DEPOT_CODE," +
"                           ZONE_CODE," +
"                           REGION_CODE," +
"                           AREA_CODE," +
"                           TERRITORY_CODE" +
"                  UNION ALL" +
"                    SELECT DEPOT_CODE," +
"                           ZONE_CODE," +
"                           REGION_CODE," +
"                           AREA_CODE," +
"                           TERRITORY_CODE," +
"                           0 TARGET_AMT," +
"                           0 TO_DAY_SALES," +
"                           0 TO_DAY_BOX," +
"                           0 UPTO_SALES," +
"                           0 UPTO_BOX," +
"                           0 LM_UPTO_SALES," +
"                           0 LM_UPTO_BOX," +
"                           COUNT (DISTINCT CUSTOMER_CODE) CM_CUST," +
"                           0 LM_CUST" +
"                      FROM INVOICE_MST A, INVOICE_DTL B" +
"                     WHERE     A.INV_MST_SLNO = B.INV_MST_SLNO" +
"                           AND INV_TYPE_CODE IN ('INV004')" +
"                           AND TO_DATE (INVOICE_DATE, 'DD/MM/RRRR') BETWEEN TO_DATE (" +
"                                                                               '"+ fromDate +"'," +
"                                                                               'DD/MM/RRRR')" +
"                                                                        AND TO_DATE (" +
"                                                                               '"+toDate + "'," +
"                                                                               'DD/MM/RRRR')" +
"                           AND PRODUCT_CODE = 'C0191' " + accessLevelParam + "" +
"                  GROUP BY DEPOT_CODE," +
"                           ZONE_CODE," +
"                           REGION_CODE," +
"                           AREA_CODE," +
"                           TERRITORY_CODE" +
"                  UNION ALL" +
"                    SELECT DEPOT_CODE," +
"                           ZONE_CODE," +
"                           REGION_CODE," +
"                           AREA_CODE," +
"                           TERRITORY_CODE," +
"                           0 TARGET_AMT," +
"                           0 TO_DAY_SALES," +
"                           0 TO_DAY_BOX," +
"                           0 UPTO_SALES," +
"                           0 UPTO_BOX," +
"                           0 LM_UPTO_SALES," +
"                           0 LM_UPTO_BOX," +
"                           0 CM_CUST," +
"                           COUNT (DISTINCT CUSTOMER_CODE) LM_CUST" +
"                      FROM INVOICE_MST A, INVOICE_DTL B" +
"                     WHERE     A.INV_MST_SLNO = B.INV_MST_SLNO" +
"                           AND INV_TYPE_CODE IN ('INV004')" +
"                           AND TO_DATE (INVOICE_DATE, 'DD/MM/RRRR') BETWEEN ADD_MONTHS (" +
"                                                                               TO_DATE (" +
"                                                                                  '"+ fromDate +"'," +
"                                                                                  'DD/MM/RRRR')," +
"                                                                               -1)" +
"                                                                        AND ADD_MONTHS (" +
"                                                                               TO_DATE (" +
"                                                                                  '"+toDate + "'," +
"                                                                                  'DD/MM/RRRR')," +
"                                                                               -1)" +
"                           AND PRODUCT_CODE = 'C0191' " + accessLevelParam + "" +
"                  GROUP BY DEPOT_CODE," +
"                           ZONE_CODE," +
"                           REGION_CODE," +
"                           AREA_CODE," +
"                           TERRITORY_CODE)" +
"        GROUP BY DEPOT_CODE," +
"                 ZONE_CODE," +
"                 REGION_CODE," +
"                 AREA_CODE," +
"                 TERRITORY_CODE) A";




            #endregion
            DataTable DCSdt = dbHelper.GetDataTable(dbConn.SAConnStrReader("Dashboard"), MSwtDrpValueQry);
            var mpoWiseSwtDrpsSalesAchValue = (from DataRow row in DCSdt.Rows
                                                select new MPOWiseSwtDrpsSalesAchValue()
                                                {
                                                    SL_No = row["SL_NO"].ToString(),
                                                    MPO_CODE = row["MPO_CODE"].ToString(),
                                                    MPO_NAME = row["MPO_NAME"].ToString(),
                                                    DESIGNATION = row["DESIGNATION"].ToString(),
                                                    TERRITORY_CODE = row["TERRITORY_CODE"].ToString(),
                                                    TERRITORY_NAME = row["TERRITORY_NAME"].ToString(),
                                                    DEPOT_CODE = row["DEPOT_CODE"].ToString(),
                                                    DEPOT_NAME = row["DEPOT_NAME"].ToString(),
                                                    DSM_CODE = row["DSM_CODE"].ToString(),
                                                    DSM_NAME = row["DSM_NAME"].ToString(),
                                                    ZONE_CODE = row["ZONE_CODE"].ToString(),
                                                    ZONE_NAME = row["ZONE_NAME"].ToString(),
                                                    RSM_CODE = row["RSM_CODE"].ToString(),
                                                    RSM_NAME = row["RSM_NAME"].ToString(),
                                                    REGION_CODE = row["REGION_CODE"].ToString(),
                                                    REGION_NAME = row["REGION_NAME"].ToString(),
                                                    AM_CODE = row["AM_CODE"].ToString(),
                                                    AM_NAME = row["AM_NAME"].ToString(),
                                                    AREA_CODE = row["AREA_CODE"].ToString(),
                                                    AREA_NAME = row["AREA_NAME"].ToString(),
                                                    TARGET_AMT = row["TARGET_AMT"].ToString(),
                                                    TO_DAY_SALES = row["TO_DAY_SALES"].ToString(),
                                                    TO_DAY_BOX = row["TO_DAY_BOX"].ToString(),
                                                    UPTO_SALES = row["UPTO_SALES"].ToString(),
                                                    UPTO_BOX = row["UPTO_BOX"].ToString(),
                                                    ACH = row["ACH"].ToString(),
                                                    LM_UPTO_SALES = row["LM_UPTO_SALES"].ToString(),
                                                    LM_UPTO_BOX = row["LM_UPTO_BOX"].ToString(),
                                                    GROWTH = row["GROWTH"].ToString(),
                                                    CM_CUST = row["CM_CUST"].ToString(),
                                                    LM_CUST = row["LM_CUST"].ToString()


                                                }).ToList();
            return mpoWiseSwtDrpsSalesAchValue;
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
