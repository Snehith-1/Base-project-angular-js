(function () {
    'use strict';
    angular
           .module('angle')
           .controller('MstRMcustomerSummary', MstRMcustomerSummary);

    MstRMcustomerSummary.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function MstRMcustomerSummary($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstRMcustomerSummary';

        activate();

        function activate() {    
            lockUI();
            var url = 'api/MstCAD/GetRMCADUrnGroupingSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.urngroupingcustomer_list = resp.data.cadapplicationlist;
            });         
        }

        function urngroupingfunction() {
            lockUI();
            var url = 'api/MstCAD/GetRMCADUrnGroupingSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.urngroupingcustomer_list = resp.data.cadapplicationlist;
            });
        }

        $scope.myteamcustomer_list = function () {
            $location.url('app/MstRmMyTeamCustomerSummary');
        }

        $scope.urngroupingtab = function () {
            lockUI();
            var url = 'api/MstCAD/GetRMCADUrnGroupingSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.urngroupingcustomer_list = resp.data.cadapplicationlist;
            });
        }

        $scope.mycustomerlisttab = function () {
            lockUI();
            var url = 'api/MstCAD/GetRMMyCustomerListSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.Rmmycustomer_list = resp.data.cadapplicationlist;
            });
        }

        $scope.process = function (customer_urn) {
            $location.url('app/MstRMCustomerDashboard?customer_urn=' + customer_urn + '&lspage=RMCADUrnGroupingMain');
        }

        $scope.customerView = function (val) {
            $location.url('app/MstCadApplicationView?application_gid=' + val + '&lspage=CadRMCustomer');
        }

        $scope.mycustomerlist_view = function (val, val1) {
            $location.url('app/MstCadApplicationView?application_gid=' + val + '&employee_gid=' + val1 + '&lspage=RMMyCustomerList');
        }

        $scope.urngrouping_view = function (val, val1) {
            $location.url('app/MstCadApplicationView?application_gid=' + val + '&employee_gid=' + val1 + '&lspage=RMMyCustomerList');
        }

        $scope.withouturn_process = function (application_gid, val1) {
            $location.url('app/MstPostCcActivitiesRMView?application_gid=' + application_gid + '&lspage=MstRMCustomerSummary');
        }

        $scope.urn_grouping = function (val, customer_urn) {
            $location.url('app/MstRMCadUrnAcceptedCustomerDtls?application_gid=' + val + '&customer_urn=' + customer_urn + '&lspage=RMCADUrnGrouping');
        }

        $scope.limit_management = function (val, customer_urn) {
            $location.url('app/MstLimitManagementView?application_gid=' + val + '&customer_urn=' + customer_urn + '&lspage=MstRMCustomerSummary');
        }

        $scope.renewal = function (application_gid, customer_urn) {
            var modalInstance = $modal.open({
                templateUrl: '/Renewalpopup.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.renewedshow = false;
                $scope.renewalshow = true;
                $scope.renewl_confirm = function () {
                    var params = {
                        customer_urn: customer_urn
                    }
                    lockUI();
                    var url = 'api/MstApplicationAdd/PostRenewalAdd';
                    SocketService.post(url, params).then(function (resp) {                        
                        if (resp.data.status == true) {
                            unlockUI();
                            $scope.renewedapplication_no = resp.data.application_no;
                            $scope.renewedshow = true;
                            $scope.renewalshow = false;
                        }
                        else {
                            alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            urngroupingfunction();
                            unlockUI;
                        }
                    });
                }                

                $scope.ok = function () {
                    $modalInstance.close('closed');
                    urngroupingfunction();
                };


            }

        }

    }
})();
