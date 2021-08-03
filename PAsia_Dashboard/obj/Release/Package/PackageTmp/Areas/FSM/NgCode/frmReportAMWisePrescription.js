app.controller("myCtrl", function ($scope, $http, uiGridConstants) {
    $scope.EventPerm(40);
    // $scope.GetDepot();
    //$scope.isDisabled = false;
    var index = "";
    var date = new Date();
    var firstDay = new Date(date.getFullYear(), date.getMonth(), 1);
    firstDay = ('0' + firstDay.getDate()).slice(-2) + '-' + ('0' + (firstDay.getMonth() + 1)).slice(-2) + '-' + firstDay.getFullYear();
    var toDay = new Date();
    toDay = ('0' + toDay.getDate()).slice(-2) + '-' + ('0' + (toDay.getMonth() + 1)).slice(-2) + '-' + toDay.getFullYear();
    $scope.FromDate = firstDay;
    $scope.ToDate = toDay;
    $scope.isDepotDisable = false;
    $scope.isZoneDisable = false;
    $scope.isAreaDisable = false;
    $scope.isRegionDisable = false;
    $scope.isTerritoryDisable = false;

    $http({
        method: "POST",
        url: MyApp.rootPath + "Default/GetAccessLevel"
        //params: { DepotCode: "" }
    }).then(function (response) {
        if (response.data !== '') {
            $scope.accessLevel = response.data[0].ACCESS_LEVEL;
            if (response.data[0].ACCESS_LEVEL === "Z") {
                $scope.Zones = response.data;
                //$scope.GetRegion(response.data[0].ZONE_CODE);
                //$scope.GetArea(response.data[0].ZONE_CODE);
                $scope.isDepotDisable = true;
                $scope.isZoneDisable = false;
                $scope.isRegionDisable = false;
                $scope.isAreaDisable = false;
                $scope.isTerritoryDisable = false;
            }
            if (response.data[0].ACCESS_LEVEL === "R") {
                $scope.Regions = response.data;
                //$scope.GetArea(response.data[0].REGION_CODE);
                $scope.isDepotDisable = true;
                $scope.isZoneDisable = true;
                $scope.isRegionDisable = false;
                $scope.isAreaDisable = false;
                $scope.isTerritoryDisable = false;
            }
            if (response.data[0].ACCESS_LEVEL === "A") {
                $scope.Areas = response.data;
                $scope.isDepotDisable = true;
                $scope.isZoneDisable = true;
                $scope.isRegionDisable = true;
                $scope.isAreaDisable = false;
                $scope.isTerritoryDisable = false;
            }
        }
        else {
            $scope.GetDepot();
        }
    }, function () {
        toastr.warning("Error Occurred");
    });

    $scope.GetDepot = function () {
        $http({
            method: "POST",
            url: MyApp.rootPath + "Default/GetDepot"
        }).then(function (response) {
            if (response.data !== '') {
                $scope.Depots = response.data;
            }
        },
            function () {
                toastr.warning("Error Occurred");
            });
    };

    $scope.GetZoneByDepot = function () {
        $http({
            method: "POST",
            url: MyApp.rootPath + "Default/GetZoneByDepot",
            params: { DepotCode: $scope.frmReportAMWisePrescription.Depot }
        }).then(function (response) {
            if (response.data !== "") {
                $scope.Zones = response.data;
                $scope.frmReportAMWisePrescription.Zone = "";
                $scope.frmReportAMWisePrescription.Region = "";
                $scope.frmReportAMWisePrescription.Area = "";
                $scope.frmReportAMWisePrescription.Territory = "";
                $scope.Regions = [];
                $scope.Areas = [];
                $scope.Territories = [];
            }
        },
            function () {
                toastr.warning("Error Occurred");
            });
    };
    $scope.GetRegionByZone = function () {
        $http({
            method: "POST",
            url: MyApp.rootPath + "Default/GetRegionByZone",
            params: {
                DepotCode: $scope.frmReportAMWisePrescription.Depot,
                ZoneCode: $scope.frmReportAMWisePrescription.Zone
            }
        }).then(function (response) {
            if (response.data !== '') {
                $scope.Regions = response.data;
                $scope.frmReportAMWisePrescription.Region = "";
                $scope.frmReportAMWisePrescription.Area = "";
                $scope.frmReportAMWisePrescription.Territory = "";
                $scope.Areas = [];
                $scope.Territories = [];
            }
        },
            function () {
                toastr.warning("Error Occurred");
            });
    };
    $scope.GetAreaByRegion = function () {
        $http({
            method: "POST",
            url: MyApp.rootPath + "Default/GetAreaByRegion",
            params: {
                DepotCode: $scope.frmReportAMWisePrescription.Depot,
                ZoneCode: $scope.frmReportAMWisePrescription.Zone,
                RegionCode: $scope.frmReportAMWisePrescription.Region
            }
        }).then(function (response) {
            if (response.data !== '') {
                $scope.Areas = response.data;
                $scope.frmReportAMWisePrescription.Area = "";
                $scope.frmReportAMWisePrescription.Territory = "";
                $scope.Territories = [];
            }
        },
            function () {
                toastr.warning("Error Occurred");
            });
    };
    $scope.GetTerritoryByArea = function () {
        $http({
            method: "POST",
            url: MyApp.rootPath + "Default/GetTerritoryByArea",
            params: {
                DepotCode: $scope.frmReportAMWisePrescription.Depot,
                ZoneCode: $scope.frmReportAMWisePrescription.Zone,
                RegionCode: $scope.frmReportAMWisePrescription.Region,
                AreaCode: $scope.frmReportAMWisePrescription.Area
            }
        }).then(function (response) {
            if (response.data !== '') {
                $scope.Territories = response.data;
                $scope.frmReportAMWisePrescription.Territory = "";
            }
        },
            function () {
                toastr.warning("Error Occurred");
            });
    };


    var columnReportAMWisePrescriptionList = [
        { name: "SL_NO", displayName: "Sln", type: "number" },
        { name: "AM_CODE", displayName: "AM Code" },
        { name: "AM_NAME", displayName: "AM NAME" },
        { name: "REGION_CODE", displayName: "REGION_CODE", visible: false },
        { name: "REGION_NAME", displayName: "Region Name" },
        { name: "AREA_CODE", displayName: "AREA_CODE", visible: false},
        { name: "AREA_NAME", displayName: "Area Name"},
        { name: "TOTAL_PRESCRIPTION", displayName: "Total Prescription", type: "number", cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number' }
        
    ];


    $scope.gridReportAMWisePrescription = {
        //showGridFooter: true,
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnReportAMWisePrescriptionList,
        //rowTemplate: rowTemplate(),
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: 'AMWisePrescription.csv',
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };
    //GetDoctorGridData
    $scope.GetAMWisePrescriptionData = function () {
        $scope.gridReportAMWisePrescription.data = [];
        $http({
            method: "POST",
            url: MyApp.rootPath + "ReportAMWisePrescription/GetAMWisePrescriptionData",
            params: { FromDate: $scope.FromDate, ToDate: $scope.ToDate }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridReportAMWisePrescription.data = response.data;
            } else {
                toastr.warning("No Data Found", { timeOut: 2000 });
            }
        }, function () {
            alert("Error Loading Category");
        });
    };

    //Reset
    $scope.Reset = function () {
        //$scope.frmReportAMWisePrescription.Depot = "";
        //$scope.frmReportAMWisePrescription.Zone = "";
        //$scope.frmReportAMWisePrescription.Region = "";
        //$scope.frmReportAMWisePrescription.Area = "";
        //$scope.frmReportAMWisePrescription.Territory = "";

        //$scope.FromDate = "";
        //$scope.ToDate = "";
        //if ($scope.accessLevel !== "Z") {
        //    $scope.Zones = [];
        //}if ($scope.accessLevel !== "R") {
        //    $scope.Regions = [];
        //}
        //if ($scope.accessLevel !== "A") {
        //    $scope.Areas = [];
        //}
        //$scope.Territories = [];
        $scope.gridReportAMWisePrescription.data = [];
        $scope.FromDate = firstDay;
        $scope.ToDate = toDay;
    };
});

