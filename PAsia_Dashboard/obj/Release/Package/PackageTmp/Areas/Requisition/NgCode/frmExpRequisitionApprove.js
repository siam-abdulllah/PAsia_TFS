app.controller("ExpRequisitionApproveCtrl", function ($scope, $http, uiGridConstants) {
    $scope.EventPerm(45);
    $('legend.collapsable').parents("fieldset").find('.form-group').show();
    $('legend.collapsable').parents("fieldset").find('.row').show();
    $('legend.collapsable').parents("fieldset").removeAttr("style");
    if ($('legend.collapsable').children("i").hasClass('fa-plus-square-o')) {
        $('legend.collapsable').children("i").removeClass("fa-plus-square-o").addClass("fa-minus-square-o");
    }
    else if ($('legend.collapsable').children("i").hasClass('fa-minus-square-o')) {
        $('legend.collapsable').children("i").removeClass("fa-minus-square-o").addClass("fa-plus-square-o");
    }
    const monthNames = ["January", "February", "March", "April", "May", "June",
        "July", "August", "September", "October", "November", "December"
    ];
    $scope.btnSaveValue = "Save";
    $scope.labelPayToValue = "Pay To";
    var date = new Date();
    var toDay = new Date();
    //$scope.ExpenditureMonth = monthNames[date.getMonth()];
    toDay = ('0' + toDay.getDate()).slice(-2) + '/' + ('0' + (toDay.getMonth() + 1)).slice(-2) + '/' + toDay.getFullYear();
    $scope.ApprovedDate = toDay;
    $scope.isSaveDisable = false;
    var methodName = "";


    $scope.SaveData = function () {
        var expReqPrepareDtlData = $scope.gridExpReqApproveDtlOptions.data;
        if (expReqPrepareDtlData.length <= 0) {
            toastr.warning("Enter Requisition information properly!");
            return false;
        }
        $scope.SaveDb = {};
        $scope.SaveDb.MstId = $scope.Id;
        $scope.SaveDb.RequisitionNo = $scope.RequisitionNo;
        $scope.SaveDb.ApprovedDate = $scope.ApprovedDate;
        $scope.SaveDb.ApprovedRemarks = $scope.ApprovedRemarks;
        $scope.SaveDb.ApprovedStatus = $scope.ApprovedStatus;
        $scope.SaveDb.PrepareBy = $scope.PrepareBy;
        $scope.SaveDb.PrepareName = $scope.PrepareName;
        $scope.SaveDb.PrepareDesig = $scope.PrepareDesig;
        $scope.SaveDb.PrepareDate = $scope.PrepareDate;
        $scope.SaveDb.PrepareRemarks = $scope.PrepareRemarks;
        $scope.SaveDb.CheckedName = $scope.CheckedName;
        $scope.SaveDb.CheckedDesig = $scope.CheckedDesig;
        $scope.SaveDb.CheckedDate = $scope.CheckedDate;

        $scope.SaveDb.DivisionalName = $scope.DivisionalName;
        $scope.SaveDb.DivisionalDesig = $scope.DivisionalDesig;
        $scope.SaveDb.DivisionalDate = $scope.DivisionalDate;

        $scope.SaveDb.VerifiedName = $scope.VerifiedName;
        $scope.SaveDb.VerifiedDesig = $scope.VerifiedDesig;
        $scope.SaveDb.VerifiedDate = $scope.VerifiedDate;
        $scope.SaveDb.RecommendedDesig = $scope.RecommendedDesig;
        $scope.SaveDb.RecommendedName = $scope.RecommendedName;
        $scope.SaveDb.RecommendedDate = $scope.RecommendedDate;
        $scope.SaveDb.TotalApprovedAmt = $scope.gridExpReqApproveDtlApi.grid.columns[7].getAggregationValue();
        $http({
            method: "POST",
            url: MyApp.rootPath + "ExpRequisitionApprove/" + methodName,
            data: { expReqPrepareMstInfo: $scope.SaveDb, expReqPrepareDtlData: expReqPrepareDtlData }
        }).then(function (response) {
            if (response.data.Status === "Ok") {
                methodName = "UpdateExpReqApprovedInfo";
                toastr.success("Saved Successfully!", { timeOut: 2000 });
                $scope.isSubmitDisable = false;
            } else {
                console.log(response);
                toastr.warning(response.data.Status, { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Data!", { timeOut: 2000 });
            }
        });
    };

    $scope.GetExpReqMstList = function () {
        var param = "";
        $http({
            method: "POST",
            url: MyApp.rootPath + "ExpRequisitionApprove/GetExpReqMstList",
            data: { param: param }
        }).then(function (response) {
            if (response.data.Status === "Ok") {
                if (response.data.Data.length > 0) {
                    $scope.gridExpReqApproveMstOptions.data = response.data.Data;
                    $('#ExpReqApproveMstModal').modal('show');
                } else {
                    toastr.warning("No Data Found!", { timeOut: 2000 });
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
    $scope.GetExpApprovedReqMstList = function () {
        var param = "";
        $http({
            method: "POST",
            url: MyApp.rootPath + "ExpRequisitionApprove/GetExpApprovedReqMstList",
            data: { param: param }
        }).then(function (response) {
            if (response.data.length > 0) {
                $('#ExpReqApproveMstModal').modal('show');
                $scope.gridExpReqApproveMstOptions.data = response.data;
            } else {
                toastr.warning("No Data Found!", { timeOut: 2000 });
            }
        },
            function (response) {
                if (response.status === 404) {
                    toastr.warning("Error Loading Requisition Type!", { timeOut: 2000 });
                }
            });

    };
    $scope.GetExpReqDtlList = function () {

        $http({
            method: "POST",
            url: MyApp.rootPath + "ExpRequisitionApprove/GetExpReqDtlList",
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
        { name: 'PayToDesig', displayName: "PayToDesig", visible: false },
        { name: 'PaymentPlace', displayName: "Payment Place", visible: false },
        { name: 'PrepareBy', displayName: "PrepareBy", visible: false },
        { name: 'PrepareName', displayName: "PrepareName", visible: false },
        { name: 'PrepareDesig', displayName: "PrepareDesig", visible: false },
        { name: 'PrepareRemarks', displayName: "PrepareRemarks", visible: false },
        { name: 'PrepareDate', displayName: "Prepare Date" },
        { name: 'CheckedName', displayName: "CheckedName", visible: false },
        { name: 'CheckedDesig', displayName: "CheckedDesig", visible: false },
        { name: 'CheckedRemarks', displayName: "CheckedRemarks", visible: false },
        { name: 'CheckedDate', displayName: "Checked Date" },
        { name: 'PreparedByConfirm', displayName: "Confirm" },


        { name: 'DivisionalName', displayName: "Divisional Name" },
        { name: 'DivisionalDesig', displayName: "Divisional Desig" },
        { name: 'DivisionalDate', displayName: "Divisional Date" },
        { name: 'DivisionalStatus', displayName: "Divisional Status" },

        { name: 'VerifiedName', displayName: "VerifiedName", visible: false },
        { name: 'VerifiedDesig', displayName: "VerifiedDesig", visible: false },
        { name: 'VerifiedDate', displayName: "Verified Date", visible: false },
        { name: 'VerifiedRemarks', displayName: "VerifiedRemarks", visible: false },
        { name: 'RecommendedName', displayName: "RecommendedName", visible: false },
        { name: 'RecommendedDesig', displayName: "RecommendedDesig", visible: false },
        { name: 'RecommendedDate', displayName: "Recommended Date", visible: false },
        { name: 'RecommendedRemarks', displayName: "Recommended Remarks", visible: false },
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
        enableGridMenu: false,
        //enableSelectAll: true,
        exporterCsvFilename: 'Requisition_Status.csv',
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
        $scope.RequisitionNo = row.entity.RequisitionNo;
        $scope.ReqType = row.entity.ReqTypeName;
        if ($scope.ReqType == "Adjustment") {
            $scope.labelPayToValue = "Adjustment By";
        }
        else {
            $scope.labelPayToValue = "Pay To";
        }
        $scope.PayTo = row.entity.PayToName;
        $scope.PaymentPlace = row.entity.PaymentPlace;
        $scope.ExpenditureMonth = row.entity.ExpenditureMonth;
        $scope.PrepareBy = row.entity.PrepareBy;
        $scope.PrepareName = row.entity.PrepareName;
        $scope.PrepareDesig = row.entity.PrepareDesig;
        $scope.PrepareDate = row.entity.PrepareDate;
        $scope.PrepareRemarks = row.entity.PrepareRemarks;
        $scope.CheckedName = row.entity.CheckedName;
        $scope.CheckedDesig = row.entity.CheckedDesig;
        $scope.CheckedDate = row.entity.CheckedDate;
        $scope.CheckedRemarks = row.entity.CheckedRemarks;

        $scope.DivisionalName = row.entity.DivisionalName;
        $scope.DivisionalDesig = row.entity.DivisionalDesig;
        $scope.DivisionalDate = row.entity.DivisionalDate;

        $scope.VerifiedName = row.entity.VerifiedName;
        $scope.VerifiedDesig = row.entity.VerifiedDesig;
        $scope.VerifiedDate = row.entity.VerifiedDate;
        $scope.VerifiedRemarks = row.entity.VerifiedRemarks;
        $scope.RecommendedName = row.entity.RecommendedName;
        $scope.RecommendedDesig = row.entity.RecommendedDesig;
        $scope.RecommendedRemarks = row.entity.RecommendedRemarks;
        $scope.RecommendedDate = row.entity.RecommendedDate;
        if (row.entity.ApprovedStatus !== "Pending") {
            $scope.ApprovedDate = row.entity.ApprovedDate;
            $scope.ApprovedStatus = row.entity.ApprovedStatus;
            $scope.ApprovedRemarks = row.entity.ApprovedRemarks;
            methodName = "UpdateExpReqApprovedInfo";
        } else {
            $scope.ApprovedStatus = "";
            methodName = "InsertExpReqApprovedInfo";
        }
        //if (row.entity.ApprovedStatus === "Approved" || row.entity.ApprovedStatus === "Not Approved") {
        //    $scope.gridExpReqApproveDtlOptions.columnDefs[13].visible = false;
        //    $scope.isSaveDisable = true;
        //}
        //else {
        //    $scope.gridExpReqApproveDtlOptions.columnDefs[13].visible = true;
        //    $scope.isSaveDisable = false;
        //}
        $scope.GetExpReqDtlList();
        $('#ExpReqApproveMstModal').modal('hide');
    };
    var columnExpReqApproveDtlInfo = [
        { name: 'DtlId', displayName: "DtlId", visible: false },
        { name: 'Mop', displayName: "Mode", aggregationType: uiGridConstants.aggregationTypes.count },
        { name: 'Purpose', displayName: "Purpose" },
        { name: 'PrepareValue', displayName: "Amount", cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2' },
        { name: 'CheckedValue', displayName: "Checked Amount", cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2' },
        { name: 'DivisionalValue', displayName: "Divisional Amount", cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2' },
        { name: 'VerifiedValue', displayName: "Verified Amount", cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2' },
        { name: 'RecommendedValue', displayName: "Recommended Amount", cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2' },
        { name: 'ApprovedValue', displayName: "Approved Amount", cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2' },
        { name: 'FromDate', displayName: "From Date" },
        { name: 'ToDate', displayName: "To Date" },
        { name: 'RequiredDate', displayName: "Required Date" },
        { name: 'TotalDays', displayName: "Total Day" },
        { name: 'Remarks', displayName: "Remarks" },
        {
            name: 'Action', enableFiltering: false, enableSorting: false,
            cellTemplate: '<div style="text-align:center;position: relative;width:100%;padding:2px 2px 2px 6px;"><button  class="btn-success" ng-click="grid.appScope.editExpReqApproveDtlOptionsRow(row)"><i class="fa fa-edit"></i>&nbspEdit</button></div>'
        }
    ];
    $scope.gridExpReqApproveDtlOptions = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnExpReqApproveDtlInfo,
        enableGridMenu: false,
        enableSelectAll: true,
        onRegisterApi: function (gridApi) {
            $scope.gridExpReqApproveDtlApi = gridApi;
        }
    };
    $scope.showPopUpExpReqApproveDtlOptions = function () {
        $scope.DtlId = "";
        $scope.Mop = "";
        $scope.Purpose = "";
        $scope.PrepareValue = "";
        $scope.CheckedValue = "";
        $scope.DivisionalValue = "";
        $scope.VerifiedValue = "";
        $scope.RecommendedValue = "";
        $scope.ApprovedValue = "";
        $scope.FromDate = "";
        $scope.ToDate = "";
        $scope.RequiredDate = "";
        $scope.Remarks = "";
        $scope.TotalDays = "";
        index = null;
        $('#ExpReqApproveDtlModal').modal('show');
    };
    $scope.editExpReqApproveDtlOptionsRow = function (row) {
        $scope.DtlId = row.entity.DtlId;
        $scope.Mop = row.entity.Mop;
        $scope.Purpose = row.entity.Purpose;
        $scope.PrepareValue = row.entity.PrepareValue;
        $scope.CheckedValue = row.entity.CheckedValue;
        $scope.DivisionalValue = row.entity.DivisionalValue;
        $scope.VerifiedValue = row.entity.VerifiedValue;
        $scope.RecommendedValue = row.entity.RecommendedValue;
        $scope.ApprovedValue = row.entity.ApprovedValue;
        $scope.FromDate = row.entity.FromDate;
        $scope.ToDate = row.entity.ToDate;
        $scope.RequiredDate = row.entity.RequiredDate;
        $scope.Remarks = row.entity.Remarks;
        $scope.TotalDays = row.entity.TotalDays;
        index = $scope.gridExpReqApproveDtlOptions.data.indexOf(row.entity);
        $('#ExpReqApproveDtlModal').modal('show');
    };

    $scope.addItemGridExpReqApproveDtlOptions = function () {
        if (index !== null) {
            $scope.gridExpReqApproveDtlOptions.data.splice(index, 1);
        }
        $scope.gridExpReqApproveDtlOptions.data.push({ DtlId: $scope.DtlId, Mop: $scope.Mop, Purpose: $scope.Purpose, PrepareValue: $scope.PrepareValue, CheckedValue: $scope.CheckedValue, VerifiedValue: $scope.VerifiedValue, RecommendedValue: $scope.RecommendedValue, ApprovedValue: $scope.ApprovedValue, FromDate: $scope.FromDate, ToDate: $scope.ToDate, RequiredDate: $scope.RequiredDate, TotalDays: $scope.TotalDays, Remarks: $scope.Remarks });
        $scope.DtlId = "";
        $scope.Mop = "";
        $scope.Purpose = "";
        $scope.PrepareValue = "";
        $scope.CheckedValue = "";
        $scope.DivisionalValue = "";
        $scope.VerifiedValue = "";
        $scope.RecommendedValue = "";
        $scope.ApprovedValue = "";
        $scope.FromDate = "";
        $scope.ToDate = "";
        $scope.RequiredDate = "";
        $scope.TotalDays = "";
        $scope.Remarks = "";
        index = null;
        $('#ExpReqApproveDtlModal').modal('hide');
    };
    //----------- Reset ----------------//

    $scope.Reset = function () {
        methodName = "";
        $scope.Id = "";
        $scope.PrepareBy = "";
        $scope.RequisitionNo = "";
        $scope.PrepareDate = "";
        $scope.PrepareRemarks = "";
        $scope.CheckedDate = "";
        $scope.DivisionalDate = "";
        $scope.CheckedRemarks = "";
        $scope.VerifiedDate = "";
        $scope.VerifiedRemarks = "";
        $scope.RecommendedDate = "";
        $scope.RecommendedRemarks = "";
        $scope.ReqType = "";
        $scope.labelPayToValue = "Pay To";
        $scope.ApprovedDate = toDay;
        $scope.ExpenditureMonth = "";
        $scope.PayTo = "";
        $scope.ApprovedStatus = "";
        $scope.ApprovedRemarks = "";
        $scope.PaymentPlace = "";
        $scope.gridExpReqApproveDtlOptions.data = [];
        $scope.gridExpReqApproveDtlOptions.columnDefs[13].visible = true;
        $scope.isSaveDisable = false;
        $scope.PrepareRemarks = "";
    };
});