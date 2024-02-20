﻿(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCadDeferralDochecklistController', MstCadDeferralDochecklistController);

    MstCadDeferralDochecklistController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function MstCadDeferralDochecklistController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCadDeferralDochecklistController';
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        var credit_gid = $location.search().credit_gid;
        var lstype = $location.search().lstype;
        var processtypeassign_gid = $location.search().processtypeassign_gid;
        var lspath = $location.search().lspath;

        activate();

        function activate() {
            lockUI();

            $scope.hideeditevent = true;
            if (lspage == "CadScannedCompleted") {
                $scope.hideeditevent = false;
            }
            else {
                var params = {
                    credit_gid: credit_gid,
                    lstype: lstype
                }
                var url = 'api/MstScannedDocument/GetPageloadScannedDocument';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();

                });
            }
            var params = {
                credit_gid: credit_gid,
                application_gid: application_gid
            }

            var url = 'api/MstScannedDocument/GetCADTrnScannedDocList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtapplication_no = resp.data.application_no;
                $scope.txtbasiccustomer_name = resp.data.customer_name;
                $scope.ScannedDocumentlist = resp.data.ScannnedDocTaggedDocument;
                $scope.ScannnedCovenantDocumentlist = resp.data.ScannnedCovenantDocTaggedDocument;
            });
            var params = {
                credit_gid: credit_gid
            }
            var url = 'api/MstCADCreditAction/Getlabels';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtcustomer_name = resp.data.customer_name;
                $scope.txtstakeholder = resp.data.stakeholder_type;
            });

        }

        $scope.Back = function () {
            if (lspage == 'CadDeferralMaker') {
                $location.url('app/MstCadDeferralGuarantorDtls?application_gid=' + application_gid + '&lspage=CadDeferralMaker&processtypeassign_gid=' + processtypeassign_gid + '&lspath=' + lspath);
            }
            else {
                $location.url('app/MstCadDeferralGuarantorDtls?application_gid=' + application_gid + '&lspage=' + lspage + '&processtypeassign_gid=' + processtypeassign_gid + '&lspath=' + lspath);
            }
        }

        $scope.tagto_deferral = function (documentcheckdtl_gid, scanned_documentcount, documenttype_code, documenttype_name, covenant_type) {
            var modalInstance = $modal.open({
                templateUrl: '/TagToDeferral.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                lockUI();
                $scope.divraisequery = true;
                var url = 'api/MstScannedDocument/tmpclearQueryuploaded';
                SocketService.get(url).then(function (resp) {
                });
                $scope.showdeferraltag = false;
                $scope.errormsg = "";
                $scope.showerrordiv = false;
                var querynotclosed = false;
                $scope.NoMasterData = false;
                var params = {
                    documentcheckdtl_gid: documentcheckdtl_gid,
                    lstype: lstype
                }
                var url = 'api/MstScannedDocument/GetMStDeferraltag';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.documentseverity_gid = resp.data.documentseverity_gid;
                    $scope.txtdocumentseverity_name = resp.data.documentseverity_name;
                    $scope.txtdocument_code = resp.data.document_code;
                    $scope.txtdocumenttype = documenttype_code;
                    $scope.txtdocumenttype_name = documenttype_name;
                    $scope.Checklist = resp.data.MstChecklist;
                    if ($scope.Checklist == null || $scope.Checklist == undefined) {
                        $scope.showerrordiv = true;
                        $scope.NoMasterData = true;
                    }
                    else {
                        var params = {
                            documentcheckdtl_gid: documentcheckdtl_gid
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
                 

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.tagdoc_submit = function () {
                    var params = {
                        documentcheckdtl_gid: documentcheckdtl_gid,
                        application_gid: application_gid,
                        credit_gid: credit_gid,
                        documentseverity_gid: $scope.documentseverity_gid,
                        documentseverity_name: $scope.txtdocumentseverity_name,
                        tagged_to: $scope.cbotaggedto,
                        covenant_type: covenant_type,
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

                $scope.raise_query = function (deferralchecklist_gid, checklist_name) {

                    var url = 'api/MstScannedDocument/tmpclearQueryuploaded';
                    SocketService.get(url).then(function (resp) {
                    });

                    $scope.txtdoccheckpoint = checklist_name;
                    $scope.raise_deferralchecklist_gid = deferralchecklist_gid;
                    $scope.divraisequery = false;
                }

                $scope.QueryDocumentUpload = function (val, val1, name) {
                    lockUI();
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
                            unlockUI();
                            return false;
                        }
                    }
                    frm.append('document_title', $scope.txtdocument_id);
                    frm.append('tagquery_gid', "");
                    frm.append('documentcheckdtl_gid', documentcheckdtl_gid);

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

                $scope.documentviewer = function (val1, val2) {
                    lockUI();
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                    if (IsValidExtension == false) {
                        Notify.alert("View is not supported for this format..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        return false;
                    }
                    DownloaddocumentService.DocumentViewer(val1, val2);
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

                $scope.query_add = function () {
                    var params = {
                        query_title: $scope.txtdocumenttype_name + '-' + $scope.txtdoccheckpoint,
                        query_description: $scope.txtquery_desc,
                        application_gid: application_gid,
                        documentcheckdtl_gid: documentcheckdtl_gid,
                        deferralchecklist_gid: $scope.raise_deferralchecklist_gid,
                        deferralchecklist_name: $scope.txtdoccheckpoint
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
                                documentcheckdtl_gid: documentcheckdtl_gid
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

                $scope.closequery = function () {
                    $scope.divraisequery = true;
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
            }
        }

        $scope.deferraldoc_view = function (documentcheckdtl_gid, signeddocument_flag) {
            var modalInstance = $modal.open({
                templateUrl: '/document_view.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.PopupHeader = "Submitted Softcopy";
                var params = {
                    documentcheckdtl_gid: documentcheckdtl_gid,
                    signeddocument_flag: signeddocument_flag
                }
                var url = 'api/MstScannedDocument/GetScannedDocument';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.scanneduploaddocumentlist = resp.data.scanneduploaddocument;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.documentviewer = function (val1, val2) {
                    lockUI();
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                    if (IsValidExtension == false) {
                        Notify.alert("View is not supported for this format..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        return false;
                    }
                    DownloaddocumentService.DocumentViewer(val1, val2);
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

                $scope.downloadall = function () {
                    for (var i = 0; i < $scope.scanneduploaddocumentlist.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.scanneduploaddocumentlist[i].file_path, $scope.scanneduploaddocumentlist[i].file_name);
                    }
                }
            }

        }

        $scope.tagtodefferal_edit = function (deferraltagdoc_gid, documentcheckdtl_gid, scanned_documentcount, documenttype_code, documenttype_name, covenant_type) {
            var modalInstance = $modal.open({
                templateUrl: '/TagToDefferalEdit.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                lockUI();
                $scope.divraisequery = true;
                var url = 'api/MstScannedDocument/tmpclearQueryuploaded';
                SocketService.get(url).then(function (resp) {
                });
                var params = {
                    documentcheckdtl_gid: documentcheckdtl_gid
                }
                var url = 'api/MstScannedDocument/GettaggedDeferralinfo';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.errormsg = "";
                    $scope.showerrordiv = false;
                    $scope.showdeferraltag = false;
                    if (resp.data.tracking_id != null)
                        $scope.showdeferraltag = true;

                    $scope.documentseverity_gid = resp.data.documentseverity_gid;
                    $scope.txtdocumentseverity_name = resp.data.documentseverity_name;
                    $scope.tracking_id = resp.data.tracking_id;
                    $scope.cbotaggedto = resp.data.tagged_to;
                    $scope.txtdue_date = resp.data.Duedate;
                    $scope.txtdocument_code = resp.data.document_code;
                    $scope.txtdocumenttype = documenttype_code;
                    $scope.txtdocumenttype_name = documenttype_name;
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

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

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
                    $scope.showerrordiv = false;
                }

                $scope.raise_query = function (deferralchecklist_gid, checklist_name) {

                    var url = 'api/MstScannedDocument/tmpclearQueryuploaded';
                    SocketService.get(url).then(function (resp) {
                    });

                    $scope.txtdoccheckpoint = checklist_name;
                    $scope.raise_deferralchecklist_gid = deferralchecklist_gid;
                    $scope.divraisequery = false;
                }

                $scope.QueryDocumentUpload = function (val, val1, name) {
                    lockUI();
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
                            unlockUI();
                            return false;
                        }
                    }
                    frm.append('document_title', $scope.txtdocument_id);
                    frm.append('tagquery_gid', "");
                    frm.append('documentcheckdtl_gid', documentcheckdtl_gid);

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

                $scope.documentviewer = function (val1, val2) {
                    lockUI();
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                    if (IsValidExtension == false) {
                        Notify.alert("View is not supported for this format..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        return false;
                    }
                    DownloaddocumentService.DocumentViewer(val1, val2);
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

                $scope.closequery = function () {
                    $scope.divraisequery = true;
                }
                $scope.query_add = function () {
                    var params = {
                        query_title: $scope.txtdocumenttype_name + '-' + $scope.txtdoccheckpoint,
                        query_description: $scope.txtquery_desc,
                        application_gid: application_gid,
                        documentcheckdtl_gid: documentcheckdtl_gid,
                        deferralchecklist_gid: $scope.raise_deferralchecklist_gid,
                        deferralchecklist_name: $scope.txtdoccheckpoint
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
                                documentcheckdtl_gid: documentcheckdtl_gid
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
                            Notify.alert(resp.data.message, {
                                status: 'error',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });
                    $scope.divraisequery = true;
                }

                $scope.downloadallins = function () {
                    for (var i = 0; i < $scope.institutionupload_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.institutionupload_list[i].file_path, $scope.institutionupload_list[i].file_name);
                    }
                }
                $scope.download_doc2 = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }

                $scope.tagdoc_update = function () {
                    var params = {
                        documentcheckdtl_gid: documentcheckdtl_gid,
                        application_gid: application_gid,
                        credit_gid: credit_gid,
                        deferraltagdoc_gid: deferraltagdoc_gid,
                        documentseverity_gid: $scope.documentseverity_gid,
                        documentseverity_name: $scope.txtdocumentseverity_name,
                        tagged_to: $scope.cbotaggedto,
                        due_date: $scope.txtdue_date,
                        cad_remarks: $scope.txtcad_remarks,
                        covenant_type: covenant_type,
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

                    $modalInstance.close('closed');
                }

            }
        }



        $scope.deferraldoc_upload = function (documentcheckdtl_gid, documenttype_name, covenant_type, signeddocument_flag) {
            var modalInstance = $modal.open({
                templateUrl: '/defferal_docupload.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.txtdocument_name = documenttype_name;
                var params = {
                    documentcheckdtl_gid: documentcheckdtl_gid,
                    signeddocument_flag: signeddocument_flag
                }
                var url = 'api/MstScannedDocument/GetScannedDocument';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.scanneduploaddocumentlist = resp.data.scanneduploaddocument;

                });

                $scope.scannedDocumentUpload = function (val, val1, name) {
                    lockUI();
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
                            unlockUI();
                            return false;
                        }
                    }

                    frm.append('document_title', documenttype_name);
                    frm.append('documentcheckdtl_gid', documentcheckdtl_gid);
                    frm.append('application_gid', application_gid);
                    frm.append('credit_gid', credit_gid);
                    frm.append('RMupload', 'N');
                    frm.append('covenant_type', covenant_type);
                    frm.append('signeddocument_flag', signeddocument_flag);

                    $scope.uploadfrm = frm;
                    if ($scope.uploadfrm != undefined) {
                        var url = 'api/MstScannedDocument/ScannedDocumentUpload';
                        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                            unlockUI();
                            $("#scannedfile").val('');
                            var params = {
                                documentcheckdtl_gid: documentcheckdtl_gid,
                                signeddocument_flag: signeddocument_flag
                            }
                            var url = 'api/MstScannedDocument/GetScannedDocument';
                            lockUI();
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.scanneduploaddocumentlist = resp.data.scanneduploaddocument;
                            });

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
                            unlockUI();
                        });
                    }
                    else {
                        alert('Please select a file.')

                    }
                }

                $scope.documentviewer = function (val1, val2) {
                    lockUI();
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                    if (IsValidExtension == false) {
                        Notify.alert("View is not supported for this format..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        return false;
                    }
                    DownloaddocumentService.DocumentViewer(val1, val2);
                }

                $scope.ok = function () {
                    activate();
                    $modalInstance.close('closed');
                };

                $scope.downloads = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }

                $scope.downloadall = function () {
                    for (var i = 0; i < $scope.scanneduploaddocumentlist.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.scanneduploaddocumentlist[i].file_path, $scope.scanneduploaddocumentlist[i].file_name);
                    }
                }

                $scope.defdoc_delete = function (scanneddocument_gid) {
                    lockUI();
                    var params = {
                        scanneddocument_gid: scanneddocument_gid
                    }
                    var url = 'api/MstScannedDocument/cancelscanneduploaddocument';
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
                        var params = {
                            documentcheckdtl_gid: documentcheckdtl_gid,
                            signeddocument_flag: 'N'
                        }
                        var url = 'api/MstScannedDocument/GetScannedDocument';
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            $scope.scanneduploaddocumentlist = resp.data.scanneduploaddocument;

                        });
                    });
                }
            }

        }

        $scope.deferraldocmultiple_upload = function (covenant_type) {
            var modalInstance = $modal.open({
                templateUrl: '/defferal_docmultipleupload.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                /*$scope.txtdocument_name = documenttype_name;*/
                //var params = {
                //    documentcheckdtl_gid: documentcheckdtl_gid,
                //    signeddocument_flag: signeddocument_flag
                //}
                //var url = 'api/MstScannedDocument/GetScannedDocument';
                //lockUI();
                //SocketService.getparams(url, params).then(function (resp) {
                //    unlockUI();
                //    $scope.scanneduploaddocumentlist = resp.data.scanneduploaddocument;

                //});

                $scope.scannedDocumentUpload = function (val, val1, name) {
                    lockUI();
                    if ($scope.txtdocument_name == "" || $scope.txtdocument_name == undefined || $scope.txtdocument_name == null) {
                        alert('Kindly enter the document title');
                        $("#scannedmultiplefile").val('');
                        unlockUI();
                        return;
                    }
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
                            unlockUI();
                            return false;
                        }
                    }

                    frm.append('document_title', $scope.txtdocument_name);
                    frm.append('application_gid', application_gid);
                    frm.append('RMupload', 'N');
                    frm.append('covenant_type', covenant_type);

                    $scope.uploadfrm = frm;
                    if ($scope.uploadfrm != undefined) {
                        var url = 'api/MstScannedDocument/ScannedDocumentMultiUpload';
                        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                            unlockUI();
                            $("#scannedmultiplefile").val('');
                            unlockUI();
                            if (resp.data.status == true) {
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                activate();
                                $modalInstance.close('closed');
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
                    else {
                        alert('Please select a file.')

                    }
                }

                $scope.documentviewer = function (val1, val2) {
                    lockUI();
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                    if (IsValidExtension == false) {
                        Notify.alert("View is not supported for this format..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        return false;
                    }
                    DownloaddocumentService.DocumentViewer(val1, val2);
                }

                $scope.ok = function () {
                    activate();
                    $modalInstance.close('closed');
                };

                //$scope.downloads = function (val1, val2) {
                //    DownloaddocumentService.Downloaddocument(val1, val2);
                //}

                //$scope.downloadall = function () {
                //    for (var i = 0; i < $scope.scanneduploaddocumentlist.length; i++) {
                //        DownloaddocumentService.Downloaddocument($scope.scanneduploaddocumentlist[i].file_path, $scope.scanneduploaddocumentlist[i].file_name);
                //    }
                //}

                //$scope.defdoc_delete = function (scanneddocument_gid) {
                //    lockUI();
                //    var params = {
                //        scanneddocument_gid: scanneddocument_gid
                //    }
                //    var url = 'api/MstScannedDocument/cancelscanneduploaddocument';
                //    SocketService.getparams(url, params).then(function (resp) {
                //        unlockUI();
                //        if (resp.data.status == true) {

                //            Notify.alert(resp.data.message, {
                //                status: 'success',
                //                pos: 'top-center',
                //                timeout: 3000
                //            });

                //        }
                //        else {
                //            Notify.alert(resp.data.message, {
                //                status: 'warning',
                //                pos: 'top-center',
                //                timeout: 3000
                //            });

                //        }
                //        var params = {
                //            documentcheckdtl_gid: documentcheckdtl_gid,
                //            signeddocument_flag: 'N'
                //        }
                //        var url = 'api/MstScannedDocument/GetScannedDocument';
                //        lockUI();
                //        SocketService.getparams(url, params).then(function (resp) {
                //            unlockUI();
                //            $scope.scanneduploaddocumentlist = resp.data.scanneduploaddocument;

                //        });
                //    });
                //}
            }

        }

        $scope.defferalphysicaldoc_view = function (documentcheckdtl_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/document_view.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.PopupHeader = "Physical Documents";
                var params = {
                    documentcheckdtl_gid: documentcheckdtl_gid
                }
                var url = 'api/MstPhysicalDocument/GetPhysicalDocument';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.scanneduploaddocumentlist = resp.data.scanneduploaddocument;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.documentviewer = function (val1, val2) {
                    lockUI();
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                    if (IsValidExtension == false) {
                        Notify.alert("View is not supported for this format..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        return false;
                    }
                    DownloaddocumentService.DocumentViewer(val1, val2);
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


            }
        }

        $scope.movedtosigned_doc = function (documentcheckdtl_gid, scanneddocument_gid, ScannedDocumentlist) {
            var modalInstance = $modal.open({
                templateUrl: '/MovedtosignedDoc.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    documentcheckdtl_gid: documentcheckdtl_gid,
                    signeddocument_flag: 'N'
                }
                var url = 'api/MstScannedDocument/GetScannedDocument';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.scanneduploaddocumentlist = resp.data.scanneduploaddocument;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.checkall = function (selected, covenantdocumentclick) {
                    angular.forEach($scope.scanneduploaddocumentlist, function (val, key) {
                        val.checked = selected;
                    });
                }

                $scope.download_doc = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }

                $scope.downloadallsca = function () {
                    for (var i = 0; i < $scope.scanneduploaddocumentlist.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.scanneduploaddocumentlist[i].file_path, $scope.scanneduploaddocumentlist[i].file_name);
                    }
                }

                $scope.documentviewer = function (val1, val2) {
                    lockUI();
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                    if (IsValidExtension == false) {
                        Notify.alert("View is not supported for this format..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        return false;
                    }
                    DownloaddocumentService.DocumentViewer(val1, val2);
                }

                $scope.yesmovedtosigned = function () {
                    lockUI();
                    var doc_gid;
                    var doclistGId = [];
                    var count = 0;
                    angular.forEach($scope.scanneduploaddocumentlist, function (val) {
                        if (val.checked == true) {
                            count = count + 1;
                            var doclist_gid = val.scanneddocument_gid;
                            doc_gid = val.scanneddocument_gid;
                            doclistGId.push(doclist_gid);
                        }
                    });

                    var params = {
                        scanneddocument_gid: doclistGId,
                        documentcheckdtl_gid: documentcheckdtl_gid
                    }
                    if (doc_gid != undefined) {
                        var url = 'api/MstScannedDocument/PostMovedtoSignedDoc';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {
                                $modalInstance.close('closed');
                                activate();
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
                                $modalInstance.close('closed');
                            }

                        });
                    }
                    else {
                        Notify.alert('Select atleast one document..!', {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }


                }
            }

        }

        $scope.addcovenantperiods = function (groupdocumentchecklist_gid, documenttype_name, covenant_periods, buffer_days, Completed) {
            var modalInstance = $modal.open({
                templateUrl: '/AddCovenantPeriods.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.Completed = false;
                if (Completed == "Y")
                    $scope.Completed = true;

                $scope.lbldocumentname = documenttype_name;
                $scope.lblcovenantperiods = covenant_periods;
                $scope.lblbufferdays = buffer_days;
                $scope.dropdowncovenant = false;
                var params = {
                    groupdocumentdtl_gid: groupdocumentchecklist_gid,
                }
                var url = 'api/MstScannedDocument/GetScannedCovenantPeriodsSummary';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.scannedcovenantperiodlist = resp.data.mdlscannedcovenantperiod;
                    if ($scope.scannedcovenantperiodlist && $scope.scannedcovenantperiodlist.length > 0) {
                        $scope.dropdowncovenant = true;
                        $scope.cbocovenantperiod = covenant_periods;
                        $scope.cbobufferdays = buffer_days;
                    }
                });
                $scope.calender01 = function ($event) {
                    $event.preventDefault();
                    $event.stopPropagation();

                    $scope.open01 = true;
                };
                // Calender Popup... //

                $scope.calender02 = function ($event) {
                    $event.preventDefault();
                    $event.stopPropagation();

                    $scope.open02 = true;
                };
                // Calender Popup... //

                $scope.calender03 = function ($event) {
                    $event.preventDefault();
                    $event.stopPropagation();

                    $scope.open03 = true;
                };
                $scope.formats = ['dd-MM-yyyy'];
                $scope.format = $scope.formats[0];
                $scope.dateOptions = {
                    formatYear: 'yy',
                    startingDay: 1
                };



                $scope.onchangecovenantstart_date = function () {
                    var addmonth = 0;
                    if ($scope.cbocovenantperiod == "Each month")
                        addmonth = 1;
                    else if ($scope.cbocovenantperiod == "Every 3 months")
                        addmonth = 3;
                    else if ($scope.cbocovenantperiod == "Every 6 months")
                        addmonth = 6;
                    else if ($scope.cbocovenantperiod == "Every 1 year")
                        addmonth = 12;
                    var getdate = $scope.txtcovenantstart_date;
                    var d = new Date(getdate);
                    d = new Date(d.setMonth(d.getMonth() + parseInt(addmonth)));
                    //let day = newDate.getDate();
                    //let month = newDate.getMonth() + 1;
                    //let year = newDate.getFullYear();
                    var datestring = ("0" + d.getDate()).slice(-2) + "-" + ("0" + (d.getMonth() + 1)).slice(-2) + "-" + d.getFullYear();

                    $scope.txtcovenantend_date = datestring;
                    $scope.txtcovenantSubmission_date = $scope.txtcovenantend_date;

                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                    activate();
                };

                $scope.addcovenentperiodsclick = function () {
                    lockUI();
                    try {
                        if ($scope.txtcovenantstart_date.split("-"))
                            $scope.txtcovenantstart_date = $scope.txtcovenantstart_date.split("-").reverse().join("-");
                    }
                    catch (e) { }
                    try {
                        if ($scope.txtcovenantend_date.split("-"))
                            $scope.txtcovenantend_date = $scope.txtcovenantend_date.split("-").reverse().join("-");
                    }
                    catch (e) { }
                    try {
                        if ($scope.txtcovenantSubmission_date.split("-"))
                            $scope.txtcovenantSubmission_date = $scope.txtcovenantSubmission_date.split("-").reverse().join("-");
                    }
                    catch (e) { }

                    var params = {
                        covenant_periods: $scope.cbocovenantperiod,
                        groupdocumentdtl_gid: groupdocumentchecklist_gid,
                        credit_gid: credit_gid,
                        covenant_startdate: $scope.txtcovenantstart_date,
                        covenant_enddate: $scope.txtcovenantend_date,
                        covenant_submissiondate: $scope.txtcovenantSubmission_date,
                         buffer_days: $scope.cbobufferdays
                    }
                    console.log(params);
                    var url = 'api/MstScannedDocument/PostScannedCovenantPeriods';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $scope.txtcovenantstart_date = "";
                            $scope.txtcovenantend_date = "";
                            $scope.txtcovenantSubmission_date = "";
                            $scope.dropdowncovenant = true;
                            var params = {
                                groupdocumentdtl_gid: groupdocumentchecklist_gid,
                            }
                            var url = 'api/MstScannedDocument/GetScannedCovenantPeriodsSummary';
                            lockUI();
                            SocketService.getparams(url, params).then(function (resp) {
                                unlockUI();
                                $scope.scannedcovenantperiodlist = resp.data.mdlscannedcovenantperiod;

                            });
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
                            $modalInstance.close('closed');
                        }

                    });
                    unlockUI();

                }

                $scope.covenantperiod_delete = function (covenantperioddtl_gid) {
                    lockUI();
                    var params = {
                        covenantperioddtl_gid: covenantperioddtl_gid,
                        previous_covenantperiods: $scope.lblcovenantperiods,
                        groupdocumentdtl_gid: groupdocumentchecklist_gid,
                    }
                    var url = 'api/MstScannedDocument/GetcancelscannedCovenantPeriod';
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
                        var params = {
                            groupdocumentdtl_gid: groupdocumentchecklist_gid,
                        }
                        var url = 'api/MstScannedDocument/GetScannedCovenantPeriodsSummary';
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            $scope.scannedcovenantperiodlist = resp.data.mdlscannedcovenantperiod;
                            if ($scope.scannedcovenantperiodlist == null) {
                                $scope.dropdowncovenant = false;
                            }
                        });
                    });
                }
            }
        }

        $scope.defferal_history = function (documentcheckdtl_gid) {
            $location.url('app/MstScannedDeferralHistory?application_gid=' + application_gid + '&credit_gid=' + credit_gid + '&lspage=' + lspage + '&lstype=' + lstype + '&lsdocumentcheckdtl_gid=' + documentcheckdtl_gid + '&processtypeassign_gid=' + processtypeassign_gid + '&lspath=' + lspath);
        }

        $scope.raise_query = function (documentcheckdtl_gid,covenant_type) {
            $location.url('app/MstCadDeferralQuery?application_gid=' + application_gid + '&credit_gid=' + credit_gid + '&lspage=' + lspage + '&lstype=' + lstype + '&lsdocumentcheckdtl_gid=' + documentcheckdtl_gid + '&processtypeassign_gid=' + processtypeassign_gid + '&lspath=' + lspath + '&lscovenant_type=' + covenant_type);
        }

        $scope.extension_waiver = function (documentcheckdtl_gid, deferraltagged) {
            $location.url('app/MstCadDeferralStatus?application_gid=' + application_gid + '&credit_gid=' + credit_gid + '&lspage=' + lspage + '&lstype=' + lstype + '&lsdocumentcheckdtl_gid=' + documentcheckdtl_gid + '&processtypeassign_gid=' + processtypeassign_gid + '&lspath=' + lspath + '&lsdeferraltag=' + deferraltagged + '&lsscanned=Y');
        }

        $scope.doc_confirmation = function (documentcheckdtl_gid, taggeddeferral, documentconfirmation_remarks, overallstatus, scanned_documentcount, due_date, extendeddue_date) {
            if ($scope.hideeditevent == false) {
                var modalInstance = $modal.open({
                    templateUrl: '/DocumentCompleted.html',
                    controller: ModalInstanceCtrl,
                    size: 'md',
                    backdrop: 'static',
                    keyboard: false
                });
            }
            else {
                var modalInstance = $modal.open({
                    templateUrl: '/DocumentConfirmation.html',
                    controller: ModalInstanceCtrl,
                    size: 'md',
                    backdrop: 'static',
                    keyboard: false
                });
            }
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.btnconfirmation = true;
                $scope.lbldocumentconfirmation_remarks = documentconfirmation_remarks;
                $scope.lbloverallstatus = overallstatus;
                $scope.onchangeremarks = function (cbofinalstatus) {
                    $scope.errormsg = "";
                    $scope.showerrordiv = false;
                    $scope.other_remarks = false;
                    $scope.btnconfirmation = false;
                    var today = new Date();
                    today.setHours(0, 0, 0, 0);
                    var dueDate = "", extendedduedate = "";
                    if (due_date != "") {
                        dueDate = due_date.split("-")
                        dueDate = new Date(dueDate[2], dueDate[1] - 1, dueDate[0]);
                    }
                    if (extendeddue_date != "") {
                        extendedduedate = extendeddue_date.split("-")
                        extendedduedate = new Date(extendedduedate[2], extendedduedate[1] - 1, extendedduedate[0]);
                    }
                    // var extendeddue_date = new Date(extendeddue_date)
                    $scope.scanfinal_remarks = '';
                    if (cbofinalstatus == "Deferral Taken" && taggeddeferral != "0") {
                        $scope.errormsg = "Document is not Tagged to Deferral";
                        $scope.btnconfirmation = true;
                        $scope.showerrordiv = true;
                    }
                    else if (cbofinalstatus == "Query Cleared") {
                        if (scanned_documentcount == "0") {
                            $scope.errormsg = "Atleast one document should be in Signed document.";
                            $scope.btnconfirmation = true;
                            $scope.showerrordiv = true;
                        }
                        else if (dueDate > today || extendedduedate > today) {
                            $scope.errormsg = "Due date/Extended Due date should not be greater than Now of Date.";
                            $scope.btnconfirmation = true;
                            $scope.showerrordiv = true;
                        }
                        else {
                            var params = {
                                groupdocumentcheckdtl_gid: documentcheckdtl_gid,
                                lstype: ''
                            }
                            var url = 'api/MstScannedDocument/GetConfirmationValidation';
                            lockUI();
                            SocketService.getparams(url, params).then(function (resp) {
                                unlockUI();
                                if (resp.data.status == true) {
                                    $scope.errormsg = "Query Not closed";
                                    $scope.btnconfirmation = true;
                                    $scope.showerrordiv = true;
                                }
                            });
                        }
                    }
                    else if (cbofinalstatus == 'Waived') {
                        var params = {
                            groupdocumentcheckdtl_gid: documentcheckdtl_gid,
                            lstype: 'Waived'
                        }
                        var url = 'api/MstScannedDocument/GetConfirmationValidation';
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {
                                $scope.errormsg = "Waiver Approval Pending";
                                $scope.btnconfirmation = true;
                                $scope.showerrordiv = true;
                            }
                        });
                    }
                    else if (cbofinalstatus == 'Others') {
                        $scope.other_remarks = true;
                        $scope.scanfinal_remarks = '';
                    }

                }

                $scope.docconfirmation_Submit = function () {
                    if ($scope.txtfinalremarks == 'Others' && ($scope.scanfinal_remarks == '' || $scope.scanfinal_remarks == undefined || $scope.scanfinal_remarks == null)) {
                        $modalInstance.close('closed');
                        Notify.alert('Kindly Enter Remarks', 'warning')
                    }
                    else {
                        var params = {
                            documentcheckdtl_gid: documentcheckdtl_gid,
                            documentconfirmation_remarks: $scope.scanfinal_remarks,
                            overall_docstatus: $scope.cbofinalstatus
                        }
                        if (documentcheckdtl_gid != undefined) {
                            var url = 'api/MstScannedDocument/PostDocumentConfirmationDoc';
                            lockUI()
                            SocketService.post(url, params).then(function (resp) {
                                if (resp.data.status == true) {
                                    Notify.alert(resp.data.message, {
                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    $modalInstance.close('closed');
                                    unlockUI();
                                    activate();
                                }
                                else {
                                    unlockUI();
                                    Notify.alert(resp.data.message, {
                                        status: 'warning',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                }
                            });
                        }
                    }
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }

        }


        $scope.documentviewer = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
                return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
        }
    }
})();
