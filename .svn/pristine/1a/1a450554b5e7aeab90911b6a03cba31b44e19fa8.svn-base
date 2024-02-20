(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrRptCadDeferralController', AgrRptCadDeferralController);

    AgrRptCadDeferralController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function AgrRptCadDeferralController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrRptCadDeferralController';

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
            //var url = 'api/AgrPmgReport/DaGetPhysicalDocCheckerPendingSummary';
            //SocketService.get(url).then(function (resp) {
            //    $scope.scannedmakerpendinglist = resp.data.rptscannedmakerapplication;
            //    unlockUI();
            //});

            var url = 'api/AgrPmgReport/GetAppScannedDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
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
            var url = 'api/AgrPmgReport/GetScannedDocApproverPendingSummary';
            SocketService.get(url).then(function (resp) {

                $scope.scannedmakerpendinglist = resp.data.rptscannedmakerapplication;
                unlockUI();
            });
            var url = 'api/AgrPmgReport/GetAppScannedDocCount';
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
            var url = 'api/AgrPmgReport/GetScannedDocMakerPendingSummary';
            SocketService.get(url).then(function (resp) {

                $scope.scannedmakerpendinglist = resp.data.rptscannedmakerapplication;
                unlockUI();
            });
            var url = 'api/AgrPmgReport/GetAppScannedDocCount';
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
            var url = 'api/AgrPmgReport/GetScannedDocMakerPendingSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedmakerpendinglist = resp.data.rptscannedmakerapplication;
                unlockUI();
            });

            var url = 'api/AgrPmgReport/GetAppScannedDocCount';
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
                var url = 'api/AgrPmgReport/GetScannedDocCheckerPendingSummary';
                SocketService.get(url).then(function (resp) {

                    $scope.scannedmakerpendinglist = resp.data.rptscannedmakerapplication;
                    unlockUI();
                });
                var url = 'api/AgrPmgReport/GetAppScannedDocCount';
                SocketService.get(url).then(function (resp) {
                    unlockUI();
                    $scope.overallCountinfo = resp.data;
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
                var url = 'api/AgrPmgReport/GetScannedDocMakerPendingSummary';
                SocketService.get(url).then(function (resp) {

                    $scope.scannedmakerpendinglist = resp.data.rptscannedmakerapplication;
                    unlockUI();
                });
                var url = 'api/AgrPmgReport/GetAppScannedDocCount';
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
                var url = 'api/AgrPmgReport/GetPhysicalDocMakerPendingSummary';
                SocketService.get(url).then(function (resp) {
                    $scope.physicalmakerpendinglist = resp.data.rptphyiscalmakerapplication;
                    unlockUI();
                });

                var url = 'api/AgrPmgReport/GetAppPhysicalDocCount';
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
                    var url = 'api/AgrPmgReport/GetPhysicalDocCheckerPendingSummary';
                    SocketService.get(url).then(function (resp) {

                        $scope.physicalmakerpendinglist = resp.data.rptphyiscalmakerapplication;
                        unlockUI();
                    });
                    var url = 'api/AgrPmgReport/GetAppPhysicalDocCount';
                    SocketService.get(url).then(function (resp) {
                        unlockUI();
                        $scope.overallCountinfo = resp.data;
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
                    var url = 'api/AgrPmgReport/GetPhysicalDocMakerPendingSummary';
                    SocketService.get(url).then(function (resp) {

                        $scope.physicalmakerpendinglist = resp.data.rptphyiscalmakerapplication;
                        unlockUI();
                    });
                    var url = 'api/AgrPmgReport/GetAppPhysicalDocCount';
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
                var url = 'api/AgrPmgReport/GetScannedDefPendingReport';
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
                var url = 'api/AgrPmgReport/GetPhysicalDefPendingReport';
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
            $location.url('app/AgrRptCadApplicationDeferralCovenantView?application_gid=' + val + '&lspage=scanneddeferral');
        }

        $scope.physicalview = function (val) {
            $location.url('app/AgrRptCadApplicationDeferralCovenantView?application_gid=' + val + '&lspage=physicaldeferral');
        }

        $scope.scanned_process = function (val) {
            $location.url('app/AgrRptCadScannedDeferralCovenantDtls?application_gid=' + val + '&lspage=scanneddeferral');
        }

        $scope.physical_process = function (val) {
            $location.url('app/AgrRptCadPhysicalDeferralCovenantDtls?application_gid=' + val + '&lspage=physicaldeferral');
        }

    }
})();
