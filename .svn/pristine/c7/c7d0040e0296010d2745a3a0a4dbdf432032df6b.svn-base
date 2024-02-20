(function () {
    'use strict';
    angular
        .module('angle')
        .controller('AtmTrnAuditCreationViewController', AtmTrnAuditCreationViewController);

    AtmTrnAuditCreationViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AtmTrnAuditCreationViewController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmTrnAuditCreationViewController';
        $scope.auditcreation_gid = $location.search().auditcreation_gid;
        var auditcreation_gid = $location.search().auditcreation_gid;

        activate();
        function activate() {
            var url = 'api/AtmTrnAuditCreation/EditAuditCreation';
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
                $scope.cboauditapprover = resp.data.auditmapping2employee_gid,
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
            var url = 'api/AtmTrnAuditCreation/ChecklistAssignView';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.checklistassignview_list = resp.data.checklistassignview_list;

            });
            var params = {
                auditcreation_gid: auditcreation_gid,

            };


            var url = 'api/AtmTrnSampling/GetSampleAuditor';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI()
                auditcreation_gid = resp.data.auditcreation_gid
                $scope.sample_list = resp.data.sample_list

            });

        }
        $scope.view = function (val1) {
            var auditcreation_gid = $scope.auditcreation_gid;
            $location.url('app/AtmTrnAuditCreationSampleView?auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + val1)
        }

        $scope.back = function (val) {
            $state.go('app.AtmTrnAuditCreationSummary');
        }
        $scope.showPopover = function (sampleimport_gid, sample_name) {
            var modalInstance = $modal.open({
                templateUrl: '/showemployee.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    sampleimport_gid: sampleimport_gid
                }
                lockUI();
                var url = 'api/AtmTrnSampling/GetEmployeeName';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.employee_name = resp.data.employee_name;
                    $scope.sample_name = resp.data.sample_name;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }



        $scope.checkpointintent = function (auditcreation2checklist_gid, checkpoint_intent) {
            var modalInstance = $modal.open({
                templateUrl: '/checkpointintent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    auditcreation2checklist_gid: auditcreation2checklist_gid
                }
                lockUI();
                var url = 'api/AtmTrnAuditCreation/GetAuditCreationIntent';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtcheckpointintent = resp.data.checkpoint_intent;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.description = function (auditcreation2checklist_gid, checkpoint_description) {
            var modalInstance = $modal.open({
                templateUrl: '/checkpointdescription.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                'use strict';

                angular
                    .module('angle')
                var params = {
                    auditcreation2checklist_gid: auditcreation2checklist_gid
                }
                lockUI();
                var url = 'api/AtmTrnAuditCreation/GetAuditCreationDescription';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtcheckpointdescription = resp.data.checkpoint_description;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.notetoauditor = function (auditcreation2checklist_gid, noteto_auditor) {
            var modalInstance = $modal.open({
                templateUrl: '/notetoauditor.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    auditcreation2checklist_gid: auditcreation2checklist_gid
                }
                lockUI();
                var url = 'api/AtmTrnAuditCreation/GetAuditCreationAuditor';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtnotetoauditor = resp.data.noteto_auditor;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
    }

})();