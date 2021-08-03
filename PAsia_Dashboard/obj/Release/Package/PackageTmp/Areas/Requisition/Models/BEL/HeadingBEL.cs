using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PAsia_Dashboard.Areas.Requisition.Models.BEL
{
    public class HeadingBEL
    {
        public string PreparedBy { get; set; }
        public string CheckedBy { get; set; }
        public string VerifiedBy { get; set; }
        public string RecommendBy { get; set; }
        public string ApprovedBy { get; set; }
    }
}