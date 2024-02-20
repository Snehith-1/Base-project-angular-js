(function () {
    'use strict';

    angular
        .module('angle')
        .controller('documentComplianceViewcontroller', documentComplianceViewcontroller);

    documentComplianceViewcontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];
    function documentComplianceViewcontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'documentComplianceViewcontroller';

        activate();

        function activate() {
            $scope.lawyer_summary = true;

            var url = "api/documentCompliance/compliancemanagement360"
            var param = {
                requestcompliance_gid: localStorage.getItem('requestcompliance_gid'),
               
            };
            $scope.requestcompliance_gid = localStorage.getItem('requestcompliance_gid');
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.requestref_no = resp.data.requestref_no;
                $scope.request_type = resp.data.request_type;
              //  $scope.deadline_date = resp.data.deadline_date;
                $scope.assigned_date = resp.data.assigned_date;
                $scope.assigned_by = resp.data.assigned_by;
                $scope.seeklawyer_remarks = resp.data.seeklawyer_remarks;
                $scope.txtremarks = resp.data.remarks;
                $scope.document_list = resp.data.document_list;
                console.log(resp.data.document_list);
                
                if (resp.data.correctedfile_name != '---') {
                    $scope.updated_download = true;
                }
            });
            var param = {
                requestcompliance2lawyerdtl_gid: localStorage.getItem('requestcompliance2lawyerdtl_gid')
            };
            $scope.requestcompliance2lawyerdtl_gid = localStorage.getItem('requestcompliance2lawyerdtl_gid');
            var url = "api/documentCompliance/getuploaddoc2lawyer"
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.taggeddoc_list = resp.data.taggeddoc_list;
                console.log(resp.data.taggeddoc_list)
            });

          
            var param = {
                requestcompliance_gid: $scope.requestcompliance_gid
            }

            var url = "api/requestCompliance/LawyerGroupDtls"

            SocketService.getparams(url, param).then(function (resp) {
                $scope.grouplastconversation = resp.data.lastconversation;
                $scope.groupnewmsg_count = resp.data.newmsg_count;
                console.log('group',resp.data.newmsg_count);
                $scope.lawyer_count = resp.data.lawyer_count;
                $scope.group_member = resp.data.group_member;
                $scope.totalmsg_count = resp.data.totalmsg_count;
              

            });

            var param = {
                requestcompliance2lawyerdtl_gid: $scope.requestcompliance2lawyerdtl_gid
            }

            var url = "api/documentCompliance/LegalDtls"

            SocketService.getparams(url, param).then(function (resp) {
                $scope.lastconversation = resp.data.lastconversation;
                $scope.newmsg_count = resp.data.newmsg_count;
              
                console.log($scope.newmsg_count);
            });

        }

        $scope.upload_document = function (val, val1, uploaddocument_gid) {
            var params = {
                uploaddocument_gid: uploaddocument_gid,
            }
            console.log(uploaddocument_gid);
            var modalInstance = $modal.open({
                templateUrl: '/uploadcorrecteddocument.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
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
                    $scope.uploadfrm = frm;

                    console.log(uploaddocument_gid);


                }
                $scope.documentupload = function () {

                    var params = {
                        uploaddocument_type: $scope.document_type,
                        uploadremarks: $scope.txtcorrected_remarks,
                        uploaddocument_gid: uploaddocument_gid
                    }
                    console.log(params);
                    console.log($scope.uploadfrm);
                    var url = 'api/documentCompliance/uploadCorrectedDoc';
                    lockUI();
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                        $scope.upload_list = resp.data.upload_list;
                        console.log(resp.data.upload_list);
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
                    var url = 'api/documentCompliance/uploadremarrks';
                    lockUI()
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI()
                            activate();
                            $state.go('app.documentComplianceView');

                            $modalInstance.close('closed');
                           
                            activate();
                        }
                        else {
                            unlockUI();
                            //  Notify.alert('Error While updating'
                        }
                        activate();
                    });
                }
            }
        }
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
            $scope.uploadfrm = frm;

            console.log(uploaddocument_gid);


        }
       
        $scope.downloads = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("StoryboardAPI");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            console.log(str);
            var link = document.createElement("a");
            var name = val2.split('.');
            link.download = name[0];
            var uri = str;
            link.href = uri;
            link.click();
        }
        $scope.downloadscorrected = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("StoryboardAPI");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            var name = val2.split('.');
            link.download = name[0];
            var uri = str;
            link.href = uri;
            link.click();
        }
        $scope.back = function () {
            $state.go('app.documentCompliance');
        }
        $scope.upload = function (val, val1, name) {
            var item = {
                name: val[0].name,
                file: val[0]
            };
            $scope.requestcompliance_gid = localStorage.getItem('requestcompliance_gid');

            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            frm.append('document_type', $scope.document_type)
            frm.append('requestcompliance_gid', $scope.requestcompliance_gid);
            $scope.uploadfrm = frm;

            var url = 'api/documentCompliance/uploadlawyerCorrected_doc';
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                $scope.lawyerupload_list = resp.data.upload_list;
                console.log(resp.data.upload_list);
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
        $scope.deletedocument = function (val) {
            var params = { lawyerdocument_gid: val };
            console.log(params)
            var url = 'api/documentCompliance/deletecorrecteddo_upload';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    angular.forEach($scope.lawyerupload_list, function (value, key) {
                        if (value.lawyerdocument_gid == val) {
                            $scope.lawyerupload_list.splice(key, 1);
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
        $scope.correcteddoc_submit = function () {
            var params = {
                requestcompliance_gid: localStorage.getItem('requestcompliance_gid'),
                requestcompliance2lawyerdtl_gid: localStorage.getItem('requestcompliance2lawyerdtl_gid')
            }

            var url = 'api/documentCompliance/submitComplianceCorrected_doc';
            lockUI();

            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    var params = {
                        requestcompliance_gid: localStorage.getItem('requestcompliance_gid'),
                        requestcompliance2lawyerdtl_gid: localStorage.getItem('requestcompliance2lawyerdtl_gid')
                    }
                    console.log(params);
                    var url = "api/documentCompliance/getcorrecteddocument"

                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.correcteddoc_list = resp.data.upload_list;
                        $scope.lawyerupload_list = resp.data.document_list;

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
                    Notify.alert('File Format Not Supported!', {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }

            });
        }
        $scope.updatestatus = function (relpath1) {
            var modalInstance = $modal.open({
                templateUrl: '/statusupdation.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.mandatoryfields = false;
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                var params =
                    {
                        requestcompliance_gid: localStorage.getItem('requestcompliance_gid')
                    }
                lockUI();
                var url = "api/documentCompliance/compliancemanagement360"
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.requestref_no = resp.data.requestref_no;
                    $scope.request_type = resp.data.request_type;
                    $scope.request_date = resp.data.assigned_date;
                    $scope.requested_by = resp.data.assigned_by;
                });
                $scope.submit = function () {
                  
                    if (($scope.cbostatus == 'Rejected')) {
                        if (($scope.txtrejected_remarks == '') || ($scope.txtrejected_remarks == undefined)) {
                            $scope.mandatoryfields = true;
                        }
                        else {
                            $scope.mandatoryfields = false;
                            var url = 'api/documentCompliance/updatestatus';
                            lockUI();
                            var params = {
                                requestcompliance2lawyerdtl_gid: localStorage.getItem('requestcompliance2lawyerdtl_gid'),
                                request_status: $scope.cbostatus,
                                rejected_remarks: $scope.txtrejected_remarks
                            }
                            console.log(params);
                            SocketService.post(url, params).then(function (resp) {
                                unlockUI();
                                if (resp.data.status == true) {

                                    Notify.alert(resp.data.message, {
                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    $state.go('app.documentCompliance');
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
                        var url = 'api/documentCompliance/updatestatus';
                        lockUI();
                        var params = {
                            requestcompliance2lawyerdtl_gid: localStorage.getItem('requestcompliance2lawyerdtl_gid'),
                            request_status: $scope.cbostatus,
                            rejected_remarks: $scope.txtrejected_remarks
                        }
                        console.log(params);
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {

                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $state.go('app.documentCompliance');
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
            }
        }

        $scope.ShowConversation = function (val, name_initial, newmsg_count, lawyeruser_name) {
            $scope.primary_gid = val;
            $scope.name_initial = name_initial;
            $scope.newmsg_count = newmsg_count;
            $scope.lawyeruser_name = lawyeruser_name;

            var params = {
                requestcompliance2lawyerdtl_gid: val,
                requestcompliance_gid: $scope.requestcompliance_gid,

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
            $scope.lawyer_summary = false;
        }
        $scope.CloseConversation = function () {
            $scope.lawyer_summary = true;

            var params = {
                requestcompliance_gid: $scope.requestcompliance_gid,
                requestcompliance2lawyerdtl_gid: $scope.requestcompliance2lawyerdtl_gid,
                user_flag:'Admin'
            };
            console.log(params);
            var url = 'api/requestCompliance/MsgViewed';
            SocketService.post(url, params).then(function (resp) {

            });
        }
        $scope.LegalConversation = function () {
         
            var params = {
                requestcompliance2lawyerdtl_gid: $scope.primary_gid,
                requestcompliance_gid: $scope.requestcompliance_gid,
                msgconversation: $scope.message_content,
                user_flag: 'Lawyer'
            };
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
                        user_flag: 'Admin'
                    };
                    console.log(params);
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
