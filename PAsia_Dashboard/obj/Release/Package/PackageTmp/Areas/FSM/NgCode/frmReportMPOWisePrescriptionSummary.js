app.controller("myCtrl", function ($scope, $http, uiGridConstants) {
    $scope.EventPerm(34);
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
    $scope.isZoneDisable = true;
    $scope.isAreaDisable = true;
    $scope.isRegionDisable = true;
    $scope.isZoneCheckBoxDisable = false;
    $scope.isAreaCheckBoxDisable = false;
    $scope.isRegionCheckBoxDisable = false;
    $http({
        method: "POST",
        url: MyApp.rootPath + "Default/GetAccessLevel"
        //params: { DepotCode: "" }
    }).then(function (response) {
        if (response.data !== '') {
            if (response.data[0].ACCESS_LEVEL === 'Z') {
                $scope.Zones = response.data;
                //$scope.GetRegion(response.data[0].ZONE_CODE);
                $scope.GetRegion(response.data[0].EMPLOYEE_CODE);
                $scope.GetArea(response.data[0].EMPLOYEE_CODE);
            }
            if (response.data[0].ACCESS_LEVEL === 'R') {
                $scope.Regions = response.data;
               // $scope.GetArea('',response.data[0].REGION_CODE);
                $scope.GetArea(response.data[0].EMPLOYEE_CODE);
                $scope.isZoneCheckBoxDisable = true;
                $scope.isAreaCheckBoxDisable = false;
                $scope.isRegionCheckBoxDisable = false;
            }
            if (response.data[0].ACCESS_LEVEL === 'A') {
                $scope.Areas = response.data;
                $scope.isZoneCheckBoxDisable = true;
                $scope.isAreaCheckBoxDisable = false;
                $scope.isRegionCheckBoxDisable = true;
            }
        }
        else {
            $scope.GetZone();
            $scope.GetRegion();
            $scope.GetArea();
        }
    }, function () {
        toastr.warning("Error Occurred");
    });
    $scope.GetZone = function (param) {
        $http({
            method: "POST",
            url: MyApp.rootPath + "Default/GetZoneByDepot"
            //params: { DepotCode: $scope.frmReportMPOWisePrescriptionSummary.Depot }
        }).then(function (response) {
            if (response.data !== '') {
                $scope.Zones = response.data;
            }
        },
            function () {
                toastr.warning("Error Occurred");
            });
    };
    $scope.GetRegion = function (dsmCodeParam) {
        $http({
            method: "POST",
            url: MyApp.rootPath + "Default/GetRegionByDSM",
            params: { dsmCode: dsmCodeParam }
        }).then(function (response) {
            if (response.data !== '') {
                if (response.data)
                    $scope.Regions = response.data;
            }
        },
            function () {
                toastr.warning("Error Occurred");
            });
    };
    $scope.GetArea = function (rsmCodeParam) {
        $http({
            method: "POST",
            url: MyApp.rootPath + "Default/GetAreaByRSM",
            params: { rsmCode: rsmCodeParam }
        }).then(function (response) {
            if (response.data !== '') {
                $scope.Areas = response.data;
            }
        },
            function () {
                toastr.warning("Error Occurred");
            });
    };
    $scope.ZoneCheckBoxClick = function () {
        $scope.gridReportMPOWisePrescriptionSummary.data = [];
        if ($scope.ZoneCheckBoxModal === "YES") {
            $scope.isZoneDisable = false;
            $scope.isRegionDisable = true;
            $scope.isAreaDisable = true;
            $scope.RegionCheckBoxModal = '';
            $scope.AreaCheckBoxModal = '';
            $scope.frmReportMPOWisePrescriptionSummary.Zone = "";
            $scope.frmReportMPOWisePrescriptionSummary.Region = "";
            $scope.frmReportMPOWisePrescriptionSummary.Area = "";
        } else {
            //$scope.isZoneDisable = true;
            //$scope.frmReportMPOWisePrescriptionSummary.Zone = "";
            $scope.Reset();
        }
    };
    $scope.RegionCheckBoxClick = function () {
        $scope.gridReportMPOWisePrescriptionSummary.data = [];
        if ($scope.RegionCheckBoxModal === "YES") {
            $scope.isZoneDisable = true;
            $scope.isRegionDisable = false;
            $scope.isAreaDisable = true;
            $scope.ZoneCheckBoxModal = '';
            $scope.AreaCheckBoxModal = '';
            $scope.frmReportMPOWisePrescriptionSummary.Zone = "";
            $scope.frmReportMPOWisePrescriptionSummary.Region = "";
            $scope.frmReportMPOWisePrescriptionSummary.Area = "";
        } else {
            //$scope.isRegionDisable = true;
            //$scope.frmReportMPOWisePrescriptionSummary.Region = "";
            $scope.Reset();
        }
    };
    $scope.AreaCheckBoxClick = function () {
        $scope.gridReportMPOWisePrescriptionSummary.data = [];
        if ($scope.AreaCheckBoxModal === "YES") {
            $scope.isZoneDisable = true;
            $scope.isRegionDisable = true;
            $scope.isAreaDisable = false;
            $scope.RegionCheckBoxModal = '';
            $scope.ZoneCheckBoxModal = '';
            $scope.frmReportMPOWisePrescriptionSummary.Zone = "";
            $scope.frmReportMPOWisePrescriptionSummary.Region = "";
            $scope.frmReportMPOWisePrescriptionSummary.Area = "";
        } else {
            //$scope.isAreaDisable = true;
            //$scope.frmReportMPOWisePrescriptionSummary.Area = "";
            $scope.Reset();
        }


    };
    //$scope.GetTerritoryByArea = function () {

    //    $http({
    //        method: "POST",
    //        url: MyApp.rootPath + "Default/GetTerritoryByArea",
    //        params: { DepotCode: $scope.frmReportMPOWisePrescriptionSummary.Depot, ZoneCode: $scope.frmReportMPOWisePrescriptionSummary.Zone, RegionCode: $scope.frmReportMPOWisePrescriptionSummary.Region, AreaCode: $scope.frmReportMPOWisePrescriptionSummary.Area }
    //    }).then(function (response) {
    //        if (response.data !== '') {
    //            $scope.Territories = response.data;
    //            $scope.frmReportMPOWisePrescriptionSummary.Territory = "";
    //        }
    //    },
    //        function () {
    //            toastr.warning("Error Occurred");
    //        });
    //};

    //$scope.GetHeadMenuList();
    //
    var columnMPOWisePrescriptionSummaryList = [
        { name: "SL_NO", displayName: "Sln", width: 80, type: "number" },
        { name: "USER_ID", displayName: "Code", width: 100 },
        { name: "USER_NAME", displayName: "User Name", width: 120 },
        { name: "ZONE_NAME", displayName: "Zone", width: 110 },
        { name: "REGION_NAME", displayName: "Region", width: 110 },
        { name: "AREA_NAME", displayName: "Area", width: 120 },
        { name: "TERRITORY_NAME", displayName: "Territory", width: 150 },
        { name: "NO_OF_OPD_PRES", displayName: "Total OPD Pres", width: 150, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number' },
        { name: "NO_OF_OTHER_PRES", displayName: "Total Other Pres", width: 150, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number' },
        { name: "TOTAL_PRES", displayName: "Total Pres", width: 150, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number' },
        { name: "TOTAL_PRODUCT", displayName: "Total Product", width: 150, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number' },
        { name: "TOTAL_XELPRO", displayName: "Total Xelpro", width: 150, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number' },
        { name: "TOTAL_BIONIC", displayName: "Total Bionic", width: 150, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number' },
        //{ name: "TOTAL_XELPRO_MUPS", displayName: "Total Xelpro MUPS", width: 150, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number' },
        { name: "TOTAL_CARDOTEL", displayName: "Total Cardotel", width: 150, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number' },
        { name: "TOTAL_FUXTIL", displayName: "Total  Fuxtil", width: 150, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number' },
        { name: "TOTAL_EZYLIFE", displayName: "Total  Ezylife", width: 150, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number' },
        //{ name: "TOTAL_EZYLIFE_KID", displayName: "Total  Ezylife Kid", width: 150, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number' },
        { name: "TOTAL_SWEET_DROPS", displayName: "Total  Sweet Drops", width: 150, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number' },
        { name: "TOTAL_MONKAST", displayName: "Total  Monkast", width: 150, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number' },
        { name: "TOTAL_STROMEC", displayName: "Total  Stromec", width: 150, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number' },
        { name: "TOTAL_DEEP_HEAT_NIGHT_RELIEF", displayName: "Total  Deep Heat Night Relief", width: 150, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number' },
        { name: "TOTAL_RY_JELLY", displayName: "Total  R-Y Jelly", width: 150, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number' },
        { name: "TOTAL_AZEXIA", displayName: "Total  AZEXIA", width: 150, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number' },
        { name: "TOTAL_CLONZY", displayName: "Total  Clonzy", width: 150, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number' },
        { name: "TOTAL_EMIREST", displayName: "Total  Emirest", width: 150, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number' },
        { name: "TOTAL_NAPRONIL_PLUS", displayName: "Total  Napronil Plus", width: 150, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number' },
        { name: "TOTAL_VELOFIX", displayName: "Total  Velofix", width: 150, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number' },
        { name: "TOTAL_RETROVIR", displayName: "Total  Retrovir", width: 150, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number' },
        { name: "TOTAL_GAVICOOL", displayName: "Total Gavicool", width: 150, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number' },
        { name: "TOTAL_AIRUP", displayName: "Total Airup", width: 150, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number' },
        { name: "TOTAL_OTHERS", displayName: "Total Others Product", width: 150, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number' }
    ];


    $scope.gridReportMPOWisePrescriptionSummary = {
        //showGridFooter: true,
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnMPOWisePrescriptionSummaryList,
        //rowTemplate: rowTemplate(),
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: 'MPOWisePrescriptionSummary.csv',
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        exporterExcelFilename: 'MPOWisePrescriptionSummary.xlsx',
        exporterExcelSheetName: 'Sheet1',
        exporterColumnScaleFactor: 4.5,
        exporterFieldApplyFilters: true,
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };

    $scope.GetMPOWisePrescriptionSummaryData = function () {
        var methodName = "GetMPOWisePrescriptionSummaryData";
        $scope.gridReportMPOWisePrescriptionSummary.data = [];
        $scope.gridReportMPOWisePrescriptionSummary.columnDefs[1].visible = true;
        $scope.gridReportMPOWisePrescriptionSummary.columnDefs[2].visible = true;
        $scope.gridReportMPOWisePrescriptionSummary.columnDefs[3].visible = false;
        $scope.gridReportMPOWisePrescriptionSummary.columnDefs[4].visible = true;
        $scope.gridReportMPOWisePrescriptionSummary.columnDefs[5].visible = true;
        $scope.gridReportMPOWisePrescriptionSummary.columnDefs[6].visible = true;
        if ($scope.ZoneCheckBoxModal === "YES") {
            $scope.gridReportMPOWisePrescriptionSummary.columnDefs[1].visible = false;
            $scope.gridReportMPOWisePrescriptionSummary.columnDefs[2].visible = false;
            $scope.gridReportMPOWisePrescriptionSummary.columnDefs[3].visible = true;
            $scope.gridReportMPOWisePrescriptionSummary.columnDefs[4].visible = false;
            $scope.gridReportMPOWisePrescriptionSummary.columnDefs[5].visible = false;
            $scope.gridReportMPOWisePrescriptionSummary.columnDefs[6].visible = false;
            methodName = "GetZoneWisePrescriptionSummaryData";

        }
        else if ($scope.RegionCheckBoxModal === "YES") {
            $scope.gridReportMPOWisePrescriptionSummary.columnDefs[1].visible = false;
            $scope.gridReportMPOWisePrescriptionSummary.columnDefs[2].visible = false;
            $scope.gridReportMPOWisePrescriptionSummary.columnDefs[3].visible = false;
            $scope.gridReportMPOWisePrescriptionSummary.columnDefs[4].visible = true;
            $scope.gridReportMPOWisePrescriptionSummary.columnDefs[5].visible = false;
            $scope.gridReportMPOWisePrescriptionSummary.columnDefs[6].visible = false;
            methodName = "GetRegionWisePrescriptionSummaryData";
        } else if ($scope.AreaCheckBoxModal === "YES") {
            $scope.gridReportMPOWisePrescriptionSummary.columnDefs[1].visible = false;
            $scope.gridReportMPOWisePrescriptionSummary.columnDefs[2].visible = false;
            $scope.gridReportMPOWisePrescriptionSummary.columnDefs[3].visible = false;
            $scope.gridReportMPOWisePrescriptionSummary.columnDefs[4].visible = false;
            $scope.gridReportMPOWisePrescriptionSummary.columnDefs[5].visible = true;
            $scope.gridReportMPOWisePrescriptionSummary.columnDefs[6].visible = false;
            methodName = "GetAreaWisePrescriptionSummaryData";
        }
        $http({
            method: "POST",
            //url: MyApp.rootPath + "ReportMPOWisePrescriptionSummary/GetMPOWisePrescriptionSummaryData",
            url: MyApp.rootPath + "ReportMPOWisePrescriptionSummary/" + methodName + "",
            params: {
                ZoneCode: $scope.frmReportMPOWisePrescriptionSummary.Zone,
                RegionCode: $scope.frmReportMPOWisePrescriptionSummary.Region,
                AreaCode: $scope.frmReportMPOWisePrescriptionSummary.Area,
                FromDate: $scope.FromDate,
                ToDate: $scope.ToDate
            }
        }).then(function (response) {
            if (response.data.Status === null || response.data.Status === undefined) {
                if (response.data.length > 0) {
                    $scope.gridReportMPOWisePrescriptionSummary.data = response.data;
                } else {
                    toastr.warning("No Data Found", { timeOut: 2000 });
                }
            } else {
                toastr.warning(response.data.Status, { timeOut: 2000 });
            }
        },
            function (response) {
                console.log(response);
                alert("Error Loading Category");
            });
    };

    //Reset
    $scope.Reset = function () {
        $scope.frmReportMPOWisePrescriptionSummary.Depot = "";
        $scope.frmReportMPOWisePrescriptionSummary.Zone = "";
        $scope.frmReportMPOWisePrescriptionSummary.Region = "";
        $scope.frmReportMPOWisePrescriptionSummary.Area = "";
        $scope.frmReportMPOWisePrescriptionSummary.Territory = "";

        $scope.FromDate = "";
        $scope.ToDate = "";
        //$scope.Regions = [];
        //$scope.Areas = [];
        //$scope.Territories = [];
        $scope.gridReportMPOWisePrescriptionSummary.data = [];
        $scope.FromDate = firstDay;
        $scope.ToDate = toDay;
        $scope.isZoneDisable = true;
        $scope.isAreaDisable = true;
        $scope.isRegionDisable = true;
        $scope.RegionCheckBoxModal = '';
        $scope.ZoneCheckBoxModal = '';
        $scope.AreaCheckBoxModal = '';
        $scope.gridReportMPOWisePrescriptionSummary.columnDefs[1].visible = true;
        $scope.gridReportMPOWisePrescriptionSummary.columnDefs[2].visible = true;
        $scope.gridReportMPOWisePrescriptionSummary.columnDefs[3].visible = true;
        $scope.gridReportMPOWisePrescriptionSummary.columnDefs[4].visible = true;
        $scope.gridReportMPOWisePrescriptionSummary.columnDefs[5].visible = true;
        $scope.gridReportMPOWisePrescriptionSummary.columnDefs[6].visible = true;
    };
});

