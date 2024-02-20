(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCADCreditExistingBankDtlEditController', MstCADCreditExistingBankDtlEditController);

        MstCADCreditExistingBankDtlEditController.$inject = ['$rootScope', '$sce', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function MstCADCreditExistingBankDtlEditController($rootScope, $sce, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCADCreditExistingBankDtlEditController';
        $scope.institution_gid = $location.search().institution_gid;
        var institution_gid = $scope.institution_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.existingbankfacility_gid = $location.search().existingbankfacility_gid;
        var existingbankfacility_gid = $scope.existingbankfacility_gid;
       
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

            var param = {
                existingbankfacility_gid: existingbankfacility_gid
             }
             
            var url = 'api/MstCADCreditAction/EditExistingBankFacility';
           
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.cboBankName = resp.data.bank_gid;
                $scope.cboFacilityType = resp.data.facilitytype_gid;
                $scope.txtfacilitysanction_date = resp.data.facilitysanctioned_on;
                $scope.cboTypeofexistingfunded = resp.data.fundedtypeindicator_gid;
          
                $scope.cboInstalmentfrequency = resp.data.instalmentfrequency_gid;
                
                $scope.txtRecord_Date = resp.data.record_date;

                //$scope.txtSanctioned_Limit = resp.data.sanctioned_limit;
                //$scope.txtInstalment_amount = resp.data.instalment_amount;
                //$scope.txtOutstanding_amount = resp.data.outstanding_amount;
                //$scope.txtOverdue_amount = resp.data.overdue_amount;
                //$scope.txtoverdue_dpd = resp.data.overdue_dpd;



                $scope.txtSampleSanctioned_Limit = resp.data.sanctioned_limit;
                $scope.txtSanctioned_Limit = (parseInt($scope.txtSampleSanctioned_Limit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountseperator = (parseInt($scope.txtSanctioned_Limit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                document.getElementById('words_totalamount4').innerHTML = $scope.lblamountwords;

                $scope.txtSampleInstalment_amount = resp.data.instalment_amount;
                $scope.txtInstalment_amount = (parseInt($scope.txtSampleInstalment_amount.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountseperator = (parseInt($scope.txtInstalment_amount.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                document.getElementById('words_totalamount1').innerHTML = $scope.lblamountwords;

                $scope.txtSampleOutstanding_amount = resp.data.outstanding_amount;
                $scope.txtOutstanding_amount = (parseInt($scope.txtSampleOutstanding_amount.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountseperator = (parseInt($scope.txtOutstanding_amount.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                document.getElementById('words_totalamount2').innerHTML = $scope.lblamountwords;

                $scope.txtSampleOverdue_amount = resp.data.overdue_amount;
                $scope.txtOverdue_amount = (parseInt($scope.txtSampleOverdue_amount.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountseperator = (parseInt($scope.txtOverdue_amount.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                document.getElementById('words_totalamount3').innerHTML = $scope.lblamountwords;

                $scope.txtSampleoverdue_dpd = resp.data.overdue_dpd;
                $scope.txtoverdue_dpd = (parseInt($scope.txtSampleoverdue_dpd.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountseperator = (parseInt($scope.txtoverdue_dpd.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                document.getElementById('words_totalamount5').innerHTML = $scope.lblamountwords;
                $scope.cboAccountClassification = resp.data.accountclassification_gid;
                $scope.txtremarks = resp.data.remarks;
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
        }


        function defaultamountwordschange(input) {
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords = cmnfunctionService.fnConvertNumbertoWord(str);
            return lswords;
        }
        $scope.ExistingBankacctdtl_Back = function () {
            $location.url('app/MstCADCreditExistingBankDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=CADAcceptanceCustomers');
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

        

        $scope.update_ExistingBankacctdtl = function () {
            if (($scope.cboBankName == undefined) || ($scope.cboBankName == '') ) {
                Notify.alert('Enter All Mandatory Fields', 'warning');
            }
            else {
            var bankdtlname = $('#bankdtl_name :selected').text();
            var creditunderwritingfacilitytype = $('#credit_underwriting_facility_type :selected').text();
            var fundedtypeindicatorname = $('#fundedtypeindicator_name :selected').text();
            var creditinstalmentfrequencyname = $('#creditinstalmentfrequency_name :selected').text();
            var creditaccountclassificationname = $('#creditaccountclassification_name :selected').text();

             var params = {
                 application_gid: application_gid,
                 credit_gid: institution_gid,
                 applicant_type: 'Institution',
                 bank_gid: $scope.cboBankName,
                 bank_name: bankdtlname,
                 facilitytype_gid: $scope.cboFacilityType,
                 facility_type: creditunderwritingfacilitytype,
                 facilitysanctionedon: $scope.txtfacilitysanction_date,
                 fundedtypeindicator_gid: $scope.cboTypeofexistingfunded,
                 fundedtypeindicator_name: fundedtypeindicatorname,
                 sanctioned_limit: $scope.txtSanctioned_Limit,
                 instalmentfrequency_gid: $scope.cboInstalmentfrequency,
                 instalmentfrequency_name: creditinstalmentfrequencyname,
                 instalment_amount: $scope.txtInstalment_amount,
                 outstanding_amount: $scope.txtOutstanding_amount,
                 recorddate: $scope.txtRecord_Date,
                 overdue_amount: $scope.txtOverdue_amount,
                 overdue_dpd: $scope.txtoverdue_dpd,
                 accountclassification_gid: $scope.cboAccountClassification,
                 account_classification: creditaccountclassificationname,
                 remarks: $scope.txtremarks,
                 existingbankfacility_gid: existingbankfacility_gid,
             }
             var url = 'api/MstCADCreditAction/UpdateExistingBankFacility';
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
                $location.url('app/MstCADCreditExistingBankDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=CADAcceptanceCustomers');
            }); 
        }   
        } 
    }
})();

