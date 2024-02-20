(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rmObservationReportView', rmObservationReportView);

    rmObservationReportView.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams','DownloaddocumentService','cmnfunctionService'];

    function rmObservationReportView($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'rmObservationReportView';
        var allocationdtl_gid=$location.search().allocationdtl_gid;
        var observation_reportgid=$location.search().observation_reportgid;

       
        activate();

        function activate() {
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];

            // Calender Popup... //

            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };

            var params = {
                observation_reportgid: observation_reportgid
            }
            var url = "api/ObservationReport/GetViewObservationdtl";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customer_name = resp.data.customer_name;
                $scope.customer_urn = resp.data.customer_urn;
                $scope.dateof_RMDvisit = resp.data.dateof_RMDvisit;
                $scope.report_pertainingto = resp.data.report_pertainingto;
                $scope.vertical = resp.data.vertical;
                $scope.disbursement_amount = resp.data.disbursement_amount;
                $scope.approving_authority = resp.data.approving_authority;
                $scope.loansanction_date = resp.data.loansanction_date;
                $scope.relationship_manager_name = resp.data.relationship_manager_name;
                $scope.PPA_name = resp.data.PPA_name;
                $scope.RMDvisit_officialname = resp.data.RMDvisit_officialname;
                $scope.loandisbursement_date = resp.data.loandisbursement_date;
                $scope.people_accompaniedRMD = resp.data.people_accompaniedRMD;
                $scope.sanction_amount = resp.data.sanction_amount;
                $scope.outstanding_amount = resp.data.outstanding_amount;
                $scope.current_DPD = resp.data.current_DPD;
                $scope.contact_details1 = resp.data.contact_details1;
                $scope.contact_details2 = resp.data.contact_details2;
                $scope.observation_flag = resp.data.observation_flag;
                $scope.txtATR_date = resp.data.atr_completiondate;
                $scope.riskcode = resp.data.risk_code;
                $scope.cboriskcode = resp.data.risk_code;
                $scope.observationriskcode = resp.data.risk_code;
              
            });

            var url = "api/ObservationReport/tmpTier1documentclear"
            SocketService.get(url).then(function (resp) {

            });

            var url = "api/ObservationReport/GetViewObservationCriticalDtl";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.criticalobservation = resp.data.criticalobservation;
            });

            var url = "api/ObservationReport/GetTier1FormatDtl";
            SocketService.getparams(url, params).then(function (resp) {

                $scope.txtobservations = resp.data.tier1_observations;
                if (resp.data.tier1_code == null || resp.data.tier1_code == "") {
                }
                else {
                    $scope.cboriskcode = resp.data.tier1_code;
                }
                $scope.txtrationale_justification = resp.data.tier1_justification;
                $scope.txtprocess_gap = resp.data.tier1_processgap;
                $scope.txtcode_changereason = resp.data.tier1code_changereason;
                $scope.tier1code_changeflag = resp.data.tier1code_changeflag;
                $scope.txtimprovement_recommendation = resp.data.tier1_processrecommendation;
                $scope.txtmanagement_comments = resp.data.tier1_managementcomments;
                $scope.txtcheifheadreverts_actionplan = resp.data.tier1_reverts_actionplan;
                //$scope.txtATR_date = resp.data.tier1_atrdate;
                $scope.tier1format_gid = resp.data.tier1format_gid;
                $scope.tier1_approvalstatus = resp.data.tier1_approvalstatus;
                $scope.tier1approvallog = resp.data.tier1approvallog;
                $scope.uploaddocument_list = resp.data.tier1doc;
                $scope.tier1rejectlog = resp.data.tier1rejectlog;
            
                if ($scope.tier1code_changeflag == 'Y') {
                    $scope.disablecodechangereasonshow = true;
                }
                else {
                    $scope.disablecodechangereasonshow = false;
                }

                if (resp.data.tier1approvallog == null) {
                    $scope.nohistoryapproval = true;
                }
                else {
                    $scope.historyapproval = true;
                }
                if (resp.data.tier1rejectlog == null) {
                    $scope.rejecthistory = false;
                }
                else {
                    $scope.rejecthistory = true;
                }

                if (($scope.tier1format_gid != null) && ($scope.tier1_approvalstatus == 'Approved')) {
                    $scope.observation_flag = "T";
                    $scope.doc = true;
                }
                else if (($scope.tier1format_gid != null) && ($scope.tier1_approvalstatus == 'Rejected')) {
                    $scope.observation_flag = "Y";
                    $scope.btnupdated = true;
                    $scope.doc = false;
                }
                else if (($scope.tier1format_gid != null) && ($scope.tier1_approvalstatus == 'Pending')) {
                    $scope.observation_flag = "T";
                    $scope.doc = true;
                }
                else {

                }

                if ($scope.observation_flag == 'Y' && $scope.tier1_approvalstatus == 'Rejected') {
                    $scope.btnupdate = true;
                }
                else if ($scope.observation_flag == 'Y') {
                    $scope.btnsubmit = true;

                }
                else {

                }

            });
        }
        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.riskcodechange = function (cboriskcode) {
            if ($scope.riskcode == cboriskcode) {
                $scope.codechangereasonshow = false;

            }
            else {
                $scope.codechangereasonshow = true;

            }
        }

        $scope.tier1formatsubmit = function () {
            var lscode_changereason = $scope.txtcode_changereason;

            if ($scope.riskcode == $scope.cboriskcode) {
                //var lscode_changereason = '-';
                var date = $scope.txtATR_date;
                var ATR_date = date.split("-").reverse().join("-");

                var params = {
                    observation_reportgid: observation_reportgid,
                    allocationdtl_gid: allocationdtl_gid,
                    customer_name: $scope.customer_name,
                    customer_urn: $scope.customer_urn,
                    tier1_observations: $scope.txtobservations,
                    tier1_code: $scope.cboriskcode,
                    tier1code_changereason: lscode_changereason,
                    tier1code_changeflag: 'N',
                    tier1_justification: $scope.txtrationale_justification,
                    tier1_processgap: $scope.txtprocess_gap,
                    tier1_processrecommendation: $scope.txtimprovement_recommendation,
                    tier1_managementcomments: $scope.txtmanagement_comments,
                    tier1_reverts_actionplan: $scope.txtcheifheadreverts_actionplan,
                    tier1_atrdate: ATR_date
                    
                }

                lockUI();
                var url = "api/ObservationReport/PostTier1Format";
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        $state.go('app.rmVisitReport');
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
            else {
                console.log($scope.riskcode == $scope.cboriskcode);
                if (lscode_changereason == "" || lscode_changereason == undefined) {
                    Notify.alert('Kindly Enter Risk Code - Change Reason', {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    var date = $scope.txtATR_date;
                    var ATR_date = date.split("-").reverse().join("-");

                    var params = {
                        observation_reportgid:observation_reportgid,
                        allocationdtl_gid: allocationdtl_gid,
                        customer_name: $scope.customer_name,
                        customer_urn: $scope.customer_urn,
                        tier1_observations: $scope.txtobservations,
                        tier1_code: $scope.cboriskcode,
                        tier1code_changereason: lscode_changereason,
                        tier1code_changeflag: 'Y',
                        tier1_justification: $scope.txtrationale_justification,
                        tier1_processgap: $scope.txtprocess_gap,
                        tier1_processrecommendation: $scope.txtimprovement_recommendation,
                        tier1_managementcomments: $scope.txtmanagement_comments,
                        tier1_reverts_actionplan: $scope.txtcheifheadreverts_actionplan,
                        tier1_atrdate: ATR_date
                      
                    }

                    lockUI();
                    var url = "api/ObservationReport/PostTier1Format";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $state.go('app.rmVisitReport');
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


        }

        $scope.tier1formatUpdate = function () {
            var date = $scope.txtATR_date;
            var ATR_date = date.split("-").reverse().join("-");
            var params = {
                tier1format_gid: $scope.tier1format_gid,
                tier1_observations: $scope.txtobservations,
                tier1_code: $scope.cboriskcode,
                tier1code_changereason: $scope.txtcode_changereason,
                tier1_justification: $scope.txtrationale_justification,
                tier1_processgap: $scope.txtprocess_gap,
                tier1_processrecommendation: $scope.txtimprovement_recommendation,
                tier1_managementcomments: $scope.txtmanagement_comments,
                tier1_reverts_actionplan: $scope.txtcheifheadreverts_actionplan,
                tier1_atrdate: ATR_date,
                tier1_rejectedremarks: $scope.txttier1_rejectremarks
            }

            lockUI();
            var url = "api/ObservationReport/PostUpdateTier1Format";
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $state.go('app.rmVisitReport');
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

        $scope.uploaddocument = function (val, val1, name) {
            if ($scope.txtdocument_title == undefined || $scope.txtdocument_title == "") {
                alert('Enter the Document Title to Upload Document');
                $("#addExternalupload").val('');
                return false;
            }
            else {

                var frm = new FormData();

                for (var i = 0; i < val.length; i++) {
                    var item = {
                        name: val[i].name,
                        file: val[i]
                    };
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[i].name, "documentformatonly");

                        if (IsValidExtension == false) {
                            Notify.alert("File format is not supported..!", {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            return false;
                        }
            }
                // var item = {
                //     name: val[0].name,
                //     file: val[0]
                // };
                // var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");

                // if (IsValidExtension == false) {
                //     Notify.alert("File format is not supported..!", {
                //         status: 'danger',
                //         pos: 'top-center',
                //         timeout: 3000
                //     });
                //     return false;
                // }
                // var frm = new FormData();
                // frm.append('fileupload', item.file);
                // frm.append('file_name', item.name);
                frm.append('document_name', $scope.documentname);
                frm.append('document_title', $scope.txtdocument_title);
                frm.append('project_flag', "documentformatonly");
                $scope.uploadfrm = frm;
                lockUI();
                var url = 'api/TierMeeting/PostTier1Upload';

                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $scope.uploaddocument_list = resp.data.tier1document;
                    $("#addExternalupload").val('');
                    $scope.txtdocument_title = "";
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.uploadflag = 'Y';
                    }
                    else {
                        Notify.alert('File Format Not Supported!', {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.uploadflag = 'N';
                    }
                });
            }
        }

        $scope.uploadcancel = function (tmp_documentGid) {
            var tier1upload = {
                tmp_documentGid: tmp_documentGid
            }
            var url = 'api/TierMeeting/GetTier1UploadCancel';
            SocketService.getparams(url, tier1upload).then(function (resp) {
                $scope.uploaddocument_list = resp.data.upload_list;
            });
        }

    }
})();
