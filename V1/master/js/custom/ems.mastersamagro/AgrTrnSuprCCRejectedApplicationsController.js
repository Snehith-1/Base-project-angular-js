(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnSuprCCRejectedApplicationsController', AgrTrnSuprCCRejectedApplicationsController);

    AgrTrnSuprCCRejectedApplicationsController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrTrnSuprCCRejectedApplicationsController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnSuprCCRejectedApplicationsController';

        activate();
        lockUI();
        function activate() {
            var url = 'api/AgrTrnSuprCAD/GetCCRejectedSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.CCRejected_list = resp.data.cadapplicationlist;
            });
            var url = 'api/AgrTrnSuprCAD/CADApplicationCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.cadreview_count = resp.data.cadreview_count;
                $scope.sentbackcc_count = resp.data.sentbackcc_count;
                $scope.accept_count = resp.data.accept_count;
                $scope.backtounderwriting_count = resp.data.backtounderwriting_count;
                $scope.ccrejected_count = resp.data.ccrejected_count;
                $scope.lstotalcount = resp.data.lstotalcount;
            });
        }

        $scope.view = function (val) {
            $location.url('app/AgrMstSuprCadApplicationView?application_gid=' + val + '&lspage=CCRejectedApplications');
        }

        //$scope.edit = function (val) {
        //    $location.url('app/MstApplicationCreationView?application_gid=' + val + '&lstab=MyApplications');
        //}

        //$scope.process = function (val) {
        //    $location.url('app/MstApplicationCreationView?application_gid=' + val + '&lstab=MyApplications');
        //}

        $scope.pendincad_review = function () {
            $location.url('app/AgrTrnSuprPendingCADReview');
        }

        $scope.cadaccepted_customers = function () {
            $location.url('app/AgrTrnSuprCadAcceptedCustomers');
        }

        $scope.sendback_underwriting = function () {
            $location.url('app/AgrTrnSuprSentBackToUnderwriting');
        }

        $scope.sendback_cc = function () {
            $location.url('app/AgrTrnSuprSentBackToCC');
        }

        $scope.cc_rejected = function () {
            $location.url('app/AgrTrnSuprCCRejectedApplications');
        }
    }
})();
