(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHardCodedTypeController', MstHardCodedTypeController);

        MstHardCodedTypeController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService'];

    function MstHardCodedTypeController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHardCodedTypeController';
        $scope.lsoneapihardcodevalue = $location.search().lsoneapihardcodevalue;
        var lsoneapihardcodevalue = $scope.lsoneapihardcodevalue;
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() { 

            var url = 'api/SystemMaster/GetCityList';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.getcity_list = resp.data.getcity_list;
                unlockUI();
            });

        }
        

       
    }
})();

