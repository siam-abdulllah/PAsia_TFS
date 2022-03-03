using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PAsia_Dashboard.Areas.Reports.Models.BEl
{
    public class TargetSales
    {
        public int SL_NO { get; set; }
        public string PRODUCT_CODE { get; set; }
        public string PRODUCT_NAME { get; set; }
        public string PACK_SIZE { get; set; }
        public string UNIT_TARGET { get; set; }
        public string VALUE_TARGET { get; set; }
        public string UNIT_SALES { get; set; }
        public string VALUE_SALES { get; set; }
        public string CURRENT_STOCK { get; set; }
        public string BRAND_NAME { get; set; }
        public String FROM_DATE { get; set; }
        public String TO_DATE { get; set; }

    }
}