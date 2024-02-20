(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstBDLeadRequestTypeController', MstBDLeadRequestTypeController);

    MstBDLeadRequestTypeController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstBDLeadRequestTypeController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstBDLeadRequestTypeController';

        activate();

        function activate() {
            var url = 'api/MstLeadRequestType/GetLeadRequestType';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.leadrequesttype_list = resp.data.leadrequesttype_list;
                unlockUI();
            });
        }
        $scope.addleadrequesttype = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addleadrequesttype.html',
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
                        leadrequesttype_name: $scope.txtleadrequesttype_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    }
                    var url = 'api/MstLeadRequestType/CreateLeadRequestType';
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
        $scope.editleadrequesttype = function (leadrequesttype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editleadrequesttype.html',
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
                    leadrequesttype_gid: leadrequesttype_gid
                }
                var url = 'api/MstLeadRequestType/EditLeadRequestType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditleadrequesttype_name = resp.data.leadrequesttype_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.leadrequesttype_gid = resp.data.leadrequesttype_gid;
                });
                $scope.back = function () {
                    $modalInstance.close('closed');
                };
                $scope.update = function () {
                    var url = 'api/MstLeadRequestType/UpdateLeadRequestType';
                    var params = {
                        leadrequesttype_name: $scope.txteditleadrequesttype_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        leadrequesttype_gid: $scope.leadrequesttype_gid
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
        $scope.Status_update = function (leadrequesttype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusleadrequesttype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    leadrequesttype_gid: leadrequesttype_gid
                }
                var url = 'api/MstLeadRequestType/EditLeadRequestType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.leadrequesttype_gid = resp.data.leadrequesttype_gid
                    $scope.txtleadrequesttype_name = resp.data.leadrequesttype_name;
                    $scope.rbo_status = resp.data.Status;
                });
                $scope.back = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {
                    var params = {
                        leadrequesttype_gid: leadrequesttype_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status
                    }
                    var url = 'api/MstLeadRequestType/InactiveLeadRequestType';
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
                    leadrequesttype_gid: leadrequesttype_gid
                }
                var url = 'api/MstLeadRequestType/InactiveLeadRequestTypeHistory';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.leadrequesttypeinactivehistory_list = resp.data.leadrequesttypeinactivehistory_list;
                    unlockUI();
                });
            }
        }
        $scope.delete = function (leadrequesttype_gid) {
            var params = {
                leadrequesttype_gid: leadrequesttype_gid
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
                    var url = 'api/MstLeadRequestType/DeleteLeadRequestType';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');
                            activate();
                        }
                        
  else if (resp.data.status == false) {
    SweetAlert.swal({
        title: 'Warning!',
        text: "Can't able to delete Lead Request Type because it is mapped to add Business Development call",
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