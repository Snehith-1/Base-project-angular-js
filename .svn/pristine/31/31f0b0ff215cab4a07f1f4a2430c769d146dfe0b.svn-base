(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstTurnovercontroller', MstTurnovercontroller);

    MstTurnovercontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function MstTurnovercontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstTurnovercontroller';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            var url = 'api/MstApplication360/GetTurnover';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.turnover_list = resp.data.application_list;
                unlockUI();
            });

        }



        //<!--ADD CODE START-->
        $scope.popupTurnover = function () {
            var modalInstance = $modal.open({
                templateUrl: '/turnoveradd.html',
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

                $scope.TurnoverSubmit = function () {
                    var params = {
                        Turnover_name: $scope.txtaddTurnover_name,
                        bureau_code: $scope.txtbureau_code,
                        lms_code: $scope.txtlms_code
                    }
                    
                    var url = 'api/MstApplication360/CreateTurnover';
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


        $scope.editTurnover = function (turnover_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editTurnover.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    turnover_gid: turnover_gid

                }
                var url = 'api/MstApplication360/EditTurnover';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditTurnover_name = resp.data.turnover_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.turnover_gid = resp.data.turnover_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/MstApplication360/UpdateTurnover';
                    var params = {
                        turnover_name: $scope.txteditTurnover_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        turnover_gid: $scope.turnover_gid
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


        $scope.Status_update = function (turnover_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusTurnover.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    turnover_gid: turnover_gid
                }
                var url = 'api/MstApplication360/EditTurnover';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.turnover_gid = resp.data.turnover_gid;
                    $scope.txtturnover_name = resp.data.turnover_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        turnover_gid: turnover_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MstApplication360/InactiveTurnover';
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
                    turnover_gid: turnover_gid
                }

                var url = 'api/MstApplication360/TurnoverInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.turnoverinactivelog_list = resp.data.application_list;
                    unlockUI();
                });

            }
        }
        //<!--STATUS CODE END-->


        //<!--DELETE CODE START-->

        $scope.delete = function (turnover_gid) {
            var params = {
                turnover_gid: turnover_gid
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
                    var url = 'api/MstApplication360/DeleteTurnover';
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
