(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCadPhysicalDocQueryController', MstCadPhysicalDocQueryController);

    MstCadPhysicalDocQueryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService','$anchorScroll','cmnfunctionService'];

    function MstCadPhysicalDocQueryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService, $anchorScroll, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCadPhysicalDocQueryController';
        var application_gid = $location.search().application_gid;
        var lspage = $location.search().lspage;
        var credit_gid = $location.search().credit_gid;
        var lstype = $location.search().lstype;
        var lsdocumentcheckdtl_gid = $location.search().lsdocumentcheckdtl_gid;
        var processtypeassign_gid = $location.search().processtypeassign_gid;
        var lspath = $location.search().lspath;
        var lscovenant_type = $location.search().lscovenant_type;

        activate();
        lockUI();
        function activate() {
            lockUI();
            $scope.calender02 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
    
                $scope.open02 = true;
            };
            $scope.formats = ['dd-MM-yyyy'];
            $scope.format = $scope.formats[0];
            $scope.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            $scope.adddeferraltable = false;
            $scope.editdeferraltable = false;
            $scope.vettingenable = true;
            lockUI();
            $scope.lsmaker_flag = (lspage == "CadPhysicalDocMaker")?'Y':'N';
            var url = 'api/MstPhysicalDocument/tmpphysicalclearQueryuploaded';
            SocketService.get(url).then(function (resp) {
            });
            var param = {
                application_gid: application_gid
            };
            var documentgid = {
                documentcheckdtl_gid: lsdocumentcheckdtl_gid
            }
            var url = 'api/MstPhysicalDocument/GetPhysicalDocument'; 
            SocketService.getparams(url, documentgid).then(function (resp) { 
                $scope.originaluploaddocumentlist = resp.data.scanneduploaddocument;
            });
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
            var url = 'api/MstPhysicalDocument/GetPhysicalAppcadQuerySummary';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.lbldocumentcode = resp.data.documenttype_code;
                $scope.lbldocumenttype_name = resp.data.documenttype_name;
                $scope.query_list = resp.data.mdlcadquery;
                $scope.txtdocumentsubmission_date = resp.data.documentsubmission_date;
                $scope.checklistcount = resp.data.checklistcount;
                $scope.waiverpendingcount = resp.data.waiverpendingcount;
                $scope.deferraltagdoc_gid = resp.data.deferraltagdoc_gid;

                if ($scope.waiverpendingcount != 0) {
                    $scope.vettingenable = false;
                }
                if ($scope.waiverpendingcount == 0 && $scope.checklistcount == 0 && $scope.originaluploaddocumentlist != null && $scope.originaluploaddocumentlist.length != 0) {
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
                            var url = 'api/MstPhysicalDocument/GetQueryRaisedinfo';
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.TagQueryCheckpoint = resp.data.MdlTagQueryCheckpoint;
                                angular.forEach($scope.Checklist, function (value, key) {
                                    var lsgetquerychecklistgid = $scope.TagQueryCheckpoint.filter(function (el) { return el.deferralchecklist_gid === value.mstchecklist_gid });
                                    if (lsgetquerychecklistgid != "" && lsgetquerychecklistgid != null)
                                        value.query_flag = "Y";
                                    else value.query_flag = "N";
                                    value.documentverified = "false"; 
                                });
                            });
                        }
                    });

                }
                else if ($scope.waiverpendingcount == 0 && $scope.checklistcount != 0) {
                    var params = {
                        documentcheckdtl_gid: lsdocumentcheckdtl_gid
                    }
                    var url = 'api/MstPhysicalDocument/GettaggedDeferralinfo';
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
                    var url = 'api/MstPhysicalDocument/GetQueryRaisedinfo';
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
                    $scope.divraisequery = true;
                }
            }); 

          

            var params = {
                documentcheckdtl_gid: lsdocumentcheckdtl_gid,
                signeddocument_flag: 'Y'
            }
            var url = 'api/MstScannedDocument/GetScannedDocument'; 
            SocketService.getparams(url, params).then(function (resp) { 
                $scope.scanneduploaddocumentlist = resp.data.scanneduploaddocument;
                if($scope.scanneduploaddocumentlist && $scope.scanneduploaddocumentlist!=null)
                  $scope.scanned_documentcount = $scope.scanneduploaddocumentlist.length;
            });

            var param = {
                documentcheckdtl_gid: lsdocumentcheckdtl_gid
            };
            var url = 'api/MstScannedDocument/GetAppcadQuerySummary';
            SocketService.getparams(url, params).then(function (resp) { 
                $scope.softcopyquery_list = resp.data.mdlcadquery;
            });

            var param = {
                documentcheckdtl_gid: lsdocumentcheckdtl_gid
            };
            var url = 'api/MstScannedDocument/GetInitiatedExtensionorwaiver';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.intiatextension_list = resp.data.mdlinitiateextendwaiver;
                unlockUI();
            });

            
            var params = {
                groupdocumentcheckdtl_gid: lsdocumentcheckdtl_gid
            }
            var url = 'api/MstPhysicalDocument/GetMakerCheckerConversation';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.makercheckerconversation = resp.data.mdlmakercheckerconversation;
            });

            if (lspage === 'CadPhysicalCompleted') {
                $scope.hideeditevent = false;
            }
            else {
                $scope.hideeditevent = true;
            }
        }

        $scope.query_add = function () {
            var maker_flag = (lspage == "CadPhysicalDocMaker")?'Y':'N';
            var params = {
                query_title: $scope.txtdocumenttype_name + '-' + $scope.txtdoccheckpoint, 
                query_description: $scope.txtquery_desc,
                application_gid: application_gid,
                documentcheckdtl_gid: lsdocumentcheckdtl_gid,
                deferralchecklist_gid: $scope.raise_deferralchecklist_gid,
                deferralchecklist_name: $scope.txtdoccheckpoint,
                document_gid: $scope.cbodocumentname.scanneddocument_gid,
                document_name: $scope.cbodocumentname.documenttype_name + ' / ' + $scope.cbodocumentname.file_name,
                maker_flag: maker_flag
            }
            var url = 'api/MstPhysicalDocument/PostAppcadPhysicalqueryadd';
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
                    refreshquery();
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

        function refreshquery (){
            var params = {
                documentcheckdtl_gid: lsdocumentcheckdtl_gid
            }
            var url = 'api/MstPhysicalDocument/GetQueryRaisedinfo';
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
            var url = 'api/MstPhysicalDocument/GetPhysicalAppcadQuerySummary';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.lbldocumentcode = resp.data.documenttype_code;
                $scope.lbldocumenttype_name = resp.data.documenttype_name;
                $scope.query_list = resp.data.mdlcadquery;
                $scope.txtdocumenttype_name = $scope.lbldocumenttype_name;
                $scope.txtdocumenttype = $scope.lbldocumentcode;
            });
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

            frm.append('tagquery_gid', "");
            frm.append('documentcheckdtl_gid', lsdocumentcheckdtl_gid);
            frm.append('project_flag', "Default");
            $scope.uploadfrm = frm;
            if ($scope.uploadfrm != undefined) {
                var url = 'api/MstPhysicalDocument/PhysicalQueryDocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $("#queryfile").val('');

                    var params = {
                        tagquery_gid: ''
                    }
                    var url = 'api/MstPhysicalDocument/GetTmpPhysicalQueryDocument';
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
                var url = 'api/MstPhysicalDocument/GetTmpPhysicalQueryDocument';
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
        $scope.view_scannedquerydesc = function (tagquery_gid,query_status,query_description, query_responseremarks) {
            var modalInstance = $modal.open({
                templateUrl: '/scannedqueryDescriptionView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.lblquery_desc = query_description;
                $scope.lblquery_responseremarks = query_responseremarks;
                var maker_flag = (lspage == "CadDeferralMaker")?'Y':'N';
                if(maker_flag!="Y")
                   $scope.lblquery_status = query_status;
                else
                   $scope.lblquery_status="";
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.querystatusupdate = function (query_status) {
                  
                    var params ={
                        query_status:query_status,
                        tagquery_gid: tagquery_gid,
                        documentcheckdtl_gid: lsdocumentcheckdtl_gid
                    }
                    var url = 'api/MstScannedDocument/GetUpdateCADRaiseQueryStatus';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) { 
                        if (resp.data.status == true) {
                            unlockUI();
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            getquerydtl();
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
                    $modalInstance.close('closed');
                }; 
            }
        }

        function getquerydtl(){
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

        $scope.view_querydesc = function (tagquery_gid,query_status,query_description, query_responseremarks) {
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
                var maker_flag = (lspage == "CadPhysicalDocMaker")?'Y':'N';
                if(maker_flag!="Y")
                   $scope.lblquery_status = query_status;
                else
                   $scope.lblquery_status="";
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.querystatusupdate = function (query_status) {
                  
                    var params ={
                        query_status:query_status,
                        tagquery_gid: tagquery_gid,
                        documentcheckdtl_gid: lsdocumentcheckdtl_gid
                    }
                    var url = 'api/MstPhysicalDocument/GetUpdateCADRaiseQueryStatus';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) { 
                        if (resp.data.status == true) {
                            unlockUI();
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            refreshquery();
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

        $scope.responseclick = function () {
            var maker_flag = "N";
            if (lspage == "CadPhysicalDocMaker") {
                maker_flag = "Y";
            }
            var params = {
                send_message: $scope.txtresponse,
                groupdocumentdtl_gid: lsdocumentcheckdtl_gid,
                application_gid: application_gid,
                credit_gid: credit_gid,
                maker_flag: maker_flag
            }
            var url = 'api/MstPhysicalDocument/postPhysicalMakerCheckerConversation';
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
                    var url = 'api/MstPhysicalDocument/GetMakerCheckerConversation';
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


        $scope.Back = function () {
            $location.url('app/MstCadPhysicalDochecklist?application_gid=' + application_gid + '&credit_gid=' + credit_gid + '&lspage=' + lspage + '&lstype=' + lstype + '&processtypeassign_gid=' + processtypeassign_gid + '&lspath=' + lspath);
             
        }

        
        function defaultamountwordschange(input) {
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords = cmnfunctionService.fnConvertNumbertoWord(str);
            return lswords;
        }
         
        $scope.reasonapproval_view = function (reason, approval_status, initiateextendorwaiver_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/remarksandreasondtl.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.lblreason = reason;
                $scope.lblapproval_status = approval_status;
                if (approval_status != 'No Approval') {
                    $scope.lblapproval_status = '';
                    var params = {
                        initiateextendorwaiver_gid: initiateextendorwaiver_gid
                    }
                    var url = 'api/MstScannedDocument/GetApprovalExtensionwaiver';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.approvallist = resp.data.mdlapprovaldtl;

                    });
                }


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }

        }

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.scanneduploaddocumentlist.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.scanneduploaddocumentlist[i].file_path, $scope.scanneduploaddocumentlist[i].file_name);
            }
        }

        $scope.downloadallsca = function () {
            for (var i = 0; i < $scope.originaluploaddocumentlist.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.originaluploaddocumentlist[i].file_path, $scope.originaluploaddocumentlist[i].file_name);
            }
        }

        $scope.updatedocumentsubmissiondate = function () {
            lockUI();
            try {
                if ($scope.txtdocumentsubmission_date.split("-"))
                    $scope.txtdocumentsubmission_date = $scope.txtdocumentsubmission_date.split("-").reverse().join("-");
            }
            catch (e) { } 

            var params = {
                groupdocumentdtl_gid: lsdocumentcheckdtl_gid,
                documentsubmission_date: $scope.txtdocumentsubmission_date, 
                covenant_type: lscovenant_type
            } 
            var url = 'api/MstPhysicalDocument/GetUpdateDocumentSubmission';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {  
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
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

        $scope.raise_query = function (deferralchecklist_gid, checklist_name) {

            var url = 'api/MstPhysicalDocument/tmpclearQueryuploaded';
            SocketService.get(url).then(function (resp) {
            });

            $scope.txtdoccheckpoint = checklist_name;
            $scope.raise_deferralchecklist_gid = deferralchecklist_gid;
            $scope.divraisequery = false;

            $location.hash('raisequerydiv');
            $anchorScroll();
        }
        $scope.closequery = function () {
            $scope.divraisequery = true;
        }

        $scope.deletependingquery = function(tagquery_gid){
            var params = {
                tagquery_gid: tagquery_gid 
            }
            var url = 'api/MstPhysicalDocument/GetPendingQueryDelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) { 
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    refreshquery();
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
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
                scanneddoc_flag: 'N',
                deferraltaggedchecklist: $scope.Checklist
            }
            var url = 'api/MstPhysicalDocument/PostPhysicalDeferralTaggedDoc';
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
                var url = 'api/MstPhysicalDocument/GetConfirmationValidation';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        querynotclosed = true;
                        $scope.showerrordiv = true;
                        if ($scope.scanned_documentcount == "0" && querynotclosed == true)
                            $scope.errorBothmsg = true;
                        else if ($scope.scanned_documentcount != "0" && querynotclosed == true)
                            $scope.errormsgqueryNC = true;
                    }
                    else {
                        if ($scope.scanned_documentcount == "0" && querynotclosed == false) {
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
                var url = 'api/MstPhysicalDocument/GetConfirmationValidation';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        querynotclosed = true;
                        $scope.showerrordiv = true;
                        if ($scope.scanned_documentcount == "0" && querynotclosed == true)
                            $scope.errorBothmsg = true;
                        else if ($scope.scanned_documentcount != "0" && querynotclosed == true)
                            $scope.errormsgqueryNC = true;
                    }
                    else {
                        if ($scope.scanned_documentcount == "0" && querynotclosed == false) {
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
            var url = 'api/MstPhysicalDocument/UpdatePhysicalDeferralTaggedDoc';
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
