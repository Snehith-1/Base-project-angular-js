(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSoftcopyVettingFollowupReportController', MstSoftcopyVettingFollowupReportController);

    MstSoftcopyVettingFollowupReportController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstSoftcopyVettingFollowupReportController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSoftcopyVettingFollowupReportController';

        activate();

        function activate() {
            lockUI();
            var url = 'api/MstScannedDocument/GetCADScannedDocFollowupSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.scannedmakerpendinglist = resp.data.scannedmakerapplication;
            });

           
            var url = 'api/MstScannedDocument/CADAppScannedDocCount';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

      

        $scope.Completed = function () {
            $location.url('app/MstScannedCompletedSummary');
        }

    }
})();