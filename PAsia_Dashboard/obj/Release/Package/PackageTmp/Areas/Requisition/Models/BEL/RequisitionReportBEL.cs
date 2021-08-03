using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PAsia_Dashboard.Areas.Requisition.Models.BEL
{
    public class RequisitionReportBEL
    {
        public string RequisitionNo { get; set; }
        public string PrepareFromDate { get; set; }
        public string PrepareToDate { get; set; }
        public string ApprovedFromDate { get; set; }
        public string ApprovedToDate { get; set; }
        public string PreparedBy { get; set; }
        public string PreparedByName { get; set; }
    }
}