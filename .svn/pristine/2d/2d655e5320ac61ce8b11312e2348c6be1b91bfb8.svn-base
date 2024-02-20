(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstChequeMakerFollowDtlsController', AgrMstChequeMakerFollowDtlsController);

    AgrMstChequeMakerFollowDtlsController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AgrMstChequeMakerFollowDtlsController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstChequeMakerFollowDtlsController';
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;

        activate();

        function activate() {
            var params = {
                application_gid: application_gid
            }
            var url = 'api/AgrUdcManagement/GetUdcSummary';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.udcmanagement_list = resp.data.udcmanagement_list;
                unlockUI();
            });

            var url = 'api/AgrUdcManagement/GetApprovalDetails';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.maker_name = resp.data.maker_name;
                $scope.checker_name = resp.data.checker_name;
                $scope.approver_name = resp.data.approver_name;
                $scope.maker_approveddate = resp.data.maker_approveddate;
                $scope.checker_approveddate = resp.data.checker_approveddate;
                $scope.approver_approveddate = resp.data.approver_approveddate;
            });

            var params = {
                application_gid: application_gid
            }
            var url = 'api/AgrMstApplicationView/GetApplicationBasicView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtapplication_no = resp.data.application_no;
                $scope.txtbasiccustomer_name = resp.data.customer_name;
            });
        }


        $scope.viewCheque = function (udcmanagement2cheque_gid, udcmanagement_gid) {
            $location.url('app/AgrMstUDCMakerView?lsudcmanagement2cheque_gid=' + udcmanagement2cheque_gid + '&lsudcmanagement_gid=' + udcmanagement_gid + '&application_gid=' + application_gid + '&lspage=' + lspage);
        }

        $scope.Back = function () {
            if (lspage == 'makerfollowup') {
                $location.url('app/AgrMstCadChequeManagementSummary?lsudcmanagement_gid=' + $scope.udcmanagement_gid + '&application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == 'checkerfollowup') {
                $location.url('app/AgrMstCadChequeMgmtCheckerSummary?lsudcmanagement_gid=' + $scope.udcmanagement_gid + '&application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == 'CompletedApprocal') {
                $location.url('app/AgrMstChequeApprovalCompleted?lsudcmanagement_gid=' + $scope.udcmanagement_gid + '&application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else {

            }
        }


    }
})();
