(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrRptCadCovenantCheckerController', AgrRptCadCovenantCheckerController);

    AgrRptCadCovenantCheckerController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function AgrRptCadCovenantCheckerController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrRptCadCovenantCheckerController';

        activate();

        function activate() {
            lockUI();
            var url = 'api/AgrPmgReport/GetScannedDocCheckerPendingSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedcheckerpendinglist = resp.data.rptscannedmakerapplication;
                unlockUI();
            });
            var url = 'api/AgrPmgReport/GetPhysicalDocCheckerPendingSummary';
            SocketService.get(url).then(function (resp) {
                $scope.physicalcheckerpendinglist = resp.data.rptphyiscalmakerapplication;
                unlockUI();
            });

            var url = 'api/AgrTrnPhysicalDocument/CADAppPhysicalDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }


        $scope.scannedcheckerview = function (val) {
            $location.url('app/AgrRptCadApplicationDeferralCovenantView?application_gid=' + val + '&lspage=scannedcheckercovenant');
        }

        $scope.physicalcheckerview = function (val) {
            $location.url('app/AgrRptCadApplicationDeferralCovenantView?application_gid=' + val + '&lspage=physicalcheckercovenant');
        }

        $scope.scannedchecker_process = function (val) {
            $location.url('app/AgrRptCadScannedDeferralCovenantDtls?application_gid=' + val + '&lspage=scannedcheckercovenant');
        }

        $scope.physicalchecker_process = function (val) {
            $location.url('app/AgrRptCadPhysicalDeferralCovenantDtls?application_gid=' + val + '&lspage=physicalcheckercovenant');
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

        $scope.exportcovenantcheckerreport = function () {

            var params = {
                lsstatus: 'Checker'
            }
            lockUI();
            var url = 'api/AgrPmgReport/GetScannedCovPendingReport';
            /*  SocketService.get(url, params).then(function (resp) {*/
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
                lsstatus: 'Checker'
            }
            lockUI();
            var url = 'api/AgrPmgReport/GetPhysicalCovPendingReport';
            /*  SocketService.get(url, params).then(function (resp) {*/
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
