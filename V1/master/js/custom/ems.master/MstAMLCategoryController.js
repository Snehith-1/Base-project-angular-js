(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstAMLCategoryController', MstAMLCategoryController);

        MstAMLCategoryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstAMLCategoryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstAMLCategoryController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() { 
            var url = 'api/MstApplication360/GetAmlCategory';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.amlcategory_data = resp.data.application_list;
                unlockUI();

            });
        }

        $scope.addAMLCategory = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addAMLCategory.html',
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
                        amlcategory_name: $scope.txtaml_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    
                    }
                    var url = 'api/MstApplication360/CreateAmlCategory';
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

        $scope.editAMLCategory = function (amlcategory_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editAMLCategory.html',
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
                    amlcategory_gid: amlcategory_gid
                }
                var url = 'api/MstApplication360/EditAmlCategory';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditaml_name = resp.data.amlcategory_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.amlcategory_gid = resp.data.amlcategory_gid;
                });
                $scope.update = function () {

                    var params = {
                        amlcategory_name: $scope.txteditaml_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        amlcategory_gid: $scope.amlcategory_gid
                    }
                    var url = 'api/MstApplication360/UpdateAmlcategory';
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

        $scope.Status_update = function (amlcategory_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusAMLCategory.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                                
                var params = {
                    amlcategory_gid: amlcategory_gid
                }

                var url = 'api/MstApplication360/EditAmlCategory';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.amlcategory_gid = resp.data.amlcategory_gid
                    $scope.txtamlcategory_name = resp.data.amlcategory_name;
                    $scope.rbo_status = resp.data.Status;
                });
                           
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        amlcategory_name: $scope.txtamlcategory_name,
                        amlcategory_gid: $scope.amlcategory_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status
                    
                    }
                    var url = 'api/MstApplication360/InactiveAmlCategory';
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
                    amlcategory_gid: amlcategory_gid
                }

                var url = 'api/MstApplication360/InactiveAmlCategoryHistory';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.amlinactivelog_lists = resp.data.inactivehistory_list;
                    unlockUI();
                });
            }
        }

        $scope.delete = function (amlcategory_gid) {
              var params = {
                 amlcategory_gid: amlcategory_gid
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
                            var url = 'api/MstApplication360/DeleteAmlCategory';
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

