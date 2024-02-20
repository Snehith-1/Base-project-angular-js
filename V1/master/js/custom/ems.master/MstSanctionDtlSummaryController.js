(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSanctionDtlSummaryController', MstSanctionDtlSummaryController);

    MstSanctionDtlSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstSanctionDtlSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSanctionDtlSummaryController';
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.employee_gid = $location.search().lsemployee_gid;
        var employee_gid = $scope.employee_gid;
        activate();
       
        function activate() {
            var params = {
                application_gid: application_gid
            }
            var url = 'api/MstCAD/GetAppSanctionSummary';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.appsanction_list = resp.data.appsanction_list;
            });

            var url = 'api/MstCAD/GetApprovalDetails';
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
            if (lspage == 'SanctionMaker') {
                $location.url('app/MstSanctionSummary');
            }
            else if (lspage == 'SanctionChecker') {
                $location.url('app/MstSanctionCheckerSummary');
            }
            else if (lspage == 'SanctionApproval') {
                $location.url('app/MstSanctionApprovalSummary');
            }
            else {
                $location.url('app/MstSanctionSummary');
            }
        }

        $scope.create_sanction = function () {
            $location.url('app/MstCreateSanction?application_gid=' + application_gid + '&employee_gid=' + employee_gid + '&lspage=SanctionMaker');
        }
        $scope.edit = function (application2sanction_gid) {
            $location.url('app/MstSanctionEdit?application2sanction_gid=' + application2sanction_gid + '&employee_gid=' + employee_gid + '&application_gid=' + application_gid + '&lspage=SanctionMaker');
        }
        $scope.sanctionlettergenerate = function (sanction_gid) {
            localStorage.setItem('RefreshTemplate', 'N'); 
            $location.url('app/MstAppSanctionLetterGeneration?sanction_gid=' + sanction_gid + '&employee_gid=' + employee_gid + '&application_gid=' + application_gid + '&lspage=SanctionMaker');
        }
    }
})();
