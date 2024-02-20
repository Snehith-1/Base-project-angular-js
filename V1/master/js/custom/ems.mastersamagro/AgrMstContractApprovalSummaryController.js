(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstContractApprovalSummaryController', AgrMstContractApprovalSummaryController);

    AgrMstContractApprovalSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function AgrMstContractApprovalSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstContractApprovalSummaryController';
        activate();
      
        function activate() {
            lockUI();
            var url = "api/AgrTrnContract/ContractApprovalSummary";
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.sanctionlist = resp.data.sanctiondetails; 
            });
            var url = 'api/AgrTrnContract/CADContractSummaryCount';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.MakerPendingCount = resp.data.MakerPendingCount;
                $scope.MakerFollowUpCount = resp.data.MakerFollowUpCount;
                $scope.CheckerPendingCount = resp.data.CheckerPendingCount;
                $scope.CheckerFollowUpCount = resp.data.CheckerFollowUpCount;
                $scope.ApproverPendingCount = resp.data.ApproverPendingCount;
                $scope.CompletedCount = resp.data.CompletedCount;
                $scope.AcceptedCount = resp.data.AcceptedCount;
                $scope.RejectedCount =  resp.data.RejectedCount;

            });

        }

     

        $scope.maker = function () {
            $location.url('app/AgrMstContractSummary');
        }

        $scope.checker = function () {
            $location.url('app/AgrMstContractCheckerSummary');
        }

        $scope.approval = function () {
            $location.url('app/AgrMstContractApprovalSummary');
        }

        $scope.approvalcompleted = function () {
            $location.url('app/AgrMstContractApprovalCompleted');
        }
        $scope.accepted = function () {
            $location.url('app/AgrMstContractAccepted');
        }
        $scope.Rejected = function () {
            $location.url('app/AgrMstContractRejectedSummary');
        }

        //$scope.view = function (val,val1) {
        //    $location.url('app/AgrMstCadApplicationView?application_gid=' + val + '&employee_gid=' + val1 + '&lspage=SanctionApproval');
        //}

        $scope.view = function (val) {
            $location.url('app/AgrMstCadApplicationView?application_gid=' + val + '&lspage=ContractApprovalView');
        }

        //$scope.approval_process = function (val) {
        //    $location.url('app/AgrMstSanctionDtlSummary?application_gid=' + val + '&lspage=SanctionApproval');
        //}

        $scope.checkerapprovalview = function (customer2sanction_gid, application_gid) {
            $location.url('app/AgrMstSanctionDtlViewSummary?sanction_gid=' + customer2sanction_gid + '&application_gid=' + application_gid + '&lspage=checkerapprovalsummary');
        }       
        $scope.approval_process = function (val, val1) {
            // $location.url('app/AgrMstContractDtlSummary');
            $location.url('app/AgrMstContractDtlSummary?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lspage=ContractApproval');
        }

        $scope.SanctionletterPDF = function (application2sanction_gid) {
            var params = {
                sanction_gid: application2sanction_gid
            };
            var url = 'api/AgrTrnContract/GetPDFGenerate';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                //var phyPath = resp.data.lspath1;
                //var relPath = phyPath.split("EMS");
                //var relpath1 = relPath[1].replace("\\", "/");
                //var hosts = window.location.host;
                //var prefix = location.protocol + "//";
                //var str = prefix.concat(hosts, relpath1);
                //var link = document.createElement("a");
                //var name = resp.data.lsname1.split(".")
                //link.download = name[0];
                //var uri = str;
                //link.href = uri;
                //link.click();
                DownloaddocumentService.Downloaddocument(resp.data.lspath1, resp.data.lsname1);
                unlockUI();
            });
        }
    }
})();
