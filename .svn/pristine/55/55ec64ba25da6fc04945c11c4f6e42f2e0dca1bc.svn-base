(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCadPhysicalDocFollowupStatusController', MstCadPhysicalDocFollowupStatusController);

    MstCadPhysicalDocFollowupStatusController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstCadPhysicalDocFollowupStatusController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCadPhysicalDocFollowupStatusController';

        activate();

        function activate() {
            lockUI();
            var url = 'api/MstPhysicalDocument/GetCADPhysicalDocFollowupSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.scannedcheckerapprovalpendinglist = resp.data.physicalmakerapplication;
            });


        }


      
        $scope.Completed = function () {
            $location.url('app/MstCadPhysicalDocCompletedSummary');
        }

        
    }
})();
