(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmTrnSampleQueryController', AtmTrnSampleQueryController);

    AtmTrnSampleQueryController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', '$sce', '$anchorScroll', 'DownloaddocumentService', 'cmnfunctionService'];

    function AtmTrnSampleQueryController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, $sce, $anchorScroll, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmTrnSampleQueryController';
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.auditcreation_gid = searchObject.auditcreation_gid;
        var auditcreation_gid = $scope.auditcreation_gid;
        $scope.sampleimport_gid = searchObject.sampleimport_gid;
        var sampleimport_gid = $scope.sampleimport_gid;
        $scope.checklistmaster_gid = searchObject.checklistmaster_gid;
        var checklistmaster_gid = $scope.checklistmaster_gid;
        $scope.lspage = searchObject.lspage;
        var lspage = $scope.lspage;
        $scope.lsobservationfill_flag = searchObject.lsobservationfill_flag;


        activate();

        function activate() {
            $scope.hide = 0;
            lockUI();
            var params = {
                auditcreation_gid: auditcreation_gid,
                sampleimport_gid: sampleimport_gid
            };



            //var url = 'api/AtmTrnMyAuditTaskAuditee/closesamplequerysummary';
            //lockUI();
            //SocketService.getparams(url, params).then(function (resp) {
            //    $scope.closequerysample_list = resp.data.closequerysample_list;
            //    unlockUI();
            //});
            var url = 'api/AtmTrnAuditorMaker/GetSampleResponseQuery';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.approval_status = resp.data.approval_status;
                if ($scope.approval_status == 'Completed') {
                    $scope.hide = 1;
                }

                unlockUI();
            });

            $scope.showdiv = true;

            var url = 'api/SystemMaster/GetEmployeelist';
            SocketService.get(url).then(function (resp) {
                $scope.cboemployee_list = resp.data.employeelist;
            });

            var url = 'api/AtmTrnSampling/GetAssignedQuerySummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.SampleAssignedQueryList = resp.data.SampleAssignedQueryList;
            });

            var params = {
                sampleimport_gid: sampleimport_gid
            };
            var url = 'api/AtmTrnSampling/GetSampleName';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.sampleimport_gid = resp.data.sampleimport_gid;
                $scope.txtsample_name = resp.data.sample_name;
            });
            $scope.lspage = $location.search().lspage;
            var lspage = $scope.lspage;

            var url = 'api/AtmTrnAuditorMaker/RaisedsampleQuerySummary';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.samplequerydatalist = resp.data.samplequerydata;

                $scope.close_disabled = false;
                if ((lspage == 'auditeemakeropen' || lspage == 'auditeemakerhold' || lspage == 'auditeemakerclosed' || lspage == 'auditeemakertagged' || lspage == 'auditeemakercompleted' ||
                    lspage == 'auditeecheckeropen' || lspage == 'auditeecheckerpending' || lspage == 'auditeecheckerhold' || lspage == 'auditeecheckerclosed' || lspage == 'auditeecheckertagged' ||
                    lspage == 'auditeecheckercompleted')) {
                    $scope.close_disabled = true;
                }

            });

            if (lspage != "auditormakerOpen" && $scope.lsobservationfill_flag != "Y") {
                $scope.lsobservationfill_flag = "N";
            }
            if ((lspage == 'auditeemakeropen' || lspage == 'auditeemakerhold' || lspage == 'auditeemakerclosed' || lspage == 'auditeemakertagged' || lspage == 'auditeemakercompleted' || lspage == 'auditeemakertagged')) {
                $scope.lsobservationfill_flag = "Y";
            }
        }

        $scope.refresh = function () {
            lockUI();
            activate();
        }



        $scope.btnraisequery = function () {
            $scope.showraisequery = true;
            $scope.showdiv = false;
        }
        $scope.Cancel = function () {
            $scope.txtquery_title = "";
            $scope.txtquery_desc = "";
            $scope.cboqueryto = "";
            $scope.showraisequery = false;
            $scope.showdiv = true;
        }
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
                    $scope.txtquery_title = "";
                    $scope.txtquery_desc = "";
                    $scope.cboqueryto = "";

                    activate();
                    $scope.showdiv = false;
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

        $scope.closesample_query = function (sampleraisequery_gid) {
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
                        sampleraisequery_gid: sampleraisequery_gid,
                        close_remarks: $scope.txtclosequeries
                    }

                    var url = 'api/AtmTrnMyAuditTaskAuditee/PostcloseSamplequery';
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


        $scope.btnulpoad = function (auditcreation_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/documentupload.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.auditcreation_gid = auditcreation_gid;

                var params = {
                    auditcreation_gid: auditcreation_gid
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.downloadall = function (val1, val2) {

                    for (var i = 0; i < val2.length; i++) {
                        //  console.log(array[i]);
                        DownloaddocumentService.Downloaddocument(val1, val2[i]);
                    }

                }

                $scope.upload = function (val, val1, name) {


                    var frm = new FormData(); //docchecklist

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

                    // var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

                    // if (IsValidExtension == false) {
                    //     Notify.alert("File format is not supported..!", {
                    //         status: 'danger',
                    //         pos: 'top-center',
                    //         timeout: 3000
                    //     });
                    //     return false;
                    // }

                    // var auditcreation_gid = $scope.auditcreation_gid;

                    // var item = {
                    //     name: val[0].name,
                    //     file: val[0]
                    // };

                    // var frm = new FormData();
                    // frm.append('fileupload', item.file);
                    // frm.append('file_name', item.name);
                    frm.append('document_name', $scope.documentname);
                    frm.append('document_title', $scope.txtdocument_title);
                    frm.append('auditcreation_gid', $scope.auditcreation_gid);
                    frm.append('project_flag', "Default");

                    $scope.uploadfrm = frm;
                    var url = 'api/AtmTrnSampling/QueryDocUpload';
                    lockUI();
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                        $("#addupload").val('');
                        $scope.txtdocument_title = '';
                        if (resp.data.status == true) {
                            unlockUI();
                            Notify.alert('Document Uploaded Successfully..!!', 'success')

                            var url = "api/AtmTrnSampling/GetDocUploadlist"
                            var param = {
                                auditcreation_gid: auditcreation_gid
                            };
                            SocketService.getparams(url, param).then(function (resp) {
                                $scope.lsfilename = resp.data.filename;
                                $scope.lsfilepath = resp.data.filepath;
                                $scope.DocUploadlogdtl = resp.data.DocUploadlogdtl;
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
                                        status: 'danger',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                }
                                unlockUI();
                            });

                            $modalInstance.close('closed');

                        }
                        else {
                            unlockUI();
                            Notify.alert('File Format Not Supported!')

                        }

                    });

                }

                var param = {
                    auditcreation_gid: auditcreation_gid
                };

                var url = "api/AtmTrnSampling/GetDocUploadlist"

                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.lsfilename = resp.data.filename;
                    $scope.lsfilepath = resp.data.filepath;
                    $scope.DocUploadlogdtl = resp.data.DocUploadlogdtl;
                    unlockUI();
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


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
        }

        //$scope.closesample_query = function (sampleraisequery_gid) {

        //    var params = {
        //        sampleraisequery_gid: sampleraisequery_gid, 
        //        closing_description: $scope.txtdescription

        //    }

        //    var url = 'api/AtmTrnMyAuditTaskAuditee/closequerysample';
        //    lockUI();
        //    SocketService.post(url, params).then(function (resp) {
        //        unlockUI();
        //        if (resp.data.status == true) {
        //            Notify.alert(resp.data.message, {
        //                status: 'success',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
        //            $location.url('app/AtmTrnAudit360View?auditcreation_gid=' + auditcreation_gid + '&checklistmaster_gid=' + checklistmaster_gid + '&sampleimport_gid=' + sampleimport_gid)
        //        }

        //        else {
        //            Notify.alert(resp.data.message, {
        //                status: 'warning',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
        //        }
        //    });
        //}


        $scope.viewresponse_samplequery = function (sampleraisequery_gid) {
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
                    sampleraisequery_gid: sampleraisequery_gid,
                }
                var url = 'api/AtmTrnMyAuditTaskAuditee/GetSampleQuerydetaillist';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.SampleQuerydetaillist = resp.data.SampleQuerydetaillist;
                    unlockUI();
                });

                $scope.replytoquery = function () {
                    var params = {
                        auditcreation_gid: auditcreation_gid,
                        sampleimport_gid: sampleimport_gid,
                        remarks: $scope.txtqueries,
                        sampleraisequery_gid: sampleraisequery_gid,
                        replied_by: lsreplied_by
                    }
                    lockUI();
                    var url = "api/AtmTrnMyAuditTaskAuditee/PostSampleQuerydetail";
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            var url = "api/AtmTrnMyAuditTaskAuditee/GetSampleQuerydetaillist";
                            var param = {
                                sampleraisequery_gid: sampleraisequery_gid
                            };
                            SocketService.getparams(url, param).then(function (resp) {
                                unlockUI();
                                $scope.SampleQuerydetaillist = resp.data.SampleQuerydetaillist;
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
                $scope.sampleraisequery_gid = sampleraisequery_gid;
                $scope.sampleimport_gid = sampleimport_gid;

                var params = {
                    auditcreation_gid: auditcreation_gid,
                    sampleraisequery_gid: sampleraisequery_gid,
                    sampleimport_gid: sampleimport_gid
                }

                $scope.uploaddocument = function (val, val1, name) {
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "Default");


                    var frm = new FormData(); //docchecklist



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

                    // if (IsValidExtension == false) {
                    //     Notify.alert("File format is not supported..!", {
                    //         status: 'danger',
                    //         pos: 'top-center',
                    //         timeout: 3000
                    //     });
                    //     return false;
                    // }

                    // var auditcreation_gid = $scope.auditcreation_gid;
                    // var sampleraisequery_gid = $scope.sampleraisequery_gid;
                    // var sampleimport_gid = $scope.sampleimport_gid;

                    // var item = {
                    //     name: val[0].name,
                    //     file: val[0]
                    // };

                    // var frm = new FormData();
                    // frm.append('fileupload', item.file);
                    // frm.append('file_name', item.name);
                    frm.append('document_name', $scope.documentname);
                    frm.append('document_title', $scope.txtdocument_title);
                    frm.append('auditcreation_gid', $scope.auditcreation_gid);
                    frm.append('sampleraisequery_gid', $scope.sampleraisequery_gid);
                    frm.append('sampleimport_gid', $scope.sampleimport_gid);
                    frm.append('project_flag', "Default");


                    $scope.uploadfrm = frm;
                    var url = 'api/AtmTrnMyAuditTaskAuditee/ResponseDocUpload';
                    lockUI();
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                        $("#addupload").val('');
                        $scope.txtdocument_title = '';
                        if (resp.data.status == true) {
                            unlockUI();
                            Notify.alert('Document Uploaded Successfully..!!', 'success')

                            var param = {
                                sampleraisequery_gid: sampleraisequery_gid
                            };

                            var url = "api/AtmTrnMyAuditTaskAuditee/GetSampleQuerydetaillist"

                            SocketService.getparams(url, param).then(function (resp) {

                                $scope.SampleQuerydetaillist = resp.data.SampleQuerydetaillist;
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
                    $modalInstance.close('closed');
                };

            }
        }

        //$scope.Back = function (val, val1) {
        //    var auditcreation_gid = $scope.auditcreation_gid;
        //    var sampleimport_gid = $scope.sampleimport_gid;
        //    $location.url('app/AtmTrnMyAuditTaskAuditeeView?auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid)
        //}


        $scope.Back = function () {
            var auditcreation_gid = $scope.auditcreation_gid;
            var sampleimport_gid = $scope.sampleimport_gid;
            var checklistmaster_gid = $scope.checklistmaster_gid;
            if (lspage == 'auditormakerOpen') {
                $location.url('app/AtmTrnAuditorMakerView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid + + '&checklistmaster_gid=' + checklistmaster_gid + '&lspage=' + lspage));
            }
            else if (lspage == 'auditormakerHold') {
                $location.url('app/AtmTrnAuditorMakerView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid + + '&checklistmaster_gid=' + checklistmaster_gid + '&lspage=' + lspage));
            }
            else if (lspage == 'auditormakerClosed') {
                $location.url('app/AtmTrnAuditorMakerView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid + + '&checklistmaster_gid=' + checklistmaster_gid + '&lspage=' + lspage));
            }
            else if (lspage == 'auditormakerTagged') {
                $location.url('app/AtmTrnAuditorMakerView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid + + '&checklistmaster_gid=' + checklistmaster_gid + '&lspage=' + lspage));
            }
            else if (lspage == 'auditormakerCompleted') {
                $location.url('app/AtmTrnAuditorMakerView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid + + '&checklistmaster_gid=' + checklistmaster_gid + '&lspage=' + lspage));
            }
            else if (lspage == 'auditorcheckerOpen') {
                $location.url('app/AtmTrnAuditorCheckerView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid + + '&checklistmaster_gid=' + checklistmaster_gid + '&lspage=' + lspage));
            }
            else if (lspage == 'auditorcheckerPending') {
                $location.url('app/AtmTrnAuditorCheckerView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid + + '&checklistmaster_gid=' + checklistmaster_gid + '&lspage=' + lspage));
            }
            else if (lspage == 'auditorcheckerClosed') {
                $location.url('app/AtmTrnAuditorCheckerView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid + + '&checklistmaster_gid=' + checklistmaster_gid + '&lspage=' + lspage));
            }
            else if (lspage == 'auditorcheckerTagged') {
                $location.url('app/AtmTrnAuditorCheckerView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid + + '&checklistmaster_gid=' + checklistmaster_gid + '&lspage=' + lspage));
            }
            else if (lspage == 'auditorcheckerHold') {
                $location.url('app/AtmTrnAuditorCheckerView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid + + '&checklistmaster_gid=' + checklistmaster_gid + '&lspage=' + lspage));
            }
            else if (lspage == 'auditorcheckerCompleted') {
                $location.url('app/AtmTrnAuditorCheckerView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid + + '&checklistmaster_gid=' + checklistmaster_gid + '&lspage=' + lspage));
            }
            else if (lspage == 'auditorapproveropen') {
                $location.url('app/AtmTrnAuditorApproverView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid + + '&checklistmaster_gid=' + checklistmaster_gid + '&lspage=' + lspage));
            }
            else if (lspage == 'auditorapproverPending') {
                $location.url('app/AtmTrnAuditorApproverView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid + + '&checklistmaster_gid=' + checklistmaster_gid + '&lspage=' + lspage));
            }
            else if (lspage == 'auditorapproverHold') {
                $location.url('app/AtmTrnAuditorApproverView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid + + '&checklistmaster_gid=' + checklistmaster_gid + '&lspage=' + lspage));
            }
            else if (lspage == 'auditorapproverClosed') {
                $location.url('app/AtmTrnAuditorApproverView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid + + '&checklistmaster_gid=' + checklistmaster_gid + '&lspage=' + lspage));
            }
            else if (lspage == 'auditorapproverTagged') {
                $location.url('app/AtmTrnAuditorApproverView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid + + '&checklistmaster_gid=' + checklistmaster_gid + '&lspage=' + lspage));
            }
            else if (lspage == 'auditorapproverCompleted') {
                $location.url('app/AtmTrnAuditorApproverView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid + + '&checklistmaster_gid=' + checklistmaster_gid + '&lspage=' + lspage));
            }
            else if (lspage == 'auditeemakeropen') {
                $location.url('app/AtmTrnMyAuditTaskAuditeeMakerView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid + + '&checklistmaster_gid=' + checklistmaster_gid + '&lspage=' + lspage));
            }
            else if (lspage == 'auditeemakerhold') {
                $location.url('app/AtmTrnMyAuditTaskAuditeeMakerView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid + + '&checklistmaster_gid=' + checklistmaster_gid + '&lspage=' + lspage));
            }
            else if (lspage == 'auditeemakerclosed') {
                $location.url('app/AtmTrnMyAuditTaskAuditeeMakerView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid + + '&checklistmaster_gid=' + checklistmaster_gid + '&lspage=' + lspage));
            }
            else if (lspage == 'auditeemakertagged') {
                $location.url('app/AtmTrnTaggedAuditeeView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid + + '&checklistmaster_gid=' + checklistmaster_gid + '&lspage=' + lspage));
            }
            else if (lspage == 'auditeemakercompleted') {
                $location.url('app/AtmTrnMyAuditTaskAuditeeMakerView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid + + '&checklistmaster_gid=' + checklistmaster_gid + '&lspage=' + lspage));
            }
            else if (lspage == 'auditeecheckeropensummary') {
                $location.url('app/AtmTrnMyAuditeeTaskCheckerAuditeeView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid + + '&checklistmaster_gid=' + checklistmaster_gid + '&lspage=' + lspage));
            }
            else if (lspage == 'auditeecheckeropen') {
                $location.url('app/AtmTrnMyAuditTaskAuditeeView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid + + '&checklistmaster_gid=' + checklistmaster_gid + '&lspage=' + lspage));
            }
            else if (lspage == 'auditeecheckerpending') {
                $location.url('app/AtmTrnMyAuditTaskAuditeeView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid + + '&checklistmaster_gid=' + checklistmaster_gid + '&lspage=' + lspage));
            }
            else if (lspage == 'auditeecheckerhold') {
                $location.url('app/AtmTrnMyAuditTaskAuditeeView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid + + '&checklistmaster_gid=' + checklistmaster_gid + '&lspage=' + lspage));
            }
            else if (lspage == 'auditeecheckerclosed') {
                $location.url('app/AtmTrnMyAuditTaskAuditeeView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid + + '&checklistmaster_gid=' + checklistmaster_gid + '&lspage=' + lspage));
            }
            else if (lspage == 'auditeecheckertagged') {
                $location.url('app/AtmTrnMyAuditTaskAuditeeView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid + + '&checklistmaster_gid=' + checklistmaster_gid + '&lspage=' + lspage));
            }
            else if (lspage == 'auditeecheckercompleted') {
                $location.url('app/AtmTrnMyAuditTaskAuditeeView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid + + '&checklistmaster_gid=' + checklistmaster_gid + '&lspage=' + lspage));
            }
            //else if (lspage == 'audit360view') {
            //    //$location.url('app/AtmTrnAudit360View?auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid + + '&checklistmaster_gid=' + checklistmaster_gid + '&lspage=' + lspage);
            //    $location.url('app/AtmTrnAudit360View?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&checklistmaster_gid=' + checklistmaster_gid + '&sampleimport_gid=' + sampleimport_gid + '&lspage=' + lspage))

            //}
            else if (lspage == 'auditclosed') {
                //$location.url('app/AtmTrnAudit360View?auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid + + '&checklistmaster_gid=' + checklistmaster_gid + '&lspage=' + lspage);
                $location.url('app/AtmTrnAudit360View?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&checklistmaster_gid=' + checklistmaster_gid + '&sampleimport_gid=' + sampleimport_gid + '&lspage=' + lspage))

            }
            else if (lspage == 'auditcompleted') {
                //$location.url('app/AtmTrnAudit360View?auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid + + '&checklistmaster_gid=' + checklistmaster_gid + '&lspage=' + lspage);
                $location.url('app/AtmTrnAudit360View?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&checklistmaster_gid=' + checklistmaster_gid + '&sampleimport_gid=' + sampleimport_gid + '&lspage=' + lspage))

            }
            else if (lspage == 'initiateaudit') {
                //$location.url('app/AtmTrnAudit360View?auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid + + '&checklistmaster_gid=' + checklistmaster_gid + '&lspage=' + lspage);
                $location.url('app/AtmTrnAudit360View?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&checklistmaster_gid=' + checklistmaster_gid + '&sampleimport_gid=' + sampleimport_gid + '&lspage=' + lspage))

            }
            else if (lspage == 'auditapproved') {
                //$location.url('app/AtmTrnAudit360View?auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid + + '&checklistmaster_gid=' + checklistmaster_gid + '&lspage=' + lspage);
                $location.url('app/AtmTrnAudit360View?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&checklistmaster_gid=' + checklistmaster_gid + '&sampleimport_gid=' + sampleimport_gid + '&lspage=' + lspage))

            }
            else if (lspage == 'audithold') {
                //$location.url('app/AtmTrnAudit360View?auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid + + '&checklistmaster_gid=' + checklistmaster_gid + '&lspage=' + lspage);
                $location.url('app/AtmTrnAudit360View?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&checklistmaster_gid=' + checklistmaster_gid + '&sampleimport_gid=' + sampleimport_gid + '&lspage=' + lspage))

            }
            else if (lspage == 'auditrejected') {
                //$location.url('app/AtmTrnAudit360View?auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid + + '&checklistmaster_gid=' + checklistmaster_gid + '&lspage=' + lspage);
                $location.url('app/AtmTrnAudit360View?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&checklistmaster_gid=' + checklistmaster_gid + '&sampleimport_gid=' + sampleimport_gid + '&lspage=' + lspage))

            }
            else if (lspage == 'Mypendingapproval') {
                $location.url('app/AtmTrnMyAuditApprover360View?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&checklistmaster_gid=' + checklistmaster_gid + '&sampleimport_gid=' + sampleimport_gid + '&lspage=' + lspage))

                //$location.url('app/AtmTrnMyAuditApprover360View?auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid + + '&checklistmaster_gid=' + checklistmaster_gid + '&lspage=' + lspage);
            }
            else if (lspage == 'Myapprovedaudits') {
                $location.url('app/AtmTrnMyAuditApprover360View?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid + + '&checklistmaster_gid=' + checklistmaster_gid + '&lspage=' + lspage));
            }
            else {

            }

        }
    }
})();