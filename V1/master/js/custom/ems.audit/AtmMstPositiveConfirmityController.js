(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmMstPositiveConfirmityController', AtmMstPositiveConfirmityController);

    AtmMstPositiveConfirmityController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AtmMstPositiveConfirmityController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmMstPositiveConfirmityController';
        activate();


        function activate() {

            var url = 'api/AtmMstPositiveConfirmity/GetPositiveConfirmity';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.positiveconfirmity_data = resp.data.positiveconfirmity_list;
                unlockUI();
            });
        }

        $scope.popuppositiveconfirmity = function () {
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

                $scope.positiveconfirmitySubmit = function () {
                    var params = {
                        positiveconfirmity_gid: $scope.positiveconfirmity_gid,
                        positiveconfirmity_name: $scope.txtpositive_confirmity,
                        positiveconfirmity_code: $scope.txtpositiveconfirmity_code,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    }

                    var url = 'api/AtmMstPositiveConfirmity/CreatePositiveConfirmity';
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

        $scope.editpositiveconfirmity = function (positiveconfirmity_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editpositiveconfirmity.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    positiveconfirmity_gid: positiveconfirmity_gid
                }
                var url = 'api/AtmMstPositiveConfirmity/EditPositiveConfirmity';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditpositiveconfirmity_code = resp.data.positiveconfirmity_code;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.txteditpositive_confirmity = resp.data.positiveconfirmity_name;
                    $scope.positiveconfirmity_gid = resp.data.positiveconfirmity_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.positiveconfirmityUpdate = function () {

                    var url = 'api/AtmMstPositiveConfirmity/UpdatePositiveConfirmity';
                    var params = {
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        positiveconfirmity_code: $scope.txteditpositiveconfirmity_code,
                        positiveconfirmity_name: $scope.txteditpositive_confirmity,
                        positiveconfirmity_gid: $scope.positiveconfirmity_gid
                    }
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
                           

                        }
                        else {
                            $modalInstance.close('closed');
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

            }
        }

        $scope.Status_update = function (positiveconfirmity_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statuspositiveconfirmity.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    positiveconfirmity_gid: positiveconfirmity_gid
                }
                
                var url = 'api/AtmMstPositiveConfirmity/EditPositiveConfirmity';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.positiveconfirmity_gid = resp.data.positiveconfirmity_gid
                    $scope.txtpositive_confirmity = resp.data.positiveconfirmity_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        positiveconfirmity_name: $scope.txtpositive_confirmity,
                        positiveconfirmity_gid: $scope.positiveconfirmity_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/AtmMstPositiveConfirmity/InactivePositiveConfirmity';
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
                    positiveconfirmity_gid: positiveconfirmity_gid
                }

                var url = 'api/AtmMstPositiveConfirmity/PositiveConfirmityInactiveLogview';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.positiveconfirmityinactivelog_data = resp.data.positiveconfirmity_list;
                    unlockUI();
                });
            }
        }

        //$scope.deletepositiveconfirmity = function (positiveconfirmity_gid) {
        //    var params = {
        //        positiveconfirmity_gid: positiveconfirmity_gid
        //    }
        //    //var url = 'api/AtmMstPositiveConfirmity/DeletePositiveConfirmity';
        //    SocketService.getparams(url, params).then(function (resp) {
        //        if (resp.data.status == true) {

        //            SweetAlert.swal({
        //                title: 'Are you sure?',
        //                text: 'Do You Want To Delete the Record ?',
        //                showCancelButton: true,
        //                confirmButtonColor: '#DD6B55',
        //                confirmButtonText: 'Yes, delete it!',
        //                closeOnConfirm: false
        //            }, function (isConfirm) {
        //                if (isConfirm) {
        //                    SweetAlert.swal('Deleted Successfully!');
        //                    unlockUI();
        //                    activate();
        //                }

        //            });
        //        }
        //        else {
        //            Notify.alert(resp.data.message, {
        //                status: 'warning',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
        //            unlockUI();
        //            activate();
        //        }
        //    });
        //};

        $scope.deletepositiveconfirmity = function (positiveconfirmity_gid) {
                var params = {
                    positiveconfirmity_gid: positiveconfirmity_gid
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
                    var url = 'api/AtmMstPositiveConfirmity/DeletePositiveConfirmity';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
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