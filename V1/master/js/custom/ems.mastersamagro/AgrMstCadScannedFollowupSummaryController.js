(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstCadScannedFollowupSummaryController', AgrMstCadScannedFollowupSummaryController);

    AgrMstCadScannedFollowupSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AgrMstCadScannedFollowupSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstCadScannedFollowupSummaryController';

        activate();

        function activate() {
            lockUI();
            var url = 'api/AgrMstScannedDocument/GetCADScannedDocFollowupSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedcheckerapprovalpendinglist = resp.data.scannedmakerapplication;
                unlockUI();
            });
           
           
        }

       

        $scope.Completed = function () {
            $location.url('app/AgrMstScannedCompletedSummary');
        }

       
    }
})();