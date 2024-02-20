(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndMstQuestionnarieCategoryController', FndMstQuestionnarieCategoryController);

    FndMstQuestionnarieCategoryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function FndMstQuestionnarieCategoryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndMstQuestionnarieCategoryController';
        activate();


        function activate() {

            var url = 'api/FndMstCategoryTypeMaster/GetCategoryType';

            lockUI();
            SocketService.get(url).then(function (resp) {
                console.log(url);
                $scope.categorytype_data = resp.data.categorytype_list;
                unlockUI();
            });
        }

        $scope.popupcategorytype = function () {
            var modalInstance = $modal.open({
                templateUrl: '/myModalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md',
                backdrop: 'static',
                keyboard: false,
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.categorytypeSubmit = function () {
                    var params = {
                        categorytype_gid: $scope.categorytype_gid,
                        categorytype_name: $scope.txtcategory_type,
                        categorytype_code: $scope.txtcategorytype_code,
                        lms_code: $scope.txtlms_code,
                        remarks: $scope.txtaddremarks,
                        bureau_code: $scope.txtbureau_code
                    }

                    var url = 'api/FndMstCategoryTypeMaster/CreateCategoryType';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert('Category Type Added Successfully..!!', 'success')
                            activate();

                        }
                        else {
                            Notify.alert(resp.data.message, 'warning')


                        }
                    });
                    $modalInstance.close('closed');
                    activate();
                }

            }
        }

        $scope.editcategorytype = function (categorytype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editcategorytype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    categorytype_gid: categorytype_gid
                }
                var url = 'api/FndMstCategoryTypeMaster/EditCategoryType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditcategorytype_code = resp.data.categorytype_code;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.txteditcategory_type = resp.data.categorytype_name;
                    $scope.txteditremarks = resp.data.remarks;
                    $scope.categorytype_gid = resp.data.categorytype_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.categorytypeUpdate = function () {

                    var url = 'api/FndMstCategoryTypeMaster/UpdateCategoryType';
                    var params = {
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        categorytype_code: $scope.txteditcategorytype_code,
                        categorytype_name: $scope.txteditcategory_type,
                        remarks: $scope.txteditremarks,
                        categorytype_gid: $scope.categorytype_gid
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


        $scope.showPopover = function (categorytype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/showremarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    categorytype_gid: categorytype_gid
                }
                var url = 'api/FndMstCategoryTypeMaster/EditCategoryType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditcategorytype_code = resp.data.categorytype_code;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.txteditcategory_type = resp.data.categorytype_name;
                    $scope.txteditremarks = resp.data.remarks;
                    $scope.categorytype_gid = resp.data.categorytype_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

               

            }
        }

        $scope.Status_update = function (categorytype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statuscategorytype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    categorytype_gid: categorytype_gid
                }
                var url = 'api/FndMstCategoryTypeMaster/EditCategoryType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.categorytype_gid = resp.data.categorytype_gid
                    $scope.txtcategory_type = resp.data.categorytype_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        categorytype_name: $scope.txtcategory_type,
                        categorytype_gid: $scope.categorytype_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/FndMstCategoryTypeMaster/InactiveCategoryType';
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
                    categorytype_gid: categorytype_gid
                }

                var url = 'api/FndMstCategoryTypeMaster/CategoryTypeInactiveLogview';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.categorytypeinactivelog_data = resp.data.categorytype_list;
                    unlockUI();
                });
            }
        }


        $scope.deletecategorytype = function (categorytype_gid) {
            var params = {
                categorytype_gid: categorytype_gid
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
                    var url = 'api/FndMstCategoryTypeMaster/DeleteCategoryType';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Category Type !!!', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000

                            });
                            activate();
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        };
    }

})();