(function () {
    'use strict';

    angular
        .module('angle')
        .controller('categorycontroller', categorycontroller);

    categorycontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function categorycontroller($rootScope, $scope, $state, $modal, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'categorycontroller';

        activate();

        function activate() {

            var url = 'api/category/categorySummary';
            SocketService.get(url).then(function (resp) {
                $scope.category_list = resp.data.category_list;
                
            });
        }

        $scope.delete = function (category_gid) {
            var params = {
                category_gid: category_gid
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
                    var url = 'api/category/categoryDelete';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');
                        }
                        else {
                            Notify.alert('You can not delete this category because course had been created under this category', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 4000
                            });
                        }
                        activate();
                    });
                   
                }

            });
        };
        $scope.popupcategory = function () {
            var modalInstance = $modal.open({
                templateUrl: '/myModalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.addcategory = function () {
                    var params = {
                        category_code: $scope.txtcategorycode,
                        category_name: $scope.txtcategoryname
                    }

                    var url = 'api/category/addCategory';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert('Category Added Successfully..!!', 'success')
                        }
                        else {
                            Notify.alert('Category code already exist!', 'warning')
                        }
                        activate();
                    });
                    $state.go('app.category');
                }
            }
        }
        $scope.edit = function (category_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/myModaledit.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    category_gid: category_gid
                }
                var url = 'api/category/categoryUpdatedetails';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txtcategorycodeedit = resp.data.category_codeedit;
                    $scope.txtcategorynameedit = resp.data.category_nameedit;
                    $scope.category_gid = resp.data.category_gid;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.updatecategory = function () {

                    var params = {
                        category_codeedit: $scope.txtcategorycodeedit,
                        category_nameedit: $scope.txtcategorynameedit,
                        category_gid: category_gid
                    }
                    console.log();
                    var url = 'api/category/updateCategory';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert('Category Updated Successfully..!!', 'success')
                        }
                        else {
                            Notify.alert('Error Occurred While Updating Category !', 'success')
                        }
                        activate();
                    });
                }
            }

        }
    }
})();
