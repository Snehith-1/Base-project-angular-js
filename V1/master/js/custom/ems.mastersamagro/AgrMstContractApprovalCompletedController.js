(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstContractApprovalCompletedController', AgrMstContractApprovalCompletedController);

    AgrMstContractApprovalCompletedController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function AgrMstContractApprovalCompletedController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstContractApprovalCompletedController';
        $scope.employee_gid = $location.search().lsemployee_gid;
        var employee_gid = $scope.employee_gid;
        activate();

        function activate() {
            lockUI();
            var url = "api/AgrTrnContract/ApprovalCompletedSummary";
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

        $scope.cadcompletedsummary = function () {
            lockUI();
            var url = "api/AgrTrnContract/ApprovalCompletedSummary";
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
            //    $scope.CompletedCADCount = resp.data.CompletedCADCount;
            //    $scope.CompletedNotAcceptedCount = resp.data.CompletedNotAcceptedCount;
            //});
        }

       
        $scope.notacceptedsummary = function () {
            lockUI();
            var url = "api/AgrTrnContract/SanctionNotAcceptedSummary";
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.sanctionlistnotaccept = resp.data.sanctiondetails;
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
            //    $scope.CompletedCADCount = resp.data.CompletedCADCount;
            //    $scope.CompletedCount = resp.data.CompletedCount;
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
        $scope.Rejected = function () {
            $location.url('app/AgrMstContractRejectedSummary');
        }

        //$scope.view = function (val) {
        //    $location.url('app/AgrMstCadApplicationView?application_gid=' + val + '&lspage=SanctionApprovalCompleted');
        //}

        $scope.view = function (val) {
            $location.url('app/AgrMstCadApplicationView?application_gid=' + val + '&lspage=ContractApprovalCompletedView');
        }

        $scope.approval_process = function (val) {
            $location.url('app/MstSanctionDtlSummary?application_gid=' + val + '&lspage=SanctionApprovalCompleted');
        }

        $scope.checkerapprovalview = function (customer2sanction_gid, application_gid) {
            $location.url('app/AgrMstContractDtlViewSummary?sanction_gid=' + customer2sanction_gid + '&application_gid=' + application_gid + '&lspage=ContractApprovalCompleted' + '&lsresubmit=submit_flag');
        }

        $scope.checkerapprovalviewnot = function (customer2sanction_gid, application_gid) {
            $location.url('app/AgrMstContractDtlViewSummary?sanction_gid=' + customer2sanction_gid + '&application_gid=' + application_gid + '&lspage=ContractApprovalCompleted' + '&lsresubmit=rewubmit_flag');
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
