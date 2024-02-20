(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmTrnApprovalViewController', AtmTrnApprovalViewController);

    AtmTrnApprovalViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$modal'];

    function AtmTrnApprovalViewController($rootScope, $scope, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $modal) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmTrnApprovalViewController';
        var auditcreation_gid = $location.search().auditcreation_gid;
        var initialapproval_gid = $location.search().initialapproval_gid;

        activate();


        function activate() {

            var params = {
                initialapproval_gid: initialapproval_gid
            }
            var url = 'api/AtmTrnAuditorMaker/GetInitialApprovalView';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.initialapprovalview_list = resp.data.initialapprovalview_list;
                unlockUI();

            });                     
        }
        $scope.approve_submit = function () {
            
         
            var params = {
                initialapproval_gid: initialapproval_gid,
                auditcreation_gid: auditcreation_gid,
                approve_remark: $scope.reject_remarks,
                
            }
          
            var url = "api/AtmTrnAuditorMaker/PostObservationApproval";
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
        $scope.reject_submit = function () {


            var params = {
                initialapproval_gid: initialapproval_gid,
                auditcreation_gid: auditcreation_gid,
                approve_remark: $scope.reject_remark,

            }
            lockUI();
            var url = "api/AtmTrnAuditorMaker/PostObservationRejected";
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