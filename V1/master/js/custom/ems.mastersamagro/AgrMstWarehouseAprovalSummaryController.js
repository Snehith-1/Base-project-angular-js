(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstWarehouseAprovalSummaryController', AgrMstWarehouseAprovalSummaryController);

    AgrMstWarehouseAprovalSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'cmnfunctionService'];

    function AgrMstWarehouseAprovalSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstWarehouseAprovalSummaryController';
        //lockUI();
        activate();
        function activate() {
            lockUI();
            var url = 'api/AgrMstProductPmgApproval/GetWarehouseApprovalCount';
            SocketService.get(url).then(function (resp) { 
                $scope.approvalCount = resp.data;
            });

            var url = 'api/AgrMstProductPmgApproval/GetApprovedwarehouseSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.warehouseadd_list = resp.data.PendingProductApprovaldtl;
            }); 

        }

        $scope.warehouse_view = function (warehouse_gid) {
            $location.url('app/AgrMstWarehouseDtlApproval?hash=' + cmnfunctionService.encryptURL('warehouse_gid=' + warehouse_gid + '&lspage=ApprovedWarehouse'));
        }

        $scope.warehouse_edit = function (warehouse_gid) {
            $location.url('app/AgrMstWarehouseEdit?hash=' + cmnfunctionService.encryptURL('warehouse_gid=' + warehouse_gid + '&lspage=ApprovedWarehouse'));
        }

        $scope.product_summary = function () {
            $location.url('app/AgrMstPendingProductApproval');
        }

        $scope.pmg_summary = function () {
            $location.url('app/AgrMstPendingPmgApproval');
        }

        $scope.completed_summary = function () {
            $location.url('app/AgrMstWarehouseAprovalSummary');
        }
        $scope.rejected_summary = function () {
            $location.url('app/AgrMstRejectedWarehouses');
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

    }
})();
