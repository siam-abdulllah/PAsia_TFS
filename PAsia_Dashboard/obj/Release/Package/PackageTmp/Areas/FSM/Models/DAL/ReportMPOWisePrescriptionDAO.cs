using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using PAsia_Dashboard.Areas.FSM.Models.BEL.BEO;
using PAsia_Dashboard.Universal.Gateway;

namespace PAsia_Dashboard.Areas.FSM.Models.DAL.DAO
{
    public class ReportMPOWisePrescriptionDAO
    {
        readonly DBConnection _dbConn = new DBConnection();
        readonly DBHelper _dbHelper = new DBHelper();
        private IDGenerated _idGenerated = new IDGenerated();
        readonly DBConnection _dbConnection = new DBConnection();
        private DataRow _row;
        private DataTable _dt;

        public List<ReportMPOWisePrescriptionInfoBEO> GetMPOWisePrescriptionData(string depotCode, string zoneCode, string regionCode, string areaCode, string territoryCode, string fromDate, string toDate, string doctorCode, string ProdType)
        {
            try
            {
                var queryParam = " ";
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
                    queryParam += "WHERE DEPOT_CODE='" + depotCode + "' ";
                }
                if (!string.IsNullOrEmpty(zoneCode))
                {
                    queryParam += "AND ZONE_CODE='" + zoneCode + "' ";
                }
                if (!string.IsNullOrEmpty(regionCode))
                {
                    queryParam += " AND REGION_CODE='" + regionCode + "' ";
                }
                if (!string.IsNullOrEmpty(areaCode))
                {
                    queryParam += " AND AREA_CODE='" + areaCode + "' ";
                }
                if (!string.IsNullOrEmpty(territoryCode))
                {
                    queryParam += " AND TERRITORY_CODE='" + territoryCode + "' ";
                }
                if (!string.IsNullOrEmpty(doctorCode))
                {
                    queryParam += " AND VFP.DOCTOR_CODE='" + doctorCode + "' ";
                } if (!string.IsNullOrEmpty(ProdType))
                {
                    queryParam += " AND P.TYPE='" + ProdType + "' ";
                }
                if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
                {
                    queryParam += " AND TRUNC(CAPTURE_TIME) BETWEEN TO_DATE('" + fromDate + "','DD-MM-YYYY') AND TO_DATE('" + toDate + "','DD-MM-YYYY') ";
                }


                string Qry = "SELECT DISTINCT VFP.MST_SL,VFP.DOCTOR_NAME,VFP.DOCTOR_CODE,ZONE_CODE,ZONE_NAME,REGION_CODE,REGION_NAME,AREA_CODE,AREA_NAME,TERRITORY_CODE,TERRITORY_NAME,TO_CHAR(VFP.CAPTURE_TIME,'dd-MM-yyyy HH:mm:ss') CAPTURE_TIME, VFP.PRESCRIPTION_URL, VFP.PRESCRIPTION_TYPE, VFP.USER_ID,EI.MIO_NAME EMPLOYEE_NAME, " +
                             " (SELECT COUNT(PRODUCT_CODE) FROM  FSM_PRESCRIPTION_DTL FPD WHERE   FPD.MST_SL=VFP.MST_SL GROUP BY FPD.MST_SL) TOTAL_PROD" +
                             " FROM VW_FSM_PRESCRIPTION  VFP INNER JOIN VW_PAL_FIELD_FORCE_MIO EI ON VFP.USER_ID = EI.MIO_CODE " +
                             " INNER JOIN PRODUCT_DETAILS P ON VFP.PRODUCT_CODE=P.S_PRODUCT_CODE " + queryParam + " ORDER BY  CAPTURE_TIME DESC,EI.MIO_NAME";
                //string Qry = "SELECT  DOCTOR_CODE, DOCTOR_NAME, PRACTICING_DAY, PRESCRIPTION_PER_DAY,HONORARIUM_AMOUNT, TERRITORY_CODE_4P, TO_CHAR(SET_DATE,'DD-MM-YYYY') SET_DATE FROM VW_FSM_DOC_HONORARIUM " + queryParam+"";
                DataTable dt = _dbHelper.GetDataTable(_dbConn.SAConnStrReader("Sales"), Qry);
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

        public bool DeletePrescription(string mstSl, string filePath)
        {
            var url = "http://202.84.32.118:8999/Image/20190512-114444-1579.jpg";
            bool isTrue = true;
            //HttpWebResponse response = null;
            //var request = (HttpWebRequest)WebRequest.Create(url);
            //request.Method = "HEAD";
            //bool isTrue = false;

            //try
            //{
            //    response = (HttpWebResponse) request.GetResponse();
            //    isTrue = File.Exists(@"\\202.84.32.118:8999\Image\20190512-114444-1579.jpg");
            //}
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    isTrue = true;
                }
                //HttpWebRequest request = (HttpWebRequest) WebRequest.Create(filePath);
                //using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                //{
                //    exist = response.StatusCode == HttpStatusCode.OK;
                //    if (exist)
                //    {
                //        //File.Delete(@"http:\\\\202.84.32.118:8999\\Image\\20190512-114444-1579.jpg");
                //        var uri = new Uri(url, UriKind.Absolute);
                //        //File.Delete(uri.AbsolutePath);
                //        //File.Delete(uri.AbsoluteUri);
                //        // FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(url);
                //        //WebRequest webRequest = WebRequest.Create(url);
                //        //webRequest.Method = WebRequestMethods.Ftp.DeleteFile; // "DELETE";
                //        //HttpWebResponse httpReponse = (HttpWebResponse)webRequest.GetResponse();
                //        //HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create("http://202.84.32.118:8999/Image/20190512-114444-1579.jpg");
                //        //request.Method = "DELETE";
                //        //request.ContentType = "application/x-www-form-urlencoded";
                //        //HttpWebResponse httpResponse = (HttpWebResponse)request.GetResponse();

                //        //if (httpResponse.StatusCode == HttpStatusCode.OK)
                //        //{
                //        //    // some code
                //        //}
                //    }
                //}


                //WebRequest webRequest = WebRequest.Create(filePath);
                //webRequest.Method = "DELETE";

                //HttpWebResponse httpResponse = (HttpWebResponse)webRequest.GetResponse();
                //var code = httpResponse.StatusCode;


                string mstQry = "delete from FSM_PRESCRIPTION_DTL where MST_SL='" + mstSl + "'";
                if (!_dbHelper.CmdExecute(_dbConn.SAConnStrReader("Dashboard"), mstQry))
                {
                    isTrue = false;
                }
                if (isTrue)
                {
                    string dtlQry = "delete from FSM_PRESCRIPTION_MST where MST_SL='" + mstSl + "'";
                    if (_dbHelper.CmdExecute(_dbConn.SAConnStrReader("Dashboard"), dtlQry))
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return isTrue;
        }
    }
}