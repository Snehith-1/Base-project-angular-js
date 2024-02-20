
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnMyUnReconClosedSummaryController', brsTrnMyUnReconClosedSummaryController);

    brsTrnMyUnReconClosedSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function brsTrnMyUnReconClosedSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {

        var vm = this;
        vm.title = 'brsTrnMyUnReconClosedSummaryController';

        activate();

        function activate() {
            $scope.limit = 6000;
            $scope.totalDisplayed = 100;

            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened = true;
            };
            vm.close = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.closed = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];

            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            var url = 'api/MyUnreconciliationManagement/GetMyunreConciliationClosedSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.MyUnreconciliationClose_list = resp.data.MyUnreconciliationClose_list;

                unlockUI();
            });

            var url = 'api/MyUnreconciliationManagement/GetMyunreConciliationSummaryCount';
            lockUI();
            SocketService.get(url).then(function (resp) {
               
                $scope.pendingrpt_count = resp.data.pendingrpt_count;
                $scope.closedrpt_count = resp.data.closedrpt_count;
                $scope.allocatependingrpt_count = resp.data.allocatependingrpt_count;
                unlockUI();
            });
            var url = 'api/MyUnreconciliationManagement/GetBank';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.bankdtllist = resp.data.bankdtllist;

            });
        }
        $scope.pending_rpt = function () {
            $state.go('app.brsTrnMyUnReconciliationSummary');
        }

        $scope.closed_rpt = function () {
            $state.go('app.brsTrnMyUnReconClosedSummary');
        }
        $scope.alloacted_rpt = function () {
            $state.go('app.brsTrnMyUnReconAlloactedPendingSummary');
        }


        $scope.onselectbank = function (bankname_gid) {
            lockUI();
            var params = {
                bankname_gid: $scope.cboBankName.bankname_gid
            }
            var url = 'api/MyUnreconciliationManagement/BankNameList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.bankdtllist = resp.data.bankdtllist;
                var lsbank_gid = '';
                var lsbank_name = '';
                if ($scope.cboBankName != undefined || $scope.cboBankName != null) {
                    lsbank_gid = $scope.cboBankName.bankname_gid;
                    lsbank_name = $scope.cboBankName.bankname_name;
                }
            });

            unlockUI();
        }

        $scope.all = function () {
            $scope.bank_gid = "";
            $scope.cr_dr = "";
            $scope.amount_greater = "";
            $scope.amount_lesser = "";
            $scope.knockoff_status = "";
            $scope.trns_date = "";


            var url = 'api/MyUnreconciliationManagement/GetMyunreConciliationClosedSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.MyUnreconciliationClose_list = resp.data.MyUnreconciliationClose_list;

                unlockUI();
            });
        }
        $scope.search = function () {
            lockUI();
            var lsbank_gid = '';
            var lsbank_name = '';
            if ($scope.cboBankName != undefined || $scope.cboBankName != null) {
                lsbank_gid = $scope.cboBankName.bankname_gid;
                lsbank_name = $scope.cboBankName.bankname_name;
            }
            if (($scope.single == false) && ($scope.cboBankName == "" || $scope.cboBankName == undefined)) {
                Notify.alert("Kindly Select Bank Name", "warning");
                unlockUI();
            }
            else {
                if ($scope.trn_date == undefined || $scope.trn_date == "") {
                    var trn_date = 'null';
                }
                else {
                    var trn_date1 = $scope.trn_date;

                    var trn_date = new Date(trn_date1.getTime() - (trn_date1.getTimezoneOffset() * 60000))
                        .toISOString()
                        .split("T")[0];
                }

                var params = {
                    bank_gid: lsbank_gid,
                    knockoff_status: $scope.knockoff_status,
                    amount_greater: $scope.txtgrt_amount,
                    amount_lesser: $scope.txtless_amount,
                    cr_dr: $scope.cr_dr,
                    trns_date: trn_date

                }

                var url = 'api/MyUnreconciliationManagement/GetMyunreConciliationClosedSummarySearch';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    $scope.MyUnreconciliationClose_list = resp.data.MyUnreconciliationClose_list;

                });

            }
        }
        $scope.view = function (banktransc_gid) {
            /* $location.url('app/brsTrnUnreconTagViewAssignedHistory?banktransc_gid=' + banktransc_gid);*/
            $location.url("app/brsTrnMyUnReconciliationClosedView?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid));
        }

        $scope.viewPDF = function (val) {
            lockUI();
            var params = {
                servicerequest_gid: val
            }

            var url = 'api/OsdTrnTicketManagement/txtfile';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                    unlockUI();
                }
                else {
                    //$modalInstance.close('closed');
                    alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                    unlockUI();
                }
            });
        }
        $scope.export = function (val1, val2) {

            lockUI();
            var lsbank_gid = '';
            var lsbank_name = '';
            if ($scope.cboBankName != undefined || $scope.cboBankName != null) {
                lsbank_gid = $scope.cboBankName.bankname_gid;
                lsbank_name = $scope.cboBankName.bankname_name;
            }
            if (($scope.single == false) && ($scope.cboBankName == "" || $scope.cboBankName == undefined)) {
                Notify.alert("Kindly Select Bank Name", "warning");
                unlockUI();
            }
            else {
                if ($scope.trn_date == undefined || $scope.trn_date == "") {
                    var trn_date = 'null';
                }
                else {
                    var trn_date1 = $scope.trn_date;

                    var trn_date = new Date(trn_date1.getTime() - (trn_date1.getTimezoneOffset() * 60000))
                        .toISOString()
                        .split("T")[0];
                }

                var params = {
                    bank_gid: lsbank_gid,
                    knockoff_status: $scope.knockoff_status,
                    amount_greater: $scope.txtgrt_amount,
                    amount_lesser: $scope.txtless_amount,
                    cr_dr: $scope.cr_dr,
                    trn_date: trn_date

                }
                var url = 'api/MyUnreconciliationManagement/UnreconClosedExport';
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI();
                        DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);

                    }
                    else {
                        unlockUI();
                        Notify.alert('Error Occurred While Export !', 'success')
                        activate();

                    }

                }
                );
            }
        }


    }
})();