using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using PAsia_Dashboard.Areas.FSM.Models.BEL.BEO;
using PAsia_Dashboard.Universal.Gateway;

namespace PAsia_Dashboard.Areas.FSM.Models.DAL.DAO
{
    public class ReportAMWisePrescriptionDAO:ReturnData
    {
        readonly DBConnection _dbConn = new DBConnection();
        readonly DBHelper _dbHelper = new DBHelper();
        private IDGenerated _idGenerated = new IDGenerated();
        readonly DBConnection _dbConnection = new DBConnection();
        private DataRow _row;
        private DataTable _dt;

        public object GetAMWisePrescriptionData(string fromDate, string toDate)
        {
            try
            {
                using (OracleConnection objConn = new OracleConnection(_dbConn.SAConnStrReader("Sales")))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_AM_WISE_PRES_REPORT";
                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.Parameters.Add("vSTART_DATE", OracleType.VarChar).Value = fromDate;
                        objCmd.Parameters.Add("vEND_DATE", OracleType.VarChar).Value = toDate;
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
                        List<ReportAMWisePrescriptionBEO> item;
                        item = (from DataRow row in dt.Rows
                                select new ReportAMWisePrescriptionBEO
                                {
                                    SL_NO = ++count,
                                    AM_CODE = row["AM_CODE"].ToString(),
                                    AM_NAME = row["AM_NAME"].ToString(),
                                    REGION_CODE = row["REGION_CODE"].ToString(),
                                    REGION_NAME = row["REGION_NAME"].ToString(),
                                    AREA_CODE = row["AREA_CODE"].ToString(),
                                    AREA_NAME = row["AREA_NAME"].ToString(),
                                    TOTAL_PRESCRIPTION = row["TOTAL_PRESCRIPTION"].ToString(),
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