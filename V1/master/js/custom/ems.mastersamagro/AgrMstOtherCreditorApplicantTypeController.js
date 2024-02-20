(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstOtherCreditorApplicantTypeController', AgrMstOtherCreditorApplicantTypeController);

    AgrMstOtherCreditorApplicantTypeController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrMstOtherCreditorApplicantTypeController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstOtherCreditorApplicantTypeController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;


        activate();

        function activate() {
            var url = 'api/AgrMstApplication360/GetOtherCreditorApplicantType';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.othercreditorapplicanttype_data = resp.data.application_list;
                unlockUI();
            });
        }

        $scope.addothercreditorapplicanttype = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addothercreditorapplicanttype.html',
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
                        othercreditorapplicanttype_name: $scope.txtothercreditor_applicanttype,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    }
                    var url = 'api/AgrMstApplication360/CreatetOtherCreditorApplicantType';
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
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                    });
                }

            }
        }

        $scope.editothercreditorapplicanttype = function (othercreditorapplicanttype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editothercreditorapplicanttype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    othercreditorapplicanttype_gid: othercreditorapplicanttype_gid
                }
                var url = 'api/AgrMstApplication360/EditOtherCreditorApplicantType';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditothercreditor_applicanttype = resp.data.othercreditorapplicanttype_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.othercreditorapplicanttype_gid = resp.data.othercreditorapplicanttype_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update = function () {
                    var url = 'api/AgrMstApplication360/UpdateOtherCreditorApplicantType';
                    var params = {
                        othercreditorapplicanttype_name: $scope.txteditothercreditor_applicanttype,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        othercreditorapplicanttype_gid: $scope.othercreditorapplicanttype_gid
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
                            activate();
                        }
                    });
                }
            }
        }

        $scope.Status_update = function (othercreditorapplicanttype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusothercreditorapplicanttype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    othercreditorapplicanttype_gid: othercreditorapplicanttype_gid
                }
                var url = 'api/AgrMstApplication360/EditOtherCreditorApplicantType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.othercreditorapplicanttype_gid = resp.data.othercreditorapplicanttype_gid
                    $scope.txtothercreditor_applicanttype = resp.data.othercreditorapplicanttype_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        othercreditorapplicanttype_gid: $scope.othercreditorapplicanttype_gid,
                        othercreditorapplicanttype_name: $scope.txtothercreditor_applicanttype,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/AgrMstApplication360/InactiveOtherCreditorApplicantType';
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
                    othercreditorapplicanttype_gid: othercreditorapplicanttype_gid
                }

                var url = 'api/AgrMstApplication360/OtherCreditorApplicantTypeInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.othercreditorapplicanttypeinactivelog_data = resp.data.application_list;
                    unlockUI();
                });
            }
        }

        $scope.delete = function (othercreditorapplicanttype_gid) {
            var params = {
                othercreditorapplicanttype_gid: othercreditorapplicanttype_gid
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
                    var url = 'api/AgrMstApplication360/DeleteOtherCreditorApplicantType';
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

