/**=========================================================
 * Module: access-login.js
 * Demo for login api
 =========================================================*/

(function() {
    'use strict';

    angular
        .module('app.pages')
        .controller('LoginFormController', LoginFormController);

    LoginFormController.$inject = ['$rootScope', '$scope', '$state', '$cookies', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];
    function LoginFormController($rootScope, $scope, $state, $cookies, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        var vm = this;

        activate();

        ////////////////

        function activate() {
            var name = 'token';
            document.cookie = name + '=; Path=/ep; Expires=Thu, 01 Jan 2020 00:00:01 GMT;';
          // bind here all data from the form
          vm.account = {};
          // place the message if something goes wrong
          vm.authMsg = '';

          vm.login = function () {
              vm.authMsg = '';


              if (vm.loginForm.$valid) {

                  var params = {
                      user_code: $scope.login.account.name,
                      user_password: $scope.login.account.password
                  }
                  var url = 'api/Login/externalVendorUserLogin';
                  lockUI();
                  SocketService.post(url, params).then(function (resp) {
                      if (resp.data.user_gid != "") {
                          console.log(resp.data.user_gid);
                          $cookies.putObject('token', resp.data.token);
                          localStorage.setItem('externaluser_gid', resp.data.user_gid);
                          $state.go('app.welcome');
                          unlockUI();
                      }
                      else if ((resp.data.user_gid == "") || (resp.data.user_gid == null)) {
                          unlockUI();
                          vm.authMsg = 'Invalid Credentials';
                      }
                      else {
                          unlockUI();
                          vm.authMsg = 'Error Occured';
                      }
                  });

              }
              else {
                  // set as dirty if the user click directly to login so we show the validation messages
                  /*jshint -W106*/
                  vm.loginForm.user_name.$dirty = true;
                  vm.loginForm.account_password.$dirty = true;
              }
          };
        }
    }
})();
