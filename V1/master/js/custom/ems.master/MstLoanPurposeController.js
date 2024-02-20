(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstLoanPurposeController', MstLoanPurposeController);

    MstLoanPurposeController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstLoanPurposeController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstLoanPurposeController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() {
            
            var url = 'api/MstApplication360/GetLoanPurpose';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.LoanPurpose_data = resp.data.application_list;
                unlockUI();

            });
        }


        $scope.addloanpurpose = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addloanpurpose.html',
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
                        loanpurpose_name: $scope.txtloan_purpose,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/MstApplication360/CreateLoanPurpose';
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

        $scope.editloanpurpose = function (loanpurpose_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editloanpurpose.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
               
                var params = {
                    loanpurpose_gid: loanpurpose_gid
                }
                var url = 'api/MstApplication360/EditLoanPurpose';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditloan_purpose = resp.data.loanpurpose_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.loanpurpose_gid = resp.data.loanpurpose_gid;
                });
                
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update = function () {

                    var url = 'api/MstApplication360/UpdateLoanpurpose';
                    var params = {
                        loanpurpose_name: $scope.txteditloan_purpose,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        loanpurpose_gid: $scope.loanpurpose_gid
                    }
                    var url = 'api/MstApplication360/UpdateLoanpurpose';
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

        $scope.Status_update = function (loanpurpose_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusloanpurpose.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    loanpurpose_gid: loanpurpose_gid
                }

                var url = 'api/MstApplication360/EditLoanPurpose';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.loanpurpose_gid = resp.data.loanpurpose_gid
                    $scope.txtloanpurpose_name = resp.data.loanpurpose_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function (loanpurpose_gid) {

                    var params = {
                        loanpurpose_gid: $scope.loanpurpose_gid,
                        loanpurpose_name: $scope.txtloanpurpose_name,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MstApplication360/InactiveLoanPurpose';
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
                    loanpurpose_gid: loanpurpose_gid
                }

                var url = 'api/MstApplication360/InactiveLoanPurposeHistory';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.loanpurposeinactivelog_lists = resp.data.inactivehistory_list;
                    unlockUI();
                });
            }
        }

        $scope.delete = function (loanpurpose_gid) {
              var params = {
                 loanpurpose_gid: loanpurpose_gid
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
                    var url = 'api/MstApplication360/DeleteLoanPurpose';
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

