(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditMappingAddController', MstCreditMappingAddController);

    MstCreditMappingAddController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstCreditMappingAddController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCreditMappingAddController';

        activate();

        function activate() {
            var url = 'api/SystemMaster/GetEmployeelist';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.cbocredithead_list = resp.data.employeelist;
                unlockUI();
            });
            var url = 'api/SystemMaster/GetEmployeelist';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.cbonationalmanager_list = resp.data.employeelist;
                unlockUI();
            });
            var url = 'api/SystemMaster/GetEmployeelist';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.cboregionalmanager_list = resp.data.employeelist;
                unlockUI();
            });
            var url = 'api/SystemMaster/GetEmployeelist';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.cbocreditmanager_list = resp.data.employeelist;
                unlockUI();
            });
        }
        $scope.submit = function () {

            var params = {
                Credithead: $scope.cbocredithead,
                Creditnationalmanager: $scope.cbonationalmanager,
                Creditregionalmanager: $scope.cboregionalmanager,
                CreditManager: $scope.cbocreditmanager,
                creditgroup_name: $scope.txtcreditgroupname

            }
            var url = 'api/MstCreditMapping/PostCreditGroupAdd';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.MstCreditMappingSummary');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
            });

        

        }

        $scope.back = function () {
            $state.go('app.MstCreditMappingSummary');
        };

     

    }
})();