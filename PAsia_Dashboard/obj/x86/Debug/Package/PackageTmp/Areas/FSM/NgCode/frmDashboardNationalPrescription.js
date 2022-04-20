app.controller("myCtrl", function ($scope, $http, $interval, uiGridConstants) {
    //$scope.EventPerm(1);

    var dashboardData = '';
    var barLevel = [];
    var barData = [];
    var barBackgroundColor = [];





    $scope.GetDashboardData = function () {
        $http({
            method: "post",
            url: MyApp.rootPath + "DashboardNationalPrescription/GetNationalPrescriptionData",
            datatype: "json",
        }).then(function (response) {
            if (response.data.Status == 'Ok') {

                $scope.dashboardData = response.data.Data;




            } else {
                toastr.error("No Data Found!", '');
            }
        });
    };
    $scope.GetDashboardData();

    $http({
        method: "post",
        url: MyApp.rootPath + "DashboardNationalPrescription/GetGridData",
        datatype: "json",
    }).then(function (response) {
        if (response.data.Status == 'Ok') {

            $scope.NationalPrescriptionGridValue.data = response.data.Data;

        } else {
            toastr.error("No Data Found!", '');
        }
    });



    var DataSourceDeclaration = [

        { name: "RegionCode", displayName: "Region Code", visible: false },
        { name: "RegionName", displayName: "Region Name" },
        { name: "TotalMPO", displayName: "Given Target MPO" },
        { name: "TodayPrescription", displayName: "Today Pres", cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number'},
        { name: "NoOfPrescription", displayName: "Given Target Pres"},
        { name: "RegionWiseTarget", displayName: "Region Wise Target" },
        { name: "CurrentMonthPrescription", displayName: "Current Month Pres", cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number'},
        { name: "LastMPSD", displayName: "Last MPSD", cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number'},
        { name: "Achievement", displayName: "Ach(%)" },
        { name: "Deficit", displayName: "Deficit" }



    ];


    $scope.NationalPrescriptionGridValue = {
        //showGridFooter: true,
        showColumnFooter: true,
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