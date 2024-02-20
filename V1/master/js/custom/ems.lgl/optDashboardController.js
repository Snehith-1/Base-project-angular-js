(function () {
    'use strict';

    angular
        .module('angle')
        .controller('optDashboardcontroller', optDashboardcontroller);

    optDashboardcontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies'];

    function optDashboardcontroller($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'optDashboardcontroller';

        activate();

        function activate() {
            $scope.master = true;
            $scope.legalsr = true;
            $scope.misdataimport = true;
            var url = 'api/optDashboard/getprivilegeGID';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                if (resp.data.master_list != null) {
                    if (resp.data.master_list.length == 1) {
                        if ((resp.data.master_list[0].Gid) == 'OPTMSTADM') {
                            $scope.master = false;
                            $scope.misdataimport = false;
                        }
                        else {
                            $scope.misdataimport = true;
                        }

                        if ((resp.data.master_list[0].Gid) == 'OPTMSTLSR') {
                            $scope.master = false;
                            $scope.legalsr = false;

                        }
                        else {
                            $scope.legalsr = true;
                        }
                    }

                    if (resp.data.master_list.length == 2) {
                        if (((resp.data.master_list[0].Gid) == 'OPTMSTADM') || ((resp.data.master_list[1].Gid) == 'OPTMSTADM')) {
                            $scope.master = false;
                            $scope.misdataimport = false;
                        }
                        else {
                            $scope.misdataimport = true;
                        }
                        if (((resp.data.master_list[1].Gid) == 'OPTMSTLSR') || ((resp.data.master_list[1].Gid) == 'OPTMSTLSR')) {
                            $scope.master = false;
                            $scope.legalsr = false;
                        }
                        else {
                            $scope.legalsr = true;
                        }
                    }
                }
                else {
                    $scope.master = true;
                    $scope.misdataimport = true;
                    $scope.legalsr = true;
                }
            });
        }

    

        
        $scope.misdataImport = function () {
            $state.go('app.misDataimport');
        }


        $scope.legalSR = function () {
            $state.go('app.legalSRsummary');
        }

      

      
    }
})();
