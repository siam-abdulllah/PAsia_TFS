using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Web;
using PAsia_Dashboard.Areas.FSM.Models.BEL;
using PAsia_Dashboard.Areas.FSM.Models.BEL.BEO;
using PAsia_Dashboard.Universal.Gateway;

namespace PAsia_Dashboard.Areas.FSM.Models.DAL
{
    public class StructureRefreshDAO:ReturnData
    {
        private readonly DBConnection dbConn = new DBConnection();
        private readonly DBHelper dbHelper = new DBHelper();

        public object GetStructureRefreshValue(string fromDate, string toDate)
        {
            try
            {

                //fromDate =( fromDate == "" || fromDate == null) ? "01/02/2022" : fromDate;
                //toDate = toDate == "" || toDate ==null ? "14/02/2022" : toDate;

                using (OracleConnection objConn = new OracleConnection(dbConn.SAConnStrReader("Sales")))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "PRC_PAL_FIELD_FORCE_MIO";
                        objCmd.CommandType = CommandType.StoredProcedure;                                             
                        objConn.Open();
                        objCmd.ExecuteNonQuery();
                       
                        return 0;

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