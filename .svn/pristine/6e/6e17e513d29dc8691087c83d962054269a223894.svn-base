(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstApprovalPendingWarehouseSummaryController', AgrMstApprovalPendingWarehouseSummaryController);

    AgrMstApprovalPendingWarehouseSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrMstApprovalPendingWarehouseSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstApprovalPendingWarehouseSummaryController';
        //lockUI();
        activate();
        function activate() {
            var url = 'api/AgrMstWarehouseAdd/GetPendingWarehouseSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.warehouseadd_list = resp.data.MdlAgrMstWarehouseCreation;
            });
            //var url = 'api//';
            //SocketService.get(url).then(function (resp) {
            //unlockUI();
            //$scope._count = resp.data._count;

            //});

        }

        $scope.Warehouse_Add = function () {
            $state.go('app.AgrMstWarehouseAdd');
        }

        //$scope.application_view = function (warehouse_gid) {
        //    $location.url('app/AgrMstWarehouseView?warehouse_gid=' + warehouse_gid);
        //}

        $scope.warehouse_edit = function (warehouse_gid) {
            $location.url('app/AgrMstWarehouseEdit?warehouse_gid=' + warehouse_gid + '&lspage=ApprovalPendingWarehouseEdit');
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

        $scope.Rejected_summary = function () {
            $location.url('app/AgrMstMyRejectedWarehouseSummary');
        }

    }
})();
