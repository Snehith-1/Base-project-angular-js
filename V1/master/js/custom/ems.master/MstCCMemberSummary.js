(function () {
    'use strict';

    angular
        .module('angle')
        .controller('ccmembersummarycontroller', ccmembersummarycontroller);

    ccmembersummarycontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function ccmembersummarycontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'ccmembersummarycontroller';

        activate();

        function activate() {
            var url = 'api/MstCCMember/Getccmember';
            SocketService.get(url).then(function (resp) {
                $scope.ccmember_list = resp.data.ccmember_list;

            });
        }
        $scope.popupaddccmember = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addccmember.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                var url = 'api/MstCCMember/Getccgroup';
                SocketService.get(url).then(function (resp) {
                    $scope.ccgroup_list = resp.data.ccgroup_list;
                });
                var url = 'api/employee/Employee';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employee_list;

                });
                $scope.ccmembersubmit = function () {
                   
                    var url = 'api/MstCCMember/postccmember';
                    var params = {
                        ccmember_list: $scope.cboccemployee_name,
                        ccgroupname_gid: $scope.cboccgroup_name.ccgroupname_gid,
                        ccgroup_name: $scope.cboccgroup_name.ccgroup_name,
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

  
        $scope.delete = function (ccmembermaster_gid) {
            var params = {
                ccmembermaster_gid: ccmembermaster_gid
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

        $scope.edit = function (ccmembermaster_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editccmember.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                var params = {
                    ccmembermaster_gid: ccmembermaster_gid
                }
                var url = 'api/MstCCMember/geteditccmember';
                SocketService.getparams(url,params).then(function (resp) {
                    $scope.ccgroupname_gid = resp.data.ccgroupname_gid;
                    $scope.CCMember_name = resp.data.CCMember_name;
                    $scope.cboemployee_gid = resp.data.ccmember_gid;
                    
                });
                var url = 'api/MstCCMember/Getccgroup';
                SocketService.get(url).then(function (resp) {
                    $scope.ccgroup_list = resp.data.ccgroup_list;

                });
                var url = 'api/employee/Employee';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employee_list;

                });
                $scope.ccmembersubmit = function () {

                    var url = 'api/MstCCMember/updateccmember';
                    var params = {
                       
                        ccmember_gid: $scope.cboemployee_gid,
                        ccgroupname_gid: $scope.ccgroupname_gid,
                        ccmembermaster_gid: ccmembermaster_gid
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
    }
})();
