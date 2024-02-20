(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnShortClosingController', AgrTrnShortClosingController);

    AgrTrnShortClosingController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrTrnShortClosingController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnShortClosingController';

        activate();
        //lockUI();
        function activate() {
            lockUI();
            var url = 'api/AgrTrnCAD/GetShortClosingSummary';
            SocketService.get(url).then(function (resp) {

              
                    $scope.Pendingcadreview_list = resp.data.cadapplicationlist;
                    unlockUI();
               
            });

        }

        $scope.view = function (val) {
            $location.url('app/AgrMstCadApplicationView?application_gid=' + val + '&lspage=ShortClosing');
        }

    }
})();
