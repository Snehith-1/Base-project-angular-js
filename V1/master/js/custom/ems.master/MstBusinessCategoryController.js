(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstBusinessCategoryController', MstBusinessCategoryController);

    MstBusinessCategoryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstBusinessCategoryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstBusinessCategoryController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() {
            
            var url = 'api/MstApplication360/GetBusinessCategory';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.businesscategory_data = resp.data.application_list;
                unlockUI();
              
            });
        }

        $scope.addBusinessCategory = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addBusinessCategory.html',
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
                $scope.submit = function () {

                    var params = {
                        businesscategory_name: $scope.txtBusinessCategory_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/MstApplication360/CreateBusinessCategory';
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

        $scope.editBusinessCategory = function (businesscategory_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editBusinessCategory.html',
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

                var params = {
                    businesscategory_gid: businesscategory_gid
                }
                var url = 'api/MstApplication360/EditBusinessCategory';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditBusinessCategory_name = resp.data.businesscategory_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.businesscategory_gid = resp.data.businesscategory_gid;
                });

                $scope.update = function () {

                    var url = 'api/MstApplication360/UpdateBusinessCategory';
                    var params = {
                        businesscategory_name: $scope.txteditBusinessCategory_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        businesscategory_gid: $scope.businesscategory_gid
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

        $scope.Status_update = function (businesscategory_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusBusinessCategory.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    businesscategory_gid: businesscategory_gid
                }
                var url = 'api/MstApplication360/EditBusinessCategory';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.businesscategory_gid = resp.data.businesscategory_gid
                    $scope.txteditBusinessCategory_name = resp.data.businesscategory_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        businesscategory_name: $scope.txteditBusinessCategory_name,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status,
                        businesscategory_gid: $scope.businesscategory_gid

                    }

                    var url = 'api/MstApplication360/InactiveBusinessCategory';
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
                var param = {
                    businesscategory_gid: businesscategory_gid
                }

                var url = 'api/MstApplication360/BusinessCategoryInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.businesscategoryinactivelog_data = resp.data.application_list;
                    
                    unlockUI();
                });
            }
        }

        $scope.delete = function (businesscategory_gid) {
            var params = {
                businesscategory_gid: businesscategory_gid
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
                            var url = 'api/MstApplication360/DeleteBusinessCategory';
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

