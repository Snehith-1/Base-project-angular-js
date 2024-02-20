(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCADCreditBankAccountDtlEditController', MstCADCreditBankAccountDtlEditController);

    MstCADCreditBankAccountDtlEditController.$inject = ['$rootScope', '$sce', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function MstCADCreditBankAccountDtlEditController($rootScope, $sce, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCADCreditBankAccountDtlEditController';
        $scope.institution_gid = $location.search().institution_gid;
        var institution_gid = $scope.institution_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.creditbankdtl_gid = $location.search().creditbankdtl_gid;
        var creditbankdtl_gid = $scope.creditbankdtl_gid;

        activate();
        function activate() { 
            $scope.creditbankdtl_gid = $location.search().creditbankdtl_gid;
            var param = {
                creditbankdtl_gid: $scope.creditbankdtl_gid
            }
            var url = 'api/MstCADCreditAction/ChequeTmpClear';
            SocketService.get(url).then(function (resp) {
            });
            var url = 'api/MstCADCreditAction/GetCreditAccountType';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.accounttype_list = resp.data.creditbankacc_list;
            });
            var url = 'api/MstCADCreditAction/EditGetCreditBankAccDtl';

            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.txtIFSC_Code = resp.data.ifsc_code;
                $scope.txtBank_Name = resp.data.bank_name;
                $scope.txtBranch_Name = resp.data.branch_name;
                $scope.txtBank_Address = resp.data.bank_address;
                $scope.txtMICR_Code = resp.data.micr_code;
                $scope.txtbankacct_no = resp.data.bankaccount_number;
                $scope.txtconfirmbankacct_no = resp.data.confirmbankaccountnumber;
                $scope.txtacctholder_name = resp.data.bankaccount_name;
                $scope.cboAccountType = resp.data.bankaccounttype_gid;
                $scope.rdbJoint_Account = resp.data.joint_account;
                $scope.txtjointacctholder_name = resp.data.jointaccountholder_name;
                $scope.rdbCheque_Book = resp.data.chequebook_status;
                $scope.txtAccountOpen_Date = resp.data.accountopen_date;
                $scope.txtAccountOpen_Date = Date.parse($scope.txtAccountOpen_Date);
                $scope.credituploaddocument_list = resp.data.credituploaddocument_list;
                unlockUI();
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
        }
        $scope.change = function () {
            $scope.txtjointacctholder_name = '';
        }
        $scope.Bankacctdtl_Back = function () {
            $location.url('app/MstCADCreditBankAccountDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=CADAcceptanceCustomers');
        }
        $scope.IFSCValidation = function () {

            if ($scope.txtIFSC_Code.length == 11) {
                var params = {
                    ifsc: $scope.txtIFSC_Code
                }
                lockUI();
                var url = 'api/CADKyc/IfscVerification';
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
        $scope.update_Bankacctdtl = function () {
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
                        var bankaccounttype_name = $('#AccountType :selected').text();
                        var params = {
                            creditbankdtl_gid: creditbankdtl_gid,
                           ifsc_code: $scope.txtIFSC_Code,
                            bank_name: $scope.txtBank_Name,
                            branch_name: $scope.txtBranch_Name,
                            Bank_Address: $scope.txtBank_Address,
                            micr_code: $scope.txtMICR_Code,
                            bankaccount_number: $scope.txtbankacct_no,
                            confirmbankaccountnumber: $scope.txtconfirmbankacct_no,
                            bankaccount_name: $scope.txtacctholder_name,
                            bankaccounttype_gid: $scope.cboAccountType,
                            bankaccounttype_name: bankaccounttype_name,
                            joint_account: $scope.rdbJoint_Account,
                            jointaccountholder_name: $scope.txtjointacctholder_name,
                            chequebook_status: $scope.rdbCheque_Book,
                            accountopen_date: $scope.txtAccountOpen_Date,

                        }
                        var url = 'api/MstCADCreditAction/UpdateCreditBankAccDtl';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
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
                            $location.url('app/MstCADCreditBankAccountDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=CADAcceptanceCustomers');
                        });
                    }
                }
                else {
                    var bankaccounttype_name = $('#AccountType :selected').text();
                    var params = {
                        creditbankdtl_gid: creditbankdtl_gid,
                       ifsc_code: $scope.txtIFSC_Code,
                        bank_name: $scope.txtBank_Name,
                        branch_name: $scope.txtBranch_Name,
                        Bank_Address: $scope.txtBank_Address,
                        micr_code: $scope.txtMICR_Code,
                        bankaccount_number: $scope.txtbankacct_no,
                        confirmbankaccountnumber: $scope.txtconfirmbankacct_no,
                        bankaccount_name: $scope.txtacctholder_name,
                        bankaccounttype_gid: $scope.cboAccountType,
                        bankaccounttype_name: bankaccounttype_name,
                        joint_account: $scope.rdbJoint_Account,
                        jointaccountholder_name: $scope.txtjointacctholder_name,
                        chequebook_status: $scope.rdbCheque_Book,
                        accountopen_date: $scope.txtAccountOpen_Date,

                    }
                    var url = 'api/MstCADCreditAction/UpdateCreditBankAccDtl';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
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
                    });
                    $location.url('app/MstCADCreditBankAccountDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=CADAcceptanceCustomers');
                }
                    }
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
                frm.append('creditbankdtl_gid', $scope.creditbankdtl_gid);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    lockUI();
                    var url = 'api/MstCADCreditAction/chequeleafdocumentUpload';
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

        $scope.uploaddocumentcancel = function (creditbankdtl2cheque_gid) {
            lockUI();
            var params = {
                creditbankdtl2cheque_gid: creditbankdtl2cheque_gid,
                credit_gid: creditbankdtl_gid
            }
            console.log(params)
            var url = 'api/MstCADCreditAction/DeleteCreditcheque';
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
                var url = 'api/CADKyc/BankAccVerification';
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
        $scope.downloadall = function () {
            for (var i = 0; i < $scope.credituploaddocument_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.credituploaddocument_list[i].chequeleaf_path, $scope.credituploaddocument_list[i].chequeleaf_name);
            }
        }
    }
})();

