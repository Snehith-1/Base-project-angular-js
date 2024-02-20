(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmMstChecklistMasterAddController', AtmMstChecklistMasterAddController);

    AtmMstChecklistMasterAddController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$filter', '$route', 'ngTableParams'];

    function AtmMstChecklistMasterAddController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $filter, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmMstChecklistMasterAddController';
       
        activate();

        function activate() {


            var url = 'api/MstApplication360/GetEntity';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.entity_data = resp.data.application_list;
                unlockUI();
            });
            var url = 'api/AtmMstCheckpointGroup/GetCheckpointGroupActive';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.checkpointgroup_data = resp.data.checkpointgroup_list;
                unlockUI();
            });
            var url = 'api/AtmMstAuditDepartment/GetAuditDepartmentActive';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.auditdepartment_list = resp.data.auditdepartment_list;
                unlockUI();
            });

            var url = 'api/AtmMstAuditType/GetAuditTypeActive';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.audittype_data = resp.data.audittype_list;
                unlockUI();
            });

            var url = 'api/SystemMaster/GetEmployeelist';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employeelist;
                unlockUI();
            });

            $scope.checklistmasterback = function (val) {
                $state.go('app.AtmMstChecklistMasterSummary');
            }


            var url = 'api/AtmMstChecklistMaster/GetCheckpointStatus';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.checklistmaster_list = resp.data.checklistmaster_list;
                unlockUI();
            });

        }

        $scope.onchangecheckpoint = function () {
            var params = {
                checkpointgroup_list : $scope.cbocheckpoint
            }
            var url = 'api/AtmMstChecklistMaster/GetMultipleCheckpointgroup';
            SocketService.post(url, params).then(function (resp) {
                $scope.checkpointgroupadd_list = resp.data.checkpointgroupadd_list;

            });
           
        }
        
        $scope.submitAddChecklist = function () {

            var lsauditdepartment_gid = '';
            var lsauditdepartment_name = '';
            if ($scope.cboauditdepartment != undefined || $scope.cboauditdepartment != null) {
                lsauditdepartment_gid = $scope.cboauditdepartment.auditdepartment_gid;
                lsauditdepartment_name = $scope.cboauditdepartment.auditdepartment_name;
            }

            var lsentity_gid = '';
            var lsentity_name = '';
            if ($scope.cboentity != undefined || $scope.cboentity != null) {
                lsentity_gid = $scope.cboentity.entity_gid;
                lsentity_name = $scope.cboentity.entity_name;
            }

            var lscheckpointgroup_gid = '';
            var lscheckpointgroup_name = '';
            if ($scope.cbocheckpoint != undefined || $scope.cbocheckpoint != null) {
                lscheckpointgroup_gid = $scope.cbocheckpoint.checkpointgroup_gid;
                lscheckpointgroup_name = $scope.cbocheckpoint.checkpointgroup_name;
            }

            var lsaudittype_gid = '';
            var lsaudittype_name = '';
            if ($scope.cboaudittype != undefined || $scope.cboaudittype != null) {
                lsaudittype_gid = $scope.cboaudittype.audittype_gid;
                lsaudittype_name = $scope.cboaudittype.audittype_name;
            }
            var lsemployee_gid = '';
            var lsemployee_name = '';
            if ($scope.cboauditmaker != undefined || $scope.cboauditmaker != null) {
                lsemployee_gid = $scope.cboauditmaker.employee_gid;
                lsemployee_name = $scope.cboauditmaker.employee_name;
            }
            var lsauditmapping_gid = '';
            var lsaudit_checker = '';
            if ($scope.cboauditchecker != undefined || $scope.cboauditchecker != null) {
                lsauditmapping_gid = $scope.cboauditchecker.employee_gid;
                lsaudit_checker = $scope.cboauditchecker.employee_name;
            }
                var params = {
                   
                    audittype_gid: lsaudittype_gid,
                    audittype_name: lsaudittype_name,
                    entity_gid: lsentity_gid,
                    entity_name: lsentity_name,
                    checkpointgroup_gid: lscheckpointgroup_gid,
                    checkpointgroup_name: lscheckpointgroup_name,
                    auditdepartment_gid: lsauditdepartment_gid,
                    auditdepartment_name: lsauditdepartment_name,
                    audit_name: $scope.txtaudit_name,
                    audit_description: $scope.txtaudit_description,
                    employee_gid: lsemployee_gid,
                    audit_maker: lsemployee_name,
                    auditmapping_gid: lsauditmapping_gid,
                    audit_checker: lsaudit_checker,
                    checkpointgroupadd_list: $scope.cbocheckpoint,
                }
               
                var url = 'api/AtmMstChecklistMaster/PostChecklistMaster';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $state.go('app.AtmMstChecklistMasterSummary');
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


        $scope.checkpointintent = function (checkpointgroupadd_gid, checkpoint_intent) {
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
                    checkpointgroupadd_gid: checkpointgroupadd_gid
                }
                lockUI();
                var url = 'api/AtmMstCheckpointGroup/GetCheckpointIntent';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtcheckpointintent = resp.data.checkpoint_intent;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.description = function (checkpointgroupadd_gid, checkpoint_description) {
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
                    checkpointgroupadd_gid: checkpointgroupadd_gid
                }
                lockUI();
                var url = 'api/AtmMstCheckpointGroup/GetCheckpointDescription';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtcheckpointdescription = resp.data.checkpoint_description;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.notetoauditor = function (checkpointgroupadd_gid, noteto_auditor) {
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
                    checkpointgroupadd_gid: checkpointgroupadd_gid
                }
                lockUI();
                var url = 'api/AtmMstCheckpointGroup/GetCheckpointNotestoAuditor';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtnotetoauditor = resp.data.noteto_auditor;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
            
        }

        $scope.approvalinformation = function (checkpointgroupadd_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/Approvalinformation.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    checkpointgroupadd_gid: checkpointgroupadd_gid
                }
                var url = 'api/AtmMstCheckpointGroup/GetAuditeeCheckpointSummaryList';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.multipleauditee_list = resp.data.multipleauditee_list;
                    unlockUI();

                });
                //var url = 'api/AtmTrnAuditCreation/GetAssignedInformation';
                //SocketService.getparams(url, params).then(function (resp) {
                //    $scope.lblaudit_maker = resp.data.audit_maker;
                //    $scope.lblaudit_checker = resp.data.audit_checker;
                //    $scope.lblauditapprover_name = resp.data.auditapprover_name;
                //    $scope.lblauditperiod_fromdate = resp.data.auditperiod_fromdate;
                //    $scope.lblauditperiod_todate = resp.data.auditperiod_todate;
                //});

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        }
    })();