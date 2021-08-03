app.controller("myCtrl", function ($scope, $http, uiGridConstants) {
    $scope.EventPerm(33);
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
            //params: { DepotCode: "" }
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
            params: { DepotCode: $scope.frmReportMPOWiseTopPrescription.Depot }
        }).then(function (response) {
            if (response.data !== "") {
                $scope.Zones = response.data;
                $scope.frmReportMPOWiseTopPrescription.Zone = "";
                $scope.frmReportMPOWiseTopPrescription.Region = "";
                $scope.frmReportMPOWiseTopPrescription.Area = "";
                $scope.frmReportMPOWiseTopPrescription.Territory = "";
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
                DepotCode: $scope.frmReportMPOWiseTopPrescription.Depot,
                ZoneCode: $scope.frmReportMPOWiseTopPrescription.Zone
            }
        }).then(function (response) {
            if (response.data !== '') {
                $scope.Regions = response.data;
                $scope.frmReportMPOWiseTopPrescription.Region = "";
                $scope.frmReportMPOWiseTopPrescription.Area = "";
                $scope.frmReportMPOWiseTopPrescription.Territory = "";
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
                DepotCode: $scope.frmReportMPOWiseTopPrescription.Depot,
                ZoneCode: $scope.frmReportMPOWiseTopPrescription.Zone,
                RegionCode: $scope.frmReportMPOWiseTopPrescription.Region
            }
        }).then(function (response) {
            if (response.data !== '') {
                $scope.Areas = response.data;
                $scope.frmReportMPOWiseTopPrescription.Area = "";
                $scope.frmReportMPOWiseTopPrescription.Territory = "";
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
                DepotCode: $scope.frmReportMPOWiseTopPrescription.Depot,
                ZoneCode: $scope.frmReportMPOWiseTopPrescription.Zone,
                RegionCode: $scope.frmReportMPOWiseTopPrescription.Region,
                AreaCode: $scope.frmReportMPOWiseTopPrescription.Area
            }
        }).then(function (response) {
            if (response.data !== '') {
                $scope.Territories = response.data;
                $scope.frmReportMPOWiseTopPrescription.Territory = "";
            }
        },
            function () {
                toastr.warning("Error Occurred");
            });
    };

    
    var columnReportMPOWiseTopPrescriptionList = [
        { name: "SL_NO", displayName: "Sln",  type: "number" },
        //{ name: "MST_SL", displayName: "ID", width: 180 },
        { name: "USER_ID", displayName: "MPO Code"  },
        { name: "MIO_NAME", displayName: "MPO Name"},
        { name: "DESIG_NAME", displayName: "Designation" },
        { name: "TERRITORY_NAME", displayName: "Territory"},
        { name: "PRESCRIPTION_QTY", displayName: "Prescription Qty", type: "number" }
    ];


    $scope.gridReportMPOWiseTopPrescription = {
        //showGridFooter: true,
        //showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnReportMPOWiseTopPrescriptionList,
        //rowTemplate: rowTemplate(),
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: 'MPOWiseTopPrescription.csv',
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };
    //GetDoctorGridData
    $scope.GetMPOWiseTopPrescriptionData = function () {
        //if ($scope.FromDate && $scope.ToDate) {
        //    if (!DateCheck($scope.FromDate, $scope.ToDate)) {
        //        $scope.isDisabled = false;
        //        return false;
        //    }
        //}
        $scope.gridReportMPOWiseTopPrescription.data = [];
        $http({
            method: "POST",
            url: MyApp.rootPath + "ReportMPOWiseTopPrescription/GetMPOWiseTopPrescriptionData",
            params: { DepotCode: $scope.frmReportMPOWiseTopPrescription.Depot, ZoneCode: $scope.frmReportMPOWiseTopPrescription.Zone, RegionCode: $scope.frmReportMPOWiseTopPrescription.Region, AreaCode: $scope.frmReportMPOWiseTopPrescription.Area, TerritoryCode: $scope.frmReportMPOWiseTopPrescription.Territory, FromDate: $scope.FromDate, ToDate: $scope.ToDate }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridReportMPOWiseTopPrescription.data = response.data;
            } else {
                toastr.warning("No Data Found", { timeOut: 2000 });
            }
        }, function () {
            alert("Error Loading Category");
        });
    };

    //Reset
    $scope.Reset = function () {
        $scope.frmReportMPOWiseTopPrescription.Depot = "";
        $scope.frmReportMPOWiseTopPrescription.Zone = "";
        $scope.frmReportMPOWiseTopPrescription.Region = "";
        $scope.frmReportMPOWiseTopPrescription.Area = "";
        $scope.frmReportMPOWiseTopPrescription.Territory = "";

        $scope.FromDate = "";
        $scope.ToDate = "";
        if ($scope.accessLevel !== "Z") {
            $scope.Zones = [];
        }if ($scope.accessLevel !== "R") {
            $scope.Regions = [];
        }
        if ($scope.accessLevel !== "A") {
            $scope.Areas = [];
        }
        $scope.Territories = [];
        $scope.gridReportMPOWiseTopPrescription.data = [];
        $scope.FromDate = firstDay;
        $scope.ToDate = toDay;
    };
});

