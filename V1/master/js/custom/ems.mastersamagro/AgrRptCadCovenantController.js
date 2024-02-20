(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrRptCadCovenantController', AgrRptCadCovenantController);

    AgrRptCadCovenantController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function AgrRptCadCovenantController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrRptCadCovenantController';

        activate();

        function activate() {
            lockUI();
            $scope.lsmaker = true;
            $scope.lschecker = false;
            $scope.lsapproval = false;
            $scope.lsstatus = 'Maker';
            $('#scanmaker').addClass('tabactivecolorstyle');

            var url = 'api/AgrPmgReport/GetScannedDocMakerPendingSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedmakerpendinglist = resp.data.rptscannedmakerapplication;
                unlockUI();
            });
            var url = 'api/AgrPmgReport/GetAppScannedDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.scannedcountinfo = resp.data;
            });



        }

        $scope.scanchecker_summary = function () {
            lockUI();
            $scope.lsstatus = 'Checker';
            $scope.lsmaker = false;
            $scope.lschecker = true;
            $scope.lsapproval = false;
            $('#scanmaker').removeClass('tabactivecolorstyle');
            $('#scanchecker').addClass('tabactivecolorstyle');
            $('#scanapprover').removeClass('tabactivecolorstyle');

            var url = 'api/AgrPmgReport/GetScannedDocCheckerPendingSummary';
            SocketService.get(url).then(function (resp) {

                $scope.scannedmakerpendinglist = resp.data.rptscannedmakerapplication;
                unlockUI();
            });
            var url = 'api/AgrPmgReport/GetAppScannedDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.scannedcountinfo = resp.data;
            });

        }

        $scope.scanapprover_summary = function () {
            lockUI();
            $scope.lsstatus = 'Approval';
            $scope.lsmaker = false;
            $scope.lschecker = false;
            $scope.lsapproval = true;
            $('#scanmaker').removeClass('tabactivecolorstyle');
            $('#scanchecker').removeClass('tabactivecolorstyle');
            $('#scanapprover').addClass('tabactivecolorstyle');

            var url = 'api/AgrPmgReport/GetScannedDocApproverPendingSummary';
            SocketService.get(url).then(function (resp) {

                $scope.scannedmakerpendinglist = resp.data.rptscannedmakerapplication;
                unlockUI();
            });
            var url = 'api/AgrPmgReport/GetAppScannedDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.scannedcountinfo = resp.data;
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

            var url = 'api/AgrPmgReport/GetScannedDocMakerPendingSummary';
            SocketService.get(url).then(function (resp) {

                $scope.scannedmakerpendinglist = resp.data.rptscannedmakerapplication;
                unlockUI();
            });
            var url = 'api/AgrPmgReport/GetAppScannedDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.scannedcountinfo = resp.data;
            });

        }



        $scope.scanneddocumentssummary = function () {
            lockUI();
            $scope.lsmaker = true;
            $scope.lschecker = false;
            $scope.lsapproval = false;
            $scope.lsstatus = 'Maker';
            $('#scanmaker').addClass('tabactivecolorstyle');
            $('#scanchecker').removeClass('tabactivecolorstyle');
            $('#scanapprover').removeClass('tabactivecolorstyle');

            var url = 'api/AgrPmgReport/GetScannedDocMakerPendingSummary';
            SocketService.get(url).then(function (resp) {

                $scope.scannedmakerpendinglist = resp.data.rptscannedmakerapplication;
                unlockUI();
            });
            var url = 'api/AgrPmgReport/GetAppScannedDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.scannedcountinfo = resp.data;
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

                var url = 'api/AgrPmgReport/GetScannedDocCheckerPendingSummary';
                SocketService.get(url).then(function (resp) {

                    $scope.scannedmakerpendinglist = resp.data.rptscannedmakerapplication;
                    unlockUI();
                });
                var url = 'api/AgrPmgReport/GetAppScannedDocCount';
                SocketService.get(url).then(function (resp) {
                    unlockUI();
                    $scope.scannedcountinfo = resp.data;
                });

            }

            $scope.scanapprover_summary = function () {
                lockUI();
                $scope.lsstatus = 'Approval';
                $scope.lsmaker = false;
                $scope.lschecker = false;
                $scope.lsapproval = true;
                $('#scanmaker').removeClass('tabactivecolorstyle');
                $('#scanchecker').removeClass('tabactivecolorstyle');
                $('#scanapprover').addClass('tabactivecolorstyle');

                var url = 'api/AgrPmgReport/GetScannedDocApproverPendingSummary';
                SocketService.get(url).then(function (resp) {

                    $scope.scannedmakerpendinglist = resp.data.rptscannedmakerapplication;
                    unlockUI();
                });
                var url = 'api/AgrPmgReport/GetAppScannedDocCount';
                SocketService.get(url).then(function (resp) {
                    unlockUI();
                    $scope.scannedcountinfo = resp.data;
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

                var url = 'api/AgrPmgReport/GetScannedDocMakerPendingSummary';
                SocketService.get(url).then(function (resp) {

                    $scope.scannedmakerpendinglist = resp.data.rptscannedmakerapplication;
                    unlockUI();
                });
                var url = 'api/AgrPmgReport/GetAppScannedDocCount';
                SocketService.get(url).then(function (resp) {
                    unlockUI();
                    $scope.scannedcountinfo = resp.data;
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

            var url = 'api/AgrPmgReport/GetPhysicalDocMakerPendingSummary';
            SocketService.get(url).then(function (resp) {

                $scope.physicalmakerpendinglist = resp.data.rptphyiscalmakerapplication;
                unlockUI();
            });
            var url = 'api/AgrPmgReport/GetAppPhysicalDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.physicalcountinfo = resp.data;
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

                var url = 'api/AgrPmgReport/GetPhysicalDocCheckerPendingSummary';
                SocketService.get(url).then(function (resp) {

                    $scope.physicalmakerpendinglist = resp.data.rptphyiscalmakerapplication;
                    unlockUI();
                });
                var url = 'api/AgrPmgReport/GetAppPhysicalDocCount';
                SocketService.get(url).then(function (resp) {
                    unlockUI();
                    $scope.physicalcountinfo = resp.data;
                });

            }

            $scope.phyapprover_summary = function () {
                lockUI();
                $scope.lsstatus = 'Approval';
                $scope.lpmaker = false;
                $scope.lpchecker = false;
                $scope.lpapproval = true;
                $('#phymaker').removeClass('tabactivecolorstyle');
                $('#phychecker').removeClass('tabactivecolorstyle');
                $('#phyapprover').addClass('tabactivecolorstyle');

                var url = 'api/AgrPmgReport/GetPhysicalDocApproverPendingSummary';
                SocketService.get(url).then(function (resp) {

                    $scope.physicalmakerpendinglist = resp.data.rptphyiscalmakerapplication;
                    unlockUI();
                });
                var url = 'api/AgrPmgReport/GetAppPhysicalDocCount';
                SocketService.get(url).then(function (resp) {
                    unlockUI();
                    $scope.physicalcountinfo = resp.data;
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

                var url = 'api/AgrPmgReport/GetPhysicalDocMakerPendingSummary';
                SocketService.get(url).then(function (resp) {

                    $scope.physicalmakerpendinglist = resp.data.rptphyiscalmakerapplication;
                    unlockUI();
                });
                var url = 'api/AgrPmgReport/GetAppPhysicalDocCount';
                SocketService.get(url).then(function (resp) {
                    unlockUI();
                    $scope.physicalcountinfo = resp.data;
                });

            }


        }


        //$scope.view = function (val) {
        //    $location.url('app/AgrRptCadApplicationDeferralCovenantView?application_gid=' + val + '&lsstatus = Maker');
        //}

        $scope.scannedmakerview = function (val) {
            $location.url('app/AgrRptCadApplicationDeferralCovenantView?application_gid=' + val + '&lspage=scannedcovenant');
        }

        $scope.physicalmakerview = function (val) {
            $location.url('app/AgrRptCadApplicationDeferralCovenantView?application_gid=' + val + '&lspage=physicalcovenant');
        }

        $scope.scanned_process = function (val) {
            $location.url('app/AgrRptCadScannedDeferralCovenantDtls?application_gid=' + val + '&lspage=scannedcovenant');
        }

        $scope.physical_process = function (val) {
            $location.url('app/AgrRptCadPhysicalDeferralCovenantDtls?application_gid=' + val + '&lspage=physicalcovenant');
        }
        //$scope.scanmaker_summary = function () {
        //    $location.url('app/AgrRptCadCovenant');
        //}

        //$scope.scanchecker_summary = function () {
        //    $location.url('app/AgrRptCadCovenantChecker');
        //}
        //$scope.scanapprover_summary = function () {
        //    $location.url('app/AgrRptCadCovenantApproval');
        //}


        //$scope.phymaker_summary = function () {
        //    $location.url('app/AgrRptCadCovenant');
        //}

        //$scope.phychecker_summary = function () {
        //    $location.url('app/AgrRptCadCovenantChecker');
        //}
        //$scope.phyapprover_summary = function () {
        //    $location.url('app/AgrRptCadCovenantApproval');
        //}



        $scope.exportcovenantmakerreport = function (lsstatus) {
           
            var params = {
                lsstatus: lsstatus
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

        $scope.exportcovphysicalreport = function (lsstatus) {

            var params = {
                lsstatus: lsstatus
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
