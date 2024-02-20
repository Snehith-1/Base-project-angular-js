(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCourierCompanysummaryController', MstCourierCompanysummaryController);

    MstCourierCompanysummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCourierCompanysummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCourierCompanysummaryController';

        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            var url = 'api/MstApplication360/GetCourierCompany';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.courier_data = resp.data.application_list;
                unlockUI();
            });
        }

        $scope.addcouriercompany = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addcouriercompany.html',
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
                        couriercompany_name: $scope.txtcourier_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code,
                        description: $scope.txtdescription
                    }
                    var url = 'api/MstApplication360/CreateCourierCompany';
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

        $scope.editcouriercompany = function (couriercompany_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editcouriercompany.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    couriercompany_gid: couriercompany_gid
                }
                var url = 'api/MstApplication360/EditCourierCompany';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditcourier_name = resp.data.couriercompany_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.txteditdescription = resp.data.description;
                    $scope.couriercompany_gid = resp.data.couriercompany_gid;
                });


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/MstApplication360/UpdateCourierCompany';
                    var params = {
                        couriercompany_name: $scope.txteditcourier_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        description: $scope.txteditdescription,
                        couriercompany_gid: $scope.couriercompany_gid
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

        $scope.Status_update = function (couriercompany_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statuscouriercompany.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    couriercompany_gid: couriercompany_gid
                }
                var url = 'api/MstApplication360/EditCourierCompany';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.couriercompany_gid = resp.data.couriercompany_gid
                    $scope.txtcourier_name = resp.data.couriercompany_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        couriercompany_gid: couriercompany_gid,                       
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status,
                        couriercompany_name: $scope.txtcourier_name

                    }
                    var url = 'api/MstApplication360/InactiveCourierCompany';
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
                    couriercompany_gid: couriercompany_gid
                }

                var url = 'api/MstApplication360/CourierCompanyInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.courierinactivelog_data = resp.data.application_list;
                    unlockUI();
                });

            }
        }

        $scope.delete = function (couriercompany_gid) {
            var params = {
                couriercompany_gid: couriercompany_gid,
                
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
                    var url = 'api/MstApplication360/DeleteCourierCompany';
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
                            unlockUI();
                        }
                    });
                }
            });
        }
    }
})();

