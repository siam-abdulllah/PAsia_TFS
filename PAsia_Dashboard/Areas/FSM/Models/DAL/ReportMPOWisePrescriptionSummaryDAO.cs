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
    public class ReportMPOWisePrescriptionSummaryDAO : ReturnData
    {
        readonly DBConnection _dbConn = new DBConnection();
        readonly DBHelper _dbHelper = new DBHelper();
        private IDGenerated _idGenerated = new IDGenerated();
        readonly DBConnection _dbConnection = new DBConnection();
        private DataRow _row;
        private DataTable _dt;
        public object GetMPOWisePrescriptionSummaryData(string depotCode, string zoneCode, string regionCode, string areaCode, string territoryCode, string fromDate, string toDate)
        {
            string CODE = HttpContext.Current.Session["CODE"].ToString();
            string ACCESS_LEVEL = HttpContext.Current.Session["ACCESS_LEVEL"].ToString();
            string dsmCode = null;
            string rsmCode = null;
            string amCode = null;
            string mioCode = null;
            if (ACCESS_LEVEL == "Z")
            {
                //string qry =
                //    "SELECT ZONE_CODE FROM EMPLOYEE_INFO WHERE EMPLOYEE_CODE='" + CODE + "' ";
                //_row = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), qry).Rows[0];
                //zoneCode = _row["ZONE_CODE"].ToString();
                 dsmCode = CODE;
            }
            if (ACCESS_LEVEL == "R")
            {
                //string qry =
                //    "SELECT REGION_CODE FROM EMPLOYEE_INFO WHERE EMPLOYEE_CODE='" + CODE + "' ";
                //_row = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), qry).Rows[0];
                //regionCode = _row["REGION_CODE"].ToString();
                 rsmCode = CODE;
            }
            else if (ACCESS_LEVEL == "A")
            {
                //string qry =
                //    "SELECT AREA_CODE FROM EMPLOYEE_INFO WHERE EMPLOYEE_CODE='" + CODE + "' ";
                //_row = _dbHelper.GetDataTable(_dbConnection.SAConnStrReader("Dashboard"), qry).Rows[0];
                //areaCode = _row["AREA_CODE"].ToString();
                 amCode = CODE;
            }
            try
            {
                //depotCode = depotCode ?? "ALL";
                //zoneCode = zoneCode ?? "ALL";
                //regionCode = regionCode ?? "ALL";
                //areaCode = areaCode ?? "ALL";
                //territoryCode = territoryCode ?? "ALL";               
                depotCode = depotCode ?? "ALL";
                dsmCode = dsmCode ?? "ALL";
                rsmCode = rsmCode ?? "ALL";
                amCode = amCode ?? "ALL";
                mioCode = mioCode ?? "ALL";
                //territoryCode = territoryCode ?? "ALL";
                using (OracleConnection objConn = new OracleConnection(_dbConn.SAConnStrReader("Sales")))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_MIO_WISE_PRES_SUM"; //"get_count_emp_by_dept";
                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.Parameters.Add("vSTART_DATE", OracleType.VarChar).Value = fromDate;
                        objCmd.Parameters.Add("vEND_DATE", OracleType.VarChar).Value = toDate;
                        objCmd.Parameters.Add("P_DEPOT_CODE", OracleType.VarChar).Value = depotCode.Trim();
                        //objCmd.Parameters.Add("P_ZONE_CODE", OracleType.VarChar).Value = zoneCode.Trim();
                        //objCmd.Parameters.Add("P_REGION_CODE", OracleType.VarChar).Value = regionCode.Trim();
                        //objCmd.Parameters.Add("P_AREA_CODE", OracleType.VarChar).Value = areaCode.Trim();
                        //objCmd.Parameters.Add("P_TERRITORY_CODE", OracleType.VarChar).Value = territoryCode.Trim();
                        objCmd.Parameters.Add("P_DSM_CODE", OracleType.VarChar).Value = dsmCode.Trim();
                        objCmd.Parameters.Add("P_RSM_CODE", OracleType.VarChar).Value = rsmCode.Trim();
                        objCmd.Parameters.Add("P_AM_CODE", OracleType.VarChar).Value = amCode.Trim();
                        objCmd.Parameters.Add("P_MIO_CODE", OracleType.VarChar).Value = mioCode.Trim();
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
                        List<ReportMPOWisePrescriptionSummaryBEO> item;
                        item = (from DataRow row in dt.Rows
                                select new ReportMPOWisePrescriptionSummaryBEO
                                {
                                    SL_NO = ++count,
                                    USER_ID = row["USER_ID"].ToString(),
                                    USER_NAME = row["USER_NAME"].ToString(),
                                    REGION_NAME = row["REGION_NAME"].ToString(),
                                    AREA_NAME = row["AREA_NAME"].ToString(),
                                    TERRITORY_NAME = row["TERRITORY_NAME"].ToString(),
                                    NO_OF_OPD_PRES = row["NO_OF_OPD_PRES"].ToString(),
                                    NO_OF_OTHER_PRES = row["NO_OF_OTHER_PRES"].ToString(),
                                    TOTAL_PRES = row["TOTAL_PRES"].ToString(),
                                    TOTAL_PRODUCT = row["TOTAL_PRODUCT"].ToString(),
                                    TOTAL_XELPRO = row["TOTAL_XELPRO"].ToString(),
                                    TOTAL_BIONIC = row["TOTAL_BIONIC"].ToString(),
                                    //TOTAL_XELPRO_MUPS = row["TOTAL_XELPRO_MUPS"].ToString(),
                                    TOTAL_CARDOTEL = row["TOTAL_CARDOTEL"].ToString(),
                                    TOTAL_FUXTIL = row["TOTAL_FUXTIL"].ToString(),
                                    TOTAL_EZYLIFE = row["TOTAL_EZYLIFE"].ToString(),
                                    //TOTAL_EZYLIFE_KID = row["TOTAL_EZYLIFE_KID"].ToString(),
                                    TOTAL_SWEET_DROPS = row["TOTAL_SWEET_DROPS"].ToString(),
                                    TOTAL_MONKAST = row["TOTAL_MONKAST"].ToString(),
                                    TOTAL_STROMEC = row["TOTAL_STROMEC"].ToString(),
                                    TOTAL_DEEP_HEAT_NIGHT_RELIEF = row["TOTAL_DEEP_HEAT_NIGHT_RELIEF"].ToString(),
                                    TOTAL_RY_JELLY = row["TOTAL_RY_JELLY"].ToString(),
                                    TOTAL_AZEXIA = row["TOTAL_AZEXIA"].ToString(),
                                    TOTAL_CLONZY = row["TOTAL_CLONZY"].ToString(),
                                    TOTAL_EMIREST = row["TOTAL_EMIREST"].ToString(),
                                    TOTAL_NAPRONIL_PLUS = row["TOTAL_NAPRONIL_PLUS"].ToString(),
                                    TOTAL_VELOFIX = row["TOTAL_VELOFIX"].ToString(),
                                    TOTAL_RETROVIR = row["TOTAL_RETROVIR"].ToString(),
                                    TOTAL_GAVICOOL = row["TOTAL_GAVICOOL"].ToString(),
                                    TOTAL_VCENT = row["TOTAL_VCENT"].ToString(),
                                    TOTAL_AIRUP = row["TOTAL_AIRUP"].ToString(),
                                    TOTAL_RINOBIL = row["TOTAL_RINOBIL"].ToString(),
                                    TOTAL_XTRACALD = row["TOTAL_XTRACALD"].ToString(),
                                    TOTAL_TELMIFAST = row["TOTAL_TELMIFAST"].ToString(),
                                    TOTAL_OTHERS = row["TOTAL_OTHERS"].ToString()
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
        public object GetZoneWisePrescriptionSummaryData(string zoneCode, string fromDate, string toDate)
        {
            try
            {
                using (OracleConnection objConn = new OracleConnection(_dbConn.SAConnStrReader("Sales")))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {

                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_ZONE_WISE_PRES_SUM";//"get_count_emp_by_dept";
                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.Parameters.Add("vSTART_DATE", OracleType.VarChar).Value = fromDate;
                        objCmd.Parameters.Add("vEND_DATE", OracleType.VarChar).Value = toDate;
                        objCmd.Parameters.Add("P_ZONE_CODE", OracleType.VarChar).Value = zoneCode.Trim();
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
                        List<ReportMPOWisePrescriptionSummaryBEO> item;
                        item = (from DataRow row in dt.Rows
                                select new ReportMPOWisePrescriptionSummaryBEO
                                {
                                    SL_NO = ++count,
                                    ZONE_NAME = row["ZONE_NAME"].ToString(),
                                    NO_OF_OPD_PRES = row["NO_OF_OPD_PRES"].ToString(),
                                    NO_OF_OTHER_PRES = row["NO_OF_OTHER_PRES"].ToString(),
                                    TOTAL_PRES = row["TOTAL_PRES"].ToString(),
                                    TOTAL_PRODUCT = row["TOTAL_PRODUCT"].ToString(),
                                    TOTAL_XELPRO = row["TOTAL_XELPRO"].ToString(),
                                    TOTAL_BIONIC = row["TOTAL_BIONIC"].ToString(),
                                    //TOTAL_XELPRO_MUPS = row["TOTAL_XELPRO_MUPS"].ToString(),
                                    TOTAL_CARDOTEL = row["TOTAL_CARDOTEL"].ToString(),
                                    TOTAL_FUXTIL = row["TOTAL_FUXTIL"].ToString(),
                                    TOTAL_EZYLIFE = row["TOTAL_EZYLIFE"].ToString(),
                                    //TOTAL_EZYLIFE_KID = row["TOTAL_EZYLIFE_KID"].ToString(),
                                    TOTAL_SWEET_DROPS = row["TOTAL_SWEET_DROPS"].ToString(),
                                    TOTAL_MONKAST = row["TOTAL_MONKAST"].ToString(),
                                    TOTAL_STROMEC = row["TOTAL_STROMEC"].ToString(),
                                    TOTAL_DEEP_HEAT_NIGHT_RELIEF = row["TOTAL_DEEP_HEAT_NIGHT_RELIEF"].ToString(),
                                    TOTAL_RY_JELLY = row["TOTAL_RY_JELLY"].ToString(),
                                    TOTAL_AZEXIA = row["TOTAL_AZEXIA"].ToString(),
                                    TOTAL_CLONZY = row["TOTAL_CLONZY"].ToString(),
                                    TOTAL_EMIREST = row["TOTAL_EMIREST"].ToString(),
                                    TOTAL_NAPRONIL_PLUS = row["TOTAL_NAPRONIL_PLUS"].ToString(),
                                    TOTAL_VELOFIX = row["TOTAL_VELOFIX"].ToString(),
                                    TOTAL_RETROVIR = row["TOTAL_RETROVIR"].ToString(),
                                    TOTAL_GAVICOOL = row["TOTAL_GAVICOOL"].ToString(),
                                    TOTAL_VCENT = row["TOTAL_VCENT"].ToString(),
                                    TOTAL_AIRUP = row["TOTAL_AIRUP"].ToString(),
                                    TOTAL_RINOBIL = row["TOTAL_RINOBIL"].ToString(),
                                    TOTAL_XTRACALD = row["TOTAL_XTRACALD"].ToString(),
                                    TOTAL_TELMIFAST = row["TOTAL_TELMIFAST"].ToString(),
                                    TOTAL_OTHERS = row["TOTAL_OTHERS"].ToString()
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
        public object GetRegionWisePrescriptionSummaryData(string regionCode, string fromDate, string toDate)
        {
            try
            {
                using (OracleConnection objConn = new OracleConnection(_dbConn.SAConnStrReader("Sales")))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_REGION_WISE_PRES_SUM";//"get_count_emp_by_dept";
                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.Parameters.Add("vSTART_DATE", OracleType.VarChar).Value = fromDate;
                        objCmd.Parameters.Add("vEND_DATE", OracleType.VarChar).Value = toDate;
                        objCmd.Parameters.Add("P_REGION_CODE", OracleType.VarChar).Value = regionCode.Trim();
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
                        List<ReportMPOWisePrescriptionSummaryBEO> item;
                        item = (from DataRow row in dt.Rows
                                select new ReportMPOWisePrescriptionSummaryBEO
                                {
                                    SL_NO = ++count,
                                    REGION_NAME = row["REGION_NAME"].ToString(),
                                    NO_OF_OPD_PRES = row["NO_OF_OPD_PRES"].ToString(),
                                    NO_OF_OTHER_PRES = row["NO_OF_OTHER_PRES"].ToString(),
                                    TOTAL_PRES = row["TOTAL_PRES"].ToString(),
                                    TOTAL_PRODUCT = row["TOTAL_PRODUCT"].ToString(),
                                    TOTAL_XELPRO = row["TOTAL_XELPRO"].ToString(),
                                    TOTAL_BIONIC = row["TOTAL_BIONIC"].ToString(),
                                    //TOTAL_XELPRO_MUPS = row["TOTAL_XELPRO_MUPS"].ToString(),
                                    TOTAL_CARDOTEL = row["TOTAL_CARDOTEL"].ToString(),
                                    TOTAL_FUXTIL = row["TOTAL_FUXTIL"].ToString(),
                                    TOTAL_EZYLIFE = row["TOTAL_EZYLIFE"].ToString(),
                                   // TOTAL_EZYLIFE_KID = row["TOTAL_EZYLIFE_KID"].ToString(),
                                    TOTAL_SWEET_DROPS = row["TOTAL_SWEET_DROPS"].ToString(),
                                    TOTAL_MONKAST = row["TOTAL_MONKAST"].ToString(),
                                    TOTAL_STROMEC = row["TOTAL_STROMEC"].ToString(),
                                    TOTAL_DEEP_HEAT_NIGHT_RELIEF = row["TOTAL_DEEP_HEAT_NIGHT_RELIEF"].ToString(),
                                    TOTAL_RY_JELLY = row["TOTAL_RY_JELLY"].ToString(),
                                    TOTAL_AZEXIA = row["TOTAL_AZEXIA"].ToString(),
                                    TOTAL_CLONZY = row["TOTAL_CLONZY"].ToString(),
                                    TOTAL_EMIREST = row["TOTAL_EMIREST"].ToString(),
                                    TOTAL_NAPRONIL_PLUS = row["TOTAL_NAPRONIL_PLUS"].ToString(),
                                    TOTAL_VELOFIX = row["TOTAL_VELOFIX"].ToString(),
                                    TOTAL_RETROVIR = row["TOTAL_RETROVIR"].ToString(),
                                    TOTAL_GAVICOOL = row["TOTAL_GAVICOOL"].ToString(),
                                    TOTAL_VCENT = row["TOTAL_VCENT"].ToString(),
                                    TOTAL_AIRUP = row["TOTAL_AIRUP"].ToString(),
                                    TOTAL_RINOBIL = row["TOTAL_RINOBIL"].ToString(),
                                    TOTAL_XTRACALD = row["TOTAL_XTRACALD"].ToString(),
                                    TOTAL_TELMIFAST = row["TOTAL_TELMIFAST"].ToString(),
                                    TOTAL_OTHERS = row["TOTAL_OTHERS"].ToString()
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
        public object GetAreaWisePrescriptionSummaryData(string areaCode, string fromDate, string toDate)
        {
            try
            {
                using (OracleConnection objConn = new OracleConnection(_dbConn.SAConnStrReader("Sales")))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_AREA_WISE_PRES_SUM";//"get_count_emp_by_dept";
                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.Parameters.Add("vSTART_DATE", OracleType.VarChar).Value = fromDate;
                        objCmd.Parameters.Add("vEND_DATE", OracleType.VarChar).Value = toDate;
                        objCmd.Parameters.Add("P_AREA_CODE", OracleType.VarChar).Value = areaCode.Trim();
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
                        List<ReportMPOWisePrescriptionSummaryBEO> item;
                        item = (from DataRow row in dt.Rows
                                select new ReportMPOWisePrescriptionSummaryBEO
                                {
                                    SL_NO = ++count,
                                    AREA_NAME = row["AREA_NAME"].ToString(),
                                    NO_OF_OPD_PRES = row["NO_OF_OPD_PRES"].ToString(),
                                    NO_OF_OTHER_PRES = row["NO_OF_OTHER_PRES"].ToString(),
                                    TOTAL_PRES = row["TOTAL_PRES"].ToString(),
                                    TOTAL_PRODUCT = row["TOTAL_PRODUCT"].ToString(),
                                    TOTAL_XELPRO = row["TOTAL_XELPRO"].ToString(),
                                    TOTAL_BIONIC = row["TOTAL_BIONIC"].ToString(),
                                    //TOTAL_XELPRO_MUPS = row["TOTAL_XELPRO_MUPS"].ToString(),
                                    TOTAL_CARDOTEL = row["TOTAL_CARDOTEL"].ToString(),
                                    TOTAL_FUXTIL = row["TOTAL_FUXTIL"].ToString(),
                                    TOTAL_EZYLIFE = row["TOTAL_EZYLIFE"].ToString(),
                                    //TOTAL_EZYLIFE_KID = row["TOTAL_EZYLIFE_KID"].ToString(),
                                    TOTAL_SWEET_DROPS = row["TOTAL_SWEET_DROPS"].ToString(),
                                    TOTAL_MONKAST = row["TOTAL_MONKAST"].ToString(),
                                    TOTAL_STROMEC = row["TOTAL_STROMEC"].ToString(),
                                    TOTAL_DEEP_HEAT_NIGHT_RELIEF = row["TOTAL_DEEP_HEAT_NIGHT_RELIEF"].ToString(),
                                    TOTAL_RY_JELLY = row["TOTAL_RY_JELLY"].ToString(),
                                    TOTAL_AZEXIA = row["TOTAL_AZEXIA"].ToString(),
                                    TOTAL_CLONZY = row["TOTAL_CLONZY"].ToString(),
                                    TOTAL_EMIREST = row["TOTAL_EMIREST"].ToString(),
                                    TOTAL_NAPRONIL_PLUS = row["TOTAL_NAPRONIL_PLUS"].ToString(),
                                    TOTAL_VELOFIX = row["TOTAL_VELOFIX"].ToString(),
                                    TOTAL_RETROVIR = row["TOTAL_RETROVIR"].ToString(),
                                    TOTAL_GAVICOOL = row["TOTAL_GAVICOOL"].ToString(),
                                    TOTAL_VCENT = row["TOTAL_VCENT"].ToString(),
                                    TOTAL_AIRUP = row["TOTAL_AIRUP"].ToString(),
                                    TOTAL_RINOBIL = row["TOTAL_RINOBIL"].ToString(),
                                    TOTAL_XTRACALD = row["TOTAL_XTRACALD"].ToString(),
                                    TOTAL_TELMIFAST = row["TOTAL_TELMIFAST"].ToString(),
                                    TOTAL_OTHERS = row["TOTAL_OTHERS"].ToString()
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