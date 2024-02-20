(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstRMDeviationDtlsController', MstRMDeviationDtlsController);

        MstRMDeviationDtlsController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstRMDeviationDtlsController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstRMDeviationDtlsController';
        var application_gid = $location.search().application_gid;


        activate();

        function activate() { 
            var params = {
                application_gid : application_gid
            }
           var url = 'api/MstApplicationView/GetDeviationView';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Deviation_list = resp.data.alldatamodified_List;
                unlockUI();
            }); 
        }
        
        $scope.Back = function () {
            $location.url('app/MstPostCcActivitiesRMView?application_gid=' + application_gid);
        }
    }
})();

