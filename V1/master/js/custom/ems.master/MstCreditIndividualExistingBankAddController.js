(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditIndividualExistingBankAddController', MstCreditIndividualExistingBankAddController);

        MstCreditIndividualExistingBankAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','$sce','cmnfunctionService'];

    function MstCreditIndividualExistingBankAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCreditIndividualExistingBankAddController';
        $scope.contact_gid = $location.search().contact_gid;
        var contact_gid = $scope.contact_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        lockUI();
        activate();
        
        function activate() {

            var url = 'api/MstAppCreditUnderWriting/BankNameList';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.bankdtl_list = resp.data.bankdtl_list;
            });
            var url = 'api/MstAppCreditUnderWriting/CreditUnderwritingFacilityTypeList';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.creditunderwritingfacilitytype_list = resp.data.creditunderwritingfacilitytype_list;
            });
            var url = 'api/MstAppCreditUnderWriting/FundedTypeIndicatorList';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.fundedtypeindicator_list = resp.data.fundedtypeindicator_list;
            });
            var url = 'api/MstAppCreditUnderWriting/CreditInstalmentFrequencyList';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.creditinstalmentfrequency_list = resp.data.creditinstalmentfrequency_list;
            });
            var url = 'api/MstAppCreditUnderWriting/CreditAccountClassificationList';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.creditaccountclassification_list = resp.data.creditaccountclassification_list;
            });

            var params = {
                applicant_type: 'Individual',
                credit_gid: contact_gid,
            }

            var url = 'api/MstAppCreditUnderWriting/GetExistingBankFacility';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.individualexistingbank_list = resp.data.cuwexistingbankfacility_list;
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
              
              vm.formats = ['dd-MM-yyyy'];
              vm.format = vm.formats[0];
              vm.dateOptions = {
                  formatYear: 'yy',
                  startingDay: 1
              };

              var params = {
                credit_gid: contact_gid,
                applicant_type: 'Individual'
            }

            var url = 'api/MstAppCreditUnderWriting/GetCreditOperationsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtindividual_name = resp.data.individual_name;
                $scope.txtstakeholder_type = resp.data.stakeholder_type;
                $scope.txturn_status = resp.data.urn_status;
                $scope.txturn_number = resp.data.urn;
            }); 

        }

        $scope.add_creditexistingbankdtl = function () {
            if (($scope.cboBankName == undefined) || ($scope.cboBankName == '') ) {
                Notify.alert('Enter All Mandatory Fields', 'warning');
            }
            else {
                var lscreditunderwritingfacilitytype_gid = '';
                var lscredit_underwriting_facility_type = '';
                var lsfundedtypeindicator_gid = '';
                var lsfundedtypeindicator_name = '';
                var lscreditinstalmentfrequency_gid = '';
                var lscreditinstalmentfrequency_name = '';
                var lscreditaccountclassification_gid = '';
                var lscreditaccountclassification_name = '';



                if ($scope.cboFacilityType != undefined || $scope.cboFacilityType != null) {
                    lscreditunderwritingfacilitytype_gid = $scope.cboFacilityType.creditunderwritingfacilitytype_gid;
                    lscredit_underwriting_facility_type = $scope.cboFacilityType.credit_underwriting_facility_type;
                }

                if ($scope.cboTypeofexistingfunded != undefined || $scope.cboTypeofexistingfunded != null) {
                    lsfundedtypeindicator_gid = $scope.cboTypeofexistingfunded.fundedtypeindicator_gid;
                    lsfundedtypeindicator_name = $scope.cboTypeofexistingfunded.fundedtypeindicator_name;
                }

                if ($scope.cboInstalmentfrequency != undefined || $scope.cboInstalmentfrequency != null) {
                    lscreditinstalmentfrequency_gid = $scope.cboInstalmentfrequency.fundedtypeindicator_gid;
                    lscreditinstalmentfrequency_name = $scope.cboInstalmentfrequency.fundedtypeindicator_name;
                }
                if ($scope.cboAccountClassification != undefined || $scope.cboAccountClassification != null) {
                    lscreditaccountclassification_gid = $scope.cboAccountClassification.creditaccountclassification_gid;
                    lscreditaccountclassification_name = $scope.cboAccountClassification.creditaccountclassification_name;
                }
             var params = {
                 application_gid: application_gid,
                 credit_gid: contact_gid,
                 applicant_type: 'Individual',
                 bank_gid: $scope.cboBankName.bankdtl_gid,
                 bank_name: $scope.cboBankName.bankdtl_name,
                 //facilitytype_gid: $scope.cboFacilityType.creditunderwritingfacilitytype_gid,
                 //facility_type: $scope.cboFacilityType.credit_underwriting_facility_type,
                 facilitytype_gid: lscreditunderwritingfacilitytype_gid,
                 facility_type: $scope.lscredit_underwriting_facility_type,
                 facilitysanctioned_on: $scope.txtfacilitysanction_date,
                 //fundedtypeindicator_gid: $scope.cboTypeofexistingfunded.fundedtypeindicator_gid,
                 //fundedtypeindicator_name: $scope.cboTypeofexistingfunded.fundedtypeindicator_name,
                 fundedtypeindicator_gid: lsfundedtypeindicator_gid,
                 fundedtypeindicator_name: lsfundedtypeindicator_name,
                 sanctioned_limit: $scope.txtSanctioned_Limit,
                 //instalmentfrequency_gid: $scope.cboInstalmentfrequency.creditinstalmentfrequency_gid,
                 //instalmentfrequency_name: $scope.cboInstalmentfrequency.creditinstalmentfrequency_name,
                 instalmentfrequency_gid: lscreditinstalmentfrequency_gid,
                 instalmentfrequency_name: lscreditinstalmentfrequency_name,
                 instalment_amount: $scope.txtInstalment_amount,
                 outstanding_amount: $scope.txtOutstanding_amount,
                 record_date: $scope.txtRecord_Date,
                 overdue_amount: $scope.txtOverdue_amount,
                 overdue_dpd: $scope.txtoverdue_dpd,
                 //accountclassification_gid: $scope.cboAccountClassification.creditaccountclassification_gid,
                 //account_classification: $scope.cboAccountClassification.creditaccountclassification_name,
                 accountclassification_gid: lscreditaccountclassification_gid,
                 account_classification: lscreditaccountclassification_name,
                 remarks: $scope.txtremarks
             }
            var url = 'api/MstAppCreditUnderWriting/PostExistingBankFacility';
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
                  $scope.cboBankName = '';
                  $scope.cboFacilityType = '';
                  $scope.txtfacilitysanction_date = '';
                  $scope.cboTypeofexistingfunded = '';
                  $scope.txtSanctioned_Limit = '';
                  $scope.cboInstalmentfrequency = '';
                  $scope.txtInstalment_amount = '';
                  $scope.txtOutstanding_amount = '';
                  $scope.txtRecord_Date = '';
                  $scope.txtOverdue_amount = '';
                  $scope.txtoverdue_dpd = '';
                  $scope.cboAccountClassification = '';
                  $scope.txtremarks = '';
                  document.getElementById('words_totalamount1').innerHTML = '';
                  document.getElementById('words_totalamount2').innerHTML = '';
                  document.getElementById('words_totalamount3').innerHTML = '';
                  document.getElementById('words_totalamount4').innerHTML = '';
                  document.getElementById('words_totalamount5').innerHTML = '';
                 
              });           
      }
    }

        $scope.creditexistingbank_delete = function (existingbankfacility_gid) {
        var params = {
            existingbankfacility_gid: existingbankfacility_gid
        }
        var url = 'api/MstAppCreditUnderWriting/DeleteExistingBankFacility';
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

    

    $scope.Instalmentamount = function () {
        var input = document.getElementById('Instalmentamount').value;
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
                $scope.txtInstalment_amount = output;
                document.getElementById('words_totalamount1').innerHTML = lswords_totalamount1;
            }
        }
    }

    $scope.Outstandingamount = function () {
        var input = document.getElementById('Outstandingamount').value;
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
                $scope.txtOutstanding_amount = output;
                document.getElementById('words_totalamount2').innerHTML = lswords_totalamount2;
            }
        }
    }

    $scope.Overdueamount = function () {
        var input = document.getElementById('Overdueamount').value;
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
                $scope.txtOverdue_amount = output;
                document.getElementById('words_totalamount3').innerHTML = lswords_totalamount3;
            }
        }
    }

    $scope.SanctionedLimit = function () {
        var input = document.getElementById('SanctionedLimit').value;
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
                $scope.txtSanctioned_Limit = output;
                document.getElementById('words_totalamount4').innerHTML = lswords_totalamount4;
            }
        }
    }

    $scope.Overduedpd = function () {
        var input = document.getElementById('Overduedpd').value;
        var amountwithdot = input.includes(".");
        if (amountwithdot == false) {
            var str1 = input.replace(/,/g, '');

            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount5= cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                // $scope.txtloanfaility_amount = "";
            }
            else {
                $scope.txtoverdue_dpd = output;
                document.getElementById('words_totalamount5').innerHTML = lswords_totalamount5;
            }
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

        $scope.individual_addcolending = function () {
            $location.url('app/MstCreditIndividualColendingDtlAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_addguarantee = function () {
            $location.url('app/MstCreditIndividualGuaranteeDtlAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_docchecklist = function () {
            $location.url('app/MstIndividualDocCheckList?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_covenantdocchecklist = function () {
            $location.url('app/MstIndividualCovenantDocChecklist?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_bureauadd = function () {
            $location.url('app/MstCreditIndividualDtlAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_bankaccount = function () {
            $location.url('app/MstCreditIndividualBankAcctAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_existingbankaccount = function () {
            $location.url('app/MstCreditIndividualExistingBankAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_PSLdata = function () {
            $location.url('app/MstCreditIndividualPSLDataFlagAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }       

        $scope.individual_repayment = function () {
            $location.url('app/MstCreditIndividualRepaymentAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_observation = function () {
            $location.url('app/MstCreditIndividualObservationAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.creditexistingbank_edit = function (existingbankfacility_gid) {
            $location.url('app/MstCreditIndividualExistingBankEdit?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&existingbankfacility_gid=' + existingbankfacility_gid + '&lspage=' + lspage);
        }
        //KYC API
        $scope.iecdetailed_vertification = function () {
            $location.url('app/MstCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=IECDETAILED' + '&lspage=' + lspage);
        }
        $scope.fssai_vertification = function () {
            $location.url('app/MstCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=FSSAI' + '&lspage=' + lspage);
        }
        $scope.fda_vertification = function () {
            $location.url('app/MstCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=FDA' + '&lspage=' + lspage);
        }
        $scope.lpgid_vertification = function () {
            $location.url('app/MstCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=LPGID' + '&lspage=' + lspage);
        }
        $scope.shop_vertification = function () {
            $location.url('app/MstCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=SHOP' + '&lspage=' + lspage);
        }
        $scope.rcauthadv_vertification = function () {
            $location.url('app/MstCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=RCAUTHADV' + '&lspage=' + lspage);
        }
        $scope.rcsearch_vertification = function () {
            $location.url('app/MstCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=RCSEARCH' + '&lspage=' + lspage);
        }
        $scope.propertytax_vertification = function () {
            $location.url('app/MstCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=PROPERTYTAX' + '&lspage=' + lspage);
        }
        $scope.company_bankstatement = function () {
            $location.url('app/MstCreditIndividualBankStatementAnalysisAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }
        $scope.crimecheck_record = function () {
            $location.url('app/MstCreditCrimeCheckRecordAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=PROPERTYTAX' + '&lspage=' + lspage);
        }
        $scope.crimecheckreport_realtime = function () {
            $location.url('app/MstCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=CRIMECHECKREPORTREALTIME' + '&lspage=' + lspage);
        }
    }
})();