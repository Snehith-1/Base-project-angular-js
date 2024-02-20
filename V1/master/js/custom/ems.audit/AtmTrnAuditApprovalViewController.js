(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmTrnAuditApprovalViewController', AtmTrnAuditApprovalViewController);

    AtmTrnAuditApprovalViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$modal'];

    function AtmTrnAuditApprovalViewController($rootScope, $scope, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $modal) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmTrnAuditApprovalViewController';
        var auditcreation_gid = $location.search().auditcreation_gid;
        var observationapproval_gid = $location.search().observationapproval_gid;
        var initialapproval_gid = $location.search().initialapproval_gid;

        activate();


        function activate() {

            var params = {
                observationapproval_gid: observationapproval_gid
            }
            var url = 'api/AtmTrnAuditorMaker/GetAuditApprovalView';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.observationapprovalview_list = resp.data.observationapprovalview_list;
                unlockUI();

            });
        }
        $scope.auditapprove_submit = function () {


            var params = {
                observationapproval_gid: observationapproval_gid,
                auditcreation_gid: auditcreation_gid,
                approve_remark: $scope.auditapproval_remarks,

            }

            var url = "api/AtmTrnAuditorMaker/PostAuditApproval";
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.AtmTrnApproval');
                }

                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }
        $scope.auditreject_submit = function () {


            var params = {
                initialapproval_gid: initialapproval_gid,
                auditcreation_gid: auditcreation_gid,
                approve_remark: $scope.auditapproval_remarks,

            }
            lockUI();
            var url = "api/AtmTrnAuditorMaker/PostAuditRejected";
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.AtmTrnApproval');
                }

                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }
        $scope.back = function () {
            $state.go('app.AtmTrnApproval');
        }
    }
})();