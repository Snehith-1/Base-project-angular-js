// JavaScript source code
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('BrsTrnUnreconTransactionDetailsController', BrsTrnUnreconTransactionDetailsController);

    BrsTrnUnreconTransactionDetailsController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'cmnfunctionService', 'DownloaddocumentService'];

    function BrsTrnUnreconTransactionDetailsController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService, DownloaddocumentService) {

        var vm = this;
        vm.title = 'BrsTrnUnreconTransactionDetailsController';
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var banktransc_gid = searchObject.banktransc_gid;
      
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
                $scope.lblremainingamount = resp.data.remaining_amount;
                $scope.brstransactiondetails_flag = resp.data.brstransactiondetails_flag;
                $scope.brstransactiondetailsadvice_flag = resp.data.brstransactiondetailsadvice_flag;
                if ($scope.lblremainingamount == "0.00") {

                    var modalInstance = $modal.open({
                        templateUrl: '/warningmessage.html',
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


                    }
                }

            });

            var url = 'api/employee/Employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });
            var url = 'api/employee/Employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list1 = resp.data.employee_list;
            });
            var url = 'api/UnreconciliationManagement/GetAdjustAdviceEmployeeWiseShow';
            SocketService.get(url).then(function (resp) {
                $scope.adjustadvicelist = resp.data.adjustadvicelist;
            });
            var url = 'api/BRSMaster/GetBRSActivityStatus';
            SocketService.get(url).then(function (resp) {
                $scope.BRSActivity_List = resp.data.BRSActivity_List;
            });
            var params = {
                banktransc_gid: banktransc_gid
            }
            var url = 'api/UnreconciliationManagement/GetUnreconTransactionList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.unrecontransactionlist = resp.data.unrecontransactionlist;

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
            var url = 'api/UnreconciliationManagement/GetDepartmentName';
            SocketService.get(url).then(function (resp) {
                $scope.department_name = resp.data.department_name;
                $scope.employeename = resp.data.employee_name;

            });
            var url = 'api/UnreconciliationManagement/GetSamfinCustomerSummary';
            SocketService.get(url).then(function (resp) {
                $scope.assignedlist1 = resp.data.assignedlist;

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
                frm.append('document_name', fname);
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
        $scope.limit_amountChange = function () {
            var input = document.getElementById('limit_amount').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_creditamount = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txt_amount = "";
            }
            else {
                $scope.txt_amount = output;
                document.getElementById('words_limitamt').innerHTML = lswords_creditamount;
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
            var url = 'api/UnreconciliationManagement/PostUnconciliationStatusUpdation';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();

                if (resp.data.status == true)
                {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $location.url('app/BrsTrnUnreconAssignedSummary');

                }
                else
                {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

            });

        }
        $scope.remarks = function ( transaction_remarks) {
            var modalInstance = $modal.open({
                templateUrl: '/remarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.transaction_remarks = transaction_remarks;
               
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.Back = function () {
            $location.url("app/BrsTrnUnreconAssignedSummary");
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
                        banktransc_gid: banktransc_gid
                    }
                    var url = 'api/UnreconciliationManagement/PostRMSendback';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                            var url = 'api/UnreconciliationManagement/GetBrsUnReconciliationSummary';
                           /* lockUI();*/
                            SocketService.get(url).then(function (resp) {
                                $scope.BrsUnreconciliation_list = resp.data.BrsUnreconciliation_list;
                            //    unlockUI();
                            });
                        }
                        else
                        {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });

                    $modalInstance.close('closed');
                    activate();
                    $location.url('app/BrsTrnUnreconAssignedSummary');

                }
            }
        }
        $scope.Assigned_Submit = function () {

            var params =
            {
                banktransc_gid: banktransc_gid,
                assigned_to: $scope.cboemployee.employee_gid,
                assigned_toname: $scope.cboemployee.employee_name,
                assigned_remarks: $scope.txtremarks,
            }
            var url = 'api/UnreconciliationManagement/Post2Assign';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    
                    //else if (lspage == "Debit") {
                    //    $state.go('app.BrsTrnUnreconAssignedSummary');
                    //}

                }
                else {

                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                $state.go('app.BrsTrnUnreconAssignedSummary');

            });

        }
        $scope.addtransaction = function () {
            var lsbrsactivityname = '';
            var lsbrsactivitygid = '';
            var lsadjustadvicename = '';
            var lsadjustadvicegid = '';
            var lssamfincustomer_name = '';
            var lssamfincustomer_gid = '';
            var lsadjustadvicename = '';
            var lsadjustadvicegid = '';
            if ($scope.cbosamfin != undefined || $scope.cbosamfin != null) {
                lssamfincustomer_gid = $scope.cbosamfin.samfincustomer_gid;
                lssamfincustomer_name = $scope.cbosamfin.samfincustomer_name;
            }


            lsbrsactivityname = $('#brsactivity :selected').text();
            lsbrsactivitygid = $scope.cbobrsactivity;

            lsadjustadvicename = $('#adjustadvice :selected').text();
            lsadjustadvicegid = $scope.cboaction;

            if ((($scope.cbobrsactivity == '' || $scope.cbobrsactivity == null) || ($scope.cboaction == '' || $scope.cboaction == null) || ($scope.txt_amount == '' || $scope.txt_amount == null) || ($scope.transactionremarks == '' || $scope.transactionremarks == null))) {
                Notify.alert('Kindly Fill Transaction Details', 'warning')
            }
            else {
                var params =
                {
                    banktransc_gid: banktransc_gid,
                    //assignby_gid: $scope.cboemployee1.employee_gid,
                    assignby_name: $scope.employeename,
                    activity_name: lsbrsactivityname,
                    activity_gid: lsbrsactivitygid,
                    samfincustomer_gid: lssamfincustomer_gid,
                    samfincustomer_name: lssamfincustomer_name,
                    action_name: lsadjustadvicename,
                    department_name: $scope.department_name,
                    amount: $scope.txt_amount,
                    transaction_remarks: $scope.transactionremarks,
                }
                var url = 'api/UnreconciliationManagement/PostUnreconTransactionDetails';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI();
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        //if (lspage == "Credit") {
                        //    $state.go('app.brsTrnUnreconCreditSummaryManagement');
                        //}
                        //else if (lspage == "Debit") {
                        //    $state.go('app.brsTrnUnreconDebitSummaryManagement');
                        //}
                        unrecontransaction_list();
                        activate();

                    }
                    else {

                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    $scope.cbosamfin = '';
                    $scope.cbobrsactivity = '';
                    $scope.cboaction = '';
                    $scope.txt_amount = '';
                    $scope.transactionremarks = '';
                    unlockUI();
                    activate();
                });
            }
        }
        function unrecontransaction_list() {
            var params = {
                banktransc_gid: banktransc_gid
            }
            var url = 'api/UnreconciliationManagement/GetUnreconTransactionList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.unrecontransactionlist = resp.data.unrecontransactionlist;

            });
        }
        $scope.remarks = function (transaction_remarks) {
            var modalInstance = $modal.open({
                templateUrl: '/remarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.transaction_remarks = transaction_remarks;

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.deletetransaction = function (unrecontransactiondetails_gid, banktransc_gid) {
            var params = {
                unrecontransactiondetails_gid: unrecontransactiondetails_gid,
                banktransc_gid: banktransc_gid

            }
            var url = 'api/UnreconciliationManagement/GetUnreconTransactionDelete';
            lockUI();
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
                unrecontransaction_list();
                activate();

            });
        }
    }
})();