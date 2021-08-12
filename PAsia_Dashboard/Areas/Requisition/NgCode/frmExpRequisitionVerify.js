app.controller("ExpRequisitionVerifyCtrl", function ($scope, $http, uiGridConstants) {
    $scope.EventPerm(43);
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
    $scope.VerifiedDate = toDay;
    $scope.isSaveDisable = false;
    var methodName = "";


    $scope.SaveData = function () {
        var expReqPrepareDtlData = $scope.gridExpReqVerifyDtlOptions.data;
        if (expReqPrepareDtlData.length <= 0) {
            toastr.warning("Enter Requisition information properly!");
            return false;
        }
        $scope.SaveDb = {};
        $scope.SaveDb.MstId = $scope.Id;
        $scope.SaveDb.RequisitionNo = $scope.RequisitionNo;
        $scope.SaveDb.VerifiedDate = $scope.VerifiedDate;
        $scope.SaveDb.VerifiedRemarks = $scope.VerifiedRemarks;
        $scope.SaveDb.VerifiedStatus = $scope.VerifiedStatus;
        $scope.SaveDb.PrepareName = $scope.PrepareName;
        $scope.SaveDb.PrepareDesig = $scope.PrepareDesig;
        $scope.SaveDb.PrepareDate = $scope.PrepareDate;
        $scope.SaveDb.PrepareRemarks = $scope.PrepareRemarks;
        $scope.SaveDb.CheckedName = $scope.CheckedName;
        $scope.SaveDb.CheckedDesig = $scope.CheckedDesig;
        $scope.SaveDb.CheckedDate = $scope.CheckedDate;
        $scope.SaveDb.CheckedDate = $scope.CheckedDate;
        $scope.SaveDb.TotalApprovedAmt = $scope.gridExpReqVerifyDtlApi.grid.columns[5].getAggregationValue();
        $http({
            method: "POST",
            url: MyApp.rootPath + "ExpRequisitionVerify/" + methodName,
            data: { expReqPrepareMstInfo: $scope.SaveDb, expReqPrepareDtlData: expReqPrepareDtlData }
        }).then(function (response) {
            if (response.data.Status === "Ok") {
                methodName = "UpdateExpReqVerifiedInfo";
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
            url: MyApp.rootPath + "ExpRequisitionVerify/GetExpReqMstList",
            data: { param: param }
        }).then(function (response) {
            if (response.data.Status === "Ok") {
                if (response.data.Data.length > 0) {
                    $scope.gridExpReqVerifyMstOptions.data = response.data.Data;
                    $('#ExpReqVerifyMstModal').modal('show');
                } else {
                    toastr.warning("No Data Found!", { timeOut: 2000 });
                }
            } else {
                toastr.warning(response.data.Status, { timeOut: 2000 });
            }
        },
            function (response) {
                if (response.status === 404) {
                   // alert(response.data);
                    toastr.warning("Error Loading Requisition Type!", { timeOut: 2000 });
                }
            });

    };
    $scope.GetExpVerifiedReqMstList = function () {
        var param = "";
        $http({
            method: "POST",
            url: MyApp.rootPath + "ExpRequisitionVerify/GetExpVerifiedReqMstList",
            data: { param: param }
        }).then(function (response) {
            if (response.data.Status === null || response.data.Status === undefined) {
                if (response.data.length > 0) {
                    $('#ExpReqVerifyMstModal').modal('show');
                    $scope.gridExpReqVerifyMstOptions.data = response.data;
                } else {
                    toastr.warning("No Data Found!", { timeOut: 2000 });
                }
            } else {
                toastr.warning(response.data.Status, { timeOut: 2000 });
            }
        },
            function (response) {
                alert(response.data);
                if (response.status === 404) {
                    toastr.warning("Error Loading Requisition Type!", { timeOut: 2000 });
                }
            });

    };
    $scope.GetExpReqDtlList = function () {

        $http({
            method: "POST",
            url: MyApp.rootPath + "ExpRequisitionVerify/GetExpReqDtlList",
            data: { mstId: $scope.Id }
        }).then(function (response) {
            if (response.data.Status === "Ok") {
                if (response.data.Data.length > 0) {
                    $scope.gridExpReqVerifyDtlOptions.data = response.data.Data;
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

    var columnExpReqVerifyMstInfo = [
        { name: 'MstId', displayName: "MstId", visible: false },
        { name: 'RequisitionNo', displayName: "Requisition No" },
        //{ name: 'RequisitionDate', displayName: "Requisition Date" },
        { name: 'ReqTypeName', displayName: "Requisition Type" },
        { name: 'ExpenditureMonth', displayName: "Expenditure Month" },
        { name: 'PayToName', displayName: "Pay To" },
        { name: 'PayToDesig', displayName: "PayToDesig", visible: false },
        { name: 'PaymentPlace', displayName: "Payment Place", visible: false },
        { name: 'PrepareName', displayName: "PrepareName", visible: false },
        { name: 'PrepareDesig', displayName: "PrepareDesig", visible: false },
        { name: 'PrepareRemarks', displayName: "PrepareRemarks", visible: false },
        { name: 'PrepareDate', displayName: "Prepare Date" },
        { name: 'CheckedName', displayName: "CheckedName", visible: false },
        { name: 'CheckedDesig', displayName: "CheckedDesig", visible: false },
        { name: 'CheckedDate', displayName: "Checked Date" },
        { name: 'PreparedByConfirm', displayName: "Confirm" },
        { name: 'VerifiedStatus', displayName: "Verified Status" },
        { name: 'VerifiedDate', displayName: "Verified Date", visible: false },
        { name: 'VerifiedRemarks', displayName: "Verified Remarks", visible: false },
        { name: 'RecommendedStatus', displayName: "Recommended Status" },
        { name: 'RecommendedDate', displayName: "Recommended Date", visible: false },
        { name: 'ApprovedStatus', displayName: "Approved Status" },
        { name: 'ApprovedDate', displayName: "Approved Date", visible: false }
    ];
    $scope.gridExpReqVerifyMstOptions = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnExpReqVerifyMstInfo,
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
        $scope.PrepareName = row.entity.PrepareName;
        $scope.PrepareDesig = row.entity.PrepareDesig;
        $scope.PrepareDate = row.entity.PrepareDate;
        $scope.PrepareRemarks = row.entity.PrepareRemarks;
        $scope.CheckedName = row.entity.CheckedName;
        $scope.CheckedDesig = row.entity.CheckedDesig;
        $scope.CheckedDate = row.entity.CheckedDate;
        if (row.entity.VerifiedStatus !== "Pending") {
            $scope.VerifiedDate = row.entity.VerifiedDate;
            $scope.VerifiedStatus = row.entity.VerifiedStatus;
            $scope.VerifiedRemarks = row.entity.VerifiedRemarks;
            methodName = "UpdateExpReqVerifiedInfo";
        } else {
            $scope.VerifiedStatus = "";
            methodName = "InsertExpReqVerifiedInfo";
        }
        if (row.entity.RecommendedStatus === "Approved" || row.entity.RecommendedStatus === "Not Approved") {
            $scope.gridExpReqVerifyDtlOptions.columnDefs[11].visible = false;
            $scope.isSaveDisable = true;
        }
        else {
            $scope.gridExpReqVerifyDtlOptions.columnDefs[11].visible = true;
            $scope.isSaveDisable = false;
        }
        $scope.GetExpReqDtlList();
        $('#ExpReqVerifyMstModal').modal('hide');
    };
    var columnExpReqVerifyDtlInfo = [
        { name: 'DtlId', displayName: "DtlId", visible: false },
        { name: 'Mop', displayName: "Mode", aggregationType: uiGridConstants.aggregationTypes.count },
        { name: 'Purpose', displayName: "Purpose" },
        { name: 'PrepareValue', displayName: "Amount", cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2' },
        { name: 'CheckedValue', displayName: "Checked Amount", cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2' },
        { name: 'VerifiedValue', displayName: "Verified Amount", cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2' },
        { name: 'FromDate', displayName: "From Date" },
        { name: 'ToDate', displayName: "To Date" },
        { name: 'RequiredDate', displayName: "Required Date" },
        { name: 'TotalDays', displayName: "Total Day" },
        { name: 'Remarks', displayName: "Remarks" },
        {
            name: 'Action', enableFiltering: false, enableSorting: false,
            cellTemplate: '<div style="text-align:center;position: relative;width:100%;padding:2px 2px 2px 6px;"><button  class="btn-success" ng-click="grid.appScope.editExpReqVerifyDtlOptionsRow(row)"><i class="fa fa-edit"></i>&nbspEdit</button></div>'
        }
    ];
    $scope.gridExpReqVerifyDtlOptions = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnExpReqVerifyDtlInfo,
        enableGridMenu: false,
        enableSelectAll: true,
        onRegisterApi: function (gridApi) {
            $scope.gridExpReqVerifyDtlApi = gridApi;
        }
    };

    $scope.showPopUpExpReqVerifyDtlOptions = function () {
        $scope.DtlId = "";
        $scope.Mop = "";
        $scope.Purpose = "";
        $scope.PrepareValue = "";
        $scope.CheckedValue = "";
        $scope.VerifiedValue = "";
        $scope.FromDate = "";
        $scope.ToDate = "";
        $scope.RequiredDate = "";
        $scope.Remarks = "";
        $scope.TotalDays = "";
        index = null;
        $('#ExpReqVerifyDtlModal').modal('show');
    };
    $scope.editExpReqVerifyDtlOptionsRow = function (row) {
        $scope.DtlId = row.entity.DtlId;
        $scope.Mop = row.entity.Mop;
        $scope.Purpose = row.entity.Purpose;
        $scope.PrepareValue = row.entity.PrepareValue;
        $scope.CheckedValue = row.entity.CheckedValue;
        $scope.VerifiedValue = row.entity.VerifiedValue;
        $scope.FromDate = row.entity.FromDate;
        $scope.ToDate = row.entity.ToDate;
        $scope.RequiredDate = row.entity.RequiredDate;
        $scope.Remarks = row.entity.Remarks;
        $scope.TotalDays = row.entity.TotalDays;
        index = $scope.gridExpReqVerifyDtlOptions.data.indexOf(row.entity);
        $('#ExpReqVerifyDtlModal').modal('show');
    };

    $scope.addItemGridExpReqVerifyDtlOptions = function () {
        if (index !== null) {
            $scope.gridExpReqVerifyDtlOptions.data.splice(index, 1);
        }
        $scope.gridExpReqVerifyDtlOptions.data.push({ DtlId: $scope.DtlId, Mop: $scope.Mop, Purpose: $scope.Purpose, PrepareValue: $scope.PrepareValue, CheckedValue: $scope.CheckedValue,VerifiedValue: $scope.VerifiedValue, FromDate: $scope.FromDate, ToDate: $scope.ToDate, RequiredDate: $scope.RequiredDate, TotalDays: $scope.TotalDays, Remarks: $scope.Remarks });
        $scope.DtlId = "";
        $scope.Mop = "";
        $scope.Purpose = "";
        $scope.PrepareValue = "";
        $scope.CheckedValue = "";
        $scope.VerifiedValue = "";
        $scope.FromDate = "";
        $scope.ToDate = "";
        $scope.RequiredDate = "";
        $scope.TotalDays = "";
        $scope.Remarks = "";
        index = null;
        $('#ExpReqVerifyDtlModal').modal('hide');
    };
    //----------- Reset ----------------//

    $scope.Reset = function () {
        methodName = "";
        $scope.Id = "";
        $scope.RequisitionNo = "";
        $scope.PrepareDate = "";
        $scope.CheckedDate = "";
        $scope.ReqType = "";
        $scope.VerifiedDate = toDay;
        $scope.ExpenditureMonth = "";
        $scope.VerifiedStatus = "";
        $scope.VerifiedRemarks = "";
        $scope.PaymentPlace = "";
        $scope.gridExpReqVerifyDtlOptions.data = [];
        $scope.gridExpReqVerifyDtlOptions.columnDefs[11].visible = true;
        $scope.isSaveDisable = false;
        $scope.PrepareRemarks = "";
        $scope.labelPayToValue = "Pay To";
    };
});