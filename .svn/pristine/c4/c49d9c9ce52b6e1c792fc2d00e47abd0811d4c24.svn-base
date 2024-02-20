  (function () {
      'use strict';

      angular
          .module('angle')
          .controller('MstBREExecutionViewController', MstBREExecutionViewController);

      MstBREExecutionViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];
      function MstBREExecutionViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
          /* jshint validthis:true */
          var vm = this;
          vm.title = 'MstBREExecutionViewController';
          $scope.postruleenginerun_gid = $location.search().postruleenginerun_gid;
          var postruleenginerun_gid = $location.search().postruleenginerun_gid;
          activate();
          function activate() {
              var param = {
                  postruleenginerun_gid: $scope.postruleenginerun_gid
              }
              var url = 'api/MstRuleEngine/PostExecuteSummaryView';
              lockUI();
              SocketService.getparams(url, param).then(function (resp) {
                  unlockUI();
                  $scope.postexecute_list = resp.data.postexecute_list;
                  $scope.template_code = resp.data.template_code;
                  $scope.template_name = resp.data.template_name;
                  $scope.application_no = resp.data.application_no;
                  $scope.percentage = resp.data.percentage;
              });

              $scope.back = function () {
                  $state.go('app.MstBREExecution');
              }
          }
          }
      })();