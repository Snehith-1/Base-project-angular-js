(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditRepaymentDtlAddController', MstCreditRepaymentDtlAddController);

        MstCreditRepaymentDtlAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','$sce','cmnfunctionService'];

    function MstCreditRepaymentDtlAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCreditRepaymentDtlAddController';
        $scope.institution_gid = $location.search().institution_gid;
        var institution_gid = $scope.institution_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        lockUI();
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
          
            var params = {
                applicant_type: 'Institution',
                credit_gid: institution_gid,
            }

            var url = 'api/MstAppCreditUnderWriting/GetRepaymentTrack';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.institutionrepayment_list = resp.data.cuwrepaymenttrack_list;
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
                credit_gid: institution_gid,
                applicant_type: 'Institution'
                }

            var url = 'api/MstAppCreditUnderWriting/GetCreditOperationsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtinstitution_name = resp.data.company_name;
                $scope.txtstakeholder_type = resp.data.stakeholder_type;
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
                            applicant_type: 'Institution',
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
                            credit_gid: institution_gid
                        }
                        var url = 'api/MstAppCreditUnderWriting/PostRepaymentTrack';
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
                            $scope.txtSanctioned_Amount = '';
                            $scope.txtAcctstatus_on = '';
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
                            applicant_type: 'Institution',
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
                            credit_gid: institution_gid
                        }
                        var url = 'api/MstAppCreditUnderWriting/PostRepaymentTrack';
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
                            $scope.txtSanctioned_Amount = '';
                            $scope.txtAcctstatus_on = '';
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
                        applicant_type: 'Institution',
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
                        credit_gid: institution_gid
                    }
                    var url = 'api/MstAppCreditUnderWriting/PostRepaymentTrack';
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
                        $scope.txtSanctioned_Amount = '';
                        $scope.txtAcctstatus_on = '';
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
            var url = 'api/MstAppCreditUnderWriting/DeleteRepaymentTrack';
            lockUI();
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


        $scope.company_deferraldocchecklist = function () {
            $location.url('app/MstDocumentCheckList?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_covenantdocchecklist = function () {
            $location.url('app/MstCreditAddCovenantCheckList?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_economicadd = function () {
            $location.url('app/MstCreditEconomicCapitalAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_geneticadd = function () {
            $location.url('app/MstCreditCompanyDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_PSLdata = function () {
            $location.url('app/MstCreditPSLDataFlaggingAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_supplier = function () {
            $location.url('app/MstCreditSuppliersDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_buyer = function () {
            $location.url('app/MstCreditBuyerDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_bankaccount = function () {
            $location.url('app/MstCreditBankAccountDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_existingbankaccount = function () {
            $location.url('app/MstCreditExistingBankDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_repayment = function () {
            $location.url('app/MstCreditRepaymentDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_observation = function () {
            $location.url('app/MstCreditObservationAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        
        $scope.creditrepayment_edit = function (creditrepaymentdtl_gid) {
            $location.url('app/MstCreditRepaymentDtlEdit?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&creditrepaymentdtl_gid=' + creditrepaymentdtl_gid + '&lspage=' + lspage);
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
        $scope.tan_verification = function () {
            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=TAN' + '&lspage=' + lspage);
        }
        $scope.companyllpno_vertification = function () {
            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=LLPNO' + '&lspage=' + lspage);
        }
        $scope.mcasign_vertification = function () {
            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=MCASIGNATURE' + '&lspage=' + lspage);
        }
        $scope.iecdetailed_vertification = function () {
            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=IECDETAILED' + '&lspage=' + lspage);
        }
        $scope.fssai_vertification = function () {
            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=FSSAI' + '&lspage=' + lspage);
        }
        $scope.fda_vertification = function () {
            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=FDA' + '&lspage=' + lspage);
        }
        $scope.gst_verification = function () {
            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=GST' + '&lspage=' + lspage);
        }
        $scope.lpgid_verification = function () {
            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=LPGID' + '&lspage=' + lspage);
        }
        $scope.shop_vertification = function () {
            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=SHOP' + '&lspage=' + lspage);
        }
        $scope.rcauthadv_vertification = function () {
            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=RCAUTHADV' + '&lspage=' + lspage);
        }
        $scope.rcsearch_vertification = function () {
            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=RCSEARCH' + '&lspage=' + lspage);
        }
        $scope.propertytax_vertification = function () {
            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=PROPERTYTAX' + '&lspage=' + lspage);
        }
        $scope.company_bankstatement = function () {
            $location.url('app/MstCreditBankStatementAnalysisAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.company_fsa = function () {
            $location.url('app/MstCreditFsaDetailAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.institution_bureauadd = function () {
            $location.url('app/MstCreditInstitutionDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.crimecheck_record = function () {
            $location.url('app/MstCompanyCrimeCheckRecordAPI?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.crimecheckreport_realtime = function () {
            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=CRIMECHECKREPORTREALTIME' + '&lspage=' + lspage);    
        }
        $scope.company_addguarantee = function () {
            $location.url('app/MstCreditGuaranteeDetailAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.company_addcolending = function () {
            $location.url('app/MstCreditColendingDetailAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
    }
})();
