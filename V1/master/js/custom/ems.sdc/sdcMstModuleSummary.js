(function () {
    'use strict';

    angular
        .module('angle')
        .controller('sdcMstModuleController', sdcMstModuleController);

    sdcMstModuleController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function sdcMstModuleController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'sdcMstModuleController';

        activate();

        function activate() {
            $scope.totalDisplayed = 100;
            var url = 'api/SdcMstModule/GetModuleSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.modulemaster_summary = resp.data.moduledtl;
                unlockUI();
            });
        }
        //$scope.loadMore = function (pagecount) {
        //    if (pagecount == undefined) {
        //        Notify.alert("Enter the Total Summary Count", "warning");
        //        return;
        //    }
        //    lockUI();

        //    var Number = parseInt(pagecount);
        //    // new code start
        //    if ($scope.modulemasterlist != null) {

        //        if (pagecount < $scope.modulemasterlist.length) {
        //            $scope.totalDisplayed += Number;
        //            if ($scope.modulemasterlist.length < $scope.totalDisplayed) {
        //                $scope.totalDisplayed = $scope.modulemasterlist.length;
        //                Notify.alert(" Total Summary " + $scope.modulemasterlist.length + " Records Only", "warning");
        //            }
        //            unlockUI();
        //        }
        //        else {
        //            unlockUI();
        //            Notify.alert(" Total Summary " + $scope.modulemasterlist.length + " Records Only", "warning");
        //            return;
        //        }
        //    }
        //    // new code end
        //    unlockUI();
        //};
        // Add Code Starts
        $scope.popupmodule = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addModuleContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
              
                $scope.activitySubmit = function () {
                    lockUI();
                    var params = {
                        module_name: $scope.module_name,
                        module_prefix: $scope.module_prefix,
                        availability: $scope.availability,
                    }
                    //console.log(params);
                    var url = 'api/SdcMstModule/PostModuleAdd';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            unlockUI();
                        }
                        else {
                            alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            unlockUI();
                        }
                    });
                }

            }
        }
         //Add Code Ends

        // Edit Code Starts
        $scope.edit = function (module_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/ModuleModaledit.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    module_gid: module_gid
                }
                var url = 'api/SdcMstModule/GetModuleView';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.module_gid = resp.data.module_gid,
                    $scope.modulenameedit = resp.data.module_name,
                    $scope.module_codeedit = resp.data.module_code;
                    $scope.moduleprefixedit = resp.data.module_prefix;
                    $scope.availabilityedit = resp.data.availability;
                   
                });
                $scope.ok = function () {
                    modalInstance.close('closed');
                };
                $scope.moduleUpdate = function () {
                    var params = {
                        module_gid: module_gid,
                        module_name: $scope.modulenameedit,
                        availability: $scope.availabilityedit,
                        module_prefix: $scope.moduleprefixedit
                    }
                    console.log(params);
                    var url = 'api/SdcMstModule/PostModuleUpdate';
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

                            alert(resp.data.message, {
                                status: 'danger',
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
        $scope.delete = function (val) {
            var params = {
                module_gid: val
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
                    var url = "api/SdcMstModule/GetModuleDelete";
                    SocketService.getparams(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            activate();
                            SweetAlert.swal('Deleted Successfully!');
                            unlockUI();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }
                    });

                }

            });
        }
        // Delete Code End

        $scope.tagCustomer = function (module_gid) {
            $scope.module_gid = module_gid;
            $scope.module_gid = localStorage.setItem('module_gid', module_gid);
            $state.go('app.sdcMstTagCustomer');

        }
    }
})();
