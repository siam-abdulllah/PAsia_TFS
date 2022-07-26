app.controller("ExpRequisitionPrepareCtrl", function ($scope, $http, uiGridConstants) {
    $scope.EventPerm(41);
    var counter = 0;
    $('legend.uncollapsed').parents("fieldset").find('.form-group').show();
    $('legend.uncollapsed').parents("fieldset").find('.row').show();
    $('legend.uncollapsed').parents("fieldset").removeAttr("style");
    if ($('legend.uncollapsed').children("i").hasClass('fa-plus-square-o')) {
        $('legend.uncollapsed').children("i").removeClass("fa-plus-square-o").addClass("fa-minus-square-o");
    }
    else if ($('legend.uncollapsed').children("i").hasClass('fa-minus-square-o')) {
        $('legend.uncollapsed').children("i").removeClass("fa-minus-square-o").addClass("fa-plus-square-o");
    }
    const monthNames = ["January", "February", "March", "April", "May", "June",
        "July", "August", "September", "October", "November", "December"
    ];
    $scope.btnSaveValue = "Save";
    $scope.labelPayToValue = "Pay To";
    var date = new Date();
    var toDay = new Date();
    $scope.ExpenditureMonth = monthNames[date.getMonth()];
    toDay = ('0' + toDay.getDate()).slice(-2) + '/' + ('0' + (toDay.getMonth() + 1)).slice(-2) + '/' + toDay.getFullYear();
    $scope.RequisitionDate = toDay;
    $scope.isSaveDisable = false;
    $scope.isDeleteDisable = false;
    $scope.isSubmitDisable = true;
    //--------Requisition Type--------//

    $scope.GetRequisitionType = function () {
        $http({
            method: "POST",
            url: MyApp.rootPath + "ExpRequisitionPrepare/GetRequisitionType"
        }).then(function (response) {
            if (response.data.Status === "Ok") {
                if (response.data.Data.length > 0) {
                    $scope.ReqTypes = response.data.Data;
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
    $scope.SendMail = function () {
        $http({
            method: "POST",
            url: MyApp.rootPath + "ExpRequisitionPrepare/SendMail"
        }).then(function (response) {
            if (response.data.Status === "Ok") {
                if (response.data.Data.length > 0) {
                    $scope.ReqTypes = response.data.Data;
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
    //$scope.SendMail();
    $scope.GetRequisitionType();


    //--------Pay To--------//

    $scope.GetPayTo = function () {
        $http({
            method: "POST",
            url: MyApp.rootPath + "ExpRequisitionPrepare/GetPayTo"
        }).then(function (response) {
            //if (response.data.Status === "Ok") {
                if (response.data.length > 0) {
                    $scope.PayTos = response.data;
              }
            //} else {
            //    toastr.warning(response.data.Status, { timeOut: 2000 });
            //}
        },
            function (response) {
                if (response.status === 404) {
                    toastr.warning("Error Loading Requisition Type!", { timeOut: 2000 });
                }
            });
    };
    $scope.GetPayTo();
    //--------Payment Place--------//

    $scope.GetPaymentPlace = function () {
        $http({
            method: "POST",
            url: MyApp.rootPath + "ExpRequisitionPrepare/GetPaymentPlace"
        }).then(function (response) {
            if (response.data.Status === "Ok") {
                if (response.data.Data.length > 0) {
                    $scope.PaymentPlaces = response.data.Data;
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
    $scope.GetPaymentPlace();

    //-----------------------Insert ----------------------------//
    $scope.DateCompare = function () {
        var splitFromDate = new Date($scope.FromDate.split("/").reverse().join("-"));
        var fromDate = new Date(splitFromDate.getFullYear(), splitFromDate.getMonth(), splitFromDate.getDate());
        var splitToDate = new Date($scope.ToDate.split("/").reverse().join("-"));
        var toDate = new Date(splitToDate.getFullYear(), splitToDate.getMonth(), splitToDate.getDate());
        var todayDate = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());
        //var splitTodayDate = (todayDate.getMonth() + 1) + '/' + todayDate.getDate() + '/' + todayDate.getFullYear();
        if (fromDate > toDate) {
            toastr.warning("From Date Cannot be Greater Than To Date !");
            $scope.TotalDays = "";
            return false;
        }
        //} if (fromDate < todayDate || toDate < todayDate) {
        //    toastr.warning("Date Cannot be Lesser Than Current Date !");
        //    $scope.TotalDays = "";
        //    return false;
        //}
        if ($scope.FromDate && $scope.ToDate) {
            var diff = splitToDate.getTime() - splitFromDate.getTime();
            $scope.TotalDays = (diff / (1000 * 3600 * 24)) + 1;
        }
        else {
            $scope.TotalDays = "";
        }
    };
    $scope.SaveData = function () {
        var expReqPrepareDtlData = $scope.gridExpReqPrepareDtlOptions.data;

        if (expReqPrepareDtlData.length <= 0) {
            toastr.warning("Enter Requisition information properly!");
            return false;
        }
        $scope.SaveDb = {};
        $scope.SaveDb.MstId = $scope.Id;
        $scope.SaveDb.RequisitionNo = $scope.RequisitionNo;
        $scope.SaveDb.RequisitionDate = $scope.RequisitionDate;
        $scope.SaveDb.PrepareDate = $scope.RequisitionDate;
        $scope.SaveDb.ReqTypeName = $scope.frmExpRequisitionPrepare.ReqType.ReqTypeName;
        $scope.SaveDb.ExpenditureMonth = $scope.ExpenditureMonth;
        $scope.SaveDb.PayToCode = $scope.frmExpRequisitionPrepare.PayTo.PayToCode;
        $scope.SaveDb.PaymentPlace = $scope.frmExpRequisitionPrepare.PaymentPlace.PaymentPlace;
        $scope.SaveDb.PrepareRemarks = $scope.PrepareRemarks;
        $scope.SaveDb.TotalApprovedAmt = $scope.gridExpReqPrepareDtlApi.grid.columns[3].getAggregationValue();
        var methodName = "";
        if ($scope.Id) { methodName = "UpdateExpReqPrepareInfo"; }
        else { methodName = "InsertExpReqPrepareInfo"; }
        $http({
            method: "POST",
            url: MyApp.rootPath + "ExpRequisitionPrepare/" + methodName,
            data: { expReqPrepareMstInfo: $scope.SaveDb, expReqPrepareDtlData: expReqPrepareDtlData }
        }).then(function (response) {
            if (response.data.Status === "Ok") {
                $scope.Id = response.data.Id;
                $scope.RequisitionNo = response.data.Code;
                $scope.btnSaveValue = "Update";
                $scope.GetExpReqDtlList();
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
    $scope.SubmitData = function () {
        var expReqPrepareDtlData = $scope.gridExpReqPrepareDtlOptions.data;

        if (expReqPrepareDtlData.length <= 0) {
            toastr.warning("Enter Requisition information properly!");
            return false;
        }
        if (!$scope.Id) {
            toastr.warning("Save the data first!", { timeOut: 2000 });
            return false;
        }
        $scope.SaveDb = {};
        $scope.SaveDb.MstId = $scope.Id;
        $scope.SaveDb.RequisitionNo = $scope.RequisitionNo;
        $scope.SaveDb.RequisitionDate = $scope.RequisitionDate;
        $scope.SaveDb.PrepareDate = $scope.RequisitionDate;
        $scope.SaveDb.ReqTypeName = $scope.frmExpRequisitionPrepare.ReqType.ReqTypeName;
        $scope.SaveDb.ExpenditureMonth = $scope.ExpenditureMonth;
        $scope.SaveDb.PayToCode = $scope.frmExpRequisitionPrepare.PayTo.PayToCode;
        $scope.SaveDb.PaymentPlace = $scope.frmExpRequisitionPrepare.PaymentPlace.PaymentPlace;
        $scope.SaveDb.PrepareRemarks = $scope.PrepareRemarks;
        $scope.SaveDb.TotalApprovedAmt = $scope.gridExpReqPrepareDtlApi.grid.columns[3].getAggregationValue();
        $http({
            method: "POST",
            url: MyApp.rootPath + "ExpRequisitionPrepare/SubmitExpReqPrepareInfo",
            data: { expReqPrepareMstInfo: $scope.SaveDb }
        }).then(function (response) {
            if (response.data.Status === "Ok") {
                $scope.isSaveDisable = true;
                $scope.isDeleteDisable = true;
                $scope.isSubmitDisable = true;
                toastr.success("Submitted Successfully!", { timeOut: 2000 });
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
            url: MyApp.rootPath + "ExpRequisitionPrepare/GetExpReqMstList",
            data: { param: param }
        }).then(function (response) {
            //if (response.data.Status === "Ok") {
                if (response.data.length > 0) {
                    $('#ExpReqPrepareMstModal').modal('show');
                    $scope.gridExpReqPrepareMstOptions.data = response.data;
                } else {
                    toastr.warning("No Data Found!", { timeOut: 2000 });
                }
            //} else {
               // toastr.warning(response.data.Status, { timeOut: 2000 });
            //}
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
            url: MyApp.rootPath + "ExpRequisitionPrepare/GetExpReqDtlList",
            data: { mstId: $scope.Id }
        }).then(function (response) {
            if (response.data.Status === "Ok") {
                if (response.data.Data.length > 0) {
                    $scope.gridExpReqPrepareDtlOptions.data = response.data.Data;
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

    var columnExpReqPrepareMstInfo = [
        { name: 'MstId', displayName: "MstId", visible: false },
        { name: 'RequisitionNo', displayName: "Requisition No" },
        { name: 'RequisitionDate', displayName: "Requisition Date" },
        { name: 'ReqTypeName', displayName: "Requisition Type" },
        { name: 'ExpenditureMonth', displayName: "Expenditure Month" },
        { name: 'PayToCode', displayName: "PayToCode", visible: false },
        { name: 'PayToName', displayName: "Pay To" },
        { name: 'PayToDesig', displayName: "PayToDesig", visible: false },
        { name: 'PaymentPlace', displayName: "Payment Place", visible: false },
        { name: 'PrepareRemarks', displayName: "Prepared Remarks", visible: false },
        { name: 'PrepareDate', displayName: "Prepared Date" },
        { name: 'PreparedByConfirm', displayName: "Confirm" },
        { name: 'CheckedStatus', displayName: "Checked Status" },
        { name: 'CheckedDate', displayName: "Checked Date", visible: false },
        { name: 'VerifiedStatus', displayName: "Verified Status" },
        { name: 'VerifiedDate', displayName: "Verified Date", visible: false },
        { name: 'RecommendedStatus', displayName: "Recommended Status" },
        { name: 'RecommendedDate', displayName: "Recommended Date", visible: false },
        { name: 'ApprovedStatus', displayName: "Approved Status" },
        { name: 'ApprovedDate', displayName: "Approved Date", visible: false }
    ];

    $scope.gridExpReqPrepareMstOptions = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnExpReqPrepareMstInfo,
        rowTemplate: rowTemplateMst(),
        enableGridMenu: false,
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
        $scope.RequisitionDate = row.entity.RequisitionDate;
        $scope.frmExpRequisitionPrepare.ReqType = row.entity;
        if ($scope.frmExpRequisitionPrepare.ReqType.ReqTypeName == "Adjustment") {
            $scope.labelPayToValue = "Adjustment By";
        }
        else {
            $scope.labelPayToValue = "Pay To";
        }
        $scope.frmExpRequisitionPrepare.PayTo = row.entity;
        $scope.frmExpRequisitionPrepare.PaymentPlace = row.entity;
        $scope.ExpenditureMonth = row.entity.ExpenditureMonth;
        $scope.PrepareRemarks = row.entity.PrepareRemarks;
        $scope.PrepareDate = row.entity.PrepareDate;
        if (row.entity.PreparedByConfirm === "Yes") {
            $scope.gridExpReqPrepareDtlOptions.columnDefs[9].visible = false;
            $scope.gridExpReqPrepareDtlOptions.columnDefs[10].visible = false;
            $scope.isSaveDisable = true;
            $scope.isDeleteDisable = true;
            $scope.isSubmitDisable = true;
        }
        else {
            $scope.gridExpReqPrepareDtlOptions.columnDefs[9].visible = true;
            $scope.gridExpReqPrepareDtlOptions.columnDefs[10].visible = true;
            $scope.isSaveDisable = false;
            $scope.isDeleteDisable = false;
            $scope.isSubmitDisable = false;
        }
        $scope.GetExpReqDtlList();
        $('#ExpReqPrepareMstModal').modal('hide');
        $scope.btnSaveValue = "Update";
    };
    var columnExpReqPrepareDtlInfo = [
        { name: 'DtlId', displayName: "DtlId", visible: false },
        { name: 'Mop', displayName: "Mode", aggregationType: uiGridConstants.aggregationTypes.count },
        { name: 'Purpose', displayName: "Purpose" },
        { name: 'PrepareValue', displayName: "Amount", cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2' },
        { name: 'FromDate', displayName: "From Date" },
        { name: 'ToDate', displayName: "To Date" },
        { name: 'RequiredDate', displayName: "Required Date" },
        { name: 'TotalDays', displayName: "Total Day" },
        { name: 'Remarks', displayName: "Remarks" },
        {
            name: 'Action', enableFiltering: false, enableSorting: false,
            cellTemplate: '<div style="text-align:center;position: relative;width:100%;padding:2px 2px 2px 6px;"><button  class="btn-success" ng-click="grid.appScope.editGridGdnInfoOptionsRow(row)"><i class="fa fa-edit"></i>&nbspEdit</button></div>'
        }, {
            name: 'Action ', enableFiltering: false, enableSorting: false,
            cellTemplate: '<div style="text-align:center;position: relative;width:100%;padding:2px 2px 2px 6px;"><button  class="btn-danger" ng-click="grid.appScope.removeGridGdnInfoOptionsRow(row)"><i class="fa fa-remove"></i>&nbspDelete</button></div>'
        }
    ];
    $scope.gridExpReqPrepareDtlOptions = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnExpReqPrepareDtlInfo,
        //rowTemplate: rowTemplateDtl(),
        enableGridMenu: false,
        enableSelectAll: true,
        //exporterCsvFilename: 'ProductInfo.csv',
        //exporterMenuPdf: false,
        //exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridExpReqPrepareDtlApi = gridApi;
        }
    };

    $scope.showPopUpExpReqPrepareDtlOptions = function () {
        if (counter === 0) { $scope.LegendCollapse();}
        counter = 1;
        $scope.DtlId = "";
        $scope.Mop = "";
        $scope.Purpose = "";
        $scope.PrepareValue = "";
        $scope.FromDate = "";
        $scope.ToDate = "";
        $scope.RequiredDate = "";
        $scope.Remarks = "";
        $scope.TotalDays = "";
        index = null;
        $('#ExpReqPrepareDtlModal').modal('show');
    };
    $scope.editGridGdnInfoOptionsRow = function (row) {
        $scope.DtlId = row.entity.DtlId;
        $scope.Mop = row.entity.Mop;
        $scope.Purpose = row.entity.Purpose;
        $scope.PrepareValue = row.entity.PrepareValue;
        $scope.FromDate = row.entity.FromDate;
        $scope.ToDate = row.entity.ToDate;
        $scope.RequiredDate = row.entity.RequiredDate;
        $scope.Remarks = row.entity.Remarks;
        $scope.TotalDays = row.entity.TotalDays;
        index = $scope.gridExpReqPrepareDtlOptions.data.indexOf(row.entity);
        $('#ExpReqPrepareDtlModal').modal('show');
    };
    $scope.removeGridGdnInfoOptionsRow = function (row) {
        if (row.entity.DtlId) {
            var result = confirm("Are you sure you want to delete this?");
            if (result) {
                $http({
                    method: "POST",
                    url: MyApp.rootPath + "ExpRequisitionPrepare/DeleteExpReqDtl",
                    data: { dtlId: row.entity.DtlId }
                }).then(function (response) {
                    if (response.data.Status === "Ok") {
                        index = $scope.gridExpReqPrepareDtlOptions.data.indexOf(row.entity);
                        $scope.gridExpReqPrepareDtlOptions.data.splice(index, 1);
                        toastr.success("Deleted Successfully!", { timeOut: 2000 });
                    } else {
                        toastr.warning(response.data.Status, { timeOut: 2000 });
                    }
                },
                    function (response) {
                        if (response.status === 404) {
                            toastr.warning("Error Loading Requisition Type!", { timeOut: 2000 });
                        }
                    });
            }

        }
    };
    $scope.DeleteExpReqMst = function () {
        if ($scope.Id) {
            var result = confirm("Are you sure you want to delete this?");
            if (result) {
                $http({
                    method: "POST",
                    url: MyApp.rootPath + "ExpRequisitionPrepare/DeleteExpReqMst",
                    data: { mstId: $scope.Id }
                }).then(function (response) {
                    if (response.data.Status === "Ok") {
                        toastr.success("Deleted Successfully !", { timeOut: 2000 });
                    } else {
                        toastr.warning(response.data.Status, { timeOut: 2000 });
                    }
                },
                    function (response) {
                        if (response.status === 404) {
                            toastr.warning("Error Loading Requisition Type!", { timeOut: 2000 });
                        }
                    });
            }
            //Logic to delete the item
        }
    };

    $scope.addItemGridExpReqPrepareDtlOptions = function () {
        if (index !== null) {
            $scope.gridExpReqPrepareDtlOptions.data.splice(index, 1);
        }
        $scope.gridExpReqPrepareDtlOptions.data.push({ DtlId: $scope.DtlId, Mop: $scope.Mop, Purpose: $scope.Purpose, PrepareValue: $scope.PrepareValue, FromDate: $scope.FromDate, ToDate: $scope.ToDate, RequiredDate: $scope.RequiredDate, TotalDays: $scope.TotalDays, Remarks: $scope.Remarks });
        $scope.DtlId = "";
        $scope.Mop = "";
        $scope.Purpose = "";
        $scope.PrepareValue = "";
        $scope.FromDate = "";
        $scope.ToDate = "";
        $scope.RequiredDate = "";
        $scope.TotalDays = "";
        $scope.Remarks = "";
        index = null;
        $('#ExpReqPrepareDtlModal').modal('hide');
    };

    $scope.LabelPayToValue = function () {
        if ($scope.frmExpRequisitionPrepare.ReqType.ReqTypeName == "Adjustment") {
            $scope.labelPayToValue = "Adjustment By";
        }
        else {
            $scope.labelPayToValue = "Pay To";
        }
    }
    //----------- Reset ----------------//

    $scope.Reset = function () {
        $scope.Id = "";
        $scope.RequisitionNo = "";
        $scope.frmExpRequisitionPrepare.ReqType = undefined;
        $scope.RequisitionDate = toDay;
        $scope.ExpenditureMonth = monthNames[date.getMonth()];
        $scope.frmExpRequisitionPrepare.PayTo = undefined;
        $scope.PrepareRemarks = "";
        $scope.frmExpRequisitionPrepare.PaymentPlace = undefined;
        $scope.gridExpReqPrepareDtlOptions.data = [];
        $scope.gridExpReqPrepareDtlOptions.columnDefs[9].visible = true;
        $scope.gridExpReqPrepareDtlOptions.columnDefs[10].visible = true;
        $scope.btnSaveValue = "Save";
        $scope.labelPayToValue = "Pay To";
        $scope.isSaveDisable = false;
        $scope.isDeleteDisable = false;
        $scope.isSubmitDisable = true;
        $scope.LegendCollapse();
        counter = 0;
    };
});