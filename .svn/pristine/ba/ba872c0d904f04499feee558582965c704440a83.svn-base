(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstIncomeTypeController', MstIncomeTypeController);

        MstIncomeTypeController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstIncomeTypeController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstIncomeTypeController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() { 
            $scope.total = 0;
            $scope.totalDisplayed = 100;
            var url = 'api/MstApplication360/GetIncomeType';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.IncomeType_list = resp.data.application_list;
                unlockUI();

                //�new�code�start   
                if ($scope.IncomeType_list == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.IncomeType_list.length;
                    if ($scope.IncomeType_list.length < 100) {
                        $scope.totalDisplayed = $scope.IncomeType_list.length;
                    }
                }
                //�new�code�endd
            });
        }

        $scope.addIncomeType = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addIncomeType.html',
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
                        incometype_name: $scope.txtIncomeType_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    
                    }
                    var url = 'api/MstApplication360/CreateIncomeType';
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
                
            }
        }

        $scope.editIncomeType = function (incometype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editIncomeType.html',
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
                    incometype_gid: incometype_gid
                }
                var url = 'api/MstApplication360/EditIncomeType';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditIncomeType_name = resp.data.incometype_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.incometype_gid = resp.data.incometype_gid;
                });
               
                $scope.update = function () {

                    var url = 'api/MstApplication360/UpdateIncomeType';
                    var params = {
                        incometype_name: $scope.txteditIncomeType_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        incometype_gid: $scope.incometype_gid
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

        $scope.Status_update = function (incometype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusIncomeType.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                                
                var params = {
                    incometype_gid: incometype_gid
                }
                var url = 'api/MstApplication360/EditIncomeType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.incometype_gid = resp.data.incometype_gid
                    $scope.txtincometype_name = resp.data.incometype_name;
                    $scope.rbo_status = resp.data.Status;
                });
                           
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status,
                        incometype_gid: incometype_gid
                    
                    }

                    var url = 'api/MstApplication360/InactiveIncomeType';
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
                    incometype_gid: incometype_gid
                }

                var url = 'api/MstApplication360/IncomeTypeInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.incometypinactivelog_data = resp.data.application_list;
                    console.log(resp.data.application_list);
                    unlockUI();
                });
            }
        }

        $scope.delete = function (incometype_gid) {
            var params = {
                incometype_gid: incometype_gid
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
                            var url = 'api/MstApplication360/DeleteIncomeType';
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

