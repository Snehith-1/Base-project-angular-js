(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrRptCadCovenantApprovalController', AgrRptCadCovenantApprovalController);

    AgrRptCadCovenantApprovalController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function AgrRptCadCovenantApprovalController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrRptCadCovenantApprovalController';

        activate();

        function activate() {
            lockUI();
            var url = 'api/AgrPmgReport/GetScannedDocApproverPendingSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedapproverpendinglist = resp.data.rptscannedmakerapplication;
                unlockUI();
            });
            var url = 'api/AgrPmgReport/GetPhysicalDocApproverPendingSummary';
            SocketService.get(url).then(function (resp) {
                $scope.physicalapproverpendinglist = resp.data.rptscannedmakerapplication;
                unlockUI();
            });

            var url = 'api/AgrTrnPhysicalDocument/CADAppPhysicalDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

       
        //$scope.view = function (val) {
        //    $location.url('app/AgrRptCadApplicationDeferralCovenantView?application_gid=' + val + '&lsstatus = ');
        //}
        $scope.scannedapproverview = function (val) {
            $location.url('app/AgrRptCadApplicationDeferralCovenantView?application_gid=' + val + '&lspage=scannedapprovalcovenant');
        }

        $scope.physicalapproverview = function (val) {
            $location.url('app/AgrRptCadApplicationDeferralCovenantView?application_gid=' + val + '&lspage=physicalapprovalcovenant');
        }

        $scope.scannedApproval_process = function (val) {
            $location.url('app/AgrRptCadScannedDeferralCovenantDtls?application_gid=' + val + '&lspage=scannedapprovalcovenant');
        }

        $scope.physicalapprover_process = function (val) {
            $location.url('app/AgrRptCadPhysicalDeferralCovenantDtls?application_gid=' + val + '&lspage=physicalapprovalcovenant');
        }

        $scope.maker_summary = function () {
            $location.url('app/AgrRptCadCovenant');
        }

        $scope.checker_summary = function () {
            $location.url('app/AgrRptCadCovenantChecker');
        }
        $scope.approver_summary = function () {
            $location.url('app/AgrRptCadCovenantApproval');
        }

        $scope.exportcovenantapproverrreport = function () {

            var params = {
                lsstatus: 'Approver'
            }
            lockUI();
            var url = 'api/AgrPmgReport/GetScannedCovPendingReport';
             /* SocketService.get(url, params).then(function (resp) {*/
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname, resp.data.lsstatus);
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !')

                }

            });
        }

        $scope.exportcovphysicalreport = function () {

            var params = {
                lsstatus: 'Approver'
            }
            lockUI();
            var url = 'api/AgrPmgReport/GetPhysicalCovPendingReport';
            /* SocketService.get(url, params).then(function (resp) {*/
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname, resp.data.lsstatus);
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !')

                }

            });
        }
    }
})();
