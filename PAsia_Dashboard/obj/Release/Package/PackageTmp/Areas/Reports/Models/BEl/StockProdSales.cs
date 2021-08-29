using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PAsia_Dashboard.Areas.Reports.Models.BEl
{
    public class StockProdSalesProduct
    {
        public string SL_No { get; set; }
        public string AREA_CODE { get; set; }
        public string AREA_NAME { get; set; }
        public string ZONE_CODE { get; set; }
        public string ZONE_NAME { get; set; }
        public string PRODUCT_CODE { get; set; }
        public string PRODUCT_NAME { get; set; }
        public string PACK_SIZE { get; set; }
        public string UNIT_TP { get; set; }
        public string UNIT_VAT { get; set; }
        public double FRESH_STOCK_QTY { get; set; }
        public double DAMAGE_STOCK_QTY { get; set; }
        public double FRESH_STOCK_TP_VAL { get; set; }
        public double FRESH_STOCK_VAT_VAL { get; set; }
        public double FRESH_STOCK_TP_VAT_VAL { get; set; }
    }
    public class StockProdSalesBEO
    {
        public int SL_NO { get; internal set; }
        public string PRODUCT_CODE { get; internal set; }
        public string PRODUCT_NAME { get; internal set; }
        public string PACK_SIZE { get; internal set; }
        public string TP_VAT { get; internal set; }
        public string OPENING_QTY { get; internal set; }
        public string THREE_NET_SALES_QTY { get; internal set; }
        public string TWO_NET_SALES_QTY { get; internal set; }
        public string ONE_NET_SALES_QTY { get; internal set; }
        public string THREE_MONTH_AVG_SALES { get; internal set; }
        public string UPTO_NET_SALES { get; internal set; }
        public string CURRENT_STOCK { get; internal set; }
        public string SALES_STOCK { get; internal set; }
        public string DEFICIT { get; internal set; }
    }
}