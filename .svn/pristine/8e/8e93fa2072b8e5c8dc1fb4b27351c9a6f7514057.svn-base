(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstBusinessRejectRevokeController', AgrMstBusinessRejectRevokeController);

    AgrMstBusinessRejectRevokeController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function AgrMstBusinessRejectRevokeController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstBusinessRejectRevokeController';
        //$scope.application_gid = $location.search().application_gid;
        //var application_gid = $scope.application_gid;
        //$scope.employee_gid = $location.search().employee_gid;
        //var employee_gid = $scope.employee_gid;
        //$scope.lspage = $location.search().lspage;
        //var lspage = $scope.lspage;

        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.application_gid = searchObject.application_gid;
        var application_gid = $scope.application_gid;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.employee_gid = searchObject.employee_gid;
        var employee_gid = $scope.employee_gid;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.lspage = searchObject.lspage;
        var lspage = $scope.lspage;
        activate();
        function activate() {
            var param = {
                application_gid: $scope.application_gid,
                employee_gid: $scope.employee_gid
            };

            var url = 'api/AgrMstApplicationApproval/Getapplicationhierarchylist';

            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.level_zero = resp.data.level_zero;
                $scope.level_one = resp.data.level_one;
                $scope.clusterhead = resp.data.clusterhead;
                $scope.zonalhead = resp.data.zonalhead;
                $scope.regionhead = resp.data.regionhead;
                $scope.businesshead = resp.data.businesshead;
            });

            var param = {
                application_gid: $scope.application_gid
            };

            var url = 'api/AgrMstApplicationApproval/GetAppApprovalSummary';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.approval_list = resp.data.applicationapprovallist;
            });

            var url = 'api/AgrMstApplicationApproval/GetAppcommentsSummary';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.comment_list = resp.data.applicationcommentslist;
            });


            var param = {
                application_gid: $scope.application_gid
            };

            var url = 'api/AgrMstApplicationApproval/Getapplicationdetails';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.lblprogram_name = resp.data.program_name;
                $scope.lblapplref_no = resp.data.application_no;
                $scope.lblapplicant_name = resp.data.customer_name;
                $scope.lblcurrentappl_stage = resp.data.approval_status;
                $scope.lblappl_state = resp.data.region;
                $scope.lbloveralllimit_request = resp.data.overalllimit_amount;
                $scope.productlist = resp.data.productlist;
            });
        }

        $scope.revoke_submit = function () {
            var params = {
                reason: $scope.txtrevoke_remarks,
                application_gid: application_gid
            }
            var url = 'api/AgrAdminApplication/PostRejectRevokeApplication';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    if (lspage == 'RejectRevokeApplication') {
                        $state.go('app.AgrMstBusinessRevokeSummary');
                    }
                    else if (lspage == 'HoldRevokeApplication') {
                        $state.go('app.AgrMstBusinessHoldRevokeSummary');
                    }
                    else {

                    }
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }

        $scope.Back = function () {
            if (lspage == 'RejectRevokeApplication') {
                $state.go('app.AgrMstBusinessRevokeSummary');
            }
            else if (lspage == 'HoldRevokeApplication') {
                $state.go('app.AgrMstBusinessHoldRevokeSummary');
            }
            else {

            }
        }

    }
})();
