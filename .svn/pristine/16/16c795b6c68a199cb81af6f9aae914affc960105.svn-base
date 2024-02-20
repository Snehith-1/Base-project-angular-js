(function () {
    'use strict';

    angular
        .module('angle')
        .controller('managementApprovalcontroller', managementApprovalcontroller);

    managementApprovalcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function managementApprovalcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'managementApprovalcontroller';

        activate();

        function activate() {
            $scope.serviceapproval_gid = localStorage.getItem('serviceapproval_gid');
            var params = {
                serviceapproval_gid: $scope.serviceapproval_gid
            };
            var url = 'api/myApprovals/viewmanagement';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.managementdetails = resp.data;
                console.log(resp.data);
            });
        }

        // Management Approve & Reject ...//

        $scope.managerapprove = function (serviceapproval_gid) {
            var params = {
                serviceapproval_gid: serviceapproval_gid
            }
            lockUI();
            var url = 'api/myApprovals/manageapprove';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status = true) {
                    unlockUI();
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
                }
                $state.go('app.myApproval');
            });
        }


        $scope.managerapprove = function (serviceapproval_gid) {
            var serviceapproval_gid = serviceapproval_gid;
            
            var modalInstance = $modal.open({
                templateUrl: '/manageapprove.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.approve_click = function (serviceapproval_gid) {

                    var params = {
                        serviceapproval_gid: localStorage.getItem('serviceapproval_gid'),
                        remarks: $scope.txtremarks
                    }
                    lockUI();
                    var url = 'api/myApprovals/manageapprove';
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
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
                        }
                        $modalInstance.close('closed');
                        $state.go('app.myApproval');
                    });
                }
            }
        }
     
        $scope.managerreject = function (serviceapproval_gid) {
            var serviceapproval_gid = serviceapproval_gid;

             var modalInstance = $modal.open({
                 templateUrl: '/managementreject.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.reject_click = function (serviceapproval_gid) {

                    var params = {
                        serviceapproval_gid: localStorage.getItem('serviceapproval_gid'),
                        remarks: $scope.txtremarks
                    }
                    lockUI();
                    var url = 'api/myApprovals/managereject';
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
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
                        }
                        $modalInstance.close('closed');
                        $state.go('app.myApproval');
                    });
                }
            }
        }
        $scope.back = function () {
            $state.go('app.myApproval');
        }
    }
})();
