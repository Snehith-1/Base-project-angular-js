(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrSuprDocumentChecklistReportController', AgrSuprDocumentChecklistReportController);

    AgrSuprDocumentChecklistReportController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService'];

    function AgrSuprDocumentChecklistReportController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrSuprDocumentChecklistReportController';

        activate();

        function activate() {
            lockUI();
            $scope.lsmaker = true;
            $scope.lschecker = false;
            $scope.lsapproval = false;
            $scope.lsstatus = 'Maker';
            $('#scanmaker').addClass('tabactivecolorstyle');
            var url = 'api/AgrPmgReport/GetSuprDocChecklistReportSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.sanctionmaker_list = resp.data.suprdocumentpendinglist;
            });

            var url = 'api/AgrPmgReport/GetSuprDocChecklistPendingCount';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.cadmaker_count = resp.data.cadmaker_count;
                $scope.cadchecker_count = resp.data.cadchecker_count;
                $scope.cadcheckerapproval_count = resp.data.cadcheckerapproval_count;

            });

        }

        $scope.exportreport = function (lsstatus) {

            var params = {
                lsstatus: lsstatus
            }
            var url = 'api/AgrPmgReport/GetSuprDocChecklistReport';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !')

                }

            });
        }


        $scope.checker = function () {
            lockUI();
            $scope.lsstatus = 'Checker';
            $scope.lsmaker = false;
            $scope.lschecker = true;
            $scope.lsapproval = false;
            $('#scanmaker').removeClass('tabactivecolorstyle');
            $('#scanchecker').addClass('tabactivecolorstyle');
            $('#scanapprover').removeClass('tabactivecolorstyle');
            var url = 'api/AgrPmgReport/GetSuprDocChecklistReportCheckerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.sanctionmaker_list = resp.data.suprdocumentpendinglist;
            });

            var url = 'api/AgrPmgReport/GetSuprDocChecklistPendingCount';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.cadmaker_count = resp.data.cadmaker_count;
                $scope.cadchecker_count = resp.data.cadchecker_count;
                $scope.cadcheckerapproval_count = resp.data.cadcheckerapproval_count;

            });

        }

        $scope.approval = function () {
            lockUI();
            $scope.lsstatus = 'Approver';
            $scope.lsmaker = false;
            $scope.lschecker = false;
            $scope.lsapproval = true;
            $('#scanmaker').removeClass('tabactivecolorstyle');
            $('#scanchecker').removeClass('tabactivecolorstyle');
            $('#scanapprover').addClass('tabactivecolorstyle');
            var url = 'api/AgrPmgReport/GetSuprDocChecklistReportApprovalSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.sanctionmaker_list = resp.data.suprdocumentpendinglist;
            });
            var url = 'api/AgrPmgReport/GetSuprDocChecklistPendingCount';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.cadmaker_count = resp.data.cadmaker_count;
                $scope.cadchecker_count = resp.data.cadchecker_count;
                $scope.cadcheckerapproval_count = resp.data.cadcheckerapproval_count;

            });

        }

        $scope.maker = function () {
            lockUI();
            $scope.lsstatus = 'Maker';
            $scope.lsmaker = true;
            $scope.lschecker = false;
            $scope.lsapproval = false;
            $('#scanmaker').addClass('tabactivecolorstyle');
            $('#scanchecker').removeClass('tabactivecolorstyle');
            $('#scanapprover').removeClass('tabactivecolorstyle');
            var url = 'api/AgrPmgReport/GetSuprDocChecklistReportSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.sanctionmaker_list = resp.data.suprdocumentpendinglist;
            });

            var url = 'api/AgrPmgReport/GetSuprDocChecklistPendingCount';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.cadmaker_count = resp.data.cadmaker_count;
                $scope.cadchecker_count = resp.data.cadchecker_count;
                $scope.cadcheckerapproval_count = resp.data.cadcheckerapproval_count;

            });

        }

    }
})();
