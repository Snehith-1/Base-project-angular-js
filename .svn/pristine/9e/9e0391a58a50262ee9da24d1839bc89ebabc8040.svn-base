(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstTypeofDebtSummaryController', MstTypeofDebtSummaryController);

    MstTypeofDebtSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstTypeofDebtSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstTypeofDebtSummaryController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() {
            var url = 'api/MstApplication360/GetTypeofDebt';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.TypeofDebt_lists = resp.data.application_list;
                unlockUI();
            });
        }

        $scope.addtypeofdebt = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addtypeofdebt.html',
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
                        typeofdebt_name: $scope.txttypeof_debt,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/MstApplication360/CreateTypeofDebt';
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

        $scope.edittypeofdebt = function (typeofdebt_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/edittypeofdebt.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                
                var params = {
                    typeofdebt_gid: typeofdebt_gid
                }
                var url = 'api/MstApplication360/EditTypeofDebt';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txtedittypeof_debt = resp.data.typeofdebt_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.typeofdebt_gid = resp.data.typeofdebt_gid;
                });
                
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update = function () {

                    var url = 'api/MstApplication360/UpdateTypeofDebt';
                     var params = {
                         typeofdebt_name: $scope.txtedittypeof_debt,
                         typeofdebt_gid: $scope.typeofdebt_gid,
                         lms_code: $scope.txteditlms_code,
                         bureau_code: $scope.txteditbureau_code
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

        $scope.Status_update = function (typeofdebt_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statustypeofdebt.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    typeofdebt_gid: typeofdebt_gid
                }
                var url = 'api/MstApplication360/EditTypeofDebt';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txttypeofdebt_name = resp.data.typeofdebt_name;
                    $scope.typeofdebt_gid = resp.data.typeofdebt_gid;
                    $scope.loanpurpose_gid = resp.data.loanpurpose_gid;
                    $scope.rbo_status = resp.data.Status;
                });  

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        typeofdebt_name: $scope.txttypeofdebt_name,
                        typeofdebt_gid: $scope.typeofdebt_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MstApplication360/InactiveTypeofDebt';
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

                    $modalInstance.close('closed');

                }
                var param = {
                    typeofdebt_gid: typeofdebt_gid
                }

                var url = 'api/MstApplication360/InactiveTypeofDebtHistory';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.TypeofDebtinactivelog_lists = resp.data.inactivehistory_list;
                    unlockUI();
                });
            }
        }

        $scope.delete = function (typeofdebt_gid) {
             var params = {
                 typeofdebt_gid: typeofdebt_gid
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
                    var url = 'api/MstApplication360/DeleteTypeofDebt';
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