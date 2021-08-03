using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Data;
using System.Linq;
using System.Web;

namespace PAsia_Dashboard.Universal.Gateway
{
    public class DBHelper
    {
        private readonly DBConnection _dbConnection = new DBConnection();
        public Boolean CmdExecute(string connString, string Qry)
        {
            bool isTrue = false;
            using (OracleConnection con = new OracleConnection(connString))
            {
                using (OracleCommand cmd = new OracleCommand(Qry, con))
                {
                    con.Open();
                    int noOfRows = cmd.ExecuteNonQuery();
                    if (noOfRows > 0)
                    {
                        isTrue = true;
                    }
                }
                return isTrue;
            }
        }

        public Tuple<Boolean, string> IsExistsWithGetSL(string connString, string qry)
        {
            string getSl = ""; bool isTrue = false;

            var dt = GetDataTable(connString, qry);
            if (dt.Rows.Count > 0)
            {
                isTrue = true;
                getSl = dt.Rows[0][0].ToString();
            }
            return Tuple.Create(isTrue, getSl);
        }
        public string GetSingleToken(string mpgRoup, string connType)
        {

            string getToken = "";
            string qry = "Select Token From Sa_UserToken Where MP_Group='" + mpgRoup + "'";
            DataTable dt = GetDataTable(_dbConnection.SAConnStrReader(connType), qry);
            if (dt.Rows.Count > 0)
            {
                getToken = dt.Rows[0][0].ToString();
            }
            return getToken;
        }
        public string GetMultipleToken(string mpgRoup, string connType)
        {
            string getToken = "";
            string Qry = "Select Token From Sa_UserToken Where MP_Group in (" + mpgRoup + ")";
            DataTable dt = GetDataTable(_dbConnection.SAConnStrReader(connType), Qry);
            if (dt.Rows.Count > 0)
            {

                getToken = dt.Rows[0][0].ToString();
            }
            return "";
        }
        //public Int64 CmdExecute(string Qry, string ConnType)
        //{

        //    Int64 noOfRows = 0;
        //    using (OracleConnection con = new OracleConnection(dbConnection.SAConnStrReader(ConnType)))
        //    {
        //        OracleCommand cmd = new OracleCommand(Qry, con);
        //        con.Open();
        //        noOfRows = cmd.ExecuteNonQuery();
        //    }
        //    return noOfRows;
        //}

        public OracleDataReader GetDataCustom(string qry, string connType)
        {
            string data = "";
            using (OracleConnection objConn = new OracleConnection(_dbConnection.SAConnStrReader(connType)))
            {
                objConn.Open();
                using (OracleCommand objCmd = new OracleCommand(qry, objConn))
                {
                    using (OracleDataReader rdr = objCmd.ExecuteReader())
                    {
                        //if (rdr.Read())
                        // {
                        // data = rdr[peram].ToString();
                        // }
                        rdr.Read();
                        objConn.Close();
                        objCmd.Cancel();
                        return rdr;
                    }
                }
            }
        }
        //public OracleDataReader GetDataCustom(string Qry)
        //{
        //    string data = "";
        //    OracleDataReader d;
        //    using (OracleConnection objConn = new OracleConnection(dbConnection.SAConnStrReader(ConnType)))
        //    {

        //        objConn.Open();
        //        using (OracleCommand objCmd = new OracleCommand(Qry, objConn))
        //        {
        //            using (OracleDataReader rdr = objCmd.ExecuteReader())
        //            {
        //                rdr.Read();
        //                d = rdr;
        //            }
        //        }
        //    }

        //    return d;


        //}
        public DataSet GetDataSet(string qry, string connType)
        {
            using (OracleDataAdapter odbcDataAdapter = new OracleDataAdapter(qry, _dbConnection.SAConnStrReader(connType)))
            {
                DataSet ds = new DataSet();
                odbcDataAdapter.Fill(ds, "Results");
                return ds;
            }

        }

        public DataTable ReturnCursorF1(string Conn, string fName, string hInput1, string vInput1)
        {
            try
            {
                using (OracleConnection objConn = new OracleConnection(Conn))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = fName;
                        objCmd.CommandType = CommandType.StoredProcedure;
                        //objCmd.Parameters.Add(hInput1, OracleType.VarChar).Value = vInput1;    
                        objCmd.Parameters.Add("return_value", OracleType.Cursor).Direction = ParameterDirection.ReturnValue;
                        objConn.Open();
                        objCmd.ExecuteNonQuery();
                        using (OracleDataReader rdr = objCmd.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            if (rdr.HasRows)
                            {
                                dt.Load(rdr);

                            }
                        }
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public DataTable GetDataTable(string connString, string qry)
        {
            try { 
            DataTable dt = new DataTable();
            using (OracleConnection objConn = new OracleConnection(connString))
            {
                using (OracleCommand objCmd = new OracleCommand())
                {
                    objCmd.CommandText = qry;
                    objCmd.Connection = objConn;
                    objConn.Open();
                    objCmd.ExecuteNonQuery();
                    using (OracleDataReader rdr = objCmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            dt.Load(rdr);
                        }
                    }
                }
            }
            return dt;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public string GetValue(string connString, string qry)
        {
            string value = "";
            using (OracleConnection odbcConnection = new OracleConnection(connString))
            {
                odbcConnection.Open();
                using (OracleCommand odbcCommand = new OracleCommand(qry, odbcConnection))
                {
                    using (OracleDataReader rdr = odbcCommand.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            value = rdr[0].ToString();
                        }
                        rdr.Close();
                    }
                    odbcConnection.Close();
                    return value;
                }

            }

        }

        public DataTable GetDataTableRefCursorF1(string funName, string fieldName, string fieldValue, string connType)
        {
            DataTable dt = new DataTable();
            using (OracleConnection objConn = new OracleConnection(_dbConnection.SAConnStrReader(connType)))
            {
                using (OracleCommand objCmd = new OracleCommand())
                {
                    objCmd.Connection = objConn;
                    objCmd.CommandText = funName;
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add(fieldName, OracleType.VarChar).Value = fieldValue;
                    objCmd.Parameters.Add("ReturnValue", OracleType.Cursor).Direction = ParameterDirection.ReturnValue;
                    objConn.Open();
                    objCmd.ExecuteNonQuery();
                    using (OracleDataReader rdr = objCmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            dt.Load(rdr);
                        }
                    }
                }
            }
            return dt;
        }
        public DataTable GetDataTableRefCursorF2(string funName, string fieldName1, string fieldName2, string fieldValue1, string fieldValue2, string connType)
        {
            DataTable dt = new DataTable();
            using (OracleConnection objConn = new OracleConnection(_dbConnection.SAConnStrReader(connType)))
            {
                using (OracleCommand objCmd = new OracleCommand())
                {
                    objCmd.Connection = objConn;
                    objCmd.CommandText = funName;
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add(fieldName1, OracleType.VarChar).Value = fieldValue1;
                    objCmd.Parameters.Add(fieldName2, OracleType.VarChar).Value = fieldValue2;
                    objCmd.Parameters.Add("ReturnValue", OracleType.Cursor).Direction = ParameterDirection.ReturnValue;
                    objConn.Open();
                    objCmd.ExecuteNonQuery();
                    using (OracleDataReader rdr = objCmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            dt.Load(rdr);
                        }
                    }
                }
            }
            return dt;
        }


