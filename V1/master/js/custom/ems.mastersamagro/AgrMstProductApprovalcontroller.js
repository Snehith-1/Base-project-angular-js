(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstProductApprovalcontroller', AgrMstProductApprovalcontroller);

    AgrMstProductApprovalcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrMstProductApprovalcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstProductApprovalcontroller';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;


        activate();

        function activate() {
            var url = 'api/AgrMstProductPmgApproval/GetProductApproval';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.productapproval_list = resp.data.ProductApprovaldtl;
                unlockUI();

            });
        }

        $scope.addProductApproval = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addProductApproval.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var url = 'api/SystemMaster/GetEmployeelist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.cboApprovalname_list = resp.data.employeelist;
                    unlockUI();
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {

                    var params = {
                        productapproval_gid: $scope.cboapproval_name.employee_gid,
                        productapproval_name: $scope.cboapproval_name.employee_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code 
                    }
                    var url = 'api/AgrMstProductPmgApproval/CreateProductApproval';
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
                        activate();
                    });

                    $modalInstance.close('closed');

                }

            }
        }

        $scope.editProductApproval = function (mstproductapproval_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editProductApproval.html',
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
                var url = 'api/SystemMaster/GetEmployeelist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.cboApprovalname_list = resp.data.employeelist;
                    unlockUI();
                });


                var params = {
                    mstproductapproval_gid: mstproductapproval_gid
                }
                var url = 'api/AgrMstProductPmgApproval/EditProductApproval';
                SocketService.getparams(url, params).then(function (resp) {

                    //$scope.cboeditapproval_name = resp.data.productapproval_gid;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.cboeditapproval_name = resp.data.productapproval_name;
                });
                $scope.update = function () {
                    var lsproductapproval_gid = "", lsproductapproval_name = "";
                    //if ($scope.cboeditapproval_name != undefined || $scope.cboeditapproval_name != null) {
                    //    lsproductapproval_gid = $scope.cboeditapproval_name;
                    //    lsproductapproval_name = $scope.cboApprovalname_list.find(function (x) { return x.employee_gid === $scope.cboeditapproval_name.employee_name })
                    //}
                    var params = {
                        mstproductapproval_gid: mstproductapproval_gid,
                        //productapproval_gid: lsproductapproval_gid,
                        //productapproval_name: lsproductapproval_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code  
                    }
                    var url = 'api/AgrMstProductPmgApproval/UpdateProductApproval';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
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
                            activate();
                        }
                    });

                }

            }
        }
         

        $scope.delete = function (mstproductapproval_gid) {
            var params = {
                mstproductapproval_gid: mstproductapproval_gid
            }

            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();
                    var url = 'api/AgrMstProductPmgApproval/DeleteProductApproval';
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');
                            activate();
                        }
                        else {
                            alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            unlockUI;
                        }
                    });
                }
            });
        }
    }
})();
