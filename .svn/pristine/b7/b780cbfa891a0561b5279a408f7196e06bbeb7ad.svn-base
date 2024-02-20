(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmMstChecklistMasterEditController', AtmMstChecklistMasterEditController);

    AtmMstChecklistMasterEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$modal', 'cmnfunctionService'];

    function AtmMstChecklistMasterEditController($rootScope, $scope, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $modal, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmMstChecklistMasterEditController';
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var checklistmaster_gid = searchObject.checklistmaster_gid;
        var checkpointgroup_gid = searchObject.checkpointgroup_gid;

        activate();
        function activate() {

            //var params = {
            //    checkpointgroup_gid: checkpointgroup_gid
            //}
            //var url = 'api/AtmMstChecklistMaster/GetCheckpointgroupMultiple';
            //SocketService.getparams(url,params).then(function (resp) {
            //    $scope.checkpointgroupadd_list = resp.data.checkpointgroupadd_list;


            //});

            $scope.cbocheckpointgroup_edit = [];
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

            //var url = 'api/SystemMaster/GetEmployeelist';
            //lockUI();
            //SocketService.get(url).then(function (resp) {
            //    $scope.employee_list = resp.data.employeelist;
            //    unlockUI();
            //});

            //var url = 'api/SystemMaster/GetEmployeelist';
            //lockUI();
            //SocketService.get(url).then(function (resp) {
            //    $scope.employee_list1 = resp.data.employeelist;
            //    unlockUI();
            //});

            var url = 'api/AtmMstChecklistMaster/GetCheckpointStatus';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.checklistmaster_list = resp.data.checklistmaster_list;
                unlockUI();
            });
            var url = 'api/AtmMstChecklistMaster/EditChecklistMasterAudit';
            var params = {
                checklistmaster_gid: checklistmaster_gid
            }
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.cboentityedit = resp.data.entity_gid,
                $scope.cboauditdepartmentedit = resp.data.auditdepartment_gid,
                $scope.cboaudittypeedit = resp.data.audittype_gid,
                $scope.cboauditeechecker_edit = resp.data.auditmapping_gid,
                $scope.cboauditeemaker_edit = resp.data.employee_gid,
                $scope.txtauditname_edit = resp.data.audit_name,
                $scope.txtauditedit_description = resp.data.audit_description,
                $scope.checkpointaddgroup_list = resp.data.checkpointaddgroup_list;
                $scope.checkpoint_list = resp.data.checkpoint_list; 
                $scope.cbocheckpointgroup_edit = [];
                if (resp.data.checkpoint_list != null) {
                    var count = resp.data.checkpoint_list.length;
                    for (var i = 0; i < count; i++) {
                        //var indexs = $scope.checkpointaddgroup_list.findIndex(x => x.checkpointgroup_gid === resp.data.checkpoint_list[i].checkpointgroup_gid);
                        var workerIndex = $scope.checkpointaddgroup_list.map(function (x) { return x.checkpointgroup_gid; }).indexOf(resp.data.checkpoint_list[i].checkpointgroup_gid);
                        $scope.cbocheckpointgroup_edit.push($scope.checkpointaddgroup_list[workerIndex]);
                        $scope.$parent.cbocheckpointgroup_edit = $scope.cbocheckpointgroup_edit;
                    }
                }
                unlockUI();
            });


            var url = 'api/AtmMstChecklistMaster/GetChecklistMasterAdd';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.checklistmasteradd_list = resp.data.checklistmasteradd_list;
                unlockUI();
            });

            //angular.forEach($scope.covenant_documentlist, function (value, key) {
            //    var getselected = $scope.covenant_taggeddoclist.find(a => a.companydocument_gid === value.document_gid)
            //    if (getselected != null) {
            //        value.groupcovdocumentchecklist_gid = getselected.groupcovdocumentchecklist_gid;
            //        value.covenantchecked = true;
            //        value.covenantperiod = getselected.covenantperiod == 'Each month' ? 1 : getselected.covenantperiod == 'Every 3 months' ? 2 : getselected.covenantperiod == 'Every 6 months' ? 3 : ""
            //        value.dropdownchk = false;
            //        if (lspage == "StartCreditUnderwriting" && getselected.taggedby == "Credit")
            //            value.taggedby = '';
            //        else if (lspage != "StartCreditUnderwriting" && getselected.taggedby == "CAD")
            //            value.taggedby = '';
            //        else if (getselected.taggedby == "N")
            //            value.taggedby = 'Business';
            //        else
            //            value.taggedby = getselected.taggedby;
            //    }
            //    else {
            //        value.dropdownchk = true;
            //        value.covenantperiod = '';
            //        value.covenantchecked = false;
            //    }
            //});

        }
        $scope.onchangecheckpoint = function (selectedchecklist) {
            lockUI();
            var params = {
                checkpointgroup_list: $scope.cbocheckpointgroup_edit,
                checklistmaster_gid: checklistmaster_gid
            }
            var url = 'api/AtmMstChecklistMaster/GetEditMultipleCheckpointgroup';
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                $scope.checklistmasteradd_list = resp.data.checkpointgroupadd_list;
            });
        }
        
        $scope.UpdateChecklistMaster = function () {

            if ($scope.checklistmasteradd_list == null) {
                Notify.alert('Select Atleast One Checkpointgroup!', 'warning');
                unlockUI();
                return false;
            }

            var entityname;
            var entityname_index = $scope.entity_data.map(function (e) { return e.entity_gid }).indexOf($scope.cboentityedit);
            if (entityname_index == -1) { entityname = ''; } else { entityname = $scope.entity_data[entityname_index].entity_name; };

            var auditdepartmentname;
            var auditdepartment_index = $scope.auditdepartment_list.map(function (e) { return e.auditdepartment_gid }).indexOf($scope.cboauditdepartmentedit);
            if (auditdepartment_index == -1) { auditdepartmentname = ''; } else { auditdepartmentname = $scope.auditdepartment_list[auditdepartment_index].auditdepartment_name; };

            var audittypename;
            var audittype_index = $scope.audittype_data.map(function (e) { return e.audittype_gid }).indexOf($scope.cboaudittypeedit);
            if (audittype_index == -1) { audittypename = ''; } else { audittypename = $scope.audittype_data[audittype_index].audittype_name; };

            var checkpointgroupname;
            var checkpointgroup_index = $scope.checkpoint_list.map(function (e) { return e.checkpointgroup_gid }).indexOf($scope.cbocheckpointgroup_edit);
            if (checkpointgroup_index == -1) { checkpointgroupname = ''; } else { checkpointgroupname = $scope.checkpoint_list[checkpointgroup_index].checkpointgroup_name; };


            var params = {

                checklistmaster_gid: checklistmaster_gid,
                entity_gid: $scope.cboentityedit,
                entity_name: entityname,
                auditdepartment_gid: $scope.cboauditdepartmentedit,
                auditdepartment_name: auditdepartmentname,
                audittype_gid: $scope.cboaudittypeedit,
                audittype_name: audittypename,
                checkpoint_list: $scope.cbocheckpointgroup_edit,
                //checkpointgroup_name: checkpointgroupname,               
                audit_name: $scope.txtauditname_edit,
                audit_description: $scope.txtauditedit_description,


            }

            var url = 'api/AtmMstChecklistMaster/UpdateChecklistMasterAudit';
            lockUI()
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI()
                    activate();
                    $state.go('app.AtmMstChecklistMasterSummary');
                    Notify.alert(resp.data.message, 'success')
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, 'warning')
                }

            });
        }

        $scope.back = function (val) {
            $state.go('app.AtmMstChecklistMasterSummary');
        }
        $scope.editcheckpoint = function (val3) {
            $location.url('app/AtmMstChecklistMasterAuditEdit?checklistmasteradd_gid=' + val3)
        }
        $scope.viewcheckpoint = function (val2) {
            $location.url('app/AtmMstChecklistMasterAuditView?checklistmasteradd_gid=' + val2)
        }
        $scope.deletechecklistmaster = function (checklistmasteradd_gid) {
            var params = {
                checklistmasteradd_gid: checklistmasteradd_gid
            }


            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {

                    var url = 'api/AtmMstChecklistMaster/DeleteChecklistMasterAdd';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting ChecklistMaster !!!', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        };
        $scope.checkpointintent = function (checklistmasteradd_gid, checkpoint_intent) {
            var modalInstance = $modal.open({
                templateUrl: '/checkpointintent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) { 
                $scope.txtcheckpointintent = checkpoint_intent; 
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.description = function (checklistmasteradd_gid, checkpoint_description) {
            var modalInstance = $modal.open({
                templateUrl: '/checkpointdescription.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {  
                $scope.txtcheckpointdescription = checkpoint_description; 
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.notetoauditor = function (checklistmasteradd_gid, noteto_auditor) {
            var modalInstance = $modal.open({
                templateUrl: '/notetoauditor.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.txtnotetoauditor = noteto_auditor;
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.auditname = function (checklistmaster_gid, audit_description) {
            var modalInstance = $modal.open({
                templateUrl: '/auditname.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    checklistmaster_gid: checklistmaster_gid
                }
                lockUI();
                var url = 'api/AtmMstChecklistMaster/GetChecklistMasterAuditorName';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtaudit_description = resp.data.audit_description;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
    }
})();