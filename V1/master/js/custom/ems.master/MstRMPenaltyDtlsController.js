(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstRMPenaltyDtlsController', MstRMPenaltyDtlsController);

        MstRMPenaltyDtlsController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstRMPenaltyDtlsController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstRMPenaltyDtlsController';
        var application_gid = $location.search().application_gid;


        activate();

        function activate() { 
            var params = {
                application_gid : application_gid
            }
             var url = 'api/MstApplicationView/GetPenaltyView';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.penality_list = resp.data.alldatamodified_List;
                unlockUI();
            }); 
        }
        
        $scope.Back = function () {
            $location.url('app/MstPostCcActivitiesRMView?application_gid=' + application_gid);
        }
    }
})();

