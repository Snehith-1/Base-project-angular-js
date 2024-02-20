(function () {
    'use strict';

    angular
        .module('angle')
        .controller('welcomecontroller', welcomecontroller);

    welcomecontroller.$inject = ['$rootScope', '$scope', '$state', '$cookies', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route'];

    function welcomecontroller($rootScope, $scope, $state, $cookies, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'welcomecontroller';

        activate();

        function activate() {
            lockUI();
            
            var params = {
                externaluser_gid: localStorage.getItem('externaluser_gid')
            }

            var url = "api/epwelcome/GetExternaldetails";
            SocketService.getparams(url, params).then(function (resp) {
                
                $scope.externaluser_code = resp.data.externaluser_code;
                $scope.externaluser_name = resp.data.externaluser_name;

                if (resp.data.external_photo != "N") {
                    var pathArray = location.href.split('/');
                    var protocol = pathArray[0];
                    var host = pathArray[2];
                    var url = protocol + '//' + host;
                    var str = resp.data.external_photo;
                    var str = str.split("StoryboardAPI");
                    var relpath1 = str[1].replace("\\", "/");
                    $scope.external_photo = url.concat(relpath1);
                }
                else {
                    $scope.external_photo = resp.data.external_photo;
                }
                $scope.count_allocation = resp.data.count_allocation;
            
            });
            unlockUI();
        }

        $scope.logout = function () {
            $state.go('page.user_login');
            var name = 'token';
            document.cookie = name + '=; Path=/ep; Expires=Thu, 01 Jan 2020 00:00:01 GMT;';
        }

        $scope.dashboard = function () {
            $state.go('app.caseAllocation');
        }

        $scope.legalSR = function () {
            $state.go('app.legalSR');
        }
 
    }
})();
