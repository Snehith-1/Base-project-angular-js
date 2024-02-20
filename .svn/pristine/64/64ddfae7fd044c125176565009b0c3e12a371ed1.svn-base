(function () {
    'use strict';
    angular
        .module('angle')
        .controller('AtmTrnMyAuditTaskController', AtmTrnMyAuditTaskController);

    AtmTrnMyAuditTaskController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AtmTrnMyAuditTaskController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmTrnMyAuditTaskController';
        var auditcreation_gid = $location.search().auditcreation_gid;
        $scope.sampleimport_gid = $location.search().sampleimport_gid;
        var sampleimport_gid = $scope.sampleimport_gid;
        activate();

        function activate() {
            var url = 'api/AtmTrnMyAuditTask/EditMyAuditTask';
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

            var url = 'api/AtmTrnMyAuditTask/MyAuditTaskView';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.myaudittask_list = resp.data.checkpointobservationview_list;
                $scope.txttotal_score = resp.data.total_score;
            });
            var url = 'api/AtmTrnSampling/GetSample';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI()
                $scope.sample_list = resp.data.sample_list

            });

        }
        $scope.back = function (val) {
            $state.go('app.AtmTrnMyAuditTaskSummary');
        }
        $scope.view = function (val, val1) {
           
            $location.url('app/AtmTrnAuditTaskSample?auditcreation_gid=' + val + '&sampleimport_gid=' + val1)
        }
        $scope.samplequery = function (val, val1) {
            $location.url('app/AtmTrnSampleQuery?auditcreation_gid=' + val + '&sampleimport_gid=' + val1)
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

    $scope.checkpointintent = function (auditcreation_gid, checkpoint_intent) {
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
                auditcreation_gid: auditcreation_gid
            }
            lockUI();
            var url = 'api/AtmTrnMyAuditTask/GetAuditCreationIntent';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtcheckpointintent = resp.data.checkpoint_intent;

            });
            $scope.ok = function () {
                $modalInstance.close('closed');
            };
        }
    }
    $scope.description = function (auditcreation_gid, checkpoint_description) {
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
                auditcreation_gid: auditcreation_gid
            }
            lockUI();
            var url = 'api/AtmTrnMyAuditTask/GetAuditCreationDescription';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtcheckpointdescription = resp.data.checkpoint_description;

            });
            $scope.ok = function () {
                $modalInstance.close('closed');
            };
        }
    }
    $scope.notetoauditor = function (auditcreation_gid, noteto_auditor) {
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
                auditcreation_gid: auditcreation_gid
            }
            lockUI();
            var url = 'api/AtmTrnMyAuditTask/GetAuditCreationAuditor';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtnotetoauditor = resp.data.noteto_auditor;

            });
            $scope.ok = function () {
                $modalInstance.close('closed');
            };
        }
    }

    $scope.raisequery = function () {
        var modalInstance = $modal.open({
            templateUrl: '/raisequery.html',
            controller: ModalInstanceCtrl,
            backdrop: 'static',
            keyboard: false,
            size: 'md'
        });

        ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        function ModalInstanceCtrl($scope, $modalInstance) {


            $scope.ok = function () {
                $modalInstance.close('closed');
            };

            //var url = 'api/SystemMaster/GetEmployeelist';
            //lockUI();
            //SocketService.get(url).then(function (resp) {
            //    $scope.employee_list = resp.data.employeelist;
            //    unlockUI();
            //});



            $scope.submit = function () {

                var params = {

                        auditcreation_gid: auditcreation_gid,
                        sampleimport_gid: sampleimport_gid,
                        remarks: $scope.txtqueries
                    }
                    //employe: $scope.cboemployee_name,


                var url = "api/AtmTrnMyAuditTaskAuditee/PostRaiseQuerysample";
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $modalInstance.close('closed');
                        activate();
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                });

                $modalInstance.close('closed');

            }

        }
    }

}

    })();


