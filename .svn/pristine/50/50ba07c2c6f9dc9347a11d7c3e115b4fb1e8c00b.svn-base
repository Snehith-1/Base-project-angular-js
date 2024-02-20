(function () {
    'use strict';

    angular
        .module('angle')
        .controller('serviceApprovalViewcontroller', serviceApprovalViewcontroller);

    serviceApprovalViewcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function serviceApprovalViewcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'serviceApprovalViewcontroller';

        activate();

        function activate() {
            $scope.serviceapproval_gid = localStorage.getItem('serviceapproval_gid');
            $scope.lsinternalapproval = localStorage.getItem('lsinternalapproval');
            var params = {
                serviceapproval_gid: $scope.serviceapproval_gid
            };
            var url = 'api/myApprovals/viewservice';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.servicedetails = resp.data;
            });
        }

        // Service Approve & Reject ...//

        $scope.serviceapprove = function (serviceapproval_gid, category_gid, complaint_gid) {
            var params = {
                serviceapproval_gid: serviceapproval_gid,
                category_gid: category_gid,
                complaint_gid: complaint_gid
            }
            lockUI();
            var url = 'api/myApprovals/serviceapprove' ;
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
                $state.go('app.myApproval');
            });
        }

     
        $scope.serviceapprove = function (serviceapproval_gid, category_gid, complaint_gid) {
            var serviceapproval_gid = serviceapproval_gid;
            var category_gid = category_gid;
            var complaint_gid = complaint_gid;
            console.log(complaint_gid)
            console.log(category_gid)
            var modalInstance = $modal.open({
                templateUrl: '/serviceapprove.html',
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
                           remarks: $scope.txtremarks,
                category_gid: category_gid,
                complaint_gid: complaint_gid
            }
            lockUI();
            var url = 'api/myApprovals/serviceapprove' ;
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

        $scope.servicereject_click = function (serviceapproval_gid) {
            var serviceapproval_gid = serviceapproval_gid;

            console.log(serviceapproval_gid)
            var modalInstance = $modal.open({
                templateUrl: '/servicereject.html',
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
                    console.log(params);
                    lockUI();
                    var url = 'api/myApprovals/servicereject';
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

        // Service Internal Approval...//

        $scope.internalapprove_click = function (serviceapproval_gid) {
            var params = {
                serviceapproval_gid: serviceapproval_gid
            }
            lockUI();
            var url = 'api/myApprovals/serviceinternalapprove';
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
                $state.go('app.myApproval');
            });
        };
        $scope.back = function () {
            $state.go('app.myApproval');
        }
    }
})();
