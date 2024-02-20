(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCcMeetingSkipHistoryController', MstCcMeetingSkipHistoryController);

    MstCcMeetingSkipHistoryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCcMeetingSkipHistoryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCcMeetingSkipHistoryController';
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;

        activate();
        function activate() {

            var params = {
                application_gid: application_gid
            }

            var url = 'api/MstApplicationView/GetApplicationBasicView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtapplication_no = resp.data.application_no;
                $scope.txtbasiccustomer_name = resp.data.customer_name;
                $scope.txtcustomer_urn = resp.data.customer_urn;
            });

            var url = 'api/MstCC/GetCCMeetingSkipHistory';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.ccmeetingskiphistory_list = resp.data.ccmeetingskiphistory_list;
            });
        }

        $scope.ccskipped_reason = function (ccmeetingskip_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/ccskippedreason.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    ccmeetingskip_gid: ccmeetingskip_gid
                }
                var url = 'api/MstCC/GetCCMeetingSkippedReason';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblccmeetingskipreason = resp.data.reason;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.Back = function () {
            if (lspage == 'CCMeetingSkip') {
                $state.go('app.MstCcMeetingSkipSummary');
            }
            else if (lspage == 'CCApproved') {
                $state.go('app.MstApplCCApproved');
            }
            else {

            }            
        }

    }
})();
