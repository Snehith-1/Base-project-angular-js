(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstLoanTypeController', MstLoanTypeController);

        MstLoanTypeController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstLoanTypeController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstLoanTypeController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() { 
          
            var url = 'api/MstApplication360/GetLoanType';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.loantype_list = resp.data.loantype_list;
                unlockUI();
            });
        }

        $scope.addloantype = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addloantype.html',
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
                        loan_type: $scope.txtloantype_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    
                    }
                    var url = 'api/MstApplication360/CreateLoanType';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    }); 

                    $modalInstance.close('closed');

                }
                
            }
        }

        $scope.editloantype = function (loantype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editloantype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                 var params = {
                    loantype_gid: loantype_gid
                }
                 var url = 'api/MstApplication360/EditLoanType';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditloantype_name = resp.data.loan_type;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.loantype_gid = resp.data.loantype_gid;
                }); 
                
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                
               
                $scope.update = function () {

                    var url = 'api/MstApplication360/UpdateLoanType';
                    var params = {
                        loan_type: $scope.txteditloantype_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        loantype_gid: $scope.loantype_gid
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

        $scope.Status_update = function (loantype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusloantype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    loantype_gid: loantype_gid
                }
                var url = 'api/MstApplication360/EditLoanType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtloantype_name = resp.data.loan_type;
                   $scope.loantype_gid = resp.data.loantype_gid;
                    $scope.rbo_status = resp.data.status_log;
                });    
                var url = 'api/MstApplication360/GetLoanTypeActiveLog';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.loantype_list = resp.data.loantype_list;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        loan_type: $scope.txtloantype_name,
                        remarks: $scope.txtremarks,
                        status_log:$scope.rbo_status,
                        loantype_gid:loantype_gid
                    }
                    var url = 'api/MstApplication360/LoanTypeStatusUpdate';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }activate();
                    }); 

                    $modalInstance.close('closed');

                }
                
            }
        }

        $scope.delete = function (loantype_gid) {
             var params = {
                loantype_gid: loantype_gid
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
                            var url = 'api/MstApplication360/LoanTypeDelete';
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

