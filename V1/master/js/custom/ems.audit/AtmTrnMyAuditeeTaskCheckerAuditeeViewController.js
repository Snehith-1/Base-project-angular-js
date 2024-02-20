(function () {
    'use strict';
    angular
        .module('angle')
        .controller('AtmTrnMyAuditeeTaskCheckerAuditeeViewController', AtmTrnMyAuditeeTaskCheckerAuditeeViewController);

    AtmTrnMyAuditeeTaskCheckerAuditeeViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function AtmTrnMyAuditeeTaskCheckerAuditeeViewController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmTrnMyAuditeeTaskCheckerAuditeeViewController';
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.auditcreation_gid = searchObject.auditcreation_gid;
        var auditcreation_gid = $scope.auditcreation_gid;
        $scope.checklistmaster_gid = searchObject.checklistmaster_gid;
        var checklistmaster_gid = $scope.checklistmaster_gid;
        $scope.checklistmasteradd_gid = searchObject.checklistmasteradd_gid;
        $scope.sampleimport_gid = searchObject.sampleimport_gid;
        var sampleimport_gid = $scope.sampleimport_gid;
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
                 $scope.txtentity_name = resp.data.entity_name,
                $scope.txtaudit_type = resp.data.audittype_name,
                $scope.txtcheckpoint_group = resp.data.checkpointgroup_name,
                    $scope.txtaudit_desc = resp.data.audit_description,
                    $scope.txttotal_score = resp.data.observation_score,
                 $scope.created_by = resp.data.created_by;
                $scope.created_date = resp.data.created_date;
                $scope.auditeechecker_approvalflag = resp.data.auditeechecker_approvalflag;
                $scope.openquerycount = resp.data.openquerycount;
                $scope.txt_percent = resp.data.observation_percentage;
                $scope.auditobservation_name = resp.data.auditobservation_name;
                $scope.samplestatus_flag = resp.data.samplestatus_flag;
                $scope.checklistverified_flag = resp.data.checklistverified_flag;

                $scope.raisequeryoption_disabled = false;
                if ((lspage == 'auditeemakeropen' || lspage == 'auditeemakerhold' || lspage == 'auditeemakerclosed' || lspage == 'auditeemakertagged' || lspage == 'auditeemakercompleted')) {
                    $scope.raisequeryoption_disabled = true;
                }
                    //if (lspage == 'auditeecheckerpending' && $scope.auditeechecker_approvalflag == "N") {
                    //    $scope.proceedtoapprover = true;
                    //}
                else {
                    $scope.raisequeryoption_disabled = false;
                }
                unlockUI();
            });

            //api 

            var params = {
                auditcreation_gid: auditcreation_gid
            }
            var url = 'api/AtmTrnAuditCreation/TrnCheckpointCreation';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.checkpointsummary_list = resp.data.auditcheckpointsummary_list;
            });
            var url = 'api/AtmTrnAuditCreation/GetAuditeeSummaryList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.multipleauditee_list = resp.data.multipleauditee_list;
                unlockUI();

            });
            var url = 'api/AtmTrnAuditorMaker/GetMultipleAuditeecheckerApproval';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.auditee_flag = resp.data.auditee_flag;
                unlockUI();

            });

            var url = 'api/AtmTrnAuditorMaker/GetAuditorMakerViewOverallscore';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txt_overallscore = resp.data.overall_score;
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
            var params = {
                auditcreation_gid: auditcreation_gid
            };


            var url = 'api/AtmTrnMyAuditTaskAuditee/AuditeeCheckerView';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.makercheckpointobservation_list = resp.data.makercheckpointobservationview_list;
                //$scope.txttotal_score = resp.data.total_score;

                for (var i = 0; i < $scope.makercheckpointobservation_list.length; i++) {
                    $scope.makercheckpointobservation_list[i].checked = true;

                }

                angular.forEach($scope.makercheckpointobservation_list, function (value, key) {

                    if (value.capture_score == value.yes_score) {
                        value.yes_radio = true;
                    }
                    else if (value.capture_score == value.no_score) {
                        value.no_radio = true;
                    }
                    else if (value.capture_score == value.partial_score) {
                        value.partialscore_radio = true;
                    }
                    else if (value.capture_score == value.na_score) {
                        value.nascore_radio = true;
                    }
                });
            });
            var url = 'api/AtmTrnAuditorMaker/AuditRaisedQuerySummary';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.auditquerydata = resp.data.auditquerydata;
                $scope.close_disabled = false;
                if ((lspage == 'auditeemakeropen' || lspage == 'auditeemakerhold' || lspage == 'auditeemakerclosed' || lspage == 'auditeemakertagged' || lspage == 'auditeemakercompleted' ||
                    lspage == 'auditeecheckeropen' || lspage == 'auditeecheckerpending' || lspage == 'auditeecheckerhold' || lspage == 'auditeecheckerclosed' || lspage == 'auditeecheckertagged' ||
                    lspage == 'auditeecheckercompleted')) {
                    $scope.close_disabled = true;
                }
            });

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
            //    // auditcreation_gid = resp.data.auditcreation_gid
            //    $scope.sample_list = resp.data.sample_list

            //});

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
                        //delete $scope.SampleDynamicTabledata[i].sampleimport_gid;
                        delete $scope.SampleDynamicTabledata[i].raisedquery_flag;
                        delete $scope.SampleDynamicTabledata[i].taguser_flag;
                        delete $scope.SampleDynamicTabledata[i].tagged_employee;
                    }

                    for (i in $scope.raisedqueryarray) {
                        //delete $scope.raisedqueryarray[i].sampleimport_gid;
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

        $scope.stripAddr = function (value) {
            return value.replace("_", " ");
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
                    activate();
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

       

        $scope.assignchecklist = function () {
            var assignList = [];
            angular.forEach($scope.checkpointsummary_list, function (val) {

                if (val.checked == true) {
                    var checklistmasteradd_gid = val.checklistmasteradd_gid;
                    assignList.push(checklistmasteradd_gid);
                    var checklistmaster_gid = val.checklistmaster_gid;

                }
            });
            if (assignList.length == 0) {
                Notify.alert('Select Atleast One Record!');
                return false;
                unlockUI();
            }
            var params = {
                checklistmasteradd_gid: assignList,
                checklistmaster_gid: checklistmaster_gid
            }

            var url = 'api/AtmTrnAuditCreation/PostChecklistAssign';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert('Checklist Assigned Successfully!', 'success');
                    //$location.url('app/AtmTrnAudit360View?auditcreation_gid=' + auditcreation_gid + '&checklistmaster_gid=' + checklistmaster_gid)
                    $state.go('app.AtmTrnAuditCreationSummary');
                }
                else {
                    Notify.alert('Select Atleast One..!!', 'warning')
                }

            });

        }

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
        $scope.notetoauditor = function (checklistmasteradd_gid, noteto_auditor, checkpoint_description) {
            var modalInstance = $modal.open({
                templateUrl: '/notetoauditor.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.txtnotetoauditor = noteto_auditor;
                $scope.txtcheckpointdescription = checkpoint_description;
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

        $scope.getapprovalclick = function (auditcreation_gid) {
            //if ($scope.openquerycount != '0') {
            //    Notify.alert("Query is not closed", {
            //        status: 'warning',
            //        pos: 'top-center',
            //        timeout: 3000
            //    });
            //}
            //else {
            var params = {
                //approval_name: $scope.cboapproval_member.employee_name,
                auditeechecker: 'Y',
                auditcreation_gid: auditcreation_gid,
                getapproval_remark: $scope.getapproval_remark,
                observation_percentage: $scope.txt_percent
            }
            lockUI();
            var url = "api/AtmTrnAuditorMaker/PostAuditeeGetApproval";
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $state.go('app.AtmTrnCheckerPendingApproval');
                }
                else {
                    modalInstance.close('closed');
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                }
            });
            //}
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
        $scope.samplequery = function (index) {
            var auditcreation_gid = $scope.auditcreation_gid;
            var checklistmaster_gid = $scope.checklistmaster_gid;
            var sampleimport_gid = "";
            var getsampleimport_gid = $scope.SampleraisedqueryTable[index];
            if (getsampleimport_gid && getsampleimport_gid.sampleimport_gid) {
                sampleimport_gid = getsampleimport_gid.sampleimport_gid;
            }
            $location.url('app/AtmTrnSampleQueryAuditor?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&checklistmaster_gid=' + checklistmaster_gid + '&sampleimport_gid=' + sampleimport_gid + '&lspage=' + lspage))
        }
        $scope.observationquery = function (checkpointgroupadd_gid, auditcreation2checklist_gid, auditcreation_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/TagToDefferalEdit.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                var params =
                    {
                        checkpointgroupadd_gid: checkpointgroupadd_gid,
                        auditcreation_gid: auditcreation_gid
                    }
                //var url = 'api/AtmMstCheckpointGroup/GetChecklistToCheckpointcreate';
                //lockUI();
                //SocketService.getparams(url,params).then(function (resp) {
                //    $scope.checklistcheckpoint_list = resp.data.checklistcheckpoint_list;
                //    unlockUI();
                //});
                var url = 'api/AtmMstCheckpointGroup/GetChecklistToCheckpointcreate';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.checklistcheckpoint_list = resp.data.checklistcheckpoint_list;
                    for (var i = 0; i < $scope.checklistcheckpoint_list.length; i++) {
                        $scope.checklistcheckpoint_list[i].checked = true;
                    }
                    angular.forEach($scope.checklistcheckpoint_list, function (value, key) {

                        if (value.overall_detail == "Yes") {
                            value.document_radio = true;
                            //value.document_radio1 = true;
                        }
                        else if (value.overall_detail == "No") {
                            value.tag_radio = true;
                            //value.tag_radio1 = true;
                        }
                        else if (value.overall_detail == "Partial") {
                            value.partial_radio = true;
                            //value.tag_radio1 = true;
                        }
                        else if (value.overall_detail == "NA") {
                            value.na_radio = true;
                            //value.tag_radio1 = true;
                        }
                    });
                });

                $scope.checkpointobservation = function () {

                    var params = {
                        checkpointgroupadd_gid: checkpointgroupadd_gid,
                        auditcreation_gid: auditcreation_gid,
                    }
                    var url = 'api/AtmTrnAuditorMaker/PostCheckpointObservation';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        lockUI();
                        if (resp.data.status == true) {
                            unlockUI();
                            Notify.alert('Observation Checkpoint Saved Successfully!', 'success');
                            $location.url('app/AtmTrnAuditorMakerView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&lspage=' + 'auditormakerOpen'))
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            //unlockUI();
                        }
                        $modalInstance.close('closed');

                    });
                }
                var params = {
                    checkpointgroupadd_gid: checkpointgroupadd_gid,
                    auditcreation_gid: auditcreation_gid
                }
                var url = 'api/AtmTrnAuditorMaker/GetAuditorCheckpointFlag';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.checklistcheckpoint_flag = resp.data.checklistcheckpoint_flag;
                    unlockUI();

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.onselected = function (val, val1) {

                    $scope.document_verified = val;
                    $scope.checklist2checkpoint = val1;
                    var params = {
                        auditcreation2checklist_gid: auditcreation2checklist_gid,
                        document_verified: $scope.document_verified,
                        checklist2checkpoint: $scope.checklist2checkpoint,

                    }
                    var url = 'api/AtmTrnAuditorMaker/PostCheckpointAgainstObservation';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            //$scope.txttotal_score = resp.data.total_amount;

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }

                    });
                }

            }
        }
        $scope.auditraisequery = function (auditcreation_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/auditraisequery.html',
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
                var params = {
                    auditcreation_gid: auditcreation_gid
                }
                $scope.submit = function () {

                    var params = {
                        auditcreation_gid: auditcreation_gid,
                        query_title: $scope.txtquery_title,
                        query_description: $scope.txtquery_description,

                    }
                    var url = 'api/AtmTrnAuditorMaker/PostAuditRaiseQuery';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            //activate();
                            auditraise_list(auditcreation_gid);
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });
                    $modalInstance.close('closed');
                }

            }
        }


        function auditraise_list(auditcreation_gid) {
            var params = {
                auditcreation_gid: auditcreation_gid,

            }

            var url = 'api/AtmTrnAuditorMaker/AuditRaisedQuerySummary';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.auditquerydata = resp.data.auditquerydata;
            });
        }

        $scope.viewresponse_samplequery = function (auditraisequery_gid) {

            var lsreplied_by = "";
            if (lspage == "auditormakerOpen") {
                lsreplied_by = 'Auditor Maker';
            }
            else if (lspage == "auditorapproveropen") {
                lsreplied_by = 'Auditor Approver';
            }
            else if (lspage == "auditorcheckerOpen") {
                lsreplied_by = 'Auditor Checker';
            }
            else if (lspage == "auditeemakeropen") {
                lsreplied_by = 'Auditee Maker';
            }
            else if (lspage == "auditeecheckeropen") {
                lsreplied_by = 'Auditee Checker';
            }
            else if (lspage == "auditeemakertagged") {
                lsreplied_by = 'Tagged User';
            }
            var modalInstance = $modal.open({
                templateUrl: '/response_view.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance', 'DownloaddocumentService'];
            function ModalInstanceCtrl($scope, $modalInstance, DownloaddocumentService) {
                $scope.hide = 0;
                var params = {
                    auditcreation_gid: auditcreation_gid
                };
                //var url = 'api/AtmTrnAuditorMaker/GetSampleResponseQuery';
                //lockUI();
                //SocketService.getparams(url, params).then(function (resp) {
                //    $scope.approval_status = resp.data.approval_status;
                //    if ($scope.approval_status == 'Completed') {
                //        $scope.hide = 1;
                //    }

                //    unlockUI();
                //});
                var params = {
                    auditraisequery_gid: auditraisequery_gid,
                }
                var url = 'api/AtmTrnAuditorMaker/GetAuditQuerydetaillist';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.auditQuerydetaillist = resp.data.auditQuerydetaillist;
                    unlockUI();
                });

                $scope.replytoquery = function () {
                    var params = {
                        auditcreation_gid: auditcreation_gid,
                        remarks: $scope.txtqueries,
                        auditraisequery_gid: auditraisequery_gid,
                        replied_by: lsreplied_by
                    }
                    lockUI();
                    var url = "api/AtmTrnAuditorMaker/PostAuditQuerydetail";
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {


                            var param = {
                                auditraisequery_gid: auditraisequery_gid
                            };
                            var url = "api/AtmTrnAuditorMaker/GetAuditQuerydetaillist";
                            SocketService.getparams(url, param).then(function (resp) {
                                unlockUI();
                                $scope.auditQuerydetaillist = resp.data.auditQuerydetaillist;
                                if (resp.data.status == true) {
                                    Notify.alert(resp.data.message, {
                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                }
                                else {
                                    Notify.alert(resp.data.message, {
                                        status: 'danger',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                }
                                unlockUI();
                            });
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        $scope.txtqueries = "";
                    });
                }

                $scope.auditcreation_gid = auditcreation_gid;
                $scope.auditraisequery_gid = auditraisequery_gid;

                var params = {
                    auditcreation_gid: auditcreation_gid,
                    auditraisequery_gid: auditraisequery_gid,
                }

                $scope.uploaddocument = function (val, val1, name) {
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "Default");

                    if (IsValidExtension == false) {
                        Notify.alert("File format is not supported..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        return false;
                    }

                    var auditcreation_gid = $scope.auditcreation_gid;
                    var auditraisequery_gid = $scope.auditraisequery_gid;

                    var item = {
                        name: val[0].name,
                        file: val[0]
                    };

                    var frm = new FormData();
                    frm.append('fileupload', item.file);
                    frm.append('file_name', item.name);
                    frm.append('document_name', $scope.documentname);
                    frm.append('document_title', $scope.txtdocument_title);
                    frm.append('auditcreation_gid', $scope.auditcreation_gid);
                    frm.append('auditraisequery_gid', $scope.auditraisequery_gid);
                    frm.append('project_flag', "Default");


                    $scope.uploadfrm = frm;
                    var url = 'api/AtmTrnAuditorMaker/AuditResponseDocUpload';
                    lockUI();
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                        $("#addupload").val('');
                        $scope.txtdocument_title = '';
                        if (resp.data.status == true) {
                            unlockUI();
                            Notify.alert('Document Uploaded Successfully..!!', 'success')

                            var param = {
                                auditraisequery_gid: auditraisequery_gid
                            };

                            var url = "api/AtmTrnAuditorMaker/GetAuditQuerydetaillist"

                            SocketService.getparams(url, param).then(function (resp) {

                                $scope.auditQuerydetaillist = resp.data.auditQuerydetaillist;
                                if (resp.data.status == true) {
                                    Notify.alert(resp.data.message, {
                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                }
                                else {
                                    Notify.alert(resp.data.message, {
                                        status: 'danger',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                }

                                unlockUI();

                            });


                        }
                        else {
                            unlockUI();
                            Notify.alert('File Format Not Supported!')

                        }
                        //alert('Document Uploaded Successfully..!!', 'success')


                    });

                }


                $scope.downloadsdocument = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                    //var phyPath = val1;
                    //console.log(val1)
                    //var relPath = phyPath.split("EMS");
                    //var relpath1 = relPath[1].replace("\\", "/");
                    //var hosts = window.location.host;
                    //var prefix = location.protocol + "//";
                    //var str = prefix.concat(hosts, relpath1);
                    //var link = document.createElement("a");
                    //var name = val2.split(".")
                    //link.download = val2;
                    //var uri = str;
                    //link.href = uri;
                    //link.click();
                }


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

            }
        }

        $scope.closesample_query = function (auditraisequery_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/closeremarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.closesample_submit = function () {

                    var params = {
                        auditraisequery_gid: auditraisequery_gid,
                        close_remarks: $scope.txtclosequeries
                    }

                    var url = 'api/AtmTrnAuditorMaker/PostAuditCloseQuery';
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
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

            }
        }

        $scope.samplecheckerobservationscoreview = function (val, val1, val2, val3) {
            $location.url('app/AtmTrnAuditeeCheckerOpenObservationView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + val + '&checklistmaster_gid=' + val1 + '&sampleimport_gid=' + val2 + '&observationscoresample_gid=' + val3))
        }
        $scope.Back = function (val) {
            if (lspage == 'auditeemakeropen') {
                $location.url('app/AtmTrnMyAuditTaskAuditeeSummary');
            }
            if (lspage == 'auditeecheckeropensummary') {
                $location.url('app/AtmTrnMyAuditeeCheckerSummary');
            }
            else if (lspage == 'auditeemakerhold') {
                $location.url('app/AtmTrnHoldAuditeeSummary');
            }
            else if (lspage == 'auditeemakerclosed') {
                $location.url('app/AtmTrnClosedAuditeeSummary');
            }
            else if (lspage == 'auditeemakertagged') {
                $location.url('app/AtmTrnTaggedAuditeeSummary');
            }
            else if (lspage == 'auditeemakercompleted') {
                $location.url('app/AtmTrnCompletedAuditeeSummary');
            }
            else if (lspage == 'auditeecheckeropen') {
                $location.url('app/AtmTrnMyAuditeeCheckerSummary');
            }
            else if (lspage == 'auditeecheckerpending') {
                $location.url('app/AtmTrnCheckerPendingApproval');
            }
            else if (lspage == 'auditeecheckerhold') {
                $location.url('app/AtmTrnCheckerHoldAuditee');
            }
            else if (lspage == 'auditeecheckerclosed') {
                $location.url('app/AtmTrnCheckerClosedAuditee');
            }
            else if (lspage == 'auditeecheckertagged') {
                $location.url('app/AtmTrnCheckerTaggedAuditee');
            }
            else if (lspage == 'auditeecheckercompleted') {
                $location.url('app/AtmTrnCheckerCompletedAuditee');
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