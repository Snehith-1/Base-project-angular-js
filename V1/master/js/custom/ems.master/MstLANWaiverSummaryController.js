(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstLANWaiverSummaryController', MstLANWaiverSummaryController);

    MstLANWaiverSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstLANWaiverSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstLANWaiverSummaryController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        
        activate();
        lockUI();
        function activate() {
            var url = 'api/MstLANWaiver/GetLANWaiver';
            SocketService.get(url).then(function (resp) {
                $scope.lanwaiver_list = resp.data.lanwaiver;
                unlockUI();
            });
        }

        //Add

        $scope.addlanwaiver = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addlanwaiver.html',
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
                        lanwaiver_code: $scope.txtlanwaiver_code,
                        lanwaiver_name: $scope.txtlanwaiver_name,
                        description: $scope.txtdescription
                    }
                    lockUI();
                    var url = 'api/MstLANWaiver/PostLANWaiver';
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
                            $modalInstance.close('closed');
                        }
                    });
                }
            }
        }


        //Edit

        $scope.editlanwaiver = function (lanwaiver_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editlanwaiver.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    lanwaiver_gid: lanwaiver_gid
                }
                lockUI();
                var url = 'api/MstLANWaiver/GetLANEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lanwaivercodeedit = resp.data.lanwaiver_code;
                    $scope.txteditlanwaiver_name = resp.data.lanwaiver_name;
                    $scope.txteditdescription = resp.data.description;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {
                    lockUI();
                    var url = 'api/MstLANWaiver/UpdateLANWaiver';
                    var params = {
                        lanwaiver_gid: lanwaiver_gid,
                        lanwaiver_code: $scope.lanwaivercodeedit,
                        lanwaiver_name: $scope.txteditlanwaiver_name,
                        description: $scope.txteditdescription,
                    }
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
                            $modalInstance.close('closed');
                        }
                    });
                }
            }
        }


        // Showoverpopup

        $scope.description = function (description) {
            var modalInstance = $modal.open({
                templateUrl: '/description.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.description = description;
                $scope.back = function () {
                    $modalInstance.close('closed');
                };
            }
        }


        //Status

        $scope.Status_update = function (lanwaiver_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statuslanwaiver.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    lanwaiver_gid: lanwaiver_gid
                }
                lockUI();
                var url = 'api/MstLANWaiver/GetLANEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lanwaivercodeedit = resp.data.lanwaiver_code;
                    $scope.txteditlanwaiver_name = resp.data.lanwaiver_name;
                    $scope.txteditdescription = resp.data.description;
                    $scope.rbo_status = resp.data.Status;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        lanwaiver_gid: lanwaiver_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status
                    }
                    lockUI();
                    var url = 'api/MstLANWaiver/InactiveLANWaiver';
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
                        } activate();
                    });

                    $modalInstance.close('closed');
                }
                var params = {
                    lanwaiver_gid: lanwaiver_gid
                }
                lockUI();
                var url = 'api/MstLANWaiver/InactiveLANWaiverHistory';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.lanwaiverinactivelog_data = resp.data.laninactivehistory_list;
                    unlockUI();
                });
            }
        }

        //Delete

        $scope.delete = function (lanwaiver_gid) {
            var params = {
                lanwaiver_gid: lanwaiver_gid
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
                    var url = 'api/MstLANWaiver/DeleteLANWaiver';
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
                        }
                    });
                }
            });
        }
    }
})();