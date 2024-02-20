(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstLoanTermPeriodSummaryController', MstLoanTermPeriodSummaryController);

    MstLoanTermPeriodSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstLoanTermPeriodSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstLoanTermPeriodSummaryController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() {
            var url = 'api/MstApplication360/GetLoanTermPeriod';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.loanterm_period_list = resp.data.loanterm_period_list;
                unlockUI();
            });
        }

        $scope.addloantermperiod = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addloantermperiod.html',
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
                        loanterm_period: $scope.txtloan_Term_period,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/MstApplication360/CreateLoanTermPeriod';
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

        $scope.editloantermperiod = function (loantermperiod_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editloantermperiod.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                
                var params = {
                    loantermperiod_gid: loantermperiod_gid
                }
                var url = 'api/MstApplication360/EditLoanTermPeriod';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditloan_Term_period = resp.data.loanterm_period;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.loantermperiod_gid = resp.data.loantermperiod_gid;
                });
                
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update = function () {

                    var url = 'api/MstApplication360/UpdateLoanTermPeriod';
                     var params = {
                         loanterm_period: $scope.txteditloan_Term_period,
                         lms_code: $scope.txteditlms_code,
                         bureau_code: $scope.txteditbureau_code,
                         loantermperiod_gid:loantermperiod_gid
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

        $scope.Status_update = function (loantermperiod_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusloantermperiod.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    loantermperiod_gid: loantermperiod_gid
                }
                var url = 'api/MstApplication360/EditLoanTermPeriod';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txtloan_term_period = resp.data.loanterm_period;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.loantermperiod_gid = resp.data.loantermperiod_gid;
                    $scope.rbo_status = resp.data.status_log;
                });
                var url = 'api/MstApplication360/GetLoanTermPeriodActiveLog';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.loanterm_period_list = resp.data.loanterm_period_list;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        loanterm_period: $scope.txtloan_term_period,
                        remarks: $scope.txtremarks,
                        status_log: $scope.rbo_status,
                        loantermperiod_gid:loantermperiod_gid
                    }
                    var url = 'api/MstApplication360/LoanTermPeriodStatusUpdate';
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

        $scope.delete = function (loantermperiod_gid) {
              var params = {
                 loantermperiod_gid: loantermperiod_gid
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
                    var url = 'api/MstApplication360/LoanTermPeriodDelete';
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

