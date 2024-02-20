(function () {
    'use strict';

    angular
        .module('angle')
        .controller('Compliance2taglawyercontroller', Compliance2taglawyercontroller);

    Compliance2taglawyercontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService', 'DownloaddocumentService','cmnfunctionService'];

    function Compliance2taglawyercontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'Compliance2taglawyercontroller';

        activate();


        function activate() {

            lockUI();
            
          
            $scope.lawyer_summary=true;
            var url = window.location.href;
            var relPath = url.split("lstab=");
            $scope.relpath1 = relPath[1];
            console.log($scope.relpath1);
            $scope.lawyerdocumentdetails = false;
            $scope.click = true;
            $scope.seeklawyer = false;
            var url = "api/requestCompliance/compliancemanagement360"
            var param = {
                requestcompliance_gid: localStorage.getItem('requestcompliance_gid')
            };
            $scope.requestcompliance_gid = localStorage.getItem('requestcompliance_gid');
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();

                $scope.requestref_no = resp.data.requestref_no;
                $scope.assign_lawyergid = resp.data.assign_lawyergid;
                $scope.assign_lawyername = resp.data.assign_lawyername;
                $scope.assign_mobileno = resp.data.assign_mobileno;
                $scope.assign_emailaddress = resp.data.assign_emailaddress;
                $scope.assignedlawyer_by = resp.data.assigned_by;
                $scope.seeklawyer_remarks = resp.data.seeklawyer_remarks;
                $scope.assigned_date = resp.data.assigned_date;
                $scope.request_type = resp.data.request_type;
                $scope.request_date = resp.data.request_date;
                $scope.requested_by = resp.data.requested_by;
                $scope.designation_name = resp.data.designation_name;
                $scope.department_name = resp.data.department_name;
                $scope.branch_name = resp.data.branch_name;
                $scope.employee_photo = resp.data.employee_photo;
                $scope.txtremarks = resp.data.remarks;
                $scope.list = resp.data.document_list;
                $scope.seeklawyerdocument = resp.data.uploadseek_list;
                if (resp.data.correctedfile_name != '---') {
                    $scope.updated_download = true;
                }
                console.log(resp.data.requestref_no);
            });
            var url = "api/requestCompliance/querieslist"

            SocketService.getparams(url, param).then(function (resp) {
                $scope.querieslog_list = resp.data.querydetails;
                unlockUI();
            });

            var url = "api/requestCompliance/lawyerList";
            SocketService.getparams(url, param).then(function (resp) {
                $scope.assignlawyerlist = resp.data.assignlawyer;
            });
            var url = "api/requestCompliance/getcorrecteddocument"

            SocketService.getparams(url, param).then(function (resp) {
                $scope.file_list = resp.data.Managecomplianuploaddoc_list;
                unlockUI();
            });
            var url = "api/requestCompliance/Gettaggedlist"

            SocketService.getparams(url, param).then(function (resp) {
                $scope.taggedlist = resp.data.taggedinfo_list;
                $scope.taggeddoc_list = resp.data.taggeddoc_list;
                console.log(resp.data.taggedlawyerdoc_list);
                $scope.taggedlawyerdoc_list = resp.data.taggedlawyerdoc_list;
            });
           

            var url = "api/requestCompliance/LawyerSummary"

            SocketService.getparams(url, param).then(function (resp) {
                $scope.conlawyer_list = resp.data.MdlLawyerSummary;

            });

            var url = "api/requestCompliance/LawyerGroupDtls"

            SocketService.getparams(url, param).then(function (resp) {
                $scope.grouplastconversation = resp.data.lastconversation;
                $scope.groupnewmsg_count = resp.data.newmsg_count;
                $scope.lawyer_count = resp.data.lawyer_count;
                $scope.group_member = resp.data.group_member;
                $scope.totalmsg_count = resp.data.totalmsg_count;
              

            });


            var url = 'api/requestCompliance/tempdelete';
            SocketService.get(url).then(function (resp) {  
            });
        }
        $scope.viewdocument = function (requestcompliance2lawyerdtl_gid, id) {
            var params = {
                requestcompliance2lawyerdtl_gid: requestcompliance2lawyerdtl_gid
            };
            var url = 'api/requestCompliance/Viewuploaddoc_lawyer';
            SocketService.getparams(url, params).then(function (resp) {
                console.log(resp.data.taggeddoc_list);
                $scope.taggedlist[id][requestcompliance2lawyerdtl_gid] = resp.data.taggeddoc_list;

            });
        }
        $scope.viewlawyerdocument = function (requestcompliance2lawyerdtl_gid, id) {
            var params = {
                requestcompliance2lawyerdtl_gid: requestcompliance2lawyerdtl_gid
            };
            var url = 'api/requestCompliance/GetuploadbyLawyer';
            SocketService.getparams(url, params).then(function (resp) {
                console.log(resp.data.taggeddoc_list);
                $scope.taggedlist[id][requestcompliance2lawyerdtl_gid] = resp.data.taggeddoc_list;

            });
        }
       
        $scope.downloads = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //var name = val2.split('.');
            //link.download = name[0];
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.downloadscorrected = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //var name = val2.split('.');
            //link.download = name[0];
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.updateddoc_downloads = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //var name = val2.split('.');
            //link.download = name[0];
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.assignLawyerSubmit = function () {
            var lawyeruser_gid = $scope.cboassignlawyer;
            var lawyeruser_name = $('#assignlawyer :selected').text();


            var params = {
                requestcompliance_gid: localStorage.getItem('requestcompliance_gid'),
                lawyeruser_gid: $scope.cboassignlawyer
            }

            var url = "api/requestCompliance/tmpseekdocumentclear";
            SocketService.get(url).then(function (resp) {

            });

            var modalInstance = $modal.open({
                templateUrl: '/lawyeruploadContent.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.lawyeruser_name = lawyeruser_name;

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.close = function () {
                    $modalInstance.close('closed');
                }

                $scope.uploadseek = function (val, val1, name) {
                    var item = {
                        name: val[0].name,
                        file: val[0]
                    };
                    var frm = new FormData();
                    frm.append('fileupload', item.file);
                    frm.append('file_name', item.name);
                    frm.append('document_name', $scope.documentname);
                    frm.append('project_flag', "Default");
                    $scope.uploadfrm = frm;

                    var url = 'api/requestCompliance/seekLawyerUpload';

                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                        $scope.uploadseek_list = resp.data.uploadseek_list;
                        $("#addupload").val('');

                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        else {
                            Notify.alert('File Format Not Supported!', {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                    });

                }

                $scope.uploadcancel = function (tmpseek_documentgid) {
                    var seekupload = {
                        tmpseek_documentgid: tmpseek_documentgid
                    }

                    var url = 'api/requestCompliance/seekLawyerUploadcancel';
                    SocketService.getparams(url, seekupload).then(function (resp) {
                        $scope.uploadseek_list = resp.data.uploadseek_list;
                    });
                }

                $scope.assignconfirm = function () {
                    lockUI();
                    var seekupload = {
                        requestcompliance_gid: localStorage.getItem('requestcompliance_gid'),
                        lawyeruser_gid: lawyeruser_gid,
                        seeklawyerremarks: $scope.seeklawyerremarks,
                    }

                    var url = "api/requestCompliance/assignLawyer";
                    SocketService.post(url, seekupload).then(function (resp) {
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
                                status: 'Warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        $scope.ok();
                        activate();

                    });
                }

            }


        }

        $scope.requestback = function (relpath1) {
            $location.url('app/complianceManagement?lstab=' + relpath1);

        }

        $scope.delete_correcteddoc = function (val) {
            var params = { uploaddocument_gid: val };
            var url = 'api/requestCompliance/correcteddoc_delete';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    angular.forEach($scope.filename_list, function (value, key) {
                        if (value.document_gid == val) {
                            $scope.filename_list.splice(key, 1);
                        }
                    });
                    Notify.alert('Document Deleted Successfully', {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                else {
                    Notify.alert('Internal Error Occurred', {
                        status: 'Warning',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                activate();
            });
        }

        $scope.upload_document = function (val, val1, uploaddocument_gid) {
            var params = {
                uploaddocument_gid: uploaddocument_gid,
            }
            var modalInstance = $modal.open({
                templateUrl: '/uploadcorrecteddocument.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            //$scope.requestcompliance_gid = requestcompliance_gid;

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.uploaddoc = function (val, val1, name) {
                    var item = {
                        name: val[0].name,
                        file: val[0],
                        uploaddocument_gid: uploaddocument_gid

                    };
                    var params = {
                        uploaddocument_gid: uploaddocument_gid,

                    }

                    var frm = new FormData();
                    frm.append('fileupload', item.file);
                    frm.append('file_name', item.name);
                    frm.append('uploaddocument_gid', uploaddocument_gid);
                    frm.append('remarks', $scope.remarks);
                    frm.append('project_flag', "Default");
                    $scope.uploadfrm = frm;
                }
                $scope.documentupload = function () {

                    var params = {
                        uploadeddocument_type: $scope.document_type,
                        uploadremarks: $scope.txtcorrected_remarks,
                        uploaddocument_gid: uploaddocument_gid
                    }

                    console.log(params);
                    var url = 'api/requestCompliance/uploadCorrectedDoc';
                    lockUI();
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                        $scope.upload_list = resp.data.upload_list;
                        $("#addupload").val('');
                        if (resp.data.status == true) {
                            unlockUI();

                            Notify.alert('Document Uploaded Successfully', 'success')

                        }
                        else {
                            unlockUI();
                            Notify.alert('File Format Not Supported!')

                        }
                        activate();
                    });
                    var url = 'api/requestCompliance/uploadremarrks';
                    lockUI()
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI()
                            activate();
                            $state.go('app.requestCompliance360');

                            $modalInstance.close('closed');
                            //  Notify.alert(' Uploaded Successfully', 'success')
                            activate();
                        }
                        else {
                            unlockUI();
                            //  Notify.alert('Error While updating')
                        }
                        activate();
                    });
                }
            }
        }

        $scope.sendclick = function () {
            var params = {
                requestcompliance_gid: localStorage.getItem('requestcompliance_gid'),
                queries: $scope.txtqueries
            }
            console.log(params);
            lockUI();
            var url = "api/requestCompliance/sendqueries";
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    var url = "api/requestCompliance/querieslist"
                    var param = {
                        requestcompliance_gid: localStorage.getItem('requestcompliance_gid')
                    };
                    SocketService.getparams(url, param).then(function (resp) {
                        $scope.querieslog_list = resp.data.querydetails;
                        unlockUI();
                    });
                    Notify.alert('Query sent Successfully', 'success');
                    $scope.txtqueries = "";
                }
                else {
                    Notify.alert('Error Occurred', 'warning');
                }
            });
        }

        $scope.cancelclick = function () {
            $scope.txtqueries = "";
        }

        $scope.cancelassignLawyer = function () {
            $scope.cboassignlawyer = '';
            $scope.seeklawyer = false;
        }

        $scope.lawyerdocument_details = function () {
            $scope.lawyer_document = true;
            $scope.lawyerdoc = true;
            $scope.click = false;
        }

        $scope.minimizedoc = function () {
            $scope.lawyer_document = false;
            $scope.lawyerdoc = false;
            $scope.click = true;
        }
        $scope.seeklawyeropinion = function () {
            $scope.seeklawyer = true;
        }
        $scope.updatestatus = function (relpath1) {
            var modalInstance = $modal.open({
                templateUrl: '/statusupdation.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                var params =
                    {
                        requestcompliance_gid: localStorage.getItem('requestcompliance_gid'),
                    }
                lockUI();
                var url = "api/requestCompliance/compliancemanagement360"
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.requestref_no = resp.data.requestref_no;
                    $scope.request_type = resp.data.request_type;
                    $scope.request_date = resp.data.request_date;
                    $scope.requested_by = resp.data.requested_by;
                });
                $scope.submit = function () {
                    if ($scope.cbostatus == 'Rejected') {
                        if (($scope.txtrejected_remarks == '') || ($scope.txtrejected_remarks == undefined)) {
                            $scope.mandatoryfields = true;
                        }
                        else {
                            $scope.mandatoryfields = false;
                            var url = 'api/requestCompliance/updatestatus';
                            lockUI();
                            var params = {
                                requestcompliance_gid: localStorage.getItem('requestcompliance_gid'),
                                request_status: $scope.cbostatus,
                                rejected_remarks: $scope.txtrejected_remarks
                            }
                            SocketService.post(url, params).then(function (resp) {
                                unlockUI();
                                if (resp.data.status == true) {

                                    Notify.alert(resp.data.message, {
                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    $location.url('app/complianceManagement?lstab=' + relpath1);
                                    activate()
                                }
                                else {
                                    Notify.alert('File Format Not Supported!', {
                                        status: 'info',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });

                                }
                                activate()
                            });

                            $modalInstance.close('closed');
                        }
                    } else if ($scope.cbostatus == 'Completed') {
                        if (($scope.txtcompleted_remarks == '') || ($scope.txtcompleted_remarks == undefined)) {
                            $scope.mandatoryfields = true;
                        }
                        else {
                            $scope.mandatoryfields = false;
                            var url = 'api/requestCompliance/updatestatus';
                            lockUI();
                            var params = {
                                requestcompliance_gid: localStorage.getItem('requestcompliance_gid'),
                                request_status: $scope.cbostatus,
                                completed_remarks: $scope.txtcompleted_remarks
                            }
                            SocketService.post(url, params).then(function (resp) {
                                unlockUI();
                                if (resp.data.status == true) {

                                    Notify.alert(resp.data.message, {
                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    $location.url('app/complianceManagement?lstab=' + relpath1);
                                    activate()
                                }
                                else {
                                    Notify.alert('File Format Not Supported!', {
                                        status: 'info',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });

                                }
                                activate()
                            });

                            $modalInstance.close('closed');
                        }

                    }
                    else {
                        $scope.mandatoryfields = false;
                        var url = 'api/requestCompliance/updatestatus';
                        lockUI();
                        var params = {
                            requestcompliance_gid: localStorage.getItem('requestcompliance_gid'),
                            request_status: $scope.cbostatus,
                            rejected_remarks: $scope.txtrejected_remarks
                        }
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {

                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $location.url('app/complianceManagement?lstab=' + relpath1);
                                activate()
                            }
                            else {
                                Notify.alert('File Format Not Supported!', {
                                    status: 'info',
                                    pos: 'top-center',
                                    timeout: 3000
                                });

                            }
                            activate()
                        });

                        $modalInstance.close('closed');
                    }
                    /*
                    var url = 'api/requestCompliance/updatestatus';
                    lockUI();
                    var params = {
                        requestcompliance_gid: localStorage.getItem('requestcompliance_gid'),
                        request_status: $scope.cbostatus
                    }

                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $location.url('app/complianceManagement?lstab=' + relpath1);
                            activate()
                        }
                        else {
                            Notify.alert('File Format Not Supported!', {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        activate()
                    });

                    $modalInstance.close('closed');
                    */
                }
            }
        }
        //------Correceted Document upload-----------------//
        $scope.upload = function (val, val1, name) {
            var item = {
                name: val[0].name,
                file: val[0]
            };

            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

            if (IsValidExtension == false) {
                Notify.alert("File format is not supported..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                return false;
            }
            $scope.requestcompliance_gid = localStorage.getItem('requestcompliance_gid');

            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            frm.append('document_type', $scope.document_type)
            frm.append('requestcompliance_gid', $scope.requestcompliance_gid);
            frm.append('project_flag', "Default");
            $scope.uploadfrm = frm;

            var url = 'api/requestCompliance/UploadComplianceCorrected_doc';
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                $scope.UploadDocumentList = resp.data.Managecomplianuploaddoc_list;

                $("#addupload").val('');
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.document_type = '';
                    $scope.showdiv = true;
                    $scope.hidediv = false;
                    Notify.alert(resp.data.message, 'success')
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message)
                }
            });
        }
        //------Delete Correceted Document -----------------//
        $scope.deletedocument = function (val) {
            var params = { document_gid: val };
            console.log(params)
            var url = 'api/requestCompliance/deletecorrecteddo_upload';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    angular.forEach($scope.UploadDocumentList, function (value, key) {
                        if (value.document_gid == val) {
                            $scope.UploadDocumentList.splice(key, 1);
                        }
                    });
                    Notify.alert('Document Deleted Successfully', {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                else {
                    Notify.alert('Internal Error Occurred', {
                        status: 'Warning',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                activate();
            });
        }
        //------Submit upladed Correceted Document-----------------//
        $scope.correcteddoc_submit = function () {
            var params = {
                requestcompliance_gid: localStorage.getItem('requestcompliance_gid')
            }

            var url = 'api/requestCompliance/submitComplianceCorrected_doc';
            lockUI();

            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    var params = {
                        requestcompliance_gid: localStorage.getItem('requestcompliance_gid')
                    }
                    var url = "api/requestCompliance/getcorrecteddocument"

                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.file_list = resp.data.Managecomplianuploaddoc_list;
                        $scope.UploadDocumentList = resp.data.uploaddoc_list;

                        unlockUI();
                    });
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate()
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
        //---------Corrected Document Download------------//
        $scope.downloadsdocument = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //var name = val2.split('.');
            //link.download = name[0];
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
       
        $scope.ShowConversation = function (val, name_initial, newmsg_count, lawyeruser_name) {
            $scope.lawyer_summary = false;
            $scope.requestcompliance2lawyerdtl_gid = val;
            $scope.name_initial = name_initial;
            $scope.newmsg_count = newmsg_count;
            $scope.lawyeruser_name = lawyeruser_name;

            var params = {
                requestcompliance2lawyerdtl_gid: val,
                requestcompliance_gid: localStorage.getItem('requestcompliance_gid'),

            };
           
            var url = 'api/requestCompliance/GetLawyerConversation';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.conversationList = resp.data.MdlConversationSummary;

                }
                else {
                    $scope.conversationList = null;
                }


            });
        }
        $scope.CloseConversation = function () {
            $scope.lawyer_summary = true;

            var params = {
                requestcompliance_gid: $scope.requestcompliance_gid,
                requestcompliance2lawyerdtl_gid: $scope.requestcompliance2lawyerdtl_gid,
                user_flag: 'Lawyer',
            };
            console.log(params);
            var url = 'api/requestCompliance/MsgViewed';
            SocketService.post(url, params).then(function (resp) {

            });
        }
        $scope.LawyerConversation = function () {
            var params = {
                requestcompliance2lawyerdtl_gid: $scope.requestcompliance2lawyerdtl_gid,
                requestcompliance_gid: localStorage.getItem('requestcompliance_gid'),
                msgconversation: $scope.message_content,
                user_flag: 'Admin'
            };
            console.log(params);
            lockUI();
            var url = 'api/requestCompliance/LawyerGroupConversation';
            SocketService.post(url, params).then(function (resp) {

                if (resp.data.status == true) {
                    $scope.message_content = "";
                    $scope.lawyer_summary = true;
                    Notify.alert(resp.data.message, 'success');
                    var params = {
                        requestcompliance2lawyerdtl_gid: $scope.requestcompliance2lawyerdtl_gid,
                        requestcompliance_gid: localStorage.getItem('requestcompliance_gid'),

                    };

                    var url = 'api/requestCompliance/GetLawyerConversation';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $scope.conversationList = resp.data.MdlConversationSummary;

                        }
                        else {

                        }


                    });

                    var params = {
                        requestcompliance_gid: $scope.requestcompliance_gid,
                        requestcompliance2lawyerdtl_gid: $scope.requestcompliance2lawyerdtl_gid,
                        user_flag: 'Lawyer'
                    };
                    
                    var url = 'api/requestCompliance/MsgViewed';
                    SocketService.post(url, params).then(function (resp) {

                    });
                }
                else {
                    Notify.alert(resp.data.message, 'warning');
                }
                activate();
                unlockUI();
            });
        }

        $scope.LawyerindividualConversation = function () {
            var params = {
                requestcompliance2lawyerdtl_gid: $scope.requestcompliance2lawyerdtl_gid,
                requestcompliance_gid: localStorage.getItem('requestcompliance_gid'),
                msgconversation: $scope.message_content,
                user_flag: 'Admin'
            };
            console.log(params);
            lockUI();
            var url = 'api/requestCompliance/LawyerConversation';
            SocketService.post(url, params).then(function (resp) {

                if (resp.data.status == true) {
                    $scope.message_content = "";
                    $scope.lawyer_summary = true;
                    Notify.alert(resp.data.message, 'success');
                    var params = {
                        requestcompliance2lawyerdtl_gid: $scope.requestcompliance2lawyerdtl_gid,
                        requestcompliance_gid: localStorage.getItem('requestcompliance_gid'),

                    };

                    var url = 'api/requestCompliance/GetLawyerConversation';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $scope.conversationList = resp.data.MdlConversationSummary;

                        }
                        else {

                        }


                    });

                    var params = {
                        requestcompliance_gid: $scope.requestcompliance_gid,
                        requestcompliance2lawyerdtl_gid: $scope.requestcompliance2lawyerdtl_gid,
                        user_flag: 'Lawyer'
                    };

                    var url = 'api/requestCompliance/MsgViewed';
                    SocketService.post(url, params).then(function (resp) {

                    });
                }
                else {
                    Notify.alert(resp.data.message, 'warning');
                }
                activate();
                unlockUI();
            });
        }
    }

})();
