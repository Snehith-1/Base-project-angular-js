(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmTrnAudit360ViewController', AtmTrnAudit360ViewController);

    AtmTrnAudit360ViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$modal', 'cmnfunctionService'];

    function AtmTrnAudit360ViewController($rootScope, $scope, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $modal, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmTrnAudit360ViewController';

        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.auditcreation_gid = searchObject.auditcreation_gid;
        var auditcreation_gid = $scope.auditcreation_gid;
        var checklistmaster_gid = searchObject.checklistmaster_gid;
        $scope.checklistmaster_gid = searchObject.checklistmaster_gid;
        var checklistmasteradd_gid = searchObject.checklistmasteradd_gid;
        $scope.checklistmasteradd_gid = searchObject.checklistmasteradd_gid;
        var sampleimport_gid = searchObject.sampleimport_gid;
        $scope.sampleimport_gid = searchObject.sampleimport_gid;
        $scope.lspage = searchObject.lspage;
        var lspage = $scope.lspage;
        activate();

        function activate() {

            var url = 'api/AtmTrnAuditCreation/EditAuditCreation';
            var params = {
                auditcreation_gid: auditcreation_gid
            }
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.cboauditdepartment = resp.data.auditdepartment_gid,
             $scope.auditobservation = resp.data.auditobservation_name,
                  $scope.cboauditdepartment = resp.data.auditdepartment_name,
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
                $scope.txtdue_date = resp.data.end_date,
                $scope.txtreport_date = resp.data.report_date,
                $scope.txtperiod_from = resp.data.periodfrom_date,
                $scope.txtperiod_to = resp.data.auditperiod_to,
                $scope.txtaudit_ref_no = resp.data.audit_uniqueno,
                $scope.cboauditeemaker = resp.data.auditeemaker_name,
                $scope.cboauditeechecker = resp.data.auditeechecker_name,
                $scope.created_by = resp.data.created_by;
                $scope.created_date = resp.data.created_date;
                $scope.txtentity_name = resp.data.entity_name,
              $scope.txtaudit_type = resp.data.audittype_name,
              $scope.txtcheckpoint_group = resp.data.checkpointgroup_name,
               $scope.txtrejected_remarks = resp.data.rejected_remarks,
              $scope.txtaudit_desc = resp.data.audit_description
                if (resp.data.status_update == 'Open') {
                    $scope.showAprrovalbutton = true;
                    var params = {
                        checklistmaster_gid: checklistmaster_gid,
                        auditcreation_gid: auditcreation_gid
                    }
                    var url = 'api/AtmTrnAuditCreation/CheckpointCreation';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.checkpointsummary_list = resp.data.auditcheckpointsummary_list;

                        angular.forEach($scope.checkpointsummary_list, function (value, key) {
                            //if (value.checklist_flag != '0') {
                            //    value.checked = true;
                            //}

                            value.checked = true;
                        });

                    });
                }
                else if (resp.data.status_update == 'Initiate Audit Approval pending')
                {
                    var params = {
                        checklistmaster_gid: checklistmaster_gid,
                        auditcreation_gid: auditcreation_gid
                    }
                    var url = 'api/AtmTrnAuditCreation/CheckpointCreation';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.checkpointsummary_list = resp.data.auditcheckpointsummary_list;                       
                    });
                }
                else {
                    var params = {
                        auditcreation_gid: auditcreation_gid
                    }
                    var url = 'api/AtmTrnAuditCreation/TrnCheckpointCreation';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.checkpointsummary_list = resp.data.auditcheckpointsummary_list;
                    }); 
                    $scope.showAprrovalbutton = false;
                }

                if (resp.data.status_update == 'Audit Approval pending' || resp.data.status_update == "Approved")
                    $scope.showsubmitbutton = true;
                else
                    $scope.showsubmitbutton = false;
                unlockUI();
            });
            var url = 'api/AtmTrnAuditorMaker/GetSampleResponseQuery';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.approval_status = resp.data.approval_status;
                if ($scope.approval_status == 'Completed')
                    $scope.hide = 1;
                unlockUI();
            });

            var url = 'api/AtmTrnAuditCreation/GetAuditeeSummaryList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.multipleauditee_list = resp.data.multipleauditee_list;
                unlockUI();

            });
            //var url = 'api/AtmTrnAuditCreation/GetAudit360View';
            //lockUI();
            //SocketService.getparams(url, params).then(function (resp) {
            //    $scope.auditcreation_list = resp.data.auditcreation_list;
            //    unlockUI();

            //});
            defaultdynamic();


            var params = {
                auditcreation_gid: auditcreation_gid,
                sampleimport_gid: sampleimport_gid
            };

            //var url = 'api/AtmTrnSampling/GetAssignedQuerySummary';
            //SocketService.getparams(url, params).then(function (resp) {
            //    $scope.SampleAssignedQueryList = resp.data.SampleAssignedQueryList;
            //    unlockUI();
            //});


            //var url = 'api/AtmTrnSampling/GetSamplesummary';
            //SocketService.getparams(url, params).then(function (resp) {
            //    unlockUI()
            //    $scope.sample_list = resp.data.sample_list

            //});

            var params = {
                checklistmaster_gid: checklistmaster_gid,
                auditcreation_gid: auditcreation_gid
            }
            var url = 'api/AtmTrnAuditCreation/CheckpointCreation';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.checkpointsummary_list = resp.data.auditcheckpointsummary_list;

                angular.forEach($scope.checkpointsummary_list, function (value, key) {
                    if (value.checklist_flag != '0') {
                        value.checked = true;
                    }
                });

            });

        }

        function defaultdynamic() {
            var params = {
                auditcreation_gid: auditcreation_gid
            }
            var url = 'api/AtmTrnSampling/GetSampleDynamicdata';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.SampleDynamicdata = JSON.parse(resp.data.JSONdata);
                    $scope.SampleDynamicTabledata = angular.copy($scope.SampleDynamicdata);
                    $scope.SampleDynamicRaisedTabledata = angular.copy($scope.SampleDynamicdata);

                    $scope.SampleDynamicTabledata = $scope.SampleDynamicTabledata.filter(function (el) { return el.raisedquery_flag === 'N' });
                    $scope.SampleDynamicTable = angular.copy($scope.SampleDynamicTabledata);
                    $scope.raisedqueryarray = $scope.SampleDynamicRaisedTabledata.filter(function (el) { return el.raisedquery_flag === 'Y' });
                    $scope.SampleraisedqueryTable = angular.copy($scope.raisedqueryarray);
                    for (i in $scope.SampleDynamicTabledata) {
                        delete $scope.SampleDynamicTabledata[i].sampleimport_gid;
                        delete $scope.SampleDynamicTabledata[i].raisedquery_flag;
                        delete $scope.SampleDynamicTabledata[i].taguser_flag;
                        delete $scope.SampleDynamicTabledata[i].tagged_employee;
                    }

                    for (i in $scope.raisedqueryarray) {
                        delete $scope.raisedqueryarray[i].sampleimport_gid;
                        delete $scope.raisedqueryarray[i].raisedquery_flag;
                    }
                    $scope.raisedquerytable = true;
                    $scope.showtablediv = true;
                    if ($scope.SampleDynamicTabledata.length == 0) {
                        $scope.showtablediv = false;
                    }
                    if ($scope.raisedqueryarray.length == 0) {
                        $scope.raisedquerytable = false;
                    }

                }
                else {
                    $scope.SampleDynamicTabledata = "";
                    $scope.raisedqueryarray = "";
                    $scope.showtablediv = false;
                    $scope.raisedquerytable = false;
                }
            });
        }

        $scope.checksampleall = function (selected) {

            angular.forEach($scope.checkpointsummary_list, function (val) {
                val.checked = selected;
            });
        }

        $scope.stripAddr = function (value) {
            return value.replace(/_/g, ' ');
        }

        $scope.dynamicsample = function (index, auditcreation_gid) {
            var sampleimport_gid = "";
            var getsampleimport_gid = $scope.SampleDynamicTable[index];
            if (getsampleimport_gid && getsampleimport_gid.sampleimport_gid) {
                sampleimport_gid = getsampleimport_gid.sampleimport_gid;
            }
            var modalInstance = $modal.open({
                templateUrl: '/raisequery.html',
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
                var url = 'api/AtmTrnSampling/GetSampleName';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.sampleimport_gid = resp.data.sampleimport_gid;
                    $scope.txtsample_name = resp.data.sample_name;
                });

                var url = 'api/SystemMaster/GetEmployeelist';
                SocketService.get(url).then(function (resp) {
                    $scope.cboemployee_list = resp.data.employeelist;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.submit = function () {

                    var query_toemployeegid = "";
                    var query_toname = "";
                    if ($scope.cboqueryto) {
                        query_toemployeegid = $scope.cboqueryto.employee_gid;
                        query_toname = $scope.cboqueryto.employee_name;
                    }

                    var params = {
                        sampleimport_gid: sampleimport_gid,
                        description: $scope.txtquery_desc,
                        auditcreation_gid: auditcreation_gid,
                        query_title: $scope.txtquery_title,
                        query_to: query_toemployeegid,
                        query_toname: query_toname,
                    }

                    var url = 'api/AtmTrnSampling/PostRaiseQuery';
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
                            defaultdynamic();
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


        $scope.dynamicsampledelete = function (index) {
            var sampleimport_gid = "";
            var getsampleimport_gid = $scope.SampleDynamicTable[index];
            if (getsampleimport_gid && getsampleimport_gid.sampleimport_gid) {
                sampleimport_gid = getsampleimport_gid.sampleimport_gid;
            }
            var params = {
                sampleimport_gid: sampleimport_gid
            }
            var url = 'api/AtmTrnAuditCreation/GetDeleteSampleImport';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    defaultdynamic();
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

        $scope.checkall = function (selected) {

            angular.forEach($scope.checkpointsummary_list, function (val) {
                val.checked = selected;
            });
        }


        $scope.assignchecklist = function (submitevent) {
            lockUI();
            var assignList = [];
            angular.forEach($scope.checkpointsummary_list, function (val) { 
                if (val.checked == true) {
                    var checklistmasteradd_gid = val.checklistmasteradd_gid;
                    assignList.push(checklistmasteradd_gid);
                    var checklistmaster_gid = val.checklistmaster_gid;

                }
            });
            if (assignList.length == 0) {
                Notify.alert('Select Atleast One Record!', 'warning'); 
                unlockUI();
                return false; 
            }
            var params = {
                auditcreation_gid: auditcreation_gid,
                checklistmasteradd_gid: assignList,
                checklistmaster_gid: checklistmaster_gid,
                submitevent: submitevent
            }

            var url = 'api/AtmTrnAuditCreation/PostChecklistAssign';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    if (submitevent == 'Y') {
                        Notify.alert('Checklist Assigned Successfully!', 'success');
                    }
                    else {
                        Notify.alert(resp.data.message, 'success');
                    }
                    $state.go('app.AtmTrnAuditCreationSummary');
                    unlockUI();
                }
                else {
                    Notify.alert('Error Occured..!', 'warning');
                    unlockUI();
                }

            });

        }
       

        //$scope.auditcreation_gid = $location.search().auditcreation_gid;
        //var auditcreation_gid = $location.search().auditcreation_gid;

        $scope.importexcel = function (auditcreation_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/importexcel.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {


                var params = {
                    auditcreation_gid: auditcreation_gid,
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.downloadtemplate_importexcel = function () {
                    //var filename = "ImportExcelSample.xlsx";
                    ////var phyPath = resp.data.file_path;
                    //var phyPath = "E:\\Web\\EMS\\templates\\ImportExcelSample.xlsx";
                    //var relPath = phyPath.split("EMS");
                    //var relpath1 = relPath[1].replace("\\", "/");
                    //var prefix = window.location.protocol;
                    //var hosts = window.location.host;
                    //var prefix = "http://"
                    //var str = prefix.concat(hosts, relpath1);
                    //var link = document.createElement("a");
                    //var name = filename.split('.');
                    //link.download = name[0];
                    //var uri = str;
                    //link.href = uri;
                    //link.click();

                    var Templateurl = apiManage.GetCommonData['TemplatePath'].Path;
                    var filename = "\ImportExcelSample.xlsx";
                    var phyPath = Templateurl + filename;
                    var relPath = phyPath.split("EMS");
                    var relpath1 = relPath[1].replace("\\", "/");
                    var prefix = window.location.protocol + "//";
                    var hosts = window.location.host;
                    var str = prefix.concat(hosts, relpath1);
                    var link = document.createElement("a");
                    var name = filename.split('.');
                    link.download = name[0];
                    link.href = str;
                    link.click();
                }

                $scope.excelupload = function (val, val1, name) {

                    var fileInput = document.getElementById('fileimport');
                    var filePath = fileInput.value;

                    $scope.fileinputvalue = filePath;

                    // Allowing file type
                    var allowedExtensions = /(\.xls|\.xlsx|\.csv)$/i;

                    if (!allowedExtensions.exec(filePath)) {
                        Notify.alert('File Format Not Supported!', 'warning')
                        $modalInstance.close('closed');
                        //fileInput.value = '';
                    }
                    else if (filePath.includes("ImportExcelSample") == false) {
                        Notify.alert('File Name / Template Not Supported!', 'warning')
                        $modalInstance.close('closed');
                    }
                    else {
                        var item = {
                            name: val[0].name,
                            file: val[0]
                        };
                        var frm = new FormData();
                        frm.append('fileupload', item.file);
                        frm.append('file_name', item.name);
                        frm.append('auditcreation_gid', auditcreation_gid);
                        $scope.uploadfrm = frm;
                    }
                }

                $scope.uploadexcel = function () {

                    if ($scope.fileinputvalue == '' || $scope.fileinputvalue == undefined || $scope.fileinputvalue == null) {
                        Notify.alert('Kindly Select the Excel file', 'warning')
                    }
                    else {
                        var url = 'api/AtmTrnSampling/ImportExcelSample';
                        lockUI();
                        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                            $modalInstance.close('closed');
                            if (resp.data.status == true) {
                                defaultdynamic();
                                unlockUI();
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                //  $location.url('app/AtmTrnAudit360View?auditcreation_gid=' + val1 + '&checklistmaster_gid=' + val2 + '&sampleimport_gid=' + val3)

                            }
                            else {
                                unlockUI();
                                Notify.alert(resp.data.message, {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $modalInstance.close('closed');
                            }
                            $("#fileimport").val('');
                        });
                    }

                }

                $scope.uploadexcelcancel = function () {
                    $("#fileimport").val('');
                };
            }
        }

        $scope.checkpointintent = function (checklistmasteradd_gid, checkpoint_intent, checkpoint_description) {
            var modalInstance = $modal.open({
                templateUrl: '/checkpointintent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.lblcheckpoint_description = checkpoint_description;
                var params = {
                    checklistmasteradd_gid: checklistmasteradd_gid
                }
                lockUI();
                var url = 'api/AtmMstChecklistMaster/GetChecklistMasterIntent';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtcheckpointintent = resp.data.checkpoint_intent;

                });
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
                'use strict';

                angular
                    .module('angle')
                var params = {
                    checklistmasteradd_gid: checklistmasteradd_gid
                }
                lockUI();
                var url = 'api/AtmMstChecklistMaster/GetChecklistMasterDescription';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtcheckpointdescription = resp.data.checkpoint_description;

                });
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
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    checklistmasteradd_gid: checklistmasteradd_gid
                }
                lockUI();
                var url = 'api/AtmMstChecklistMaster/GetChecklistMasterAuditor';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtnotetoauditor = resp.data.noteto_auditor;

                });

                var params = {
                    checklistmasteradd_gid: checklistmasteradd_gid
                }
                lockUI();
                var url = 'api/AtmMstChecklistMaster/GetChecklistMasterDescription';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtcheckpointdescription = resp.data.checkpoint_description;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
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

        $scope.btntaguser = function (index, auditcreation_gid, raisedquery) {
            var sampleimport_gid = "";

            if (raisedquery == 'Y') {
                var getsampleimport_gid = $scope.SampleraisedqueryTable[index];
                if (getsampleimport_gid && getsampleimport_gid.sampleimport_gid) {
                    sampleimport_gid = getsampleimport_gid.sampleimport_gid;
                }
            }
            else {
                var getsampleimport_gid = $scope.SampleDynamicTable[index];
                if (getsampleimport_gid && getsampleimport_gid.sampleimport_gid) {
                    sampleimport_gid = getsampleimport_gid.sampleimport_gid;
                }
            }

            var modalInstance = $modal.open({
                templateUrl: '/taguser.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.hide = 0;
                var params = {
                    auditcreation_gid: auditcreation_gid
                };
                var url = 'api/AtmTrnAuditorMaker/GetSampleResponseQuery';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.approval_status = resp.data.approval_status;
                    if ($scope.approval_status == 'Completed') {
                        $scope.hide = 1;
                    }

                    unlockUI();
                });
                var params = {
                    sampleimport_gid: sampleimport_gid
                }

                var url = 'api/AtmTrnSampling/AssignedTagUserSummary';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.SampleTagUserList = resp.data.SampleAssignedQueryList;
                });

                var url = 'api/AtmTrnSampling/GetSampleName';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtsample_name = resp.data.sample_name;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };



                var url = 'api/SystemMaster/GetEmployeelist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employeelist;
                    unlockUI();
                });

                $scope.btnconfirm = function () {

                    var params = {
                        employelist: $scope.cboemployee_name,
                        sample_name: $scope.txtsample_name,
                        sampleimport_gid: sampleimport_gid,
                        auditcreation_gid: auditcreation_gid,
                        description: $scope.txttaguser_desc
                    }

                    var url = 'api/AtmTrnSampling/GetTagUser';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
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

        $scope.raisequery = function (sampleimport_gid, auditcreation_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/raisequery.html',
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

                var url = 'api/AtmTrnSampling/GetSampleName';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.sampleimport_gid = resp.data.sampleimport_gid;
                    $scope.txtsample_name = resp.data.sample_name;
                });

                var params = {
                    auditcreation_gid: auditcreation_gid,
                    sampleimport_gid: sampleimport_gid
                }
                var url = 'api/AtmTrnSampling/EditSampleQuery';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.sampleimport_gid = resp.data.sampleimport_gid
                    $scope.auditcreation_gid = resp.data.auditcreation_gid
                });


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };



                $scope.submit = function () {

                    var params = {
                        sampleimport_gid: $scope.sampleimport_gid,
                        sample_name: $scope.txtsample_name,
                        description: $scope.txtdescription,
                        auditcreation_gid: $scope.auditcreation_gid
                    }

                    var url = 'api/AtmTrnSampling/PostRaiseQuery';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            location.reload();
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            //$location.url('app/AtmTrnAudit360View?auditcreation_gid=' + val1 + '&checklistmaster_gid=' + val2 + '&sampleimport_gid=' + val3)
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





        //$scope.back = function (val) {
        //    $state.go('app.AtmTrnAuditCreationSummary');
        //}
        $scope.back = function (val) {
            if (lspage == 'initiateaudit') {
                $location.url('app/AtmTrnAuditCreationSummary');
            }
            else if (lspage == 'auditapproved') {
                $location.url('app/AtmTrnInitiateAuditApproved');
            }
            else if (lspage == 'audithold') {
                $location.url('app/AtmTrnInitiateAuditHold');
            }
            else if (lspage == 'auditclosed') {
                $location.url('app/AtmTrnInitiateAuditClosed');
            }
            else if (lspage == 'auditcompleted') {
                $location.url('app/AtmTrnInitiateAuditCompleted');
            }
            else if (lspage == 'auditrejected') {
                $location.url('app/AtmTrnInitiateAuditRejected');
            }

        }
        $scope.samplequery = function (index) {
            // var auditcreation_gid = $scope.auditcreation_gid;
            var checklistmaster_gid = $scope.checklistmaster_gid;
            var sampleimport_gid = "";
            var getsampleimport_gid = $scope.SampleraisedqueryTable[index];
            if (getsampleimport_gid && getsampleimport_gid.sampleimport_gid) {
                sampleimport_gid = getsampleimport_gid.sampleimport_gid;
            }
            $location.url('app/AtmTrnSampleQueryAuditor?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&checklistmaster_gid=' + checklistmaster_gid + '&sampleimport_gid=' + sampleimport_gid + '&lspage=' + lspage))
        }

        //$scope.view = function (val1, val2, val3) {
        //    var auditcreation_gid = $scope.auditcreation_gid;
        //    var checklistmaster_gid = $scope.checklistmaster_gid;
        //    var sampleimport_gid = $scope.sampleimport_gid;
        //    $location.url('app/AtmTrnAuditTaskSample?auditcreation_gid=' + val1 + '&checklistmaster_gid=' + val2 + '&sampleimport_gid=' + val3)
        //}
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
