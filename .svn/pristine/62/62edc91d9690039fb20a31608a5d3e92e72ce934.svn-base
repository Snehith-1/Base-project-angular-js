(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditGroupBankAcctAddController', MstCreditGroupBankAcctAddController);

    MstCreditGroupBankAcctAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService','cmnfunctionService'];

    function MstCreditGroupBankAcctAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCreditGroupBankAcctAddController';
        $scope.group_gid = $location.search().group_gid;
        var group_gid = $scope.group_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        lockUI();
        activate();
        
        function activate() {

            var params = {
                credit_gid: group_gid
             }
            var url = 'api/MstAppCreditUnderWriting/GetCreditAccountType';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.accounttype_list = resp.data.creditbankacc_list;
            });
            var url = 'api/MstAppCreditUnderWriting/ChequeTmpClear';
            SocketService.get(url).then(function (resp) {
                unlockUI();
            });
            var url = 'api/MstAppCreditUnderWriting/GetCrediBankAccDtl';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.creditbankacc_list = resp.data.creditbankacc_list;
            });
    
              vm.submitted = false;
              vm.validateInput = function(name, type) {
                var input = vm.formValidate[name];
                return (input.$dirty || vm.submitted) && input.$error[type];
              };
    
              // Submit form
              vm.submitForm = function() {
                vm.submitted = true;
                if (vm.formValidate.$valid) {
                } else {
                  return false;
                }
              };

               // Calender Popup... //

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
                credit_gid: group_gid,
                applicant_type: 'Group'
            }

            var url = 'api/MstAppCreditUnderWriting/GetCreditOperationsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtgroup_name = resp.data.group_name;
                $scope.txtgroup_type = resp.data.group_type;
            }); 

        }
        $scope.IFSCValidation = function () {

            if ($scope.txtIFSC_Code.length == 11) {
                var params = {
                    ifsc: $scope.txtIFSC_Code
                }
                lockUI();
                var url = 'api/Kyc/IfscVerification';
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.result.bank != "" && resp.data.result.bank != null) {
                        $scope.ifscvalidation = true;
                        $scope.txtBank_Name = resp.data.result.bank;
                        $scope.txtBranch_Name = resp.data.result.branch;
                        $scope.txtBank_Address = resp.data.result.address;
                        $scope.txtMICR_Code = resp.data.result.micr;

                        if (resp.data.result.micr != "" && resp.data.result.micr != null) {
                            $scope.micrempty = true;
                        }

                    } else if (resp.data.result.bank == "" || resp.data.result.bank == null) {
                        $scope.ifscvalidation = false;
                        Notify.alert('IFSC is not verified..!', 'warning');
                        $scope.txtBank_Name = '';
                        $scope.txtBranch_Name = '';
                        $scope.txtBank_Address = '';
                        $scope.txtMICR_Code = '';
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }

                });
            }
        }
        $scope.change = function () {
            $scope.txtjointacctholder_name = '';
        }
        $scope.add_creditbankacctdtl = function () {
            if (($scope.txtIFSC_Code == undefined) || ($scope.txtIFSC_Code == '') || ($scope.txtbankacct_no == undefined) || ($scope.txtbankacct_no == '') ||
                 ($scope.txtconfirmbankacct_no == undefined) || ($scope.txtconfirmbankacct_no == '') || ($scope.txtacctholder_name == undefined) || ($scope.txtacctholder_name == '') ||
             ($scope.cboAccountType == undefined) || ($scope.cboAccountType == '') || ($scope.rdbJoint_Account == undefined) || ($scope.rdbJoint_Account == '') ||
                 ($scope.rdbCheque_Book == undefined) || ($scope.rdbCheque_Book == '')) {
                Notify.alert('Enter All Mandatory Fields', 'warning');
            }
            else {
                if ($scope.rdbJoint_Account == 'Yes') {

                    if (($scope.txtjointacctholder_name == undefined) || ($scope.txtjointacctholder_name == '')) {
                        Notify.alert('Enter Joint Account Holder Name', 'warning');
                    }
                    else {
                        var params = {
                            application_gid: application_gid,
                            credit_gid: group_gid,
                            applicant_type: 'Group',
                            ifsc_code: $scope.txtIFSC_Code,
                            bank_name: $scope.txtBank_Name,
                            branch_name: $scope.txtBranch_Name,
                            Bank_Address: $scope.txtBank_Address,
                            micr_code: $scope.txtMICR_Code,
                            bankaccount_number: $scope.txtbankacct_no,
                            confirmbankaccountnumber: $scope.txtconfirmbankacct_no,
                            bankaccount_name: $scope.txtacctholder_name,
                            bankaccounttype_gid: $scope.cboAccountType.bankaccounttype_gid,
                            bankaccounttype_name: $scope.cboAccountType.bankaccounttype_name,
                            joint_account: $scope.rdbJoint_Account,
                            jointaccountholder_name: $scope.txtjointacctholder_name,
                            chequebook_status: $scope.rdbCheque_Book,
                            accountopen_date: $scope.txtAccountOpen_Date,
                        }
                        var url = 'api/MstAppCreditUnderWriting/PostCreditBank';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {
                                $scope.creditbankacc_list = resp.data.creditbankacc_list;
                                $scope.credituploaddocument_list = null;
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
                            $scope.txtIFSC_Code = '';
                            $scope.txtBank_Name = '';
                            $scope.txtBranch_Name = '';
                            $scope.txtBank_Address = '';
                            $scope.txtMICR_Code = '';
                            $scope.txtbankacct_no = '';
                            $scope.txtconfirmbankacct_no = '';
                            $scope.txtacctholder_name = '';
                            $scope.cboAccountType = '';
                            $scope.rdbJoint_Account = '';
                            $scope.txtjointacctholder_name = '';
                            $scope.rdbCheque_Book = '';
                            $scope.txtAccountOpen_Date = '';
                            activate();
                        });
                    }
                }
                else {
                    var params = {
                        application_gid: application_gid,
                        credit_gid: group_gid,
                        applicant_type: 'Group',
                        ifsc_code: $scope.txtIFSC_Code,
                        bank_name: $scope.txtBank_Name,
                        branch_name: $scope.txtBranch_Name,
                        Bank_Address: $scope.txtBank_Address,
                        micr_code: $scope.txtMICR_Code,
                        bankaccount_number: $scope.txtbankacct_no,
                        confirmbankaccountnumber: $scope.txtconfirmbankacct_no,
                        bankaccount_name: $scope.txtacctholder_name,
                        bankaccounttype_gid: $scope.cboAccountType.bankaccounttype_gid,
                        bankaccounttype_name: $scope.cboAccountType.bankaccounttype_name,
                        joint_account: $scope.rdbJoint_Account,
                        jointaccountholder_name: $scope.txtjointacctholder_name,
                        chequebook_status: $scope.rdbCheque_Book,
                        accountopen_date: $scope.txtAccountOpen_Date,
                    }
                    var url = 'api/MstAppCreditUnderWriting/PostCreditBank';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            $scope.creditbankacc_list = resp.data.creditbankacc_list;
                            $scope.credituploaddocument_list = null;
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
                        $scope.txtIFSC_Code = '';
                        $scope.txtBank_Name = '';
                        $scope.txtBranch_Name = '';
                        $scope.txtBank_Address = '';
                        $scope.txtMICR_Code = '';
                        $scope.txtbankacct_no = '';
                        $scope.txtconfirmbankacct_no = '';
                        $scope.txtacctholder_name = '';
                        $scope.cboAccountType = '';
                        $scope.rdbJoint_Account = '';
                        $scope.txtjointacctholder_name = '';
                        $scope.rdbCheque_Book = '';
                        $scope.txtAccountOpen_Date = '';
                        activate();
                    });
                }
          }
      }
        $scope.creditbankacctdtl_delete = function (creditbankdtl_gid) {
            var params = {
                creditbankdtl_gid: creditbankdtl_gid,
                credit_gid: group_gid
            }
            var url = 'api/MstAppCreditUnderWriting/DeletecreditBankAcc';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.creditbankacc_list = resp.data.creditbankacc_list;
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
                    activate();
                }


            });
        }

        $scope.chequeleafdocumentUpload = function (val) {
            if (($scope.txtdocument_title == null) || ($scope.txtdocument_title == '') || ($scope.txtdocument_title == undefined)) {
                $("#chequefilefile").val('');
                Notify.alert('Kindly Enter the Document Title', 'warning');
            } else {
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

                        return false;
                    }
                }
                frm.append('document_title', $scope.txtdocument_title);
                frm.append('creditbankdtl_gid', '');
                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    lockUI();
                    var url = 'api/MstAppCreditUnderWriting/chequeleafdocumentUpload';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                        $scope.credituploaddocument_list = resp.data.credituploaddocument_list;
                        unlockUI();

                        $("#chequefilefile").val('');
                        $scope.uploadfrm = undefined;

                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $scope.txtdocument_title = '';
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
                    alert('Document is not Available..!');
                    return;
                }
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

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.uploaddocumentcancel = function (creditbankdtl2cheque_gid) {
            lockUI();
            var params = {
                creditbankdtl2cheque_gid: creditbankdtl2cheque_gid,
                credit_gid: 'test'
            }
            var url = 'api/MstAppCreditUnderWriting/DeleteCreditcheque';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.credituploaddocument_list = resp.data.credituploaddocument_list;
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

        $scope.BankAccValidation = function () {
            if ($scope.txtbankacct_no == $scope.txtconfirmbankacct_no) {
                var params = {
                    ifsc: $scope.txtIFSC_Code,
                    accountNumber: $scope.txtconfirmbankacct_no
                }
                var url = 'api/Kyc/BankAccVerification';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                unlockUI();
                    if (resp.data.result.bankTxnStatus != "" && resp.data.result.bankTxnStatus != null) {
                        $scope.bankaccvalidation = true;
                        $scope.txtbank_accountname = resp.data.result.accountName;
        
                    } else if (resp.data.result.bankTxnStatus == "" || resp.data.result.bankTxnStatus == null) {
                        $scope.bankaccvalidation = false;
                        Notify.alert('Bank Account is not verified..!', 'warning');
                        $scope.txtbank_accountname = '';
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
        }

        $scope.Back = function () {
            if (lspage == "myapp") {
                $location.url('app/MstStartCreditUnderwriting?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "CreditApproval") {
                $location.url('app/MstStartCreditUnderwriting?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "PendingCADReview") {
                $location.url('app/MstCADPendingApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "CADApplicationEdit") {
                $location.url('app/MstCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "CADAcceptanceCustomers") {
                $location.url('app/MstCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else {

            }
        }

        $scope.group_addcolending = function () {
            $location.url('app/MstCreditGroupColendingDtlAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }

        $scope.group_addguarantee = function () {
            $location.url('app/MstCreditGroupGuaranteeDtlAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }

        $scope.group_docchecklist = function () {
            $location.url('app/MstGroupDocCheckList?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }

        $scope.group_covenantdocchecklist = function () {
            $location.url('app/MstGroupCovenantDocChecklist?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }


        $scope.group_bureauadd = function () {
            $location.url('app/MstCreditGroupDtlAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }

        $scope.group_bankaccount = function () {
            $location.url('app/MstCreditGroupBankAcctAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }

        $scope.group_existingbankaccount = function () {
            $location.url('app/MstCreditGroupExistingBankAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }

        $scope.group_PSLdata = function () {
            $location.url('app/MstCreditGroupPSLDataFlagAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }       

        $scope.group_repayment = function () {
            $location.url('app/MstCreditGroupRepaymentAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }

        $scope.group_observation = function () {
            $location.url('app/MstCreditGroupObservationAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }

        $scope.creditbankacctdtl_edit = function (creditbankdtl_gid) {
            $location.url('app/MstCreditGroupBankAcctEdit?application_gid=' + application_gid + '&group_gid=' + group_gid + '&creditbankdtl_gid=' + creditbankdtl_gid + '&lspage=' + lspage);
        }
        $scope.company_bankstatement = function () {
            $location.url('app/MstCreditGroupBankStatementAnalysisAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }


        $scope.downloadall = function () {
            for (var i = 0; i < $scope.credituploaddocument_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.credituploaddocument_list[i].chequeleaf_path, $scope.credituploaddocument_list[i].chequeleaf_name);
            }
        }
    }
})();
