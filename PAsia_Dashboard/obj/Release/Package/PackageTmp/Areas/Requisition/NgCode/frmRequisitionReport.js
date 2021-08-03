app.controller("RequisitionReportCtrl", function ($scope, $http, uiGridConstants) {
    $scope.EventPerm(47);

    //$scope.ViewReport = function () {
    //    console.log("function call");
    //    $scope.RequisitionDb = {};
    //    $scope.RequisitionDb.FromDate = $scope.FromDate;
    //    $scope.RequisitionDb.ToDate = $scope.ToDate;
    //    $scope.RequisitionDb.RequisitionNo = $scope.RequisitionNo;
    //    $scope.RequisitionDb.MstId = $scope.Id;
    //    debugger;
    //    $http({
    //        method: "POST",
    //        url: MyApp.rootPath + "RequisitionReport/RV",
    //        data: { requisitionReportBEL: $scope.RequisitionDb }
    //    });
    //};
    $scope.IsPrepareDateRequired = true;
    $scope.IsApprovedDateRequired = true;
    $scope.enablePrepareDate = function () {
        document.getElementById("PrepareToDate").disabled = false;
        $scope.IsPrepareDateRequired = true;
        $scope.IsApprovedDateRequired = false;
    }
    $scope.enableApprovedDate = function () {
        document.getElementById("ApprovedToDate").disabled = false;
        $scope.IsPrepareDateRequired = false;
        $scope.IsApprovedDateRequired = true;
    }


    $scope.dateFunction = function () {
        var prepareFromDate = document.getElementById("PrepareFromDate").value;
        var prepareToDate = document.getElementById("PrepareToDate").value;
        var approvedFromDate = document.getElementById("ApprovedFromDate").value;
        var approvedToDate = document.getElementById("ApprovedToDate").value;

        $scope.LoadEmployee(prepareFromDate, prepareToDate, approvedFromDate, approvedToDate);
        $scope.GetRequisionNo(prepareFromDate, prepareToDate, approvedFromDate, approvedToDate);

    };

    $scope.requisitionLoadFunction = function () {
        $scope.PreparedBy = $scope.frmRequisitionReport.PreparedByInfo.PreparedBy;
        var prepareFromDate = document.getElementById("PrepareFromDate").value;
        var prepareToDate = document.getElementById("PrepareToDate").value;
        var approvedFromDate = document.getElementById("ApprovedFromDate").value;
        var approvedToDate = document.getElementById("ApprovedToDate").value;
        var empCode = $scope.PreparedBy;
        $scope.frmRequisitionReport.Requisition = undefined;
        $scope.GetRequisionNo(prepareFromDate, prepareToDate, approvedFromDate, approvedToDate, empCode);

    };
    $scope.requisitionChangeFunction = function () {
        $scope.RequisitionNo = $scope.frmRequisitionReport.Requisition.RequisitionNo;
    };

    $scope.LoadEmployee = function (PrepareFromDate, PrepareToDate, ApprovedFromDate, ApprovedToDate) {
        $http({
            method: "POST",
            url: MyApp.rootPath + "RequisitionReport/GetEmployees",
            data: { PrepareFromDate: PrepareFromDate, PrepareToDate: PrepareToDate, ApprovedFromDate: ApprovedFromDate, ApprovedToDate: ApprovedToDate }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.PreparedByList = response.data;
            } else {
                $scope.PreparedByList = [];
                toastr.warning("Data Not Found!", { timeOut: 2000 });
            }
        },
            function (response) {
                if (response.status === 404) {
                    toastr.warning("Error Loading Requisition Type!", { timeOut: 2000 });
                }
            });

    };
    //$scope.LoadEmployee();
    $scope.GetRequisionNo = function (PrepareFromDate, PrepareToDate, ApprovedFromDate, ApprovedToDate, empCode) {
       
        $http({
            method: "POST",
            url: MyApp.rootPath + "RequisitionReport/GetRequisitionNo",
            data: { PrepareFromDate: PrepareFromDate, PrepareToDate: PrepareToDate, ApprovedFromDate: ApprovedFromDate, ApprovedToDate: ApprovedToDate, empCode: empCode }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.RequisitionList = response.data;
            } else {
                $scope.RequisitionList = [];
                toastr.warning("Data Not Found!", { timeOut: 2000 });
            }
        },
            function (response) {
                if (response.status === 404) {
                    toastr.warning("Error Loading Requisition Type!", { timeOut: 2000 });
                }
            });

    };



    $scope.Reset = function () {
        $scope.PreparedByList = [];
        $scope.RequisitionList = [];
        $scope.frmRequisitionReport.PreparedByInfo = undefined;
        $scope.frmRequisitionReport.Requisition = undefined;
        $scope.PrepareFromDate = "";
        $scope.PrepareToDate = "";
        $scope.ApprovedFromDate = "";
        $scope.ApprovedToDate = "";
        $scope.IsPrepareDateRequired = true;
        $scope.IsApprovedDateRequired = true;
        $('#RequisitionNo').empty();
        $('#RequisitionNo').append($('<option>', { value: "", html: "Please Select" }, '<option/>'));
        $('#PreparedBy').empty();
        $('#PreparedBy').append($('<option>', { value: "", html: "Please Select" }, '<option/>'));
    };
});
