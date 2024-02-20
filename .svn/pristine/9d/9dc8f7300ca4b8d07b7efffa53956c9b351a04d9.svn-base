(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnCreditGroupRepaymentAddController', AgrTrnCreditGroupRepaymentAddController);

        AgrTrnCreditGroupRepaymentAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','$sce','cmnfunctionService'];

    function AgrTrnCreditGroupRepaymentAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnCreditGroupRepaymentAddController';
        $scope.group_gid = $location.search().group_gid;
        var group_gid = $scope.group_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
      
        activate();
        lockUI();
        function activate() {

            var url = 'api/AgrTrnAppCreditUnderWriting/LenderTypeList';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.lendertype_list = resp.data.lendertype_list;
            });
            var url = 'api/AgrTrnAppCreditUnderWriting/CreditAccountClassificationList';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.creditaccountclassification_list = resp.data.creditaccountclassification_list;
            });

            var params = {
                applicant_type: 'Group',
                credit_gid: group_gid,
            }

            var url = 'api/AgrTrnAppCreditUnderWriting/GetRepaymentTrack';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.grouprepayment_list = resp.data.cuwrepaymenttrack_list;
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

              var params = {
                credit_gid: group_gid,
                applicant_type: 'Group'
            }

            var url = 'api/AgrTrnAppCreditUnderWriting/GetCreditOperationsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtgroup_name = resp.data.group_name;
                $scope.txtgroup_type = resp.data.group_type;
            }); 

        }

        $scope.lendertype_change = function (cboLenderType) {
            cboLenderType = $scope.cboLenderType;
                if($scope.cboLenderType.lendertype_name == 'Bank')
                   {                     
                       $scope.txtNBFC_Name = '';
                   }
                   else if($scope.cboLenderType.lendertype_name == 'Non Bank')
                   {
                    $scope.txtifsc_code = '';
                    $scope.txtBank_Name = '';
                   }
                   else {
                    $scope.txtifsc_code = '';
                    $scope.txtBank_Name = '';
                    $scope.txtNBFC_Name = '';
                }
           }

        $scope.add_creditrepaymentdtl = function () {
            if (($scope.cboLenderType == undefined) || ($scope.txtSanctioned_on == undefined) || ($scope.txtcurrentoutstanding_Amt == undefined) ||
            ($scope.txtSanctioned_Amount == undefined) || ($scope.txtInstalment_Frequency == undefined) || ($scope.txtInstalment_Amount == undefined) || ($scope.txtoriginaltenure_year == undefined) || ($scope.txtoriginalTenure_month == undefined) || ($scope.txtoriginalTenure_days == undefined) || ($scope.txtBalancetenure_year == undefined) ||
            ($scope.txtBalanceTenure_month == undefined) || ($scope.txtBalanceTenure_days == undefined) || ($scope.cboAcctClassification == undefined) || ($scope.txtOverdue_Amount == undefined) || ($scope.txtnoofdays_overdue == undefined) || ($scope.txtremarks == undefined) || 
            ($scope.cboLenderType == '' )|| ($scope.txtSanctioned_on == '' ) || ($scope.txtcurrentoutstanding_Amt == '' ) ||
            ($scope.txtSanctioned_Amount == '' ) || ($scope.txtInstalment_Frequency == '' ) || ($scope.txtInstalment_Amount == '' ) || ($scope.txtoriginaltenure_year == '' ) || ($scope.txtoriginalTenure_month == '' ) || ($scope.txtoriginalTenure_days == '' ) || ($scope.txtBalancetenure_year == '' ) ||
            ($scope.txtBalanceTenure_month == '' ) || ($scope.txtBalanceTenure_days == '' ) || ($scope.cboAcctClassification == '' ) || ($scope.txtOverdue_Amount == '' ) || ($scope.txtnoofdays_overdue == '' ) || ($scope.txtremarks == '' )) {
                Notify.alert('Enter All Mandatory Fields', 'warning');
            }
            else {
                if(($scope.cboLenderType.lendertype_name =='Bank'))
                {
                    if(($scope.txtifsc_code == undefined) || ($scope.txtBank_Name == undefined) ||                         
                         ($scope.txtifsc_code == '' ) || ($scope.txtBank_Name == '' ))
                    {
                        Notify.alert('Enter All Mandatory Fields', 'warning');
                    }
                    else {
                        var params = {
                            application_gid: application_gid,
                            applicant_type: 'Group',
                            lendertype_gid: $scope.cboLenderType.lendertype_gid,
                            lender_type: $scope.cboLenderType.lendertype_name,
                            ifsc_code: $scope.txtifsc_code,
                            bank_name: $scope.txtBank_Name,
                            nbfc_name: $scope.txtNBFC_Name,
                            branch_name: $scope.txtBranchName,
                            facility_type: $scope.txtfacility_type,
                            sanctionreference_id: $scope.txtsanctionref_no,
                            sanctioned_on: $scope.txtSanctioned_on,
                            sanction_amount: $scope.txtSanctioned_Amount,
                            accountstatus_on: $scope.txtAcctstatus_on,
                            currentoutsatnding_amount: $scope.txtcurrentoutstanding_Amt,
                            instalment_frequency: $scope.txtInstalment_Frequency,
                            instalment_amount: $scope.txtInstalment_Amount,
                            demanddue_date: $scope.txtDemandDue_Date,
                            oringinaltenure_year: $scope.txtoriginaltenure_year,
                            oringinaltenure_month: $scope.txtoriginalTenure_month,
                            oringinaltenure_days: $scope.txtoriginalTenure_days,
                            balancetenure_year: $scope.txtBalancetenure_year,
                            balancetenure_month: $scope.txtBalanceTenure_month,
                            balancetenure_days: $scope.txtBalanceTenure_days,
                            accountclassification_gid: $scope.cboAcctClassification.creditaccountclassification_gid,
                            account_classification: $scope.cboAcctClassification.creditaccountclassification_name,
                            overdue_amount: $scope.txtOverdue_Amount,
                            numberofdays_overdue: $scope.txtnoofdays_overdue,
                            remarks: $scope.txtremarks,
                            credit_gid: group_gid
                        }
                        var url = 'api/AgrTrnAppCreditUnderWriting/PostRepaymentTrack';
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
                            $scope.cboLenderType = '';
                            $scope.txtifsc_code = '';
                            $scope.txtBank_Name = '';
                            $scope.txtNBFC_Name = '';
                            $scope.txtBranchName = '';
                            $scope.txtfacility_type = '';
                            $scope.txtsanctionref_no = '';
                            $scope.txtSanctioned_on = '';
                            $scope.txtcurrentoutstanding_Amt = '';
                            $scope.txtInstalment_Frequency = '';
                            $scope.txtInstalment_Amount = '';
                            $scope.txtDemandDue_Date = '';
                            $scope.txtoriginaltenure_year = '';
                            $scope.txtoriginalTenure_month = '';
                            $scope.txtoriginalTenure_days = '';
                            $scope.txtBalancetenure_year = '';
                            $scope.txtBalanceTenure_month = '';
                            $scope.txtBalanceTenure_days = '';
                            $scope.cboAcctClassification = '';
                            $scope.txtOverdue_Amount = '';
                            $scope.txtnoofdays_overdue = '';
                            $scope.txtremarks = '';
                            $scope.txtAcctstatus_on = '';
                            $scope.txtSanctioned_Amount = '';
                            document.getElementById('words_totalamount1').innerHTML = '';
                            document.getElementById('words_totalamount2').innerHTML = '';
                            document.getElementById('words_totalamount3').innerHTML = '';
                            document.getElementById('words_totalamount4').innerHTML = '';
                        });
                    }
                }
                else if(($scope.cboLenderType.lendertype_name =='Non Bank'))
                {
                    if(($scope.txtNBFC_Name == undefined) || ($scope.txtNBFC_Name == ''))
                    {
                        Notify.alert('Enter All Mandatory Fields', 'warning');
                    }
                    else {
                        var params = {
                            application_gid: application_gid,
                            applicant_type: 'Group',
                            lendertype_gid: $scope.cboLenderType.lendertype_gid,
                            lender_type: $scope.cboLenderType.lendertype_name,
                            ifsc_code: $scope.txtifsc_code,
                            bank_name: $scope.txtBank_Name,
                            nbfc_name: $scope.txtNBFC_Name,
                            branch_name: $scope.txtBranchName,
                            facility_type: $scope.txtfacility_type,
                            sanctionreference_id: $scope.txtsanctionref_no,
                            sanctioned_on: $scope.txtSanctioned_on,
                            sanction_amount: $scope.txtSanctioned_Amount,
                            accountstatus_on: $scope.txtAcctstatus_on,
                            currentoutsatnding_amount: $scope.txtcurrentoutstanding_Amt,
                            instalment_frequency: $scope.txtInstalment_Frequency,
                            instalment_amount: $scope.txtInstalment_Amount,
                            demanddue_date: $scope.txtDemandDue_Date,
                            oringinaltenure_year: $scope.txtoriginaltenure_year,
                            oringinaltenure_month: $scope.txtoriginalTenure_month,
                            oringinaltenure_days: $scope.txtoriginalTenure_days,
                            balancetenure_year: $scope.txtBalancetenure_year,
                            balancetenure_month: $scope.txtBalanceTenure_month,
                            balancetenure_days: $scope.txtBalanceTenure_days,
                            accountclassification_gid: $scope.cboAcctClassification.creditaccountclassification_gid,
                            account_classification: $scope.cboAcctClassification.creditaccountclassification_name,
                            overdue_amount: $scope.txtOverdue_Amount,
                            numberofdays_overdue: $scope.txtnoofdays_overdue,
                            remarks: $scope.txtremarks,
                            credit_gid: group_gid
                        }
                        var url = 'api/AgrTrnAppCreditUnderWriting/PostRepaymentTrack';
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
                            $scope.cboLenderType = '';
                            $scope.txtifsc_code = '';
                            $scope.txtBank_Name = '';
                            $scope.txtNBFC_Name = '';
                            $scope.txtBranchName = '';
                            $scope.txtfacility_type = '';
                            $scope.txtsanctionref_no = '';
                            $scope.txtSanctioned_on = '';
                            $scope.txtcurrentoutstanding_Amt = '';
                            $scope.txtInstalment_Frequency = '';
                            $scope.txtInstalment_Amount = '';
                            $scope.txtDemandDue_Date = '';
                            $scope.txtoriginaltenure_year = '';
                            $scope.txtoriginalTenure_month = '';
                            $scope.txtoriginalTenure_days = '';
                            $scope.txtBalancetenure_year = '';
                            $scope.txtBalanceTenure_month = '';
                            $scope.txtBalanceTenure_days = '';
                            $scope.cboAcctClassification = '';
                            $scope.txtOverdue_Amount = '';
                            $scope.txtnoofdays_overdue = '';
                            $scope.txtremarks = '';
                            $scope.txtAcctstatus_on = '';
                            $scope.txtSanctioned_Amount = '';
                            document.getElementById('words_totalamount1').innerHTML = '';
                            document.getElementById('words_totalamount2').innerHTML = '';
                            document.getElementById('words_totalamount3').innerHTML = '';
                            document.getElementById('words_totalamount4').innerHTML = '';
                        });
                    }
                }
                else {
                    var params = {
                        application_gid: application_gid,
                        applicant_type: 'Group',
                        lendertype_gid: $scope.cboLenderType.lendertype_gid,
                        lender_type: $scope.cboLenderType.lendertype_name,
                        ifsc_code: $scope.txtifsc_code,
                        bank_name: $scope.txtBank_Name,
                        nbfc_name: $scope.txtNBFC_Name,
                        branch_name: $scope.txtBranchName,
                        facility_type: $scope.txtfacility_type,
                        sanctionreference_id: $scope.txtsanctionref_no,
                        sanctioned_on: $scope.txtSanctioned_on,
                        sanction_amount: $scope.txtSanctioned_Amount,
                        accountstatus_on: $scope.txtAcctstatus_on,
                        currentoutsatnding_amount: $scope.txtcurrentoutstanding_Amt,
                        instalment_frequency: $scope.txtInstalment_Frequency,
                        instalment_amount: $scope.txtInstalment_Amount,
                        demanddue_date: $scope.txtDemandDue_Date,
                        oringinaltenure_year: $scope.txtoriginaltenure_year,
                        oringinaltenure_month: $scope.txtoriginalTenure_month,
                        oringinaltenure_days: $scope.txtoriginalTenure_days,
                        balancetenure_year: $scope.txtBalancetenure_year,
                        balancetenure_month: $scope.txtBalanceTenure_month,
                        balancetenure_days: $scope.txtBalanceTenure_days,
                        accountclassification_gid: $scope.cboAcctClassification.creditaccountclassification_gid,
                        account_classification: $scope.cboAcctClassification.creditaccountclassification_name,
                        overdue_amount: $scope.txtOverdue_Amount,
                        numberofdays_overdue: $scope.txtnoofdays_overdue,
                        remarks: $scope.txtremarks,
                        credit_gid: group_gid
                    }
                    var url = 'api/AgrTrnAppCreditUnderWriting/PostRepaymentTrack';
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
                        $scope.cboLenderType = '';
                        $scope.txtifsc_code = '';
                        $scope.txtBank_Name = '';
                        $scope.txtNBFC_Name = '';
                        $scope.txtBranchName = '';
                        $scope.txtfacility_type = '';
                        $scope.txtsanctionref_no = '';
                        $scope.txtSanctioned_on = '';
                        $scope.txtcurrentoutstanding_Amt = '';
                        $scope.txtInstalment_Frequency = '';
                        $scope.txtInstalment_Amount = '';
                        $scope.txtDemandDue_Date = '';
                        $scope.txtoriginaltenure_year = '';
                        $scope.txtoriginalTenure_month = '';
                        $scope.txtoriginalTenure_days = '';
                        $scope.txtBalancetenure_year = '';
                        $scope.txtBalanceTenure_month = '';
                        $scope.txtBalanceTenure_days = '';
                        $scope.cboAcctClassification = '';
                        $scope.txtOverdue_Amount = '';
                        $scope.txtnoofdays_overdue = '';
                        $scope.txtremarks = '';
                        $scope.txtAcctstatus_on = '';
                        $scope.txtSanctioned_Amount = '';
                        document.getElementById('words_totalamount1').innerHTML = '';
                        document.getElementById('words_totalamount2').innerHTML = '';
                        document.getElementById('words_totalamount3').innerHTML = '';
                        document.getElementById('words_totalamount4').innerHTML = '';
                    });
                }
            }
        }
    

        $scope.creditrepayment_delete = function (creditrepaymentdtl_gid) {
            var params = {
                creditrepaymentdtl_gid: creditrepaymentdtl_gid
            }
            var url = 'api/AgrTrnAppCreditUnderWriting/DeleteRepaymentTrack';
            SocketService.getparams(url, params).then(function (resp) {
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
        }

        
        $scope.SanctionedAmount = function () {
            var input = document.getElementById('SanctionedAmount').value;
            var amountwithdot = input.includes(".");
            if (amountwithdot == false) {
                var str1 = input.replace(/,/g, '');

                var str = Math.round(str1);
                var output = Number(str).toLocaleString('en-IN');
                var lswords_totalamount1= cmnfunctionService.fnConvertNumbertoWord(str);
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
                    document.getElementById('words_totalamount1').innerHTML = lswords_totalamount1;
                }
            }
        }

        $scope.currentoutstandingAmt = function () {
            var input = document.getElementById('currentoutstandingAmt').value;
            var amountwithdot = input.includes(".");
            if (amountwithdot == false) {
                var str1 = input.replace(/,/g, '');

                var str = Math.round(str1);
                var output = Number(str).toLocaleString('en-IN');
                var lswords_totalamount2= cmnfunctionService.fnConvertNumbertoWord(str);
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
                    document.getElementById('words_totalamount2').innerHTML = lswords_totalamount2;
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
                var lswords_totalamount3= cmnfunctionService.fnConvertNumbertoWord(str);
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
                var lswords_totalamount4= cmnfunctionService.fnConvertNumbertoWord(str);
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

        $scope.Back = function () {
            if (lspage == "StartCreditUnderwriting") {
                $location.url('app/AgrTrnStartCreditUnderwriting?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "PendingCADReview") {
                $location.url('app/AgrTrnCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "CADApplicationEdit") {
                $location.url('app/AgrTrnCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "CADAcceptanceCustomers") {
                $location.url('app/AgrTrnCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else {

            }
        }

        $scope.group_docchecklist = function () {
            $location.url('app/AgrTrnGroupDocCheckList?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }

        $scope.group_covenantdocchecklist = function () {
            $location.url('app/AgrTrnGroupCovenantDocChecklist?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }

        $scope.group_bureauadd = function () {
            $location.url('app/AgrTrnCreditGroupDtlAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }

        $scope.group_bankaccount = function () {
            $location.url('app/AgrTrnCreditGroupBankAcctAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }

        $scope.group_existingbankaccount = function () {
            $location.url('app/AgrTrnCreditGroupExistingBankAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }

        $scope.group_PSLdata = function () {
            $location.url('app/AgrTrnCreditGroupPSLDataFlagAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }       

        $scope.group_repayment = function () {
            $location.url('app/AgrTrnCreditGroupRepaymentAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }

        $scope.group_observation = function () {
            $location.url('app/AgrTrnCreditGroupObservationAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }
        
        $scope.creditrepayment_edit = function (creditrepaymentdtl_gid) {
            $location.url('app/AgrTrnCreditGroupRepaymentEdit?application_gid=' + application_gid + '&group_gid=' + group_gid + '&creditrepaymentdtl_gid=' + creditrepaymentdtl_gid + '&lspage=' + lspage);
        }

        $scope.company_bankstatement = function () {
            $location.url('app/AgrTrnCreditGroupBankStatementAnalysisAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }
        
        $scope.IFSCValidation = function () {

            if ($scope.txtifsc_code.length == 11) {
                var params = {
                    ifsc: $scope.txtifsc_code
                }

                var url = 'api/AgrKyc/IfscVerification';
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
    }
})();
