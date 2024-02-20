(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstRptCadCovenantController', MstRptCadCovenantController);

    MstRptCadCovenantController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function MstRptCadCovenantController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstRptCadCovenantController';

        activate();

        function activate() {
            lockUI();
            $scope.lsstatus = 'Maker';
            $scope.lsmaker = true;
            $scope.lschecker = false;
            $scope.lsapproval = false;
           /* $scope.lsstatus = 'Maker';*/
            $('#scanmaker').addClass('tabactivecolorstyle');
            var url = 'api/MstCadCovenentReport/GetScannedDocMakerPendingSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedmakerpendinglist = resp.data.reportscannedmakerapplication;
                unlockUI();
            });

            var url = 'api/MstCadCovenentReport/GetAppScannedDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });


        }

        //$scope.sanctionmisreportview = function (application2sanction_gid, application_gid) {
        //    $location.url('app/MstSanctionMISReportView?application2sanction_gid=' + application2sanction_gid + '&application_gid=' + application_gid + '&lsreportpage=Checker');
        //}


        $scope.scanchecker_summary = function () {
            lockUI();
            $scope.lsstatus = 'Checker';
            $scope.lsmaker = false;
            $scope.lschecker = true;
            $scope.lsapproval = false;
            $('#scanmaker').removeClass('tabactivecolorstyle');
            $('#scanchecker').addClass('tabactivecolorstyle');
            $('#scanapprover').removeClass('tabactivecolorstyle');
            var url = 'api/MstCadCovenentReport/GetScannedDocCheckerPendingSummary';
            SocketService.get(url).then(function (resp) {

                $scope.scannedmakerpendinglist = resp.data.reportscannedmakerapplication;
                unlockUI();
            });
            var url = 'api/MstCadCovenentReport/GetAppScannedDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });

        }

        $scope.scanapprover_summary = function () {
            lockUI();
            $scope.lsstatus = 'Approver';
            $scope.lsmaker = false;
            $scope.lschecker = false;
            $scope.lsapproval = true;
            $('#scanmaker').removeClass('tabactivecolorstyle');
            $('#scanchecker').removeClass('tabactivecolorstyle');
            $('#scanapprover').addClass('tabactivecolorstyle');
            var url = 'api/MstCadCovenentReport/GetScannedDocApproverPendingSummary';
            SocketService.get(url).then(function (resp) {

                $scope.scannedmakerpendinglist = resp.data.reportscannedmakerapplication;
                unlockUI();
            });
            var url = 'api/MstCadCovenentReport/GetAppScannedDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });

        }

        $scope.scanmaker_summary = function () {
            lockUI();
            $scope.lsstatus = 'Maker';
            $scope.lsmaker = true;
            $scope.lschecker = false;
            $scope.lsapproval = false;
            $('#scanmaker').addClass('tabactivecolorstyle');
            $('#scanchecker').removeClass('tabactivecolorstyle');
            $('#scanapprover').removeClass('tabactivecolorstyle');
            var url = 'api/MstCadCovenentReport/GetScannedDocMakerPendingSummary';
            SocketService.get(url).then(function (resp) {

                $scope.scannedmakerpendinglist = resp.data.reportscannedmakerapplication;
                unlockUI();
            });
            var url = 'api/MstCadCovenentReport/GetAppScannedDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });

        }


        $scope.scanneddocumentssummary = function () {
            lockUI();
            $scope.lsstatus = 'Maker';
            $scope.lsmaker = true;
            $scope.lschecker = false;
            $scope.lsapproval = false;
            $('#scanmaker').addClass('tabactivecolorstyle');
            $('#scanchecker').removeClass('tabactivecolorstyle');
            $('#scanapprover').removeClass('tabactivecolorstyle');
            var url = 'api/MstCadCovenentReport/GetScannedDocMakerPendingSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedmakerpendinglist = resp.data.reportscannedmakerapplication;
                unlockUI();
            });

            var url = 'api/MstCadCovenentReport/GetAppScannedDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });

            $scope.scanchecker_summary = function () {
                lockUI();
                $scope.lsstatus = 'Checker';
                $scope.lsmaker = false;
                $scope.lschecker = true;
                $scope.lsapproval = false;
                $('#scanmaker').removeClass('tabactivecolorstyle');
                $('#scanchecker').addClass('tabactivecolorstyle');
                $('#scanapprover').removeClass('tabactivecolorstyle');
                var url = 'api/MstCadCovenentReport/GetScannedDocCheckerPendingSummary';
                SocketService.get(url).then(function (resp) {

                    $scope.scannedmakerpendinglist = resp.data.reportscannedmakerapplication;
                    unlockUI();
                });
                var url = 'api/MstCadCovenentReport/GetAppScannedDocCount';
                SocketService.get(url).then(function (resp) {
                    unlockUI();
                    $scope.overallCountinfo = resp.data;
                });

            }

            $scope.scanapprover_summary = function () {
                lockUI();
                $scope.lsstatus = 'Approver';
                $scope.lsmaker = false;
                $scope.lschecker = false;
                $scope.lsapproval = true;
                $('#scanmaker').removeClass('tabactivecolorstyle');
                $('#scanchecker').removeClass('tabactivecolorstyle');
                $('#scanapprover').addClass('tabactivecolorstyle');
                var url = 'api/MstCadCovenentReport/GetScannedDocApproverPendingSummary';
                SocketService.get(url).then(function (resp) {

                    $scope.scannedmakerpendinglist = resp.data.reportscannedmakerapplication;
                    unlockUI();
                });
                var url = 'api/MstCadCovenentReport/GetAppScannedDocCount';
                SocketService.get(url).then(function (resp) {
                    unlockUI();
                    $scope.overallCountinfo = resp.data;
                });

            }

            $scope.scanmaker_summary = function () {
                lockUI();
                $scope.lsstatus = 'Maker';
                $scope.lsmaker = false;
                $scope.lschecker = false;
                $scope.lsapproval = true;
                $('#scanmaker').addClass('tabactivecolorstyle');
                $('#scanchecker').removeClass('tabactivecolorstyle');
                $('#scanapprover').removeClass('tabactivecolorstyle');
                var url = 'api/MstCadCovenentReport/GetScannedDocMakerPendingSummary';
                SocketService.get(url).then(function (resp) {

                    $scope.scannedmakerpendinglist = resp.data.reportscannedmakerapplication;
                    unlockUI();
                });
                var url = 'api/MstCadCovenentReport/GetAppScannedDocCount';
                SocketService.get(url).then(function (resp) {
                    unlockUI();
                    $scope.overallCountinfo = resp.data;
                });

            }

        }
        $scope.physicaldocumentsSummary = function () {
            lockUI();
            $scope.lsstatus = 'Maker';
            $scope.lpmaker = true;
            $scope.lpchecker = false;
            $scope.lpapproval = false;
            $('#phymaker').addClass('tabactivecolorstyle');
            $('#phychecker').removeClass('tabactivecolorstyle');
            $('#phyapprover').removeClass('tabactivecolorstyle');
            var url = 'api/MstCadCovenentReport/GetPhysicalDocMakerPendingSummary';
            SocketService.get(url).then(function (resp) {
                $scope.physicalmakerpendinglist = resp.data.reportphyiscalmakerapplication;
                unlockUI();
            });

            var url = 'api/MstCadCovenentReport/GetAppPhysicalDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });



            $scope.phychecker_summary = function () {
                lockUI();
                $scope.lsstatus = 'Checker';
                $scope.lpmaker = false;
                $scope.lpchecker = true;
                $scope.lpapproval = false;
                $('#phymaker').removeClass('tabactivecolorstyle');
                $('#phychecker').addClass('tabactivecolorstyle');
                $('#phyapprover').removeClass('tabactivecolorstyle');
                var url = 'api/MstCadCovenentReport/GetPhysicalDocCheckerPendingSummary';
                SocketService.get(url).then(function (resp) {

                    $scope.physicalmakerpendinglist = resp.data.reportphyiscalmakerapplication;
                    unlockUI();
                });
                var url = 'api/MstCadCovenentReport/GetAppPhysicalDocCount';
                SocketService.get(url).then(function (resp) {
                    unlockUI();
                    $scope.overallCountinfo = resp.data;
                });

            }

            $scope.phyapprover_summary = function () {
                lockUI();
                $scope.lsstatus = 'Approver';
                $scope.lpmaker = false;
                $scope.lpchecker = false;
                $scope.lpapproval = true;
                $('#phymaker').removeClass('tabactivecolorstyle');
                $('#phychecker').removeClass('tabactivecolorstyle');
                $('#phyapprover').addClass('tabactivecolorstyle');
                var url = 'api/MstCadCovenentReport/GetPhysicalDocApproverPendingSummary';
                SocketService.get(url).then(function (resp) {

                    $scope.physicalmakerpendinglist = resp.data.reportphyiscalmakerapplication;
                    unlockUI();
                });
                var url = 'api/MstCadCovenentReport/GetAppPhysicalDocCount';
                SocketService.get(url).then(function (resp) {
                    unlockUI();
                    $scope.overallCountinfo = resp.data;
                });

            }

            $scope.phymaker_summary = function () {
                lockUI();
                $scope.lsstatus = 'Maker';
                $scope.lpmaker = true;
                $scope.lpchecker = false;
                $scope.lpapproval = false;
                $('#phymaker').addClass('tabactivecolorstyle');
                $('#phychecker').removeClass('tabactivecolorstyle');
                $('#phyapprover').removeClass('tabactivecolorstyle');
                var url = 'api/MstCadCovenentReport/GetPhysicalDocMakerPendingSummary';
                SocketService.get(url).then(function (resp) {

                    $scope.physicalmakerpendinglist = resp.data.reportphyiscalmakerapplication;
                    unlockUI();
                });
                var url = 'api/MstCadCovenentReport/GetAppPhysicalDocCount';
                SocketService.get(url).then(function (resp) {
                    unlockUI();
                    $scope.overallCountinfo = resp.data;
                });

            }

        }


        $scope.exportsanctionreport = function (lsstatus) {

            var params = {
                lsstatus: lsstatus
            }
            var url = 'api/MstCadCovenentReport/GetScannedCovPendingReport';
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

        $scope.exportphysicalreport = function (lsstatus) {

            var params = {
                lsstatus: lsstatus
            }
            var url = 'api/MstCadCovenentReport/GetPhysicalCovPendingReport';
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

        $scope.scannedview = function (val) {
            $location.url('app/MstRptCadApplicationDeferralCovenantView?application_gid=' + val + '&lspage=scannedcovenant');
        }

        $scope.physicalview = function (val) {
            $location.url('app/MstRptCadApplicationDeferralCovenantView?application_gid=' + val + '&lspage=physicalcovenant');
        }

        $scope.scanned_process = function (val) {
            $location.url('app/MstRptCadScannedDeferralCovenantDtls?application_gid=' + val + '&lspage=scannedcovenant');
        }

        $scope.physical_process = function (val) {
            $location.url('app/MstRptCadPhysicalDeferralCovenantDtls?application_gid=' + val + '&lspage=physicalcovenant');
        }

    }
})();
