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
                lawyerregister_gid: localStorage.getItem('lawyeruser_gid')
            }

            var url = "api/welcome/Getlawyerdetails";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lawyer_name = resp.data.lawyer_name;
                $scope.educational_qualification = resp.data.educational_qualification,
                $scope.lawyeruser_code = resp.data.lawyeruser_code;

                if (resp.data.lawyer_photo != "N") {
                    var pathArray = location.href.split('/');
                    var protocol = pathArray[0];
                    var host = pathArray[2];
                    var url = protocol + '//' + host;
                    var str = resp.data.lawyer_photo;
                    str = str.substring(str.indexOf("StoryboardAPI/") + 3);
                    $scope.lawyer_photo = url.concat(str);
                }
                else {
                    $scope.lawyer_photo = resp.data.lawyer_photo;
                }
                $scope.count_compliance = resp.data.count_compliance;
                $scope.count_legalSR = resp.data.count_legalSR;
                $scope.count_invoice = resp.data.count_invoice;
            });
            unlockUI();
        }

        $scope.dashboard = function () {
            $state.go('app.documentCompliance');
        }

        $scope.legalSR = function () {
            $state.go('app.legalSR');
        }

        $scope.invoice = function () {
            $state.go('app.invoiceSummary');
        }
    }
})();
