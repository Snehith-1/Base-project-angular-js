(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnPmgAdvanceRejectedSummaryController', AgrTrnPmgAdvanceRejectedSummaryController);

    AgrTrnPmgAdvanceRejectedSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrTrnPmgAdvanceRejectedSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnPmgAdvanceRejectedSummaryController';

        activate();
        lockUI();
        function activate() {
            var url = 'api/AgrTrnCAD/GetPMGAdvanceRejectedSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.CCRejected_list = resp.data.cadapplicationlist;
            });
            var url = 'api/AgrTrnCAD/CADApplicationCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.cadreview_count = resp.data.cadreview_count;
                $scope.sentbackcc_count = resp.data.sentbackcc_count;
                $scope.accept_count = resp.data.accept_count;
                $scope.backtounderwriting_count = resp.data.backtounderwriting_count;
                $scope.ccrejected_count = resp.data.ccrejected_count;
                $scope.advancerejected_count = resp.data.advancerejected_count;
                $scope.lstotalcount = resp.data.lstotalcount;
            });
        }

        $scope.view = function (val) {
            $location.url('app/AgrMstCadApplicationView?application_gid=' + val + '&lspage=AdvanceRejected');
        }

        //$scope.edit = function (val) {
        //    $location.url('app/MstApplicationCreationView?application_gid=' + val + '&lstab=MyApplications');
        //}

        //$scope.process = function (val) {
        //    $location.url('app/MstApplicationCreationView?application_gid=' + val + '&lstab=MyApplications');
        //}

        $scope.pendincad_review = function () {
            $location.url('app/AgrTrnPendingCADReview');
        }

        $scope.cadaccepted_customers = function () {
            $location.url('app/AgrTrnCadAcceptedCustomers');
        }

        $scope.sendback_underwriting = function () {
            $location.url('app/AgrTrnSentBackToUnderwriting');
        }

        $scope.sendback_cc = function () {
            $location.url('app/AgrTrnSentBackToCC');
        }

        $scope.cc_rejected = function () {
            $location.url('app/AgrTrnCCRejectedApplications');
        }

        $scope.Advance_rejected = function () {
            $location.url('app/AgrTrnPmgAdvanceRejectedSummary');
        }

    }
})();
