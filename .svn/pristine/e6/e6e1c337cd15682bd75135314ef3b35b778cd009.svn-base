(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstInsuranceCompanyController', AgrMstInsuranceCompanyController);

    AgrMstInsuranceCompanyController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrMstInsuranceCompanyController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstInsuranceCompanyController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;


        activate();

        function activate() {
            var url = 'api/AgrMstSamAgroMaster/GetInsuranceCompanySummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.insurancecompany_list = resp.data.insurancecompany_list;
                unlockUI();
            });
        }
       

        $scope.addinsurancecompany = function () {
            $location.url('app/AgrMstInsuranceCompanyAdd');
        }
       
        $scope.editinsurancecompany = function (insurancecompany_gid) {
            $location.url('app/AgrMstInsuranceCompanyEdit?lsinsurancecompany_gid=' + insurancecompany_gid);
        }

        $scope.viewinsurancecompany = function (insurancecompany_gid) {
            $location.url('app/AgrMstInsuranceCompanyView?lsinsurancecompany_gid=' + insurancecompany_gid);
        }
        
       
        $scope.Status_update = function (insurancecompany_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusinsurancecompany.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    insurancecompany_gid: insurancecompany_gid
                }
                var url = 'api/AgrMstSamAgroMaster/EditInsuranceCompany';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.insurancecompany_gid = resp.data.insurancecompany_gid
                    $scope.txtinsurancecompany_name = resp.data.insurancecompany_name;
                    $scope.rbo_status = resp.data.rbo_status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        insurancecompany_gid: insurancecompany_gid,
                        insurancecompany_name: $scope.txtinsurancecompany_name,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/AgrMstSamAgroMaster/InactiveInsuranceCompany';
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
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        activate();
                    });

                    $modalInstance.close('closed');

                }

                var param = {
                    insurancecompany_gid: insurancecompany_gid
                }

                var url = 'api/AgrMstSamAgroMaster/InsuranceCompanyInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.insurancecompanylog_list = resp.data.insurancecompany_list;
                    unlockUI();
                });

            }
        }


    }
})();