        public DataTable GetDataTableRefCursorF3(string funName, string fieldName1, string fieldName2, string fieldName3, string fieldValue1, string fieldValue2, string fieldValue3, string connType)
        {
            DataTable dt = new DataTable();
            using (OracleConnection objConn = new OracleConnection(_dbConnection.SAConnStrReader(connType)))
            {
                using (OracleCommand objCmd = new OracleCommand())
                {
                    objCmd.Connection = objConn;
                    objCmd.CommandText = funName;
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add(fieldName1, OracleType.VarChar).Value = fieldValue1;
                    objCmd.Parameters.Add(fieldName2, OracleType.VarChar).Value = fieldValue2;
                    objCmd.Parameters.Add(fieldName3, OracleType.VarChar).Value = fieldValue3;
                    objCmd.Parameters.Add("ReturnValue", OracleType.Cursor).Direction = ParameterDirection.ReturnValue;
                    objConn.Open();
                    objCmd.ExecuteNonQuery();
                    using (OracleDataReader rdr = objCmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            dt.Load(rdr);
                        }
                    }
                }
            }
            return dt;
        }


        public Boolean CmdProcedureF1(string qry, string spName, string fieldName, string fieldValue, string connType)
        {
            bool isTrue = false;
            using (OracleConnection oracleConnection = new OracleConnection(_dbConnection.SAConnStrReader(connType)))
            {
                using (OracleCommand oracleCommand = new OracleCommand())
                {
                    oracleCommand.Connection = oracleConnection;
                    oracleCommand.CommandText = spName;
                    oracleCommand.CommandType = CommandType.StoredProcedure;
                    oracleCommand.Parameters.Add(fieldName, OracleType.VarChar).Value = fieldValue;
                    oracleConnection.Open();
                    if (oracleCommand.ExecuteNonQuery() > 0)
                    {
                        isTrue = true;
                    }
                }
            }
            return isTrue;
        }

        public Boolean CmdProcedureF2(string spName, string fieldName1, string fieldName2, string fieldValue1, string fieldValue2, string connType)
        {
            bool isTrue = false;
            using (OracleConnection oracleConnection = new OracleConnection(_dbConnection.SAConnStrReader(connType)))
            {
                using (OracleCommand oracleCommand = new OracleCommand())
                {
                    oracleCommand.Connection = oracleConnection;
                    oracleCommand.CommandText = spName;
                    oracleCommand.CommandType = CommandType.StoredProcedure;
                    oracleCommand.Parameters.Add(fieldName1, OracleType.VarChar).Value = fieldValue1;
                    oracleCommand.Parameters.Add(fieldName2, OracleType.VarChar).Value = fieldValue2;

                    oracleConnection.Open();

                    if (oracleCommand.ExecuteNonQuery() > 0)
                    {
                        isTrue = true;
                    }
                }
            }
            return isTrue;
        }

        public Boolean CmdProcedureF3(string spName, string fieldName1, string fieldName2, string fieldName3, string fieldValue1, string fieldValue2, string fieldValue3, string connType)
        {
            bool isTrue = false;
            using (OracleConnection oracleConnection = new OracleConnection(_dbConnection.SAConnStrReader(connType)))
            {
                using (OracleCommand oracleCommand = new OracleCommand())
                {
                    oracleCommand.Connection = oracleConnection;
                    oracleCommand.CommandText = spName;
                    oracleCommand.CommandType = CommandType.StoredProcedure;
                    oracleCommand.Parameters.Add(fieldName1, OracleType.VarChar).Value = fieldValue1;
                    oracleCommand.Parameters.Add(fieldName2, OracleType.VarChar).Value = fieldValue2;
                    oracleCommand.Parameters.Add(fieldName3, OracleType.VarChar).Value = fieldValue3;
                    oracleConnection.Open();
                    if (oracleCommand.ExecuteNonQuery() > 0)
                    {
                        isTrue = true;
                    }
                }
            }
            return isTrue;
        }
        public DataTable dt { get; set; }
    }
}
