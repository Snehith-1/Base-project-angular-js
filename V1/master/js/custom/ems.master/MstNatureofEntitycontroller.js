(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstNatureofEntitycontroller', MstNatureofEntitycontroller);

    MstNatureofEntitycontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function MstNatureofEntitycontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstNatureofEntitycontroller';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            var url = 'api/MstApplication360/GetNatureofEntity';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.natureofentity_list = resp.data.application_list;
                unlockUI();
            });

        }



        //<!--ADD CODE START-->
        $scope.popupNatureofEntity = function () {
            var modalInstance = $modal.open({
                templateUrl: '/natureofentityadd.html',
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

                $scope.NatureofEntitySubmit = function () {
                    var params = {
                        NatureofEntity_name: $scope.txtaddNatureofEntity_name,
                        bureau_code: $scope.txtbureau_code,
                        lms_code: $scope.txtlms_code
                    }
                    
                    var url = 'api/MstApplication360/CreateNatureofEntity';
                    SocketService.post(url, params).then(function (resp) {
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
        //<!--ADD CODE END-->



        //<!--EDIT CODE START-->


        $scope.editNatureofEntity = function (natureofentity_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editNatureofEntity.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    natureofentity_gid: natureofentity_gid

                }
                var url = 'api/MstApplication360/EditNatureofEntity';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditNatureofEntity_name = resp.data.natureofentity_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.natureofentity_gid = resp.data.natureofentity_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/MstApplication360/UpdateNatureofEntity';
                    var params = {
                        natureofentity_name: $scope.txteditNatureofEntity_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        natureofentity_gid: $scope.natureofentity_gid
                    }
                    SocketService.post(url, params).then(function (resp) {
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

        //<!--EDIT CODE END-->


        //<!--STATUS CODE START-->


        $scope.Status_update = function (natureofentity_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusNatureofEntity.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    natureofentity_gid: natureofentity_gid
                }
                var url = 'api/MstApplication360/EditNatureofEntity';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.natureofentity_gid = resp.data.natureofentity_gid;
                    $scope.txtnatureofentity_name = resp.data.natureofentity_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        natureofentity_gid: natureofentity_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MstApplication360/InactiveNatureofEntity';
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
                    natureofentity_gid: natureofentity_gid
                }

                var url = 'api/MstApplication360/NatureofEntityInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.natureofentityinactivelog_list = resp.data.application_list;
                    unlockUI();
                });

            }
        }
        //<!--STATUS CODE END-->

        //<!--DELETE CODE START-->

        $scope.delete = function (natureofentity_gid) {
            var params = {
                natureofentity_gid: natureofentity_gid
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
                    var url = 'api/MstApplication360/DeleteNatureofEntity';
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
        //<!--DELETE CODE END-->

    }
})();
