(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstRptCadScannedQueryController', MstRptCadScannedQueryController);

    MstRptCadScannedQueryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService', '$anchorScroll', 'cmnfunctionService'];

    function MstRptCadScannedQueryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService, $anchorScroll, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstRptCadScannedQueryController';
        var application_gid = $location.search().application_gid;
        var lspage = $location.search().lspage;
        var credit_gid = $location.search().credit_gid;
        var lstype = $location.search().lstype;
        var lsdocumentcheckdtl_gid = $location.search().lsdocumentcheckdtl_gid;
        var processtypeassign_gid = $location.search().processtypeassign_gid;
        var lspath = $location.search().lspath;
        var lscovenant_type = $location.search().lscovenant_type;

        activate();

        function activate() {
            $scope.adddeferraltable = false;
            $scope.editdeferraltable = false;
            $scope.vettingenable = true;
            lockUI();
            var url = 'api/MstScannedDocument/tmpclearQueryuploaded';
            SocketService.get(url).then(function (resp) {
            });
            var params = {
                documentcheckdtl_gid: lsdocumentcheckdtl_gid,
                signeddocument_flag: 'Y'
            }
            var url = 'api/MstScannedDocument/GetScannedDocument';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.scanneduploaddocumentlist = resp.data.scanneduploaddocument;

            });

            var param = {
                application_gid: application_gid
            };
            var url = 'api/MstApplicationApproval/Getapplicationdetails';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.lblapplref_no = resp.data.application_no;
                $scope.lblapplicant_name = resp.data.customer_name;
                $scope.lblcurrentappl_stage = resp.data.approval_status;
                $scope.lblappl_state = resp.data.region;
                $scope.lbloveralllimit_request = resp.data.overalllimit_amount;
                $scope.lbloveralllimit_request = (parseInt($scope.lbloveralllimit_request.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountwords = defaultamountwordschange($scope.lbloveralllimit_request);
            });
            var param = {
                documentcheckdtl_gid: lsdocumentcheckdtl_gid
            };
            var url = 'api/MstScannedDocument/GetAppcadQuerySummary';
            SocketService.getparams(url, param).then(function (resp) {
                if (resp.data != null) {
                    $scope.lbldocumentcode = resp.data.documenttype_code;
                    $scope.lbldocumenttype_name = resp.data.documenttype_name;
                    $scope.query_list = resp.data.mdlcadquery;
                    $scope.txtdocumenttype_name = $scope.lbldocumenttype_name;
                    $scope.txtdocumenttype = $scope.lbldocumentcode;
                    $scope.checklistcount = resp.data.checklistcount;
                    $scope.waiverpendingcount = resp.data.waiverpendingcount;
                    $scope.deferraltagdoc_gid = resp.data.deferraltagdoc_gid;

                    if ($scope.waiverpendingcount != 0) {
                        $scope.vettingenable = false;
                    }
                    if ($scope.waiverpendingcount == 0 && $scope.checklistcount == 0 && $scope.scanneduploaddocumentlist != null && $scope.scanneduploaddocumentlist.length != 0) {
                        $scope.divraisequery = true;
                        $scope.adddeferraltable = true;
                        $scope.editdeferraltable = false;
                        $scope.showdeferraltag = false;
                        $scope.errormsg = "";
                        $scope.showerrordiv = false;
                        var querynotclosed = false;
                        $scope.NoMasterData = false;
                        var params = {
                            documentcheckdtl_gid: lsdocumentcheckdtl_gid,
                            lstype: lstype
                        }
                        var url = 'api/MstScannedDocument/GetMStDeferraltag';
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            $scope.documentseverity_gid = resp.data.documentseverity_gid;
                            $scope.txtdocumentseverity_name = resp.data.documentseverity_name;
                            $scope.txtdocument_code = resp.data.document_code;
                            $scope.Checklist = resp.data.MstChecklist;
                            if ($scope.Checklist == null || $scope.Checklist == undefined) {
                                $scope.showerrordiv = true;
                                $scope.NoMasterData = true;
                            }
                            else {
                                var params = {
                                    documentcheckdtl_gid: lsdocumentcheckdtl_gid
                                }
                                var url = 'api/MstScannedDocument/GetQueryRaisedinfo';
                                SocketService.getparams(url, params).then(function (resp) {
                                    $scope.TagQueryCheckpoint = resp.data.MdlTagQueryCheckpoint;
                                    angular.forEach($scope.Checklist, function (value, key) {
                                        var lsgetquerychecklistgid = $scope.TagQueryCheckpoint.filter(function (el) { return el.deferralchecklist_gid === value.mstchecklist_gid });
                                        if (lsgetquerychecklistgid != "" && lsgetquerychecklistgid != null)
                                            value.query_flag = "Y";
                                        else value.query_flag = "N";
                                    });
                                });
                            }
                        });

                    }
                    else if ($scope.waiverpendingcount == 0 && $scope.checklistcount != 0) {
                        var params = {
                            documentcheckdtl_gid: lsdocumentcheckdtl_gid
                        }
                        var url = 'api/MstScannedDocument/GettaggedDeferralinfo';
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            $scope.errormsg = "";
                            $scope.divraisequery = true;
                            $scope.showerrordiv = false;
                            $scope.adddeferraltable = false;
                            $scope.editdeferraltable = true;
                            $scope.showdeferraltag = false;
                            if (resp.data.tracking_id != null)
                                $scope.showdeferraltag = true;

                            $scope.documentseverity_gid = resp.data.documentseverity_gid;
                            $scope.txtdocumentseverity_name = resp.data.documentseverity_name;
                            $scope.tracking_id = resp.data.tracking_id;
                            $scope.cbotaggedto = resp.data.tagged_to;
                            $scope.txtdue_date = resp.data.Duedate;
                            //$scope.txtdocument_code = resp.data.document_code;
                            //$scope.txtdocumenttype = documenttype_code;
                            //$scope.txtdocumenttype_name = documenttype_name;
                            if ($scope.txtdue_date != "") {
                                $scope.txtdue_date = new Date($scope.txtdue_date);
                            }
                            $scope.txtcad_remarks = resp.data.cad_remarks;
                            $scope.deferraltaggedchecklist = resp.data.deferraltaggedchecklist;
                            angular.forEach($scope.deferraltaggedchecklist, function (value, key) {
                                if (value.documentverified == true) {
                                    value.documentverified = "true";
                                    value.deferraltagged = false;
                                }
                                else if (value.deferraltagged == true) {
                                    value.documentverified = false;
                                    value.deferraltagged = "true";
                                }
                            });
                        });

                        var params = {
                            documentcheckdtl_gid: lsdocumentcheckdtl_gid
                        }
                        var url = 'api/MstScannedDocument/GetQueryRaisedinfo';
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.TagQueryCheckpoint = resp.data.MdlTagQueryCheckpoint;
                            angular.forEach($scope.deferraltaggedchecklist, function (value, key) {
                                var lsgetquerychecklistgid = $scope.TagQueryCheckpoint.filter(function (el) { return el.deferralchecklist_gid === value.deferralchecklist_gid });
                                if (lsgetquerychecklistgid != "" && lsgetquerychecklistgid != null)
                                    value.query_flag = "Y";
                                else value.query_flag = "N";
                            });
                        });
                    }
                    else {
                        $scope.vettingenable = false;
                    }
                }


            });

            var params = {
                groupdocumentcheckdtl_gid: lsdocumentcheckdtl_gid
            }
            var url = 'api/MstScannedDocument/GetMakerCheckerConversation';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.makercheckerconversation = resp.data.mdlmakercheckerconversation;
            });
            if (lspage === 'CadScannedCompleted') {
                $scope.hideeditevent = false;
                $scope.vettingenable = false;
            }
            else {
                $scope.hideeditevent = true;
            }
        }


        $scope.query_add = function () {
            var params = {
                query_title: $scope.txtdocumenttype_name + '-' + $scope.txtdoccheckpoint,
                query_description: $scope.txtquery_desc,
                application_gid: application_gid,
                documentcheckdtl_gid: lsdocumentcheckdtl_gid,
                deferralchecklist_gid: $scope.raise_deferralchecklist_gid,
                deferralchecklist_name: $scope.txtdoccheckpoint,
                document_gid: $scope.cbodocumentname.scanneddocument_gid,
                document_name: $scope.cbodocumentname.documenttype_name + ' / ' + $scope.cbodocumentname.file_name
            }
            var url = 'api/MstScannedDocument/PostAppcadqueryadd';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.txtquery_title = '';
                    $scope.txtquery_desc = '';
                    $scope.institutionupload_list = '';
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });

                    var params = {
                        documentcheckdtl_gid: lsdocumentcheckdtl_gid
                    }
                    var url = 'api/MstScannedDocument/GetQueryRaisedinfo';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.TagQueryCheckpoint = resp.data.MdlTagQueryCheckpoint;
                        angular.forEach($scope.Checklist, function (value, key) {
                            var lsgetquerychecklistgid = $scope.TagQueryCheckpoint.filter(function (el) { return el.deferralchecklist_gid === value.mstchecklist_gid });
                            if (lsgetquerychecklistgid != "" && lsgetquerychecklistgid != null)
                                value.query_flag = "Y";
                            else value.query_flag = "N";
                        });
                    });

                    var param = {
                        documentcheckdtl_gid: lsdocumentcheckdtl_gid
                    };
                    var url = 'api/MstScannedDocument/GetAppcadQuerySummary';
                    SocketService.getparams(url, param).then(function (resp) {
                        $scope.lbldocumentcode = resp.data.documenttype_code;
                        $scope.lbldocumenttype_name = resp.data.documenttype_name;
                        $scope.query_list = resp.data.mdlcadquery;
                        $scope.txtdocumenttype_name = $scope.lbldocumenttype_name;
                        $scope.txtdocumenttype = $scope.lbldocumentcode;

                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'error',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
            $scope.divraisequery = true;
        }

        $scope.QueryDocumentUpload = function (val, val1, name) {
            lockUI();
            var frm = new FormData();
            var arrayfile = [];
            for (var i = 0; i < val.length; i++) {
                var item = {
                    name: val[i].name,
                    file: val[i]
                };

                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);

                var CheckuploadedfileName = "";
                if ($scope.institutionupload_list != null) {
                    CheckuploadedfileName = $scope.institutionupload_list.filter(function (el) { return el.file_name === item.name });
                }
                var CheckfileName = arrayfile.filter(function (el) { return el.name === item.name });
                if ((CheckuploadedfileName && CheckuploadedfileName.length > 0) || (CheckfileName && CheckfileName.length > 0)) {
                    Notify.alert('Duplicate Document Name Found..!', {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $("#queryfile").val('');
                    $scope.txtdocument_id = "";
                    return;
                }
                arrayfile.push(item);
            }

            frm.append('document_title', $scope.txtdocument_id);
            frm.append('tagquery_gid', "");
            frm.append('documentcheckdtl_gid', lsdocumentcheckdtl_gid);

            $scope.uploadfrm = frm;
            if ($scope.uploadfrm != undefined) {
                var url = 'api/MstScannedDocument/QueryDocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $("#queryfile").val('');
                    $scope.txtdocument_id = "";
                    var params = {
                        tagquery_gid: ''
                    }
                    var url = 'api/MstScannedDocument/GetTmpQueryDocument';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.institutionupload_list = resp.data.queryuploaddocument;
                    });
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
                    unlockUI();
                });
            }
            else {
                alert('Please select a file.')

            }
        }

        $scope.download_doc2 = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }


        $scope.downloadallins = function () {
            for (var i = 0; i < $scope.institutionupload_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.institutionupload_list[i].file_path, $scope.institutionupload_list[i].file_name);
            }
        }
        $scope.defdoc_delete = function (tagquerydocument_gid, tagquery_gid) {
            lockUI();
            var params = {
                tagquerydocument_gid: tagquerydocument_gid
            }
            var url = 'api/MstScannedDocument/cancelQueryuploaddocument';
            SocketService.getparams(url, params).then(function (resp) {

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
                var params = {
                    tagquery_gid: tagquery_gid
                }
                var url = 'api/MstScannedDocument/GetTmpQueryDocument';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.institutionupload_list = resp.data.queryuploaddocument;
                });
                unlockUI();
            });
        }


        $scope.download_doc = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.view_querydesc = function (query_description, query_responseremarks) {
            var modalInstance = $modal.open({
                templateUrl: '/queryDescriptionView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.lblquery_desc = query_description;
                $scope.lblquery_responseremarks = query_responseremarks;
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.defferaldoc_view = function (tagquery_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/document_view.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    tagquery_gid: tagquery_gid
                }
                var url = 'api/MstScannedDocument/GetQueryDocument';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.queryuploaddocument = resp.data.queryuploaddocument;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.download_doc1 = function (val1, val2) {
                    //var phyPath = val1;
                    //var relPath = phyPath.split("StoryboardAPI");
                    //var relpath1 = relPath[1].replace("\\", "/");
                    //var hosts = window.location.host;
                    //var prefix = location.protocol + "//";
                    //var str = prefix.concat(hosts, relpath1);
                    //var link = document.createElement("a");
                    //link.download = val2;
                    //var uri = str;
                    //link.href = uri;
                    //link.click();
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }


                $scope.downloadallquery = function () {
                    for (var i = 0; i < $scope.queryuploaddocument.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.queryuploaddocument[i].file_path, $scope.queryuploaddocument[i].file_name);
                    }
                }
            }

        }



        $scope.Back = function () {
            $location.url('app/MstRptCadScannedDocchecklist?application_gid=' + application_gid + '&credit_gid=' + credit_gid + '&lspage=' + lspage + '&lstype=' + lstype + '&processtypeassign_gid=' + processtypeassign_gid + '&lspath=' + lspath);
        }

        $scope.responseclick = function () {
            var maker_flag = "N";
            if (lspage == "CadDeferralMaker") {
                maker_flag = "Y";
            }
            var params = {
                send_message: $scope.txtresponse,
                groupdocumentdtl_gid: lsdocumentcheckdtl_gid,
                application_gid: application_gid,
                credit_gid: credit_gid,
                maker_flag: maker_flag
            }
            var url = 'api/MstScannedDocument/postMakerCheckerConversation';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    var params = {
                        groupdocumentcheckdtl_gid: lsdocumentcheckdtl_gid
                    }
                    var url = 'api/MstScannedDocument/GetMakerCheckerConversation';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.txtresponse = "";
                        $scope.makercheckerconversation = resp.data.mdlmakercheckerconversation;
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'error',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }

        $scope.clearmessage = function () {
            $scope.txtresponse = "";
        }

        $scope.closequery = function () {
            $scope.divraisequery = true;
        }


        $scope.downloadall = function () {
            for (var i = 0; i < $scope.scanneduploaddocumentlist.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.scanneduploaddocumentlist[i].file_path, $scope.scanneduploaddocumentlist[i].file_name);
            }
        }

        $scope.raise_query = function (deferralchecklist_gid, checklist_name) {

            var url = 'api/MstScannedDocument/tmpclearQueryuploaded';
            SocketService.get(url).then(function (resp) {
            });

            $scope.txtdoccheckpoint = checklist_name;
            $scope.raise_deferralchecklist_gid = deferralchecklist_gid;
            $scope.divraisequery = false;

            $location.hash('raisequerydiv');
            $anchorScroll();
        }

        $scope.Cancel = function () {
            angular.forEach($scope.Checklist, function (value, key) {
                value.documentverified = false;
                value.deferraltagged = false;
            });
        }

        $scope.documentverifiededitcheck = function (index) {
            var querynotclosed = false;
            $scope.errormsgScannedDoc = false;
            $scope.errorBothmsg = false;
            $scope.errormsgScannedDoc = false;
            angular.forEach($scope.deferraltaggedchecklist, function (value, key) {
                if (key == index)
                    value.deferraltagged = false;
            });
            var deferrralchecked = $scope.deferraltaggedchecklist.filter(function (el) { return el.deferraltagged === true || el.deferraltagged === "true" });
            if (deferrralchecked && deferrralchecked.length > 0)
                $scope.showdeferraltag = true;
            else
                $scope.showdeferraltag = false;
            var documentverifychecked = $scope.deferraltaggedchecklist.filter(function (el) { return el.documentverified === true || el.documentverified === "true" });
            if (documentverifychecked != null && documentverifychecked.length == $scope.deferraltaggedchecklist.length) {
                var params = {
                    groupdocumentcheckdtl_gid: lsdocumentcheckdtl_gid,
                    lstype: ''
                }
                var url = 'api/MstScannedDocument/GetConfirmationValidation';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        querynotclosed = true;
                        $scope.showerrordiv = true;
                        if (scanned_documentcount == "0" && querynotclosed == true)
                            $scope.errorBothmsg = true;
                        else if (scanned_documentcount != "0" && querynotclosed == true)
                            $scope.errormsgqueryNC = true;
                    }
                    else {
                        if (scanned_documentcount == "0" && querynotclosed == false) {
                            $scope.errormsgScannedDoc = true;
                            $scope.showerrordiv = true;
                        }
                    }
                });
            }
        }

        $scope.deferraltaggededitcheck = function (index) {
            angular.forEach($scope.deferraltaggedchecklist, function (value, key) {
                if (key == index) {
                    value.documentverified = "false";
                    value.deferraltagged = "true";
                }
            });
            var deferrralchecked = $scope.deferraltaggedchecklist.filter(function (el) { return el.deferraltagged === true || el.deferraltagged === "true" });
            if (deferrralchecked && deferrralchecked.length > 0) {
                $scope.showdeferraltag = true;
            }
            else {
                $scope.showdeferraltag = false;
            }
        }

        $scope.tagdoc_update = function (deferraltagdoc_gid) {
            var params = {
                documentcheckdtl_gid: lsdocumentcheckdtl_gid,
                application_gid: application_gid,
                credit_gid: credit_gid,
                deferraltagdoc_gid: $scope.deferraltagdoc_gid,
                documentseverity_gid: $scope.documentseverity_gid,
                documentseverity_name: $scope.txtdocumentseverity_name,
                tagged_to: $scope.cbotaggedto,
                due_date: $scope.txtdue_date,
                cad_remarks: $scope.txtcad_remarks,
                covenant_type: lscovenant_type,
                scanneddoc_flag: 'Y',
                deferraltaggedchecklist: $scope.deferraltaggedchecklist
            }
            var url = 'api/MstScannedDocument/UpdateDeferralTaggedDoc';
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
                    activate();
                }
            });

        }

        $scope.downloadallins = function () {
            for (var i = 0; i < $scope.institutionupload_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.institutionupload_list[i].file_path, $scope.institutionupload_list[i].file_name);
            }
        }

        $scope.tagdoc_submit = function () {
            var params = {
                documentcheckdtl_gid: lsdocumentcheckdtl_gid,
                application_gid: application_gid,
                credit_gid: credit_gid,
                documentseverity_gid: $scope.documentseverity_gid,
                documentseverity_name: $scope.txtdocumentseverity_name,
                tagged_to: $scope.cbotaggedto,
                covenant_type: lscovenant_type,
                scanneddoc_flag: 'Y',
                deferraltaggedchecklist: $scope.Checklist
            }
            var url = 'api/MstScannedDocument/PostDeferralTaggedDoc';
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
                    activate();
                }
            });
            $modalInstance.close('closed');
        }

        $scope.documentverifiedcheck = function (index) {
            $scope.errormsgScannedDoc = false;
            $scope.errorBothmsg = false;
            $scope.errormsgScannedDoc = false;
            angular.forEach($scope.Checklist, function (value, key) {
                if (key == index)
                    value.deferraltagged = false;
            });
            var deferrralchecked = $scope.Checklist.filter(function (el) { return el.deferraltagged === "true" });
            if (deferrralchecked && deferrralchecked.length > 0)
                $scope.showdeferraltag = true;
            else
                $scope.showdeferraltag = false;
            var documentverifychecked = $scope.Checklist.filter(function (el) { return el.documentverified === "true" });
            if (documentverifychecked != null && documentverifychecked.length == $scope.Checklist.length) {
                var params = {
                    groupdocumentcheckdtl_gid: documentcheckdtl_gid,
                    lstype: ''
                }
                var url = 'api/MstScannedDocument/GetConfirmationValidation';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        querynotclosed = true;
                        $scope.showerrordiv = true;
                        if (scanned_documentcount == "0" && querynotclosed == true)
                            $scope.errorBothmsg = true;
                        else if (scanned_documentcount != "0" && querynotclosed == true)
                            $scope.errormsgqueryNC = true;
                    }
                    else {
                        if (scanned_documentcount == "0" && querynotclosed == false) {
                            $scope.errormsgScannedDoc = true;
                            $scope.showerrordiv = true;
                        }
                    }
                });
            }
        }

        $scope.deferraltaggedcheck = function (index) {
            angular.forEach($scope.Checklist, function (value, key) {
                if (key == index)
                    value.documentverified = false;
            });
            var deferrralchecked = $scope.Checklist.filter(function (el) { return el.deferraltagged === "true" });
            if (deferrralchecked && deferrralchecked.length > 0) {
                $scope.showdeferraltag = true;
            }
            else {
                $scope.showdeferraltag = false;
            }
        }

        function defaultamountwordschange(input) {
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords = cmnfunctionService.fnConvertNumbertoWord(str);
            return lswords;
        }

    }
})();
