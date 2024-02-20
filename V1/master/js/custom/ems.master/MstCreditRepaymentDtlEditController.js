(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditRepaymentDtlEditController', MstCreditRepaymentDtlEditController);

    MstCreditRepaymentDtlEditController.$inject = ['$rootScope', '$sce', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function MstCreditRepaymentDtlEditController($rootScope, $sce, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCreditRepaymentDtlEditController';
        $scope.institution_gid = $location.search().institution_gid;
        var institution_gid = $scope.institution_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.creditrepaymentdtl_gid = $location.search().creditrepaymentdtl_gid;
        var creditrepaymentdtl_gid = $scope.creditrepaymentdtl_gid;
        $scope.lspage = $location.search().lspage;

        activate();
        function activate() {
            var url = 'api/MstAppCreditUnderWriting/LenderTypeList';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.lendertype_list = resp.data.lendertype_list;
            });
            var url = 'api/MstAppCreditUnderWriting/CreditAccountClassificationList';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.creditaccountclassification_list = resp.data.creditaccountclassification_list;
            });
            
            var param = {
                creditrepaymentdtl_gid: creditrepaymentdtl_gid
            }

            var url = 'api/MstAppCreditUnderWriting/EditRepaymentTrack';

            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.cboLenderType = resp.data.lendertype_gid;
               var lender_type = resp.data.lender_type;
               if(lender_type=='Bank')
               {
                   $scope.bank=true;
                   $scope.nonbank=false;
               }
               else if(lender_type=='Non Bank')
               {
                $scope.bank=false;
                $scope.nonbank=true;
               }
               else{
                $scope.bank=false;
                $scope.nonbank=false;
               }
                $scope.txtifsc_code = resp.data.ifsc_code;
                $scope.txtBank_Name = resp.data.bank_name;
                $scope.txtNBFC_Name = resp.data.nbfc_name;
                $scope.txtBranchName = resp.data.branch_name;
                $scope.txtfacility_type = resp.data.facility_type;
                $scope.txtsanctionref_no = resp.data.sanctionreference_id;
                $scope.txtSanctioned_on = resp.data.sanctioned_on;
                $scope.txtSanctioned_on = Date.parse($scope.txtSanctioned_on);
                $scope.txtSanctioned_Amount = resp.data.sanction_amount;
                $scope.txtAcctstatus_on = resp.data.accountstatus_on;
                $scope.txtAcctstatus_on = Date.parse($scope.txtAcctstatus_on);
                $scope.txtcurrentoutstanding_Amt = resp.data.currentoutsatnding_amount;
                $scope.txtInstalment_Frequency = resp.data.instalment_frequency;
                $scope.txtInstalment_Amount = resp.data.instalment_amount;
                $scope.txtDemandDue_Date = resp.data.demanddue_date;
                $scope.txtDemandDue_Date = Date.parse($scope.txtDemandDue_Date);
                $scope.txtoriginaltenure_year = resp.data.oringinaltenure_year;
                $scope.txtoriginalTenure_month = resp.data.oringinaltenure_month;
                $scope.txtoriginalTenure_days = resp.data.oringinaltenure_days;
                $scope.txtBalancetenure_year = resp.data.balancetenure_year;
                $scope.txtBalanceTenure_month = resp.data.balancetenure_month;
                $scope.txtBalanceTenure_days = resp.data.balancetenure_days;
                $scope.cboAcctClassification = resp.data.accountclassification_gid;
                $scope.txtOverdue_Amount = resp.data.overdue_amount;
                $scope.txtnoofdays_overdue = resp.data.numberofdays_overdue;
                $scope.txtremarks = resp.data.remarks;
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

            vm.calender2 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open2 = true;
            };

            vm.calender3 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open3 = true;
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
        }

        $scope.lendertype_change = function (cboLenderType) {
            var lendertypename = $('#lendertype_name :selected').text();
                if(lendertypename == 'Bank')
                   {
                       $scope.bank=true;
                       $scope.nonbank=false;
                       $scope.txtNBFC_Name = '';
                   }
                   else if(lendertypename == 'Non Bank')
                   {
                    $scope.bank=false;
                    $scope.nonbank=true;
                    $scope.txtifsc_code = '';
                    $scope.txtBank_Name = '';
                   }
                   else{
                    $scope.bank=false;
                    $scope.nonbank=false;
                    $scope.txtifsc_code = '';
                    $scope.txtBank_Name = '';
                    $scope.txtNBFC_Name = '';
                }
           }

        $scope.repaymentdtl_Back = function () {
            $location.url('app/MstCreditRepaymentDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + $scope.lspage);
        }

        $scope.update_repaymentdtl = function () {
            if (($scope.cboLenderType == undefined) || ($scope.txtifsc_code == undefined) || ($scope.txtBank_Name == undefined) || ($scope.txtNBFC_Name == undefined) || ($scope.txtSanctioned_on == undefined) || ($scope.txtcurrentoutstanding_Amt == undefined) ||
            ($scope.txtSanctioned_Amount == undefined) || ($scope.txtInstalment_Frequency == undefined) || ($scope.txtInstalment_Amount == undefined) || ($scope.txtoriginaltenure_year == undefined) || ($scope.txtoriginalTenure_month == undefined) || ($scope.txtoriginalTenure_days == undefined) || ($scope.txtBalancetenure_year == undefined) ||
            ($scope.txtBalanceTenure_month == undefined) || ($scope.txtBalanceTenure_days == undefined) || ($scope.cboAcctClassification == undefined) || ($scope.txtOverdue_Amount == undefined) || ($scope.txtnoofdays_overdue == undefined) || ($scope.txtremarks == undefined)) {
                Notify.alert('Enter All Mandatory Fields', 'warning');
            }
            else {
                var lendertypename = $('#lendertype_name :selected').text();
                if((lendertypename =='Bank'))
                {
                    if(($scope.txtifsc_code == undefined) || ($scope.txtBank_Name == undefined) ||                         
                         ($scope.txtifsc_code == '' ) || ($scope.txtBank_Name == '' ))
                    {
                        Notify.alert('Enter All Mandatory Fields', 'warning');
                    }
                    else {
                        var lendertypename = $('#lendertype_name :selected').text();
                        var creditaccountclassificationname = $('#creditaccountclassification_name :selected').text();
        
                        var params = {
                            application_gid: application_gid,
                            applicant_type: 'Institution',
                            lendertype_gid: $scope.cboLenderType,
                            lender_type: lendertypename,
                            ifsc_code: $scope.txtifsc_code,
                            bank_name: $scope.txtBank_Name,
                            nbfc_name: $scope.txtNBFC_Name,
                            branch_name: $scope.txtBranchName,
                            facility_type: $scope.txtfacility_type,
                            sanctionreference_id: $scope.txtsanctionref_no,
                            sanctionedon: $scope.txtSanctioned_on,
                            sanction_amount: $scope.txtSanctioned_Amount,
                            accountstatuson: $scope.txtAcctstatus_on,
                            currentoutsatnding_amount: $scope.txtcurrentoutstanding_Amt,
                            instalment_frequency: $scope.txtInstalment_Frequency,
                            instalment_amount: $scope.txtInstalment_Amount,
                            demandduedate: $scope.txtDemandDue_Date,
                            oringinaltenure_year: $scope.txtoriginaltenure_year,
                            oringinaltenure_month: $scope.txtoriginalTenure_month,
                            oringinaltenure_days: $scope.txtoriginalTenure_days,
                            balancetenure_year: $scope.txtBalancetenure_year,
                            balancetenure_month: $scope.txtBalanceTenure_month,
                            balancetenure_days: $scope.txtBalanceTenure_days,
                            accountclassification_gid: $scope.cboAcctClassification,
                            account_classification: creditaccountclassificationname,
                            overdue_amount: $scope.txtOverdue_Amount,
                            numberofdays_overdue: $scope.txtnoofdays_overdue,
                            remarks: $scope.txtremarks,
                            credit_gid: institution_gid,
                            creditrepaymentdtl_gid: creditrepaymentdtl_gid,
                        }
                        var url = 'api/MstAppCreditUnderWriting/UpdateRepaymentTrack';
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
                                    status: 'info',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                            }
                            $location.url('app/MstCreditRepaymentDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + $scope.lspage);
                        });
                    }
                }
                else if((lendertypename =='Non Bank'))
                {
                    if(($scope.txtNBFC_Name == undefined) || ($scope.txtNBFC_Name == ''))
                    {
                        Notify.alert('Enter All Mandatory Fields', 'warning');
                    }
                    else {
                        var lendertypename = $('#lendertype_name :selected').text();
                        var creditaccountclassificationname = $('#creditaccountclassification_name :selected').text();
        
                        var params = {
                            application_gid: application_gid,
                            applicant_type: 'Institution',
                            lendertype_gid: $scope.cboLenderType,
                            lender_type: lendertypename,
                            ifsc_code: $scope.txtifsc_code,
                            bank_name: $scope.txtBank_Name,
                            nbfc_name: $scope.txtNBFC_Name,
                            branch_name: $scope.txtBranchName,
                            facility_type: $scope.txtfacility_type,
                            sanctionreference_id: $scope.txtsanctionref_no,
                            sanctionedon: $scope.txtSanctioned_on,
                            sanction_amount: $scope.txtSanctioned_Amount,
                            accountstatuson: $scope.txtAcctstatus_on,
                            currentoutsatnding_amount: $scope.txtcurrentoutstanding_Amt,
                            instalment_frequency: $scope.txtInstalment_Frequency,
                            instalment_amount: $scope.txtInstalment_Amount,
                            demandduedate: $scope.txtDemandDue_Date,
                            oringinaltenure_year: $scope.txtoriginaltenure_year,
                            oringinaltenure_month: $scope.txtoriginalTenure_month,
                            oringinaltenure_days: $scope.txtoriginalTenure_days,
                            balancetenure_year: $scope.txtBalancetenure_year,
                            balancetenure_month: $scope.txtBalanceTenure_month,
                            balancetenure_days: $scope.txtBalanceTenure_days,
                            accountclassification_gid: $scope.cboAcctClassification,
                            account_classification: creditaccountclassificationname,
                            overdue_amount: $scope.txtOverdue_Amount,
                            numberofdays_overdue: $scope.txtnoofdays_overdue,
                            remarks: $scope.txtremarks,
                            credit_gid: institution_gid,
                            creditrepaymentdtl_gid: creditrepaymentdtl_gid,
                        }
                        var url = 'api/MstAppCreditUnderWriting/UpdateRepaymentTrack';
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
                                    status: 'info',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                            }
                            $location.url('app/MstCreditRepaymentDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + $scope.lspage);
                        });
                    }
                }
            else {
                var lendertypename = $('#lendertype_name :selected').text();
                var creditaccountclassificationname = $('#creditaccountclassification_name :selected').text();

                var params = {
                    application_gid: application_gid,
                    applicant_type: 'Institution',
                    lendertype_gid: $scope.cboLenderType,
                    lender_type: lendertypename,
                    ifsc_code: $scope.txtifsc_code,
                    bank_name: $scope.txtBank_Name,
                    nbfc_name: $scope.txtNBFC_Name,
                    branch_name: $scope.txtBranchName,
                    facility_type: $scope.txtfacility_type,
                    sanctionreference_id: $scope.txtsanctionref_no,
                    sanctionedon: $scope.txtSanctioned_on,
                    sanction_amount: $scope.txtSanctioned_Amount,
                    accountstatuson: $scope.txtAcctstatus_on,
                    currentoutsatnding_amount: $scope.txtcurrentoutstanding_Amt,
                    instalment_frequency: $scope.txtInstalment_Frequency,
                    instalment_amount: $scope.txtInstalment_Amount,
                    demandduedate: $scope.txtDemandDue_Date,
                    oringinaltenure_year: $scope.txtoriginaltenure_year,
                    oringinaltenure_month: $scope.txtoriginalTenure_month,
                    oringinaltenure_days: $scope.txtoriginalTenure_days,
                    balancetenure_year: $scope.txtBalancetenure_year,
                    balancetenure_month: $scope.txtBalanceTenure_month,
                    balancetenure_days: $scope.txtBalanceTenure_days,
                    accountclassification_gid: $scope.cboAcctClassification,
                    account_classification: creditaccountclassificationname,
                    overdue_amount: $scope.txtOverdue_Amount,
                    numberofdays_overdue: $scope.txtnoofdays_overdue,
                    remarks: $scope.txtremarks,
                    credit_gid: institution_gid,
                    creditrepaymentdtl_gid: creditrepaymentdtl_gid,
                }
                var url = 'api/MstAppCreditUnderWriting/UpdateRepaymentTrack';
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
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    $location.url('app/MstCreditRepaymentDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + $scope.lspage);
                });
            }
        }
        }

        $scope.IFSCValidation = function () {

            if ($scope.txtifsc_code.length == 11) {
                var params = {
                    ifsc: $scope.txtifsc_code
                }

                var url = 'api/Kyc/IfscVerification';
                SocketService.post(url, params).then(function (resp) {

                    if (resp.data.result.bank != "" && resp.data.result.bank != null) {
                        $scope.ifscvalidation = true;
                        $scope.txtBank_Name = resp.data.result.bank;
                        $scope.txtBranchName = resp.data.result.branch;

                    } else if (resp.data.result.bank == "" || resp.data.result.bank == null) {
                        $scope.ifscvalidation = false;
                        Notify.alert('IFSC is not verified..!', 'warning');
                        $scope.txtBank_Name = '';
                        $scope.txtBranchName = '';
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }

                });
            }
        }

        

        $scope.currentoutstandingAmt = function () {
            var input = document.getElementById('currentoutstandingAmt').value;
            var amountwithdot = input.includes(".");
            if (amountwithdot == false) {
                var str1 = input.replace(/,/g, '');

                var str = Math.round(str1);
                var output = Number(str).toLocaleString('en-IN');
                var lswords_totalamount1 = cmnfunctionService.fnConvertNumbertoWord(str);
                if (output == "NaN") {
                    Notify.alert('Accept Number Format Only..!', {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    // $scope.txtloanfaility_amount = "";
                }
                else {
                    $scope.txtcurrentoutstanding_Amt = output;
                    document.getElementById('words_totalamount1').innerHTML = lswords_totalamount1;
                }
            }
        }

        $scope.InstalmentAmount = function () {
            var input = document.getElementById('InstalmentAmount').value;
            var amountwithdot = input.includes(".");
            if (amountwithdot == false) {
                var str1 = input.replace(/,/g, '');

                var str = Math.round(str1);
                var output = Number(str).toLocaleString('en-IN');
                var lswords_totalamount2 = cmnfunctionService.fnConvertNumbertoWord(str);
                if (output == "NaN") {
                    Notify.alert('Accept Number Format Only..!', {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    // $scope.txtloanfaility_amount = "";
                }
                else {
                    $scope.txtInstalment_Amount = output;
                    document.getElementById('words_totalamount2').innerHTML = lswords_totalamount2;
                }
            }
        }

        $scope.SanctionedAmount = function () {
            var input = document.getElementById('SanctionedAmount').value;
            var amountwithdot = input.includes(".");
            if (amountwithdot == false) {
                var str1 = input.replace(/,/g, '');

                var str = Math.round(str1);
                var output = Number(str).toLocaleString('en-IN');
                var lswords_totalamount3 = cmnfunctionService.fnConvertNumbertoWord(str);
                if (output == "NaN") {
                    Notify.alert('Accept Number Format Only..!', {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    // $scope.txtloanfaility_amount = "";
                }
                else {
                    $scope.txtSanctioned_Amount = output;
                    document.getElementById('words_totalamount3').innerHTML = lswords_totalamount3;
                }
            }
        }

        $scope.OverdueAmount = function () {
            var input = document.getElementById('OverdueAmount').value;
            var amountwithdot = input.includes(".");
            if (amountwithdot == false) {
                var str1 = input.replace(/,/g, '');

                var str = Math.round(str1);
                var output = Number(str).toLocaleString('en-IN');
                var lswords_totalamount4 = cmnfunctionService.fnConvertNumbertoWord(str);
                if (output == "NaN") {
                    Notify.alert('Accept Number Format Only..!', {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    // $scope.txtloanfaility_amount = "";
                }
                else {
                    $scope.txtOverdue_Amount = output;
                    document.getElementById('words_totalamount4').innerHTML = lswords_totalamount4;
                }
            }
        }
    }
})();