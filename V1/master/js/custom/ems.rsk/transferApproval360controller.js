(function () {
    'use strict';

    angular
        .module('angle')
        .controller('transferApproval360controller', transferApproval360controller);

    transferApproval360controller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService'];

    function transferApproval360controller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'transferApproval360controller';

        activate();

        function activate() {
            lockUI();

            $scope.MyApprovalPage = localStorage.getItem('MyApprovalPage');

            var allocationdtl_gid = {
                allocationdtl_gid: localStorage.getItem('allocationdtl_gid')
            }
            var url = "api/allocationManagement/getallocatedtls";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                $scope.state_name = resp.data.transferFrom_statename;
                $scope.district_name = resp.data.transferFrom_districtname;
                $scope.assigned_RM = resp.data.transferfrom_assignedRM;
                $scope.customername = resp.data.customername;
                $scope.customer_urn = resp.data.customer_urn;
                $scope.ZonalRMname = resp.data.transferFrom_ZonalRMname;
                $scope.customer_gid = resp.data.customer_gid;
                $scope.zonal_name = resp.data.zonal_name;
            });

            var url = "api/allocationManagement/GetAllocationCustomerDtl";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                $scope.customerdetails = resp.data;
                $scope.sanctiondetails = resp.data.loandtl;
                $scope.customerCollateral = resp.data.collateraldtl;
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

            var allocation_transfergid = {
                allocation_transfergid: localStorage.getItem('allocation_transfergid')
            }
            var url = "api/allocationTransfer/getviewtransapprovaldtl";
            SocketService.getparams(url, allocation_transfergid).then(function (resp) {
                $scope.transapprovaldtl = resp.data;
            });

            var url = "api/allocationManagement/getAllocationdocument";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                if (Array.isArray(resp.data.upload_list) && resp.data.upload_list.length) {
                    $scope.upload_list = resp.data.upload_list;
                }
                else {
                }
            });

            var customer_gid = {
                customer_gid: localStorage.getItem('allocation_customer_gid')
            }
            var url = 'api/raiseLegalSR/GetDemandNoticedtl';

            SocketService.getparams(url, customer_gid).then(function (resp) {
                $scope.demandnotice_list = resp.data.demandnotice_list;
                $scope.demand_status = resp.data.demand_status;

            });

            if (localStorage.getItem('MyAllocation') == "Y") {
                $scope.MyAllocationView = true;
            }
            else {
                $scope.AllocationSummary = true;
            }

            unlockUI();
        }


        $scope.tranferApprove = function () {
            lockUI();
            var params = {
                transferapproval_gid: localStorage.getItem('transferapproval_gid'),
                approval_Remarks: $scope.textapprovalRemarks
            }
            console.log(params)
            if (localStorage.getItem('TransferFromApproval') == "Y") {
                var url = "api/allocationTransfer/posttransferFromApprove";
            }
            else {
                if (localStorage.getItem('ZonalApprovalToFlag') == "Y") {
                    var url = "api/allocationTransfer/postransferToApprove";
                }
                else {
                    var url = "api/allocationTransfer/posttransferFromApprove";
                }

            }

            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $state.go('app.transferApproval');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                }
            });

        }


        $scope.tranferReject = function () {
            lockUI();
            var params = {
                transferapproval_gid: localStorage.getItem('transferapproval_gid'),
                approval_Remarks: $scope.textapprovalRemarks
            }

            if (localStorage.getItem('TransferFromApproval') == "Y") {
                var url = "api/allocationTransfer/posttransferFromReject";
            }
            else {
                if (localStorage.getItem('ZonalApprovalToFlag') == "Y") {
                    var url = "api/allocationTransfer/posttransferToReject";
                }
                else {
                    var url = "api/allocationTransfer/posttransferFromReject";
                }
            }
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $state.go('app.transferApproval');
                    activate();

                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $state.go('app.transferApproval');
                    activate();
                }
            });

        }

        $scope.downloads = function (val1, val2) {
           DownloaddocumentService.Downloaddocument(val1, val2);
        }

    }
})();
