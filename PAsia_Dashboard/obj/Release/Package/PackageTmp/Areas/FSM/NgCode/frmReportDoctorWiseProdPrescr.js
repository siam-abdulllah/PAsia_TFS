app.controller("myCtrl", function ($scope, $http, uiGridConstants, $q) {
    $scope.EventPerm(37);
    var date = new Date();
    var firstDay = new Date(date.getFullYear(), date.getMonth(), 1);
    firstDay = ('0' + firstDay.getDate()).slice(-2) + '-' + ('0' + (firstDay.getMonth() + 1)).slice(-2) + '-' + firstDay.getFullYear();
    var toDay = new Date();
    toDay = ('0' + toDay.getDate()).slice(-2) + '-' + ('0' + (toDay.getMonth() + 1)).slice(-2) + '-' + toDay.getFullYear();
    $scope.FromDate = firstDay;
    $scope.ToDate = toDay;
    var serverIPAdd = window.location.origin.replace('8998', '8999');
    $http({
        method: "POST",
        url: MyApp.rootPath + "Default/GetProdType"
        //params: { DepotCode: "" }
    }).then(function (response) {
        if (response.data !== '') {
            $scope.ProdTypes = response.data;
        }
    }, function () {
        toastr.warning("Error Occurred");
        });

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

    $scope.GetZoneByDepot = function () {
        $http({
            method: "POST",
            url: MyApp.rootPath + "Default/GetZoneByDepot",
            params: { DepotCode: $scope.frmReportDoctorWiseProdPrescr.Depot }
        }).then(function (response) {
            if (response.data !== '') {
                $scope.Zones = response.data;
                $scope.frmReportDoctorWiseProdPrescr.Zone = "";
                $scope.frmReportDoctorWiseProdPrescr.Region = "";
                $scope.frmReportDoctorWiseProdPrescr.Area = "";
                $scope.frmReportDoctorWiseProdPrescr.Territory = "";
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
            params: { DepotCode: $scope.frmReportDoctorWiseProdPrescr.Depot, ZoneCode: $scope.frmReportDoctorWiseProdPrescr.Zone }
        }).then(function (response) {
            if (response.data !== '') {
                $scope.Regions = response.data;

                $scope.frmReportDoctorWiseProdPrescr.Region = "";
                $scope.frmReportDoctorWiseProdPrescr.Area = "";
                $scope.frmReportDoctorWiseProdPrescr.Territory = "";
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
            params: { DepotCode: $scope.frmReportDoctorWiseProdPrescr.Depot, ZoneCode: $scope.frmReportDoctorWiseProdPrescr.Zone, RegionCode: $scope.frmReportDoctorWiseProdPrescr.Region }
        }).then(function (response) {
            if (response.data !== '') {
                $scope.Areas = response.data;
                $scope.frmReportDoctorWiseProdPrescr.Area = "";
                $scope.frmReportDoctorWiseProdPrescr.Territory = "";
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
            params: { DepotCode: $scope.frmReportDoctorWiseProdPrescr.Depot, ZoneCode: $scope.frmReportDoctorWiseProdPrescr.Zone, RegionCode: $scope.frmReportDoctorWiseProdPrescr.Region, AreaCode: $scope.frmReportDoctorWiseProdPrescr.Area }
        }).then(function (response) {
            if (response.data !== '') {
                $scope.Territories = response.data;
                $scope.frmReportDoctorWiseProdPrescr.Territory = "";
            }
        },
            function () {
                toastr.warning("Error Occurred");
            });
    };

    var columnReportDoctorWiseProdPrescrList = [
        { name: "SL_NO", displayName: "Sln", width: 80, type: "number" },
        { name: "DOCTOR_CODE", displayName: "Code", width: 80 },
        { name: "DOCTOR_NAME", displayName: "Doctor Name", width: 200 },
        { name: "DEGREES", displayName: "Degree", width: 110 },
        { name: "ZONE_CODE", displayName: "ZONE_CODE",visible:false },
        { name: "REGION_NAME", displayName: "Region Name", width: 110 },
        { name: "REGION_CODE", displayName: "REGION_CODE", visible: false },
        { name: "AREA_CODE", displayName: "AREA_CODE", visible: false },
        { name: "AREA_NAME", displayName: "Area Name", width: 110 },
        { name: "TERRITORY_CODE", displayName: "TERRITORY_CODE", visible: false },
        { name: "TERRITORY_NAME", displayName: "Territory Name", width: 110 },
        { name: "TOT_PRES", displayName: "Total Prescription", width: 110, type: "number"  },
        { name: "CLASS_GROUP", displayName: "Doctor Type", width: 110 },
        {
            name: 'View Action', displayName: "Action", enableFiltering: false, width: "100", pinnedRight: true,
            cellTemplate: '<div style="text-align:center;position: relative;width:100%;padding:2px 2px 2px 5px;"><button class="btn btn-primary btn-sm" ng-click="grid.appScope.viewItemGrid(row)"><i class="fa fa-search"></i>&nbspView</button></div>'
        }

    ];
    $scope.gridReportDoctorWiseProdPrescr = {
        //showGridFooter: true,
        //showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnReportDoctorWiseProdPrescrList,
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
    $scope.viewItemGrid = function (row) {
        var r = row;
        $http({
            method: "POST",
            url: MyApp.rootPath + "ReportMPOWisePrescription/GetMPOWisePrescriptionData",
            params: { ZoneCode: row.entity.ZONE_CODE, RegionCode: row.entity.REGION_CODE, AreaCode: row.entity.AREA_CODE, TerritoryCode: row.entity.TERRITORY_CODE, FromDate: $scope.FromDate, ToDate: $scope.ToDate, DoctorCode: row.entity.DOCTOR_CODE, ProdType: $scope.frmReportDoctorWiseProdPrescr.ProdType}
        }).then(function (response) {
            if (response.data.Status === null || response.data.Status === undefined) {
                if (response.data.length > 0) {
                    $scope.gridReportMPOWisePrescription.data = response.data;
                    $('#DoctorPresModal').modal('show');
                } else {
                    toastr.warning("No Data Found", { timeOut: 2000 });
                }
            } else {
                toastr.warning(response.data.Status, { timeOut: 2000 });
            }
        }, function (response) {
            console.log(response);
            toastr.warning("Error Occurred!", { timeOut: 2000 });
        });
    };
    $scope.GetGridData = function () {
        $scope.gridReportDoctorWiseProdPrescr.data = [];
        $http({
            method: "POST",
            url: MyApp.rootPath + "ReportDoctorWiseProdPrescr/GetDoctorWiseProdPrescrData",
            params: { DepotCode: $scope.frmReportDoctorWiseProdPrescr.Depot, ZoneCode: $scope.frmReportDoctorWiseProdPrescr.Zone, RegionCode: $scope.frmReportDoctorWiseProdPrescr.Region, AreaCode: $scope.frmReportDoctorWiseProdPrescr.Area, TerritoryCode: $scope.frmReportDoctorWiseProdPrescr.Territory, FromDate: $scope.FromDate, ToDate: $scope.ToDate, ProdType: $scope.frmReportDoctorWiseProdPrescr.ProdType, DoctorType: $scope.DoctorType}
        }).then(function (response) {
            if (response.data.Status === null || response.data.Status === undefined) {
                if (response.data.length > 0) {
                    $scope.gridReportDoctorWiseProdPrescr.data = response.data;
                } else {
                    toastr.warning("No Data Found", { timeOut: 2000 });
                }
            } else {
                toastr.warning(response.data.Status, { timeOut: 2000 });
            }
        }, function (response) {
            console.log(response);
            toastr.warning("Error Occurred!", { timeOut: 2000 });
        });
    };
    //Reset
    $scope.Reset = function () {
        $scope.gridReportDoctorWiseProdPrescr.data = [];
        $scope.frmReportDoctorWiseProdPrescr.Depot = "";
        $scope.frmReportDoctorWiseProdPrescr.Zone = "";
        $scope.frmReportDoctorWiseProdPrescr.Region = "";
        $scope.frmReportDoctorWiseProdPrescr.Area = "";
        $scope.frmReportDoctorWiseProdPrescr.Territory = "";
        $scope.frmReportDoctorWiseProdPrescr.ProdType =undefined;
        $scope.Regions = [];
        $scope.Zones = [];
        $scope.Areas = [];
        $scope.Territories = [];
        $scope.FromDate = firstDay;
        $scope.ToDate = toDay;
        $scope.DoctorType = "Honorarium";
    };

    var columnReportMPOWisePrescriptionList = [
        { name: "SL_NO", displayName: "Sln", width: 80, type: "number" },
        { name: "USER_ID", displayName: "User ID", visible: false },
        { name: "EMPLOYEE_NAME", displayName: "Employee Name", width: 140 },
        { name: "CAPTURE_TIME", displayName: "Capture Date & Time", width: 140 },
        { name: "PRESCRIPTION_TYPE", displayName: "Pescription Type", width: 120 },
        //{ field: 'PRESCRIPTION_URL', displayName: "Image", width: 120, cellTemplate: '<div style="text-align:center;position: relative;width:100%;padding:2px 2px 2px 6px;"><img width="50px" ng-src="' + serverIPAdd + '{{grid.getCellValue(row, col)}}" lazy-src ng-click="grid.appScope.showImage(row)"></div>' },
        {
            field: 'PRESCRIPTION_URL', displayName: "Image", width: 120, cellTemplate: '<div style="text-align:center;position: relative;width:100%;padding:2px 2px 2px 6px;"><a data-magnify="gallery" data-caption="Prescription" href="' + serverIPAdd + '{{grid.getCellValue(row, col)}}"><img width="50px"  src="' + serverIPAdd + '{{grid.getCellValue(row, col)}}" alt=""></a></div>'
        },
        { name: "DOCTOR_CODE", displayName: "Doctor Code", visible: false },
        { name: "DOCTOR_NAME", displayName: "Doctor Name", width: 140 },
        { name: "Chemist Name", displayName: "Chemist Name", width: 130 },
        { name: "TOTAL_PROD", displayName: "Total Product", width: 110, type: "number" }
    ];


    $scope.gridReportMPOWisePrescription = {
        //showGridFooter: true,
        //showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnReportMPOWisePrescriptionList,
        //rowTemplate: rowTemplate(),
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: 'MPOWisePrescription.csv',
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
            $scope.gridApi.grid.registerDataChangeCallback(function () {
                $scope.gridApi.treeBase.expandAllRows();
            });
        }
    };

    $scope.showImage = function (row) {
        // $('#showImageModal').modal('show');
        $("#prescriptionImg").html(" ");
        //$scope.prescriptionImageSRC = row.entity.PRESCRIPTION_URL.replace("~", "");
        // $scope.prescriptionImageSRC = row.entity.PRESCRIPTION_URL;
        var curect_file_path = serverIPAdd + row.entity.PRESCRIPTION_URL.replace("~", "");
        $("#prescriptionImg").verySimpleImageViewer({
            imageSource: curect_file_path,
            frame: ['100%', '100%'],
            maxZoom: '900%',
            zoomFactor: '10%',
            saveZoomPos: false,
            mouse: true,
            keyboard: true,
            toolbar: true,
            rotateToolbar: true
        });
        $('#showImageModal').modal('show');
        //$('#prescriptionImg img').width(300); 
        //$('#prescriptionImg img').height(400);
        $('.jqvsiv_main_image_content>img').attr('id', 'image');
        //$('.jqvsiv_main_image_content>img').attr({
        //    width: 600,
        //    height: 400
        //});

    };
    $('#showImageModal').on('shown.bs.modal', function (event) {
        //var h = $('.jqvsiv_main_image_content img').attr('height', $('.jqvsiv_main_image_content img').height());
        //var w = $('.jqvsiv_main_image_content img').attr('width', $('.jqvsiv_main_image_content img').width());


        //var myImg = document.querySelector("#image");
        //var w = myImg.naturalWidth;
        //var h = myImg.naturalHeight;

        //alert(w + " " + h);
        $('#image').attr({
            width: 330,
            height: 440
        });
        $('#image').css("left", "135px");
    });
});

