(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstAnswerTypeController', MstAnswerTypeController);

        MstAnswerTypeController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstAnswerTypeController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstAnswerTypeController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            $scope.totalDisplayed = 100;
            var url = 'api/MstBRE/GetAnswerType';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.answertype_list = resp.data.answertype_list;
                unlockUI();

            });
        }
        // Add Code Starts
        $scope.popupanswertype = function () {
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
                $scope.answertypeSubmit = function () {
                    var params = {
                        bureau_code: $scope.txtbureau_code,
                        lms_code: $scope.txtlms_code,
                        answertype_name: $scope.answertype_name
                    }
                    var url = 'api/MstBRE/CreateAnswerType';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert('Answer Type Added Successfully', {
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
        $scope.edit = function (answertype_gid) {
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
                    answertype_gid: answertype_gid
                }
                var url = 'api/MstBRE/EditAnswerType';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.answertypenameedit = resp.data.answertype_name;
                    $scope.answertype_gid = resp.data.answertype_gid;
                });
                
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.answertypeUpdate = function () {

                    var params = {
                        bureau_code: $scope.txteditbureau_code,
                        lms_code: $scope.txteditlms_code,
                        answertype_name: $scope.answertypenameedit,
                        answertype_gid: $scope.answertype_gid
                    }
                    var url = 'api/MstBRE/UpdateAnswerType';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert('Answer Type Updated Successfully', {
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

        /* // Delete Code Starts
        $scope.delete = function (answertype_gid) {
            var params = {
                answertype_gid: answertype_gid
            }
            var url = 'api/MstBRE/DeleteAnswerType';
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

        $scope.Status_update = function (answertype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusAnswerType.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    answertype_gid: answertype_gid
                }
                var url = 'api/MstBRE/EditAnswerType';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txtbureau_codeedit = resp.data.bureau_code;
                    $scope.txtlms_codeedit = resp.data.lms_code;
                    $scope.answertypenameedit = resp.data.answertype_name;
                    $scope.answertype_gid = resp.data.answertype_gid;
                    $scope.rbo_status = resp.data.Status;
                });
                var url = 'api/MstBRE/GetAnswerTypeInactiveLog';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.answertype_list = resp.data.answertype_list;
                 });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        remarks: $scope.txtremarks,
                        Status: $scope.rbo_status,
                        answertype_gid: answertype_gid
                    }
                    var url = 'api/MstBRE/AnswerTypeStatusUpdate';
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
