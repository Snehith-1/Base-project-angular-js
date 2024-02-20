(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstGroupTitleController', MstGroupTitleController);

        MstGroupTitleController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstGroupTitleController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstGroupTitleController';

        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            $scope.totalDisplayed = 100;
            var url = 'api/MstBRE/GetGroupTitle';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.grouptitle_list = resp.data.grouptitle_list;
                unlockUI();

            });
        }
        // Add Code Starts
        $scope.popupgrouptitle = function () {
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
                $scope.grouptitleSubmit = function () {
                    var params = {
                        bureau_code: $scope.txtbureau_code,
                        lms_code: $scope.txtlms_code,
                        grouptitle_name: $scope.grouptitle_name
                    }
                    var url = 'api/MstBRE/CreateGroupTitle';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert('Group Title Added Successfully', {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();

                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert('Error Occured While Adding', {
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
        $scope.edit = function (grouptitle_gid) {
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
                    grouptitle_gid: grouptitle_gid
                }
                var url = 'api/MstBRE/EditGroupTitle';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.grouptitlenameedit = resp.data.grouptitle_name;
                    $scope.grouptitle_gid = resp.data.grouptitle_gid;
                });
                
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.grouptitleUpdate = function () {

                    var params = {
                        bureau_code: $scope.txteditbureau_code,
                        lms_code: $scope.txteditlms_code,
                        grouptitle_name: $scope.grouptitlenameedit,
                        grouptitle_gid: $scope.grouptitle_gid
                    }
                    var url = 'api/MstBRE/UpdateGroupTitle';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert('Group Title Updated Successfully', {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();

                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert('Error Occured While Updating', {
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

        /*  // Delete Code Starts
        $scope.delete = function (grouptitle_gid) {
            var params = {
                grouptitle_gid: grouptitle_gid
            }
            var url = 'api/MstBRE/DeleteGroupTitle';
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
        // Delete Code Ends */

        $scope.Status_update = function (grouptitle_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusGroupTitle.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    grouptitle_gid: grouptitle_gid
                }
                var url = 'api/MstBRE/EditGroupTitle';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txtbureau_codeedit = resp.data.bureau_code;
                    $scope.txtlms_codeedit = resp.data.lms_code;
                    $scope.grouptitlenameedit = resp.data.grouptitle_name;
                    $scope.grouptitle_gid = resp.data.grouptitle_gid;
                    $scope.rbo_status = resp.data.Status;
                });
                var url = 'api/MstBRE/GetGroupTitleInactiveLog';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.grouptitle_list = resp.data.grouptitle_list;
                 });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        remarks: $scope.txtremarks,
                        Status: $scope.rbo_status,
                        grouptitle_gid: grouptitle_gid
                    }
                    var url = 'api/MstBRE/GroupTitleStatusUpdate';
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
