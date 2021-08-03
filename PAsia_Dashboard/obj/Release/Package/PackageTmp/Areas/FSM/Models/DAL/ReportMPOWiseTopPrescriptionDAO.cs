using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using PAsia_Dashboard.Areas.FSM.Models.BEL.BEO;
using PAsia_Dashboard.Universal.Gateway;

namespace PAsia_Dashboard.Areas.FSM.Models.DAL.DAO
{
    public class ReportMPOWiseTopPrescriptionDAO
    {
        readonly DBConnection _dbConn = new DBConnection();
        readonly DBHelper _dbHelper = new DBHelper();
        private IDGenerated _idGenerated = new IDGenerated();
        readonly DBConnection _dbConnection = new DBConnection();
        private DataRow _row;
        private DataTable _dt;

        public List<ReportMPOWiseTopPrescriptionBEO> GetMPOWiseTopPrescriptionData(string depotCode, string zoneCode, string regionCode, string areaCode, string territoryCode, string fromDate, string toDate)
        {
            try
            {
                string queryParam = " ";
                string CODE = HttpContext.Current.Session["CODE"].ToString();
                string ACCESS_LEVEL = HttpContext.Current.Session["ACCESS_LEVEL"].ToString();
                if (ACCESS_LEVEL == "Z")
                {
                    string qry =
                        "SELECT ZONE_CODE FROM EMPLOYEE_INFO WHERE EMPLOYEE_CODE='" + CODE + "' ";
                    _row = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), qry).Rows[0];
                    zoneCode = _row["ZONE_CODE"].ToString();
                }
                if (ACCESS_LEVEL == "R")
                {
                    string qry =
                        "SELECT REGION_CODE FROM EMPLOYEE_INFO WHERE EMPLOYEE_CODE='" + CODE + "' ";
                    _row = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), qry).Rows[0];
                    regionCode = _row["REGION_CODE"].ToString();
                }
                else if (ACCESS_LEVEL == "A")
                {
                    string qry =
                        "SELECT AREA_CODE FROM EMPLOYEE_INFO WHERE EMPLOYEE_CODE='" + CODE + "' ";
                    _row = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), qry).Rows[0];
                    areaCode = _row["AREA_CODE"].ToString();
                }

                if (!string.IsNullOrEmpty(depotCode))
                {
                    queryParam += "AND FFM.DEPOT_CODE='" + depotCode + "' ";
                }
                if (!string.IsNullOrEmpty(zoneCode))
                {
                    queryParam += "AND FFM.ZONE_CODE='" + zoneCode + "' ";
                }
                if (!string.IsNullOrEmpty(regionCode))
                {
                    queryParam += " AND FFM.REGION_CODE='" + regionCode + "' ";
                }
                if (!string.IsNullOrEmpty(areaCode))
                {
                    queryParam += " AND FFM.AREA_CODE='" + areaCode + "' ";
                }
                if (!string.IsNullOrEmpty(territoryCode))
                {
                    queryParam += " AND FFM.TERRITORY_CODE='" + territoryCode + "' ";
                }
                if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
                {
                    queryParam += " AND TRUNC(SET_DATE) BETWEEN TO_DATE('" + fromDate + "','DD-MM-YYYY') AND TO_DATE('" + toDate + "','DD-MM-YYYY') ";
                }
                //string Qry = "SELECT DISTINCT USER_ID,MIO_NAME,FN_MIO_DESIG_NAME(USER_ID) DESIG_NAME,TERRITORY_NAME,(SELECT COUNT(USER_ID) FROM FSM_PRESCRIPTION_MST WHERE USER_ID = fpm.USER_ID GROUP BY USER_ID )" +
                //         " PRESCRIPTION_QTY FROM FSM_PRESCRIPTION_MST fpm INNER JOIN VW_PAL_FIELD_FORCE_MIO ffm ON FPM.USER_ID = FFM.MIO_CODE " + queryParam + " ORDER BY PRESCRIPTION_QTY DESC";
                ////string Qry = "SELECT  DOCTOR_CODE, DOCTOR_NAME, PRACTICING_DAY, PRESCRIPTION_PER_DAY,HONORARIUM_AMOUNT, TERRITORY_CODE_4P, TO_CHAR(SET_DATE,'DD-MM-YYYY') SET_DATE FROM VW_FSM_DOC_HONORARIUM " + queryParam+"";
                string Qry = "SELECT DISTINCT USER_ID,MIO_NAME,FN_MIO_DESIG_NAME(USER_ID) DESIG_NAME,TERRITORY_NAME,COUNT (distinct MST_SL) PRESCRIPTION_QTY" +
                                        " FROM FSM_PRESCRIPTION_MST fpm INNER JOIN VW_PAL_FIELD_FORCE_MIO ffm ON FPM.USER_ID = FFM.MIO_CODE WHERE fpm.MST_SL  IN (SELECT DISTINCT MST_SL FROM FSM_PRESCRIPTION_DTL) " + queryParam + " GROUP BY USER_ID,MIO_NAME,TERRITORY_NAME" +
                                        "  ORDER BY PRESCRIPTION_QTY DESC";

                DataTable dt = _dbHelper.GetDataTable(_dbConn.SAConnStrReader("Sales"), Qry);
                int count = 0;
                List<ReportMPOWiseTopPrescriptionBEO> item;
                item = (from DataRow row in dt.Rows
                        select new ReportMPOWiseTopPrescriptionBEO
                        {
                            SL_NO = ++count,
                            //MST_SL = row["MST_SL"].ToString(),
                            USER_ID = row["USER_ID"].ToString(),
                            MIO_NAME = row["MIO_NAME"].ToString(),
                            DESIG_NAME = row["DESIG_NAME"].ToString(),
                            TERRITORY_NAME = row["TERRITORY_NAME"].ToString(),
                            PRESCRIPTION_QTY = row["PRESCRIPTION_QTY"].ToString(),

                        }).ToList();
                return item;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}