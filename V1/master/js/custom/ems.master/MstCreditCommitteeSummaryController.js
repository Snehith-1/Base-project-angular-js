(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditCommitteeSummaryController', MstCreditCommitteeSummaryController);

        MstCreditCommitteeSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCreditCommitteeSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCreditCommitteeSummaryController';
        
        lockUI();
        activate();
        function activate() {           
            var url = 'api/MstCC/GetCCPendingSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.applicationadd_list = resp.data.applicationadd_list;
            });
            var url = 'api/MstCC/GetCCcount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.lblscheduled_count = resp.data.scheduled_count;
                $scope.lblpending_count = resp.data.pending_count;
                $scope.lblcompleted_count = resp.data.completed_count;
                $scope.lblsentbackcc_count = resp.data.cctocredit_count;
                $scope.lblccmeetingskip_count = resp.data.ccmeetingskip_count;
            });
            
        }

        $scope.application_view = function (application_gid) {
            $location.url('app/MstCcCommitteeApplicationView?application_gid=' + application_gid+ '&lspage=ScheduleMeeting');
        }
        $scope.schedule_meeting = function (application_gid) {
            $location.url('app/MstCCMeetingSchedule?application_gid=' + application_gid+ '&lstab=Pending');
        }
        $scope.scheduled_summary = function () {
            $location.url('app/MstCCscheduledSummary');
        }
        $scope.pending_summary = function () {
            $location.url('app/MstCreditCommitteeSummary');
        }
        $scope.completed_summary = function () {
            $location.url('app/MstCCCompletedSummary');
        }
        $scope.sentbackto_credit = function () {
            $location.url('app/MstSentbackcctoCredit');
        }
        $scope.ccmeeting_skiptab = function () {
            $location.url('app/MstCcMeetingSkipSummary');
        }

        $scope.sentback_credit = function (application_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/sentbacktounderwriting.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {
                    var params = {
                        cctocredit_reason: $scope.txtreason,
                        application_gid : application_gid
                    }
                    var url = 'api/MstCC/PostRevertCCtoCredit';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

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
                        }
                    });

                    $modalInstance.close('closed');
                }
            }
        }

        $scope.history = function (application_gid) {
            $location.url('app/MstSentbackCadtoCcHistory?application_gid=' + application_gid + '&lspage=Pending');
        }

        $scope.reverse_log = function (application_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/reverselogpopup.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
               
                    var params = {
                        application_gid: application_gid
                    }
                    var url = 'api/MstCC/GetCCtoCreditLog';
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.cctocreditlog_list = resp.data.cctocreditlog_list;
                    });

                    var url = 'api/MstCAD/GetCADtoCCMeetingLog';
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.cadtoccmeetinglog_list = resp.data.cadtoccmeetinglog_list;
                    });

                    var url = 'api/MstCAD/GetCADtoCreditLog';
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.cadtocreditlog_list = resp.data.cadtocreditlog_list;
                    });
                   
                    $scope.ok = function () {
                        $modalInstance.close('closed');
                    };
            }
        }

        $scope.ccmeeting_skip = function (application_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/ccmeetingskip.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {
                    var params = {
                        ccmeetingskip_reason: $scope.txtccmeetingskip_reason,
                        application_gid: application_gid
                    }
                    var url = 'api/MstCC/PostCcMeetingSkip';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

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
                        }
                    });

                    $modalInstance.close('closed');
                }
            }
        }

    }
})();
