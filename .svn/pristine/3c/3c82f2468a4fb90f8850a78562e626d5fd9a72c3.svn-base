(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCCRejectedApplicationsController', MstCCRejectedApplicationsController);

    MstCCRejectedApplicationsController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCCRejectedApplicationsController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCCRejectedApplicationsController';

        activate();
        //lockUI();
        function activate() {
            lockUI();
            var url = 'api/MstCAD/GetCCRejectedSummary';
            SocketService.get(url).then(function (resp) {
                if (resp.data.cadapplicationlist != null && resp.data.cadapplicationlist.length > 0) {
                $scope.CCRejected_list = resp.data.cadapplicationlist;
                unlockUI();
            }
            else if (resp.data.status == false)
            unlockUI();
            });
            var url = 'api/MstCAD/CADApplicationCount';
       
            SocketService.get(url).then(function (resp) {
               
                $scope.cadreview_count = resp.data.cadreview_count;
                $scope.sentbackcc_count = resp.data.sentbackcc_count;
                $scope.accept_count = resp.data.accept_count;
                $scope.backtounderwriting_count = resp.data.backtounderwriting_count;
                $scope.ccrejected_count = resp.data.ccrejected_count;
                $scope.lstotalcount = resp.data.lstotalcount;
                $scope.urngrouping_count = resp.data.urngrouping_count;
            });
        }

        $scope.view = function (val,val1) {
            $location.url('app/MstCadPendingApplicationView?application_gid=' + val + '&employee_gid=' + val1 + '&lspage=CCRejectedApplications');
        }

        //$scope.edit = function (val) {
        //    $location.url('app/MstApplicationCreationView?application_gid=' + val + '&lstab=MyApplications');
        //}

        //$scope.process = function (val) {
        //    $location.url('app/MstApplicationCreationView?application_gid=' + val + '&lstab=MyApplications');
        //}

        $scope.pendincad_review = function () {
            $location.url('app/MstPendingCADReview');
        }

        $scope.urn_grouping = function () {
            $location.url('app/MstCadUrnAcceptedCustomers');
        }

        $scope.cadaccepted_customers = function () {
            $location.url('app/MstCadAcceptedCustomers');
        }

        $scope.sendback_underwriting = function () {
            $location.url('app/MstSentBackToUnderwriting');
        }

        $scope.sendback_cc = function () {
            $location.url('app/MstSentBackToCC');
        }

        $scope.cc_rejected = function () {
            $location.url('app/MstCCRejectedApplications');
        }
    }
})();
