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
    public class StockProdSalesDAO:ReturnData
    {

        HomeDashboardDAO homeDashboardDao = new HomeDashboardDAO();
        DBHelper dbHelper = new DBHelper();
        DBConnection _dbConn = new DBConnection();
        

        public object GetStockProdSalesValue(string fromDate, string toDate)
        {
            try
            {
                using (OracleConnection objConn = new OracleConnection(_dbConn.SAConnStrReader("Sales")))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_STOCK_AND_PRODUCT_SALES";
                        objCmd.CommandType = CommandType.StoredProcedure;
                        //objCmd.Parameters.Add("vSTART_DATE", OracleType.VarChar).Value = fromDate;
                        //objCmd.Parameters.Add("vEND_DATE", OracleType.VarChar).Value = toDate;
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
                        List<StockProdSalesBEO> item;
                        item = (from DataRow row in dt.Rows
                                select new StockProdSalesBEO
                                {
                                    SL_NO = ++count,
                                    PRODUCT_CODE = row["PRODUCT_CODE"].ToString(),
                                    PRODUCT_NAME = row["PRODUCT_NAME"].ToString(),
                                    PACK_SIZE = row["PACK_SIZE"].ToString(),
                                    TP_VAT = row["TP_VAT"].ToString(),
                                    OPENING_QTY = row["OPENING_QTY"].ToString(),
                                    THREE_NET_SALES_QTY = row["THREE_NET_SALES_QTY"].ToString(),
                                    TWO_NET_SALES_QTY = row["TWO_NET_SALES_QTY"].ToString(),
                                    ONE_NET_SALES_QTY = row["ONE_NET_SALES_QTY"].ToString(),
                                    THREE_MONTH_AVG_SALES = row["THREE_MONTH_AVG_SALES"].ToString(),
                                    UPTO_NET_SALES = row["UPTO_NET_SALES"].ToString(),
                                    CURRENT_STOCK = row["CURRENT_STOCK"].ToString(),
                                    SALES_STOCK = row["SALES_STOCK"].ToString(),
                                    DEFICIT = row["DEFICIT"].ToString(),
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
