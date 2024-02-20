(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstProductHeadController', SysMstProductHeadController);

    SysMstProductHeadController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstProductHeadController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstProductHeadController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() { 

            var url = 'api/SystemMaster/GetProductHeadSummary';
        lockUI();
        SocketService.get(url).then(function (resp) {
            $scope.producthead_list = resp.data.producthead_list;
            unlockUI();
        });
    }

    $scope.addproducthead = function () {
        var modalInstance = $modal.open({
            templateUrl: '/addproducthead.html',
            controller: ModalInstanceCtrl,
            backdrop: 'static',
            keyboard: false,
            size: 'md'
        });

        ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        function ModalInstanceCtrl($scope, $modalInstance) {

            $scope.ok = function () {
                $modalInstance.close('closed');
            };

            var url = 'api/SystemMaster/GetZonallist';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.zone_list = resp.data.zone_list;
                unlockUI();
            });
         
            var url = 'api/SystemMaster/GetEmployeelist';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employeelist;
                unlockUI();
            });

            $scope.submit = function () {

                var params = {
                    product_gid: $scope.cboproduct.zone_gid,
                    product_name: $scope.cboproduct.zone_name,
                    employee_gid: $scope.cboemployee.employee_gid,
                    employee_name: $scope.cboemployee.employee_name
                }
               
                var url = 'api/SystemMaster/PostProductheadAdd';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $modalInstance.close('closed');
                        activate();
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                });

                $modalInstance.close('closed');

            }

        }
    }
    $scope.editproduct = function (producthead_gid) {
        var modalInstance = $modal.open({
            templateUrl: '/editproducthead.html',
            controller: ModalInstanceCtrl,
            backdrop: 'static',
            keyboard: false,
            size: 'md'
        });

        ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        function ModalInstanceCtrl($scope, $modalInstance) {
            var params = {
                producthead_gid: producthead_gid
            }
            var url = 'api/SystemMaster/GetProductHeadEdit';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.cboproductedit = resp.data.product_gid,
                 $scope.cboemployeeedit = resp.data.employee_gid,
                $scope.zone_list = resp.data.zone_list,
                $scope.employee_list = resp.data.employeelist
            });


            $scope.ok = function () {
                $modalInstance.close('closed');
            };

            $scope.update = function () {
                var productname;
                var product_index = $scope.zone_list.map(function (e) { return e.zone_gid }).indexOf($scope.cboproductedit);
                if (product_index == -1) { productname = ''; } else { productname = $scope.zone_list[product_index].zone_name; };

              
                var employeename;
                var employee_index = $scope.employee_list.map(function (e) { return e.employee_gid }).indexOf($scope.cboemployeeedit);
                if (employee_index == -1) { employeename = ''; } else { employeename = $scope.employee_list[employee_index].employee_name; };

                var url = 'api/SystemMaster/PostProductHeadUpdate';
                var params = {
                    product_gid: $scope.cboproductedit,
                    product_name: productname,
                    employee_gid: $scope.cboemployeeedit,
                    employee_name: employeename,
                    producthead_gid: producthead_gid
                }
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $modalInstance.close('closed');
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        activate();

                    }
                    else {
                        $modalInstance.close('closed');
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                });
                $modalInstance.close('closed');
            }
        }
    }

    $scope.Status_update = function (producthead_gid) {
        var modalInstance = $modal.open({
            templateUrl: '/statusproducthead.html',
            controller: ModalInstanceCtrl,
            backdrop: 'static',
            keyboard: false,
            size: 'md'
        });
        ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        function ModalInstanceCtrl($scope, $modalInstance) {
            var params = {
                producthead_gid: producthead_gid
            }
            var url = 'api/SystemMaster/GetProductHeadEdit';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtproducthead_name = resp.data.employee_name,
                $scope.rbo_status = resp.data.producthead_status

            });



            $scope.ok = function () {
                $modalInstance.close('closed');
            };
            $scope.update_status = function () {

                var params = {
                    producthead_gid: producthead_gid,
                    employee_name: $scope.txtclusterhead_name,
                    remarks: $scope.txtremarks,
                    rbo_status: $scope.rbo_status

                }
                var url = 'api/SystemMaster/InactiveProducthead';
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
                producthead_gid: producthead_gid
            }

            var url = 'api/SystemMaster/ProductheadInactiveLogview';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.productheadinactivelog_list = resp.data.master_list;
                unlockUI();
            });

        }
    }
}
})();
