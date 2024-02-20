(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstRptCadScannedDeferralStatusController', MstRptCadScannedDeferralStatusController);

    MstRptCadScannedDeferralStatusController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstRptCadScannedDeferralStatusController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstRptCadScannedDeferralStatusController';
        var application_gid = $location.search().application_gid;
        var lspage = $location.search().lspage;
        var credit_gid = $location.search().credit_gid;
        var lstype = $location.search().lstype;
        var lsdocumentcheckdtl_gid = $location.search().lsdocumentcheckdtl_gid;
        var processtypeassign_gid = $location.search().processtypeassign_gid;
        var lspath = $location.search().lspath;
        var lsdeferraltag = $location.search().lsdeferraltag;
        var lscompleted = $location.search().lscompleted;
        var lsscanned = $location.search().lsscanned;

        activate();
        function activate() {
            $scope.errormsg = "";
            $scope.showerrordiv = false;
            vm.calender6 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open6 = true;
            };
            vm.calender7 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open7 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            var param = {
                documentcheckdtl_gid: lsdocumentcheckdtl_gid
            };
            if (lsscanned == "Y")
                var url = 'api/MstScannedDocument/GetInitiatedExtensionorwaiver';
            else
                var url = 'api/MstPhysicalDocument/GetInitiatedExtensionorwaiver';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.intiatextension_list = resp.data.mdlinitiateextendwaiver;
                unlockUI();
            });

            var url = 'api/SystemMaster/GetEmployeelist';
            SocketService.get(url).then(function (resp) {
                $scope.cboemployee_list = resp.data.employeelist;
                unlockUI();
            });

            var param = {
                application_gid: application_gid
            };
            var url = 'api/MstScannedDocument/GetScannedApprovalList';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                if (resp.data != null) {
                    $scope.txtcreditnationalmanager_gid = resp.data.creditnationalmanager_gid;
                    $scope.txtcreditnationalmanager_name = resp.data.creditnationalmanager_name;
                    $scope.txtcreditregionalmanager_gid = resp.data.creditregionalmanager_gid;
                    $scope.txtcreditregionalmanager_name = resp.data.creditregionalmanager_name;

                    $scope.cboapprovalperson = [
                        { employee_gid: $scope.txtcreditnationalmanager_gid, employee_name: $scope.txtcreditnationalmanager_name, approval_person: 'NCM' },
                        { employee_gid: $scope.txtcreditregionalmanager_gid, employee_name: $scope.txtcreditregionalmanager_name, approval_person: 'RCM' }
                    ]
                    unlockUI();
                }
            });



            $scope.hideeditevent = true;
            if (lspage == "CadScannedCompleted" || lscompleted == "CadScannedCompleted" || lspage != "RMDocChecklist") {
                $scope.hideeditevent = false;
            }
            if (lsdeferraltag == "null") {
                $scope.activity_typelist = [
                    { id: 0, activitytype_name: 'Waiver', activity_type: 'Waiver' },
                    { id: 1, activitytype_name: 'Request deferral approval', activity_type: 'Tag to Deferral' },
                    /* { id: 3, activity_type: 'Document Upload' },*/
                    { id: 2, activitytype_name: '', activity_type: '' }
                ]
            }
            else {
                $scope.activity_typelist = [
                    { id: 0, activitytype_name: 'Extension', activity_type: 'Extension' },
                    { id: 1, activitytype_name: 'Waiver', activity_type: 'Extension' },
                    { id: 2, activitytype_name: 'Request deferral approval', activity_type: 'Tag to Deferral' },
                    /* { id: 3, activity_type: 'Document Upload' },*/
                    { id: 3, activitytype_name: '', activity_type: '' }
                ]
            }

        }

        $scope.change_activity = function (cboactivitytype) {
            $scope.errormsg = "";
            $scope.showerrordiv = false;
            $scope.requireddatepicker = false;
            $scope.txttitle = '';
            $scope.txtextendeddue_date = '';
            $scope.txtreason = '';
            /* $scope.cboapprovalperson = '';*/
            $scope.txtcad_remarks = '';
            $scope.txtdue_date = '';
            if ($scope.cboactivitytype.activity_type == 'Extension' && lsdeferraltag == "") {
                $scope.errormsg = "Deferral is not tagged";
                $scope.txttitle = '';
                $scope.txtextendeddue_date = '';
                $scope.txtreason = '';
                /* $scope.cboapprovalperson = '';*/
                $scope.showerrordiv = true;

            }
            else if ($scope.cboactivitytype.activity_type == 'Extension') {
                $scope.extensionshow = true;
                $scope.waivershow = true;
                $scope.closeshow = false;
                $scope.requireddatepicker = true;
                $scope.showdeferraltag = false;
                $scope.showdocumentupload = false;
            }
            else if ($scope.cboactivitytype.activity_type == 'Waiver') {
                $scope.extensionshow = true;
                $scope.waivershow = false;
                $scope.closeshow = false;
                $scope.showdeferraltag = false;
                $scope.showdocumentupload = false;
            }
            else if ($scope.cboactivitytype.activity_type == 'Tag to Deferral') {
                $scope.extensionshow = false;
                $scope.waivershow = false;
                $scope.closeshow = false;
                $scope.showdeferraltag = true;
                $scope.showdocumentupload = false;

            }
            else if ($scope.cboactivitytype.activity_type == 'Document Upload') {
                $scope.extensionshow = false;
                $scope.waivershow = false;
                $scope.closeshow = false;
                $scope.showdeferraltag = false;
                $scope.showdocumentupload = true;
            }
            else if ($scope.cboactivitytype.activity_type == 'Close') {
                $scope.closeshow = true;
                $scope.waivershow = false;
                $scope.extensionshow = false;
                $scope.showdeferraltag = false;
                $scope.showdocumentupload = false;
            }
            else {
                $scope.extensionshow = false;
                $scope.waivershow = false;
                $scope.closeshow = false;
                $scope.showdeferraltag = false;
                $scope.showdocumentupload = false;
            }
        }

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
            }

            frm.append('document_title', documenttype_name);
            frm.append('RMupload', 'Y');
            frm.append('documentcheckdtl_gid', documentcheckdtl_gid);
            frm.append('application_gid', application_gid);
            frm.append('credit_gid', credit_gid);
            frm.append('covenant_type', covenant_type);
            frm.append('signeddocument_flag', signeddocument_flag);

            $scope.uploadfrm = frm;
            if ($scope.uploadfrm != undefined) {
                var url = 'api/MstScannedDocument/ScannedDocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $("#scannedfile").val('');
                    var params = {
                        documentcheckdtl_gid: documentcheckdtl_gid,
                        signeddocument_flag: signeddocument_flag
                    }
                    var url = 'api/MstScannedDocument/GetRMScannedDocument';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.scannedmyuploaddocumentlist = resp.data.scanneduploaddocument;
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

        $scope.defdoc_delete = function (scanneddocument_gid) {
            lockUI();
            var params = {
                scanneddocument_gid: scanneddocument_gid
            }
            var url = 'api/MstScannedDocument/cancelscanneduploaddocument';
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
                    documentcheckdtl_gid: documentcheckdtl_gid,
                    signeddocument_flag: 'N'
                }
                var url = 'api/MstScannedDocument/GetRMScannedDocument';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.scannedmyuploaddocumentlist = resp.data.scanneduploaddocument;

                });
            });
        }

        $scope.tagdoc_submit = function () {
            var params = {
                documentcheckdtl_gid: documentcheckdtl_gid,
                application_gid: application_gid,
                credit_gid: credit_gid,
                documentseverity_gid: $scope.documentseverity_gid,
                documentseverity_name: $scope.txtdocumentseverity_name,
                tagged_to: $scope.cbotaggedto,
                due_date: $scope.txtdue_date,
                cad_remarks: $scope.txtcad_remarks,
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

        $scope.intiatextension_submit = function () {
            if ($scope.cboactivitytype.activity_type === "Extension" && $scope.txtextendeddue_date == "") {
                Notify.alert('Kindly Choose Extended Due Date !', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
            }
            else if ($scope.cboactivitytype.activity_type === "Waiver" && $scope.cboapprovalperson.length == undefined) {
                Notify.alert('Kindly select Approval person !', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
            }
            //else if ($scope.cboactivitytype.activity_type == "Tag to Deferral") { 
            //    var params = {
            //        documentcheckdtl_gid: lsdocumentcheckdtl_gid,
            //        application_gid: application_gid,
            //        credit_gid: credit_gid,
            //        documentseverity_gid: $scope.documentseverity_gid,
            //        documentseverity_name: $scope.txtdocumentseverity_name,
            //        tagged_to: $scope.cbotaggedto,
            //        due_date: $scope.txtdue_date,
            //        cad_remarks: $scope.txtcad_remarks, 
            //        scanneddoc_flag: lsscanned
            //    }
            //    var url = 'api/MstScannedDocument/PostUpdateTagToDefStatus';
            //    lockUI();
            //    SocketService.post(url, params).then(function (resp) {
            //        unlockUI();
            //        if (resp.data.status == true) {
            //            Notify.alert(resp.data.message, {
            //                status: 'success',
            //                pos: 'top-center',
            //                timeout: 3000
            //            });
            //            activate();
            //            $scope.Cancel();
            //        }
            //        else {
            //            Notify.alert(resp.data.message, {
            //                status: 'warning',
            //                pos: 'top-center',
            //                timeout: 3000
            //            });
            //            activate();
            //        }
            //    }); 
            //}
            else {

                var approval_status = $scope.cboapprovalperson == undefined ? 'No Approval' : 'Pending';
                var params = {
                    activity_type: $scope.cboactivitytype.activity_type,
                    activity_title: $scope.txttitle,
                    extendeddue_date: $scope.txtextendeddue_date,
                    reason: $scope.txtreason,
                    application_gid: application_gid,
                    documentcheckdtl_gid: lsdocumentcheckdtl_gid,
                    mdlapproval: $scope.cboapprovalperson,
                    approval_status: approval_status,
                    credit_gid: credit_gid,
                    documentseverity_gid: $scope.documentseverity_gid,
                    documentseverity_name: $scope.txtdocumentseverity_name,
                    due_date: $scope.txtdue_date,
                    scanneddoc_flag: lsscanned
                }
                if (lsscanned == "Y")
                    var url = 'api/MstScannedDocument/PostInitiateExtensionorwaiver';
                else
                    var url = 'api/MstPhysicalDocument/PostPhysicalInitiateExtensionorwaiver';
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
                        $scope.Cancel();
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                });
            }
        }

        $scope.Cancel = function () {
            $scope.cboactivitytype = '';
            $scope.txttitle = '';
            $scope.txtextendeddue_date = '';
            $scope.txtreason = '';
            $scope.cboapprovalperson = '';
            $scope.txtcad_remarks = '';
            $scope.txtdue_date = '';
            //$scope.txtcreditnationalmanager_gid = '';
            //$scope.txtcreditnationalmanager_name = '';
            //$scope.txtcreditregionalmanager_gid = '';
            //$scope.txtcreditregionalmanager_name = '';
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

        $scope.Back = function () {
            if (lspage == "RMDocChecklist") {
                $location.url('app/MstRMDeferralDtlsView?application_gid=' + application_gid + '&credit_gid=' + credit_gid + '&lspage=' + lspage + '&lstype=' + lstype);
            }
            else {
                $location.url('app/MstRptCadScannedDocchecklist?application_gid=' + application_gid + '&credit_gid=' + credit_gid + '&lspage=' + lspage + '&lstype=' + lstype + '&processtypeassign_gid=' + processtypeassign_gid + '&lspath=' + lspath);
            }
        }


    }
})();
