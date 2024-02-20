(function () {
    'use strict';

    angular
        .module('angle')
        .controller('packageAccessoriescontroller', packageAccessoriescontroller);

    packageAccessoriescontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function packageAccessoriescontroller($rootScope, $scope, $state, $modal, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'packageAccessoriescontroller';

        activate();

        function activate() {
            var url = 'api/accessories/accessoriesSummary';
            SocketService.get(url).then(function (resp) {
                $scope.accessories_list = resp.data.accessories_list;

            });
        }
        $scope.delete = function (packageaccessories_gid) {
            var params = {
                packageaccessories_gid: packageaccessories_gid
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
                    var url = 'api/accessories/accessoriesDelete';
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
        $scope.popupaccessories = function () {
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
                $scope.addaccessories = function () {
                    var params = {
                        accessories_code: $scope.txtaccessories_code,
                        accessories_name: $scope.txtaccessories_name,
                        accessories_amnt: $scope.txtaccessories_amnt
                    }

                    var url = 'api/accessories/addAccessories';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert('Accessories Added Successfully..!!', 'success')
                        }
                        else {
                            Notify.alert('Accessories code already exist!', 'warning')
                        }
                        activate();
                    });
                    $state.go('app.Accessories');
                }
            }
        }
        $scope.edit = function (packageaccessories_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/myModaledit.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    packageaccessories_gid: packageaccessories_gid
                }
                var url = 'api/accessories/accessoriesUpdatedetails';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.accessories_codeedit = resp.data.accessories_codeedit;
                    $scope.accessories_nameedit = resp.data.accessories_nameedit;
                    $scope.txtaccessories_amntedit = resp.data.txtaccessories_amntedit;
                    $scope.packageaccessories_gid = resp.data.packageaccessories_gid;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.updateaccessories = function () {

                    var params = {
                        accessories_codeedit: $scope.accessories_codeedit,
                        accessories_nameedit: $scope.accessories_nameedit,
                        txtaccessories_amntedit: $scope.txtaccessories_amntedit,
                        packageaccessories_gid: packageaccessories_gid
                    }
                    console.log();
                    var url = 'api/accessories/updateAccessories';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert('Accessories Updated Successfully..!!', 'success')
                        }
                        else {
                            Notify.alert('Error Occurred While Updating Accessories !', 'success')
                        }
                        activate();
                    });
                }
            }

        }
    }
})();
