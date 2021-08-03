app.controller("myCtrl", function ($scope, $http, uiGridConstants, $q) {
    $scope.EventPerm(39);
    var date = new Date();
    var firstDay = new Date(date.getFullYear(), date.getMonth(), 1);
    firstDay = ('0' + firstDay.getDate()).slice(-2) + '-' + ('0' + (firstDay.getMonth() + 1)).slice(-2) + '-' + firstDay.getFullYear();
    var toDay = new Date();
    toDay = ('0' + toDay.getDate()).slice(-2) + '-' + ('0' + (toDay.getMonth() + 1)).slice(-2) + '-' + toDay.getFullYear();
    $scope.FromDate = firstDay;
    $scope.ToDate = toDay;
    var columnReportDoctorWiseSpecialProdPrescrList = [
        { name: "SL_NO", displayName: "Sln", type: "number", width: 50  },
        { name: "DOCTOR_CODE", displayName: "Code", width: 80 },
        { name: "DOCTOR_NAME", displayName: "Doctor Name", width: 80 },
        { name: "DEGREES", displayName: "Degree", width: 80 },
        { name: "REGION_NAME", displayName: "Region Name", width: 80 },
        { name: "AREA_NAME", displayName: "Area Name", width: 80},
        { name: "TERRITORY_NAME", displayName: "Territory Name", width: 80 },
        { name: "MIO_CODE", displayName: "MIO Code", type: "number", width: 80 },
        { name: "MIO_NAME", displayName: "MIO Name", width: 80},
        { name: "XELPRO", displayName: "Xelpro", width: 75, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number' },
        { name: "EZYLIFE", displayName: "Ezylife", width: 75, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number' },
        //{ name: "EZYLIFE_KIDS", displayName: "Ezylife Kids", width: 75, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number' },
        { name: "BIONIC", displayName: "Bionic", width: 75, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number' },
        { name: "CARTODEL", displayName: "Cartodel", width: 75, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number'},
        { name: "CLONZY", displayName: "Clonzy", width: 75, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number'},
        { name: "EMIREST", displayName: "Emirest", width: 75, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number'},
        { name: "ERAXIT", displayName: "Eraxit", width: 75, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number'},
        { name: "FUXTIL", displayName: "Fuxtil", width: 75, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number'},
        { name: "MELASIN", displayName: "Melasin", width: 75, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number'},
        { name: "MONKAST", displayName: "Monkast", width: 75, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number'},
        { name: "NAPRONIL", displayName: "Napronil", width: 75, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number'},
        { name: "ORALFRESH", displayName: "Oral Fresh", width: 75, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number'},
        { name: "TELFEX", displayName: "Telflex", width: 75, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number'},
        { name: "TRIMAX", displayName: "Trimax", width: 75, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number'},
        { name: "VELOFIX", displayName: "Velofix", width: 75, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number'},
        { name: "XEMIMAX", displayName: "Xemimax", width: 75, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number'},
        { name: "XTRACAL", displayName: "Xtracal", width: 75, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number'},
        { name: "STROMEC", displayName: "Stromec", width: 75, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number'},
        { name: "DEEP_HEAT_NIGHT_RELIEF", displayName: "Deep Heat Night Relief", width: 75, cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number'}

    ];
    $scope.gridReportDoctorWiseSpecialProdPrescr = {
        //showGridFooter: true,
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnReportDoctorWiseSpecialProdPrescrList,
        //columnDefs: $scope.columns,
        //rowTemplate: rowTemplate(),
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: 'Doctor_wise_product_prescription.csv',
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };
    $scope.GetGridData = function () {
        $scope.gridReportDoctorWiseSpecialProdPrescr.data = [];
        $http({
            method: "POST",
            url: MyApp.rootPath + "ReportDoctorWiseSpecialProdPrescr/GetDoctorWiseProdPrescrData",
            params: { DepotCode: $scope.frmReportDoctorWiseSpecialProdPrescr.Depot, ZoneCode: $scope.frmReportDoctorWiseSpecialProdPrescr.Zone, RegionCode: $scope.frmReportDoctorWiseSpecialProdPrescr.Region, AreaCode: $scope.frmReportDoctorWiseSpecialProdPrescr.Area, TerritoryCode: $scope.frmReportDoctorWiseSpecialProdPrescr.Territory, FromDate: $scope.FromDate, ToDate: $scope.ToDate , ProdType: $scope.frmReportDoctorWiseSpecialProdPrescr.ProdType }
        }).then(function (response) {
            if (response.data.Status === null || response.data.Status === undefined) {
                if (response.data.length > 0) {
                    $scope.gridReportDoctorWiseSpecialProdPrescr.data = response.data;
                } else {
                    toastr.warning("No Data Found", { timeOut: 2000 });
                }
            } else {
                toastr.warning(response.data.Status, { timeOut: 2000 });
            }
            }, function (response) {
            alert(response.data);
            console.log(response);
            toastr.warning("Error Occurred!", { timeOut: 2000 });
        });
    };
    //Reset
    $scope.Reset = function () {
        $scope.gridReportDoctorWiseSpecialProdPrescr.data = [];
        $scope.Regions = [];
        $scope.Zones = [];
        $scope.Areas = [];
        $scope.Territories = [];
        $scope.FromDate = firstDay;
        $scope.ToDate = toDay;
    };
});

