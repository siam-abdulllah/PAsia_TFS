using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PAsia_Dashboard.Universal.Gateway
{
    public class DBConnection
    {
        string connectionString = "";
        public DBConnection()
        {
            //SAConnStrReader("Dashboard");
        }

        public string SAConnStrReader(string ConnType)
        {
            if (ConnType == "Dashboard")
            {
                connectionString = ConfigurationManager.ConnectionStrings["ConnDash"].ToString();
            }
            else if (ConnType == "Sales")
            {
                connectionString = ConfigurationManager.ConnectionStrings["ConnSales"].ToString();
            }


            return connectionString;
        }


    }
}
