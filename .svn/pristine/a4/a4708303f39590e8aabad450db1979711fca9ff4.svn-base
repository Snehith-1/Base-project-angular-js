(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmTrnAuditorApprovalController', AtmTrnAuditorApprovalController);

    AtmTrnAuditorApprovalController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$modal'];

    function AtmTrnAuditorApprovalController($rootScope, $scope, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $modal) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmTrnAuditorApprovalController';
        var auditcreation_gid = $location.search().auditcreation_gid;
        var observationapproval_gid = $location.search().observationapproval_gid;
        var initialapproval_gid = $location.search().initialapproval_gid;

        activate();

            function activate() {
                var url = 'api/AtmTrnAuditorMaker/EditAuditorMaker';
                var params = {
                    auditcreation_gid: auditcreation_gid
                }
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.cboauditfrequency = resp.data.auditfrequency_gid,
                      $scope.cboauditfrequency = resp.data.auditfrequency_name,
                     $scope.cboauditname = resp.data.checklistmaster_gid,
                      $scope.cboauditname = resp.data.audit_name,
                      $scope.cboauditmaker = resp.data.employee_gid,
                      $scope.cboauditmaker = resp.data.audit_maker,
                     $scope.cboauditchecker = resp.data.auditmapping_gid,
                      $scope.cboauditchecker = resp.data.audit_checker,
                      $scope.cboauditapprover = resp.data.employee_gid,
                      $scope.cboauditapprover = resp.data.audit_approver,
                     $scope.cboauditpriority = resp.data.auditpriority_gid,
                      $scope.cboauditpriority = resp.data.auditpriority_name,
                    $scope.txtdue_date = resp.data.due_date,
                    $scope.txtreport_date = resp.data.report_date,
                    $scope.txtperiod_from = resp.data.periodfrom_date,
                    $scope.txtperiod_to = resp.data.auditperiod_to,
                    $scope.txtaudit_ref_no = resp.data.audit_uniqueno,
                    $scope.cboauditmaker_name = resp.data.auditmaker_name,
                    $scope.cboauditchecker_name = resp.data.auditchecker_name,
                    unlockUI();
                });

                var url = 'api/AtmTrnAuditorMaker/AuditorMakerView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.makercheckpointobservationview_list = resp.data.makercheckpointobservationview_list;
                    $scope.txttotal_score = resp.data.total_score;
                });
                var url = 'api/AtmTrnSampling/GetSampleAuditor';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI()
                    $scope.sample_list = resp.data.sample_list

                });
        }
        $scope.auditorapproval_submit = function () {
            var params = {
                auditcreation_gid: auditcreation_gid,
                auditorapproval_remark: $scope.auditorapproval_remark,

            }

            var url = "api/AtmTrnAuditorMaker/PostAuditorApproval";
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.AtmTrnAuditorApproverSummary');
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
        $scope.auditorreject_submit = function () {
            var params = {
                auditcreation_gid: auditcreation_gid,
                auditorreject_remark: $scope.auditorapproval_remark,

            }
            lockUI();
            var url = "api/AtmTrnAuditorMaker/PostAuditorRejected";
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.AtmTrnAuditorApproverSummary');
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
            $state.go('app.AtmTrnAuditorApproverSummary');
        }
    }
})();