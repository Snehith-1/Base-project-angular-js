(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstContractDtlSummaryController', AgrMstContractDtlSummaryController);

    AgrMstContractDtlSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrMstContractDtlSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstContractDtlSummaryController';
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.application_gid = $location.search().application_gid;
        //$scope.onboard_gid = $location.search().onboard_gid;
        //var onboard_gid = $scope.onboard_gid;
        var application_gid = $scope.application_gid;
        $scope.employee_gid = $location.search().lsemployee_gid;
        var employee_gid = $scope.employee_gid;
        activate();
       
        function activate() {
            var params = {
                application_gid: application_gid
            }
            var url = 'api/AgrTrnContract/GetContract';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.contract_list = resp.data.contractsummary_list;
            });

            var url = 'api/AgrTrnContract/GetApprovalDetails';
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
        }

        $scope.Back = function () {
            if (lspage == 'ContractMaker') {
                $location.url('app/AgrMstContractSummary');
            }
            else if (lspage == 'SanctionChecker') {
                $location.url('app/MstSanctionCheckerSummary');
            }
            else if (lspage == 'SanctionApproval') {
                $location.url('app/MstSanctionApprovalSummary');
            }
            else if (lspage == 'ContractChecker') {
                $location.url('app/AgrMstContractCheckerSummary');
            }
            else if (lspage == 'ContractApproval') {
                $location.url('app/AgrMstContractApprovalSummary');
            }
            else {
                $location.url('app/MstSanctionSummary');
            }
        }

        $scope.create_sanction = function () {
            // $location.url('app/AgrMstCreateContract');
           $location.url('app/AgrMstCreateContract?application_gid=' + application_gid + '&employee_gid=' + employee_gid + '&lspage=ContractMaker');
        }
        $scope.edit = function (application2sanction_gid) {
            $location.url('app/AgrMstContractEdit?application2sanction_gid=' + application2sanction_gid + '&employee_gid=' + employee_gid + '&application_gid=' + application_gid  + '&lspage=' + lspage);
        }
        $scope.sanctionlettergenerate = function (application2sanction_gid) {
            //localStorage.setItem('RefreshTemplate', 'N');
            $location.url('app/AgrMstAppSanctionLetterGeneration?sanction_gid=' + application2sanction_gid + '&employee_gid=' + employee_gid + '&application_gid=' + application_gid + '&lspage=' + lspage);
        } 
    }
})();
