(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditSuppliersDtlEditController', MstCreditSuppliersDtlEditController);

        MstCreditSuppliersDtlEditController.$inject = ['$rootScope', '$sce', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function MstCreditSuppliersDtlEditController($rootScope, $sce, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCreditSuppliersDtlEditController';
        $scope.creditsupplier_gid = $location.search().creditsupplier_gid;
        var creditsupplier_gid = $scope.creditsupplier_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.institution_gid = $location.search().institution_gid;
        var institution_gid = $scope.institution_gid;
        $scope.lspage = $location.search().lspage;
       
        activate();
        function activate() { 

            $scope.creditsupplier_gid = $location.search().creditsupplier_gid;
           
            var param = {
                creditsupplier_gid: $scope.creditsupplier_gid
             }
            var url = 'api/MstAppCreditUnderWriting/GetSupplierList';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.supplier_list = resp.data.supplier_list;
              });
            var url = 'api/MstAppCreditUnderWriting/EditGetCreditSupplier';
           
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.cboSupplierName = resp.data.supplier_gid;
                $scope.txtRelationshipVintageYear = resp.data.relationship_vintage_year;
                $scope.txtRelationshipVintagemonth = resp.data.relationship_vintage_month;               
                $scope.txtEnd_Date = resp.data.end_date;
                $scope.txtSamplePurchaseamt = resp.data.purchase_amount;
                $scope.txtPurchaseamt = (parseInt($scope.txtSamplePurchaseamt.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountseperator = (parseInt($scope.txtPurchaseamt.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                document.getElementById('words_totalamount1').innerHTML = $scope.lblamountwords;

                $scope.txtSampleBankDebitamt = resp.data.bankdebit_amount;
                $scope.txtBankDebitamt = (parseInt($scope.txtSampleBankDebitamt.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountseperator = (parseInt($scope.txtBankDebitamt.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                document.getElementById('words_totalamount2').innerHTML = $scope.lblamountwords;



                $scope.txtrelationship_supplier = resp.data.relationship_supplier;
                $scope.txtStart_Date = resp.data.start_date;
                $scope.txtStart_Date = Date.parse($scope.txtStart_Date);
                $scope.txtEnd_Date = resp.data.end_date;
                $scope.txtEnd_Date = Date.parse($scope.txtEnd_Date);
                unlockUI();
            });  
            var url = 'api/MstAppCreditUnderWriting/GetCreditAccountType';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.accounttype_list = resp.data.creditbankacc_list;
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
        
        $scope.BankDebitamt = function () {

            var input = document.getElementById('BankDebitamt').value;
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
                    $scope.txtBankDebitamt = output;
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
        $scope.supplierdtl_Back = function () {
            $location.url('app/MstCreditSuppliersDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + $scope.lspage);
        }

        $scope.update_supplierdtl = function () {
            if (($scope.cboSupplierName == undefined) || ($scope.txtRelationshipVintageYear == undefined) || ($scope.txtRelationshipVintagemonth == undefined) || ($scope.txtStart_Date == undefined) || ($scope.txtEnd_Date == undefined) || ($scope.txtPurchaseamt == undefined) ||
            ($scope.txtBankDebitamt == undefined) || ($scope.txtrelationship_supplier == undefined) )
           {
              Notify.alert('Enter All Mandatory Fields','warning');
          }
            else {

                var supplier_Name = $('#SupplierName :selected').text();
                var params = {
                    creditsupplier_gid:creditsupplier_gid,
                applicant_type:'Institution',
                 supplier_gid : $scope.cboSupplierName,
                 supplier_name: supplier_Name,
                 relationship_vintage_year: $scope.txtRelationshipVintageYear,
                 relationship_vintage_month: $scope.txtRelationshipVintagemonth,
                 startdate: $scope.txtStart_Date,
                 enddate: $scope.txtEnd_Date,
                 purchase_amount: $scope.txtPurchaseamt,
                 bankdebit_amount : $scope.txtBankDebitamt,
                 relationship_supplier : $scope.txtrelationship_supplier
             }
             var url = 'api/MstAppCreditUnderWriting/UpdateCreditSupplier';
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
                $location.url('app/MstCreditSuppliersDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + $scope.lspage);
            });
    }
} 
       
    }
})();

