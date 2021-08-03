using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PAsia_Dashboard.Areas.Requisition.Models.BEL
{
    public class ExpRequisitionPrepareBEL
    {

        public class ExpReqPrepareMst
        {
            public int MstId { get; set; }
            public string RequisitionNo { get; set; }
            public string RequisitionDate { get; set; }
            public string RequisitionType { get; set; }
            public string ReqTypeName { get; set; }
            public string ExpenditureMonth { get; set; }
            public string RequisitionBy { get; set; }
            public string RequisitionByName { get; set; }
            public string RequisitionByDesig { get; set; }
            public string PayToCode { get; set; }
            public string PayToName { get; set; }
            public string PayToDesig { get; set; }
            public string PaymentPlace { get; set; }
            public string PrepareBy { get; set; }
            public string PrepareName { get; set; }
            public string PrepareDesig { get; set; }
            public string PrepareDate { get; set; }
            public string PrepareRemarks { get; set; }

            public string CheckedBy { get; set; }
            public string CheckedStatus { get; set; }
            public string CheckedName { get; set; }
            public string CheckedDesig { get; set; }
            public string CheckedDate { get; set; }
            public string CheckedRemarks { get; set; }


            public string VerifiedBy { get; set; }
            public string VerifiedStatus { get; set; }
            public string VerifiedName { get; set; }
            public string VerifiedDesig { get; set; }
            public string VerifiedDate { get; set; }
            public string VerifiedRemarks { get; set; }


            public string RecommendedBy { get; set; }
            public string RecommendedStatus { get; set; }
            public string RecommendedName { get; set; }
            public string RecommendedDesig { get; set; }
            public string RecommendedDate { get; set; }
            public string RecommendedRemarks { get; set; }

            public string ApprovedBy { get; set; }
            public string ApprovedStatus { get; set; }
            public string ApprovedName { get; set; }
            public string ApprovedDesig { get; set; }
            public string ApprovedDate { get; set; }
            public string ApprovedRemarks { get; set; }
            public string PrintFlag { get; set; }
            public int TotalApprovedAmt { get; set; }
            public string PreparedByConfirm { get;  set; }
            public string AdjustmentByName { get;  set; }
        }

        public class ExpReqPrepareDtl
        {

            public int DtlId { get; set; }
            public int MstId { get; set; }
            public string Mop { get; set; }
            public int PrepareValue { get; set; }
            public int CheckedValue { get; set; }
            public int VerifiedValue { get; set; }
            public int RecommendedValue { get; set; }
            public int ApprovedValue { get; set; }
            public string RequiredDate { get; set; }
            public string Purpose { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public string Remarks { get; set; }
            public string TotalDays { get; set; }



        }




    }
}