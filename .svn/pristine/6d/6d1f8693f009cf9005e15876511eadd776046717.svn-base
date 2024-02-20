(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstMenuMappingController', SysMstMenuMappingController);

    SysMstMenuMappingController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstMenuMappingController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstMenuMappingController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {

            var url = 'api/SystemMaster/GetMenuMappingSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.menusummary_list = resp.data.menusummary_list;
                unlockUI();
            });
        }

        $scope.addmenu = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addmenu.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var url = 'api/SystemMaster/GetFirstLevelMenu';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.levelonemenu_list = resp.data.menu_list;
                    unlockUI();
                });
                $scope.getSecondlevel = function (levelone)
                {
                    var url = 'api/SystemMaster/GetSecondLevelMenu';
                    var params = {
                        module_gid_parent: levelone.module_gid
                    }
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.leveltwomenu_list = resp.data.menu_list;
                            unlockUI();
                    });
                }
                $scope.getThirdevel = function (leveltwo) {
                    var url = 'api/SystemMaster/GetThirdLevelMenu';
                    var params = {
                        module_gid_parent: leveltwo.module_gid
                    }
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.levelthreemenu_list = resp.data.menu_list;
                        unlockUI();
                    });
                }
                $scope.getFourthevel = function (levelthree) {
                    var url = 'api/SystemMaster/GetFourthLevelMenu';
                    var params = {
                        module_gid_parent: levelthree.module_gid
                    }
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.levelfourmenu_list = resp.data.menu_list;
                        unlockUI();
                    });
                }
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {

                    var params = {
                        module_gid: $scope.levelfour.module_gid,
                        module_name: $scope.levelfour.module_name

                    }
                    console.log(params);
                    var url = 'api/SystemMaster/PostMenudAdd';
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
            }
        }

        //$scope.editsaentitytype = function (saentitytype_gid) {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/editsaentitytype.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'md'
        //    });
        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {

        //        var url = 'api/MstApplication360/SATypeList';
        //        lockUI();
        //        SocketService.get(url).then(function (resp) {
        //            $scope.satype_list = resp.data.satype_list;
        //            unlockUI();
        //        });
        //        var params = {
        //            saentitytype_gid: saentitytype_gid
        //        }
        //        var url = 'api/MstApplication360/EditSAEntityType';
        //        SocketService.getparams(url, params).then(function (resp) {
        //            $scope.satype_Name = resp.data.satype_name;
        //            $scope.satype_Gid = resp.data.satype_gid;

        //            $scope.txteditsaentitytype_name = resp.data.saentitytype_name;
        //            $scope.txteditlms_code = resp.data.lms_code;
        //            $scope.txteditbureau_code = resp.data.bureau_code;
        //            $scope.saentitytype_gid = resp.data.saentitytype_gid;
        //        });
        //        $scope.titlename = function (string) {
        //            if (string.length >= 255) {
        //                $scope.message = "Allowed Only 255 Characters";
        //            }
        //            else {
        //                $scope.message = "";
        //            }
        //        }
        //        $scope.lmslength = function (string) {
        //            if (string.length >= 30) {
        //                $scope.lmsmessage = "Allowed Only 30 Characters";
        //            }
        //            else {
        //                $scope.lmsmessage = "";
        //            }
        //        }
        //        $scope.bureaulength = function (string) {
        //            if (string.length >= 10) {
        //                $scope.bureaumessage = "Allowed Only 10 Characters";
        //            }
        //            else {
        //                $scope.bureaumessage = "";
        //            }
        //        }
        //        $scope.ok = function () {
        //            $modalInstance.close('closed');
        //        };

        //        $scope.update = function () {
        //            var satype_name = $('#satype_Name :selected').text();
        //            var url = 'api/MstApplication360/UpdateSAEntityType';
        //            var params = {
        //                saentitytype_name: $scope.txteditsaentitytype_name,
        //                lms_code: $scope.txteditlms_code,
        //                bureau_code: $scope.txteditbureau_code,
        //                saentitytype_gid: $scope.saentitytype_gid,
        //                satype_name: satype_name,
        //                satype_gid: $scope.satype_Gid
        //            }
        //            SocketService.post(url, params).then(function (resp) {
        //                if (resp.data.status == true) {
        //                    $modalInstance.close('closed');
        //                    Notify.alert(resp.data.message, {
        //                        status: 'success',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                    activate();

        //                }
        //                else {
        //                    $modalInstance.close('closed');
        //                    Notify.alert(resp.data.message, {
        //                        status: 'warning',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                    activate();
        //                }
        //            });

        //        }
        //    }
        //}

        $scope.Status_update = function (menu_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusmenumapping.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    menu_gid: menu_gid
                }
                var url = 'api/SystemMaster/GetMenuMappingEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtmodule_name = resp.data.module_name;
                    $scope.txtmodule_gid = resp.data.module_gid;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        module_name: $scope.txtmodule_name,
                        menu_gid: menu_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/SystemMaster/GetMenuMappingInactivate';
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

                var url = 'api/SystemMaster/GetMenuMappingInactivateview';

                var param = {
                    menu_gid: menu_gid
                }

                SocketService.getparams(url, param).then(function (resp) {
                   $scope.menuinactivelog_list = resp.data.menusummary_list;
                });
            }
        }

        $scope.delete = function (menu_gid) {
            var params = {
                menu_gid: menu_gid
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
                    var url = 'api/SystemMaster/GetMenuMappingDelete';
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            SweetAlert.swal(resp.data.message);
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

