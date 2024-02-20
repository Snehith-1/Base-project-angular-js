(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditAddCovenantCheckListController', MstCreditAddCovenantCheckListController);

    MstCreditAddCovenantCheckListController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function MstCreditAddCovenantCheckListController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCreditAddCovenantCheckList';
        $scope.institution_gid = $location.search().institution_gid;
        var institution_gid = $scope.institution_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.lstype = $location.search().lstype;
        var lstype = $scope.lstype;
       
        activate();
        function activate() {
            $scope.covenantall = false;

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
                credit_gid: '',
                application_gid: application_gid
            }

            var url = "api/MstCAD/GetCovenantDocumentTypeList";
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
               /* if (resp.data.CADDocument != null) {*/
                    $scope.covenant_documentlist = resp.data.CADDocument;
                /*}*/
            });
            var params = {
                credit_gid: institution_gid
            }
            var geturl = "";
            if (lspage != "myapp" || lspage != "CreditApproval" || lspage != "PendingCADReview")
                geturl = "api/MstCAD/GetCADTrnCovenantTaggedDocList";
            else
                geturl = "api/MstCAD/GetCADCovenantTaggedDocList";
            lockUI();
            SocketService.getparams(geturl, params).then(function (resp) {
                unlockUI();
                if (resp.data.TaggedDocument != null) {
                    $scope.covenant_taggeddoclist = resp.data.TaggedDocument;
                    angular.forEach($scope.covenant_documentlist, function (value, key) {
                        var getselected = $scope.covenant_taggeddoclist.find(function (a) { return a.companydocument_gid === value.document_gid })
                        if (getselected != null) {
                            value.groupcovdocumentchecklist_gid = getselected.groupcovdocumentchecklist_gid;
                            value.covenantchecked = true;
                            value.covenantperiod = getselected.covenantperiod == 'Every month' ? 1 : getselected.covenantperiod == 'Every 3 months' ? 2 : getselected.covenantperiod == 'Every 6 months' ? 3 : getselected.covenantperiod == 'Every 1 year' ? 4 : ""
                            value.buffer_days = getselected.buffer_days == '0 days' ? 1 : getselected.buffer_days == '5 days' ? 2 : getselected.buffer_days == '10 days' ? 3 : getselected.buffer_days == '15 days' ? 4 : getselected.buffer_days == '20 days' ? 5 :
                             getselected.buffer_days == '25 days' ? 6 : getselected.buffer_days == '30 days' ? 7 : getselected.buffer_days == '35 days' ? 8 : getselected.buffer_days == '40 days' ? 9 :
                             getselected.buffer_days == '45 days' ? 10 : getselected.buffer_days == '50 days' ? 11 : getselected.buffer_days == '55 days' ? 12 : getselected.buffer_days == '60 days' ? 13 : "";
                            value.dropdownchk = false;
                            if ((lspage == "myapp" || lspage == "CreditApproval" || lspage == "PendingCADReview") && getselected.taggedby == "Credit")
                                value.taggedby = '';
                            else if ((lspage != "myapp" || lspage != "CreditApproval" || lspage != "PendingCADReview") && getselected.taggedby == "CAD")
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
            if (checked){
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
            var url = "api/MstCAD/UnTagDocument";
            var params = {
                documentcheckdtl_gid: documentcheckdtl_gid
            };
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                   
                    activate();
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

            });
        }
        $scope.applytoallcovenant = function () {
            var taggeddocument_list = $scope.covenant_documentlist;
            var taggedby = '';
            //if (lspage == "myapp" || lspage == "CreditApproval")
            //    taggedby = 'Credit';
            //else
            //    var params = {
            //        application_gid: application_gid,
            //        credit_gid: contact_gid,
            //        applicant_type: 'Individual'
            //    };
            var params = {
                application_gid: application_gid,
                credit_gid: institution_gid,
                applicant_type: 'Institution'
            };
            if (taggeddocument_list && taggeddocument_list != null) {
                var url = "api/MstCAD/ApplyALLCovenantDocumentList";
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
                if (selected){
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
                taggeddocumentlist = $scope.documentlist_gid
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
                credit_gid: institution_gid,
                lstype: lstype
            }
            //consolelog(params)
            if (doc_gid != undefined) {
                var url = 'api/MstCAD/PostDocumentCheckList';
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
                    var getselected = $scope.covenant_taggeddoclist.find(function (a) { return a.companydocument_gid === val.document_gid })
                    if (getselected != null) {
                        val.groupcovdocumentchecklist_gid = getselected.groupcovdocumentchecklist_gid;
                    }
                }
                val.lstype = 'Institution';
                val.application_gid = application_gid;
                val.credit_gid = institution_gid;
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
            //if (emptycovenant) {
            //     angular.forEach($scope.covenant_documentlist, function (val) {
            //        val.covenantperiod = val.covenantperiod == 'Every month' ? 1 : val.covenantperiod == 'Every 3 months' ? 2 : val.covenantperiod == 'Every 6 months' ? 3 : getselected.covenantperiod == 'Every 1 year' ? 4 : "";
            //    });
            //    unlockUI();
            //    Notify.alert('Kindly Select the Covenant Periods for the selected Document..!', 'warning')
            //    return false;

            //}
            //if (emptybuffer) {
            //     angular.forEach($scope.covenant_documentlist, function (val) {
            //        val.buffer_days = val.buffer_days == '0 days' ? 1 : val.buffer_days == '5 days' ? 2 : val.buffer_days == '10 days' ? 3 : val.buffer_days == '15 days' ? 4 : val.buffer_days == '20 days' ? 5 :
            //            val.buffer_days == '25 days' ? 6 : val.buffer_days == '30 days' ? 7 : val.buffer_days == '35 days' ? 8 : val.buffer_days == '40 days' ? 9 :
            //                val.buffer_days == '45 days' ? 10 : val.buffer_days == '50 days' ? 11 : val.buffer_days == '55 days' ? 12 : val.buffer_days == '60 days' ? 13 : "";

            //    });
            //    unlockUI();
            //    Notify.alert('Kindly Select the Buffer Days for the selected Document..!', 'warning')
            //    return false;

            //}
            //else {
                var taggeddocument_list = $scope.covenant_documentlist;
                var taggedby = '';
                if (lspage == "myapp" || lspage == "CreditApproval" || lspage == "PendingCADReview")
                    taggedby = 'Credit';
                else
                    taggedby = 'CAD';
                var params = {
                    taggedby: taggedby,
                    CovenantPeriod: taggeddocument_list
                }
                if (taggeddocument_list && taggeddocument_list != null) {
                    var url = 'api/MstCAD/PostCovenantPeriods';
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
                            activate();
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
          /*  }*/

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
        $scope.probe42_api = function () {
            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=PROBEAPI' + '&lspage=' + lspage);
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
