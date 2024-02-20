(function () {
    'use strict';

    angular
        .module('angle')
        .controller('Addresstypecontroller', Addresstypecontroller);

    Addresstypecontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function Addresstypecontroller($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'Addresstypecontroller';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() {
            $scope.totalDisplayed = 100;
            var url = 'api/AddressType/GetAddressType';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.address = resp.data.addresstype_list;
                unlockUI();
            });
        }

        $scope.loadMore = function (pagecount) {
            if (pagecount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
            // new code start
            if ($scope.address != null) {

                if (pagecount < $scope.address.length) {
                    $scope.totalDisplayed += Number;
                    if ($scope.address.length < $scope.totalDisplayed) {
                        $scope.totalDisplayed = $scope.address.length;
                        Notify.alert(" Total Summary " + $scope.address.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.address.length + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            unlockUI();
        };

      // Add Code Starts
        $scope.popupaddresstype = function () {
            var modalInstance = $modal.open({
                templateUrl: '/myModalContent.html',
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
                $scope.addressSubmit = function () {
                    var params = {
                        bureau_code: $scope.txtbureau_code,
                        lms_code: $scope.txtlms_code,
                        address_type: $scope.address_type
                    }
                    var url = 'api/AddressType/CreateAddressType';

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
                           
                        }
                    });
                    $modalInstance.close('closed');
                    activate();
                }
                
            }
        }
     // Add Code Ends

    // Edit Code Starts
        $scope.edit = function (address_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/myModaledit.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    address_gid: address_gid
                }
                var url = 'api/AddressType/EditAddressType';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txtbureau_codeedit = resp.data.bureau_code;
                    $scope.txtlms_codeedit = resp.data.lms_code;
                    $scope.addressTypeedit = resp.data.address_type;
                    $scope.address_gid = resp.data.address_gid;
                });
                
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.addresstypeUpdate = function () {

                    var params = {
                        bureau_code: $scope.txtbureau_codeedit,
                        lms_code: $scope.txtlms_codeedit,
                        address_type: $scope.addressTypeedit,
                        address_gid: $scope.address_gid
                    }
                    var url = 'api/AddressType/UpdateAddressType';

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

    // Edit Code Ends
        
    // Delete Code Starts
        $scope.delete = function (address_gid) {
            var params = {
                address_gid: address_gid
            }
            var url = 'api/AddressType/AddressTypeDelete';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    SweetAlert.swal({
                        title: 'Are you sure?',
                        text: 'Do You Want To Delete the Record ?',
                        showCancelButton: true,
                        confirmButtonColor: '#DD6B55',
                        confirmButtonText: 'Yes, delete it!',
                        closeOnConfirm: false
                    }, function (isConfirm) {
                        if (isConfirm) {
                            SweetAlert.swal('Deleted Successfully!');
                            unlockUI();
                            activate();
                        }

                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    activate();
                }
            });
        };

        $scope.Status_update = function (address_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusCompanyDocument.html',
                controller: ModalInstanceCtrl,
                backdrop:'static',
                keyboard:false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    address_gid: address_gid
                }
                var url = 'api/AddressType/EditAddressType';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txtbureau_codeedit = resp.data.bureau_code;
                    $scope.txtlms_codeedit = resp.data.lms_code;
                    $scope.addressTypeedit = resp.data.address_type;
                    $scope.address_gid = resp.data.address_gid;
                    $scope.rbo_status = resp.data.status_log;
                });
                var url = 'api/AddressType/GetActiveLog';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.addresstype_list = resp.data.addresstype_list;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        remarks: $scope.txtremarks,
                        status_log: $scope.rbo_status,
                        address_gid: address_gid
                    }
                    var url = 'api/AddressType/AddressTypeStatusUpdate';
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
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });

                    $modalInstance.close('closed');

                }
            }
        }
    }
})();
