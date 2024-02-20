(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHRLoanTermsandConditionsController', MstHRLoanTermsandConditionsController);

    MstHRLoanTermsandConditionsController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstHRLoanTermsandConditionsController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHRLoanTermsandConditionsController';

        activate();

        function activate() {
            var url = 'api/MstHRLoanTermsandConditions/GetHRLoanTermsandConditions';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.termsandconditions_data = resp.data.hrloantermsandconditions_list;
                unlockUI();
            });
        }
        $scope.addtermsandconditions = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addtermsandconditions.html',
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
                        hrloantermsandconditions_name: $scope.txttermsandconditions_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/MstHRLoanTermsandConditions/CreateHRLoanTermsandConditions';
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
                            $modalInstance.close('closed');
                            activate();
                        }
                    });
                }

            }
        }
        $scope.edittermsandconditions = function (hrloantermsandconditions_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/edittermsandconditions.html',
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
                    hrloantermsandconditions_gid: hrloantermsandconditions_gid
                }
                var url = 'api/MstHRLoanTermsandConditions/EditHRLoanTermsandConditions';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txtedittermsandconditions_name = resp.data.hrloantermsandconditions_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.hrloantermsandconditions_gid = resp.data.hrloantermsandconditions_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/MstHRLoanTermsandConditions/UpdateHRLoanTermsandConditions';
                    var params = {
                        hrloantermsandconditions_name: $scope.txtedittermsandconditions_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        hrloantermsandconditions_gid: $scope.hrloantermsandconditions_gid
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

        $scope.Status_update = function (hrloantermsandconditions_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statustermsandconditions.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    hrloantermsandconditions_gid: hrloantermsandconditions_gid
                }
                var url = 'api/MstHRLoanTermsandConditions/EditHRLoanTermsandConditions';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.hrloantermsandconditions_gid = resp.data.hrloantermsandconditionsgid
                    $scope.txttermsandconditions_name = resp.data.hrloantermsandconditions_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        hrloantermsandconditions_gid: hrloantermsandconditions_gid,
                        hrloantermsandconditions_name: $scope.txttermsandconditions_name,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MstHRLoanTermsandConditions/InactiveHRLoanTermsandConditions';
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
                        } activate();
                    });

                    $modalInstance.close('closed');

                }
                var params = {
                    hrloantermsandconditions_gid: hrloantermsandconditions_gid
                }

                var url = 'api/MstHRLoanTermsandConditions/InactiveHRLoanTermsandConditionsHistory';

                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.termsandconditionsinactivelog_data = resp.data.termsandconditionsinactivehistory_list;
                    unlockUI();
                });
            }
        }

        $scope.delete = function (hrloantermsandconditions_gid) {
            var params = {
                hrloantermsandconditions_gid: hrloantermsandconditions_gid
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
                    var url = 'api/MstHRLoanTermsandConditions/DeleteHRLoanTermsandConditions';
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
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