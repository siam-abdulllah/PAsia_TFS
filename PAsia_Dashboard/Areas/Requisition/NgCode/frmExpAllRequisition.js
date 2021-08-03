app.controller("ExpAllRequisitionCtrl", function ($scope, $http, uiGridConstants) {
    $scope.EventPerm(46);
    
    $('legend.uncollapsed').parents("fieldset").find('.form-group').show();
    $('legend.uncollapsed').parents("fieldset").find('.row').show();
    $('legend.uncollapsed').parents("fieldset").removeAttr("style");
    if ($('legend.uncollapsed').children("i").hasClass('fa-plus-square-o')) {
        $('legend.uncollapsed').children("i").removeClass("fa-plus-square-o").addClass("fa-minus-square-o");
    }
    else if ($('legend.uncollapsed').children("i").hasClass('fa-minus-square-o')) {
        $('legend.uncollapsed').children("i").removeClass("fa-minus-square-o").addClass("fa-plus-square-o");
    }
    $scope.GetExpApprovedReqMstList = function () {
        var param = "AND TO_DATE(PREPARED_DATE,'dd/MM/YYYY') BETWEEN TO_DATE('" + $scope.FromDate + "','dd/MM/YYYY') AND TO_DATE('" + $scope.ToDate + "','dd/MM/YYYY')";
        $http({
            method: "POST",
            url: MyApp.rootPath + "ExpAllRequisition/GetExpAllReqMstList",
            data: { param: param }
        }).then(function (response) {
            if (response.data.Status === "Ok") {
                if (response.data.Data.length > 0) {
                    $scope.gridExpReqApproveMstOptions.data = response.data.Data;
                } else {
                    toastr.warning("No Data Found!", { timeOut: 2000 });
                }
            } else {
                toastr.warning(response.data.Status, { timeOut: 2000 });
            }
        },
            function (response) {
                if (response.status === 404) {
                    toastr.warning("Error Loading!", { timeOut: 2000 });
                }
            });

    };
    $scope.GetExpReqDtlList = function () {
        $http({
            method: "POST",
            url: MyApp.rootPath + "ExpAllRequisition/GetExpReqDtlList",
            data: { mstId: $scope.Id }
        }).then(function (response) {
            if (response.data.Status === "Ok") {
                if (response.data.Data.length > 0) {
                    $scope.gridExpReqApproveDtlOptions.data = response.data.Data;
                }
            } else {
                toastr.warning(response.data.Status, { timeOut: 2000 });
            }
        },
            function (response) {
                if (response.status === 404) {
                    toastr.warning("Error Loading Requisition Type!", { timeOut: 2000 });
                }
            });

    };
    //---------------- Grid Start -------------//

    var columnExpReqApproveMstInfo = [
        { name: 'MstId', displayName: "MstId", visible: false },
        { name: 'RequisitionNo', displayName: "Requisition No" },
        //{ name: 'RequisitionDate', displayName: "Requisition Date" },
        { name: 'ReqTypeName', displayName: "Requisition Type" },
        { name: 'ExpenditureMonth', displayName: "Expenditure Month" },
        { name: 'PayToName', displayName: "Pay To" },
        { name: 'AdjustmentByName', displayName: "Adjustment By" },
        { name: 'PayToDesig', displayName: "PayToDesig", visible: false },
        { name: 'PaymentPlace', displayName: "Payment Place", visible: false },
        { name: 'PrepareName', displayName: "PrepareName", visible: false },
        { name: 'PrepareDesig', displayName: "PrepareDesig", visible: false },
        { name: 'PrepareDate', displayName: "Prepare Date" },
        { name: 'CheckedName', displayName: "CheckedName", visible: false },
        { name: 'CheckedDesig', displayName: "CheckedDesig", visible: false },
        { name: 'CheckedStatus', displayName: "Che. Status" },
        { name: 'CheckedDate', displayName: "Che. Date" },
        { name: 'PreparedByConfirm', displayName: "Confirm" },
        { name: 'VerifiedName', displayName: "VerifiedName", visible: false },
        { name: 'VerifiedDesig', displayName: "VerifiedDesig", visible: false },
        { name: 'VerifiedStatus', displayName: "Ver. Status" },
        { name: 'VerifiedDate', displayName: "Ver. Date" },
        { name: 'RecommendedName', displayName: "RecommendedName", visible: false },
        { name: 'RecommendedDesig', displayName: "RecommendedDesig", visible: false },
        { name: 'RecommendedStatus', displayName: "Recommended Status" },
        { name: 'RecommendedDate', displayName: "Rec. Date" },
        { name: 'ApprovedName', displayName: "ApprovedName", visible: false },
        { name: 'ApprovedDesig', displayName: "ApprovedDesig", visible: false },
        { name: 'ApprovedStatus', displayName: "Approved Status" },
        { name: 'ApprovedDate', displayName: "Approved Date", visible: false },
        { name: 'ApprovedRemarks', displayName: "Approved Remarks", visible: false }
    ];
    $scope.gridExpReqApproveMstOptions = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnExpReqApproveMstInfo,
        rowTemplate: rowTemplateMst(),
        enableGridMenu: true,
        //enableSelectAll: true,
        exporterCsvFilename: 'Requisitions.csv',
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridExpReqPrepareMstApi = gridApi;
        }
    };
    function rowTemplateMst() {
        return '<div ng-dblclick="grid.appScope.rowDblClickMst(row)" >' +
            '  <div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }"  ui-grid-cell></div></div>';
    }
    $scope.rowDblClickMst = function (row) {
        $scope.Id = row.entity.MstId;
        $scope.PrepareName = row.entity.PrepareName;
        $scope.PrepareDesig = row.entity.PrepareDesig;
        $scope.PrepareDate = row.entity.PrepareDate;
        $scope.CheckedName = row.entity.CheckedName;
        $scope.CheckedDesig = row.entity.CheckedDesig;
        $scope.CheckedDate = row.entity.CheckedDate;
        $scope.VerifiedName = row.entity.VerifiedName;
        $scope.VerifiedDesig = row.entity.VerifiedDesig;
        $scope.VerifiedDate = row.entity.VerifiedDate;
        $scope.RecommendedName = row.entity.RecommendedName;
        $scope.RecommendedDesig = row.entity.RecommendedDesig;
        $scope.RecommendedDate = row.entity.RecommendedDate;
        $scope.ApprovedName = row.entity.ApprovedName;
        $scope.ApprovedDesig = row.entity.ApprovedDesig;
        $scope.ApprovedDate = row.entity.ApprovedDate;
        $scope.ApprovedStatus = row.entity.ApprovedStatus;
        $scope.ApprovedRemarks = row.entity.ApprovedRemarks;


        $scope.GetExpReqDtlList();

    };
    var columnExpReqApproveDtlInfo = [
        { name: 'DtlId', displayName: "DtlId", visible: false },
        { name: 'Mop', displayName: "Mode", aggregationType: uiGridConstants.aggregationTypes.count },
        { name: 'Purpose', displayName: "Purpose" },
        { name: 'PrepareValue', displayName: "Amount", cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2' },
        { name: 'CheckedValue', displayName: "Checked Amount", cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2' },
        { name: 'VerifiedValue', displayName: "Verified Amount", cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2' },
        { name: 'RecommendedValue', displayName: "Recommended Amount", cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2' },
        { name: 'ApprovedValue', displayName: "Approved Amount", cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2' },
        { name: 'FromDate', displayName: "From Date" },
        { name: 'ToDate', displayName: "To Date" },
        { name: 'RequiredDate', displayName: "Required Date" },
        { name: 'TotalDays', displayName: "Total Day" },
        { name: 'Remarks', displayName: "Remarks" }
    ];
    $scope.gridExpReqApproveDtlOptions = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnExpReqApproveDtlInfo,
        enableGridMenu: true,
        exporterCsvFilename: 'Requisitions_Detail.csv',
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridExpReqApproveDtlApi = gridApi;
        }
    };

    //----------- Reset ----------------//

    $scope.Reset = function () {
        $scope.FromDate = "";
        $scope.ToDate = "";
        $scope.PrepareDate = "";
        $scope.CheckedDate = "";
        $scope.VerifiedDate = "";
        $scope.RecommendedDate = "";
        $scope.ApprovedDate = "";
        $scope.gridExpReqApproveMstOptions.data = [];
        $scope.gridExpReqApproveDtlOptions.data = [];
        $scope.isSaveDisable = false;
    };
});