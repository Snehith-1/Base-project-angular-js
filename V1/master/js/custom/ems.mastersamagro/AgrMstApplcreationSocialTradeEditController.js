(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstApplcreationSocialTradeEditController', AgrMstApplcreationSocialTradeEditController);

    AgrMstApplcreationSocialTradeEditController.$inject = ['$rootScope', '$sce', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrMstApplcreationSocialTradeEditController($rootScope, $sce, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstApplcreationSocialTradeEditController';
       
       
        activate();
        function activate() { 
            $scope.application_gid = $location.search().lsapplication_gid;
            $scope.lstab = $location.search().lstab;
           var param = {
                 application_gid: $scope.application_gid
             }
             
             var url = 'api/AgrMstApplicationEdit/SocialAndTradeEdit';
           
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.SocialCapital = resp.data.social_capital;
                $scope.TradeCapital = resp.data.trade_capital;
                $scope.application_gid = resp.data.application_gid;
                unlockUI();
            }); 
        }
      
        $scope.SocialTradeapital_Back = function () {
            if ($scope.lstab == 'add') {
                $location.url('app/AgrMstApplicationGeneralAdd?lstab=' + $scope.lstab);
            }
            else {
                $state.go('app.AgrMstApplicationGeneralEdit');
            }
        }


        $scope.update_SocialTradeCapital = function () {
            var params = {
                social_capital: $scope.SocialCapital,
                trade_capital: $scope.TradeCapital,
                application_gid: $scope.application_gid,
            }
            var url = 'api/AgrMstApplicationEdit/SocialAndTradeCapitalUpdate';
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
                if ($scope.lstab == 'add') {
                    $location.url('app/AgrMstApplicationGeneralAdd?lstab=' + $scope.lstab);
                }
                else {
                    $state.go('app.AgrMstApplicationGeneralEdit');
                }
            });
    } 

    $scope.doneclick = function () {
        var url = 'api/AgrMstApplicationEdit/EditProceed';
        SocketService.get(url).then(function (resp) {
            $scope.proceed_flag = resp.data.proceed_flag;
        });
    }

    $scope.overallsubmit_application = function () {
        var url = 'api/AgrMstApplicationEdit/EditAppProceed';
        lockUI();
        SocketService.post(url, params).then(function (resp) {
            unlockUI();
            if (resp.data.status == true) {
                activate();
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
            $state.go('app.AgrMstApplicationCreationSummary');
        });

    }

       
       
    }
})();

