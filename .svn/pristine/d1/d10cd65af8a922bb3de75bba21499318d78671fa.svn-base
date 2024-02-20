(function () {
    'use strict';

    angular
        .module('angle')
        .controller('osdBamAllocatedToRMController', osdBamAllocatedToRMController);

        osdBamAllocatedToRMController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams','$sce','cmnfunctionService'];

    function osdBamAllocatedToRMController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams,$sce,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'osdBamAllocatedToRMController';
        var banktransc_gid = $location.search().lsbanktransc_gid;

        activate();
        function activate() {
            $scope.tab = {};
            var url = window.location.href;
            var relPath = url.split("lstab=");
            var relpath1 = relPath[1];
            if (relpath1 != undefined) {
                if (relpath1 == "requiredlist") {
                    $scope.taballocatedlist = true;
                }
                else if (relpath1 == "allocatedlist") {
                    $scope.taballocatedlist = true;
                }
                else if (relpath1 == "Unreconciliationlist") {
                    $scope.tabUnreconciliationlist = true;
                }
                else if (relpath1 == "assignlist") {
                    $scope.tabassignlist = true;
                }
                else if (relpath1 == "transferlist") {
                    $scope.tabtransferlist = true;
                }
            }
            else {
                if ($scope.tab.activeTabId == undefined) {
                    $scope.taballocatedlist = true;
                }
                else if ($scope.tab.activeTabId == 'pending') {
                    $scope.taballocatedlist = true;

                }
                else if ($scope.tab.activeTabId == 'allocatedlist') {
                    $scope.taballocatedlist = true;
                }
                else if ($scope.tab.activeTabId == 'Unreconciliationlist') {
                    $scope.tabUnreconciliationlist = true;
                }
                else if ($scope.tab.activeTabId == 'assignlist') {
                    $scope.tabassignlist = true;
                }
                else if ($scope.tab.activeTabId == "transferlist") {
                    $scope.tabtransferlist = true;
                }
            }
            lockUI();
            var url = 'api/OsdTrnBankAlert/GetAllocatedSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.BankAlertAllocated_list = resp.data.BankAlertAllocated_list;
                unlockUI();
            });
            var url = 'api/OsdTrnBankAlert/GetAllocatedAssignedSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.BankAlertAllocatedAssigned_list = resp.data.BankAlertAllocatedAssigned_list;
                unlockUI();
            });
            var url = 'api/OsdTrnBankAlert/GetRMTransferSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.BankAlerttransfer_list = resp.data.BankAlerttransfer_list;
                unlockUI();
            });
            var url = 'api/OsdTrnBankAlert/GetAllocatedCount';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.allocated_count = resp.data.allocated_count;
                $scope.allocatedassigned_count = resp.data.allocatedassigned_count;
                $scope.transfer_count = resp.data.transfer_count;
                unlockUI();
            });
            var url = 'api/OsdTrnBankAlert/GetUnReconciliationSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.banktransc_gid = resp.data.banktransc_gid;
                $scope.BankAlertUnreconciliation_list = resp.data.BankAlertUnreconciliation_list;
                unlockUI();
            });
            var url = 'api/OsdTrnBankAlert/GetAllocatedCount';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.allocated_count = resp.data.allocated_count;
                $scope.allocatedassigned_count = resp.data.allocatedassigned_count;
                $scope.transfer_count = resp.data.transfer_count;
                $scope.unreconciliation_count = resp.data.unreconciliation_count;
                unlockUI();
            });
        }

        $scope.BankalertView = function (bankalert2allocated_gid, customer_gid,customer_urn) {
            $location.url('app/osdBamAllocatedToRMView?'+'lsbankalert2allocated_gid=' + bankalert2allocated_gid + '&lscustomer_gid=' + customer_gid + '&lscustomer_urn=' + customer_urn + '&lspage=Allocatedsummary' + '&lstab=allocatedlist');
        }
        $scope.transfer = function (bankalert2allocated_gid, customer_gid,customer_urn) {
            $location.url('app/osdBAMrmtransfer?hash=' + cmnfunctionService.encryptURL('lsbankalert2allocated_gid=' + bankalert2allocated_gid + '&lscustomer_gid=' + customer_gid + '&lscustomer_urn=' + customer_urn + '&lstab=transferlist'));
        }
        $scope.BankalertAssignedView = function (bankalert2allocated_gid, customer_gid, customer_urn) {
            $location.url('app/osdBamAllocatedToAssignedView?hash=' + cmnfunctionService.encryptURL('lsbankalert2allocated_gid=' + bankalert2allocated_gid + '&lscustomer_gid=' + customer_gid + '&lscustomer_urn=' + customer_urn + '&lstab=assignlist'));
        }
        $scope.UnreconciliationView = function (bankalert2allocated_gid, banktransc_gid) {
            $location.url('app/osdTrnUnreconciliationTransactionView?hash=' + cmnfunctionService.encryptURL('lsbankalert2allocated_gid=' + bankalert2allocated_gid + '&lsbanktransc_gid=' + banktransc_gid +  '&lstab=Unreconciliationlist'));
        }
        $scope.initiate_transfer = function (bankalert2allocated_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/initiatetransfer.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
          
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.tickettransfer = false;
                $scope.rmtransfer = false;
                $scope.warning = false;
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.transferstatus=function()
                {
                    $scope.tickettransfer = true;
                    $scope.rmtransfer = false;
                }
                $scope.rmstatus = function () {
                    $scope.rmtransfer = true;
                    $scope.tickettransfer = false;
                }
                $scope.changewarning=function()
                {
                    $scope.warning = false;
                }
                $scope.transfer_submit = function () {
                    if ($scope.rdbtransfer_type == 'Ticket Transfer')
                    {
                       if($scope.txttickettransfer_remarks =='' || $scope.txttickettransfer_remarks==undefined || $scope.txttickettransfer_remarks==null)
                        {                            
                            $scope.warning = true;
                        }
                        else {
                            var params = {
                                bankalert2allocated_gid: bankalert2allocated_gid,
                                transfer_type: $scope.rdbtransfer_type,
                                tickettransfer_remarks: $scope.txttickettransfer_remarks
                            }
                            console.log(params);
                            var url = 'api/OsdTrnBankAlert/Posttransfer';

                            SocketService.post(url, params).then(function (resp) {
                                if (resp.data.status == true) {
                                    $modalInstance.close('closed');
                                    Notify.alert(resp.data.message, {
                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    activate();

                                }
                                else {
                                    Notify.alert(resp.data.message, {
                                        status: 'warning',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    activate();
                                }
                            });
                        }
                    }
                    else {
                        if ($scope.txtrmtransfer_remarks == '' || $scope.txtrmtransfer_remarks == undefined || $scope.txtrmtransfer_remarks == null) {
                            $scope.warning = true;
                        }
                        else {
                            var params = {
                                bankalert2allocated_gid: bankalert2allocated_gid,
                                transfer_type: $scope.rdbtransfer_type,
                                rmtransfer_remarks: $scope.txtrmtransfer_remarks
                            }
                            console.log(params);
                            var url = 'api/OsdTrnBankAlert/Posttransfer';

                            SocketService.post(url, params).then(function (resp) {
                                if (resp.data.status == true) {
                                    $modalInstance.close('closed');
                                    Notify.alert(resp.data.message, {
                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    activate();

                                }
                                else {
                                    Notify.alert(resp.data.message, {
                                        status: 'warning',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    activate();
                                }
                            });
                        }
                    }
                   

                }
            }
        }

        $scope.Complete_list = function () {
            $location.url('app/OsdBamRMCompletedSummary');
        }       

    }
})();
