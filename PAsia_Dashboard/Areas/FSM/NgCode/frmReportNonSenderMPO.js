app.controller("myCtrl", function ($scope, $http, uiGridConstants) {
    $scope.EventPerm(38);
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
    $http({
        method: "POST",
        url: MyApp.rootPath + "Default/GetDepot"
        //params: { DepotCode: "" }
    }).then(function (response) {
        if (response.data !== '') {
            $scope.Depots = response.data;
        }
    }, function () {
        toastr.warning("Error Occurred");
    });

    $scope.GetZoneByDepot = function() {
        $http({
            method: "POST",
            url: MyApp.rootPath + "Default/GetZoneByDepot",
            params: { DepotCode: $scope.frmReportNonSenderMPO.Depot }
        }).then(function(response) {
                if (response.data !== '') {
                    $scope.Zones = response.data;
                    $scope.frmReportNonSenderMPO.Zone = "";
                    $scope.frmReportNonSenderMPO.Region = "";
                    $scope.frmReportNonSenderMPO.Area = "";
                    $scope.frmReportNonSenderMPO.Territory = "";
                    $scope.Regions = [];
                    $scope.Areas = [];
                    $scope.Territories = [];


                }
            },
            function() {
                toastr.warning("Error Occurred");
            });
    };
    $scope.GetRegionByZone = function() {

        $http({
            method: "POST",
            url: MyApp.rootPath + "Default/GetRegionByZone",
            params: {
                DepotCode: $scope.frmReportNonSenderMPO.Depot,
                ZoneCode: $scope.frmReportNonSenderMPO.Zone
            }
        }).then(function(response) {
                if (response.data !== '') {
                    $scope.Regions = response.data;

                    $scope.frmReportNonSenderMPO.Region = "";
                    $scope.frmReportNonSenderMPO.Area = "";
                    $scope.frmReportNonSenderMPO.Territory = "";
                    $scope.Areas = [];
                    $scope.Territories = [];
                }
            },
            function() {
                toastr.warning("Error Occurred");
            });
    };


    $scope.GetAreaByRegion = function() {

        $http({
            method: "POST",
            url: MyApp.rootPath + "Default/GetAreaByRegion",
            params: {
                DepotCode: $scope.frmReportNonSenderMPO.Depot,
                ZoneCode: $scope.frmReportNonSenderMPO.Zone,
                RegionCode: $scope.frmReportNonSenderMPO.Region
            }
        }).then(function(response) {
                if (response.data !== '') {
                    $scope.Areas = response.data;

                    $scope.frmReportNonSenderMPO.Area = "";
                    $scope.frmReportNonSenderMPO.Territory = "";
                    $scope.Territories = [];
                }
            },
            function() {
                toastr.warning("Error Occurred");
            });
    };
    $scope.GetTerritoryByArea = function() {

        $http({
            method: "POST",
            url: MyApp.rootPath + "Default/GetTerritoryByArea",
            params: {
                DepotCode: $scope.frmReportNonSenderMPO.Depot,
                ZoneCode: $scope.frmReportNonSenderMPO.Zone,
                RegionCode: $scope.frmReportNonSenderMPO.Region,
                AreaCode: $scope.frmReportNonSenderMPO.Area
            }
        }).then(function(response) {
                if (response.data !== '') {
                    $scope.Territories = response.data;
                    $scope.frmReportNonSenderMPO.Territory = "";
                }
            },
            function() {
                toastr.warning("Error Occurred");
            });
    };

    //$scope.GetHeadMenuList();
    //
    var columnReportNonSenderMPOList = [
        { name: "SL_NO", displayName: "Sln", width: 80, type: "number" },
        //{ name: "MST_SL", displayName: "ID", width: 180 },
        { name: "MIO_CODE", displayName: "MPO Code", width: 120 },
        { name: "MIO_NAME", displayName: "MPO Name", width:250 },
        { name: "ZONE_CODE", displayName: "ZONE_CODE",visible:false},
        { name: "ZONE_NAME", displayName: "ZONE", width: 150 },
        { name: "REGION_CODE", displayName: "REGION_CODE", visible: false },
        { name: "REGION_NAME", displayName: "REGION", width: 150 },
        { name: "AREA_CODE", displayName: "AREA_CODE",visible:false },
        { name: "AREA_NAME", displayName: "Area", width: 180 },
        { name: "TERRITORY_CODE", displayName: "Territory_Code", visible: false },
        { name: "TERRITORY_NAME", displayName: "Territory", width: 180 }
    ];


    $scope.gridReportNonSenderMPO = {
        //showGridFooter: true,
        //showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnReportNonSenderMPOList,
        //rowTemplate: rowTemplate(),
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: 'NonSenderMPO.csv',
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };
  

    //GetDoctorGridData
    $scope.GetNonSenderMPOData = function () {
        //if ($scope.FromDate && $scope.ToDate) {
        //    if (!DateCheck($scope.FromDate, $scope.ToDate)) {
        //        $scope.isDisabled = false;
        //        return false;
        //    }
        //}
        $scope.gridReportNonSenderMPO.data = [];
        $http({
            method: "POST",
            url: MyApp.rootPath + "ReportNonSenderMPO/GetNonSenderMPOData",
            params: { DepotCode: $scope.frmReportNonSenderMPO.Depot, ZoneCode: $scope.frmReportNonSenderMPO.Zone, RegionCode: $scope.frmReportNonSenderMPO.Region, AreaCode: $scope.frmReportNonSenderMPO.Area, TerritoryCode: $scope.frmReportNonSenderMPO.Territory, FromDate: $scope.FromDate, ToDate: $scope.ToDate }
        }).then(function (response) {
            if (response.data.length>0) {
                $scope.gridReportNonSenderMPO.data = response.data;
            } else {
                toastr.warning("No Data Found", { timeOut: 2000 });
            }
        }, function () {
            alert("Error Loading Category");
        });
    };

    //Reset
    $scope.Reset = function () {
        $scope.frmReportNonSenderMPO.Depot = "";
        $scope.frmReportNonSenderMPO.Zone = "";
        $scope.frmReportNonSenderMPO.Region = "";
        $scope.frmReportNonSenderMPO.Area = "";
        $scope.frmReportNonSenderMPO.Territory = "";
        
        $scope.FromDate ="";
        $scope.ToDate = "";
        $scope.Regions = [];
        $scope.Areas = [];
        $scope.Territories = [];
        $scope.gridReportNonSenderMPO.data = [];
        $scope.FromDate = firstDay;
        $scope.ToDate = toDay;
    };
});

