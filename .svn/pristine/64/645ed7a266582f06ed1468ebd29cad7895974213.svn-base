(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditSuppliersDtlAddController', MstCreditSuppliersDtlAddController);

        MstCreditSuppliersDtlAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','$sce','cmnfunctionService'];

    function MstCreditSuppliersDtlAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCreditSuppliersDtlAddController';
        $scope.institution_gid = $location.search().institution_gid;
        var institution_gid = $scope.institution_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
       
        activate();
        function activate() {

            var params = {
                credit_gid: institution_gid
             }
            
            var url = 'api/MstAppCreditUnderWriting/GetSupplierList';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.supplier_list = resp.data.supplier_list;
            });
            var url = 'api/MstAppCreditUnderWriting/GetCreditSupplierList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.institutionsupplier_list = resp.data.supplier_list;
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
                credit_gid: institution_gid,
                applicant_type: 'Institution'
                }

              var url = 'api/MstAppCreditUnderWriting/GetCreditOperationsView';
              lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtinstitution_name = resp.data.company_name;
                $scope.txtstakeholder_type = resp.data.stakeholder_type;
            }); 

        }
      
       
        $scope.BankDebitamt = function () {

            var input = document.getElementById('BankDebitamt').value;
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
        $scope.add_creditsupplierdtl = function () {
            if (($scope.cboSupplierName == undefined) || ($scope.txtRelationshipVintageYear == undefined) || ($scope.txtRelationshipVintagemonth == undefined) || ($scope.txtStart_Date == undefined) || ($scope.txtEnd_Date == undefined) || ($scope.txtPurchaseamt == undefined) ||
            ($scope.txtBankDebitamt == undefined) || ($scope.txtrelationship_supplier == undefined) ||
            ($scope.cboSupplierName == '' ) || ($scope.txtRelationshipVintageYear == '') || ($scope.txtRelationshipVintagemonth == '') || ($scope.txtStart_Date == '') || ($scope.txtEnd_Date == '') || ($scope.txtPurchaseamt == '') ||
            ($scope.txtBankDebitamt == '') || ($scope.txtrelationship_supplier == '')
            )
           {
              Notify.alert('Enter All Mandatory Fields','warning');
          }
          else {
             var params = {
                 application_gid: application_gid,
                 credit_gid: institution_gid,
                 applicant_type:'Institution',
                 supplier_gid : $scope.cboSupplierName.supplier_gid,
                 supplier_name : $scope.cboSupplierName.supplier_name,
                 relationship_vintage_year: $scope.txtRelationshipVintageYear,
                 relationship_vintage_month: $scope.txtRelationshipVintagemonth,
                 start_date: $scope.txtStart_Date,
                 end_date: $scope.txtEnd_Date,
                 purchase_amount: $scope.txtPurchaseamt,
                 bankdebit_amount : $scope.txtBankDebitamt,
                 relationship_supplier : $scope.txtrelationship_supplier
             }
             var url = 'api/MstAppCreditUnderWriting/PostCreditSupplier';
              lockUI();
              SocketService.post(url, params).then(function (resp) {
                  unlockUI();
                  if (resp.data.status == true) {
                      $scope.institutionsupplier_list = resp.data.supplier_list;
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
                  }
                  $scope.cboSupplierName = '';
                  $scope.txtRelationshipVintageYear = '';
                  $scope.txtRelationshipVintagemonth = '';
                  $scope.txtStart_Date = '';
                  $scope.txtEnd_Date = '';
                  $scope.txtPurchaseamt = '';
                  $scope.txtBankDebitamt = '';
                  $scope.txtrelationship_supplier = '';
                  document.getElementById('words_totalamount2').innerHTML = '';
                  document.getElementById('words_totalamount1').innerHTML = '';
              }); 
          }
      }

        $scope.creditsupplier_delete = function (creditsupplier_gid) {
        var params = {
            creditsupplier_gid: creditsupplier_gid,
            credit_gid: institution_gid
        }
        var url = 'api/MstAppCreditUnderWriting/DeleteCreditSupplier';
        lockUI();
        SocketService.getparams(url, params).then(function (resp) {
            unlockUI();
            if (resp.data.status == true) {
                $scope.institutionsupplier_list=resp.data.supplier_list;
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

        $scope.creditsupplier_edit = function (creditsupplier_gid) {
            $location.url('app/MstCreditSuppliersDtlEdit?application_gid=' + application_gid + '&creditsupplier_gid=' + creditsupplier_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
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
