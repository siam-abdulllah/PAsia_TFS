using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using PAsia_Dashboard.Areas.Dashboard.Models.BEL;
using PAsia_Dashboard.Areas.Dashboard.Models.DAO;
using PAsia_Dashboard.Areas.Reports.Models.BEl;
using PAsia_Dashboard.Universal.Gateway;

namespace PAsia_Dashboard.Areas.Reports.Models.DAO
{
    public class TargetSalesDAO:ReturnData
    {
        HomeDashboardDAO homeDashboardDao = new HomeDashboardDAO();
        DBHelper dbHelper = new DBHelper();
        DBConnection _dbConn = new DBConnection();

        public object GetTargetSalesValue(string fromDate, string toDate)
        {
            try
            {

                //fromDate =( fromDate == "" || fromDate == null) ? "01/02/2022" : fromDate;
                //toDate = toDate == "" || toDate ==null ? "14/02/2022" : toDate;

                using (OracleConnection objConn = new OracleConnection(_dbConn.SAConnStrReader("Sales")))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_NATIONAL_TAR_SALES";
                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.Parameters.Add("vFROM_DATE", OracleType.VarChar).Value = fromDate;
                        objCmd.Parameters.Add("vTO_DATE", OracleType.VarChar).Value = toDate;
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
                        List<TargetSales> item;
                        item = (from DataRow row in dt.Rows
                                select new TargetSales
                                {
                                    SL_NO = ++count,
                                    PRODUCT_CODE = row["PRODUCT_CODE"].ToString(),
                                    PRODUCT_NAME = row["PRODUCT_NAME"].ToString(),
                                    BRAND_NAME = row["BRAND_NAME"].ToString(),
                                    PACK_SIZE = row["PACK_SIZE"].ToString(),
                                    UNIT_TARGET= row["UNIT_TARGET"].ToString(),
                                    VALUE_TARGET = row["VALUE_TARGET"].ToString(),
                                    UNIT_SALES = row["UNIT_SALES"].ToString(),
                                    VALUE_SALES = row["VALUE_SALES"].ToString(),
                                    CURRENT_STOCK = row["CURRENT_STOCK"].ToString(),
                                    FROM_DATE = row["FROM_DATE"].ToString(),
                                    TO_DATE = row["TO_DATE"].ToString(),
                                }).ToList();
                        return item;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return ExceptionReturn = "";
            }
        }

    }
}