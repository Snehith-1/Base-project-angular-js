(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSanctionSummaryController', MstSanctionSummaryController);

    MstSanctionSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstSanctionSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSanctionSummaryController';

        activate();
        
        function activate() {
            lockUI();
            var url = 'api/MstCAD/GetSanctionMakerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.sanctionmaker_list = resp.data.cadapplicationlist;
            });
            var url = 'api/MstCAD/CADSanctionSummaryCount';
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
            });
        }

        $scope.makerPendingSUmmary = function () {
            lockUI();
            var url = 'api/MstCAD/GetSanctionMakerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.sanctionmaker_list = resp.data.cadapplicationlist;
            });
            var url = 'api/MstCADFlow/CADSanctionSummaryCount';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.MakerPendingCount = resp.data.MakerPendingCount;
                $scope.MakerFollowUpCount = resp.data.MakerFollowUpCount;
                $scope.CheckerPendingCount = resp.data.CheckerPendingCount;
                $scope.CheckerFollowUpCount = resp.data.CheckerFollowUpCount;
                $scope.ApproverPendingCount = resp.data.ApproverPendingCount;
                $scope.CompletedCount = resp.data.CompletedCount; 
            });
        }

        $scope.makerfollowupSUmmary = function () {
            lockUI();
            var url = 'api/MstCAD/GetSanctionFollowupMakerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.sanctionmaker_list = resp.data.cadapplicationlist;
            });
            var url = 'api/MstCADFlow/CADSanctionSummaryCount';
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
            });
        }  

        $scope.maker = function () {
            $location.url('app/MstSanctionSummary');
        }

        $scope.checker = function () {
            $location.url('app/MstSanctionCheckerSummary');
        }

        $scope.approval = function () {
            $location.url('app/MstSanctionApprovalSummary');
        }

        $scope.approvalcompleted = function () {
            $location.url('app/MstSanctionApprovalCompleted');
        }


        $scope.accepted = function () {
            $location.url('app/MstSanctionAccepted');
        }

        $scope.view = function (val) {
            $location.url('app/MstCadApplicationView?application_gid=' + val +  '&lspage=SanctionMaker');
        }

        $scope.maker_process = function (val, val1) {
            $location.url('app/MstSanctionDtlSummary?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lspage=SanctionMaker');
        }
    }
})();
