﻿app.controller("myCtrl", function ($scope, $http, uiGridConstants) {
    //$scope.EventPerm(18);


    //$scope.GetHeadMenuList();
    //
    var columnZWiseSalesAchValueList = [
        //{ name: "STOCK_DATE", displayName: "Stock Date", cellFilter: "FullDateTime", width: '13%'},
        { name: "ZONE_CODE", displayName: "Zone Code", visible: false },
        { name: "DSM_CODE", displayName: "DSM CODE", visible: false },
        { name: "SL_No", displayName: "SL. ", width: 40 },
        { name: "ZONE_NAME", displayName: "Zone Name ", width: 120},
        { name: "DSM_NAME", displayName: "DSM Name ", visible: false},
        { name: "TARGET_AMT", displayName: "Target", aggregationType: uiGridConstants.aggregationTypes.sum, width: 130, cellClass: 'grid-align', footerCellFilter: 'number:2'},
        { name: "TO_DAY_SALES", displayName: "Today Sales", aggregationType: uiGridConstants.aggregationTypes.sum, width: 110, cellClass: 'grid-align', footerCellFilter: 'number:2'},
        { name: "UPTO_SALES", displayName: "Up To Sales", aggregationType: uiGridConstants.aggregationTypes.sum, width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2'},
        //{ name: "PACK_SIZE", displayName: "Total Sales", width: 130},
        { name: "ACH", displayName: "Achievement", aggregationType: uiGridConstants.aggregationTypes.sum, width: 100, cellClass: 'grid-align', footerCellFilter: 'number:2'},
        { name: "LM_UPTO_SALES", displayName: "LMUS", aggregationType: uiGridConstants.aggregationTypes.sum, width: 110, cellClass: 'grid-align', footerCellFilter: 'number:2'},
        { name: "GROWTH", displayName: " Growth", aggregationType: uiGridConstants.aggregationTypes.sum, width: 110, cellClass: 'grid-align', footerCellFilter: 'number:2'},
        { name: "CM_MPO", displayName: "CM MPO", aggregationType: uiGridConstants.aggregationTypes.sum, width: 80, cellClass: 'grid-align', footerCellFilter: 'number:2'},
        { name: "LM_MPO", displayName: "LM MPO", aggregationType: uiGridConstants.aggregationTypes.sum, width: 80, cellClass: 'grid-align', footerCellFilter: 'number:2'},
        { name: "CM_CUST", displayName: "CM Customer", aggregationType: uiGridConstants.aggregationTypes.sum, width: 90, cellClass: 'grid-align', footerCellFilter: 'number:2'},
        { name: "LM_CUST", displayName: "LM Customer", aggregationType: uiGridConstants.aggregationTypes.sum, width: 90, cellClass: 'grid-align', footerCellFilter: 'number:2'}
        //{ name: "FRESH_STOCK_TP_VAL", displayName: "TP Value", aggregationType: uiGridConstants.aggregationTypes.sum, width: 130 },
        //{ name: "FRESH_STOCK_VAT_VAL", displayName: "VAT Value", aggregationType: uiGridConstants.aggregationTypes.sum, width: 130 },
        //{ name: "FRESH_STOCK_TP_VAT_VAL", displayName: "TP+ VAT Value", aggregationType: uiGridConstants.aggregationTypes.sum, width: 130 }

    ];

    $scope.gridZWiseSalesAchOptionsValue = {
        //showGridFooter: true,
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnZWiseSalesAchValueList,
        //rowTemplate: rowTemplate(),
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: 'Zone_Wise_Sales_Achievement_Value.csv',
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApiValue) {
            $scope.gridApi = gridApiValue;
        }

    };
   
   
    $scope.GetDashboardData = function () {
        $http({
            method: "post",
            url: MyApp.rootPath + "ZoneWiseSalesAchievement/GetDashboardData",
            datatype: "json",
        }).then(function (response) {
            if (response.data.Status == 'Ok') {
                $scope.dashboardData = response.data.Data;
                $scope.dashboardData.Total_Stock_Valuation = ((parseFloat(response.data.Data.Commercial_Stock_Valuation) || 0) + (parseFloat(response.data.Data.Sample_Stock_Valuation) || 0) + (parseFloat(response.data.Data.PPM_Stock_Valuation) || 0) + (parseFloat(response.data.Data.Gift_Stock_Valuation) || 0)).toFixed(2);

            }
            else {
                toastr.warning("No Data Found!");
            }
        });
    };
    //$scope.GetDashboardData();

    $scope.GetZoneWiseSalesValueAchievement = function () {
        if (!DateCheck($scope.FromDate, $scope.ToDate)) {
            return false;
        }
        $http({
            method: "POST",
            url: MyApp.rootPath + "ZoneWiseSalesAchievement/GetZoneWiseSalesValueAchievement",
            data: { fromDate: $scope.FromDate, toDate:$scope.ToDate}
        }).then(function (response) {
            if (response.data.Status == 'Ok') {
                $scope.gridZWiseSalesAchOptionsValue.data = response.data.Data;

            }
            else {
                toastr.warning("No Data Found!", '');
            }
        }, function () {
            toastr.error("Error!");
        });
    };


    //
    $scope.Reset = function () {
        $scope.FromDate = "";
        $scope.ToDate = "";
        $scope.gridZWiseSalesAchOptionsValue.data = [];
    };
});
//Date picker
