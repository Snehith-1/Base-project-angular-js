(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCADIndividualCovenantDoccontroller', MstCADIndividualCovenantDoccontroller);

    MstCADIndividualCovenantDoccontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function MstCADIndividualCovenantDoccontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCADIndividualCovenantDoccontroller';
        $scope.contact_gid = $location.search().contact_gid;
        var contact_gid = $scope.contact_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        lockUI();
        activate();

        function activate() {
            $scope.covenantall = false; 
            var params = {
                credit_gid: contact_gid
            }
            var url = 'api/MstApplicationAdd/GetGeneticCodeList';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.genetic_list = resp.data.genetic_list;
            });

            var url = 'api/MstAppCreditUnderWriting/GetGeneticCodeList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.mstgeneticcode_list = resp.data.mstcuwgeneticcode_list;
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

            var params = {
                credit_gid: contact_gid,
                applicant_type: 'Individual'
            }

            var url = 'api/MstCADCreditAction/GetCreditOperationsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtinstitution_name = resp.data.company_name;
                $scope.txtstakeholder_type = resp.data.stakeholder_type;
                if (lspage != "CreditApproval") {
                    if ($scope.txtstakeholder_type == 'Applicant') {
                        $scope.txtstakeholder_type_flag = 'N';
                    }
                    else { $scope.txtstakeholder_type_flag = 'Y'; }
                    if ($scope.txtstakeholder_type == 'Guarantor') {
                        $scope.lblstakeholdername = 'Apply to all Institution (Guarantor)';
                    }
                    if ($scope.txtstakeholder_type == 'Member') {
                        $scope.lblstakeholdername = 'Apply to all Institution (Member)';
                    }
                }
                else { $scope.txtstakeholder_type_flag = 'N'; }

            });
            var params = {
                credit_gid: "",
                application_gid: application_gid,
            }
            var url = "api/MstCADCreditAction/GetCovenantIndividualDocumentList";
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
               /* if (resp.data.CADDocument != null) {*/
                    $scope.covenant_documentlist = resp.data.CADDocument;
               /* }*/
            }); 

            var params = {
                credit_gid: contact_gid
            }
            var geturl = "";
            if (lspage != "StartCreditUnderwriting")
                geturl = "api/MstCADCreditAction/GetCADTrnCovenantTaggedDocList";
            else
                geturl = "api/MstCADCreditAction/GetCADCovenantTaggedDocList"; 
            SocketService.getparams(geturl, params).then(function (resp) {
                unlockUI();
                if (resp.data.TaggedDocument != null) {
                    $scope.covenant_taggeddoclist = resp.data.TaggedDocument;
                    angular.forEach($scope.covenant_documentlist, function (value, key) {
                        var getselected = $scope.covenant_taggeddoclist.find(function (a) { return a.companydocument_gid === value.document_gid });
                        if (getselected != null) {
                            value.covenantchecked = true;
                            value.covenantperiod = getselected.covenantperiod == 'Every month' ? 1 : getselected.covenantperiod == 'Every 3 months' ? 2 : getselected.covenantperiod == 'Every 6 months' ? 3 : getselected.covenantperiod == 'Every 1 year' ? 4 : ""
                            value.buffer_days = getselected.buffer_days == '0 days' ? 1 : getselected.buffer_days == '5 days' ? 2 : getselected.buffer_days == '10 days' ? 3 : getselected.buffer_days == '15 days' ? 4 : getselected.buffer_days == '20 days' ? 5 :
                                getselected.buffer_days == '25 days' ? 6 : getselected.buffer_days == '30 days' ? 7 : getselected.buffer_days == '35 days' ? 8 : getselected.buffer_days == '40 days' ? 9 :
                                getselected.buffer_days == '45 days' ? 10 : getselected.buffer_days == '50 days' ? 11 : getselected.buffer_days == '55 days' ? 12 : getselected.buffer_days == '60 days' ? 13 : "";
                            value.dropdownchk = false; 
                            value.dropdownchk = false; 
                            value.groupcovdocumentchecklist_gid = getselected.groupcovdocumentchecklist_gid;
                            if (lspage == "StartCreditUnderwriting" && getselected.taggedby == "Credit")
                                value.taggedby = '';
                            else if (lspage != "StartCreditUnderwriting" && getselected.taggedby == "CAD")
                                value.taggedby = '';
                            else if (getselected.taggedby == "N")
                                value.taggedby = 'Business';
                            else
                                value.taggedby = getselected.taggedby;
                        }
                        else {
                            value.dropdownchk = true;
                            value.covenantperiod = '';
                            value.buffer_days = '';
                            value.covenantchecked = false;
                        }
                       
                    });
                }
                else {
                    angular.forEach($scope.covenant_documentlist, function (value, key) {
                        value.dropdownchk = true;
                        value.covenantchecked = false;
                    });
                }
            });
        }

        $scope.enableperiods = function (index, checked) {
            var dropdown = '#drop_' + index;
            var dropdown1 = '#drop1_' + index;
            if (checked) {
                $(dropdown).prop('disabled', false);
                $(dropdown1).prop('disabled', false);
            }
            else {
                $(dropdown).prop('disabled', true);
                $(dropdown).val('');
                $(dropdown1).prop('disabled', true);
                $(dropdown1).val('');
            }
        }

        $scope.untag = function (documentcheckdtl_gid) {
            var url = "api/MstCADCreditAction/UnTagDocument";
            var params = {
                documentcheckdtl_gid: documentcheckdtl_gid
            };
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    activate();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }
    $scope.applytoallcovenant = function () {
        var taggeddocument_list = $scope.covenant_documentlist;
        var taggedby = '';
      
        var params = {
            application_gid: application_gid,
            credit_gid: contact_gid,
            applicant_type: 'Individual'
        };
        if (taggeddocument_list && taggeddocument_list != null) {
            var url = "api/MstCADCreditAction/ApplyALLCovenantDocumentList";
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    activate();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

            });
        }
        else {
            Notify.alert('Select Atleast One Covenant Document!', 'warning')
        }

    }
        $scope.checkall = function (selected, covenantdocumentclick) {
            angular.forEach($scope.covenant_documentlist, function (val, key) {
                val.covenantchecked = selected;
                var dropdown = '#drop_' + key;
                var dropdown1 = '#drop1_' + key;
                if (selected) { 
                    $(dropdown).prop('disabled', false);
                     $(dropdown1).prop('disabled', false);
                   }
                else {
                    $(dropdown).prop('disabled', true);
                    $(dropdown).val('');
                    $(dropdown1).prop('disabled', true);
                    $(dropdown1).val('');
                }
            });
        }

        $scope.addDoc_checklist = function (covenantdocumentclick) {
            var doc_gid;
            var doclistGId = [];
            var taggeddocumentlist = [];
            if (covenantdocumentclick)
                taggeddocumentlist = $scope.covenant_documentlist;
            else
                taggeddocumentlist = $scope.documentlist_gid;
            angular.forEach(taggeddocumentlist, function (val) {
                if (val.checked == true) {
                    var doclist_gid = val.document_gid;
                    doc_gid = val.document_gid;
                    doclistGId.push(doclist_gid);
                }
            });

            var params = {
                document_gid: doclistGId,
                application_gid: application_gid,
                credit_gid: contact_gid
            }

            if (doc_gid != undefined) {
                var url = 'api/MstCADCreditAction/PostIndividualCheckList';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI();
                        activate();
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        unlockUI();
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }

                });
            }
            else {
                Notify.alert('Select Atleast One Document!', 'warning')
            }
        }

        $scope.updateDoc_covenantperiod = function (documentcheckdtl_gid) {
            lockUI();
            var emptycovenant = false;
            var emptybuffer = false;
            angular.forEach($scope.covenant_documentlist, function (val) {
                if ($scope.covenant_taggeddoclist != undefined) {
                    var getselected = $scope.covenant_taggeddoclist.find(function (a) { return a.companydocument_gid === val.document_gid });
                    if (getselected != null) {
                        val.individual2document_gid = getselected.individual2document_gid;
                        val.groupcovdocumentchecklist_gid = getselected.groupcovdocumentchecklist_gid;
                    }
                }
                val.application_gid = application_gid;
                val.credit_gid = contact_gid;
                val.lstype = 'Individual';
                val.companydocument_gid = val.document_gid;
                if (val.covenantchecked == true) { 
                    if (val.covenantperiod == "" || val.covenantperiod == undefined || val.covenantperiod == null)
                        emptycovenant = true;
                    val.covenantperiod = val.covenantperiod == 1 ? 'Every month' : val.covenantperiod == 2 ? 'Every 3 months' : val.covenantperiod == 3 ? 'Every 6 months' : val.covenantperiod == 4 ? 'Every 1 year' : ""
                }
                if (val.covenantchecked == true) {
                    if (val.buffer_days == "" || val.buffer_days == undefined || val.buffer_days == null)
                        emptybuffer = true;
                    val.buffer_days = val.buffer_days == 1 ? '0 days' : val.buffer_days == 2 ? '5 days' : val.buffer_days == 3 ? '10 days' : val.buffer_days == 4 ? '15 days' : val.buffer_days == 5 ? '20 days' :
                        val.buffer_days == 6 ? '25 days' : val.buffer_days == 7 ? '30 days' : val.buffer_days == 8 ? '35 days' : val.buffer_days == 9 ? '40 days' :
                            val.buffer_days == 10 ? '45 days' : val.buffer_days == 11 ? '50 days' : val.buffer_days == 12 ? '55 days' : val.buffer_days == 13 ? '60 days' : "";

                }
            });
            if (emptycovenant) {
                angular.forEach($scope.covenant_documentlist, function (val) {  
                    val.covenantperiod = val.covenantperiod == 'Every month' ? 1 : val.covenantperiod == 'Every 3 months' ? 2 : val.covenantperiod == 'Every 6 months' ? 3 : getselected.covenantperiod == 'Every 1 year' ? 4 : "";
                });
                unlockUI();
                Notify.alert('Kindly Select the Covenant Periods for the selected Document..!', 'warning')
                return false;
               
            }
            if (emptybuffer) {
                angular.forEach($scope.covenant_documentlist, function (val) {
                    val.buffer_days = val.buffer_days == '0 days' ? 1 : val.buffer_days == '5 days' ? 2 : val.buffer_days == '10 days' ? 3 : val.buffer_days == '15 days' ? 4 : val.buffer_days == '20 days' ? 5 :
                        val.buffer_days == '25 days' ? 6 : val.buffer_days == '30 days' ? 7 : val.buffer_days == '35 days' ? 8 : val.buffer_days == '40 days' ? 9 :
                            val.buffer_days == '45 days' ? 10 : val.buffer_days == '50 days' ? 11 : val.buffer_days == '55 days' ? 12 : val.buffer_days == '60 days' ? 13 : "";

                });
                unlockUI();
                Notify.alert('Kindly Select the Buffer Days for the selected Document..!', 'warning')
                return false;

            }
            else {
                var taggeddocument_list = $scope.covenant_documentlist;
                var taggedby = '';
                if (lspage == "StartCreditUnderwriting")
                    taggedby = 'Credit';
                else
                    taggedby = 'CAD';
                var params = {
                    taggedby: taggedby,
                    CovenantPeriod: taggeddocument_list
                }
                if (taggeddocument_list && taggeddocument_list != null) {
                    var url = 'api/MstCADCreditAction/PostCovenantPeriods';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            activate();
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        else {
                            unlockUI();
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }

                    });
                }
                else {
                    Notify.alert('Select Atleast One Covenant Document!', 'warning')
                }
            }
          

        }

        $scope.CheckAllIndividualDocument = function () {
            var params = {
                application_gid: application_gid,
                credit_gid: contact_gid,
                applicant_type: 'Individual'
            };

            var url = "api/MstCADCreditAction/CheckALLDocumentList";
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    activate();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }

        $scope.Back = function () {
            if (lspage == "StartCreditUnderwriting") {
                $location.url('app/MstStartCreditUnderwriting?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "PendingCADReview") {
                $location.url('app/MstCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
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

        $scope.individual_addguarantee = function () {
            $location.url('app/MstCADIndividualGuaranteeDtlAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_docchecklist = function () {
            $location.url('app/MstCADIndividualDocCheckList?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_covenantdocchecklist = function () {
            $location.url('app/MstCADIndividualCovenantDocChecklist?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_bureauadd = function () {
            $location.url('app/MstCADCreditIndividualDtlAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_bankaccount = function () {
            $location.url('app/MstCADCreditIndividualBankAcctAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_existingbankaccount = function () {
            $location.url('app/MstCADCreditIndividualExistingBankAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_PSLdata = function () {
            $location.url('app/MstCADCreditIndividualPSLDataFlagAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_repayment = function () {
            $location.url('app/MstCADCreditIndividualRepaymentAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_observation = function () {
            $location.url('app/MstCADCreditIndividualObservationAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.creditbankacctdtl_edit = function (creditbankdtl_gid) {
            $location.url('app/MstCADCreditIndividualBankAcctEdit?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&creditbankdtl_gid=' + creditbankdtl_gid + '&lspage=' + lspage);
        }
        //KYC API
        $scope.iecdetailed_vertification = function () {
            $location.url('app/MstCADCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=IECDETAILED' + '&lspage=' + lspage);
        }
        $scope.fssai_vertification = function () {
            $location.url('app/MstCADCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=FSSAI' + '&lspage=' + lspage);
        }
        $scope.fda_vertification = function () {
            $location.url('app/MstCADCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=FDA' + '&lspage=' + lspage);
        }
        $scope.lpgid_vertification = function () {
            $location.url('app/MstCADCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=LPGID' + '&lspage=' + lspage);
        }
        $scope.shop_vertification = function () {
            $location.url('app/MstCADCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=SHOP' + '&lspage=' + lspage);
        }
        $scope.rcauthadv_vertification = function () {
            $location.url('app/MstCADCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=RCAUTHADV' + '&lspage=' + lspage);
        }
        $scope.rcsearch_vertification = function () {
            $location.url('app/MstCADCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=RCSEARCH' + '&lspage=' + lspage);
        }
        $scope.propertytax_vertification = function () {
            $location.url('app/MstCADCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=PROPERTYTAX' + '&lspage=' + lspage);
        }
        $scope.company_bankstatement = function () {
            $location.url('app/MstCADCreditIndividualBankStatementAnalysisAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }
        $scope.crimecheck_record = function () {
            $location.url('app/MstCADCreditCrimeCheckRecordAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=PROPERTYTAX' + '&lspage=' + lspage);
        }
        $scope.crimecheckreport_realtime = function () {
            $location.url('app/MstCADCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=CRIMECHECKREPORTREALTIME' + '&lspage=' + lspage);
        }
    }
})();
