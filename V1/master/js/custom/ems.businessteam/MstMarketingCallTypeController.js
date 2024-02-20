(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingCallTypeController', MstMarketingCallTypeController);

    MstMarketingCallTypeController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstMarketingCallTypeController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingCallTypeController';

        activate();

        function activate() {          
            var url = 'api/MstMarketingCallType/GetCreateMarketingCallType';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.calltype_list = resp.data.marketingcalltype_list;
                unlockUI();
            });
        }
        $scope.addcalltype = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addcalltype.html',
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
                        marketingcalltype_name: $scope.txtcalltype_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    }
                    var url = 'api/MstMarketingCallType/CreateMarketingCallType';
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
        $scope.editcalltype = function (marketingcalltype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editcalltype.html',
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
                    marketingcalltype_gid: marketingcalltype_gid
                }
                var url = 'api/MstMarketingCallType/EditMarketingCallType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditcalltype_name = resp.data.marketingcalltype_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.marketingcalltype_gid = resp.data.marketingcalltype_gid;
                });
                $scope.back = function () {
                    $modalInstance.close('closed');
                };
                $scope.update = function () {
                    var url = 'api/MstMarketingCallType/UpdateMarketingCallType';
                    var params = {
                        marketingcalltype_name: $scope.txteditcalltype_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        marketingcalltype_gid: $scope.marketingcalltype_gid
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
        $scope.Status_update = function (marketingcalltype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statuscalltype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    marketingcalltype_gid: marketingcalltype_gid
                }
                var url = 'api/MstMarketingCallType/EditMarketingCallType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.marketingcalltype_gid = resp.data.marketingcalltype_gid
                    $scope.txtcalltype_name = resp.data.marketingcalltype_name;
                    $scope.rbo_status = resp.data.Status;
                });
                $scope.back = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {
                    var params = {
                        marketingcalltype_gid: marketingcalltype_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status
                    }
                    var url = 'api/MstMarketingCallType/InactiveMarketingCallType';
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
                    marketingcalltype_gid: marketingcalltype_gid
                }
                var url = 'api/MstMarketingCallType/InactiveMarketingCallTypeHistory';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.calltypeinactivelog_list = resp.data.marketinginactivehistory_list;
                    unlockUI();
                });
            }
        }
        $scope.delete = function (marketingcalltype_gid) {
            var params = {
                marketingcalltype_gid: marketingcalltype_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',
                showSpinner: true,
                showCancelButton: true,
  //confirmButtonColor: '#3085d6',
  CancelButtonColor: '#3085d6',
  confirmButtonText: 'Yes, delete it!',
               // showCancelButton: true,
              //  cancelButtonColor: '#d9dcde',
              //  showCancelButton: true,
                confirmButtonColor: '#d64b3c',
               // confirmButtonText: 'Yes, delete it!',
              //  showConfirmButton: true,
             // confirmButtonClass: 'btn btn-success',
cancelButtonClass: 'btn btn-danger',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = 'api/MstMarketingCallType/DeleteMarketingCallType';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');
                            activate();
                        }
                        
  else if (resp.data.status == false) {
    SweetAlert.swal({
        title: 'Warning!',
        text: "Can't able to delete Enquiry Type because it is mapped to add Business Development call",
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