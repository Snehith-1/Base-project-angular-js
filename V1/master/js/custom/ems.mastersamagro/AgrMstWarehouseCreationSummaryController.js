﻿(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstWarehouseCreationSummaryController', AgrMstWarehouseCreationSummaryController);

    AgrMstWarehouseCreationSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'cmnfunctionService'];

    function AgrMstWarehouseCreationSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,  cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstWarehouseCreationSummaryController';
        //lockUI();
        activate();
        function activate() {
            lockUI();
            $scope.showeditbtn = false;
            $scope.showapprovalstatus = false;
            $('#approvedwarehousetab').addClass('tabactivecolorstyle');
            var url = 'api/AgrMstWarehouseAdd/GetWarehouseCount';
            SocketService.get(url).then(function (resp) { 
                $scope.warehousecount = resp.data;
            });
            //var url = 'api/AgrMstWarehouseAdd/GetNewWarehouseSummary';
            //SocketService.get(url).then(function (resp) {
            //    unlockUI();
            //    $scope.warehouseadd_list = resp.data.MdlAgrMstWarehouseCreation;
            //});  

            var url = 'api/AgrMstWarehouseAdd/DaGetApprovedWarehouseSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.warehouseadd_list = resp.data.MdlAgrMstWarehouseCreation;
            });
        }

        //$scope.MyWarehouses_summary = function () {
        //    $scope.showeditbtn = true;
        //    $('#mywarehousetab').addClass('tabactivecolorstyle');
        //    $('#pendingwarehousetab').removeClass('tabactivecolorstyle');
        //    $('#approvedwarehousetab').removeClass('tabactivecolorstyle');
        //    $('#rejectedwarehousetab').removeClass('tabactivecolorstyle'); 
        //    lockUI();
        //    var url = 'api/AgrMstWarehouseAdd/GetWarehouseCount';
        //    SocketService.get(url).then(function (resp) {
        //        $scope.warehousecount = resp.data;
        //    });
        //    var url = 'api/AgrMstWarehouseAdd/GetNewWarehouseSummary';
        //    SocketService.get(url).then(function (resp) {
        //        unlockUI();
        //        $scope.warehouseadd_list = resp.data.MdlAgrMstWarehouseCreation;
        //    });
        //}

        $scope.ApprovalPending_summary = function () {
            $scope.showeditbtn = true;
            $scope.showapprovalstatus = true;
            $('#pendingwarehousetab').addClass('tabactivecolorstyle');
            $('#mywarehousetab').removeClass('tabactivecolorstyle');
            $('#approvedwarehousetab').removeClass('tabactivecolorstyle');
            $('#rejectedwarehousetab').removeClass('tabactivecolorstyle');
            lockUI();
            var url = 'api/AgrMstWarehouseAdd/GetWarehouseCount';
            SocketService.get(url).then(function (resp) {
                $scope.warehousecount = resp.data;
            });
            var url = 'api/AgrMstWarehouseAdd/GetApprovalPendingWarehouseSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.warehouseadd_list = resp.data.MdlAgrMstWarehouseCreation;
            });
        }

        $scope.Approved_summary = function () {
            $scope.showeditbtn = false;
            $scope.showapprovalstatus = false;
            $('#approvedwarehousetab').addClass('tabactivecolorstyle');
            $('#mywarehousetab').removeClass('tabactivecolorstyle');
            $('#pendingwarehousetab').removeClass('tabactivecolorstyle');
            $('#rejectedwarehousetab').removeClass('tabactivecolorstyle');
            lockUI();
            var url = 'api/AgrMstWarehouseAdd/GetWarehouseCount';
            SocketService.get(url).then(function (resp) {
                $scope.warehousecount = resp.data;
            });
            var url = 'api/AgrMstWarehouseAdd/DaGetApprovedWarehouseSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.warehouseadd_list = resp.data.MdlAgrMstWarehouseCreation;
            });
        }

        $scope.Rejected_summary = function () {
            $scope.showeditbtn = false;
            $scope.showapprovalstatus = true;
            $('#rejectedwarehousetab').addClass('tabactivecolorstyle');
            $('#mywarehousetab').removeClass('tabactivecolorstyle');
            $('#pendingwarehousetab').removeClass('tabactivecolorstyle');
            $('#approvedwarehousetab').removeClass('tabactivecolorstyle');
            lockUI();
            var url = 'api/AgrMstWarehouseAdd/GetWarehouseCount';
            SocketService.get(url).then(function (resp) {
                $scope.warehousecount = resp.data;
            });
            var url = 'api/AgrMstWarehouseAdd/GetRejectedWarehouseSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.warehouseadd_list = resp.data.MdlAgrMstWarehouseCreation;
            });
        }

    $scope.Warehouse_Add = function () {
            $state.go('app.AgrMstWarehouseAdd');
        }

    $scope.warehouse_view = function (warehouse_gid) {
        $location.url('app/AgrMstWarehouseDtlApproval?hash=' + cmnfunctionService.encryptURL('warehouse_gid=' + warehouse_gid + '&lspage=FromWarehouseCreation'));
    }

    $scope.warehouse_edit = function (warehouse_gid) {
        $location.url('app/AgrMstWarehouseEdit?hash=' + cmnfunctionService.encryptURL('warehouse_gid=' + warehouse_gid + '&lspage=WarehouseEdit'));
    }

    $scope.showPopover = function (warehouse_gid) {
        var modalInstance = $modal.open({
            templateUrl: '/showlocation.html',
            controller: ModalInstanceCtrl,
            backdrop: 'static',
            keyboard: false,
            size: 'md'
        });
        ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        function ModalInstanceCtrl($scope, $modalInstance) {
            var params = {
                warehouse_gid: warehouse_gid
            }
            var url = 'api/AgrMstWarehouseAdd/GetWarehouseProductcommodity';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.varietyname_list = resp.data.Warehousevarietyname_list;
                $scope.product_name = resp.data.product_name;
            });
            $scope.ok = function () {
                $modalInstance.close('closed');
            };
        }
    }


    $scope.warhouse_summary = function () {
        $location.url('app/AgrMstWarehouseCreationSummary');
    }

    $scope.pending_summary = function () {
        $location.url('app/AgrMstApprovalPendingWarehouseSummary');
    }

    $scope.completed_summary = function () {
        $location.url('app/AgrMstMyApprovalWarehouseSummary');
    } 
 
    }
})();
