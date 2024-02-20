/**=========================================================
 * Module: access-register.js
 * Demo for register account api
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.pages')
        .controller('forgotpasswordController', forgotpasswordController);

    forgotpasswordController.$inject = ['$rootScope', '$scope', '$state', '$cookies', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route'];

    function forgotpasswordController($rootScope, $scope, $state, $cookies, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route) {
        /* jshint validthis:true */
        var vm = this;

        activate();

        ////////////////

        function activate() {

            $scope.password = true;
            $scope.details = false;

        }
        $scope.btnsubmit = function () {

            var params = {
                lawyer_email_id: $scope.email_id,
                user_code: $scope.user_code
            }
            console.log(params);
            lockUI();
            var url = "api/welcome/GetLawyerEmail";

            SocketService.post(url, params).then(function (resp) {
                console.log(resp.data.status);
                if (resp.data.status == true) {
                    //  $scope.message='Mail Sent';
                    $scope.password = false;
                    $scope.details = true;

                    unlockUI();
                }
                else {
                   // $scope.return_message = 'Error';
                    $scope.details = false;
                    $scope.invalid = true;
                    unlockUI();
                }

            });
        }
        $scope.pwd_submit = function () {
            var params = {
                lawyer_password: $scope.lawyer_password,
                user_code: $scope.user_code,
                lawyer_email_id: $scope.email_id

            }
            console.log(params);
            lockUI();
            var url = "api/welcome/updatelawyerpassword";

            SocketService.post(url, params).then(function (resp) {
                console.log(resp.data.status);
                if (resp.data.status == true) {
                    //  $scope.message='Mail Sent';
                    $state.go('page.user_login');
                    unlockUI();
                }
                else {
                    //  $scope.return_message='Error';
                    $scope.details = false;
                    $scope.invalid = true;
                    unlockUI();
                }

            });
        }
    }
})();
