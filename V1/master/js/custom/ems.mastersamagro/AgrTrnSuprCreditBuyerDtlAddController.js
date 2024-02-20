(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnSuprCreditBuyerDtlAddController', AgrTrnSuprCreditBuyerDtlAddController);

    AgrTrnSuprCreditBuyerDtlAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce','cmnfunctionService'];

    function AgrTrnSuprCreditBuyerDtlAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnSuprCreditBuyerDtlAddController';
        $scope.institution_gid = $location.search().institution_gid;
        var institution_gid = $scope.institution_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        lockUI();
        activate();
        function activate() {
            $scope.institution_gid = $location.search().institution_gid;
            var params = {
                credit_gid: $scope.institution_gid
            }
            var url = 'api/AgrTrnSuprAppCreditUnderWriting/GetCreBuyerList';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.buyer_list = resp.data.creditbuyer_list;
            });

            var url = 'api/AgrTrnSuprAppCreditUnderWriting/GetCreditBuyerList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.institutionbuyer_list = resp.data.creditbuyer_list;
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

            var params = {
                credit_gid: institution_gid,
                applicant_type: 'Institution'
            }

            var url = 'api/AgrTrnSuprAppCreditUnderWriting/GetCreditOperationsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtinstitution_name = resp.data.company_name;
                $scope.txtstakeholder_type = resp.data.stakeholder_type;
            });

        }
        $scope.buyer = function () {
            var params = {
                buyer_gid: $scope.cboBuyerName.buyer_gid,
            }
            var url = 'api/AgrMstSuprApplicationAdd/GetBuyerInfo';
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
        $scope.add_creditbuyerdtl = function () {
            if (($scope.cboBuyerName == undefined) || ($scope.cboTopBuyer == undefined) || ($scope.txtBillTenuredays == undefined) ||
            ($scope.txtMargin == undefined) || ($scope.txtRelationshipVintageyear == undefined) || ($scope.txtRelationshipVintagemonth == undefined) || ($scope.txtStart_Date == undefined) || ($scope.txtEnd_Date == undefined) || ($scope.txtPurchaseamt == undefined) || ($scope.txtBankCredit_Date == undefined) ||
            ($scope.txtBankCreditValue == undefined) || ($scope.rdbDeductionat_Source == undefined) ||
            ($scope.cboBuyerName == '') || ($scope.cboTopBuyer == '') || ($scope.txtBillTenuredays == '') ||
            ($scope.txtMargin == '') || ($scope.txtRelationshipVintageyear == '') || ($scope.txtRelationshipVintagemonth == '') || ($scope.txtStart_Date == '') || ($scope.txtEnd_Date == '') || ($scope.txtPurchaseamt == '') || ($scope.txtBankCredit_Date == '') ||
            ($scope.txtBankCreditValue == '') || ($scope.rdbDeductionat_Source == '')) {
                Notify.alert('Enter All Mandatory Fields', 'warning');
            }
            else {
                var params = {
                    application_gid: application_gid,
                    credit_gid: institution_gid,
                    applicant_type: 'Institution',
                    buyer_gid: $scope.cboBuyerName.buyer_gid,
                    buyer_name: $scope.cboBuyerName.buyer_name,
                    buyer_limit: $scope.txtbuyer_limit,
                    availed_limit: $scope.txtAvailed_limit,
                    balance_limit: $scope.txtBalance_limit,
                    top_buyer: $scope.cboTopBuyer,
                    bill_tenuredays: $scope.txtBillTenuredays,
                    margin: $scope.txtMargin,
                    relationship_vintage_year: $scope.txtRelationshipVintageyear,
                    relationship_vintage_month: $scope.txtRelationshipVintagemonth,
                    start_date: $scope.txtStart_Date,
                    end_date: $scope.txtEnd_Date,
                    purchase_amount: $scope.txtPurchaseamt,
                    bankcredit_date: $scope.txtBankCredit_Date,
                    bankcredit_value: $scope.txtBankCreditValue,
                    source_deduction: $scope.rdbDeductionat_Source,
                    relationship_borrower: $scope.txtrelationship_borrower,
                    enduse_monitoring: $scope.txtEndUse_Monitoring
                }
                var url = 'api/AgrTrnSuprAppCreditUnderWriting/PostCreditBuyer';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        $scope.institutionbuyer_list = resp.data.creditbuyer_list;
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
                    $scope.cboBuyerName = '';
                    $scope.txtbuyer_limit = '';
                    $scope.txtAvailed_limit = '';
                    $scope.txtBalance_limit = '';
                    $scope.cboTopBuyer = '';
                    $scope.txtBillTenuredays = '';
                    $scope.txtMargin = '';
                    $scope.txtRelationshipVintageyear = '';
                    $scope.txtRelationshipVintagemonth = '';
                    $scope.txtStart_Date = '';
                    $scope.txtEnd_Date = '';
                    $scope.txtPurchaseamt = '';
                    $scope.txtBankCredit_Date = '';
                    $scope.txtBankCreditValue = '';
                    $scope.rdbDeductionat_Source = '';
                    $scope.txtrelationship_borrower = '';
                    $scope.txtEndUse_Monitoring = '';
                    document.getElementById('words_totalamount2').innerHTML = '';
                    document.getElementById('words_totalamount1').innerHTML = '';
                });
            }
        }

        $scope.creditbuyer_delete = function (creditbuyer_gid) {
            var params = {
                creditbuyer_gid: creditbuyer_gid,
                credit_gid: institution_gid
            }
            var url = 'api/AgrTrnSuprAppCreditUnderWriting/DeleteCreditBuyer';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.institutionbuyer_list = resp.data.creditbuyer_list;
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
            if (lspage == "StartCreditUnderwriting") {
                $location.url('app/AgrTrnSuprStartCreditUnderwriting?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "PendingCADReview") {
                $location.url('app/AgrTrnSuprCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "CADApplicationEdit") {
                $location.url('app/AgrTrnSuprCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "CADAcceptanceCustomers") {
                $location.url('app/AgrTrnSuprCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else {

            }
        }

        $scope.company_deferraldocchecklist = function () {
            $location.url('app/AgrTrnSuprDocumentCheckList?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_covenantdocchecklist = function () {
            $location.url('app/AgrTrnSuprCreditAddCovenantCheckList?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_economicadd = function () {
            $location.url('app/AgrTrnSuprCreditEconomicCapitalAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_geneticadd = function () {
            $location.url('app/AgrTrnSuprCreditCompanyDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_PSLdata = function () {
            $location.url('app/AgrTrnSuprCreditPSLDataFlaggingAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_supplier = function () {
            $location.url('app/AgrTrnSuprCreditSuppliersDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_buyer = function () {
            $location.url('app/AgrTrnSuprCreditBuyerDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_bankaccount = function () {
            $location.url('app/AgrTrnSuprCreditBankAccountDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_existingbankaccount = function () {
            $location.url('app/AgrTrnSuprCreditExistingBankDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_repayment = function () {
            $location.url('app/AgrTrnSuprCreditRepaymentDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_observation = function () {
            $location.url('app/AgrTrnSuprCreditObservationAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.creditbuyer_edit = function (creditbuyer_gid) {
            $location.url('app/AgrTrnSuprCreditBuyerDtlEdit?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&creditbuyer_gid=' + creditbuyer_gid + '&lspage=' + lspage);
        }

        $scope.tan_verification = function () {
            $location.url('app/AgrTrnSuprCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=TAN' + '&lspage=' + lspage);
        }
        $scope.companyllpno_vertification = function () {
            $location.url('app/AgrTrnSuprCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=LLPNO' + '&lspage=' + lspage);
        }
        $scope.mcasign_vertification = function () {
            $location.url('app/AgrTrnSuprCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=MCASIGNATURE' + '&lspage=' + lspage);
        }
        $scope.iecdetailed_vertification = function () {
            $location.url('app/AgrTrnSuprCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=IECDETAILED' + '&lspage=' + lspage);
        }
        $scope.fssai_vertification = function () {
            $location.url('app/AgrTrnSuprCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=FSSAI' + '&lspage=' + lspage);
        }
        $scope.fda_vertification = function () {
            $location.url('app/AgrTrnSuprCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=FDA' + '&lspage=' + lspage);
        }
        $scope.gst_verification = function () {
            $location.url('app/AgrTrnSuprCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=GST' + '&lspage=' + lspage);
        }
        $scope.lpgid_verification = function () {
            $location.url('app/AgrTrnSuprCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=LPGID' + '&lspage=' + lspage);
        }
        $scope.shop_vertification = function () {
            $location.url('app/AgrTrnSuprCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=SHOP' + '&lspage=' + lspage);
        }
        $scope.rcauthadv_vertification = function () {
            $location.url('app/AgrTrnSuprCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=RCAUTHADV' + '&lspage=' + lspage);
        }
        $scope.rcsearch_vertification = function () {
            $location.url('app/AgrTrnSuprCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=RCSEARCH' + '&lspage=' + lspage);
        }
        $scope.propertytax_vertification = function () {
            $location.url('app/AgrTrnSuprCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=PROPERTYTAX' + '&lspage=' + lspage);
        }
        $scope.company_bankstatement = function () {
            $location.url('app/AgrTrnSuprCreditBankStatementAnalysisAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.company_fsa = function () {
            $location.url('app/AgrTrnSuprCreditFsaDetailAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.institution_bureauadd = function () {
            $location.url('app/AgrTrnSuprCreditInstitutionDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.crimecheck_record = function () {
            $location.url('app/AgrTrnSuprCompanyCrimeCheckRecordAPI?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.crimecheckreport_realtime = function () {
            $location.url('app/AgrTrnSuprCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=CRIMECHECKREPORTREALTIME' + '&lspage=' + lspage);
        }
    }
})();
