(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHRLoanTypeofFinancialAssistanceController', MstHRLoanTypeofFinancialAssistanceController);

        MstHRLoanTypeofFinancialAssistanceController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstHRLoanTypeofFinancialAssistanceController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHRLoanTypeofFinancialAssistanceController';

        activate();

        function activate() {          
            var url = 'api/MstHRLoanTypeofFinancialAssistance/GetHRLoanTypeofFinancialAssistance';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.typeoffinancialassistance_data = resp.data.typeoffinancialassistance_list;
                unlockUI();
            });
        }
        $scope.addtypeoffinancialassistance = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addfinancial.html',
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
                        hrloantypeoffinancialassistance_name: $scope.txtfinancial_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    
                    }
                    var url = 'api/MstHRLoanTypeofFinancialAssistance/CreateHRLoanTypeofFinancialAssistance';
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
        $scope.edittypeoffinancialassistance = function (hrloantypeoffinancialassistance_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editfinancial.html',
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
                    hrloantypeoffinancialassistance_gid: hrloantypeoffinancialassistance_gid
                }
                var url = 'api/MstHRLoanTypeofFinancialAssistance/EditHRLoanTypeofFinancialAssistance';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditfinancial_name = resp.data.hrloantypeoffinancialassistance_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.hrloantypeoffinancialassistance_gid = resp.data.hrloantypeoffinancialassistance_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/MstHRLoanTypeofFinancialAssistance/UpdateHRLoanTypeofFinancialAssistance';
                    var params = {
                        hrloantypeoffinancialassistance_name: $scope.txteditfinancial_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        hrloantypeoffinancialassistance_gid: $scope.hrloantypeoffinancialassistance_gid
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

        $scope.Status_update = function (hrloantypeoffinancialassistance_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusfinancial.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    hrloantypeoffinancialassistance_gid: hrloantypeoffinancialassistance_gid
                }            
                var url = 'api/MstHRLoanTypeofFinancialAssistance/EditHRLoanTypeofFinancialAssistance';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.hrloantypeoffinancialassistance_gid = resp.data.hrloantypeoffinancialassistance_gid
                    $scope.txtfinancial_name = resp.data.hrloantypeoffinancialassistance_name;
                    $scope.rbo_status = resp.data.Status;
                });    
                           
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        hrloantypeoffinancialassistance_gid: hrloantypeoffinancialassistance_gid,
                        hrloantypeoffinancialassistance_name: $scope.txtfinancial_name,
                        remarks: $scope.txtremarks,
                        rbo_status:$scope.rbo_status
                    
                    }
                    var url = 'api/MstHRLoanTypeofFinancialAssistance/InactiveHRLoanTypeofFinancialAssistance';
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
                        }activate();
                    }); 

                    $modalInstance.close('closed');

                }
                var params = {
                    hrloantypeoffinancialassistance_gid: hrloantypeoffinancialassistance_gid
                }

                var url = 'api/MstHRLoanTypeofFinancialAssistance/InactiveHRLoanTypeofFinancialAssistanceHistory';

                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.typeoffinancialassistanceinactivelog_data = resp.data.inactivehistory_list;
                    unlockUI();
                }); 
            }
        }

        $scope.delete = function (hrloantypeoffinancialassistance_gid) {
            var params = {
                hrloantypeoffinancialassistance_gid: hrloantypeoffinancialassistance_gid
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
                            var url = 'api/MstHRLoanTypeofFinancialAssistance/DeleteHRLoanTypeofFinancialAssistance';
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