(function () {
    'use strict';

    angular
        .module('angle')
        .controller('landingcontroller', landingcontroller);

    landingcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function landingcontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'landingcontroller';

        activate();


        function activate() {
            var url = apiManage.apiList['notification'].api;
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
               
                $scope.count_acknowledgement = resp.data.count_acknowledgement;
                $scope.count_myasset = resp.data.count_myasset;
                $scope.count_surrender = resp.data.count_surrender;
                $scope.count_temporaryhandover = resp.data.count_temporaryhandover;
                $scope.employee_id = resp.data.employee_id;
                $scope.count_response = resp.data.count_response;
                $scope.count_myapprovals = resp.data.count_myapprovals;
            });
        }
        $scope.view = function (val,count) {
                $scope.employee_id = val;
                ScopeValueService.store("ackCtrl", $scope);
                $state.go('app.viewasset');
        };
        $scope.acknowledge = function (val, count) {
            $scope.employee_id = val;
            ScopeValueService.store("ackCtrl", $scope);
            $state.go('app.acknowledgemyasset');
        };

        $scope.surrender = function (val, count) {
                $scope.employee_id = val;
                ScopeValueService.store("ackCtrl", $scope);
                $state.go('app.assetsurrender');
        };

        $scope.temporaryhandover = function (val, count) {
                $scope.employee_id = val;
                ScopeValueService.store("ackCtrl", $scope);
                $state.go('app.temporaryhandover');
        };

         $scope.newserviceticket = function (val) {
         $scope.employee_id = val;
         ScopeValueService.store("ackCtrl", $scope);
         $state.go('app.newservice_ticket');
        };
         $scope.newtaskrequest = function (val) {
             $scope.employee_id = val;
             ScopeValueService.store("ackCtrl", $scope);
             $state.go('app.newtaskrequest');
         };
        
         $scope.viewserviceticket = function (val) {
             $scope.employee_id = val;
             ScopeValueService.store("ackCtrl", $scope);
             $state.go('app.viewserviceticket');
         };
         $scope.myapprovals = function (val) {
             $scope.employee_id = val;
             ScopeValueService.store("ackCtrl", $scope);
             $state.go('app.myapprovals');
         };
         $scope.loanManagement = function (val) {
             $scope.employee_id = val;
             ScopeValueService.store("ackCtrl", $scope);
             $state.go('app.loanManagement');
         };
         $scope.rmManagement = function (val) {
             $scope.employee_id = val;
             ScopeValueService.store("ackCtrl", $scope);
             $state.go('app.rmManagement');
         };
         $scope.deferralManagement = function (val) {
             $scope.employee_id = val;
             ScopeValueService.store("ackCtrl", $scope);
             $state.go('app.DeferralManagement');
         };
         $scope.segment = function (val) {
             $scope.employee_id = val;
             ScopeValueService.store("ackCtrl", $scope);
             $state.go('app.segment');
         };
         $scope.deferral = function (val) {
             $scope.employee_id = val;
             ScopeValueService.store("ackCtrl", $scope);
             $state.go('app.deferral');
         };
         $scope.covenantType = function (val) {
             $scope.employee_id = val;
             ScopeValueService.store("ackCtrl", $scope);
             $state.go('app.covenantType');
         };
         $scope.transferRM = function (val) {
             $scope.employee_id = val;
             ScopeValueService.store("ackCtrl", $scope);
             $state.go('app.transferRM');
         };
         $scope.customerMaster = function (val) {
             $scope.employee_id = val;
             ScopeValueService.store("ackCtrl", $scope);
             $state.go('app.customerMaster');
         };
         $scope.cadApproval = function (val) {
             $scope.employee_id = val;
             ScopeValueService.store("ackCtrl", $scope);
             $state.go('app.cadApproval');
         };

         $scope.extensionApproval = function (val) {
             $scope.employee_id = val;
             ScopeValueService.store("ackCtrl", $scope);
             $state.go('app.extensionApproval');
         };
         $scope.adminlogin = function () {
                 var url = apiManage.apiList['SValues'].api;
                 lockUI();
                 SocketService.get(url).then(function (resp) {
                     unlockUI();
                     var host = window.location.host;
                     var prefix = "https://"
                     var win= window.open(prefix.concat(host,"/Framework/adlogin.aspx?userCode=",resp.data.user_code,"&?&userPassword=",resp.data.user_password,"&?&companyCode=",resp.data.company_code),'_blank');
                    win.focus();
                 })
             };
    }
})();