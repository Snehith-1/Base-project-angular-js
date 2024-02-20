(function () {
    'use strict';

    angular
        .module('angle')
        .controller('Businessunitcontroller', Businessunitcontroller);

    Businessunitcontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function Businessunitcontroller($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'Businessunitcontroller';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() {
            $scope.totalDisplayed = 100;
            var url = 'api/BusinessUnit/GetBusinessUnit';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.business_unit = resp.data.businessunit_list;
                unlockUI();
            });
        }
        // Add Code Starts
        $scope.popupbusinessunit = function () {
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
                $scope.businessunitSubmit = function () {
                    var params = {
                        bureau_code: $scope.txtbureau_code,
                        lms_code: $scope.txtlms_code,
                        businessunit_name: $scope.businessunit_name
                    }
                    var url = 'api/BusinessUnit/CreateBusinessUnit';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {

                            Notify.alert('Strategic Business Unit Added Successfully', {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                            activate();
                        }
                        else {

                            Notify.alert('Error Occured while adding', {
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
        $scope.edit = function (businessunit_gid) {
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
                    businessunit_gid: businessunit_gid
                }
                var url = 'api/BusinessUnit/EditBusinessUnit';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txtbureau_codeedit = resp.data.bureau_code;
                    $scope.txtlms_codeedit = resp.data.lms_code;
                    $scope.businessunitnameedit = resp.data.businessunit_name;
                    $scope.businessunit_gid = resp.data.businessunit_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.businessunitUpdate = function () {

                    var params = {
                        bureau_code: $scope.txtbureau_codeedit,
                        lms_code: $scope.txtlms_codeedit,
                        businessunit_name: $scope.businessunitnameedit,
                        businessunit_gid: $scope.businessunit_gid
                    }
                    var url = 'api/BusinessUnit/UpdateBusinessUnit';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {

                            Notify.alert('Strategic Business Unit updated Successfully', {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();

                        }
                        else {

                            Notify.alert('Error Occured while updating', {
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


        // Edit Code Ends

        // Delete Code Starts
        $scope.delete = function (businessunit_gid) {
            var params = {
                businessunit_gid: businessunit_gid
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
                            var url = 'api/BusinessUnit/DeleteBusinessUnit';
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
        // Delete Code Ends
        $scope.Status_update = function (businessunit_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusCompanyDocument.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    businessunit_gid: businessunit_gid
                }
                var url = 'api/BusinessUnit/EditBusinessUnit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtbureau_codeedit = resp.data.bureau_code;
                    $scope.txtlms_codeedit = resp.data.lms_code;
                    $scope.businessunitnameedit = resp.data.businessunit_name;
                    $scope.businessunit_gid = resp.data.businessunit_gid;
                    $scope.rbo_status = resp.data.status_log;
                });
                var url = 'api/BusinessUnit/GetActiveLog';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.businessunit_list = resp.data.businessunit_list;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        remarks: $scope.txtremarks,
                        status_log: $scope.rbo_status,
                        businessunit_gid: businessunit_gid
                    }
                    var url = 'api/BusinessUnit/BusinessUnitStatusUpdate';
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
    }
})();
