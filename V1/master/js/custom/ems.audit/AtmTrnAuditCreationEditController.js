(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmTrnAuditCreationEditController', AtmTrnAuditCreationEditController);

    AtmTrnAuditCreationEditController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'cmnfunctionService'];

    function AtmTrnAuditCreationEditController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmTrnAuditCreationEditController';
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var auditcreation_gid = searchObject.auditcreation_gid;
        $scope.lspage = searchObject.lspage;
        var lspage = $scope.lspage;
        activate();
        function activate() {
           
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open1 = true;
            };
            vm.calender2 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open2 = true;
            };
            vm.calender3 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open3 = true;
            };     
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            var url = 'api/AtmMstChecklistMaster/GetChecklistMaster';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.checklistmaster_list = resp.data.checklistmaster_list
                unlockUI();
            });
            var url = 'api/AtmTrnAuditCreation/TempDeleteAuditee';
            SocketService.get(url).then(function (resp) {
            });
            var url = 'api/AtmMstAuditPriority/GetAuditPriorityActive';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.auditpriority_data = resp.data.auditpriority_list;
                unlockUI();
            });
            var url = 'api/AtmMstAuditType/GetAuditTypeActive';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.audittype_list = resp.data.audittype_list;
                unlockUI();
            });
            var url = 'api/AtmMstAuditMapping/GetAuditMappingMaker';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();

                $scope.auditmapping1_list = resp.data.auditmappingmaker_list;

            });

            var url = 'api/AtmMstAuditMapping/GetAuditMappingChecker';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.auditmapping_list = resp.data.auditmappingchecker_list;

            });
            var url = 'api/AtmMstAuditMapping/GetAuditMappingApprover';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.auditmapping2_list = resp.data.auditmappingapprover_list;

            });
            var url = 'api/AtmMstAuditDepartment/GetAuditDepartment';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.auditdepartment_list = resp.data.auditdepartment_list;
                unlockUI();
            });
            var url = 'api/SystemMaster/GetEmployeelist';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employeelist;
                unlockUI();
            });

            var url = 'api/SystemMaster/GetEmployeelist';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.employee1_list = resp.data.employeelist;
                unlockUI();
            });

            var url = 'api/AtmTrnAuditCreation/EditAuditCreation';
            var params = {
                auditcreation_gid: auditcreation_gid
            }
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.cboauditfrequencyedit = resp.data.auditfrequency_gid,
                $scope.cboauditobservationedit = resp.data.auditobservation_name,
                 $scope.cboauditnameedit = resp.data.checklistmaster_gid,
                 $scope.cboauditdepartmentedit = resp.data.auditdepartment_gid,
                  $scope.cboaudittypeedit = resp.data.audittype_gid,
                  $scope.cboauditmakeredit = resp.data.employee_gid,
                 $scope.cboauditcheckeredit = resp.data.auditmapping_gid,
                 $scope.cboauditapproveredit = resp.data.auditmapping2employee_gid,
                 $scope.cboauditpriorityedit = resp.data.auditpriority_gid,               
                 $scope.txteditaudit_ref_no = resp.data.audit_uniqueno,              
                $scope.txteditend_date = resp.data.end_date,
                $scope.txteditperiod_from = resp.data.periodfrom_date,
                $scope.txteditperiod_to = resp.data.auditperiod_to,
                $scope.txteditaudit_ref_no = resp.data.audit_uniqueno,
                 $scope.cboauditeechecker_edit = resp.data.auditeechecker_gid,
                 $scope.cboauditeemaker_edit = resp.data.auditeemaker_gid,
                $scope.txtentity_name = resp.data.entity_name,
                $scope.txtcheckpointgroup_name = resp.data.checkpointgroup_name,
                 $scope.txtauditdescription_name = resp.data.audit_description,
                unlockUI();
            });
            var url = 'api/AtmTrnAuditCreation/GetAuditeeSummaryList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.multipleauditee_list = resp.data.multipleauditee_list;
                unlockUI();

            });
        }
       
        $scope.auditname_change = function (cboauditnameedit) {
            var params = {
                //checklistmaster_gid: $scope.cboauditname.checklistmaster_gid
                checklistmaster_gid: cboauditnameedit

            }
            var url = 'api/AtmTrnAuditCreation/GetAuditNameDetail';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.checklistmaster_gid = resp.data.checklistmaster_gid;
                $scope.checkpointgroup_gid = resp.data.checkpointgroup_gid;
                $scope.txtentity_name = resp.data.entity_name;
                $scope.txtauditdescription_name = resp.data.audit_description;
                $scope.txtcheckpointgroup_name = resp.data.checkpointgroup_name;
            });
            $scope.txtentity_name = '';
        }
        $scope.auditdepartmentname_change = function (cboauditdepartmentedit) {
            var params = {
                // auditdepartment_gid: $scope.cboauditdepartment.auditdepartment_gid
                auditdepartment_gid: cboauditdepartmentedit
            }
            var url = 'api/AtmTrnAuditCreation/GetAuditName';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.checklistmaster_list = resp.data.auditname_list;
            });

        }

       
        $scope.UpdateAuditCreation = function () {

            var auditname;
            var auditname_index = $scope.checklistmaster_list.map(function (e) { return e.checklistmaster_gid }).indexOf($scope.cboauditnameedit);
            if (auditname_index == -1) { auditname = ''; } else { auditname = $scope.checklistmaster_list[auditname_index].audit_name; };

            var auditpriorityname;
            var auditpriority_index = $scope.auditpriority_data.map(function (e) { return e.auditpriority_gid }).indexOf($scope.cboauditpriorityedit);
            if (auditpriority_index == -1) { auditpriorityname = ''; } else { auditpriorityname = $scope.auditpriority_data[auditpriority_index].auditpriority_name; };

            var auditdepartmentname;
            var auditdepartment_index = $scope.auditdepartment_list.map(function (e) { return e.auditdepartment_gid }).indexOf($scope.cboauditdepartmentedit);
            if (auditdepartment_index == -1) { auditdepartmentname = ''; } else { auditdepartmentname = $scope.auditdepartment_list[auditdepartment_index].auditdepartment_name; };

            var audittypename;
            var audittype_index = $scope.audittype_list.map(function (e) { return e.audittype_gid }).indexOf($scope.cboaudittypeedit);
            if (audittype_index == -1) { audittypename = ''; } else { audittypename = $scope.audittype_list[audittype_index].audittype_name; };

            var auditmakername;
            var auditmaker_index = $scope.auditmapping1_list.map(function (e) { return e.employee_gid }).indexOf($scope.cboauditmakeredit);
            if (auditmaker_index == -1) { auditmakername = ''; } else { auditmakername = $scope.auditmapping1_list[auditmaker_index].employee_name; };

            var auditcheckername;
            var auditchecker_index = $scope.auditmapping_list.map(function (e) { return e.auditmapping_gid }).indexOf($scope.cboauditcheckeredit);
            if (auditchecker_index == -1) { auditcheckername = ''; } else { auditcheckername = $scope.auditmapping_list[auditchecker_index].employee_name; };

            var auditapprovername;
            var auditapprover_index = $scope.auditmapping2_list.map(function (e) { return e.auditmapping2employee_gid }).indexOf($scope.cboauditapproveredit);
            if (auditapprover_index == -1) { auditapprovername = ''; } else { auditapprovername = $scope.auditmapping2_list[auditapprover_index].approver_name; };

            var auditeemakername;
            var auditeemaker_index = $scope.employee_list.map(function (e) { return e.employee_gid }).indexOf($scope.cboauditeemaker_edit);
            if (auditeemaker_index == -1) { auditeemakername = ''; } else { auditeemakername = $scope.employee_list[auditeemaker_index].employee_name; };

            var auditeecheckername;
            var auditeechecker_index = $scope.employee1_list.map(function (e) { return e.employee_gid }).indexOf($scope.cboauditeechecker_edit);
            if (auditeechecker_index == -1) { auditeecheckername = ''; } else { auditeecheckername = $scope.employee1_list[auditeechecker_index].employee_name; };

            var params = {

                auditcreation_gid: auditcreation_gid,
                checklistmaster_gid: $scope.cboauditnameedit,
                audit_name: auditname,
                auditdepartment_gid: $scope.cboauditdepartmentedit,
                auditdepartment_name: auditdepartmentname,
                auditpriority_gid: $scope.cboauditpriorityedit,
                auditpriority_name: auditpriorityname,
                audittype_gid: $scope.cboaudittypeedit,
                audittype_name: audittypename,
                auditmapping_gid: $scope.cboauditcheckeredit,
                audit_checker: auditcheckername,
                employee_gid: $scope.cboauditmakeredit,
                audit_maker: auditmakername,
                auditmapping2employee_gid: $scope.cboauditapproveredit,
                audit_approver: auditapprovername,
                audit_uniqueno: $scope.txteditaudit_ref_no,
                end_date: $scope.txteditend_date,             
                periodfrom_date: $scope.txteditperiod_from,
                auditperiod_to: $scope.txteditperiod_to,
                //auditeemaker_gid: $scope.cboauditeemaker_edit,
                //auditeemaker_name: auditeemakername,
                //auditeechecker_gid: $scope.cboauditeechecker_edit,
                //auditeechecker_name: auditeecheckername,
                auditobservation_name: $scope.cboauditobservationedit,
                checkpointgroup_name: $scope.txtcheckpointgroup_name,
                checkpointgroup_gid: $scope.checkpointgroup_gid
            }
           
                var url = 'api/AtmTrnAuditCreation/UpdateAuditCreation';
                lockUI()
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI()
                        activate();
                        $state.go('app.AtmTrnAuditCreationSummary');
                        Notify.alert(resp.data.message, 'success')
                    }
                    else {
                        unlockUI();
                        Notify.alert(resp.data.message, 'warning')
                    }

                });
            }
        
        $scope.back = function (val) {
            if (lspage == 'auditapproved') {
                $location.url('app/AtmTrnInitiateAuditApproved');
            }
            else if (lspage == 'initiateaudit') {
                $location.url('app/AtmTrnAuditCreationSummary');
            }
        }

        $scope.add_auditee = function () {
            var lsauditeemaker_gid = '';
            var lsauditeemaker_name = '';
            if ($scope.cboauditeemaker != undefined || $scope.cboauditeemaker != null) {
                lsauditeemaker_gid = $scope.cboauditeemaker.employee_gid;
                lsauditeemaker_name = $scope.cboauditeemaker.employee_name;
            }
            var lsauditeechecker_gid = '';
            var lsauditeechecker_name = '';
            if ($scope.cboauditeechecker != undefined || $scope.cboauditeechecker != null) {
                lsauditeechecker_gid = $scope.cboauditeechecker.employee_gid;
                lsauditeechecker_name = $scope.cboauditeechecker.employee_name;
            }

            if (($scope.cboauditeemaker == '' || $scope.cboauditeemaker == null) || ($scope.cboauditeechecker == '' || $scope.cboauditeechecker == null)) {
                Notify.alert('Kindly Fill Auditee Details', 'warning')
            }
            else {
                var params = {
                    auditeemaker_gid: lsauditeemaker_gid,
                    auditeemaker_name: lsauditeemaker_name,
                    auditeechecker_gid: lsauditeechecker_gid,
                    auditeechecker_name: lsauditeechecker_name,
                }
                var url = 'api/AtmTrnAuditCreation/MultipleAuditeeEdit';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

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
                    auditee_list();
                    $scope.cboauditeemaker = '';
                    $scope.cboauditeechecker = '';
                });
            }
            
        }
        $scope.delete_auditee = function (multipleauditee_gid) {
            var params =
                {
                    multipleauditee_gid: multipleauditee_gid
                }
            var url = 'api/AtmTrnAuditCreation/DeleteAuditee';

            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
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
                auditee_list();
            });
        }
        function auditee_list() {
            var params =
            {
                auditcreation_gid: auditcreation_gid,
            }
            var url = 'api/AtmTrnAuditCreation/GetTempAssignedAuditeeList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.multipleauditee_list = resp.data.multipleauditee_list;
            });
        }
       
    }
})();