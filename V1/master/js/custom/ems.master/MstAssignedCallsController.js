(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstAssignedCallsController', MstAssignedCallsController);

    MstAssignedCallsController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','DownloaddocumentService','cmnfunctionService'];

    function MstAssignedCallsController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstAssignedCallsController';
       
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var inboundcall_gid = searchObject.inboundcall_gid;
        activate();
        lockUI();
        function activate() {
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            var params = {
                inboundcall_gid: inboundcall_gid
            }

            var url = 'api/TeleCalling/GetIBCallAssignedView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtticket_refid = resp.data.ticket_refid,
                $scope.txtentity_name = resp.data.entity_name,
                $scope.txtsourceofcontact_name = resp.data.sourceofcontact_name,
                $scope.txtcallreceivednumber_name = resp.data.callreceivednumber_name,
                $scope.txtcustomer_type = resp.data.customer_type,
                $scope.txtcallreceived_date = resp.data.callreceived_date,
                $scope.txtcaller_name = resp.data.caller_name,
                $scope.txtinternalreference_name = resp.data.internalreference_name,
                $scope.txtcallerassociate_company = resp.data.callerassociate_company,
                $scope.txtoffice_landlineno = resp.data.office_landlineno,
                $scope.txtcalltype_name = resp.data.calltype_name,
                $scope.txtfunction_name = resp.data.function_name,
                $scope.txtfunction_remarks = resp.data.function_remarks,
                $scope.txttat_hours = resp.data.tat_hours,
                $scope.txttat_days = resp.data.tat_days,
                $scope.txttat_date = resp.data.tat_date,
                $scope.txtrequirement = resp.data.requirement,
                $scope.txtenquiry_description = resp.data.enquiry_description,
                $scope.txtcallclosure_status = resp.data.callclosure_status,
                $scope.txtassignemployee_name = resp.data.assignemployee_name,
                $scope.txtassign_date = resp.data.assign_date,
                $scope.txttagemployee_name = resp.data.tagemployee_name,
                $scope.txtassignclosure_remarks = resp.data.assignclosure_remarks,
                $scope.ibcalladdress_list = resp.data.ibcalladdress_list;
                $scope.ibcallfollowup_list = resp.data.ibcallfollowup_list;
                $scope.ibcalltaggedmember_list = resp.data.ibcalltaggedmember_list;
                $scope.txtacknowledge_date = resp.data.acknowledge_date,
                unlockUI();
            });
            var url = 'api/TeleCalling/IBCallRecordingDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lufilename = resp.data.filename;
                $scope.lufilepath = resp.data.filepath;
                $scope.Uploadcallrecordingdocument_list = resp.data.callproofupload_list;
            });
            var url = 'api/TeleCalling/IBCallProofDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lpfilename = resp.data.filename;
                $scope.lpfilepath = resp.data.filepath;
                $scope.Uploadcallproofdocument_list = resp.data.callproofupload_list;
            });
            var url = 'api/TeleCalling/IBCallDocTempClear';
            SocketService.get(url).then(function () {
            });
        }
        $scope.Back = function () {
            $location.url("app/MstWorkInprogressCallSummary");
        }

        $scope.changeclosurestatus = function (cboclosurestatus) {
            if (cboclosurestatus == 'Extend Follow Up') {
                $scope.followup_show = true;
            }
            else {
                $scope.followup_show = false;
            }
            if (cboclosurestatus == 'Complete') {
                $scope.completed_show = true;
            }
            else {
                $scope.completed_show = false;
            }
        }

        $scope.Submit = function () {
            if (($scope.cboclosurestatus == 'Extend Follow Up') && ($scope.txtfollowup_date == '' || $scope.txtfollowup_date == null || $scope.txtfollowup_time == null || $scope.txtfollowup_time == null || $scope.txtfollowup_remarks == null || $scope.txtfollowup_remarks == null)) {
                Notify.alert('Kindly Fill Follow Up Details', 'warning')
            }
            else if (($scope.cboclosurestatus == 'Complete') && ($scope.completedremarks == '' || $scope.completedremarks == null)) {
                Notify.alert('Kindly Fill Completed Remarks', 'warning')
            }
            else {
                var params = {
                    inboundcall_gid: inboundcall_gid,
                    followup_date: $scope.txtfollowup_date,
                    followup_time: $scope.txtfollowup_time,
                    completed_remarks: $scope.completedremarks,
                    closure_status: $scope.cboclosurestatus,
                    followup_remarks: $scope.txtfollowup_remarks
                }
                var url = 'api/TeleCalling/PostCompletedCall';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $location.url("app/MstWorkInprogressCallSummary");
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

                 $scope.documentviewerproof = function (val1, val2) {
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

        $scope.uploadcall_recording = function (val, val1, name) {

            var IsValidExtension =cmnfunctionService.fnCheckValidDocType(val[0].name, "BD")
            
            
            if (IsValidExtension == false) {
                $("#fileupload").val('');
                Notify.alert("File format is not supported..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                return false;
            }
            else{
            var frm = new FormData();

            for (var i = 0; i < val.length; i++) {
                var item = {
                    name: val[i].name,
                    file: val[i]
                };
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
            }
            frm.append('document_title', $scope.txtdocument_title);
                frm.append('inboundcall_gid', inboundcall_gid);
                frm.append('project_flag', "BD");

                $scope.uploadfrm = frm;
        
    }
}

        $scope.uploadcallrecording_doc = function () {

            if ($scope.uploadfrm != undefined) {
                lockUI();
                var url = 'api/TeleCalling/CallRecordingDocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $("#file").val('');

                    $scope.txtdocument_title = "";
                    $scope.uploadfrm = undefined;
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
                    callrecording_list();
                    unlockUI();
                });
            }
            else {
                Notify.alert('Please select a file', 'warning')
               
            }
            $scope.txtdocument_title = '';
        }

        function callrecording_list() {
            var params = {
                inboundcall_gid: inboundcall_gid
            }
            var url = 'api/TeleCalling/IBCallRecordingDocumentTmpList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Uploadcallrecordingdocument_list = resp.data.callproofupload_list;
                $scope.lufilename = resp.data.filename;
                $scope.lufilepath = resp.data.filepath;           
            });
        }
        $scope.rec_cancel = function (ibcallrecordingocupload_gid) {

            var params = {
                ibcallrecordingocupload_gid: ibcallrecordingocupload_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Photo ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();
                    var url = 'api/TeleCalling/IBCallRecordingDocumentDelete';
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
                        callrecording_list();
                        unlockUI();
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }
            });
        }

        //$scope.rec_downloads = function (val1, val2) {
        //    var phyPath = val1;
        //    var relPath = phyPath.split("StoryboardAPI");
        //    var relpath1 = relPath[1].replace("\\", "/");
        //    var hosts = window.location.host;
        //    var prefix = location.protocol + "//";
        //    var str = prefix.concat(hosts, relpath1);
        //    var link = document.createElement("a");
        //    link.download = val2;
        //    var uri = str;
        //    link.href = uri;
        //    link.click();
        //}
        
        $scope.rec_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }


        $scope.proofdocument_upload = function (val, val1, name) {
            var IsValidExtension =cmnfunctionService.fnCheckValidDocType(val[0].name, "BD")
            
            
            if (IsValidExtension == false) {
                $("#fileupload").val('');
                Notify.alert("File format is not supported..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                return false;
            }
            else{
           
            var frm = new FormData();

            for (var i = 0; i < val.length; i++) {
                var item = {
                    name: val[i].name,
                    file: val[i]
                };
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
            }
            frm.append('document_title', $scope.txtdocumentcallproof_title);
           frm.append('inboundcall_gid',inboundcall_gid);
                frm.append('project_flag', "BD");

                $scope.uploadfrm = frm;
        
        }
    }

        $scope.uploadcallproof_doc = function () {

            if ($scope.uploadfrm != undefined) {
                lockUI();
                var url = 'api/TeleCalling/CallProofDocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $("#fileupload").val('');
                  
                    $scope.txtdocumentcallproof_title = "";
                    $scope.uploadfrm = undefined;
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
                    proofdocument_list();
                    unlockUI();
                });
            }
            else {
                Notify.alert('Please select a file', 'warning')
            }
            $scope.txtdocumentcallproof_title = '';
        }

        function proofdocument_list() {
            var params = {
                inboundcall_gid: inboundcall_gid
            }
            var url = 'api/TeleCalling/IBCallProofDocumentTmpList';
            SocketService.getparams(url, params).then(function (resp) {
               $scope.lpfilename = resp.data.filename;
                $scope.lpfilepath = resp.data.filepath;
                $scope.Uploadcallproofdocument_list = resp.data.callproofupload_list;
            });
        }

        $scope.downloadall = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.downloadall1 = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }


        $scope.recproof_cancel = function (ibcallproofdocupload_gid) {

            var params = {
                ibcallproofdocupload_gid: ibcallproofdocupload_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Photo ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();
                    var url = 'api/TeleCalling/IBCallProofDocumentDelete';
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
                        proofdocument_list();
                        unlockUI();
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }
            });
        }

        $scope.recproof_downloads = function (val1, val2) {

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
})();
