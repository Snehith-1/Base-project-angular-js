(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnDocumentCheckListController', AgrTrnDocumentCheckListController);

        AgrTrnDocumentCheckListController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function AgrTrnDocumentCheckListController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnDocumentCheckListController';
        $scope.institution_gid = $location.search().institution_gid;
        var institution_gid = $scope.institution_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.lstype = $location.search().lstype;
        var lstype = $scope.lstype;
        $scope.document_status = $location.search().lsdocument_status;
        var document_status = $scope.document_status;
        //OK OK
        lockUI();
        activate();
        function activate() {
            $scope.covenantall = false;
            $scope.all = false;
            var params = {
                credit_gid: institution_gid
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
                credit_gid: institution_gid,
                applicant_type: 'Institution'
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
                    else
                    { $scope.txtstakeholder_type_flag = 'Y'; }
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
                credit_gid: institution_gid,
                application_gid: application_gid
            }
            if ($scope.lstype == 'Individual') {
                var url = "api/AgrTrnCAD/GetIndividualTypeList";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.documentlist_gid = resp.data.CADDocument;
                });
            }
            else if ($scope.lstype == 'Group') {
                var params = {
                    credit_gid: institution_gid,
                }
                var url = "api/AgrTrnCAD/GetGroupTypeList";
                 SocketService.getparams(url, params).then(function (resp) {
                    $scope.documentlist_gid = resp.data.CADDocument;
                });
            }
            else { 
                var params = {
                    credit_gid: institution_gid,
                    application_gid: application_gid
                }
                var url = "api/AgrTrnCAD/GetDocumentTypeList";
                SocketService.getparams(url, params).then(function (resp) {
                   /* if (resp.data.CADDocument != null) {*/
                        $scope.documentlist_gid = resp.data.CADDocument;
                    /*}*/
                });
            }

            var params = {
                credit_gid: institution_gid
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


        $scope.untag = function (documentcheckdtl_gid, institution2documentupload_gid) {
            documentcheckdtl_gid = documentcheckdtl_gid != "" ? documentcheckdtl_gid : institution2documentupload_gid;
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
        $scope.removetoall = function () {
            var params = {
                application_gid: application_gid,
                credit_gid: institution_gid,
                applicant_type: 'Institution'
            };

            var url = "api/AgrTrnCAD/RemoveALLDocumentList";
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
        $scope.applytoall = function () {
            var params = {
                application_gid: application_gid,
                credit_gid: institution_gid,
                applicant_type: 'Institution'
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
            angular.forEach($scope.documentlist_gid, function (val) {
                val.checked = selected;
            });
        }

        $scope.addDoc_checklist = function (covenantdocumentclick) {
            var doc_gid;
            var doclistGId = [];
            angular.forEach($scope.documentlist_gid, function (val) {
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
                credit_gid: institution_gid,
                lstype: lstype,
                taggedby: taggedby
            }
            //consolelog(params)
            if (doc_gid != undefined) {
                var url = 'api/AgrTrnCAD/PostDocumentCheckList';
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

        $scope.CheckAllInstitutionDocument = function () {
            var params = {
                application_gid: application_gid,
                credit_gid: institution_gid,
                applicant_type: 'Institution'
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

        $scope.company_deferraldocchecklist = function () {
            $location.url('app/AgrTrnDocumentCheckList?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_covenantdocchecklist = function () {
            $location.url('app/AgrTrnCreditAddCovenantCheckList?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_economicadd = function () {
            $location.url('app/AgrTrnCreditEconomicCapitalAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_geneticadd = function () {
            $location.url('app/AgrTrnCreditCompanyDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_PSLdata = function () {
            $location.url('app/AgrTrnCreditPSLDataFlaggingAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_supplier = function () {
            $location.url('app/AgrTrnCreditSuppliersDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_buyer = function () {
            $location.url('app/AgrTrnCreditBuyerDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_bankaccount = function () {
            $location.url('app/AgrTrnCreditBankAccountDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_existingbankaccount = function () {
            $location.url('app/AgrTrnCreditExistingBankDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_repayment = function () {
            $location.url('app/AgrTrnCreditRepaymentDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_observation = function () {
            $location.url('app/AgrTrnCreditObservationAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.tan_verification = function () {
            $location.url('app/AgrTrnCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=TAN' + '&lspage=' + lspage);
        }
        $scope.companyllpno_vertification = function () {
            $location.url('app/AgrTrnCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=LLPNO' + '&lspage=' + lspage);
        }
        $scope.mcasign_vertification = function () {
            $location.url('app/AgrTrnCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=MCASIGNATURE' + '&lspage=' + lspage);
        }
        $scope.iecdetailed_vertification = function () {
            $location.url('app/AgrTrnCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=IECDETAILED' + '&lspage=' + lspage);
        }
        $scope.fssai_vertification = function () {
            $location.url('app/AgrTrnCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=FSSAI' + '&lspage=' + lspage);
        }
        $scope.fda_vertification = function () {
            $location.url('app/AgrTrnCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=FDA' + '&lspage=' + lspage);
        }
        $scope.gst_verification = function () {
            $location.url('app/AgrTrnCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=GST' + '&lspage=' + lspage);
        }
        $scope.lpgid_verification = function () {
            $location.url('app/AgrTrnCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=LPGID' + '&lspage=' + lspage);
        }
        $scope.shop_vertification = function () {
            $location.url('app/AgrTrnCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=SHOP' + '&lspage=' + lspage);
        }
        $scope.rcauthadv_vertification = function () {
            $location.url('app/AgrTrnCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=RCAUTHADV' + '&lspage=' + lspage);
        }
        $scope.rcsearch_vertification = function () {
            $location.url('app/AgrTrnCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=RCSEARCH' + '&lspage=' + lspage);
        }
        $scope.propertytax_vertification = function () {
            $location.url('app/AgrTrnCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=PROPERTYTAX' + '&lspage=' + lspage);
        }
        $scope.probe42_api = function () {
            $location.url('app/AgrTrnCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=PROBEAPI' + '&lspage=' + lspage);
        }
        $scope.company_bankstatement = function () {
            $location.url('app/AgrTrnCreditBankStatementAnalysisAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.company_fsa = function () {
            $location.url('app/AgrTrnCreditFsaDetailAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.institution_bureauadd = function () {
            $location.url('app/AgrTrnCreditInstitutionDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }       
        $scope.crimecheck_record = function () {
            $location.url('app/AgrTrnCompanyCrimeCheckRecordAPI?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.crimecheckreport_realtime = function () {
            $location.url('app/AgrTrnCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=CRIMECHECKREPORTREALTIME' + '&lspage=' + lspage);    
        }
    }
})();
