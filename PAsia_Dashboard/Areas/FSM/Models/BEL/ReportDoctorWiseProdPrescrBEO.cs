using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PAsia_Dashboard.Areas.FSM.Models.BEL
{
    public class ReportDoctorWiseProdPrescrBEO
    {
        public int SL_NO { get; set; }
        public string DOCTOR_CODE { get; set; }
        public string DOCTOR_NAME { get; set; }
        public string DESIGNATION { get; set; }
        public string DEGREES { get; set; }
        public string DEPOT_CODE { get; set; }
        public string DEPOT_NAME { get; set; }
        public string ZONE_CODE { get; set; }
        public string ZONE_NAME { get; set; }
        public string REGION_CODE { get; set; }
        public string REGION_NAME { get; set; }
        public string AREA_CODE { get; set; }
        public string AREA_NAME { get; set; }
        public string TERRITORY_CODE { get; set; }
        public string TERRITORY_NAME { get; set; }
        public string TOT_PRES { get; set; }
        public string CLASS_GROUP { get; set; }
    }
}