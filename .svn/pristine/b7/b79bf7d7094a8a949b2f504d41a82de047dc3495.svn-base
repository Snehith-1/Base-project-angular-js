(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstContractAcceptedController', AgrMstContractAcceptedController);

    AgrMstContractAcceptedController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function AgrMstContractAcceptedController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstContractAcceptedController';

        activate();

        function activate() {
            lockUI();
            var url = "api/AgrTrnContract/SanctionAcceptedSummary";
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.sanctionlist = resp.data.sanctiondetails;
            });
            //var url = 'api/AgrTrnContract/CADSanctionSummaryCount';
            //lockUI();
            //SocketService.get(url).then(function (resp) {
            //    unlockUI();
            //    $scope.MakerPendingCount = resp.data.MakerPendingCount;
            //    $scope.MakerFollowUpCount = resp.data.MakerFollowUpCount;
            //    $scope.CheckerPendingCount = resp.data.CheckerPendingCount;
            //    $scope.CheckerFollowUpCount = resp.data.CheckerFollowUpCount;
            //    $scope.ApproverPendingCount = resp.data.ApproverPendingCount;
            //    $scope.CompletedCount = resp.data.CompletedCount;
            //    $scope.AcceptedCount = resp.data.AcceptedCount;
            //});
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

        $scope.view = function (val) {
            $location.url('app/AgrMstApplicationView?application_gid=' + val + '&lspage=SanctionAcceptedCustomer');
        }

        $scope.approval_process = function (val) {
            $location.url('app/AgrMstContractDtlSummary?application_gid=' + val + '&lspage=SanctionAcceptedCustomer');
        }

        $scope.checkerapprovalview = function (customer2sanction_gid, application_gid) {
            $location.url('app/AgrMstSanctionDtlViewSummary?sanction_gid=' + customer2sanction_gid + '&application_gid=' + application_gid + '&lspage=SanctionAcceptedCustomer');
        }


        $scope.SanctionletterPDF = function (application2sanction_gid) {
            var params = {
                sanction_gid: application2sanction_gid
            };
            var url = 'api/AgrTrnContractS/GetPDFGenerate';
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
