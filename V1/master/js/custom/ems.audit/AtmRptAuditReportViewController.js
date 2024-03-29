﻿(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmRptAuditReportViewController', AtmRptAuditReportViewController);

    AtmRptAuditReportViewController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'cmnfunctionService'];

    function AtmRptAuditReportViewController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmRptAuditReportViewController';
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.auditcreation_gid = searchObject.auditcreation_gid;
        var auditcreation_gid = $scope.auditcreation_gid;
        
        activate();
        function activate() {

            var params = {
                auditcreation_gid: auditcreation_gid
            }
           
            var url = 'api/AtmRptAuditReports/EditAuditReports';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {

                $scope.txtentity_name = resp.data.entity_name,
                $scope.txtauditdepartment_name = resp.data.auditdepartment_name,
                $scope.txtaudit_name = resp.data.audit_name,
                $scope.txtpriority_name = resp.data.auditpriority_name,
                $scope.txtaudit_type = resp.data.audittype_name,
                $scope.txtcheckpoint_group = resp.data.checkpointgroup_name,
                $scope.txtaudit_description = resp.data.audit_description,
                $scope.txtend_date = resp.data.due_date,
                $scope.txtfrom_date = resp.data.auditperiod_fromdate,
                $scope.txtto_date = resp.data.auditperiod_todate,
                $scope.txtauditee_maker = resp.data.auditeemaker_name,
                $scope.txtauditee_checker = resp.data.auditeechecker_name,
                $scope.txtauditor_maker = resp.data.auditmaker_name,
                $scope.txtauditor_checker = resp.data.auditchecker_name,
                $scope.txtauditor_approver = resp.data.auditapprover_name,
                $scope.txtauditcreated_date = resp.data.created_date,
                $scope.txtauditcreated_by = resp.data.created_by,
                $scope.txtapproved_date = resp.data.auditapproved_date,
                $scope.txtapproved_by = resp.data.auditapproved_by,
                $scope.txttag_user = resp.data.tagged_user,
                $scope.txtmaker_initiate = resp.data.AuditorMakerInitiatedDate,
                $scope.txtauditee_initiate = resp.data.AuditeeCheckerInitiatedDate,
                $scope.txtauditorapproved_date = resp.data.AuditorApprovedDate,
                $scope.txtchecker_initiate = resp.data.Auditorcheckerinitiated_date
                unlockUI();
            });
            var url = 'api/AtmTrnAuditCreation/GetAuditeeSummaryList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.multipleauditee_list = resp.data.multipleauditee_list;
                unlockUI();

            });
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
                    $scope.created_by = resp.data.created_by;
                $scope.created_date = resp.data.created_date;
                $scope.txtentity_name = resp.data.entity_name,
                    $scope.txtaudit_type = resp.data.audittype_name,
                    $scope.txtcheckpoint_group = resp.data.checkpointgroup_name,
                    $scope.txtaudit_desc = resp.data.audit_description,
                    $scope.openquerycount = resp.data.openquerycount,
                    $scope.status_update = resp.data.status_update,
                    $scope.DBobservation_score = resp.data.observation_score,
                    $scope.txttotal_score = $scope.DBobservation_score,
                    $scope.observationfill = resp.data.observation_fill;
                $scope.auditormaker_approvalflag = resp.data.auditormaker_approvalflag;
                $scope.txt_percent = resp.data.observation_percentage;
                $scope.auditobservation_name = resp.data.auditobservation_name;
                $scope.samplestatus_flag = resp.data.samplestatus_flag;
                $scope.checklistverified_flag = resp.data.checklistverified_flag;

                //$scope.tagsamplebutton = true;
                //if (resp.data.observation_fill == 'N') {
                //    $scope.observationfillenable = false;
                //    $scope.makersaveapprove = false;

                //}
                //else if (resp.data.observation_fill == 'Y' && lspage == "auditormakerOpen" && $scope.auditormaker_approvalflag == 'N') {
                //    $scope.observationfillenable = true;
                //    $scope.makersaveapprove = true;
                //}
                //else {
                //    $scope.observationfillenable = "Disable";
                //    $scope.makersaveapprove = false;
                //}

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

            var url = 'api/AtmTrnAuditCreation/GetAudit360View';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.auditcreation_list = resp.data.auditcreation_list;
                unlockUI();

            });


            //var url = 'api/AtmTrnAuditorMaker/AuditRaisedQuerySummary';
            //lockUI();
            //SocketService.getparams(url, params).then(function (resp) {
            //    unlockUI();
            //    $scope.auditquerydata = resp.data.auditquerydata;
            //    $scope.close_disabled = false;
            //    if ((lspage == 'auditeemakeropen' || lspage == 'auditeemakerhold' || lspage == 'auditeemakerclosed' || lspage == 'auditeemakertagged' || lspage == 'auditeemakercompleted' ||
            //        lspage == 'auditeecheckeropen' || lspage == 'auditeecheckerpending' || lspage == 'auditeecheckerhold' || lspage == 'auditeecheckerclosed' || lspage == 'auditeecheckertagged' ||
            //        lspage == 'auditeecheckercompleted')) {
            //        $scope.close_disabled = true;
            //    }
            //});
            defaultdynamic();



            var params = {
                auditcreation_gid: auditcreation_gid,
            }
            var url = 'api/AtmTrnAuditCreation/TrnCheckpointCreation';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.checkpointsummary_list = resp.data.auditcheckpointsummary_list;

            });
            $scope.allobservationfilled = true;
            var params = {
                auditcreation_gid: auditcreation_gid
            };

            var url = 'api/AtmTrnAuditorMaker/AuditorMakerView';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.makercheckpointobservation_list = resp.data.makercheckpointobservationview_list;
                for (var i = 0; i < $scope.makercheckpointobservation_list.length; i++) {
                    $scope.makercheckpointobservation_list[i].checked = true;

                }

                angular.forEach($scope.makercheckpointobservation_list, function (value, key) {


                    if (value.capture_field == "Yes") {
                        value.yes_radio = true;
                        value.yes_radio1 = true;
                    }
                    else if (value.capture_field == "No") {
                        value.no_radio = true;
                        value.no_radio1 = true;
                    }
                    else if (value.capture_field == "Partial") {
                        value.partialscore_radio = true;
                        value.partialscore_radio1 = true;
                    }
                    else if (value.capture_field == "NA") {
                        value.nascore_radio = true;
                        value.nascore_radio1 = true;
                    }
                    else if (value.capture_field == "") {
                        $scope.allobservationfilled = false;

                    }

                });
            });
            /*    var url = 'api/AtmTrnAuditorMaker/MakerObservationSampleView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.makerobservationsampleview_list = resp.data.makerobservationsampleview_list;
                    for (var i = 0; i < $scope.makerobservationsampleview_list.length; i++) {
                        $scope.makerobservationsampleview_list[i].checked = true;
    
                    }
    
                    angular.forEach($scope.makerobservationsampleview_list, function (value, key) {
    
    
                        if (value.capture_field == "Yes") {
                            value.yes_radio = true;
                            value.yes_radio1 = true;
                        }
                        else if (value.capture_field == "No") {
                            value.no_radio = true;
                            value.no_radio1 = true;
                        }
                        else if (value.capture_field == "Partial") {
                            value.partialscore_radio = true;
                            value.partialscore_radio1 = true;
                        }
                        else if (value.capture_field == "NA") {
                            value.nascore_radio = true;
                            value.nascore_radio1 = true;
                        }
                        else if (value.capture_field == "") {
                            $scope.allobservationfilled = false;
    
                        }
    
                    });
                }); */
            function expand(expand_flag) {
                if (expand_flag = true) {
                    alert('true');
                }
                else {
                    alert('fasle');
                }
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
                        $scope.expandCount = 3;
                        $scope.SampleDynamicTabledata = $scope.SampleDynamicTabledata.filter(function (el) { return el.raisedquery_flag === 'N' });
                        $scope.SampleDynamicTable = angular.copy($scope.SampleDynamicTabledata);
                        $scope.raisedqueryarray = $scope.SampleDynamicRaisedTabledata.filter(function (el) { return el.raisedquery_flag === 'Y' });
                        $scope.SampleraisedqueryTable = angular.copy($scope.raisedqueryarray);
                        for (i in $scope.SampleDynamicTabledata) {

                            //delete $scope.SampleDynamicTabledata[i].makerobservationsampleview_list;
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
                    //else {
                    //    $scope.SampleDynamicTabledata = "";
                    //    $scope.raisedqueryarray = "";
                    //    $scope.showtablediv = false;
                    //    $scope.raisedquerytable = false;
                    //}
                    //angular.forEach($scope.SampleDynamicTabledata, function (value, key) {


                    //    var params = {
                    //        sampleimport_gid: value.sampleimport_gid,
                    //    };
                    //    var url = 'api/AtmTrnAuditorMaker/MakerObservationSampleView';
                    //    lockUI();
                    //    SocketService.getparams(url, params).then(function (resp) {
                    //        unlockUI();
                    //        value.makerobservationsampleview_list = resp.data.makerobservationsampleview_list;

                    //        for (var i = 0; i < value.makerobservationview_list.length; i++) {
                    //            value.makerobservationview_list[i].checked = true;

                    //        }

                    //        angular.forEach(value.makerobservationview_list, function (value, key) {


                    //            if (value.capture_field == "Yes") {
                    //                value.yes_radio = true;
                    //                value.yes_radio1 = true;
                    //            }
                    //            else if (value.capture_field == "No") {
                    //                value.no_radio = true;
                    //                value.no_radio1 = true;
                    //            }
                    //            else if (value.capture_field == "Partial") {
                    //                value.partialscore_radio = true;
                    //                value.partialscore_radio1 = true;
                    //            }
                    //            else if (value.capture_field == "NA") {
                    //                value.nascore_radio = true;
                    //                value.nascore_radio1 = true;
                    //            }
                    //            else if (value.capture_field == "") {
                    //                $scope.allobservationfilled = false;

                    //            }

                    //        });
                    //    });
                    //    value.expand = false;

                    //});
                });
            }
        }


        $scope.back = function () {
            $state.go('app.AtmMstAuditReportSummary');
        }
        $scope.onselected = function (val, val1, val2) {
            if ($scope.openquerycount != '0') {
                Notify.alert("Observation can't be filled, Query is not closed", {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
            }
            else {
                $scope.total_score = val;
                $scope.auditcreation2checklist_gid = val1;
                $scope.checklistmasteradd_gid = val2;


                var params = {
                    auditcreation_gid: $scope.auditcreation_gid,
                    auditcreation2checklist_gid: $scope.auditcreation2checklist_gid,
                    checklistmasteradd_gid: $scope.checklistmasteradd_gid,
                    capture_score: $scope.total_score,

                }
                var url = 'api/AtmTrnAuditorMaker/PostAuditorMakerObservationTotalAmount';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        $scope.txttotal_score = resp.data.total_amount;
                        if (resp.data.allobservationfilled == true) {
                            $scope.allobservationfilled = true;
                            $scope.makersaveapprove = true;
                        }
                        else {
                            $scope.allobservationfilled = false;
                        }
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



        $scope.submitobservation = function () {
            if ($scope.openquerycount != '0') {
                Notify.alert("Observation can't be filled, Query is not closed", {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
            }
            else {
                lockUI();
                var assignList = [];
                angular.forEach($scope.makercheckpointobservation_list, function (val) {

                    if (val.checked == true) {
                        var auditcreation2checklist_gid = val.auditcreation2checklist_gid;
                        assignList.push(auditcreation2checklist_gid);

                    }
                });

                var params = {
                    auditcreation2checklist_gid: assignList,
                    auditcreation_gid: auditcreation_gid,
                }

                var url = 'api/AtmTrnAuditorMaker/PostAuditorMakerCheckpointObservation';
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });

                        var params = {
                            auditcreation_gid: auditcreation_gid
                        };

                        var url = 'api/AtmTrnAuditorMaker/AuditorMakerView';
                        SocketService.getparams(url, params).then(function (resp) {

                            $scope.makercheckpointobservation_list = resp.data.makercheckpointobservationview_list;
                            $scope.txttotal_score = resp.data.total_score;

                            for (var i = 0; i < $scope.makercheckpointobservation_list.length; i++) {
                                $scope.makercheckpointobservation_list[i].checked = true;

                            }
                            $scope.allobservationfilled = true;
                            angular.forEach($scope.makercheckpointobservation_list, function (value, key) {

                                if (value.capture_score == value.yes_score) {
                                    value.yes_radio = true;
                                    value.yes_radio1 = true;
                                }
                                else if (value.capture_score == value.no_score) {
                                    value.no_radio = true;
                                    value.no_radio1 = true;
                                }
                                else if (value.capture_score == value.partial_score) {
                                    value.partialscore_radio = true;
                                    value.partialscore_radio1 = true;
                                }
                                else if (value.capture_score == value.na_score) {
                                    value.nascore_radio = true;
                                    value.nascore_radio1 = true;
                                }
                                else if (value.capture_score == "") {
                                    $scope.allobservationfilled = false;

                                }

                            });
                            unlockUI();
                        });
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                    }
                    $scope.txttotal_score = "";
                });
            }


        }

        $scope.fillobservation = function () {
            if ($scope.openquerycount != '0') {
                Notify.alert("Observation can't be filled, Query is not closed", {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
            }
            else {
                var modalInstance = $modal.open({
                    templateUrl: '/warningObservation.html',
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

                    $scope.btnproceed = function () {
                        var params = {
                            auditcreation_gid: auditcreation_gid
                        };
                        var url = 'api/AtmTrnAuditCreation/observationfill';
                        SocketService.getparams(url, params).then(function (resp) {
                            if (resp.data.status == true) {
                                showobservationoption();
                            }
                        });
                        $modalInstance.close('closed');
                    }
                }
            }
        }

        function showobservationoption() {
            $scope.observationfillenable = true;
        }

        $scope.makerinitiateapproval = function () {
            if ($scope.openquerycount != '0' && $scope.DBobservation_score != "") {
                Notify.alert("Query is not closed", {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
            }

            else if ($scope.auditobservation_name == 'Observation score for Audit & sample' && $scope.checklistverified_flag == 'N') {
                Notify.alert("Atleast one sample observation score saved", {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
            }

            else {

                var txt_percent = $scope.txt_percent;

                var params = {
                    auditcreation_gid: $scope.auditcreation_gid,
                    makerinitiate_approvalflag: 'Y',
                    observation_percentage: $scope.txt_percent

                }
                var url = 'api/AtmTrnAuditorMaker/PostMakerInitiateApproval';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        $state.go('app.AtmTrnAuditorMakerSummary');
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                    }

                });
            }

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
                    var Templateurl = apiManage.GetCommonData['TemplatePath'].Path;
                    var filename = "\ImportExcelSample.xlsx";
                    //var phyPath = resp.data.file_path;
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
                            $scope.tagsamplebutton = true;
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
                activate();

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
        //$scope.description = function (checklistmasteradd_gid, checkpoint_description) {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/checkpointdescription.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'md'
        //    });
        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {
        //        'use strict';

        //        angular
        //            .module('angle')
        //        var params = {
        //            checklistmasteradd_gid: checklistmasteradd_gid
        //        }
        //        lockUI();
        //        var url = 'api/AtmMstChecklistMaster/GetChecklistMasterDescription';
        //        SocketService.getparams(url, params).then(function (resp) {
        //            unlockUI();
        //            $scope.txtcheckpointdescription = resp.data.checkpoint_description;

        //        });
        //        $scope.ok = function () {
        //            $modalInstance.close('closed');
        //        };
        //    }
        //}
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

        $scope.observationremarks = function (auditcreation2checklist_gid, auditcreation_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/observationremarksupdate.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    auditcreation2checklist_gid: auditcreation2checklist_gid,
                    auditcreation_gid: auditcreation_gid

                }

                $scope.remark_submit = function () {

                    var params = {
                        auditcreation2checklist_gid: auditcreation2checklist_gid,
                        auditcreation_gid: auditcreation_gid,
                        observation_remarks: $scope.txtremarks
                    }

                    var url = 'api/AtmTrnAuditorMaker/AuditObservatioRemarks';
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

                var param = {
                    auditcreation2checklist_gid: auditcreation2checklist_gid
                }

                var url = 'api/AtmTrnAuditorMaker/AuditObservatioRemarksview';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.observationremarkslog_data = resp.data.myauditormaker_list;
                    unlockUI();
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };



            }
        }

        $scope.observationquery = function () {

            $state.go('app.AtmTrnAuditObservationQuery')
        }

        $scope.samplequery = function (index) {
            // var auditcreation_gid = $scope.auditcreation_gid; 
            var checklistmaster_gid = $scope.checklistmaster_gid;
            var sampleimport_gid = "";
            var getsampleimport_gid = $scope.SampleraisedqueryTable[index];
            if (getsampleimport_gid && getsampleimport_gid.sampleimport_gid) {
                sampleimport_gid = getsampleimport_gid.sampleimport_gid;
            }
            $location.url('app/AtmTrnSampleQueryAuditor?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&checklistmaster_gid=' + checklistmaster_gid + '&sampleimport_gid=' + sampleimport_gid + '&lspage=' + lspage + '&lsobservationfill_flag=' + $scope.observationfill))
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
                            $scope.txttotal_score = resp.data.total_amount;

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
        $scope.sampleobservationquery = function (checkpointgroupadd_gid, auditcreation2checklist_gid, sampleimport_gid, auditcreation_gid) {
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
                    sampleimport_gid: sampleimport_gid
                }
                //var url = 'api/AtmMstCheckpointGroup/GetChecklistToCheckpointcreate';
                //lockUI();
                //SocketService.getparams(url,params).then(function (resp) {
                //    $scope.checklistcheckpoint_list = resp.data.checklistcheckpoint_list;
                //    unlockUI();
                //});
                var url = 'api/AtmTrnAuditorMaker/GetSampleToCheckpoint';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.checklistcheckpoint_list = resp.data.checklistcheckpoint_list;
                    for (var i = 0; i < $scope.checklistcheckpoint_list.length; i++) {
                        $scope.checklistcheckpoint_list[i].checked = true;

                    }

                    angular.forEach($scope.checklistcheckpoint_list, function (value, key) {


                        if (value.checklist_verified == "Yes") {
                            value.document_verifiedradio = true;
                            value.document_verifiedradio1 = true;
                        }
                        else if (value.checklist_verified == "No") {
                            value.tag_verifiedradio = true;
                            value.tag_verifiedradio1 = true;
                        }
                        else if (value.checklist_verified == "Partial") {
                            value.partial_verifiedradio = true;
                        }
                        else if (value.checklist_verified == "NA") {
                            value.na_verifiedradio = true;
                        }


                    });
                });

                var url = 'api/AtmTrnAuditorMaker/GetAuditorSampleFlag';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.sample_flag = resp.data.sample_flag;
                    unlockUI();

                });


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.onselected = function (val, val1) {

                    $scope.document_verified = val;
                    $scope.sample2checkpoint = val1;


                    var params = {
                        auditcreation2checklist_gid: auditcreation2checklist_gid,
                        document_verified: $scope.document_verified,
                        sample2checkpoint: $scope.sample2checkpoint,
                        auditcreation_gid: auditcreation_gid,


                    }
                    var url = 'api/AtmTrnAuditorMaker/PostSampleCheckpointAgainstObservation';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            $scope.txttotal_score = resp.data.total_amount;

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

                $scope.checkall = function (selected) {

                    angular.forEach($scope.checklistcheckpoint_list, function (val) {
                        val.checked = selected;
                    });
                }
                $scope.samplecheckpointassign = function () {
                    var assignList = [];
                    angular.forEach($scope.checklistcheckpoint_list, function (val) {

                        if (val.checked == true) {
                            var checkpointgroupadd_gid = val.checkpointgroupadd_gid;
                            assignList.push(checkpointgroupadd_gid);
                            //var checklistmaster_gid = val.checklistmaster_gid;

                        }
                    });
                    if (assignList.length == 0) {
                        Notify.alert('Select Atleast One Record!');
                        return false;
                        unlockUI();
                    }
                    var params = {
                        checkpointgroupadd_gid: assignList,
                        sampleimport_gid: sampleimport_gid,
                        auditcreation_gid: auditcreation_gid,
                    }

                    var url = 'api/AtmTrnAuditorMaker/PostSampleCheckpointAssign';
                    SocketService.post(url, params).then(function (resp) {
                        lockUI();
                        if (resp.data.status == true) {
                            unlockUI();
                            Notify.alert('Sample Checkpoint Saved Successfully!', 'success');
                        }
                        else {
                            Notify.alert('Select Atleast One..!!', 'warning')
                        }
                        $modalInstance.close('closed');
                    });

                }
                $scope.samplecheckpointassignupdate = function () {
                    var assignList = [];
                    angular.forEach($scope.checklistcheckpoint_list, function (val) {

                        if (val.checked == true) {
                            var checkpointgroupadd_gid = val.checkpointgroupadd_gid;
                            assignList.push(checkpointgroupadd_gid);
                            //var checklistmaster_gid = val.checklistmaster_gid;

                        }
                    });
                    if (assignList.length == 0) {
                        Notify.alert('Select Atleast One Record!');
                        return false;
                        unlockUI();
                    }
                    var params = {
                        checkpointgroupadd_gid: assignList,
                        sampleimport_gid: sampleimport_gid,
                        auditcreation_gid: auditcreation_gid,
                    }

                    var url = 'api/AtmTrnAuditorMaker/PostSampleCheckpointAssignUpdate';
                    SocketService.post(url, params).then(function (resp) {
                        lockUI();
                        if (resp.data.status == true) {
                            unlockUI();
                            Notify.alert('Sample Checkpoint Saved Successfully!', 'success');
                        }
                        else {
                            Notify.alert('Select Atleast One..!!', 'warning')
                        }
                        $modalInstance.close('closed');
                    });

                }
            }
        }
        $scope.dynamicsamplescore = function (val, val1, val2, val3) {
            $location.url('app/AtmRptAuditReportSampleScoreView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + val + '&checklistmaster_gid=' + val1 + '&sampleimport_gid=' + val2 + '&observationscoresample_gid=' + val3))

        }
        $scope.dynamicsamplequeryscore = function (val, val1, val2, val3) {
            $location.url('app/AtmRptAuditReportSampleScoreView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + val + '&checklistmaster_gid=' + val1 + '&sampleimport_gid=' + val2 + '&observationscoresample_gid=' + val3))
        }
        $scope.samplecheckerobservationscoreview = function (val, val1, val2, val3) {
            $location.url('app/AtmRptAuditReportSampleScoreView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + val + '&checklistmaster_gid=' + val1 + '&sampleimport_gid=' + val2 + '&observationscoresample_gid=' + val3))
        }
        $scope.back = function (val) {
            $location.url('app/AtmMstAuditReportSummary')

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
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

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