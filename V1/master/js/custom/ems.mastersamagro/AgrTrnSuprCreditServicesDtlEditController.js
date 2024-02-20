(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnSuprCreditServicesDtlEditController', AgrTrnSuprCreditServicesDtlEditController);

    AgrTrnSuprCreditServicesDtlEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function AgrTrnSuprCreditServicesDtlEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnSuprCreditServicesDtlEditController';

        $scope.application2servicecharge_gid = $location.search().application2servicecharge_gid;
        var application2servicecharge_gid = $scope.application2servicecharge_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.product_gid = $location.search().product_gid;
        var product_gid = $scope.product_gid;
        $scope.variety_gid = $location.search().variety_gid;
        var variety_gid = $scope.variety_gid;

        activate();
        function activate() {
            var param = {
                application2servicecharge_gid: $scope.application2servicecharge_gid
            }
            var url = 'api/AgrTrnSuprAppCreditUnderWriting/ServicechargeEdit';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.txtprocessing_fee = resp.data.processing_fee;
                if($scope.txtprocessing_fee!=null && $scope.txtprocessing_fee!=undefined && $scope.txtprocessing_fee!="")
                {
                    $scope.txtannual_income_edit = (parseInt($scope.txtprocessing_fee.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountseperator = (parseInt($scope.txtannual_income_edit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                    document.getElementById('words_totalamount51').innerHTML = $scope.lblamountwords;
                }
                
                $scope.rdbprocessing_collectiontype = resp.data.processing_collectiontype;
                $scope.txtdoc_charges = resp.data.doc_charges;
                if($scope.txtdoc_charges!=null && $scope.txtdoc_charges!=undefined && $scope.txtdoc_charges!="")
                {
                    $scope.txtannual_income_edit = (parseInt($scope.txtdoc_charges.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountseperator = (parseInt($scope.txtannual_income_edit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                    document.getElementById('words_totalamount52').innerHTML = $scope.lblamountwords;
                }
                
                $scope.rdbdoccharge_collectiontype = resp.data.doccharge_collectiontype;
                $scope.txtfieldvisit_charges = resp.data.fieldvisit_charge;
                if($scope.txtfieldvisit_charges!=null && $scope.txtfieldvisit_charges!=undefined && $scope.txtfieldvisit_charges!="")
                {
                    $scope.txtannual_income_edit = (parseInt($scope.txtfieldvisit_charges.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountseperator = (parseInt($scope.txtannual_income_edit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                    document.getElementById('words_totalamount53').innerHTML = $scope.lblamountwords;
                }
                
                $scope.rdbfieldvisit_collectiontype = resp.data.fieldvisit_charges_collectiontype;
                $scope.txtadhoc_fee = resp.data.adhoc_fee;
                if($scope.txtadhoc_fee!=null && $scope.txtadhoc_fee!=undefined && $scope.txtadhoc_fee!="")
                {
                    $scope.txtannual_income_edit = (parseInt($scope.txtadhoc_fee.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountseperator = (parseInt($scope.txtannual_income_edit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                    document.getElementById('words_totalamount54').innerHTML = $scope.lblamountwords;
                }
                
                $scope.rdbadhoc_collectiontype = resp.data.adhoc_collectiontype;
                $scope.txtlife_insurance = resp.data.life_insurance;
                if($scope.txtlife_insurance!=null && $scope.txtlife_insurance!=undefined && $scope.txtlife_insurance!="")
                {
                    $scope.txtannual_income_edit = (parseInt($scope.txtlife_insurance.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountseperator = (parseInt($scope.txtannual_income_edit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                    document.getElementById('words_totalamount55').innerHTML = $scope.lblamountwords;
                }
                
                $scope.rdblifeinsurance_collectiontype = resp.data.lifeinsurance_collectiontype;
                $scope.rdbpersonalaccident_collectiontype = resp.data.acctinsurance_collectiontype;
                $scope.txtacct_insurance = resp.data.acct_insurance;
                if($scope.txtacct_insurance!=null && $scope.txtacct_insurance!=undefined && $scope.txtacct_insurance!="")
                {
                    $scope.txtannual_income_edit = (parseInt($scope.txtacct_insurance.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountseperator = (parseInt($scope.txtannual_income_edit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                    document.getElementById('words_totalamount56').innerHTML = $scope.lblamountwords;
                }
                
                document.getElementById("total_collect").value = resp.data.total_collect;
                document.getElementById("total_deduct").value = resp.data.total_deduct;
                $scope.txtproduct_type = resp.data.product_type;
            });


        }

        function defaultamountwordschange(input) {
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords = cmnfunctionService.fnConvertNumbertoWord(str);
            return lswords;
        }

        
        $scope.txtamountchange1 = function () {
            var input = document.getElementById('processingfee').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount51 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtprocessing_fee = "";
            }
            else {
                //   $scope.txtprocessing_fee = output;
                document.getElementById('words_totalamount51').innerHTML = lswords_totalamount51;
            }
            var processing_fee = 0;
            var doc_charges = 0;
            var fieldvisit_charges = 0;
            var adhoc_fee = 0;
            var life_insurance = 0;
            var personal_accident = 0;

            if ($scope.rdbprocessing_collectiontype == 'Collect') {
                processing_fee = parseInt(document.getElementById("processingfee").value)
            }
            if ($scope.rdbdoccharge_collectiontype == 'Collect') {
                doc_charges = parseInt(document.getElementById("doc_charges").value)

            }
            if ($scope.rdbfieldvisit_collectiontype == 'Collect') {
                fieldvisit_charges = parseInt(document.getElementById("fieldvisit_charges").value)
            }
            if ($scope.rdbadhoc_collectiontype == 'Collect') {
                adhoc_fee = parseInt(document.getElementById("adhoc_fee").value)
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Collect') {
                life_insurance = parseInt(document.getElementById("life_insurance").value)
            }
            if ($scope.rdbpersonalaccident_collectiontype == 'Collect') {
                personal_accident = parseInt(document.getElementById("acct_insurance").value)
            }
            var result = processing_fee + doc_charges + fieldvisit_charges + adhoc_fee + life_insurance + personal_accident

            document.getElementById("total_collect").value = result;

            var processing_fee_deduct = 0;
            var doc_charges_deduct = 0;
            var fieldvisit_charges_deduct = 0;
            var adhoc_fee_deduct = 0;
            var life_insurance_deduct = 0;
            var personal_accident_deduct = 0;

            if ($scope.rdbprocessing_collectiontype == 'Deduct') {
                processing_fee_deduct = parseInt(document.getElementById("processingfee").value)
            }
            if ($scope.rdbdoccharge_collectiontype == 'Deduct') {
                doc_charges_deduct = parseInt(document.getElementById("doc_charges").value)

            }
            if ($scope.rdbfieldvisit_collectiontype == 'Deduct') {
                fieldvisit_charges_deduct = parseInt(document.getElementById("fieldvisit_charges").value)
            }
            if ($scope.rdbadhoc_collectiontype == 'Deduct') {
                adhoc_fee_deduct = parseInt(document.getElementById("adhoc_fee").value)
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Deduct') {
                life_insurance_deduct = parseInt(document.getElementById("life_insurance").value)
            }
            if ($scope.rdbpersonalaccident_collectiontype == 'Deduct') {
                personal_accident_deduct = parseInt(document.getElementById("acct_insurance").value)
            }
            var result_deduct = processing_fee_deduct + doc_charges_deduct + fieldvisit_charges_deduct + adhoc_fee_deduct + life_insurance_deduct + personal_accident_deduct

            document.getElementById("total_deduct").value = result_deduct;

        }

        $scope.txtamountchange2 = function () {
            var input = document.getElementById('doc_charges').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount52 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtdoc_charges = "";
            }
            else {
                //   $scope.txtprocessing_fee = output;
                document.getElementById('words_totalamount52').innerHTML = lswords_totalamount52;
            }
            var processing_fee = 0;
            var doc_charges = 0;
            var fieldvisit_charges = 0;
            var adhoc_fee = 0;
            var life_insurance = 0;
            var personal_accident = 0;

            if ($scope.rdbprocessing_collectiontype == 'Collect') {
                processing_fee = parseInt(document.getElementById("processingfee").value)
            }
            if ($scope.rdbdoccharge_collectiontype == 'Collect') {
                doc_charges = parseInt(document.getElementById("doc_charges").value)

            }
            if ($scope.rdbfieldvisit_collectiontype == 'Collect') {
                fieldvisit_charges = parseInt(document.getElementById("fieldvisit_charges").value)
            }
            if ($scope.rdbadhoc_collectiontype == 'Collect') {
                adhoc_fee = parseInt(document.getElementById("adhoc_fee").value)
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Collect') {
                life_insurance = parseInt(document.getElementById("life_insurance").value)
            }
            if ($scope.rdbpersonalaccident_collectiontype == 'Collect') {
                personal_accident = parseInt(document.getElementById("acct_insurance").value)
            }
            var result = processing_fee + doc_charges + fieldvisit_charges + adhoc_fee + life_insurance + personal_accident

            document.getElementById("total_collect").value = result;

            var processing_fee_deduct = 0;
            var doc_charges_deduct = 0;
            var fieldvisit_charges_deduct = 0;
            var adhoc_fee_deduct = 0;
            var life_insurance_deduct = 0;
            var personal_accident_deduct = 0;

            if ($scope.rdbprocessing_collectiontype == 'Deduct') {
                processing_fee_deduct = parseInt(document.getElementById("processingfee").value)
            }
            if ($scope.rdbdoccharge_collectiontype == 'Deduct') {
                doc_charges_deduct = parseInt(document.getElementById("doc_charges").value)

            }
            if ($scope.rdbfieldvisit_collectiontype == 'Deduct') {
                fieldvisit_charges_deduct = parseInt(document.getElementById("fieldvisit_charges").value)
            }
            if ($scope.rdbadhoc_collectiontype == 'Deduct') {
                adhoc_fee_deduct = parseInt(document.getElementById("adhoc_fee").value)
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Deduct') {
                life_insurance_deduct = parseInt(document.getElementById("life_insurance").value)
            }
            if ($scope.rdbpersonalaccident_collectiontype == 'Deduct') {
                personal_accident_deduct = parseInt(document.getElementById("acct_insurance").value)
            }
            var result_deduct = processing_fee_deduct + doc_charges_deduct + fieldvisit_charges_deduct + adhoc_fee_deduct + life_insurance_deduct + personal_accident_deduct

            document.getElementById("total_deduct").value = result_deduct;

        }

        $scope.txtamountchange3 = function () {
            var input = document.getElementById('fieldvisit_charges').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount53 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtfieldvisit_charges = "";
            }
            else {
                //   $scope.txtprocessing_fee = output;
                document.getElementById('words_totalamount53').innerHTML = lswords_totalamount53;
            }
            var processing_fee = 0;
            var doc_charges = 0;
            var fieldvisit_charges = 0;
            var adhoc_fee = 0;
            var life_insurance = 0;
            var personal_accident = 0;

            if ($scope.rdbprocessing_collectiontype == 'Collect') {
                processing_fee = parseInt(document.getElementById("processingfee").value)
            }
            if ($scope.rdbdoccharge_collectiontype == 'Collect') {
                doc_charges = parseInt(document.getElementById("doc_charges").value)

            }
            if ($scope.rdbfieldvisit_collectiontype == 'Collect') {
                fieldvisit_charges = parseInt(document.getElementById("fieldvisit_charges").value)
            }
            if ($scope.rdbadhoc_collectiontype == 'Collect') {
                adhoc_fee = parseInt(document.getElementById("adhoc_fee").value)
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Collect') {
                life_insurance = parseInt(document.getElementById("life_insurance").value)
            }
            if ($scope.rdbpersonalaccident_collectiontype == 'Collect') {
                personal_accident = parseInt(document.getElementById("acct_insurance").value)
            }
            var result = processing_fee + doc_charges + fieldvisit_charges + adhoc_fee + life_insurance + personal_accident

            document.getElementById("total_collect").value = result;

            var processing_fee_deduct = 0;
            var doc_charges_deduct = 0;
            var fieldvisit_charges_deduct = 0;
            var adhoc_fee_deduct = 0;
            var life_insurance_deduct = 0;
            var personal_accident_deduct = 0;

            if ($scope.rdbprocessing_collectiontype == 'Deduct') {
                processing_fee_deduct = parseInt(document.getElementById("processingfee").value)
            }
            if ($scope.rdbdoccharge_collectiontype == 'Deduct') {
                doc_charges_deduct = parseInt(document.getElementById("doc_charges").value)

            }
            if ($scope.rdbfieldvisit_collectiontype == 'Deduct') {
                fieldvisit_charges_deduct = parseInt(document.getElementById("fieldvisit_charges").value)
            }
            if ($scope.rdbadhoc_collectiontype == 'Deduct') {
                adhoc_fee_deduct = parseInt(document.getElementById("adhoc_fee").value)
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Deduct') {
                life_insurance_deduct = parseInt(document.getElementById("life_insurance").value)
            }
            if ($scope.rdbpersonalaccident_collectiontype == 'Deduct') {
                personal_accident_deduct = parseInt(document.getElementById("acct_insurance").value)
            }
            var result_deduct = processing_fee_deduct + doc_charges_deduct + fieldvisit_charges_deduct + adhoc_fee_deduct + life_insurance_deduct + personal_accident_deduct

            document.getElementById("total_deduct").value = result_deduct;

        }

        $scope.txtamountchange4 = function () {
            var input = document.getElementById('adhoc_fee').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount54 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtadhoc_fee = "";
            }
            else {
                //   $scope.txtprocessing_fee = output;
                document.getElementById('words_totalamount54').innerHTML = lswords_totalamount54;
            }
            var processing_fee = 0;
            var doc_charges = 0;
            var fieldvisit_charges = 0;
            var adhoc_fee = 0;
            var life_insurance = 0;
            if ($scope.rdbprocessing_collectiontype == 'Collect') {
                processing_fee = parseInt(document.getElementById("processingfee").value)
            }
            if ($scope.rdbdoccharge_collectiontype == 'Collect') {
                doc_charges = parseInt(document.getElementById("doc_charges").value)

            }
            if ($scope.rdbfieldvisit_collectiontype == 'Collect') {
                fieldvisit_charges = parseInt(document.getElementById("fieldvisit_charges").value)
            }
            if ($scope.rdbadhoc_collectiontype == 'Collect') {
                adhoc_fee = parseInt(document.getElementById("adhoc_fee").value)
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Collect') {
                life_insurance = parseInt(document.getElementById("life_insurance").value)
            }
            var result = processing_fee + doc_charges + fieldvisit_charges + adhoc_fee + life_insurance

            document.getElementById("total_collect").value = result;

            var processing_fee_deduct = 0;
            var doc_charges_deduct = 0;
            var fieldvisit_charges_deduct = 0;
            var adhoc_fee_deduct = 0;
            var life_insurance_deduct = 0;
            var personal_accident_deduct = 0;

            if ($scope.rdbprocessing_collectiontype == 'Deduct') {
                processing_fee_deduct = parseInt(document.getElementById("processingfee").value)
            }
            if ($scope.rdbdoccharge_collectiontype == 'Deduct') {
                doc_charges_deduct = parseInt(document.getElementById("doc_charges").value)

            }
            if ($scope.rdbfieldvisit_collectiontype == 'Deduct') {
                fieldvisit_charges_deduct = parseInt(document.getElementById("fieldvisit_charges").value)
            }
            if ($scope.rdbadhoc_collectiontype == 'Deduct') {
                adhoc_fee_deduct = parseInt(document.getElementById("adhoc_fee").value)
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Deduct') {
                life_insurance_deduct = parseInt(document.getElementById("life_insurance").value)
            }
            if ($scope.rdbpersonalaccident_collectiontype == 'Deduct') {
                personal_accident_deduct = parseInt(document.getElementById("acct_insurance").value)
            }
            var result_deduct = processing_fee_deduct + doc_charges_deduct + fieldvisit_charges_deduct + adhoc_fee_deduct + life_insurance_deduct + personal_accident_deduct

            document.getElementById("total_deduct").value = result_deduct;

        }

        $scope.txtamountchange5 = function () {
            var input = document.getElementById('life_insurance').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount55 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtlife_insurance = "";
            }
            else {
                //   $scope.txtprocessing_fee = output;
                document.getElementById('words_totalamount55').innerHTML = lswords_totalamount55;
            }
            var processing_fee = 0;
            var doc_charges = 0;
            var fieldvisit_charges = 0;
            var adhoc_fee = 0;
            var life_insurance = 0;
            if ($scope.rdbprocessing_collectiontype == 'Collect') {
                processing_fee = parseInt(document.getElementById("processingfee").value)
            }
            if ($scope.rdbdoccharge_collectiontype == 'Collect') {
                doc_charges = parseInt(document.getElementById("doc_charges").value)

            }
            if ($scope.rdbfieldvisit_collectiontype == 'Collect') {
                fieldvisit_charges = parseInt(document.getElementById("fieldvisit_charges").value)
            }
            if ($scope.rdbadhoc_collectiontype == 'Collect') {
                adhoc_fee = parseInt(document.getElementById("adhoc_fee").value)
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Collect') {
                life_insurance = parseInt(document.getElementById("life_insurance").value)
            }
            var result = processing_fee + doc_charges + fieldvisit_charges + adhoc_fee + life_insurance

            document.getElementById("total_collect").value = result;

            var processing_fee_deduct = 0;
            var doc_charges_deduct = 0;
            var fieldvisit_charges_deduct = 0;
            var adhoc_fee_deduct = 0;
            var life_insurance_deduct = 0;
            var personal_accident_deduct = 0;

            if ($scope.rdbprocessing_collectiontype == 'Deduct') {
                processing_fee_deduct = parseInt(document.getElementById("processingfee").value)
            }
            if ($scope.rdbdoccharge_collectiontype == 'Deduct') {
                doc_charges_deduct = parseInt(document.getElementById("doc_charges").value)

            }
            if ($scope.rdbfieldvisit_collectiontype == 'Deduct') {
                fieldvisit_charges_deduct = parseInt(document.getElementById("fieldvisit_charges").value)
            }
            if ($scope.rdbadhoc_collectiontype == 'Deduct') {
                adhoc_fee_deduct = parseInt(document.getElementById("adhoc_fee").value)
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Deduct') {
                life_insurance_deduct = parseInt(document.getElementById("life_insurance").value)
            }
            if ($scope.rdbpersonalaccident_collectiontype == 'Deduct') {
                personal_accident_deduct = parseInt(document.getElementById("acct_insurance").value)
            }
            var result_deduct = processing_fee_deduct + doc_charges_deduct + fieldvisit_charges_deduct + adhoc_fee_deduct + life_insurance_deduct + personal_accident_deduct

            document.getElementById("total_deduct").value = result_deduct;

        }

        $scope.txtamountchange6 = function () {
            var input = document.getElementById('acct_insurance').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount56 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtacct_insurance = "";
            }
            else {
                //   $scope.txtprocessing_fee = output;
                document.getElementById('words_totalamount56').innerHTML = lswords_totalamount56;
            }
        }

        $scope.collectdeduct = function () {
            var processing_fee = 0;
            var doc_charges = 0;
            var fieldvisit_charges = 0;
            var adhoc_fee = 0;
            var life_insurance = 0;
            var personal_accident = 0;

            if ($scope.rdbprocessing_collectiontype == 'Collect') {
                processing_fee = parseInt(document.getElementById("processingfee").value)
            }
            if ($scope.rdbdoccharge_collectiontype == 'Collect') {
                doc_charges = parseInt(document.getElementById("doc_charges").value)
                console.log(doc_charges)
            }
            if ($scope.rdbfieldvisit_collectiontype == 'Collect') {
                fieldvisit_charges = parseInt(document.getElementById("fieldvisit_charges").value)
            }
            if ($scope.rdbadhoc_collectiontype == 'Collect') {
                adhoc_fee = parseInt(document.getElementById("adhoc_fee").value)
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Collect') {
                life_insurance = parseInt(document.getElementById("life_insurance").value)
            }
            if ($scope.rdbpersonalaccident_collectiontype == 'Collect') {
                personal_accident = parseInt(document.getElementById("acct_insurance").value)
            }
            var result = processing_fee + doc_charges + fieldvisit_charges + adhoc_fee + life_insurance + personal_accident

            document.getElementById("total_collect").value = result;

            var processing_fee_deduct = 0;
            var doc_charges_deduct = 0;
            var fieldvisit_charges_deduct = 0;
            var adhoc_fee_deduct = 0;
            var life_insurance_deduct = 0;
            var personal_accident_deduct = 0;

            if ($scope.rdbprocessing_collectiontype == 'Deduct') {
                processing_fee_deduct = parseInt(document.getElementById("processingfee").value)
            }
            if ($scope.rdbdoccharge_collectiontype == 'Deduct') {
                doc_charges_deduct = parseInt(document.getElementById("doc_charges").value)
                console.log(doc_charges)
            }
            if ($scope.rdbfieldvisit_collectiontype == 'Deduct') {
                fieldvisit_charges_deduct = parseInt(document.getElementById("fieldvisit_charges").value)
            }
            if ($scope.rdbadhoc_collectiontype == 'Deduct') {
                adhoc_fee_deduct = parseInt(document.getElementById("adhoc_fee").value)
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Deduct') {
                life_insurance_deduct = parseInt(document.getElementById("life_insurance").value)
            }
            if ($scope.rdbpersonalaccident_collectiontype == 'Deduct') {
                personal_accident_deduct = parseInt(document.getElementById("acct_insurance").value)
            }
            var result_deduct = processing_fee_deduct + doc_charges_deduct + fieldvisit_charges_deduct + adhoc_fee_deduct + life_insurance_deduct + personal_accident_deduct

            document.getElementById("total_deduct").value = result_deduct;

        }

        $scope.update_ServiceCharges = function () {
            var params = {
                processing_fee: $scope.txtprocessing_fee,
                processing_collectiontype: $scope.rdbprocessing_collectiontype,
                doc_charges: $scope.txtdoc_charges,
                doccharge_collectiontype: $scope.rdbdoccharge_collectiontype,
                fieldvisit_charge: $scope.txtfieldvisit_charges,
                fieldvisit_collectiontype: $scope.rdbfieldvisit_collectiontype,
                adhoc_fee: $scope.txtadhoc_fee,
                adhoc_collectiontype: $scope.rdbadhoc_collectiontype,
                life_insurance: $scope.txtlife_insurance,
                lifeinsurance_collectiontype: $scope.rdblifeinsurance_collectiontype,
                acct_insurance: $scope.txtacct_insurance,
                acctinsurance_collectiontype: $scope.rdbpersonalaccident_collectiontype,
                total_collect: document.getElementById("total_collect").value,
                total_deduct: document.getElementById("total_deduct").value,
                application2servicecharge_gid: application2servicecharge_gid
            }
            var url = 'api/AgrTrnSuprAppCreditUnderWriting/ServicechargeUpdate';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    if (lspage == "StartCreditUnderwriting") {
                        $location.url('app/AgrTrnSuprCreditProductChargesDtlEdit?application_gid=' + application_gid + '&lspage=' + lspage + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid);
                    }
                    else if (lspage == "CADApplicationEdit") {
                        $location.url('app/AgrTrnSuprCreditProductChargesDtlEdit?application_gid=' + application_gid + '&lspage=' + lspage + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid);
                    }
                    else if (lspage == "PendingCADReview") {
                        $location.url('app/AgrTrnSuprCreditProductChargesDtlEdit?application_gid=' + application_gid + '&lspage=' + lspage + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid);
                    }
                    else {

                    }
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
        }

        $scope.Back = function () {
            if (lspage == "StartCreditUnderwriting") {
                $location.url('app/AgrTrnSuprCreditProductChargesDtlEdit?application_gid=' + application_gid + '&lspage=' + lspage + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid);
            }
            else if (lspage == "CADApplicationEdit") {
                $location.url('app/AgrTrnSuprCreditProductChargesDtlEdit?application_gid=' + application_gid + '&lspage=' + lspage + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid);
            }
            else if (lspage == "PendingCADReview") {
                $location.url('app/AgrTrnSuprCreditProductChargesDtlEdit?application_gid=' + application_gid + '&lspage=' + lspage + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid);
            }
            else if (lspage == "CADAcceptanceCustomers") {
                $location.url('app/AgrTrnSuprCreditProductChargesDtlEdit?application_gid=' + application_gid + '&lspage=' + lspage + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid);
            }
            else {

            }
        }

    }
})();
