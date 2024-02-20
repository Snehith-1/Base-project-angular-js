(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnCadPhysicalDocFollowupSummaryController', AgrTrnCadPhysicalDocFollowupSummaryController);

    AgrTrnCadPhysicalDocFollowupSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AgrTrnCadPhysicalDocFollowupSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnCadPhysicalDocFollowupSummaryController';

        activate();

        function activate() {
            lockUI();
            var url = 'api/AgrTrnPhysicalDocument/GetCADPhysicalDocFollowupSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedcheckerapprovalpendinglist = resp.data.physicalmakerapplication;
                unlockUI();
            });

           
        }

       

     

        $scope.Completed = function () {
            $location.url('app/AgrTrnCadPhysicalDocCompletedSummary');
        }

        

    }
})();
