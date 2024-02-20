(function () {
    'use strict';

    angular
        .module('angle')
        .controller('OsdTrnBAMSummary', OsdTrnBAMSummary);

    OsdTrnBAMSummary.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams', '$sce','cmnfunctionService'];

    function OsdTrnBAMSummary($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, $sce,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'OsdTrnBAMSummary';
        
       
        activate();
        function activate() {
            lockUI();

            var url = 'api/OsdTrnBankAlert/GetServiceprivilege';
            SocketService.get(url).then(function (resp) {
                if (resp.data.privilege == "true") {
                    $scope.privilege = true;
                   
                }
                else {
                    $scope.privilege = false;
                   
                }
                unlockUI();
            });
            $scope.tab = {};
            var url = window.location.href;
            var relPath = url.split("lstab=");
            var relpath1 = relPath[1];
            if (relpath1 != undefined) {
                if (relpath1 == "requiredlist") {
                    $scope.tabrequiredlist = true;
                }
                else if (relpath1 == "allocatedlist") {
                    $scope.taballocatedlist = true;
                } 
                else if (relpath1 == "rhApprovePendinglist") {
                    $scope.tabrhApprovePendinglist = true;
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
                    $scope.tabrequiredlist = true;
                }
                else if ($scope.tab.activeTabId == 'pending') {
                    $scope.tabrequiredlist = true;

                }
                else if ($scope.tab.activeTabId == 'allocatedlist') {
                    $scope.taballocatedlist = true;
                } 
                else if ($scope.tab.activeTabId == 'rhApprovePendinglist') {
                    $scope.tabrhApprovePendinglist = true;
                }
                else if ($scope.tab.activeTabId == 'assignlist') {
                    $scope.tabassignlist = true;
                }
                else if ($scope.tab.activeTabId == "transferlist") {
                    $scope.tabtransferlist = true;
                }
            }
            lockUI();
            var url = 'api/OsdTrnBankAlert/GetSeenHistory';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true)
                {
                    unlockUI();
                }
              
              
            });
            var url = 'api/OsdTrnBankAlert/GetBAMDtlpendingSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.BAMDtlpending = resp.data.BankAlertAllocated_list;
            });
            var url = 'api/OsdTrnBankAlert/GetBAMpendingSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.BAMpendingSummary = resp.data.BankAlertAllocated_list;
            });
            var url = 'api/OsdTrnBankAlert/GetBAMRHApprovalpendingSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.RhpendingSummary = resp.data.BankAlertAllocated_list;
            });
            var url = 'api/OsdTrnBankAlert/GetBAMOperationpendingSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.BAMOperationpending = resp.data.BankAlertAllocated_list;
                //for (var i = 0; i < $scope.BAMOperationpending.length; i++) {
                //    $scope.BAMOperationpending[i].checked = true;

                //}

                //angular.forEach($scope.BAMOperationpending, function (value, key) {


                //    if (value.brs_flag == "Y") {
                //        $scope.yes_brs = true;
                //        $scope.yes_kotak = false;
                //        $scope.no_kotak = false;

                       
                //        value.yes_radio1 = true;
                //    }
                //    else if (value.kotakAPI_flag == "Y") {
                //        $scope.yes_kotak = true;
                //        $scope.yes_brs = false;
                //        $scope.no_kotak = false;
                       
                //        value.no_radio1 = true;
                //    }
                //    else if (value.kotakAPI_flag == "N") {
                //        $scope.no_kotak = true;
                //        $scope.yes_kotak = false;
                //        $scope.yes_brs = false;
                      
                //        value.partialscore_radio1 = true;
                //    }
                    
                //});
            });
           
            var url = 'api/OsdTrnBankAlert/GetBAMtransferSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.BAMtransferSummary = resp.data.BankAlerttransfer_list;
            });
            var url = 'api/OsdTrnBankAlert/GetCount';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.operation_count = resp.data.operation_count;
                $scope.allocated_count = resp.data.allocated_count;
                $scope.notallocated_count = resp.data.notallocated_count;
                $scope.transfer_count = resp.data.transfer_count;
                $scope.rhApprovePending_count = resp.data.rhApprovePending_count;
                
            });
        }
        $scope.requiredlist = function (bankalert2notallocated_gid, customer_gid) {
            $location.url('app/osdBamNotAllocatedView?hash=' + cmnfunctionService.encryptURL('lsbankalert2notallocated_gid=' + bankalert2notallocated_gid + '&lstab=requiredlist'));
        }
        $scope.allocatedtoRM = function (bankalert2allocated_gid, customer_gid, customer_urn) {
            $location.url('app/osdBamAllocatedView?lsbankalert2allocated_gid=' + bankalert2allocated_gid + '&lscustomer_gid=' + customer_gid + '&lstab=allocatedlist' + '&lscustomer_urn=' + customer_urn);
        }
        $scope.rhApprovePending = function (bankalert2allocated_gid, customer_gid, customer_urn) {
            $location.url('app/osdBamAllocatedView?lsbankalert2allocated_gid=' + bankalert2allocated_gid + '&lscustomer_gid=' + customer_gid + '&lstab=rhApprovePendinglist' + '&lscustomer_urn=' + customer_urn);
        }
        $scope.BankalertView = function (bankalert2allocated_gid, customer_gid, customer_urn, ticketref_no) {
            $location.url('app/osdBamAssign2operation?lsbankalert2allocated_gid=' + bankalert2allocated_gid + '&lscustomer_gid=' + customer_gid + '&lstab=assignlist' + '&lscustomer_urn=' + customer_urn + '&lsticketref_no=' + ticketref_no);
        }
        $scope.transfer = function (bankalert2allocated_gid, customer_gid, customer_urn, ticketref_no) {
            $location.url('app/osdBamAllocatedView?lsbankalert2allocated_gid=' + bankalert2allocated_gid + '&lscustomer_gid=' + customer_gid + '&lstab=transferlist' + '&lscustomer_urn=' + customer_urn + '&lsticketref_no=' + ticketref_no);
        }
    }
})();
