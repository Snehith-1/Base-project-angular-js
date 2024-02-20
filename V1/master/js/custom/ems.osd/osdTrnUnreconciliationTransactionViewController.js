// JavaScript source code
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('osdTrnUnreconciliationTransactionViewController', osdTrnUnreconciliationTransactionViewController);

    osdTrnUnreconciliationTransactionViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'cmnfunctionService', 'DownloaddocumentService'];

    function osdTrnUnreconciliationTransactionViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService, DownloaddocumentService) {

        var vm = this;
        vm.title = 'osdTrnUnreconciliationTransactionViewController';
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var banktransc_gid = searchObject.lsbanktransc_gid;
        var lsbankalert2allocated_gid = searchObject.lsbankalert2allocated_gid;
        var lstab = searchObject.lstab;
        activate();
        function activate() {


            var url = 'api/UnreconciliationManagement/GetAllocatedDetail';
            var param = {
                banktransc_gid: banktransc_gid

            }

            SocketService.getparams(url, param).then(function (resp) {
                $scope.lblbanktransc_gid = banktransc_gid;
                $scope.lblbank_name = resp.data.bank_name;
                $scope.lblcustomer_refno = resp.data.custref_no;
                $scope.lblbranch_name = resp.data.branch_name;
                $scope.lblcr_dr = resp.data.cr_dr;
                $scope.lbltransc_value = resp.data.transact_val;
                $scope.lbltransact_particulars = resp.data.transact_particulars;
                $scope.lbltransc_balance = resp.data.transc_balance;
                $scope.lblacc_no = resp.data.acc_no;
                $scope.lblallocated_status = resp.data.allocated_status;
                $scope.lbltrn_date = resp.data.trn_date;
                $scope.lblrm_status = resp.data.rm_status;
                $scope.lblrm_remarks = resp.data.rm_remarks;
                $scope.lblchq_no = resp.data.chq_no;
                $scope.lblremarks = resp.data.remarks;


            });
           
            var url = 'api/employee/Employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });
            var param = {
                banktransc_gid: banktransc_gid,
            }

            var url = 'api/OsdTrnBankAlert/GetUnReconUploadedDoc';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.uploaddocument = resp.data.MdlDocDetails;
                $scope.filename = resp.data.filename;
                $scope.filepath = resp.data.filepath;

            });

            var url = 'api/UnreconciliationManagement/GetAssignedHistory';
            var param = {
                banktransc_gid: banktransc_gid
            }
            SocketService.getparams(url, param).then(function (resp) {
                $scope.assignedlist = resp.data.assignedlist;
            });

            var url = 'api/UnreconciliationManagement/GetTransferredHistory';
            var param = {
                banktransc_gid: banktransc_gid
            }
            SocketService.getparams(url, param).then(function (resp) {
                $scope.transferlist = resp.data.transferlist;
            });

        }

        $scope.UploadDocCancel = function (id) {
            var params = {
                fileupload_gid: id
            }
            var url = 'api/OsdTrnBankAlert/DeleteUnReconUploadedDoc';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    var param = {
                        banktransc_gid: banktransc_gid

                    } 
                    var url = 'api/OsdTrnBankAlert/GetUnReconUploadedDoc';
                    SocketService.getparams(url, param).then(function (resp) {
                        $scope.uploaddocument = resp.data.MdlDocDetails;
                        $scope.filename = resp.data.filename;
                        $scope.filepath = resp.data.filepath;

                    });
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }
        $scope.download = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.Udownloadall = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.uploadattachment = function () {
            var fi = document.getElementById('file');
            if (fi.files.length > 0) {
                lockUI();

                var frm = new FormData();
                for (var i = 0; i < fi.files.length; i++) {

                    frm.append(fi.files[i].name, fi.files[i]);
                    
                    var fname = fi.files.item(i).name;
                    var fsize = fi.files.item(i).size;
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(fi.files[i].name, "documentformatonly");
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
                frm.append('project_flag', "documentformatonly");
                frm.append('banktransc_gid', banktransc_gid);
               /* frm.append('document_name', $scope.test_document);*/
                /*frm.append('fileupload', item.file);*/
                frm.append('document_name',fname);
        //        $scope.uploadfrm = frm;
                $scope.uploadfrm = frm;

                var url = 'api/OsdTrnBankAlert/UnReconDocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $("#file").val('');

                    unlockUI();
                    if (resp.data.status == true) {
                        var param = {
                            banktransc_gid: banktransc_gid

                        }   
                       
                        var url = 'api/OsdTrnBankAlert/GetUnReconUploadedDoc';
                        SocketService.getparams(url, param).then(function (resp) {
                            $scope.uploaddocument = resp.data.MdlDocDetails;
                            $scope.lblfilename = resp.data.filename;
                            $scope.lblfilepath = resp.data.filepath;
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
                    }
                    unlockUI();
                });
            }
            else {
                Notify.alert('Please select a file.', 'warning')
            }
        }
        $scope.Submit = function () {

            var params =
            {
                banktransc_gid: banktransc_gid,
                cbounreconciliation_status: $scope.cborm_status,
                /*brs_status: $scope.brs_status,*/
                updation_remarks: $scope.txtremarks
               
            }
            var url = 'api/OsdTrnBankAlert/PostUnconciliationStatusUpdation';
            lockUI();
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
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                $location.url('app/osdBamAllocatedToRM');
            });

        }

        $scope.Back = function () {
            $location.url("app/osdBamAllocatedToRM?lstab=" + lstab);
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

        $scope.Sendbacktobrs = function () {
            var modalInstance = $modal.open({
                templateUrl: '/sentbacktobrs.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {
                    var params = {
                        sendback_reason: $scope.txtsendbackreason,
                        banktransc_gid: banktransc_gid,
                        bankalert2allocated_gid: lsbankalert2allocated_gid
                    }
                    var url = 'api/OsdTrnBankAlert/PostRMSendback';
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
                        }
                    });

                    $modalInstance.close('closed');
                    $location.url('app/osdBamAllocatedToRM');
                }
            }
        }

    }
})();