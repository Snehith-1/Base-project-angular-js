(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmTrnRaiseQueryHistoryController', AtmTrnRaiseQueryHistoryController);

    AtmTrnRaiseQueryHistoryController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', '$sce', '$anchorScroll', 'DownloaddocumentService', 'cmnfunctionService'];

    function AtmTrnRaiseQueryHistoryController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, $sce, $anchorScroll, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmTrnRaiseQueryHistoryController';
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
            lockUI();
            var params = {
                auditcreation_gid: auditcreation_gid,
            };   
            
            var url = 'api/AtmTrnAuditorMaker/RaiseQueryHistorySummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.raisequeryhistory = resp.data.raisequeryhistory;
                unlockUI();
            });                     
                     
        }       
               
        $scope.tagsample = function (sampleimport_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/taguser.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    sampleimport_gid: sampleimport_gid
                };
                var url = 'api/AtmTrnAuditorMaker/AssignedTagUser';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.SampleAssignedTag = resp.data.SampleAssignedTag;
                });

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

            }
        }


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
                    var sampleraisequery_gid = $scope.sampleraisequery_gid;
                    var sampleimport_gid = $scope.sampleimport_gid;

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
                    frm.append('sampleraisequery_gid', $scope.sampleraisequery_gid);
                    frm.append('sampleimport_gid', $scope.sampleimport_gid);


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
            if (lspage == 'auditeecheckerpending') {
                $location.url('app/AtmTrnAuditorMakerView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid + '&lspage=' + lspage));
            }          
        }
        $scope.back = function () {          
          
            $location.url('app/AtmTrnMyAuditTaskAuditeeView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid + '&lspage=auditeecheckerpending'));
            
        }
    }
})();