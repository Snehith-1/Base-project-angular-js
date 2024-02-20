(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstLenderTypeController', MstLenderTypeController);

    MstLenderTypeController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstLenderTypeController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstLenderTypeController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            var url = 'api/MstApplication360/GetLenderType';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.lendertype_data = resp.data.application_list;
                unlockUI();
            });
        }


        $scope.addLenderType = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addLenderType.html',
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
                        lendertype_name: $scope.txtlender_type,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/MstApplication360/CreateLenderType';
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
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                    });

                    $modalInstance.close('closed');

                }
                $scope.titlename = function (string) {
                    if (string.length >= 255) {
                        $scope.message = "Allowed only 255 characters";
                    }
                    else {
                        $scope.message = "";
                    }
                }
                $scope.lmslength = function (string) {
                    if (string.length >= 30) {
                        $scope.lmsmessage = "Allowed only 30 characters";
                    }
                    else {
                        $scope.lmsmessage = "";
                    }
                }
                $scope.bureaulength = function (string) {
                    if (string.length >= 10) {
                        $scope.bureaumessage = "Allowed only 10 characters";
                    }
                    else {
                        $scope.bureaumessage = "";
                    }
                }
                $scope.RestrictSpaceSpecial = function (e) {
                    var k;
                    document.all ? k = e.keyCode : k = e.which;
                    return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || (k >= 48 && k <= 57));
                }
                $scope.valid = function (f) {
                    !(/^[A-zÑñ0-9]*$/i).test(f.value) ? f.value = f.value.replace(/[^A-zÑñ0-9]/ig, '') : null;
                }
            }
        }

        $scope.editLenderType = function (lendertype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editLenderType.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    lendertype_gid: lendertype_gid
                }
                var url = 'api/MstApplication360/EditLenderType';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditlender_type = resp.data.lendertype_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.lendertype_gid = resp.data.lendertype_gid;
                });

                $scope.titlename = function (string) {
                    if (string.length >= 255) {
                        $scope.message = "Allow only 255 characters";
                    }
                    else {
                        $scope.message = "";
                    }
                }
                $scope.lmslength = function (string) {
                    if (string.length >= 30) {
                        $scope.lmsmessage = "Allow only 30 characters";
                    }
                    else {
                        $scope.lmsmessage = "";
                    }
                }
                $scope.bureaulength = function (string) {
                    if (string.length >= 10) {
                        $scope.bureaumessage = "Allow only 10 characters";
                    }
                    else {
                        $scope.bureaumessage = "";
                    }
                }
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/MstApplication360/UpdateLenderType';
                    var params = {
                        lendertype_name: $scope.txteditlender_type,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        lendertype_gid: $scope.lendertype_gid
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

        $scope.Status_update = function (lendertype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusLenderType.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    lendertype_gid: lendertype_gid
                }
                var url = 'api/MstApplication360/EditLenderType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.lendertype_gid = resp.data.lendertype_gid
                    $scope.txtlender_type = resp.data.lendertype_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        lendertype_gid: lendertype_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MstApplication360/InactiveLenderType';
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
                    lendertype_gid: lendertype_gid
                }

                var url = 'api/MstApplication360/InactiveLenderTypeHistory';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.lendertypeinactivelog_data = resp.data.inactivehistory_list;
                    unlockUI();
                });

            }
        }

        $scope.delete = function (lendertype_gid) {
            var params = {
                lendertype_gid: lendertype_gid
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
                    var url = 'api/MstApplication360/DeleteLenderType';
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
