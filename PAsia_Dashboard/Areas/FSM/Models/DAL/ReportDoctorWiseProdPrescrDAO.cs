using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using PAsia_Dashboard.Areas.FSM.Models.BEL;
using PAsia_Dashboard.Areas.FSM.Models.BEL.BEO;
using PAsia_Dashboard.Universal.Gateway;

namespace PAsia_Dashboard.Areas.FSM.Models.DAL
{
    public class ReportDoctorWiseProdPrescrDAO : ReturnData
    {
        DBConnection dbConn = new DBConnection();
        DBHelper dbHelper = new DBHelper();
        IDGenerated idGenerated = new IDGenerated();
        private DataRow _row;

        public object GetDoctorWiseProdPrescrData(string depotCode, string zoneCode, string regionCode, string areaCode, string territoryCode, string fromDate, string toDate, string prodType, string doctorType)
        {
            try
            {
                depotCode = depotCode ?? "";
                zoneCode = zoneCode ?? "";
                regionCode = regionCode ?? "";
                areaCode = areaCode ?? "";
                territoryCode = territoryCode ?? "";
                prodType = prodType ?? "";
                doctorType = doctorType ?? "";
                using (OracleConnection objConn = new OracleConnection(dbConn.SAConnStrReader("Sales")))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_DOC_WISE_PRES";//"get_count_emp_by_dept";
                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.Parameters.Add("vSTART_DATE", OracleType.VarChar).Value = fromDate;
                        objCmd.Parameters.Add("vEND_DATE", OracleType.VarChar).Value = toDate;
                        objCmd.Parameters.Add("P_ZONE_CODE", OracleType.VarChar).Value = zoneCode.Trim();
                        objCmd.Parameters.Add("P_REGION_CODE", OracleType.VarChar).Value = regionCode.Trim();
                        objCmd.Parameters.Add("P_AREA_CODE", OracleType.VarChar).Value = areaCode.Trim();
                        objCmd.Parameters.Add("P_TERRITORY_CODE", OracleType.VarChar).Value = territoryCode.Trim();
                        objCmd.Parameters.Add("P_DEPOT_CODE", OracleType.VarChar).Value = depotCode.Trim();
                        objCmd.Parameters.Add("P_TYPE", OracleType.VarChar).Value = prodType;
                        objCmd.Parameters.Add("D_TYPE", OracleType.VarChar).Value = doctorType;
                        objCmd.Parameters.Add("return_value", OracleType.Cursor).Direction = ParameterDirection.ReturnValue;
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
                                    select new ReportDoctorWiseProdPrescrBEO
                                    {
                                        SL_NO = ++count,
                                        DOCTOR_CODE = row["DOCTOR_CODE"].ToString(),
                                        DOCTOR_NAME = row["DOCTOR_NAME"].ToString(),
                                        DESIGNATION = row["DESIGNATION"].ToString(),
                                        DEGREES = row["DEGREES"].ToString(),
                                        //DEPOT_CODE = row["DEPOT_CODE"].ToString(),
                                        // DEPOT_NAME = row["DEPOT_NAME"].ToString(),
                                        ZONE_CODE = row["ZONE_CODE"].ToString(),
                                        ZONE_NAME = row["ZONE_NAME"].ToString(),
                                        REGION_CODE = row["REGION_CODE"].ToString(),
                                        REGION_NAME = row["REGION_NAME"].ToString(),
                                        AREA_CODE = row["AREA_CODE"].ToString(),
                                        AREA_NAME = row["AREA_NAME"].ToString(),
                                        TERRITORY_CODE = row["TERRITORY_CODE"].ToString(),
                                        TERRITORY_NAME = row["TERRITORY_NAME"].ToString(),
                                        TOT_PRES = row["TOT_PRES"].ToString(),
                                        CLASS_GROUP = row["CLASS_GROUP"].ToString(),
                                    }).ToList();
                        return item;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return ExceptionReturn = e.Message;
            }
        }

