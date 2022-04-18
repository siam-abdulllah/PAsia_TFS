app.controller("ExpRequisitionDivisionalCtrl", function ($scope, $http, uiGridConstants) {
    $scope.EventPerm(56);
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
    $scope.DivisionalDate = toDay;
    $scope.isSaveDisable = false;
    var methodName = "";
    $scope.SaveData = function () {
        var expReqPrepareDtlData = $scope.gridExpReqDivisionalDtlOptions.data;
        if (expReqPrepareDtlData.length <= 0) {
            toastr.warning("Enter Requisition information properly!");
            return false;
        }
        $scope.SaveDb = {};
        $scope.SaveDb.MstId = $scope.Id;
        $scope.SaveDb.RequisitionNo = $scope.RequisitionNo;
        $scope.SaveDb.PreparedByConfirm = $scope.PreparedByConfirm;

        $scope.SaveDb.CheckedDate = $scope.CheckedDate;
        $scope.SaveDb.CheckeedRemarks = $scope.CheckedRemarks;
        $scope.SaveDb.CheckedStatus = $scope.CheckedStatus;


        $scope.SaveDb.DivisionalDate = $scope.DivisionalDate;
        $scope.SaveDb.DivisionalRemarks = $scope.DivisionalRemarks;
        $scope.SaveDb.DivisionalStatus = $scope.DivisionalStatus;


        $scope.SaveDb.PrepareName = $scope.PrepareName;
        $scope.SaveDb.PrepareDesig = $scope.PrepareDesig;
        $scope.SaveDb.PrepareDate = $scope.PrepareDate;
        $scope.SaveDb.PrepareRemarks = $scope.PrepareRemarks;
        $scope.SaveDb.TotalApprovedAmt = $scope.gridExpReqDivisionalDtlApi.grid.columns[4].getAggregationValue();
        $http({
            method: "POST",
            url: MyApp.rootPath + "ExpRequisitionDivisional/" + methodName,
            data: { expReqPrepareMstInfo: $scope.SaveDb, expReqPrepareDtlData: expReqPrepareDtlData }
        }).then(function (response) {
            if (response.data.Status === "Ok") {
                methodName = "UpdateExpReqDivisionalInfo";
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
            url: MyApp.rootPath + "ExpRequisitionDivisional/GetExpReqMstList",
            data: { param: param }
        }).then(function (response) {
            if (response.data.Status === "Ok") {
                if (response.data.Data.length > 0) {
                    $scope.gridExpReqDivisionalMstOptions.data = response.data.Data;
                    $('#ExpReqDivisionalMstModal').modal('show');
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
    $scope.GetExpDivisionalReqMstList = function () {
        var param = "";
        $http({
            method: "POST",
            url: MyApp.rootPath + "ExpRequisitionDivisional/GetExpDivisionalReqMstList",
            data: { param: param }
        }).then(function (response) {
            if (response.data.length > 0) {
                $('#ExpReqDivisionalMstModal').modal('show');
                $scope.gridExpReqDivisionalMstOptions.data = response.data;
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
            url: MyApp.rootPath + "ExpRequisitionDivisional/GetExpReqDtlList",
            data: { mstId: $scope.Id }
        }).then(function (response) {
            if (response.data.Status === "Ok") {
                if (response.data.Data.length > 0) {
                    $scope.gridExpReqDivisionalDtlOptions.data = response.data.Data;
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

    var columnExpReqDivisionalMstInfo = [
        { name: 'MstId', displayName: "MstId", visible: false },
        { name: 'RequisitionNo', displayName: "Requisition No" },
        { name: 'ReqTypeName', displayName: "Requisition Type" },
        { name: 'ExpenditureMonth', displayName: "Expenditure Month" },
        { name: 'PayToName', displayName: "Pay To" },
        { name: 'PayToDesig', displayName: "PayToDesig", visible: false },
        { name: 'PaymentPlace', displayName: "Payment Place", visible: false },
        { name: 'PrepareRemarks', displayName: "Prepare Remarks", visible: false },
        { name: 'PrepareName', displayName: "PrepareName", visible: false },
        { name: 'PrepareDesig', displayName: "PrepareDesig", visible: false },
        { name: 'PrepareDate', displayName: "Prepare Date" },
        { name: 'PreparedByConfirm', displayName: "Confirm" },

        { name: 'CheckedStatus', displayName: "Checked Status" },
        { name: 'CheckedRemarks', displayName: "Checked Remarks", visible: false },
        { name: 'CheckedDate', displayName: "Checked Date", visible: false },

        { name: 'DivisionalStatus', displayName: "Forwarded Status" },
        { name: 'DivisionalRemarks', displayName: "Forwarded Remarks", visible: false },
        { name: 'DivisionalDate', displayName: "Forwarded Date", visible: false },

        { name: 'VerifiedStatus', displayName: "Verified Status" },
        { name: 'VerifiedDate', displayName: "Verified Date", visible: false },
        { name: 'RecommendedStatus', displayName: "Recommended Status" },
        { name: 'RecommendedDate', displayName: "Recommended Date", visible: false },
        { name: 'ApprovedStatus', displayName: "Approved Status" },
        { name: 'ApprovedDate', displayName: "Approved Date", visible: false }
    ];
    $scope.gridExpReqDivisionalMstOptions = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnExpReqDivisionalMstInfo,
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
        if ($scope.ReqType === "Adjustment") {
            $scope.labelPayToValue = "Adjustment By";
        }
        else {
            $scope.labelPayToValue = "Pay To";
        }
        $scope.PayTo = row.entity.PayToName;
        $scope.PaymentPlace = row.entity.PaymentPlace;
        $scope.PreparedByConfirm = row.entity.PreparedByConfirm;
        $scope.ExpenditureMonth = row.entity.ExpenditureMonth;

        $scope.PrepareRemarks = row.entity.PrepareRemarks;
        $scope.PrepareName = row.entity.PrepareName;
        $scope.PrepareDesig = row.entity.PrepareDesig;
        $scope.PrepareDate = row.entity.PrepareDate;

        $scope.CheckedName = row.entity.CheckedName;
        $scope.CheckedDesig = row.entity.CheckedDesig;
        $scope.CheckedDate = row.entity.CheckedDate;


        if (row.entity.DivisionalStatus !== "Pending") {
            $scope.DivisionalDate = row.entity.DivisionalDate;
            $scope.DivisionalStatus = row.entity.DivisionalStatus;
            $scope.DivisionalRemarks = row.entity.DivisionalRemarks;
            methodName = "UpdateExpReqDivisionalInfo";
        } else {
            $scope.DivisionalStatus = "";
            methodName = "InsertExpReqDivisionalInfo";
        }
        if (row.entity.VerifiedStatus === "Approved" || row.entity.VerifiedStatus === "Not Approved") {

            $scope.DivisionalStatus = row.entity.DivisionalStatus;
            $scope.gridExpReqDivisionalDtlOptions.columnDefs[10].visible = false;
            $scope.isSaveDisable = true;
        }
        else {
            $scope.gridExpReqDivisionalDtlOptions.columnDefs[10].visible = true;
            $scope.isSaveDisable = false;
        }
        $scope.GetExpReqDtlList();
        $('#ExpReqDivisionalMstModal').modal('hide');
    };


    var columnExpReqDivisionalDtlInfo = [
        { name: 'DtlId', displayName: "DtlId", visible: false },
        { name: 'Mop', displayName: "Mode", aggregationType: uiGridConstants.aggregationTypes.count },
        { name: 'Purpose', displayName: "Purpose" },
        { name: 'PrepareValue', displayName: "Amount", cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2' },
        { name: 'CheckedValue', displayName: "Checked Amount", cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2' },
        { name: 'DivisionalValue', displayName: "Forwarded Amount", cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2' },
        { name: 'FromDate', displayName: "From Date" },
        { name: 'ToDate', displayName: "To Date" },
        { name: 'RequiredDate', displayName: "Required Date" },
        { name: 'TotalDays', displayName: "Total Day" },
        { name: 'Remarks', displayName: "Remarks" },
        {
            name: 'Action', enableFiltering: false, enableSorting: false,
            cellTemplate: '<div style="text-align:center;position: relative;width:100%;padding:2px 2px 2px 6px;"><button  class="btn-success" ng-click="grid.appScope.editExpReqCheckDtlOptionsRow(row)"><i class="fa fa-edit"></i>&nbspEdit</button></div>'
        }
    ];
    $scope.gridExpReqDivisionalDtlOptions = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnExpReqDivisionalDtlInfo,
        //rowTemplate: rowTemplateDtl(),
        enableGridMenu: false,
        enableSelectAll: true,
        //exporterCsvFilename: 'ProductInfo.csv',
        //exporterMenuPdf: false,
        //exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridExpReqDivisionalDtlApi = gridApi;
        }
    };
    var counter = 0;
    $scope.showPopUpExpReqDivisionalDtlOptions = function () {
        if (counter === 0) { $scope.LegendCollapse(); }
        counter = 1;
        $scope.DtlId = "";
        $scope.Mop = "";
        $scope.Purpose = "";
        $scope.PrepareValue = "";
        $scope.CheckedValue = "";
        $scope.DivisionalValue = "";
        $scope.FromDate = "";
        $scope.ToDate = "";
        $scope.RequiredDate = "";
        $scope.Remarks = "";
        $scope.TotalDays = "";
        index = null;
        $('#ExpReqDivisionalDtlModal').modal('show');
    };
    $scope.editExpReqDivisionalDtlOptionsRow = function (row) {
        $scope.DtlId = row.entity.DtlId;
        $scope.Mop = row.entity.Mop;
        $scope.Purpose = row.entity.Purpose;
        $scope.PrepareValue = row.entity.PrepareValue;
        $scope.CheckedValue = row.entity.CheckedValue;
        $scope.DivisionalValue = row.entity.DivisionalValue;
        $scope.FromDate = row.entity.FromDate;
        $scope.ToDate = row.entity.ToDate;
        $scope.RequiredDate = row.entity.RequiredDate;
        $scope.Remarks = row.entity.Remarks;
        $scope.TotalDays = row.entity.TotalDays;
        index = $scope.gridExpDivisionalDtlOptions.data.indexOf(row.entity);
        $('#ExpReqDivisionalDtlModal').modal('show');
    };

    $scope.addItemGridExpReqDivisionalDtlOptions = function () {
        if (index !== null) {
            $scope.gridExpReqDivisionalDtlOptions.data.splice(index, 1);
        }
        $scope.gridExpReqDivisionalDtlOptions.data.push({ DtlId: $scope.DtlId, Mop: $scope.Mop, Purpose: $scope.Purpose, PrepareValue: $scope.PrepareValue, CheckedValue: $scope.CheckedValue, DivisionalValue: $scope.DivisionalValue,FromDate: $scope.FromDate, ToDate: $scope.ToDate, RequiredDate: $scope.RequiredDate, TotalDays: $scope.TotalDays, Remarks: $scope.Remarks });
        $scope.DtlId = "";
        $scope.Mop = "";
        $scope.Purpose = "";
        $scope.PrepareValue = "";
        $scope.CheckedValue = "";
        $scope.DivisionalValue = "";
        $scope.FromDate = "";
        $scope.ToDate = "";
        $scope.RequiredDate = "";
        $scope.TotalDays = "";
        $scope.Remarks = "";
        index = null;
        $('#ExpReqDivisionalDtlModal').modal('hide');
    };
    //----------- Reset ----------------//

    $scope.Reset = function () {
        counter = 0;
        methodName = "";
        $scope.Id = "";
        $scope.RequisitionNo = "";
        $scope.PreparedByConfirm = "";
        $scope.PrepareDate = "";
        $scope.ReqType = "";
        $scope.CheckedDate = "";
        $scope.ExpenditureMonth = "";
        $scope.PayTo = "";
        $scope.CheckedStatus = "";
        $scope.CheckedRemarks = "";

        $scope.DivionalDate = toDay;
        $scope.DivionalStatus = "";
        $scope.DivionalRemarks = "";


        $scope.PaymentPlace = "";
        $scope.gridExpReqDivisionalDtlOptions.data = [];
        $scope.gridExpReqDivisionalDtlOptions.columnDefs[10].visible = true;
        $scope.btnSaveValue = "Save";
        $scope.labelPayToValue = "Pay To";
        $scope.isSaveDisable = false;
        $scope.isDeleteDisable = false;
        $scope.isSubmitDisable = true;
        $scope.PrepareRemarks = "";
    };
});