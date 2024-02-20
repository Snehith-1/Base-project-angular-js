﻿(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCadDisburseReqCheckerSummary', MstCadDisburseReqCheckerSummary);

    MstCadDisburseReqCheckerSummary.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstCadDisburseReqCheckerSummary($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCadDisburseReqCheckerSummary';

        activate();

        function activate() {
            var url = 'api/MstCAD/GetCADAcceptedCustomerSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.sanctionmaker_list = resp.data.cadapplicationlist;
            });
            var url = 'api/MstCAD/CADAppSanctionCount';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.cadmaker_count = resp.data.cadmaker_count;
                $scope.cadchecker_count = resp.data.cadchecker_count;
                $scope.cadcheckerapproval_count = resp.data.cadcheckerapproval_count;
            });
        }

        $scope.maker = function () {
            $location.url('app/MstCadDisbursementRequestSummary');
        }

        $scope.checker = function () {
            $location.url('app/MstCadDisburseReqCheckerSummary');
        }

        $scope.approval = function () {
            $location.url('app/MstCadDisburseReqApprovalSummary');
        }
    }
})();