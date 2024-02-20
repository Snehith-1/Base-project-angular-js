(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstAmortizationmethodSummaryController', MstAmortizationmethodSummaryController);

    MstAmortizationmethodSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstAmortizationmethodSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstAmortizationmethodSummaryController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() {
            var url = 'api/MstApplication360/Getamortization_method';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.amortizationmethod_list = resp.data.amortizationmethod_list;
                unlockUI();
            });
        }

        $scope.addamortizationmethod = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addamortizationmethod.html',
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
                        amortization_method: $scope.txtamortization_method,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/MstApplication360/Createamortization_method';
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

        $scope.editamortizationmethod = function (amortization_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editamortizationmethod.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                
                var params = {
                    amortization_gid: amortization_gid
                }
                 var url = 'api/MstApplication360/Editamortization_mtd';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditamortization_method = resp.data.amortization_method;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.amortization_gid = resp.data.amortization_gid;
                });
                
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update = function () {

                    var url = 'api/MstApplication360/Updateamortization_mtd';
                     var params = {
                         amortization_method: $scope.txteditamortization_method,
                         lms_code: $scope.txteditlms_code,
                         bureau_code: $scope.txteditbureau_code,
                         amortization_gid: $scope.amortization_gid
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

        $scope.Status_update = function (amortization_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusamortizationmethod.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    amortization_gid: amortization_gid
                }

                var url = 'api/MstApplication360/Editamortization_mtd';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txtamortization_method = resp.data.amortization_method;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.amortization_gid = resp.data.amortization_gid;
                    $scope.rbo_status = resp.data.status_log;
                });
                var url = 'api/MstApplication360/GetamortizationActiveLog';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.amortizationmethod_list = resp.data.amortizationmethod_list;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        amortization_method: $scope.txtamortization_method,
                        remarks: $scope.txtremarks,
                        status_log: $scope.rbo_status,
                        amortization_gid: amortization_gid
                    }
                    var url = 'api/MstApplication360/amortization_mtdStatusUpdate';
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

        $scope.delete = function (amortization_gid) {
             var params = {
                 amortization_gid: amortization_gid
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
                    var url = 'api/MstApplication360/amortization_mtdDelete';
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

