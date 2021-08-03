using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using PAsia_Dashboard.Areas.FSM.Models.BEL;
using PAsia_Dashboard.Universal.Gateway;

namespace PAsia_Dashboard.Areas.FSM.Models.DAL
{
    public class ReportDoctorWiseProdPrescrDAO : ReturnData
    {
        DBConnection dbConn = new DBConnection();
        DBHelper dbHelper = new DBHelper();
        IDGenerated idGenerated = new IDGenerated();

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

    }
}