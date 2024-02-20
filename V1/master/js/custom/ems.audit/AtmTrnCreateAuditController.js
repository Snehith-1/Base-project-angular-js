(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmTrnCreateAuditController', AtmTrnCreateAuditController);

    AtmTrnCreateAuditController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AtmTrnCreateAuditController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmTrnCreateAuditController';
        var checklistmasteradd_gid = $location.search().checklistmasteradd_gid;

        activate();

        function activate() {

            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            // Calender Popup... //

            vm.calender2 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open2 = true;
            };

            // Calender Popup... //

            vm.calender3 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open3 = true;
            };


            // Calender Popup... //                   
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
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
            var url = 'api/AtmMstAuditDepartment/GetMappingDepartment';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.auditdepartment_list = resp.data.auditmappingdepartment_list;
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
           
            var d = new Date();
            var time = d.toLocaleString([], { hour: '2-digit', minute: '2-digit' });


            var today = new Date();
            var date = 0 + today.getDate() + '-' + (today.getMonth() + 1) + '-' + today.getFullYear();
            var todaytime = date ;
            $scope.txttoday_date = today;
        }
        $scope.auditname_change = function (cboauditname) {
            var params = {
                checklistmaster_gid: $scope.cboauditname.checklistmaster_gid
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
        $scope.auditdepartmentname_change = function (cboauditdepartment) {
            var params = {
                auditdepartment_gid: $scope.cboauditdepartment.auditdepartment_gid
            }
            var url = 'api/AtmTrnAuditCreation/GetAuditName';           
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.auditname_list = resp.data.auditname_list;
            });

        }
        $scope.submitcreateaudit = function () {

            var lschecklistmaster_gid = '';
            var lsaudit_name = '';
            if ($scope.cboauditname != undefined || $scope.cboauditname != null) {
                lschecklistmaster_gid = $scope.cboauditname.checklistmaster_gid;
                lsaudit_name = $scope.cboauditname.audit_name;
            }

            var lsauditpriority_gid = '';
            var lsauditpriority_name = '';
            if ($scope.cboauditpriority != undefined || $scope.cboauditpriority != null) {
                lsauditpriority_gid = $scope.cboauditpriority.auditpriority_gid;
                lsauditpriority_name = $scope.cboauditpriority.auditpriority_name;
            }
            var lsaudittype_gid = '';
            var lsaudittype_name = '';
            if ($scope.cboaudittype != undefined || $scope.cboaudittype != null) {
                lsaudittype_gid = $scope.cboaudittype.audittype_gid;
                lsaudittype_name = $scope.cboaudittype.audittype_name;
            }

            var lsauditdepartment_gid = '';
            var lsauditdepartment_name = '';
            if ($scope.cboauditdepartment != undefined || $scope.cboauditdepartment != null) {
                lsauditdepartment_gid = $scope.cboauditdepartment.auditdepartment_gid;
                lsauditdepartment_name = $scope.cboauditdepartment.auditdepartment_name;
            }

            var lsauditmapping2employee_gid = '';
            var lsaudittype_approver = '';
            if ($scope.cboauditapprover != undefined || $scope.cboauditapprover != null) {
                lsauditmapping2employee_gid = $scope.cboauditapprover.auditmapping2employee_gid;
                lsaudittype_approver = $scope.cboauditapprover.approver_name;
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
                lsauditmapping_gid = $scope.cboauditchecker.auditmapping_gid;
                lsaudit_checker = $scope.cboauditchecker.employee_name;
            }
            //if ($scope.txtperiod_to < $scope.txtperiod_from) {
            //    Notify.alert("Audit Period to date should be greater than Audit Period From date..!", 'warning');
            //}
            //else if ($scope.txtend_date < $scope.txttoday_date) {
            //    Notify.alert("Audit end date should be greater than Audit Start date..!", 'warning');
            //}
           

            var params = {
                checklistmasteradd_gid: checklistmasteradd_gid,
                audit_uniqueno: $scope.txtaudit_ref_no,
                auditdepartment_gid: lsauditdepartment_gid,
                auditdepartment_name: lsauditdepartment_name,
                audittype_gid: lsaudittype_gid,
                audittype_name: lsaudittype_name,
                auditobservation_name: $scope.auditobservation,
                checklistmaster_gid: lschecklistmaster_gid,
                audit_name: lsaudit_name,
                auditpriority_gid: lsauditpriority_gid,
                auditpriority_name: lsauditpriority_name,
                auditorapprover_gid: lsauditmapping2employee_gid,
                audit_approver: lsaudittype_approver,
                auditormaker_gid: lsemployee_gid,
                audit_maker: lsemployee_name,
                auditorchecker_gid: lsauditmapping_gid,
                audit_checker: lsaudit_checker,
                end_date: $scope.txtend_date,
                periodfrom_date: $scope.txtperiod_from,
                auditperiod_to: $scope.txtperiod_to,
                //auditeemaker_gid: lsauditeemaker_gid,
                //auditeemaker_name: lsauditeemaker_name,
                //auditeechecker_gid: lsauditeechecker_gid,
                //auditeechecker_name: lsauditeechecker_name,
                entity_name: $scope.txtentity_name,
                //audittype_name: $scope.txtaudittype_name,
                checkpointgroup_name: $scope.txtcheckpointgroup_name,
                checkpointgroup_gid: $scope.checkpointgroup_gid,
                audit_description: $scope.txtauditdescription_name,

            }

            var url = 'api/AtmTrnAuditCreation/PostAuditCreation';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.AtmTrnAuditCreationSummary');
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
        
        $scope.back = function (val) {
            $state.go('app.AtmTrnAuditCreationSummary');
        }
        $scope.addauditee = function ()
        {
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
                var url = 'api/AtmTrnAuditCreation/PostMultipleAuditee';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        auditee_list();

                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    $scope.cboauditeemaker = '';
                    $scope.cboauditeechecker = '';
                });
            }
        }


        function auditee_list() {

            var url = 'api/AtmTrnAuditCreation/GetAuditeeList';
            SocketService.get(url).then(function (resp) {
                $scope.multipleauditee_list = resp.data.multipleauditee_list;
            });
        }
        $scope.delete_auditee = function (multipleauditee_gid) {
            var params =
                {
                    multipleauditee_gid: multipleauditee_gid
                }
            var url = 'api/AtmTrnAuditCreation/DeleteAuditeeList';

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
                checkpoint_list();
            });
        }
        function checkpoint_list() {
            var url = 'api/AtmTrnAuditCreation/GetAuditeeList';
            SocketService.get(url).then(function (resp) {
                $scope.multipleauditee_list = resp.data.multipleauditee_list;
            });
        }
    }
})();