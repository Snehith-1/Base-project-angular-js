(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSupplierController', MstSupplierController);

    MstSupplierController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstSupplierController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSupplierController';
        $scope.IsVisible = false;
        $scope.IsVisible1 = false;
        $scope.IsVisible2 = false;
        $scope.IsVisible3 = false;
        activate();

        function activate() {
            var url = 'api/MstApplication360/GetSupplier';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.supplier_list = resp.data.application_list;
                unlockUI();
            });

            // var url = 'api/MstApplication360/GetSupplierTempClear';
            // SocketService.get(url).then(function (resp) {
            // });
            
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

        $scope.delete = function (supplier_gid) {
            var params = {
                supplier_gid: supplier_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = 'api/MstApplication360/DeleteSupplier';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Supplier!', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        };

        $scope.addsupplier = function () {
            $scope.IsVisible = $scope.IsVisible ? false : true;
            $scope.IsVisible1 = $scope.IsVisible1 ? false : true;
            $scope.IsVisible2 = false;
            $scope.IsVisible3 = false;
            $scope.Alreadyaddedpanaadhar = null;
            $scope.txtsupplier_name = '';
            $scope.txtsupplierRef_no = '';
            $scope.txtpan_number = '';
            $scope.creditbankacc_list = '';
            activate();
            var url = 'api/MstAppCreditUnderWriting/GetCreditAccountType';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.accounttype_list = resp.data.creditbankacc_list;
            });

            var url = 'api/MstApplication360/GetSupplierTempClear';
            SocketService.get(url).then(function (resp) {
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
                    if(resp.data.result != null) {
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
                        } 
                    }
                    else {
                        Notify.alert(resp.data.message, 'warning')
                    }

                });
            }
        }
        $scope.change = function () {
            $scope.txtjointacctholder_name = '';
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
                    if(resp.data.result != null) {
                        if (resp.data.result.bankTxnStatus != "" && resp.data.result.bankTxnStatus != null) {
                            $scope.bankaccvalidation = true;
                            $scope.txtacctholder_name = resp.data.result.accountName;
    
                        } else if (resp.data.result.bankTxnStatus == "" || resp.data.result.bankTxnStatus == null) {
                            $scope.bankaccvalidation = false;
                            Notify.alert('Bank Account is not verified..!', 'warning');
                            $scope.txtacctholder_name = '';
                        } 
                    }
                    else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
        }

        $scope.add_bankacctdtl = function () {

            var lsaccounttype_gid = '';
            var lsaccounttype_name = '';

            if ($scope.accounttype_list != undefined || $scope.accounttype_list != null) {
                lsaccounttype_gid = $scope.accounttype_list.bankaccounttype_gid;
                lsaccounttype_name = $scope.accounttype_list.bankaccounttype_name;
            }

            if (($scope.txtIFSC_Code == undefined) || ($scope.txtIFSC_Code == '') || ($scope.txtbankacct_no == undefined) || ($scope.txtbankacct_no == '') ||
                ($scope.txtconfirmbankacct_no == undefined) || ($scope.txtconfirmbankacct_no == '') || ($scope.txtacctholder_name == undefined) || ($scope.txtacctholder_name == '') ||
            // ($scope.cboAccountType == undefined) || ($scope.cboAccountType == '') || 
            // ($scope.rdbJoint_Account == undefined) || ($scope.rdbJoint_Account == '') ||
                // ($scope.rdbCheque_Book == undefined) || ($scope.rdbCheque_Book == '') ||
                 ($scope.txtBank_Address == undefined) ||
                ($scope.txtBank_Address == '') || ($scope.txtBranch_Name == undefined) || ($scope.txtBranch_Name == '')) {
                Notify.alert('Enter All Mandatory Fields', 'warning');
            }

            else if ($scope.txtbankacct_no > $scope.txtconfirmbankacct_no || $scope.txtbankacct_no < $scope.txtconfirmbankacct_no) {
                Notify.alert('Account Number does not match', 'warning');
            }

            else {
                if ($scope.rdbJoint_Account == 'Yes') {

                    if (($scope.txtjointacctholder_name == undefined) || ($scope.txtjointacctholder_name == '')) {
                        Notify.alert('Enter Joint Account Holder Name', 'warning');
                    }
                    else {
                        var params = {
                            supplier_gid: '',                            
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
                            // primary_status: $scope.rdbprimarystatus
                        }
                        var url = 'api/MstApplication360/PostSupplierBank';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {
                                $scope.creditbankacc_list = resp.data.supplierbankacc_list;
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
                            $scope.rdbprimarystatus = '';
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
                            $scope.txtverify = '';
                            $scope.ifscvalidation = false;
                            $scope.bankaccvalidation = null;
                            //activate();
                        });
                    }
                }
                else {
                    var params = {
                        supplier_gid: '',                            
                        ifsc_code: $scope.txtIFSC_Code,
                        bank_name: $scope.txtBank_Name,
                        branch_name: $scope.txtBranch_Name,
                        Bank_Address: $scope.txtBank_Address,
                        micr_code: $scope.txtMICR_Code,
                        bankaccount_number: $scope.txtbankacct_no,
                        confirmbankaccountnumber: $scope.txtconfirmbankacct_no,
                        bankaccount_name: $scope.txtacctholder_name,
                        // bankaccounttype_gid: $scope.cboAccountType.bankaccounttype_gid,
                        // bankaccounttype_name: $scope.cboAccountType.bankaccounttype_name,
                        bankaccounttype_gid: $scope.cboAccountType.bankaccounttype_gid,
                            bankaccounttype_name: $scope.cboAccountType.bankaccounttype_name,
                        joint_account: $scope.rdbJoint_Account,
                        jointaccountholder_name: $scope.txtjointacctholder_name,
                        chequebook_status: $scope.rdbCheque_Book,
                        accountopen_date: $scope.txtAccountOpen_Date,
                    }
                    var url = 'api/MstApplication360/PostSupplierBank';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            $scope.creditbankacc_list = resp.data.supplierbankacc_list;

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
                        $scope.txtverify = '';
                        $scope.rdbprimarystatus = '';
                        $scope.ifscvalidation = false;
                        $scope.bankaccvalidation = null;
                    });
                }

            }
        }

        $scope.add_bankacctedit = function (id) {

            var lsaccounttype_gid = '';
            var lsaccounttype_name = '';

            if ($scope.accounttype_list != undefined || $scope.accounttype_list != null) {
                lsaccounttype_gid = $scope.accounttype_list.bankaccounttype_gid;
                lsaccounttype_name = $scope.accounttype_list.bankaccounttype_name;
            }

            if (($scope.txtIFSC_Code == undefined) || ($scope.txtIFSC_Code == '') || ($scope.txtbankacct_no == undefined) || ($scope.txtbankacct_no == '') ||
                ($scope.txtconfirmbankacct_no == undefined) || ($scope.txtconfirmbankacct_no == '') || ($scope.txtacctholder_name == undefined) || ($scope.txtacctholder_name == '') ||
            // ($scope.cboAccountType == undefined) || ($scope.cboAccountType == '') || 
            // ($scope.rdbJoint_Account == undefined) || ($scope.rdbJoint_Account == '') ||
                // ($scope.rdbCheque_Book == undefined) || ($scope.rdbCheque_Book == '') ||
                 ($scope.txtBank_Address == undefined) ||
                ($scope.txtBank_Address == '') || ($scope.txtBranch_Name == undefined) || ($scope.txtBranch_Name == '')) {
                Notify.alert('Enter All Mandatory Fields', 'warning');
            }

            else if ($scope.txtbankacct_no > $scope.txtconfirmbankacct_no || $scope.txtbankacct_no < $scope.txtconfirmbankacct_no) {
                Notify.alert('Account Number does not match', 'warning');
            }

            else {
                if ($scope.rdbJoint_Account == 'Yes') {

                    if (($scope.txtjointacctholder_name == undefined) || ($scope.txtjointacctholder_name == '')) {
                        Notify.alert('Enter Joint Account Holder Name', 'warning');
                    }
                    else {
                        var params = {
                            supplier_gid: id,                            
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
                            // primary_status: $scope.rdbprimarystatus
                        }
                        var url = 'api/MstApplication360/PostSupplierBank';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {
                                $scope.creditbankacc_list = resp.data.supplierbankacc_list;
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
                            $scope.rdbprimarystatus = '';
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
                            $scope.txtverify = '';
                            $scope.ifscvalidation = false;
                            $scope.bankaccvalidation = null;
                            //activate();
                        });
                    }
                }
                else {
                    var params = {
                        supplier_gid: id,                            
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
                        // bankaccounttype_gid: lsaccounttype_gid,
                        //     bankaccounttype_name: lsaccounttype_name,
                        joint_account: $scope.rdbJoint_Account,
                        jointaccountholder_name: $scope.txtjointacctholder_name,
                        chequebook_status: $scope.rdbCheque_Book,
                        accountopen_date: $scope.txtAccountOpen_Date,
                    }
                    var url = 'api/MstApplication360/PostSupplierBank';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            $scope.creditbankacc_list = resp.data.supplierbankacc_list;

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
                        $scope.txtverify = '';
                        $scope.rdbprimarystatus = '';
                        $scope.ifscvalidation = false;
                        $scope.bankaccvalidation = null;
                    });
                }

            }
        }

        $scope.creditbankacctdtl_delete = function (supplier2bank_gid, supplier_gid) {
            var params = {
                supplier2bank_gid: supplier2bank_gid,
                supplier_gid: supplier_gid
            }
            var url = 'api/MstApplication360/DeleteSupplierBankAcc';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.creditbankacc_list = resp.data.supplierbankacc_list;
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

        $scope.creditbankacctdtl_edit = function (supplier2bank_gid,  supplier_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/companybankdtl.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.calender01 = function ($event) {
                    $event.preventDefault();
                    $event.stopPropagation();

                    $scope.open01 = true;
                };
                $scope.formats = ['dd-MM-yyyy'];
                $scope.format = $scope.formats[0];
                $scope.dateOptions = {
                    formatYear: 'yy',
                    startingDay: 1
                };

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                var url = 'api/MstAppCreditUnderWriting/GetCreditAccountType';
                SocketService.get(url).then(function (resp) {
                    unlockUI();
                    $scope.accounttype_list = resp.data.creditbankacc_list;
                });

                var param = {
                    supplier2bank_gid: supplier2bank_gid
                }

                var url = 'api/MstApplication360/EditGetSupplierBankAccDtl';

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
                    $scope.rdbprimarystatus = resp.data.primary_status;
                    //$scope.txtAccountOpen_Date = Date.parse($scope.txtAccountOpen_Date);
                    //$scope.credituploaddocument_list = resp.data.credituploaddocument_list;
                    unlockUI();
                });

                vm.submitted = false;
                vm.validateInput = function (name, type) {
                    var input = vm.formValidate[name];
                    return (input.$dirty || vm.submitted) && input.$error[type];
                };

                // Submit form
                vm.submitForm = function () {
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

                $scope.change = function () {
                    $scope.txtjointacctholder_name = '';
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
                            if(resp.data.result != null) {
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
                                } 
                            }
                            else {
                                Notify.alert(resp.data.message, 'warning')
                            }

                        });
                    }
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update_Bankacctdtl = function () {
                    if (($scope.txtIFSC_Code == undefined) || ($scope.txtIFSC_Code == '') || ($scope.txtbankacct_no == undefined) || ($scope.txtbankacct_no == '') ||
                       ($scope.txtconfirmbankacct_no == undefined) || ($scope.txtconfirmbankacct_no == '') || ($scope.txtacctholder_name == undefined) || ($scope.txtacctholder_name == '') ||
                //    ($scope.cboAccountType == undefined) || ($scope.cboAccountType == '') || 
                //    ($scope.rdbJoint_Account == undefined) || ($scope.rdbJoint_Account == '') ||
                        // ($scope.rdbCheque_Book == undefined) || ($scope.rdbCheque_Book == '') || 
                        ($scope.txtBank_Address == undefined) || ($scope.txtBank_Address == '') || ($scope.txtBranch_Name == undefined) || ($scope.txtBranch_Name == '')) {
                        Notify.alert('Enter All Mandatory Fields', 'warning');
                    }

                    else if ($scope.txtbankacct_no > $scope.txtconfirmbankacct_no || $scope.txtbankacct_no < $scope.txtconfirmbankacct_no) {
                        Notify.alert('Account Number does not match', 'warning');
                    }

                    else {
                        if ($scope.rdbJoint_Account == 'Yes') {

                            if (($scope.txtjointacctholder_name == undefined) || ($scope.txtjointacctholder_name == '')) {
                                Notify.alert('Enter Joint Account Holder Name', 'warning');
                            }
                            else {
                                var bankaccounttype_name = $('#AccountType :selected').text();
                                var params = {
                                    supplier2bank_gid: supplier2bank_gid,
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
                                    // primary_status: $scope.rdbprimarystatus

                                }
                                var url = 'api/MstApplication360/UpdateSupplierBankAccDtl';
                                lockUI();
                                SocketService.post(url, params).then(function (resp) {
                                    unlockUI();
                                    if (resp.data.status == true) {
                                        $scope.creditbankacc_list = resp.data.supplierbankacc_list;
                                        Notify.alert(resp.data.message, {
                                            status: 'success',
                                            pos: 'top-center',
                                            timeout: 3000
                                        });
                                        supplierbankacctlist(supplier2bank_gid, supplier_gid);
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
                                activate();
                            }
                        }
                        else {
                            var bankaccounttype_name = $('#AccountType :selected').text();
                            var params = {
                                supplier2bank_gid: supplier2bank_gid,
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
                                // primary_status: $scope.rdbprimarystatus

                            }
                            var url = 'api/MstApplication360/UpdateSupplierBankAccDtl';
                            lockUI();
                            SocketService.post(url, params).then(function (resp) {
                                unlockUI();
                                if (resp.data.status == true) {
                                    $scope.creditbankacc_list = resp.data.supplierbankacc_list;
                                    Notify.alert(resp.data.message, {
                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    supplierbankacctlist(supplier2bank_gid, supplier_gid);
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
                            activate();
                        }
                    }
                }

            }
        }


        function supplierbankacctlist(supplier2bank_gid, supplier_gid){
            if (supplier_gid != undefined || supplier_gid != null || supplier_gid != ''){
                $scope.lssupplier_gid = supplier_gid;
            }
            else {
                $scope.lssupplier_gid = '';
            }
            var params = {
                supplier2bank_gid: supplier2bank_gid,
                supplier_gid:  $scope.lssupplier_gid
            }
            var url = 'api/MstApplication360/GetSupplierBankList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.creditbankacc_list = resp.data.supplierbankacc_list;
                unlockUI();
            });
    }
        $scope.creditbankacctdtl_view = function (supplier2bank_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/companybankdtlview.html',
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

                var param = {
                    supplier2bank_gid: supplier2bank_gid
                }

                var url = 'api/MstApplication360/EditGetSupplierBankAccDtl';

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
                    $scope.rdbprimarystatus = resp.data.primary_status;
                    //$scope.txtAccountOpen_Date = Date.parse($scope.txtAccountOpen_Date);
                    //$scope.credituploaddocument_list = resp.data.credituploaddocument_list;
                    unlockUI();
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

               
            }
        }

        $scope.getPANbasedGSTADD = function () {
            if ($scope.txtpan_number != undefined && $scope.txtpan_number.length == 10) {
                
                var params = {
                    pan: $scope.txtpan_number
                }
                var url = 'api/Kyc/GSTSBPAN';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.statusCode == 101) {
                        $scope.panvalidation = true;
                        const GstArray = resp.data.result;

                        var param = {
                            pan: $scope.txtpan_number
                        }
                        var url = 'api/Kyc/PANNumber';
                        lockUI();
                        SocketService.post(url, param).then(function (resp) {
                            unlockUI();
                            if(resp.data.result != null) {
                                if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                                    $scope.txtcompany_name = resp.data.result.name;
                                }
                            }
                            else {
                                Notify.alert(resp.data.message, 'warning')
                            }
                        });

                    } else if (resp.data.statusCode == 103) {
                        var param = {
                            pan: $scope.txtpan_number
                        }
                        var url = 'api/Kyc/PANNumber';
                        lockUI();
                        SocketService.post(url, param).then(function (resp) {
                            unlockUI();
                            if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                                $scope.txtcompany_name = resp.data.result.name;
                                $scope.panvalidation = true;
                                // institutiongstlist();
                            } else if (resp.data.result.name == "" || resp.data.result.name == undefined) {
                                $scope.panvalidation = false;
                                Notify.alert('PAN is not verified..!', 'warning');
                                // institutiongstlist();
                            } else {
                                Notify.alert(resp.data.message, 'warning')
                            }

                        });

                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }

                    var pan_no = ($scope.txtpan_number =="" || $scope.txtpan_number ==undefined) ? 'No': $scope.txtpan_number;
                     var params = {
                         pan_no: pan_no,
                         supplier_gid: '',

                     }
                     var url = 'api/MstApplication360/SupplierPanValidation';
                     SocketService.post(url, params).then(function (resp) {
                         $scope.lblcreated_by = resp.data.lscreatedby_name;
                         unlockUI();
                         if (resp.data.status == true) {
                             if (resp.data.panoraadhar =="PAN")
                                 $scope.Alreadyaddedpanaadhar = true;
                             else
                                 $scope.Alreadyaddedpanaadhar = false;
                         }
                         else {
                             $scope.Alreadyaddedpanaadhar = false;
                         }
                     });
                
                     $scope.Alreadyaddedpanaadhar = false;

                });


					 
            }
        }

        $scope.getPANbasedGSTEDIT = function () {
            if ($scope.txtpan_number != undefined && $scope.txtpan_number.length == 10) {
                
                var params = {
                    pan: $scope.txtpan_number
                }
                var url = 'api/Kyc/GSTSBPAN';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.statusCode == 101) {
                        $scope.panvalidation = true;
                        const GstArray = resp.data.result;

                        var param = {
                            pan: $scope.txtpan_number
                        }
                        var url = 'api/Kyc/PANNumber';
                        lockUI();
                        SocketService.post(url, param).then(function (resp) {
                            unlockUI();
                            if(resp.data.result != null) {
                                if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                                    $scope.txtcompany_name = resp.data.result.name;
                                }
                            }
                            else {
                                Notify.alert(resp.data.message, 'warning')
                            }
                        });

                    } else if (resp.data.statusCode == 103) {
                        var param = {
                            pan: $scope.txtpan_number
                        }
                        var url = 'api/Kyc/PANNumber';
                        lockUI();
                        SocketService.post(url, param).then(function (resp) {
                            unlockUI();
                            if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                                $scope.txtcompany_name = resp.data.result.name;
                                $scope.panvalidation = true;
                                // institutiongstlist();
                            } else if (resp.data.result.name == "" || resp.data.result.name == undefined) {
                                $scope.panvalidation = false;
                                Notify.alert('PAN is not verified..!', 'warning');
                                // institutiongstlist();
                            } else {
                                Notify.alert(resp.data.message, 'warning')
                            }

                        });

                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }

                    var pan_no = ($scope.txtpan_number =="" || $scope.txtpan_number ==undefined) ? 'No': $scope.txtpan_number;
                     var params = {
                         pan_no: pan_no,
                         supplier_gid: $scope.supplier_gid,

                     }
                     var url = 'api/MstApplication360/SupplierPanValidation';
                     SocketService.post(url, params).then(function (resp) {
                         $scope.lblcreated_by = resp.data.lscreatedby_name;
                         unlockUI();
                         if (resp.data.status == true) {
                             if (resp.data.panoraadhar =="PAN")
                                 $scope.Alreadyaddedpanaadhar = true;
                             else
                                 $scope.Alreadyaddedpanaadhar = false;
                         }
                         else {
                             $scope.Alreadyaddedpanaadhar = false;
                         }
                     });
                
                     $scope.Alreadyaddedpanaadhar = false;

                });


					 
            }
        }


        $scope.submitAdd = function () {

            var pan_no = ($scope.txtpan_number =="" || $scope.txtpan_number ==undefined) ? 'No': $scope.txtpan_number;
                     var param = {
                         pan_no: pan_no,
                         supplier_gid: '',
                     }
                     var url = 'api/MstApplication360/SupplierPanValidation';
                     SocketService.post(url, param).then(function (resp) {
                        //  $scope.lblcreated_by = resp.data.lscreatedby_name;
                         unlockUI();
                         if (resp.data.status == true) {
                             if (resp.data.panoraadhar =="PAN"){
                             Notify.alert('This PAN number is already added', 'warning')
                             return
                             }

                             else {

                                var params = {
                                    supplier_name: $scope.txtsupplier_name,
                                    pan_no: $scope.txtpan_number,
                                    
                                }
                                var url = 'api/MstApplication360/CreateSupplier';
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
                                        $scope.IsVisible = $scope.IsVisible ? false : true;
                                                $scope.IsVisible1 = $scope.IsVisible1 ? false : true;
                                                $scope.IsVisible2 = false;
                                                $scope.IsVisible3 = false;
                                    }
                                    else {
                                        Notify.alert(resp.data.message, {
                                            status: 'warning',
                                            pos: 'top-center',
                                            timeout: 3000
                                        });
         
                                    }
                                                });
                                                activate();
                                                
                             }

                         }

                       
                     });

        //     if ($scope.Alreadyaddedpanaadhar == true) {
        //        Notify.alert('PAN number is already approved, you cannot add', 'warning')
        //        return
        //    }

                                    }
   
               $scope.update = function (id) {
                

                var pan_no = ($scope.txtpan_number =="" || $scope.txtpan_number ==undefined) ? 'No': $scope.txtpan_number;
                     var param = {
                         pan_no: pan_no,
                         supplier_gid: id,
                     }
                     var url = 'api/MstApplication360/SupplierPanValidation';
                     SocketService.post(url, param).then(function (resp) {
                        //  $scope.lblcreated_by = resp.data.lscreatedby_name;
                         unlockUI();
                         if (resp.data.status == true) {
                             if (resp.data.panoraadhar =="PAN"){
                             Notify.alert('This PAN number is already added', 'warning')
                             return
                             }
                             else {

                            
                                var params = {
                                    supplier_name: $scope.txtsupplier_name,
                                    pan_no: $scope.txtpan_number,
                                    supplier_gid: id
                                }
                                var url = 'api/MstApplication360/UpdateSupplier';
                                SocketService.post(url, params).then(function (resp) {
                                    if (resp.data.status == true) {
                                        Notify.alert(resp.data.message, {
                                            status: 'success',
                                            pos: 'top-center',
                                            timeout: 3000
                                        });
                                       
                                        $scope.IsVisible2 = $scope.IsVisible2 ? false : true;
                                        $scope.IsVisible = false;
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
                         }
                       
                     });

                //    if ($scope.Alreadyaddedpanaadhar == true) {
                //        Notify.alert('PAN number is already approved, you cannot add', 'warning')
                //        return
                //    }
                  
               }

        $scope.editsupplier = function (supplier_gid) {

            $scope.IsVisible2 = true;
            $scope.IsVisible3 = false;
            $scope.IsVisible1 = false;
            $scope.IsVisible = $scope.IsVisible ? false : true;
            $scope.id = supplier_gid;
            $scope.Alreadyaddedpanaadhar = null;
            var url = 'api/MstAppCreditUnderWriting/GetCreditAccountType';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.accounttype_list = resp.data.creditbankacc_list;
                unlockUI();
            });

            var url = 'api/MstApplication360/GetSupplierTempClear';
            SocketService.get(url).then(function (resp) {
            });
            
            var params = {
                            supplier_gid: supplier_gid
                        }
                        var url = 'api/MstApplication360/EditSupplier';
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.txtsupplier_name = resp.data.supplier_name;
                            $scope.txtsupplierRef_no = resp.data.supplier_ref_no;
                            $scope.supplier_gid = resp.data.supplier_gid;
                            $scope.txtpan_number = resp.data.pan_no;
                            $scope.creditbankacc_list = resp.data.supplierbankacc_list;
                            unlockUI();
                        });
                 
            }

        $scope.edit_back = function () {
            $scope.IsVisible2 = $scope.IsVisible2 ? false : true;
            //$scope.IsVisible = $scope.IsVisible ? false : true;
            $scope.IsVisible = false;
    
            var url = 'api/MstApplication360/GetSupplierTempClear';
            SocketService.get(url).then(function (resp) {
            });

        }

        $scope.viewsupplier = function (supplier_gid) {
            //$scope.IsVisible = $scope.IsVisible ? false : true;
            //$scope.IsVisible3 = $scope.IsVisible3 ? false : true;
    
    
            var params = {
                supplier_gid: supplier_gid
            }
            var url = 'api/MstApplication360/EditSupplier';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtsupplier_name = resp.data.supplier_name;
                $scope.txtsupplierRef_no = resp.data.supplier_ref_no;
                $scope.supplier_gid = resp.data.supplier_gid;
                $scope.txtpan_number = resp.data.pan_no;
                $scope.creditbankacc_list = resp.data.supplierbankacc_list;
                unlockUI();
            });
    
            $scope.IsVisible3 = true;
            $scope.IsVisible2 = false;
            $scope.IsVisible1 = false;
            //$scope.IsVisible3 = $scope.IsVisible3 ? false : true;
            $scope.IsVisible = $scope.IsVisible ? false : true;
           
        }

        $scope.viewback = function () {

            
            $scope.IsVisible3 = $scope.IsVisible3 ? false : true;
            $scope.IsVisible = $scope.IsVisible ? false : true;
            // $scope.IsVisible2 = $scope.IsVisible2 ? false : true;
        }

        $scope.Status_update = function (supplier_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statussupplier.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    supplier_gid: supplier_gid
                }
                var url = 'api/MstApplication360/EditSupplier';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.supplier_gid = resp.data.supplier_gid
                    $scope.txtsupplier_name = resp.data.supplier_name;
                    $scope.rbo_status = resp.data.Status;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        supplier_gid: supplier_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status
                    }
                    var url = 'api/MstApplication360/InactiveSupplier';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                          closedivs ();
                            // $scope.IsVisible = false;
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        activate();
                    });
                    $modalInstance.close('closed');

                }

                var param = {
                    supplier_gid: supplier_gid
                }

                var url = 'api/MstApplication360/SuppierInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.Supplierinactivelog_list = resp.data.application_list
                    unlockUI();
                });

            }
        }

function closedivs(){

    $scope.IsVisible3 =  false;
    $scope.IsVisible2 = false;
    $scope.IsVisible1 =  false;
}

    }
})();

