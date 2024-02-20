(function () {
    'use strict';

    angular
        .module('angle')
        .controller('departmentApprovalView', departmentApprovalView);

    departmentApprovalView.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function departmentApprovalView($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'departmentApprovalView';

        activate();

        function activate() {
            $scope.serviceapproval_gid = localStorage.getItem('serviceapproval_gid');
            $scope.lsinternalapproval = localStorage.getItem('lsinternalapproval');
            var params = {
                serviceapproval_gid: $scope.serviceapproval_gid
            }
            lockUI();
            var url = 'api/myApprovals/viewdepartment';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.viewdepartment = resp.data;
            });
        }

        // Department Approve ...//

        //$scope.departmentapprove_click = function (category_gid, serviceapproval_gid) {
        //    var params = {
        //        category_gid: category_gid,
        //        serviceapproval_gid: serviceapproval_gid
        //    }
        //    lockUI();
        //    var url = 'api/myApprovals/departmentApproveclick';
        //    SocketService.post(url, params).then(function (resp) {
        //        unlockUI();
        //        if (resp.data.status = true) {
        //            Notify.alert(resp.data.message, {
        //                status: 'success',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
        //        }
        //        else {
        //            Notify.alert(resp.data.message, {
        //                status: 'warning',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
        //        }
        //        $state.go('app.myApproval');
        //    });
        //}

        $scope.departmentapprove_click = function (category_gid, serviceapproval_gid) {
            var serviceapproval_gid = serviceapproval_gid;
            var category_gid = category_gid;
            console.log(category_gid)
            var modalInstance = $modal.open({
                templateUrl: '/departmentapprove.html',
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
                                category_gid: category_gid,
                                serviceapproval_gid: localStorage.getItem('serviceapproval_gid'),
                                remarks: $scope.txtremarks
                            }
                    console.log(params);
                    lockUI();
                    var url = 'api/myApprovals/departmentApproveclick';
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


        // Department  Reject...//
        $scope.departmentreject_click = function (serviceapproval_gid) {
            var serviceapproval_gid = serviceapproval_gid;

            console.log(serviceapproval_gid)
            var modalInstance = $modal.open({
                templateUrl: '/departmentreject.html',
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
                        remarks:$scope.txtremarks
                    }
                    console.log(params);
                    lockUI();
                    var url = 'api/myApprovals/departmentreject';
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
       

        // Department Internal Approval...//

        

        $scope.departmentinternal_click = function (complaint_gid, serviceapproval_gid) {
            var params = {
                serviceapproval_gid: serviceapproval_gid,
                complaint_gid: complaint_gid,
                remarks: $scope.txtremarks
            }
            var url = 'api/myApprovals/departmentinternal';
            lockUI();
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

        $scope.back = function () {
            $state.go('app.myApproval');
        }
    }
})();
