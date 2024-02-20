(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnIndividualDocCheckListController', AgrTrnIndividualDocCheckListController);

        AgrTrnIndividualDocCheckListController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function AgrTrnIndividualDocCheckListController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnIndividualDocCheckListController';
        $scope.contact_gid = $location.search().contact_gid;
        var contact_gid = $scope.contact_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.document_status = $location.search().lsdocument_status;
        var document_status = $scope.document_status;
        lockUI();
        activate();
        function activate() {
            $scope.covenantall = false; 
            var params = {
                credit_gid: contact_gid
            }
            var url = 'api/AgrMstApplicationAdd/GetGeneticCodeList';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.genetic_list = resp.data.genetic_list;
            });

            var url = 'api/AgrTrnAppCreditUnderWriting/GetGeneticCodeList';
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

            var url = 'api/AgrTrnAppCreditUnderWriting/GetCreditOperationsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtinstitution_name = resp.data.company_name;
                $scope.txtstakeholder_type = resp.data.stakeholder_type;
                if (lspage != "CreditApproval") {
                    if ($scope.txtstakeholder_type == 'Applicant') {
                        $scope.txtstakeholder_type_flag = 'N';
                    }
                    else {
                        $scope.txtstakeholder_type_flag = 'Y';
                    }
                    if ($scope.txtstakeholder_type == 'Guarantor') {
                        $scope.lblstakeholdername = 'Apply to all Individual (Guarantor)';
                    }
                    if ($scope.txtstakeholder_type == 'Member') {
                        $scope.lblstakeholdername = 'Apply to all Individual (Member)';
                    }
                }
                else { $scope.txtstakeholder_type_flag = 'N'; }
              
            });
            var params = {
                credit_gid: contact_gid,
                application_gid: application_gid

            }
            var url = "api/AgrTrnCAD/GetIndividualTypeList";
            SocketService.getparams(url, params).then(function (resp) {
              /*  if (resp.data.CADDocument != null) {*/
                    $scope.documentlist_gid = resp.data.CADDocument;
                /*}*/
            });
            var params = {
                credit_gid: contact_gid

            }
            var geturl = "";
            if (lspage != "myapp" || lspage != "CreditApproval")
                geturl = "api/AgrTrnCAD/GetCADTrnTaggedDocList";
            else
                geturl = "api/AgrTrnCAD/GetCADTaggedDocList"; 
            SocketService.getparams(geturl, params).then(function (resp) {
                if (resp.data.TaggedDocument != null) {
                    $scope.taggeddoc_list = resp.data.TaggedDocument;
                    angular.forEach($scope.taggeddoc_list, function (value, key) {
                        if ((lspage == "myapp" || lspage == "CreditApproval") && value.taggedby == "Credit")
                            value.taggedby = '';
                        else if ((lspage != "myapp" || lspage != "CreditApproval") && value.taggedby == "CAD")
                            value.taggedby = '';
                        else if (value.taggedby == "N")
                            value.taggedby = 'Business';
                    });
                }
            });
        }

        $scope.untag = function (documentcheckdtl_gid, individual2document_gid) {
            documentcheckdtl_gid = documentcheckdtl_gid != "" ? documentcheckdtl_gid : individual2document_gid;
            var url = "api/AgrTrnCAD/UnTagDocument";
            var params = {
                documentcheckdtl_gid: documentcheckdtl_gid
            };
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
        $scope.applytoall = function () {
            var params = {
                application_gid: application_gid,
                credit_gid: contact_gid,
                applicant_type: 'Individual'
            };

            var url = "api/AgrTrnCAD/ApplyALLDocumentList";
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
        $scope.checkall = function (selected, covenantdocumentclick) {
            if (covenantdocumentclick) {
                angular.forEach($scope.covenant_documentlist, function (val) {
                    val.checked = selected;
                });
            }
            else {
                angular.forEach($scope.documentlist_gid, function (val) {
                    val.checked = selected;
                });
            } 
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
            var taggedby = '';
            if (lspage == "myapp" || lspage == "CreditApproval") { 
            taggedby = 'Credit';
        }
         else if (document_status == "After Approval") {
            taggedby = 'After Approval';
        }
        else {
            taggedby = 'CAD';
        }
            var params = {
                document_gid: doclistGId,
                application_gid: application_gid,
                credit_gid: contact_gid,
                taggedby: taggedby
            }

            if (doc_gid != undefined) {
                var url = 'api/AgrTrnCAD/PostIndividualCheckList';
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

        $scope.CheckAllIndividualDocument = function () {
            var params = {
                application_gid: application_gid,
                credit_gid: contact_gid,
                applicant_type: 'Individual'
            };

            var url = "api/AgrTrnCAD/CheckALLDocumentList";
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
            if (lspage == "myapp") {
                $location.url('app/AgrTrnStartCreditUnderwriting?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "CreditApproval") {
                $location.url('app/AgrTrnStartCreditUnderwriting?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "PendingCADReview") {
                $location.url('app/AgrTrnCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "CADApplicationEdit") {
                $location.url('app/AgrTrnCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "CADAcceptanceCustomers") {
                $location.url('app/AgrTrnCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else {

            }

        }

        $scope.individual_docchecklist = function () {
            $location.url('app/AgrTrnIndividualDocCheckList?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_covenantdocchecklist = function () {
            $location.url('app/AgrTrnIndividualCovenantDocChecklist?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_bureauadd = function () {
            $location.url('app/AgrTrnCreditIndividualDtlAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_bankaccount = function () {
            $location.url('app/AgrTrnCreditIndividualBankAcctAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_existingbankaccount = function () {
            $location.url('app/AgrTrnCreditIndividualExistingBankAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_PSLdata = function () {
            $location.url('app/AgrTrnCreditIndividualPSLDataFlagAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_repayment = function () {
            $location.url('app/AgrTrnCreditIndividualRepaymentAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_observation = function () {
            $location.url('app/AgrTrnCreditIndividualObservationAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.creditbankacctdtl_edit = function (creditbankdtl_gid) {
            $location.url('app/AgrTrnCreditIndividualBankAcctEdit?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&creditbankdtl_gid=' + creditbankdtl_gid + '&lspage=' + lspage);
        }
        //KYC API
        $scope.iecdetailed_vertification = function () {
            $location.url('app/AgrTrnCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=IECDETAILED' + '&lspage=' + lspage);
        }
        $scope.fssai_vertification = function () {
            $location.url('app/AgrTrnCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=FSSAI' + '&lspage=' + lspage);
        }
        $scope.fda_vertification = function () {
            $location.url('app/AgrTrnCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=FDA' + '&lspage=' + lspage);
        }
        $scope.lpgid_vertification = function () {
            $location.url('app/AgrTrnCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=LPGID' + '&lspage=' + lspage);
        }
        $scope.shop_vertification = function () {
            $location.url('app/AgrTrnCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=SHOP' + '&lspage=' + lspage);
        }
        $scope.rcauthadv_vertification = function () {
            $location.url('app/AgrTrnCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=RCAUTHADV' + '&lspage=' + lspage);
        }
        $scope.rcsearch_vertification = function () {
            $location.url('app/AgrTrnCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=RCSEARCH' + '&lspage=' + lspage);
        }
        $scope.propertytax_vertification = function () {
            $location.url('app/AgrTrnCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=PROPERTYTAX' + '&lspage=' + lspage);
        }
        $scope.company_bankstatement = function () {
            $location.url('app/AgrTrnCreditIndividualBankStatementAnalysisAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }
        $scope.crimecheck_record = function () {
            $location.url('app/AgrTrnCreditCrimeCheckRecordAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=PROPERTYTAX' + '&lspage=' + lspage);
        }
        $scope.crimecheckreport_realtime = function () {
            $location.url('app/AgrTrnCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=CRIMECHECKREPORTREALTIME' + '&lspage=' + lspage);
        }
    }
})();
