using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PAsia_Dashboard.Areas.Dashboard.Models.BEL
{
    public class FactoryDashboard
    {
        public string POSTING_LOCATION { get; set; }
        public string NoOfBatch { get; set; }
        public string RawMatIssueVal { get; set; }
        public string RawMatRcvVal { get; set; }
        public string RawMatRelVal { get; set; }
        public string PackMatIssueVal { get; set; }
        public string PackMatRcvVal { get; set; }
        public string PackMatRelVal { get; set; }
        public string WipTotalVal { get; set; }
        public string ProdToWarBox { get; set; }
        public string WarToCWHBox { get; set; }
        public string NoOfQCTestToday { get; set; }
        public string NoOfPMTest { get; set; }
        public string NoOfRMTest { get; set; }
        public string NoOfMicrobiologyTest { get; set; }
        public string NoOfPMReqToday { get; set; }
        public string NoOfRMReqToday { get; set; }
        public string NoOfRMReqTotal { get; set; }
        public string NoOfPMReqTotal { get; set; }
        public string NoOfRMReqThirtyForty { get; set; }
        public string NoOfPMReqFiftyHundred { get; set; }
        public string SCMShortageSignal { get; set; }
        public string NoOfPMManPower{get; set;}
        public string NoOfLOPToday { get; set; }
        public string NoOfLOPUpTo { get; set; }
        public string NoOfLCUpTo { get; set; }
        public string NoOfCSToday { get; set; }
        public string NoOfCSTotal { get; set; }
        public string NoOfManpowerManu { get; set; }
        public string NoOfRMShortageCurr { get; set; }
        public string NoOfRMExcessCurr { get; set; }
        public string NoOfPMShortageCurr { get; set; }
        public string NoOfRejectItem { get; set; }
        public string NoOfCommStockCurr { get; set; }
        public string ValOfCommStockCurr { get; set; }
        public string NoOfSampleStockCurr { get; set; }
        public string ValOfSampleStockCurr { get; set; }
        public string NoOfGiftStockCurr { get; set; }
        public string ValOfGiftStockCurr { get; set; }
        public string NoOfCommSalesUpTO { get; set; }
        public string NoOfSampleStockInvUpTO { get; set; }
        public string NoOfGiftStockInvUpTO { get; set; }
        public string NoOfRMOtherRcvReqToday { get; set; }
        public string NoOfRMOtherRcvReqTotal { get; set; }
        public string NoOfPMOtherRcvReqToday { get; set; }
        public string NoOfPMOtherRcvReqTotal { get; set; }
        public string NoOfPPMOtherRcvReqToday { get; set; }
        public string NoOfPPMOtherRcvReqTotal { get; set; }
        public string NoOfGIFTOtherRcvReqToday { get; set; }
        public string NoOfGIFTOtherRcvReqTotal { get; set; }
    }
}