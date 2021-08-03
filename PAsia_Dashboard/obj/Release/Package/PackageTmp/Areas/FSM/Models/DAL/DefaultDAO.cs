﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PAsia_Dashboard.Areas.FSM.Models.BEL.BEO;
using PAsia_Dashboard.Universal.Gateway;

namespace PAsia_Dashboard.Areas.FSM.Models.DAL.DAO
{
    public class DefaultDAO : ReturnData
    {
        readonly DBConnection _dbConn = new DBConnection();
        readonly DBHelper _dbHelper = new DBHelper();
        private IDGenerated _idGenerated = new IDGenerated();
        public string queryParam = " ";
        public List<MonthInfoBEO> GetMonthName()
        {
            string Qry = "SELECT MONTH_CODE, MONTH_NAME FROM MONTH_INFORMATION ";
            DataTable dt = _dbHelper.GetDataTable(_dbConn.SAConnStrReader("Sales"), Qry);
            List<MonthInfoBEO> item;
            item = (from DataRow row in dt.Rows
                    select new MonthInfoBEO
                    {
                        MONTH_CODE = row["MONTH_CODE"].ToString(),
                        MONTH_NAME = row["MONTH_NAME"].ToString()
                    }).ToList();
            return item;
        }
        public List<AccessInfoBEO> GetAccessLevel()
        {
            try
            {
                string employeeCode = HttpContext.Current.Session["CODE"].ToString();
                string accessLevel = HttpContext.Current.Session["ACCESS_LEVEL"].ToString();

                List<AccessInfoBEO> accessInfoBeo = null;
                    if (accessLevel == "D")
                    {
                        string qry =
                            "SELECT DISTINCT DEPOT_CODE,DEPOT_NAME FROM VW_PAL_FIELD_FORCE_MIO WHERE EMPLOYEE_CODE=" + employeeCode + " ";
                        //
                        DataTable dt = _dbHelper.GetDataTable(_dbConn.SAConnStrReader("Dashboard"), qry);
                        accessInfoBeo = (from DataRow row in dt.Rows
                                         select new AccessInfoBEO
                                         {
                                             ACCESS_LEVEL = accessLevel,
                                             EMPLOYEE_CODE = employeeCode,
                                             DEPOT_CODE = row["DEPOT_CODE"].ToString(),
                                             DEPOT_NAME = row["DEPOT_NAME"].ToString()
                                         }).ToList();

                    }
                    if (accessLevel == "Z")
                    {
                        string qry =
                            "SELECT DISTINCT ZONE_CODE,ZONE_NAME FROM VW_PAL_FIELD_FORCE_MIO WHERE DSM_CODE=" + employeeCode + " ";
                        //
                        DataTable dt = _dbHelper.GetDataTable(_dbConn.SAConnStrReader("Dashboard"), qry);
                        accessInfoBeo = (from DataRow row in dt.Rows
                                         select new AccessInfoBEO
                                         {
                                             ACCESS_LEVEL = accessLevel,
                                             EMPLOYEE_CODE = employeeCode,
                                             ZONE_CODE = row["ZONE_CODE"].ToString(),
                                             ZONE_NAME = row["ZONE_NAME"].ToString()
                                         }).ToList();


                    }
                    if (accessLevel == "R")
                    {
                        string qry =
                            "SELECT DISTINCT REGION_CODE,REGION_NAME FROM VW_PAL_FIELD_FORCE_MIO WHERE RSM_CODE=" + employeeCode + " ";
                        //
                        DataTable dt = _dbHelper.GetDataTable(_dbConn.SAConnStrReader("Dashboard"), qry);
                        accessInfoBeo = (from DataRow row in dt.Rows
                                         select new AccessInfoBEO
                                         {
                                             ACCESS_LEVEL = accessLevel,
                                             EMPLOYEE_CODE = employeeCode,
                                             REGION_CODE = row["REGION_CODE"].ToString(),
                                             REGION_NAME = row["REGION_NAME"].ToString()
                                         }).ToList();
                    }
                    if (accessLevel == "A")
                    {
                        string qry =
                            "SELECT DISTINCT AREA_NAME,AREA_CODE FROM VW_PAL_FIELD_FORCE_MIO WHERE AM_CODE=" + employeeCode + " ";
                        DataTable dt = _dbHelper.GetDataTable(_dbConn.SAConnStrReader("Dashboard"), qry);
                        accessInfoBeo = (from DataRow row in dt.Rows
                                         select new AccessInfoBEO
                                         {
                                             ACCESS_LEVEL = accessLevel,
                                             EMPLOYEE_CODE = employeeCode,
                                             AREA_CODE = row["AREA_CODE"].ToString(),
                                             AREA_NAME = row["AREA_NAME"].ToString()
                                         }).ToList();
                    }
                return accessInfoBeo;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public List<DepotInfoBEO> GetDepot()
        {
            string Qry = "SELECT UNIT_CODE,UNIT_NAME FROM SC_COMP_UNIT WHERE UNIT_CODE NOT IN ('00','01','02','19') ORDER BY UNIT_NAME";
            DataTable dt = _dbHelper.GetDataTable(_dbConn.SAConnStrReader("Sales"), Qry);
            List<DepotInfoBEO> item;
            item = (from DataRow row in dt.Rows
                    select new DepotInfoBEO
                    {
                        DEPOT_CODE = row["UNIT_CODE"].ToString(),
                        DEPOT_NAME = row["UNIT_NAME"].ToString()
                    }).ToList();
            return item;
        }
        public List<ZoneInfoBEO> GetZoneByDepot(string depotCode)
        {
            if (!string.IsNullOrEmpty(depotCode))
            {
                queryParam = "WHERE DEPOT_CODE='" + depotCode + "' ";
            }
            string Qry = "SELECT DISTINCT ZONE_CODE, ZONE_NAME FROM VW_PAL_FIELD_FORCE_MIO " + queryParam + " ORDER BY ZONE_NAME ";
            DataTable dt = _dbHelper.GetDataTable(_dbConn.SAConnStrReader("Sales"), Qry);
            List<ZoneInfoBEO> item;
            item = (from DataRow row in dt.Rows
                    select new ZoneInfoBEO
                    {
                        ZONE_CODE = row["ZONE_CODE"].ToString(),
                        ZONE_NAME = row["ZONE_NAME"].ToString()

                    }).ToList();
            return item;
        }

        public List<RegionInfoBEO> GetRegionByZone(string depotCode, string zoneCode)
        {
            if (!string.IsNullOrEmpty(depotCode))
            {
                queryParam = "AND DEPOT_CODE='" + depotCode + "' ";
            }
            if (!string.IsNullOrEmpty(zoneCode))
            {
                queryParam = queryParam + "AND ZONE_CODE='" + zoneCode + "' ";
            }
            string Qry = "SELECT DISTINCT REGION_CODE, REGION_NAME FROM VW_PAL_FIELD_FORCE_MIO WHERE 1=1 " + queryParam + " ORDER BY REGION_NAME ";
            DataTable dt = _dbHelper.GetDataTable(_dbConn.SAConnStrReader("Sales"), Qry);
            List<RegionInfoBEO> item;

            item = (from DataRow row in dt.Rows
                    select new RegionInfoBEO
                    {
                        REGION_CODE = row["REGION_CODE"].ToString(),
                        REGION_NAME = row["REGION_NAME"].ToString()
                    }).ToList();
            return item;

        }

        public List<AreaInfoBEO> GetAreaByRegion(string depotCode, string zoneCode, string regionCode)
        {
            if (!string.IsNullOrEmpty(depotCode))
            {
                queryParam = "AND DEPOT_CODE='" + depotCode + "' ";
            }
            if (!string.IsNullOrEmpty(zoneCode))
            {
                queryParam = queryParam + "AND ZONE_CODE='" + zoneCode + "' ";
            }
            if (!string.IsNullOrEmpty(regionCode))
            {
                queryParam = queryParam + "AND REGION_CODE='" + regionCode + "' ";
            }

            string Qry = "SELECT DISTINCT AREA_CODE, AREA_NAME FROM VW_PAL_FIELD_FORCE_MIO WHERE 1=1 " + queryParam + " ORDER BY AREA_NAME ";
            DataTable dt = _dbHelper.GetDataTable(_dbConn.SAConnStrReader("Sales"), Qry);
            List<AreaInfoBEO> item;

            item = (from DataRow row in dt.Rows
                    select new AreaInfoBEO
                    {
                        AREA_CODE = row["AREA_CODE"].ToString(),
                        AREA_NAME = row["AREA_NAME"].ToString(),

                    }).ToList();
            return item;
        }

        public List<TerritoryInfoBEO> GetTerritoryByArea(string depotCode, string zoneCode, string regionCode, string areaCode)
        {
            if (!string.IsNullOrEmpty(depotCode))
            {
                queryParam = "AND DEPOT_CODE='" + depotCode + "' ";
            }
            if (!string.IsNullOrEmpty(zoneCode))
            {
                queryParam = queryParam + "AND ZONE_CODE='" + zoneCode + "' ";
            }
            if (!string.IsNullOrEmpty(regionCode))
            {
                queryParam = queryParam + "AND REGION_CODE='" + regionCode + "' ";
            }

            if (!string.IsNullOrEmpty(areaCode))
            {
                queryParam = queryParam + "AND AREA_CODE='" + areaCode + "' ";
            }

            string Qry = "SELECT DISTINCT TERRITORY_CODE, TERRITORY_NAME FROM VW_PAL_FIELD_FORCE_MIO WHERE 1=1 " + queryParam + " ";
            DataTable dt = _dbHelper.GetDataTable(_dbConn.SAConnStrReader("Sales"), Qry);
            List<TerritoryInfoBEO> item;

            item = (from DataRow row in dt.Rows
                    select new TerritoryInfoBEO
                    {
                        TERRITORY_CODE = row["TERRITORY_CODE"].ToString(),
                        TERRITORY_NAME = row["TERRITORY_NAME"].ToString(),

                    }).ToList();
            return item;
        }
        public List<RegionInfoBEO> GetRegionByDSM( string dsmCode)
        {
            
            if (!string.IsNullOrEmpty(dsmCode))
            {
                queryParam = queryParam + "AND DSM_CODE='" + dsmCode + "' ";
            }
            string Qry = "SELECT DISTINCT REGION_CODE, REGION_NAME FROM VW_PAL_FIELD_FORCE_MIO WHERE 1=1 " + queryParam + " ORDER BY REGION_NAME ";
            DataTable dt = _dbHelper.GetDataTable(_dbConn.SAConnStrReader("Sales"), Qry);
            List<RegionInfoBEO> item;

            item = (from DataRow row in dt.Rows
                    select new RegionInfoBEO
                    {
                        REGION_CODE = row["REGION_CODE"].ToString(),
                        REGION_NAME = row["REGION_NAME"].ToString()
                    }).ToList();
            return item;

        }

        public List<AreaInfoBEO> GetAreaByRSM( string rsmCode)
        {
           
            if (!string.IsNullOrEmpty(rsmCode))
            {
                queryParam = queryParam + "AND RSM_CODE='" + rsmCode + "' ";
            }

            string Qry = "SELECT DISTINCT AREA_CODE, AREA_NAME FROM VW_PAL_FIELD_FORCE_MIO WHERE 1=1 " + queryParam + " ORDER BY AREA_NAME ";
            DataTable dt = _dbHelper.GetDataTable(_dbConn.SAConnStrReader("Sales"), Qry);
            List<AreaInfoBEO> item;

            item = (from DataRow row in dt.Rows
                    select new AreaInfoBEO
                    {
                        AREA_CODE = row["AREA_CODE"].ToString(),
                        AREA_NAME = row["AREA_NAME"].ToString(),

                    }).ToList();
            return item;
        }

        public List<TerritoryInfoBEO> GetTerritoryByAM(string amCode)
        {
            
            if (!string.IsNullOrEmpty(amCode))
            {
                queryParam = queryParam + "AND AM_CODE='" + amCode + "' ";
            }

            string Qry = "SELECT DISTINCT TERRITORY_CODE, TERRITORY_NAME FROM VW_PAL_FIELD_FORCE_MIO WHERE 1=1 " + queryParam + " ";
            DataTable dt = _dbHelper.GetDataTable(_dbConn.SAConnStrReader("Sales"), Qry);
            List<TerritoryInfoBEO> item;

            item = (from DataRow row in dt.Rows
                    select new TerritoryInfoBEO
                    {
                        TERRITORY_CODE = row["TERRITORY_CODE"].ToString(),
                        TERRITORY_NAME = row["TERRITORY_NAME"].ToString(),

                    }).ToList();
            return item;
        }




        public object LoadExcelFile(string fileName, string physicalPath)
        {
            Object data = null;
            DataSet dataSet = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                string connectionString = "";
                string[] d = fileName.Split('.');
                string fileExtension = "." + d[d.Length - 1].ToString();
                if (d.Length > 0)
                {
                    if (fileExtension == ".xls")
                    {
                        connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + physicalPath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
                    }
                    //connection String for xlsx file format.
                    else if (fileExtension == ".xlsx")
                    {
                        connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + physicalPath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\"";
                    }
                    else if (fileExtension == ".csv")
                    {
                        connectionString = string.Format(
                            @"Provider=Microsoft.Jet.OleDb.4.0; Data Source={0};Extended Properties=""Text;HDR=YES;FMT=Delimited""",
                            Path.GetDirectoryName(physicalPath)
                        );
                    }
                }


                //Create Connection to Excel work book and add oledb namespace
                OleDbConnection excelConnection = new OleDbConnection(connectionString);
                excelConnection.Open();


                dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (dt == null)
                {
                    return null;
                }

                String[] excelSheets = new String[dt.Rows.Count];
                int t = 0;
                //excel data saves in temp file here.
                foreach (DataRow row in dt.Rows)
                {
                    excelSheets[t] = row["TABLE_NAME"].ToString();
                    t++;
                }
                OleDbConnection excelConnection1 = new OleDbConnection(connectionString);
                //DataSet dataSet = new DataSet();

                string query = string.Format("Select * from [{0}]", excelSheets[0]);
                using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                {
                    dataAdapter.Fill(dataSet);
                }

                dt = dataSet.Tables[0];

                excelConnection.Close();

                //return dt;

                List<DoctorDataUploadInfo> item = new List<DoctorDataUploadInfo>();

                foreach (DataRow row in dt.Rows)
                {
                    DoctorDataUploadInfo dData = new DoctorDataUploadInfo();
                    dData.DOCTOR_CODE = row["DoctorCode"].ToString();
                    dData.DOCTOR_NAME = row["DoctorName"].ToString();
                    // DOCTOR_CODE_4P = row["Doctor Code 4P"].ToString(),
                    dData.CLASS_GROUP = row["Class"].ToString();
                    dData.ADDRESS = row["Address"].ToString();
                    dData.DEGREES = row["Degrees"].ToString();
                    dData.DESIGNATION = row["Designation"].ToString();
                    dData.CONTRACT_NO = row["ContractNo"].ToString();
                    dData.EMAIL = row["Email"].ToString();
                    dData.TERRITORY_CODE_4P = row["TerritoryCode4P"].ToString();
                    dData.SPECIALTY = row["Specialty"].ToString();
                    //if (!string.IsNullOrEmpty(dData.DOCTOR_CODE) && !string.IsNullOrEmpty(dData.DOCTOR_NAME) && !string.IsNullOrEmpty(dData.TERRITORY_CODE_4P))
                    //{
                    item.Add(dData);
                    //}
                }



                //SaveUpdate(item);
                return item;


            }
            catch (Exception ex)
            {
                ExceptionReturn = ex.Message;
                return ExceptionReturn;
            }

        }


        public List<ProdTypeInfoBEO> GetProdType()
        {
            string Qry = "SELECT  Distinct TYPE,TYPE_NAME FROM FSM_PROD_DETAIL ORDER BY TYPE_NAME ";
            DataTable dt = _dbHelper.GetDataTable(_dbConn.SAConnStrReader("Dashboard"), Qry);

            var item = (from DataRow row in dt.Rows
                        select new ProdTypeInfoBEO
                        {
                            //PRODUCT_CODE = row["PRODUCT_CODE"].ToString(),
                            //PRODUCT_NAME = row["PRODUCT_NAME"].ToString(),
                            TYPE = row["TYPE"].ToString(),
                            TYPE_NAME = row["TYPE_NAME"].ToString(),

                        }).ToList();
            return item;
        }
    }
}