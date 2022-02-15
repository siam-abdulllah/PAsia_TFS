app.controller("myCtrl", function ($scope, $http, uiGridConstants) {
    $scope.EventPerm(55);
    $scope.isDisabled = false;

    //$scope.GetHeadMenuList();


    //var columnTargetSalesList = [
    //    { name: "SL_NO", displayName: "Sl. No", width: 50, },
    //    { name: "PRODUCT_CODE", displayName: "Product Code", width: 90, },
    //    { name: "PRODUCT_NAME", displayName: "Product Name", width: 100, },
    //    { name: "BRAND_NAME", displayName: "Brand Name", width: 100, },
    //    { name: "PACK_SIZE", displayName: "Pack Size", width: 90, },
    //    { name: "UNIT_TARGET", displayName: "Unit Target", cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, width: 130, cellClass: 'grid-align', footerCellFilter: 'number:2' },
    //    { name: "VALUE_TARGET", displayName: "Value Target", cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, width: 110, cellClass: 'grid-align', footerCellFilter: 'number:2' },
    //    { name: "UNIT_SALES", displayName: "Unit Sales", cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, width: 110, cellClass: 'grid-align', footerCellFilter: 'number:2' },
    //    { name: "VALUE_SALES", displayName: "Value Sales", cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' },
    //    { name: "CURRENT_STOCK", displayName: "Cuttent Stock", cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' }

    //];

    //$scope.gridTargetSalesOptionsValue = {
    //    //showGridFooter: true,
    //    showColumnFooter: true,
    //    enableFiltering: true,
    //    enableSorting: true,
    //    columnDefs: columnTargetSalesList,
    //    //rowTemplate: rowTemplate(),
    //    enableGridMenu: true,
    //    enableSelectAll: true,
    //    exporterCsvFilename: 'National Target Sales.csv',
    //    exporterMenuPdf: false,
    //    exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
    //    onRegisterApi: function (gridApiValue) {
    //        $scope.gridApi = gridApiValue;
    //    }

    //};
    //function customAchAggregate(upToSalesValue, target) {
    //    var result = (upToSalesValue * 100) / target;
    //    if (!isFinite(result)) {
    //        result = "total: 0.00";
    //    } else {
    //        result = "total: " + result.toFixed(2);
    //    }
    //    return result;

    //}



    $scope.GetStructureRefreshAch = function () {
        $scope.isDisabled = true;
        //if (!DateCheck($scope.FromDate, $scope.ToDate)) {
        //    $scope.isDisabled = false;
        //    return false;
        //}
        $http({
            method: "POST",
            url: MyApp.rootPath + "StructureRefresh/GetStructureRefreshValue",
            data: { fromDate: $scope.FromDate, toDate: $scope.ToDate }
        }).then(function (response) {

            //if (response.data.length > 0) {
            //    $scope.gridTargetSalesOptionsValue.data = response.data;

            //}
            //else {
            //    toastr.warning("No Data Found!", '');
            //    $scope.gridTargetSalesOptionsValue.data = [];
            //}
            $scope.isDisabled = false;
        }, function (response) {
            toastr.error("Error!");
            $scope.isDisabled = false;
        });
    };

    //$scope.GetTargetSalesAch();

    $scope.Reset = function () {
        $scope.FromDate = "";
        $scope.ToDate = "";
        //$scope.gridTargetSalesOptionsValue.data = [];
        $scope.isDisabled = false;
    };
});
//Date picker
