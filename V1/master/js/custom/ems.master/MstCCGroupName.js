(function () {
    'use strict';

    angular
        .module('angle')
        .controller('ccgroupsummarycontroller', ccgroupsummarycontroller);

    ccgroupsummarycontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function ccgroupsummarycontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'ccgroupsummarycontroller';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() {
            var url = 'api/MstCCMember/Getccgroup';
            SocketService.get(url).then(function (resp) {
                $scope.ccgroup_list = resp.data.ccgroup_list;

            });
        }
        $scope.popupgroup = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addccgroup.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
               
                $scope.ccgroupsubmit = function () {

                    var url = 'api/MstCCMember/postccgroup';
                    var params = {
                        ccgroup_name: $scope.txtgroup_name,
                        ccgroup_code: $scope.txtgroup_code
                    }
                    console.log(params);
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


        $scope.delete = function (ccmembermaster_gid) {
            var params = {
                ccmembermaster_gid: ccmembermaster_gid
            }
            console.log(ccmembermaster_gid);
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',

                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = 'api/MstCCMember/deleteccmember';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred !', {
                                status: 'warning',
                                pos: 'top-right',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        };

        $scope.edit = function (ccgroupname_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editccgroup.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                var url = 'api/MstCCMember/geteditccgroup';
                var params = {
                    ccgroupname_gid:ccgroupname_gid
                }
                console.log(params);
                SocketService.getparams(url,params).then(function (resp) {
                    $scope.txteditccgroup_name = resp.data.ccgroup_name;
                    $scope.txteditccgroup_code = resp.data.ccgroup_code;
                    console.log(resp.data.ccgroup_name);
                });
               
                $scope.editccgroup = function () {

                    var url = 'api/MstCCMember/updateccgroup';
                    var params = {
                        ccgroup_name: $scope.txteditccgroup_name,
                        ccgroup_code: $scope.txteditccgroup_code,
                        ccgroupname_gid: ccgroupname_gid
                    }
                    console.log(params);
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
    }
})();