        public List<ReportMPOWisePrescriptionInfoBEO> GetMPOWisePrescriptionData(string depotCode, string zoneCode, string regionCode, string areaCode, string territoryCode, string fromDate, string toDate, string doctorCode, string ProdType)
        {
            try
            {
                var queryParam = " ";
                string CODE = HttpContext.Current.Session["CODE"].ToString();
                string ACCESS_LEVEL = HttpContext.Current.Session["ACCESS_LEVEL"].ToString();
                //if (ACCESS_LEVEL == "Z")
                //{
                //    string qry =
                //        "SELECT ZONE_CODE FROM EMPLOYEE_INFO WHERE EMPLOYEE_CODE='" + CODE + "' ";
                //    _row = dbHelper.GetDataTable(dbConn.SAConnStrReader("Dashboard"), qry).Rows[0];
                //    zoneCode = _row["ZONE_CODE"].ToString();
                //}
                //if (ACCESS_LEVEL == "R")
                //{
                //    string qry =
                //        "SELECT REGION_CODE FROM EMPLOYEE_INFO WHERE EMPLOYEE_CODE='" + CODE + "' ";
                //    _row = dbHelper.GetDataTable(dbConn.SAConnStrReader("Dashboard"), qry).Rows[0];
                //    regionCode = _row["REGION_CODE"].ToString();
                //}
                //else if (ACCESS_LEVEL == "A")
                //{
                //    string qry =
                //        "SELECT AREA_CODE FROM EMPLOYEE_INFO WHERE EMPLOYEE_CODE='" + CODE + "' ";
                //    _row = dbHelper.GetDataTable(dbConn.SAConnStrReader("Dashboard"), qry).Rows[0];
                //    areaCode = _row["AREA_CODE"].ToString();
                //}
                //if (!string.IsNullOrEmpty(depotCode))
                //{
                //    queryParam += "WHERE DEPOT_CODE='" + depotCode + "' ";
                //}
                //if (!string.IsNullOrEmpty(zoneCode))
                //{
                //    queryParam += "AND ZONE_CODE='" + zoneCode + "' ";
                //}
                //if (!string.IsNullOrEmpty(regionCode))
                //{
                //    queryParam += " AND REGION_CODE='" + regionCode + "' ";
                //}
                //if (!string.IsNullOrEmpty(areaCode))
                //{
                //    queryParam += " AND AREA_CODE='" + areaCode + "' ";
                //}
                //if (!string.IsNullOrEmpty(territoryCode))
                //{
                //    queryParam += " AND TERRITORY_CODE='" + territoryCode + "' ";
                //}
                if (!string.IsNullOrEmpty(doctorCode))
                {
                    queryParam += " Where VFP.DOCTOR_CODE='" + doctorCode + "' ";
                }
                if (!string.IsNullOrEmpty(ProdType))
                {
                    queryParam += " AND P.TYPE='" + ProdType + "' ";
                }
                if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
                {
                    queryParam += " AND TRUNC(CAPTURE_TIME) BETWEEN TO_DATE('" + fromDate + "','DD-MM-YYYY') AND TO_DATE('" + toDate + "','DD-MM-YYYY') ";
                }


                string Qry = "SELECT DISTINCT VFP.MST_SL,VFP.DOCTOR_NAME,VFP.DOCTOR_CODE,ZONE_CODE,ZONE_NAME,REGION_CODE,REGION_NAME,AREA_CODE,AREA_NAME,TERRITORY_CODE,TERRITORY_NAME,TO_CHAR(VFP.CAPTURE_TIME,'dd-MM-yyyy HH:mm:ss') CAPTURE_TIME, VFP.PRESCRIPTION_URL, VFP.PRESCRIPTION_TYPE, VFP.USER_ID,EI.MIO_NAME EMPLOYEE_NAME, " +
                             " (SELECT COUNT(PRODUCT_CODE) FROM  FSM_PRESCRIPTION_DTL FPD WHERE   FPD.MST_SL=VFP.MST_SL GROUP BY FPD.MST_SL) TOTAL_PROD" +
                             " FROM VW_FSM_PRESCRIPTION  VFP LEFT JOIN VW_PAL_FIELD_FORCE_MIO EI ON VFP.USER_ID = EI.MIO_CODE " +
                             " INNER JOIN PRODUCT_DETAILS P ON VFP.PRODUCT_CODE=P.S_PRODUCT_CODE " + queryParam + " ORDER BY  CAPTURE_TIME DESC,EI.MIO_NAME";
                //string Qry = "SELECT  DOCTOR_CODE, DOCTOR_NAME, PRACTICING_DAY, PRESCRIPTION_PER_DAY,HONORARIUM_AMOUNT, TERRITORY_CODE_4P, TO_CHAR(SET_DATE,'DD-MM-YYYY') SET_DATE FROM VW_FSM_DOC_HONORARIUM " + queryParam+"";
                DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader("Sales"), Qry);
                int count = 0;
                List<ReportMPOWisePrescriptionInfoBEO> item;
                item = (from DataRow row in dt.Rows
                        select new ReportMPOWisePrescriptionInfoBEO
                        {
                            SL_NO = ++count,
                            MST_SL = row["MST_SL"].ToString(),
                            //SET_DATE = row["SET_DATE"].ToString(),
                            DOCTOR_CODE = row["DOCTOR_CODE"].ToString(),
                            DOCTOR_NAME = row["DOCTOR_NAME"].ToString(),
                            CAPTURE_TIME = row["CAPTURE_TIME"].ToString(),
                            REGION_NAME = row["REGION_NAME"].ToString(),
                            AREA_NAME = row["AREA_NAME"].ToString(),
                            TERRITORY_NAME = row["TERRITORY_NAME"].ToString(),
                            //CAPTURE_TIME = row["CAPTURE_TIME"].ToString() == ""
                            //    ? ""
                            //    : ((DateTime)row["CAPTURE_TIME"]).ToString("dd-MM-yyyy HH:mm:ss"),
                            PRESCRIPTION_URL = row["PRESCRIPTION_URL"].ToString().Replace("~", ""),
                            PRESCRIPTION_TYPE = row["PRESCRIPTION_TYPE"].ToString(),
                            USER_ID = row["USER_ID"].ToString(),
                            EMPLOYEE_NAME = row["EMPLOYEE_NAME"].ToString(),
                            TOTAL_PROD = row["TOTAL_PROD"].ToString(),


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