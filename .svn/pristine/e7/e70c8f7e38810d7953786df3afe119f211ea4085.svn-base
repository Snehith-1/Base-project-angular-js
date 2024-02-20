(function () {
    'use strict';

    angular
        .module('angle')
        .controller('RMDetails', RMDetails);

    RMDetails.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function RMDetails($rootScope, $scope, $modal, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'RMDetails';
        
        activate();
        function activate() {


            $scope.relationshipmgmt_gid = localStorage.getItem('relationshipmgmt_gid');
            var params = {
                relationshipmgmt_gid: $scope.relationshipmgmt_gid
            };
         
            var url = 'api/deferral/rmdeferraldetails';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.deferral_data = resp.data.deferralSummaryDtls;
                unlockUI();
            });
           
            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });


        }

        $scope.checkall = function (selected) {
            //console.log(selected);
            angular.forEach($scope.deferral_data, function (val) {
                val.checked = selected;
            });
        }

        $scope.back = function (val) {
            $state.go('app.transferRM');
        }

       

        $scope.transfer = function () {
            var deferralGidList = [];

            angular.forEach($scope.deferral_data, function (val) {

                if (val.checked == true) {
                    var deferral_gid = val.deferral_gid;
                    deferralGidList.push(deferral_gid);
                    
                }
            });
          
           
            var params = {
                deferral_gid: deferralGidList,
                employee_gid: $scope.employee_gid
            }
            //console.log(params);
            var url = 'api/deferral/deferralTransfer';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert('Transferred Successfully!', 'success');
                    $state.go('app.transferRM');
                }
                else {
                    Notify.alert('Select Atleast One!')                
                }
                
            });

        }

    }
})();