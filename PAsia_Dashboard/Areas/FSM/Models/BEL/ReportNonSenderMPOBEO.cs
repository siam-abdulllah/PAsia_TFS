using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PAsia_Dashboard.Areas.FSM.Models.BEL.BEO
{
    public class ReportNonSenderMPOBEO
    {
        
        
       
        public int SL_NO { get; set; }
        public string MIO_CODE { get; set; }
        public string MIO_NAME { get; internal set; }
        public string ZONE_CODE { get; set; }
        public string ZONE_NAME { get; set; }
        public string REGION_CODE { get; set; }
        public string REGION_NAME { get; set; }
        public string AREA_CODE { get; set; }
        public string AREA_NAME { get; set; }
        public string TERRITORY_CODE { get; set; }
        public string TERRITORY_NAME { get; set; }
    }
    }
