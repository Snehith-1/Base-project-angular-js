(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnCreditBuyerDtlEditController', AgrTrnCreditBuyerDtlEditController);

        AgrTrnCreditBuyerDtlEditController.$inject = ['$rootScope', '$sce', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function AgrTrnCreditBuyerDtlEditController($rootScope, $sce, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnCreditBuyerDtlEditController';
        $scope.institution_gid = $location.search().institution_gid;
        var institution_gid = $scope.institution_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.creditbuyer_gid = $location.search().creditbuyer_gid;
        var creditbuyer_gid = $scope.creditbuyer_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        activate();
        function activate() { 

            $scope.creditbuyer_gid = $location.search().creditbuyer_gid;
           
            var param = {
                creditbuyer_gid: $scope.creditbuyer_gid
             }
            var url = 'api/AgrTrnAppCreditUnderWriting/GetCreBuyerList';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.buyer_list = resp.data.creditbuyer_list;
            });
            var url = 'api/AgrTrnAppCreditUnderWriting/EditGetCreditBuyer';
           
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.cboBuyerName = resp.data.buyer_gid;
                $scope.txtBuyer_limit = resp.data. buyer_limit;
                $scope.txtAvailed_limit = resp.data.availed_limit;
                $scope.txtBalance_limit = resp.data.balance_limit; 
                $scope.cboTopBuyer = resp.data.top_buyer;
                $scope.txtBillTenuredays = resp.data.bill_tenuredays;
                $scope.txtMargin = resp.data.margin;
                $scope.txtRelationshipVintageyear = resp.data.relationship_vintage_year;
                $scope.txtRelationshipVintagemonth = resp.data.relationship_vintage_month;
                $scope.txtStart_Date = resp.data.start_date;
                $scope.txtStart_Date = Date.parse($scope.txtStart_Date);
                $scope.txtEnd_Date = resp.data.end_date;
                $scope.txtEnd_Date = Date.parse($scope.txtEnd_Date);
                $scope.txtPurchaseamt = resp.data.purchase_amount;
                if($scope.txtPurchaseamt!=null && $scope.txtPurchaseamt!=undefined && $scope.txtPurchaseamt!="")
                {
                    $scope.txtannual_income_edit = (parseInt($scope.txtPurchaseamt.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountseperator = (parseInt($scope.txtannual_income_edit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                    document.getElementById('words_totalamount1').innerHTML = $scope.lblamountwords;
                }
                
                $scope.txtBankCredit_Date = resp.data.bankcredit_date;
                $scope.txtBankCredit_Date = Date.parse($scope.txtBankCredit_Date);
                $scope.txtBankCreditValue = resp.data.bankcredit_value;
                if($scope.txtBankCreditValue!=null && $scope.txtBankCreditValue!=undefined && $scope.txtBankCreditValue!="")
                {
                    $scope.txtannual_income_edit = (parseInt($scope.txtBankCreditValue.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountseperator = (parseInt($scope.txtannual_income_edit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                    document.getElementById('words_totalamount2').innerHTML = $scope.lblamountwords;
                }
                
                $scope.rdbDeductionat_Source = resp.data.source_deduction;
                $scope.txtrelationship_borrower = resp.data.relationship_borrower; 
                $scope.txtEndUse_Monitoring = resp.data.enduse_monitoring;
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

        function defaultamountwordschange(input) {
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords = cmnfunctionService.fnConvertNumbertoWord(str);
            return lswords;
        }
      
        $scope.buyerdtl_Back = function () {
            $location.url('app/AgrTrnCreditBuyerDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.buyer = function () {
            var params = {
                buyer_gid: $scope.cboBuyerName.buyer_gid,
            }
            var url = 'api/AgrMstApplicationAdd/GetBuyerInfo';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtbuyer_limit = resp.data.buyer_limit;
            });
        }
        
        $scope.BankCreditValue = function () {

            var input = document.getElementById('BankCreditValue').value;
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
                    $scope.txtBankCreditValue = output;
                    document.getElementById('words_totalamount2').innerHTML = lswords_totalamount2;
                }
            }
        }
        $scope.Purchaseamt = function () {

            var input = document.getElementById('Purchaseamt').value;
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
                    $scope.txtPurchaseamt = output;
                    document.getElementById('words_totalamount1').innerHTML = lswords_totalamount1;
                }
            }
        }
        $scope.update_buyerdtl = function () {
            if (($scope.cboBuyerName == undefined) || ($scope.txtBuyer_limit == undefined) || ($scope.txtAvailed_limit == undefined) || ($scope.txtBalance_limit == undefined) || ($scope.cboTopBuyer == undefined) || ($scope.txtBillTenuredays == undefined) ||
            ($scope.txtMargin == undefined) || ($scope.txtRelationshipVintageyear == undefined) || ($scope.txtRelationshipVintagemonth == undefined) || ($scope.txtStart_Date == undefined) || ($scope.txtEnd_Date == undefined) || ($scope.txtPurchaseamt == undefined) || ($scope.txtBankCredit_Date == undefined) ||
            ($scope.txtBankCreditValue == undefined) || ($scope.rdbDeductionat_Source == undefined) || ($scope.txtrelationship_borrower == undefined) || ($scope.txtEndUse_Monitoring == undefined))
           {
              Notify.alert('Enter All Mandatory Fields','warning');
          }
          else {
             var params = {
                 creditbuyer_gid: creditbuyer_gid,
                 applicant_type:'Institution',
                 buyer_gid : $scope.cboBuyerName.buyer_gid,
                 buyer_name : $scope.cboBuyerName.buyer_name,
                 buyer_limit: $scope.txtBuyer_limit,
                 availed_limit : $scope.txtAvailed_limit,
                 balance_limit : $scope.txtBalance_limit,
                 top_buyer: $scope.cboTopBuyer,
                 bill_tenuredays : $scope.txtBillTenuredays,
                 margin : $scope.txtMargin,
                 relationship_vintage_year : $scope.txtRelationshipVintageyear,
                 relationship_vintage_month : $scope.txtRelationshipVintagemonth,
                 startdate : $scope.txtStart_Date,
                 enddate : $scope.txtEnd_Date,
                 purchase_amount : $scope.txtPurchaseamt,
                 bankcreditdate : $scope.txtBankCredit_Date,
                 bankcredit_value : $scope.txtBankCreditValue,
                 source_deduction : $scope.rdbDeductionat_Source,
                 relationship_borrower : $scope.txtrelationship_borrower,
                 enduse_monitoring : $scope.txtEndUse_Monitoring
             }
             var url = 'api/AgrTrnAppCreditUnderWriting/UpdateCreditBuyer';
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
                $location.url('app/AgrTrnCreditBuyerDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
            });
    }
} 
       
    }
})();

