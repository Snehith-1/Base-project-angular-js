(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditServicesDtlEditController', MstCreditServicesDtlEditController);

        MstCreditServicesDtlEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function MstCreditServicesDtlEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCreditServicesDtlEditController';

        $scope.application2servicecharge_gid = $location.search().application2servicecharge_gid;
        var application2servicecharge_gid = $scope.application2servicecharge_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
      
        activate();
        function activate() {
            var param = {
                application2servicecharge_gid: $scope.application2servicecharge_gid
            }
            var url = 'api/MstAppCreditUnderWriting/ServicechargeEdit';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.txtsampleprocessing_fee = resp.data.processing_fee;
                $scope.txtprocessing_fee = (parseInt($scope.txtsampleprocessing_fee.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountseperator = (parseInt($scope.txtprocessing_fee.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                document.getElementById('words_totalamount51').innerHTML = $scope.lblamountwords;
                $scope.rdbprocessing_collectiontype = resp.data.processing_collectiontype;
                $scope.txtsampledoc_charges = resp.data.doc_charges;
                $scope.txtdoc_charges = (parseInt($scope.txtsampledoc_charges.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountseperator = (parseInt($scope.txtdoc_charges.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                document.getElementById('words_totalamount52').innerHTML = $scope.lblamountwords;
                $scope.rdbdoccharge_collectiontype = resp.data.doccharge_collectiontype;
                $scope.txtsamplefieldvisit_charges = resp.data.fieldvisit_charge;
                $scope.txtfieldvisit_charges = (parseInt($scope.txtsamplefieldvisit_charges.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountseperator = (parseInt($scope.txtfieldvisit_charges.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                document.getElementById('words_totalamount53').innerHTML = $scope.lblamountwords;
                $scope.rdbfieldvisit_collectiontype = resp.data.fieldvisit_charges_collectiontype;
                $scope.txtsampleadhoc_fee = resp.data.adhoc_fee;
                $scope.txtadhoc_fee = (parseInt($scope.txtsampleadhoc_fee.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountseperator = (parseInt($scope.txtadhoc_fee.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                document.getElementById('words_totalamount54').innerHTML = $scope.lblamountwords;
                $scope.rdbadhoc_collectiontype = resp.data.adhoc_collectiontype;
                $scope.txtsamplelife_insurance = resp.data.life_insurance;
                $scope.txtlife_insurance = (parseInt($scope.txtsamplelife_insurance.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountseperator = (parseInt($scope.txtlife_insurance.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                document.getElementById('words_totalamount55').innerHTML = $scope.lblamountwords;
                $scope.rdblifeinsurance_collectiontype = resp.data.lifeinsurance_collectiontype;
                $scope.rdbpersonalaccident_collectiontype = resp.data.acctinsurance_collectiontype;
                $scope.txtsampleacct_insurance = resp.data.acct_insurance;
                $scope.txtacct_insurance = (parseInt($scope.txtsampleacct_insurance.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountseperator = (parseInt($scope.txtacct_insurance.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                document.getElementById('words_totalamount56').innerHTML = $scope.lblamountwords;
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
                var txtprocessing_fee = parseInt($scope.txtprocessing_fee.replace(/[\s,]+/g, '').trim());
            }
            $scope.txtprocessing_fee = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
            var processing_fee = 0;
            var doc_charges = 0;
            var fieldvisit_charges = 0;
            var adhoc_fee = 0;
            var life_insurance = 0;
            var personal_accident = 0;

            if ($scope.rdbprocessing_collectiontype == 'Collect') {
                var processing_fee = parseInt(document.getElementById("processingfee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbdoccharge_collectiontype == 'Collect') {
                var doc_charges = parseInt(document.getElementById("doc_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbfieldvisit_collectiontype == 'Collect') {
                var fieldvisit_charges = parseInt(document.getElementById("fieldvisit_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbadhoc_collectiontype == 'Collect') {
                var adhoc_fee = parseInt(document.getElementById("adhoc_fee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdblifeinsurance_collectiontype == 'Collect') {
                var life_insurance = parseInt(document.getElementById("life_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbpersonalaccident_collectiontype == 'Collect') {
                var personal_accident = parseInt(document.getElementById("acct_insurance").value.replace(/[\s,]+/g, '').trim());
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
                var processing_fee_deduct = parseInt(document.getElementById("processingfee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbdoccharge_collectiontype == 'Deduct') {
                var doc_charges_deduct = parseInt(document.getElementById("doc_charges").value.replace(/[\s,]+/g, '').trim());

            }
            if ($scope.rdbfieldvisit_collectiontype == 'Deduct') {
                var fieldvisit_charges_deduct = parseInt(document.getElementById("fieldvisit_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbadhoc_collectiontype == 'Deduct') {
                var adhoc_fee_deduct = parseInt(document.getElementById("adhoc_fee").value.replace(/[\s,]+/g, '').trim());
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Deduct') {
                var life_insurance_deduct = parseInt(document.getElementById("life_insurance").value.replace(/[\s,]+/g, '').trim());
            }

            if ($scope.rdbpersonalaccident_collectiontype == 'Deduct') {
                var personal_accident_deduct = parseInt(document.getElementById("acct_insurance").value.replace(/[\s,]+/g, '').trim());
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
                var txtdoc_charges = parseInt($scope.txtdoc_charges.replace(/[\s,]+/g, '').trim());
            }
            $scope.txtdoc_charges = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
            var processing_fee = 0;
            var doc_charges = 0;
            var fieldvisit_charges = 0;
            var adhoc_fee = 0;
            var life_insurance = 0;
            var personal_accident = 0;

            if ($scope.rdbprocessing_collectiontype == 'Collect') {
                var processing_fee = parseInt(document.getElementById("processingfee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbdoccharge_collectiontype == 'Collect') {
                var doc_charges = parseInt(document.getElementById("doc_charges").value.replace(/[\s,]+/g, '').trim());

            }
            if ($scope.rdbfieldvisit_collectiontype == 'Collect') {
                var fieldvisit_charges = parseInt(document.getElementById("fieldvisit_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbadhoc_collectiontype == 'Collect') {
                var adhoc_fee = parseInt(document.getElementById("adhoc_fee").value.replace(/[\s,]+/g, '').trim());
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Collect') {
                var life_insurance = parseInt(document.getElementById("life_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbpersonalaccident_collectiontype == 'Collect') {
                var personal_accident = parseInt(document.getElementById("acct_insurance").value.replace(/[\s,]+/g, '').trim());
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
                var processing_fee_deduct = parseInt(document.getElementById("processingfee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbdoccharge_collectiontype == 'Deduct') {
                var doc_charges_deduct = parseInt(document.getElementById("doc_charges").value.replace(/[\s,]+/g, '').trim());

            }
            if ($scope.rdbfieldvisit_collectiontype == 'Deduct') {
                var fieldvisit_charges_deduct = parseInt(document.getElementById("fieldvisit_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbadhoc_collectiontype == 'Deduct') {
                var adhoc_fee_deduct = parseInt(document.getElementById("adhoc_fee").value.replace(/[\s,]+/g, '').trim());
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Deduct') {
                var life_insurance_deduct = parseInt(document.getElementById("life_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbpersonalaccident_collectiontype == 'Deduct') {
                var personal_accident_deduct = parseInt(document.getElementById("acct_insurance").value.replace(/[\s,]+/g, '').trim());
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
                var txtfieldvisit_charges = parseInt($scope.txtfieldvisit_charges.replace(/[\s,]+/g, '').trim());
            }
            $scope.txtfieldvisit_charges = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
            var processing_fee = 0;
            var doc_charges = 0;
            var fieldvisit_charges = 0;
            var adhoc_fee = 0;
            var life_insurance = 0;
            var personal_accident = 0;
            if ($scope.rdbprocessing_collectiontype == 'Collect') {
                var processing_fee = parseInt(document.getElementById("processingfee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbdoccharge_collectiontype == 'Collect') {
                var doc_charges = parseInt(document.getElementById("doc_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbfieldvisit_collectiontype == 'Collect') {
                var fieldvisit_charges = parseInt(document.getElementById("fieldvisit_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbadhoc_collectiontype == 'Collect') {
                var adhoc_fee = parseInt(document.getElementById("adhoc_fee").value.replace(/[\s,]+/g, '').trim());
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Collect') {
                var life_insurance = parseInt(document.getElementById("life_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbpersonalaccident_collectiontype == 'Collect') {
                var personal_accident = parseInt(document.getElementById("acct_insurance").value.replace(/[\s,]+/g, '').trim());
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
                var processing_fee_deduct = parseInt(document.getElementById("processingfee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbdoccharge_collectiontype == 'Deduct') {
                var doc_charges_deduct = parseInt(document.getElementById("doc_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbfieldvisit_collectiontype == 'Deduct') {
                var fieldvisit_charges_deduct = parseInt(document.getElementById("fieldvisit_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbadhoc_collectiontype == 'Deduct') {
                var adhoc_fee_deduct = parseInt(document.getElementById("adhoc_fee").value.replace(/[\s,]+/g, '').trim());
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Deduct') {
                var life_insurance_deduct = parseInt(document.getElementById("life_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbpersonalaccident_collectiontype == 'Deduct') {
                var personal_accident_deduct = parseInt(document.getElementById("acct_insurance").value.replace(/[\s,]+/g, '').trim());
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
                var txtadhoc_fee = parseInt($scope.txtadhoc_fee.replace(/[\s,]+/g, '').trim());
            }
            $scope.txtadhoc_fee = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
            var processing_fee = 0;
            var doc_charges = 0;
            var fieldvisit_charges = 0;
            var adhoc_fee = 0;
            var life_insurance = 0;
            var personal_accident = 0;

            if ($scope.rdbprocessing_collectiontype == 'Collect') {
                var processing_fee = parseInt(document.getElementById("processingfee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbdoccharge_collectiontype == 'Collect') {
                var doc_charges = parseInt(document.getElementById("doc_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbfieldvisit_collectiontype == 'Collect') {
                var fieldvisit_charges = parseInt(document.getElementById("fieldvisit_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbadhoc_collectiontype == 'Collect') {
                var adhoc_fee = parseInt(document.getElementById("adhoc_fee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdblifeinsurance_collectiontype == 'Collect') {
                var life_insurance = parseInt(document.getElementById("life_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbpersonalaccident_collectiontype == 'Collect') {
                var personal_accident = parseInt(document.getElementById("acct_insurance").value.replace(/[\s,]+/g, '').trim());
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
                var processing_fee_deduct = parseInt(document.getElementById("processingfee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbdoccharge_collectiontype == 'Deduct') {
                var doc_charges_deduct = parseInt(document.getElementById("doc_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbfieldvisit_collectiontype == 'Deduct') {
                var fieldvisit_charges_deduct = parseInt(document.getElementById("fieldvisit_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbadhoc_collectiontype == 'Deduct') {
                var adhoc_fee_deduct = parseInt(document.getElementById("adhoc_fee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdblifeinsurance_collectiontype == 'Deduct') {
                var life_insurance_deduct = parseInt(document.getElementById("life_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbpersonalaccident_collectiontype == 'Deduct') {
                var personal_accident_deduct = parseInt(document.getElementById("acct_insurance").value.replace(/[\s,]+/g, '').trim());
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
                var txtlife_insurance = parseInt($scope.txtlife_insurance.replace(/[\s,]+/g, '').trim());
            }
            $scope.txtlife_insurance = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
            var processing_fee = 0;
            var doc_charges = 0;
            var fieldvisit_charges = 0;
            var adhoc_fee = 0;
            var life_insurance = 0;
            var personal_accident = 0;

            if ($scope.rdbprocessing_collectiontype == 'Collect') {
                var processing_fee = parseInt(document.getElementById("processingfee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbdoccharge_collectiontype == 'Collect') {
                var doc_charges = parseInt(document.getElementById("doc_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbfieldvisit_collectiontype == 'Collect') {
                var fieldvisit_charges = parseInt(document.getElementById("fieldvisit_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbadhoc_collectiontype == 'Collect') {
                var adhoc_fee = parseInt(document.getElementById("adhoc_fee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdblifeinsurance_collectiontype == 'Collect') {
                var life_insurance = parseInt(document.getElementById("life_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbpersonalaccident_collectiontype == 'Collect') {
                var personal_accident = parseInt(document.getElementById("acct_insurance").value.replace(/[\s,]+/g, '').trim());
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
                var processing_fee_deduct = parseInt(document.getElementById("processingfee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbdoccharge_collectiontype == 'Deduct') {
                var doc_charges_deduct = parseInt(document.getElementById("doc_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbfieldvisit_collectiontype == 'Deduct') {
                var fieldvisit_charges_deduct = parseInt(document.getElementById("fieldvisit_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbadhoc_collectiontype == 'Deduct') {
                var adhoc_fee_deduct = parseInt(document.getElementById("adhoc_fee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdblifeinsurance_collectiontype == 'Deduct') {
                var life_insurance_deduct = parseInt(document.getElementById("life_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbpersonalaccident_collectiontype == 'Deduct') {
                var personal_accident_deduct = parseInt(document.getElementById("acct_insurance").value.replace(/[\s,]+/g, '').trim());
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
                var txtacct_insurance = parseInt($scope.txtacct_insurance.replace(/[\s,]+/g, '').trim());
            }
            $scope.txtacct_insurance = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
            var processing_fee = 0;
            var doc_charges = 0;
            var fieldvisit_charges = 0;
            var adhoc_fee = 0;
            var life_insurance = 0;
            var personal_accident = 0;

            if ($scope.rdbprocessing_collectiontype == 'Collect') {
                var processing_fee = parseInt(document.getElementById("processingfee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbdoccharge_collectiontype == 'Collect') {
                var doc_charges = parseInt(document.getElementById("doc_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbfieldvisit_collectiontype == 'Collect') {
                var fieldvisit_charges = parseInt(document.getElementById("fieldvisit_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbadhoc_collectiontype == 'Collect') {
                var adhoc_fee = parseInt(document.getElementById("adhoc_fee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdblifeinsurance_collectiontype == 'Collect') {
                var life_insurance = parseInt(document.getElementById("life_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbpersonalaccident_collectiontype == 'Collect') {
                var personal_accident = parseInt(document.getElementById("acct_insurance").value.replace(/[\s,]+/g, '').trim());
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
                var processing_fee_deduct = parseInt(document.getElementById("processingfee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbdoccharge_collectiontype == 'Deduct') {
                var doc_charges_deduct = parseInt(document.getElementById("doc_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbfieldvisit_collectiontype == 'Deduct') {
                var fieldvisit_charges_deduct = parseInt(document.getElementById("fieldvisit_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbadhoc_collectiontype == 'Deduct') {
                var adhoc_fee_deduct = parseInt(document.getElementById("adhoc_fee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdblifeinsurance_collectiontype == 'Deduct') {
                var life_insurance_deduct = parseInt(document.getElementById("life_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbpersonalaccident_collectiontype == 'Deduct') {
                var personal_accident_deduct = parseInt(document.getElementById("acct_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            var result_deduct = processing_fee_deduct + doc_charges_deduct + fieldvisit_charges_deduct + adhoc_fee_deduct + life_insurance_deduct + personal_accident_deduct

            document.getElementById("total_deduct").value = result_deduct;
        }

        $scope.collectdeduct = function () {
            var processing_fee = 0;
            var doc_charges = 0;
            var fieldvisit_charges = 0;
            var adhoc_fee = 0;
            var life_insurance = 0;
            var personal_accident = 0;

            if ($scope.rdbprocessing_collectiontype == 'Collect') {
                var processing_fee = parseInt(document.getElementById("processingfee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbdoccharge_collectiontype == 'Collect') {
                var doc_charges = parseInt(document.getElementById("doc_charges").value.replace(/[\s,]+/g, '').trim());

            }
            if ($scope.rdbfieldvisit_collectiontype == 'Collect') {
                var fieldvisit_charges = parseInt(document.getElementById("fieldvisit_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbadhoc_collectiontype == 'Collect') {
                var adhoc_fee = parseInt(document.getElementById("adhoc_fee").value.replace(/[\s,]+/g, '').trim());
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Collect') {
                var life_insurance = parseInt(document.getElementById("life_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbpersonalaccident_collectiontype == 'Collect') {
                var personal_accident = parseInt(document.getElementById("acct_insurance").value.replace(/[\s,]+/g, '').trim());
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
                var processing_fee_deduct = parseInt(document.getElementById("processingfee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbdoccharge_collectiontype == 'Deduct') {
                var doc_charges_deduct = parseInt(document.getElementById("doc_charges").value.replace(/[\s,]+/g, '').trim());

            }
            if ($scope.rdbfieldvisit_collectiontype == 'Deduct') {
                var fieldvisit_charges_deduct = parseInt(document.getElementById("fieldvisit_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbadhoc_collectiontype == 'Deduct') {
                var adhoc_fee_deduct = parseInt(document.getElementById("adhoc_fee").value.replace(/[\s,]+/g, '').trim());
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Deduct') {
                var life_insurance_deduct = parseInt(document.getElementById("life_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbpersonalaccident_collectiontype == 'Deduct') {
                var personal_accident_deduct = parseInt(document.getElementById("acct_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            var result_deduct = processing_fee_deduct + doc_charges_deduct + fieldvisit_charges_deduct + adhoc_fee_deduct + life_insurance_deduct + personal_accident_deduct

            document.getElementById("total_deduct").value = result_deduct;

        }

        $scope.update_ServiceCharges = function () {
            if ($scope.txtprocessing_fee == '' || $scope.txtprocessing_fee == null || $scope.txtprocessing_fee == undefined) {
                var lsprocessing_fee = null;
            }
            else {
                var lsprocessing_fee = parseInt($scope.txtprocessing_fee.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.txtdoc_charges == '' || $scope.txtdoc_charges == null || $scope.txtdoc_charges == undefined) {
                var lsdoc_charges = null;
            }
            else {
                var lsdoc_charges = parseInt($scope.txtdoc_charges.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.txtfieldvisit_charges == '' || $scope.txtfieldvisit_charges == null || $scope.txtfieldvisit_charges == undefined) {
                var lsfieldvisit_charges = null;
            }
            else {
                var lsfieldvisit_charges = parseInt($scope.txtfieldvisit_charges.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.txtadhoc_fee == '' || $scope.txtadhoc_fee == null || $scope.txtadhoc_fee == undefined) {
                var lsadhoc_fee = null;
            }
            else {
                var lsadhoc_fee = parseInt($scope.txtadhoc_fee.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.txtlife_insurance == '' || $scope.txtlife_insurance == null || $scope.txtlife_insurance == undefined) {
                var lslife_insurance = null;
            }
            else {
                var lslife_insurance = parseInt($scope.txtlife_insurance.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.txtacct_insurance == '' || $scope.txtacct_insurance == null || $scope.txtacct_insurance == undefined) {
                var lsacct_insurance = null;
            }
            else {
                var lsacct_insurance = parseInt($scope.txtacct_insurance.replace(/[\s,]+/g, '').trim());
            }

            var params = {
                processing_fee: lsprocessing_fee,
                processing_collectiontype: $scope.rdbprocessing_collectiontype,
                doc_charges: lsdoc_charges,
                doccharge_collectiontype: $scope.rdbdoccharge_collectiontype,
                fieldvisit_charge: lsfieldvisit_charges,
                fieldvisit_collectiontype: $scope.rdbfieldvisit_collectiontype,
                adhoc_fee: lsadhoc_fee,
                adhoc_collectiontype: $scope.rdbadhoc_collectiontype,
                life_insurance: lslife_insurance,
                lifeinsurance_collectiontype: $scope.rdblifeinsurance_collectiontype,
                acct_insurance: lsacct_insurance,
                acctinsurance_collectiontype : $scope.rdbpersonalaccident_collectiontype,
                total_collect: document.getElementById("total_collect").value,
                total_deduct: document.getElementById("total_deduct").value,
                application2servicecharge_gid : application2servicecharge_gid
            }
            var url = 'api/MstAppCreditUnderWriting/ServicechargeUpdate';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    if (lspage == "myapp") {
                        $location.url('app/MstCreditProductChargesDtlEdit?application_gid=' + application_gid + '&lspage='+ lspage);
                    }
                    else if (lspage == "CreditApproval") {
                        $location.url('app/MstCreditProductChargesDtlEdit?application_gid=' + application_gid + '&lspage=' + lspage);
                    }
                    else if (lspage == "CADApplicationEdit") {
                        $location.url('app/MstCreditProductChargesDtlEdit?application_gid=' + application_gid + '&lspage='+ lspage);
                    }
                    else if (lspage == "PendingCADReview") {
                        $location.url('app/MstCreditProductChargesDtlEdit?application_gid=' + application_gid + '&lspage=' + lspage);
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
            });
        }

        $scope.Back = function () {
            if (lspage == "myapp") {
                $location.url('app/MstCreditProductChargesDtlEdit?application_gid=' + application_gid + '&lspage='+ lspage);
            }
            else if (lspage == "CreditApproval") {
                $location.url('app/MstCreditProductChargesDtlEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "CADApplicationEdit") {
                $location.url('app/MstCreditProductChargesDtlEdit?application_gid=' + application_gid + '&lspage='+ lspage);
            }
            else if (lspage == "PendingCADReview") {
                $location.url('app/MstCreditProductChargesDtlEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else {

            }
        }
       

     
    }
})();

