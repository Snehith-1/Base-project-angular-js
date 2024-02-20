(function () {
    'use strict';

    angular
        .module('angle')
        .controller('zonalAllocation360controller', zonalAllocation360controller);

    zonalAllocation360controller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout','DownloaddocumentService'];

    function zonalAllocation360controller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'zonalAllocation360controller';

        activate();

        function activate() {

            lockUI();
            var allocationdtl_gid = {
                allocationdtl_gid: localStorage.getItem('allocationdtl_gid')
            }
            var url = "api/allocationManagement/getallocatedtls";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                $scope.state_name = resp.data.state_name;
                $scope.district_name = resp.data.district_name;
                $scope.assigned_RM = resp.data.assigned_RM;
                $scope.customername = resp.data.customername;
                $scope.customer_urn = resp.data.customer_urn;
                $scope.ZonalRMname = resp.data.ZonalRMname;
                $scope.customer_gid = resp.data.customer_gid;
                $scope.zonal_name = resp.data.zonal_name;
            });

            var url = "api/allocationManagement/GetAllocationCustomerDtl";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                $scope.customerdetails = resp.data;
                $scope.sanctiondetails = resp.data.loandtl;
                $scope.customerCollateral = resp.data.collateraldtl;
                $scope.holdallocationlist = resp.data.holdallocation;
                $scope.U2CMovedallocation = resp.data.U2CMovedallocation;
                $scope.customerguarantorlist = resp.data.Guarantorsdtl;
                $scope.customerPromotorlist = resp.data.Promoterdtl;
                angular.forEach($scope.sanctiondetails, function (value, key) {
                    var params = {
                        sanction_gid: value.sanction_gid,
                        allocationdtl_gid: localStorage.getItem('allocationdtl_gid')
                    };

                    var url = 'api/allocationManagement/GetAllocateloanList';
                    SocketService.post(url, params).then(function (resp) {
                        value.loandetails = resp.data.loanList;
                        value.expand = false;
                    });
                });
            });
            var customer_gid = {
                customer_gid: localStorage.getItem('allocation_customer_gid')
            }
            var url = 'api/raiseLegalSR/GetDemandNoticedtl';

            SocketService.getparams(url, customer_gid).then(function (resp) {
                $scope.demandnotice_list = resp.data.demandnotice_list;
                $scope.demand_status = resp.data.demand_status;

            });
            var url = "api/customerManagement/EscrowSummary";
            SocketService.getparams(url, customer_gid).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.escrowlist = resp.data.escrowSummary;
                }
            });

            var url = "api/allocationManagement/getAllocationdocument";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                if (Array.isArray(resp.data.upload_list) && resp.data.upload_list.length) {
                    $scope.upload_list = resp.data.upload_list;
                    $scope.documentUpload=true
                }
                else {
                    $scope.documentNotUpload = true

                }
            });

            var url = "api/visitReport/GetAllocationLogDetail";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                $scope.scheduleList = resp.data.schedulelogdtl;
                $scope.calllogdtlList = resp.data.calllogdtl;
                
            });
            unlockUI();
        }

        $scope.escrowInfoView = function (escrow_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/EscrowInfoModal.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    escrow_gid: escrow_gid
                }
                //var url = "api/visitReport/GetScheduleLogHistory";
                //SocketService.getparams(url, params).then(function (resp) {
                //    $scope.scheduleList = resp.data.schedulelogdtl;
                //});

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

            }
        }

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
    }
})();
