(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingTelecallingFunctionController', MstMarketingTelecallingFunctionController);

    MstMarketingTelecallingFunctionController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstMarketingTelecallingFunctionController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingTelecallingFunctionController';

        activate();

        function activate() {          
            var url = 'api/MstMarketingTelecallingFunction/GetMarketingTelecallingFunction';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.marketingtelecallingfunction_list = resp.data.marketingtelecallingfunction_list;
                unlockUI();
            });
        }
        $scope.addtelecallingfunction = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addtcfunction.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                               
                $scope.back = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {
                    var params = {
                        marketingtelecallingfunction_name: $scope.txttcfunction_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    }
                    var url = 'api/MstMarketingTelecallingFunction/CreateMarketingTelecallingFunction';
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
        $scope.edittelecallingfunction = function (marketingtelecallingfunction_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/edittcfunction.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.back = function () {
                    $modalInstance.close('closed');
                };
                var params = {
                    marketingtelecallingfunction_gid: marketingtelecallingfunction_gid
                }
                var url = 'api/MstMarketingTelecallingFunction/EditMarketingTelecallingFunction';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtedittcfunction_name= resp.data.marketingtelecallingfunction_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.marketingtelecallingfunction_gid = resp.data.marketingtelecallingfunction_gid;
                });
                $scope.back = function () {
                    $modalInstance.close('closed');
                };
                $scope.update = function () {
                    var url = 'api/MstMarketingTelecallingFunction/UpdateMarketingTelecallingFunction';
                    var params = {
                        marketingtelecallingfunction_name: $scope.txtedittcfunction_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        marketingtelecallingfunction_gid: $scope.marketingtelecallingfunction_gid
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

        $scope.Status_update = function (marketingtelecallingfunction_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statustcfunction.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    marketingtelecallingfunction_gid: marketingtelecallingfunction_gid
                }
                var url = 'api/MstMarketingTelecallingFunction/EditMarketingTelecallingFunction';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.marketingtelecallingfunction_gid = resp.data.marketingtelecallingfunction_gid
                    $scope.txttcfunction_name = resp.data.marketingtelecallingfunction_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        marketingtelecallingfunction_gid: marketingtelecallingfunction_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MstMarketingTelecallingFunction/InactiveMarketingTelecallingFunction';
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
                    marketingtelecallingfunction_gid: marketingtelecallingfunction_gid
                }

                var url = 'api/MstMarketingTelecallingFunction/InactiveMarketingTelecallingFunctionHistory';

                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.telecallingfunctioninactivelog_data = resp.data.marketingtelecallinginactivehistory_list;
                    unlockUI();
                });
            }
        }

        $scope.delete = function (marketingtelecallingfunction_gid) {
            var params = {
                marketingtelecallingfunction_gid: marketingtelecallingfunction_gid
            }
            SweetAlert.swal({
                
 title: 'Are you sure?',
 text: 'Do You Want To Delete the Record ?',
 showSpinner: true,
// showCancelButton: true,
 CancelButtonColor: '#dedad9',
 showCancelButton: true,
 confirmButtonColor: '#d64b3c',
 confirmButtonText: 'Yes, delete it!',
//  showConfirmButton: true,
 closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = 'api/MstMarketingTelecallingFunction/DeleteMarketingTelecallingFunction';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');
                            activate();
                        }
                        else if (resp.data.status == false) {
                            SweetAlert.swal({
                                title: 'Warning!',
                                text: "Can't able to delete Business Development Function because it is mapped to add Business Development call",
                                timer: 5000,                                       
                                showCancelButton: false,
                                showConfirmButton: false,                                        
                                backgroundcolor: '#d64b3c'
                        });
                            activate();
                            }
                        else {
                            Notify.alert(resp.data.message, {
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