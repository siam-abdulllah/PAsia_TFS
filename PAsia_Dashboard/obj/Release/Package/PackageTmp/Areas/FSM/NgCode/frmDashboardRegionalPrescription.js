app.controller("myCtrl", function ($scope, $http, $interval, uiGridConstants) {
    //$scope.EventPerm(1);

    var dashboardData = '';
    var barLevel = [];
    var barData = [];
    var barBackgroundColor = [];
    $http({
        method: "POST",
        url: MyApp.rootPath + "Default/GetProdType"
        //params: { DepotCode: "" }
    }).then(function (response) {
        if (response.data !== '') {
            $scope.ProdTypes = response.data;
            $scope.frmDashboardRegionalPrescription.ProdType = { TYPE_NAME: 'Xelpro', TYPE: 'PB067' };
            $scope.GetDashboardData();
        }
    }, function () {
        toastr.warning("Error Occurred");
    });
    $scope.GetDashboardData = function () {
        $scope.prodName = $scope.frmDashboardRegionalPrescription.ProdType.TYPE_NAME;
        $http({
            method: "post",
            url: MyApp.rootPath + "DashboardRegionalPrescription/GetRegionalPrescriptionData",
            params: { prodType: $scope.frmDashboardRegionalPrescription.ProdType.TYPE},
            datatype: "json"
        }).then(function (response) {
            if (response.data.Status === 'Ok') {
                $scope.dashboardData = response.data.Data;
                $scope.GetGridData();
            } else {
                toastr.error("No Data Found!", '');
            }
        });
    };
    
    $scope.GetGridData = function() {
        $http({
            method: "post",
            url: MyApp.rootPath + "DashboardRegionalPrescription/GetGridData",
            params: { prodType: $scope.frmDashboardRegionalPrescription.ProdType.TYPE },
            datatype: "json",
        }).then(function(response) {
            if (response.data.Status == 'Ok') {

                $scope.RegionalPrescriptionGridValue.data = response.data.Data;

            } else {
                toastr.error("No Data Found!", '');
            }
        });
    }


    var DataSourceDeclaration = [

        { name: "RegionCode", displayName: "Region Code", visible: false },
        { name: "RegionName", displayName: "Region Name" },
        { name: "Commitment", displayName: "Commitment" },
        { name: "TodayPrescription", displayName: "Today Pres" },
        { name: "Cumulative", displayName: "Cumulative" },
        { name: "Achievement", displayName: "Ach(%)" },
        { name: "LastMPSD", displayName: "Last MPSD" },
        { name: "Growth", displayName: "Growth" },
        { name: "HonorariumAmount", displayName: "Honorarium Amount" },
        { name: "ExistingMPO", displayName: "Existing MPO" },
        { name: "SendingMPO", displayName: "Sending MPO" }
    ];


    $scope.RegionalPrescriptionGridValue = {
        //showGridFooter: true,
        //showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: DataSourceDeclaration,
        //rowTemplate: rowTemplate(),
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: 'RegionalPrescription.csv',
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }

    };
});