(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingCallReceivedNumberController', MstMarketingCallReceivedNumberController);

    MstMarketingCallReceivedNumberController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstMarketingCallReceivedNumberController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingCallReceivedNumberController';

        activate();

        function activate() {          
            var url = 'api/MstMarketingCallReceivedNumber/GetMarketingCallReceivedNumber';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.callreceivednumber_list = resp.data.marketingcallreceivednumber_list;
                unlockUI();
            });
        }
        $scope.addcallreceivednumber = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addcallreceivednumber.html',
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
                        marketingcallreceivednumber_name: $scope.txtcallreceivednumber_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    }
                    var url = 'api/MstMarketingCallReceivedNumber/CreateMarketingCallReceivedNumber';
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
        $scope.editcallreceivednumber = function (marketingcallreceivednumber_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editcallreceivednumber.html',
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
                    marketingcallreceivednumber_gid: marketingcallreceivednumber_gid
                }
                var url = 'api/MstMarketingCallReceivedNumber/EditMarketingCallReceivedNumber';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditcallreceivednumber_name = resp.data.marketingcallreceivednumber_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.marketingcallreceivednumber_gid = resp.data.marketingcallreceivednumber_gid;
                });
                $scope.back = function () {
                    $modalInstance.close('closed');
                };
                $scope.update = function () {
                    var url = 'api/MstMarketingCallReceivedNumber/UpdateMarketingCallReceivedNumber';
                    var params = {
                        marketingcallreceivednumber_name: $scope.txteditcallreceivednumber_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        marketingcallreceivednumber_gid: $scope.marketingcallreceivednumber_gid
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
        $scope.Status_update = function (marketingcallreceivednumber_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statuscallreceivednumber.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    marketingcallreceivednumber_gid: marketingcallreceivednumber_gid
                }
                var url = 'api/MstMarketingCallReceivedNumber/EditmarketingCallReceivedNumber';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.marketingcallreceivednumber_gid = resp.data.marketingcallreceivednumber_gid
                    $scope.txtcallreceivednumber_name = resp.data.marketingcallreceivednumber_name;
                    $scope.rbo_status = resp.data.Status;
                });
                $scope.back = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {
                    var params = {
                        marketingcallreceivednumber_gid: marketingcallreceivednumber_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status
                    }
                    var url = 'api/MstMarketingCallReceivedNumber/InactiveMarketingCallReceivedNumber';
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
                    marketingcallreceivednumber_gid: marketingcallreceivednumber_gid
                }
                var url = 'api/MstMarketingCallReceivedNumber/InactiveMarketingCallReceivedNumberHistory';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.callreceivednumberinactivelog_list = resp.data.marketingcallreceivednumberinactivehistory_list;
                    unlockUI();
                });
            }
        }

        $scope.delete = function (marketingcallreceivednumber_gid) {
            var params = {
                marketingcallreceivednumber_gid: marketingcallreceivednumber_gid
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
                    var url = 'api/MstMarketingCallReceivedNumber/DeleteMarketingCallReceivedNumber';
                    SocketService.getparams(url, params).then(function (resp) {
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
                        }
                    });
                }
            });
        }
    }
})();