(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditEconomicCapitalEditController', MstCreditEconomicCapitalEditController);

        MstCreditEconomicCapitalEditController.$inject = ['$rootScope', '$sce', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCreditEconomicCapitalEditController($rootScope, $sce, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCreditEconomicCapitalEditController';
        $scope.institution_gid = $location.search().institution_gid;
        var institution_gid = $scope.institution_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
       
        activate();
        function activate() { 
            
            var param = {
                applicant_type: 'Institution',
                credit_gid: institution_gid,
             }
             
            var url = 'api/MstAppCreditUnderWriting/EditSocialAndTradeCapital';
           
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.SocialCapital = resp.data.social_capital;
                $scope.TradeCapital = resp.data.trade_capital;
                unlockUI();
            });  

            vm.submitted = false;
            vm.validateInput = function(name, type) {
              var input = vm.formValidate[name];
              return (input.$dirty || vm.submitted) && input.$error[type];
            };
  
            // Submit form
            vm.submitForm = function() {
              vm.submitted = true;
              if (vm.formValidate.$valid) {
              } else {
                return false;
              }
            };
        }
      
        $scope.SocialTradeapital_Back = function () {
            $location.url('app/MstCreditEconomicCapitalAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + $scope.lspage);
        }

        $scope.update_SocialTradeCapital = function () {
            if (($scope.SocialCapital == undefined) || ($scope.TradeCapital == undefined) || ($scope.SocialCapital == null) || ($scope.TradeCapital == null) || ($scope.SocialCapital == '') || ($scope.TradeCapital == '') ) {
                Notify.alert('Enter All Mandatory Fields','warning');
            }
            else {
            var params = {
                social_capital: $scope.SocialCapital,
                trade_capital: $scope.TradeCapital,
                applicant_type: 'Institution',
                institution_gid: institution_gid,
            }
            var url = 'api/MstAppCreditUnderWriting/SocialAndTradeCapitalUpdate';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                $location.url('app/MstCreditEconomicCapitalAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + $scope.lspage);
            });
    }
} 
       
    }
})();

