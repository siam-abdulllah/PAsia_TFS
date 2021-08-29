app.controller("myCtrl", function ($scope, $http, uiGridConstants) {
    $scope.EventPerm(53);
    $scope.isDisabled = false;

    //$scope.GetHeadMenuList();
    //
    var columnStockProdSalesList = [
        //{ name: "STOCK_DATE", displayName: "Stock Date", cellFilter: "FullDateTime", width: '13%'},
        { name: "SL_NO", displayName: "Sl. No",width: 50, },
        { name: "PRODUCT_CODE", displayName: "Product Code",width: 90, },
        { name: "PRODUCT_NAME", displayName: "Product Name", width: 100,},
        { name: "PACK_SIZE", displayName: "Pack Size", width: 90,},
        { name: "TP_VAT", displayName: "TP VAT", cellFilter: 'number:2' , aggregationType: uiGridConstants.aggregationTypes.sum, width: 130, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: "OPENING_QTY", displayName: "Opening QTY", cellFilter: 'number:2' , aggregationType: uiGridConstants.aggregationTypes.sum, width: 110, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: "THREE_NET_SALES_QTY", displayName: "Three Net Sales QTY", cellFilter: 'number:2' , aggregationType: uiGridConstants.aggregationTypes.sum, width: 110, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: "TWO_NET_SALES_QTY", displayName: "Two Net Sales QTY", cellFilter: 'number:2' , aggregationType: uiGridConstants.aggregationTypes.sum, width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: "ONE_NET_SALES_QTY", displayName: "One Net Sales QTY", cellFilter: 'number:2' , aggregationType: uiGridConstants.aggregationTypes.sum, width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        
       
        { name: "THREE_MONTH_AVG_SALES", displayName: "Three Month AVG Sales", cellFilter: 'number:2' , aggregationType: uiGridConstants.aggregationTypes.sum, width: 130, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: "UPTO_NET_SALES", displayName: "UPTO NET Sales", cellFilter: 'number:2' , aggregationType: uiGridConstants.aggregationTypes.sum, width: 130, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: "CURRENT_STOCK", displayName: " Current Stock", cellFilter: 'number:2' , aggregationType: uiGridConstants.aggregationTypes.sum, width: 130, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        //{ name: "CM_MPO", displayName: "CM MPO", aggregationType: uiGridConstants.aggregationTypes.sum, width: 80, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        //{ name: "LM_MPO", displayName: "LM MPO", aggregationType: uiGridConstants.aggregationTypes.sum, width: 80, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: "SALES_STOCK", displayName: "Sales Stock", cellFilter: 'number:2' , aggregationType: uiGridConstants.aggregationTypes.sum, width: 100, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: "DEFICIT", displayName: "Deficit", cellFilter: 'number:2' , aggregationType: uiGridConstants.aggregationTypes.sum, width: 100, cellClass: 'grid-align', footerCellFilter: 'number:2' }
        //{ name: "FRESH_STOCK_TP_VAL", displayName: "TP Value", aggregationType: uiGridConstants.aggregationTypes.sum, width: 130 },
        //{ name: "FRESH_STOCK_VAT_VAL", displayName: "VAT Value", aggregationType: uiGridConstants.aggregationTypes.sum, width: 130 },
        //{ name: "FRESH_STOCK_TP_VAT_VAL", displayName: "TP+ VAT Value", aggregationType: uiGridConstants.aggregationTypes.sum, width: 130 }

    ];

    $scope.gridStockProdSalesOptionsValue = {
        //showGridFooter: true,
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnStockProdSalesList,
        //rowTemplate: rowTemplate(),
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: 'MPO_Wise_Sales_Achievement_Value.csv',
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApiValue) {
            $scope.gridApi = gridApiValue;
        }

    };
    function customAchAggregate(upToSalesValue, target) {
        var result = (upToSalesValue * 100) / target;
        if (!isFinite(result)) {
            result = "total: 0.00";
        } else {
            result = "total: " + result.toFixed(2);
        }
        return result;

    }
    $scope.GetStockProdSalesSalesAch = function () {
        $scope.isDisabled = true;
       // if (!DateCheck($scope.FromDate, $scope.ToDate)) {
            //$scope.isDisabled = false;
            //return false;
        //}
        $http({
            method: "POST",
            url: MyApp.rootPath + "StockProdSales/GetStockProdSalesValue",
            data: { fromDate: $scope.FromDate, toDate: $scope.ToDate }
        }).then(function (response) {
            debugger;
            if (response.data.length>0) {
                $scope.gridStockProdSalesOptionsValue.data = response.data;

            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridStockProdSalesOptionsValue.data = [];
            }
            $scope.isDisabled = false;
        }, function (response) {
            toastr.error("Error!");
            $scope.isDisabled = false;
        });
    };

$scope.GetStockProdSalesSalesAch();
    //
    $scope.Reset = function () {
        //$scope.FromDate = "";
        //$scope.ToDate = "";
        $scope.gridStockProdSalesOptionsValue.data = [];
        $scope.isDisabled = false;
    };
});
//Date picker
