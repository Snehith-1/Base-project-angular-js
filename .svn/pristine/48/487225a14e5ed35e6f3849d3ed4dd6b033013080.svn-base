(function () {
    'use strict';
    angular
        .module('angle')
        .controller('AtmTrnSampleAgainstCheckerObservationScoreController', AtmTrnSampleAgainstCheckerObservationScoreController);

    AtmTrnSampleAgainstCheckerObservationScoreController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','cmnfunctionService'];

    function AtmTrnSampleAgainstCheckerObservationScoreController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmTrnSampleAgainstCheckerObservationScoreController';
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.auditcreation_gid = searchObject.auditcreation_gid;
        var auditcreation_gid = $scope.auditcreation_gid;
        $scope.checklistmaster_gid = searchObject.checklistmaster_gid;
        var checklistmaster_gid = $scope.checklistmaster_gid;
        $scope.checklistmasteradd_gid = searchObject.checklistmasteradd_gid;
        $scope.sampleimport_gid = searchObject.sampleimport_gid;
        var sampleimport_gid = $scope.sampleimport_gid;
        $scope.observationscoresample_gid = searchObject.observationscoresample_gid;
        var observationscoresample_gid = $scope.observationscoresample_gid;
        $scope.lspage = searchObject.lspage;
        var lspage = $scope.lspage;
        $scope.data = {};

        activate();

        function activate() {

            var url = 'api/AtmTrnAuditCreation/SampleObservationScore';
            var params = {
                auditcreation_gid: auditcreation_gid,
                sampleimport_gid: sampleimport_gid,
                observationscoresample_gid: observationscoresample_gid

            }
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.openquerycount = resp.data.openquerycount,
                $scope.status_flag = resp.data.status_flag,
                $scope.DBobservation_score = resp.data.sampleobservation_score,
                $scope.txttotal_score = $scope.DBobservation_score,
                $scope.observationfill = resp.data.observation_fill,
                $scope.auditormaker_approvalflag = resp.data.auditormaker_approvalflag,
               $scope.samplechecklistverified_flag = resp.data.samplechecklistverified_flag,
                $scope.samplecapture_field = resp.data.samplecapture_field,
                //$scope.txt_overallscore = resp.data.overall_score;
                $scope.txt_percent = resp.data.sampleobservation_percentage
                if (resp.data.observation_fill == 'N') {
                    $scope.observationfillenable = false;
                    $scope.makersaveapprove = false;
                }
                else if (resp.data.observation_fill == 'Y') {
                    $scope.observationfillenable = true;
                    $scope.makersaveapprove = true;
                }
                else {
                    $scope.observationfillenable = "Disable";
                    $scope.makersaveapprove = false;
                }

                unlockUI();
            });

            var url = 'api/AtmTrnAuditorMaker/GetAuditorSampleViewOverallscore';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txt_overallscore = resp.data.overall_score;
                unlockUI();

            });

            var params = {
                auditcreation_gid: auditcreation_gid,
                sampleimport_gid: sampleimport_gid
            };
            var url = 'api/AtmTrnAuditorMaker/AuditorSampleView';
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
                        //value.yes_radio1 = true;
                    }
                    else if (value.capture_field == "No") {
                        value.no_radio = true;
                        //value.no_radio1 = true;
                    }
                    else if (value.capture_field == "Partial") {
                        value.partialscore_radio = true;
                        //value.partialscore_radio1 = true;
                    }
                    else if (value.capture_field == "NA") {
                        value.nascore_radio = true;
                        //value.nascore_radio1 = true;
                    }
                    else if (value.capture_field == "") {
                        $scope.allobservationfilled = false;
                    }
                });
            });
        }
        $scope.allobservationfilled = true;

        $scope.onselected = function (val, val1, val2, val3) {
            if ($scope.openquerycount != '0') {
                Notify.alert("Observation can't be filled, Query is not closed", {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
            }

            else if($scope.samplechecklistverified_flag == 'Y')
            {
                $scope.total_score = val;
                $scope.auditcreation2checklist_gid = val1;
                $scope.checklistmasteradd_gid = val2;
                $scope.observationscoresample_gid = val3;
                var params = {
                    auditcreation_gid: auditcreation_gid,
                    sampleimport_gid: sampleimport_gid,
                    auditcreation2checklist_gid: $scope.auditcreation2checklist_gid,
                    checklistmasteradd_gid: $scope.checklistmasteradd_gid,
                    capture_score: $scope.total_score,
                    observationscoresample_gid: $scope.observationscoresample_gid,

                }
                var url = 'api/AtmTrnAuditorMaker/PostAuditorUpdateSampleObservationTotalAmount';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        //$scope.txttotal_samplescore = resp.data.total_amount;
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
            
            else {
                $scope.total_score = val;
                $scope.auditcreation2checklist_gid = val1;
                $scope.checklistmasteradd_gid = val2;
                $scope.observationscoresample_gid = val3;


                var params = {
                    auditcreation_gid: auditcreation_gid,
                    sampleimport_gid: sampleimport_gid,
                    auditcreation2checklist_gid: $scope.auditcreation2checklist_gid,
                    checklistmasteradd_gid: $scope.checklistmasteradd_gid,
                    capture_score: $scope.total_score,
                    observationscoresample_gid: $scope.observationscoresample_gid,

                }
                var url = 'api/AtmTrnAuditorMaker/PostAuditorSampleObservationTotalAmount';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        //$scope.txttotal_samplescore = resp.data.total_amount;
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

        
        function checklist() {
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

            if ($scope.samplecapture_field != '0') {
                Notify.alert("Sample Observation score can't be filled", {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
            }
            else {
                var assignList = [];
                angular.forEach($scope.makercheckpointobservation_list, function (val) {

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
                    checklistmaster_gid: checklistmaster_gid,
                    sampleimport_gid: sampleimport_gid,
                    auditcreation_gid: auditcreation_gid

                }

                var url = 'api/AtmTrnAuditCreation/PostSampleAssign';
                SocketService.post(url, params).then(function (resp) {
                    lockUI();
                    if (resp.data.status == true) {
                        unlockUI();
                        Notify.alert('Sample Observation score Saved Successfully!', 'success');
                        $location.url('app/AtmTrnAuditorCheckerView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&lspage=' + 'auditorcheckerPending'))
                    }
                    else {
                        Notify.alert('Select Atleast One..!!', 'warning')
                    }

                });
            }
        }

        $scope.makersampleupdateapproval = function () {
            //var assignList = [];
            //angular.forEach($scope.makercheckpointobservation_list, function (val) {

            //    if (val.checked == true) {
            //        var checklistmasteradd_gid = val.checklistmasteradd_gid;
            //        assignList.push(checklistmasteradd_gid);
            //        var checklistmaster_gid = val.checklistmaster_gid;

            //    }
            //});
            //if (assignList.length == 0) {
            //    Notify.alert('Select Atleast One Record!');
            //    return false;
            //    unlockUI();
            //}
            var params = {
                //checklistmasteradd_gid: assignList,
                //checklistmaster_gid: checklistmaster_gid,
                sampleimport_gid: sampleimport_gid,
                auditcreation_gid: auditcreation_gid

            }

            var url = 'api/AtmTrnAuditCreation/PostSampleAssignUpdate';
            SocketService.post(url, params).then(function (resp) {
                lockUI();
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert('Sample Observation score Saved Successfully!', 'success');
                    $location.url('app/AtmTrnAuditorCheckerView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&lspage=' + 'auditorcheckerPending'))
                }
                else {
                    Notify.alert('Select Atleast One..!!', 'warning')
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
        $scope.observationquery = function (checkpointgroupadd_gid, auditcreation2checklist_gid, sampleimport_gid, auditcreation_gid) {
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
                           Notify. alert(resp.data.message, {
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
                $scope.overallselected = function (val) {

                    $scope.document_verified = val;
                    var params = {
                        auditcreation2checklist_gid: auditcreation2checklist_gid,
                        document_verified: $scope.document_verified,
                        checklist2checkpoint: $scope.checklist2checkpoint,
                        sampleimport_gid: sampleimport_gid,
                        checkpointgroupadd_gid: checkpointgroupadd_gid,


                    }
                    var url = 'api/AtmTrnAuditorMaker/PostOverallCheckpointAgainstSample';
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
                            checklist();

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
                function checklist() {
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
                }
                $scope.overallonselected = function (val) {

                    $scope.document_verified = val;
                    var params = {
                        auditcreation2checklist_gid: auditcreation2checklist_gid,
                        document_verified: $scope.document_verified,
                        checklist2checkpoint: $scope.checklist2checkpoint,
                        sampleimport_gid: sampleimport_gid,
                        checkpointgroupadd_gid: checkpointgroupadd_gid,


                    }
                    var url = 'api/AtmTrnAuditorMaker/PostOverallCheckpointAgainstSample';
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
                            checklist();

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
                $scope.overallpartialselected = function (val) {

                    $scope.document_verified = val;
                    var params = {
                        auditcreation2checklist_gid: auditcreation2checklist_gid,
                        document_verified: $scope.document_verified,
                        checklist2checkpoint: $scope.checklist2checkpoint,
                        sampleimport_gid: sampleimport_gid,
                        checkpointgroupadd_gid: checkpointgroupadd_gid,


                    }
                    var url = 'api/AtmTrnAuditorMaker/PostOverallCheckpointAgainstSample';
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
                            checklist();

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
                $scope.overallnaselected = function (val) {

                    $scope.document_verified = val;
                    var params = {
                        auditcreation2checklist_gid: auditcreation2checklist_gid,
                        document_verified: $scope.document_verified,
                        checklist2checkpoint: $scope.checklist2checkpoint,
                        sampleimport_gid: sampleimport_gid,
                        checkpointgroupadd_gid: checkpointgroupadd_gid,


                    }
                    var url = 'api/AtmTrnAuditorMaker/PostOverallCheckpointAgainstSample';
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
                            checklist();

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

                //$scope.checkall = function (selected) {

                //    angular.forEach($scope.checklistcheckpoint_list, function (val) {
                //        val.checked = selected;
                //    });
                //}
                $scope.samplecheckpointassign = function () {
                    //var assignList = [];
                    //angular.forEach($scope.checklistcheckpoint_list, function (val) {

                    //    if (val.checked == true) {
                    //        var checkpointgroupadd_gid = val.checkpointgroupadd_gid;
                    //        assignList.push(checkpointgroupadd_gid);
                    //    }
                    //});
                    //if (assignList.length == 0) {
                    //    Notify.alert('Select Atleast One Record!');
                    //    return false;
                    //    unlockUI();
                    //}
                    var params = {
                        checkpointgroupadd_gid: checkpointgroupadd_gid,
                        sampleimport_gid: sampleimport_gid,
                        auditcreation_gid: auditcreation_gid,
                    }

                    var url = 'api/AtmTrnAuditorMaker/PostSampleCheckpointAssign';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        lockUI();
                        if (resp.data.status == true) {
                            unlockUI();
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
                            unlockUI();
                        }
                        $modalInstance.close('closed');
                    });

                }
                $scope.samplecheckpointassignupdate = function () {
                    //var assignList = [];
                    //angular.forEach($scope.checklistcheckpoint_list, function (val) {

                    //    if (val.checked == true) {
                    //        var checkpointgroupadd_gid = val.checkpointgroupadd_gid;
                    //        assignList.push(checkpointgroupadd_gid);
                    //    }
                    //});
                    //if (assignList.length == 0) {
                    //    Notify.alert('Select Atleast One Record!');
                    //    return false;
                    //    unlockUI();
                    //}
                    var params = {
                        checkpointgroupadd_gid: checkpointgroupadd_gid,
                        sampleimport_gid: sampleimport_gid,
                        auditcreation_gid: auditcreation_gid,
                    }

                    var url = 'api/AtmTrnAuditorMaker/PostSampleCheckpointAssignUpdate';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        lockUI();
                        if (resp.data.status == true) {
                            unlockUI();
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
                            unlockUI();
                        }
                        $modalInstance.close('closed');
                    });

                }
            }
        }
        $scope.dynamicsamplescore = function (val, val1, val2, val3) {
            $location.url('app/AtmTrnSampleAgainstObservationScore?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + val + '&checklistmaster_gid=' + val1 + '&sampleimport_gid=' + val2 + '&observationscoresample_gid=' + val3))

        }
        $scope.back = function (auditorcheckerPending) {
            $location.url('app/AtmTrnAuditorCheckerView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&lspage=' + 'auditorcheckerPending'))

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
