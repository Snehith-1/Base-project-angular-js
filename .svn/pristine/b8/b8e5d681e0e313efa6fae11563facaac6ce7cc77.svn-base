(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCADGroupCovenantDocChecklistcontroller', MstCADGroupCovenantDocChecklistcontroller);

    MstCADGroupCovenantDocChecklistcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', '$resource'];

    function MstCADGroupCovenantDocChecklistcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, $resource) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCADGroupCovenantDocChecklistcontroller';

        $scope.group_gid = $location.search().group_gid;
        var group_gid = $scope.group_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        lockUI();
        activate();

        function activate() {
            $scope.covenantall = false;
            var params = {
                credit_gid: group_gid
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
                credit_gid: group_gid,
                applicant_type: 'Group'
            }

            var url = 'api/MstCADCreditAction/GetCreditOperationsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtgroup_name = resp.data.group_name;
                $scope.txtgroup_type = resp.data.group_type;
            });
            var params = {
                credit_gid: '', 
                application_gid: application_gid
            }
            var url = "api/MstCADCreditAction/GetCovenantGroupDocumentList";
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.CADDocument != null) {
                    $scope.covenant_documentlist = resp.data.CADDocument;
                }
            });

            var params = {
                credit_gid: group_gid
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
                        var getselected = $scope.covenant_taggeddoclist.find(function (a) { return a.companydocument_gid === value.document_gid })
                        if (getselected != null) {
                            value.covenantchecked = true;
                            value.covenantperiod = getselected.covenantperiod == 'Every month' ? 1 : getselected.covenantperiod == 'Every 3 months' ? 2 : getselected.covenantperiod == 'Every 6 months' ? 3 : ""
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
            if (checked)
                $(dropdown).prop('disabled', false);
            else {
                $(dropdown).prop('disabled', true);
                $(dropdown).val('');
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

        $scope.checkall = function (selected, covenantdocumentclick) {
            angular.forEach($scope.covenant_documentlist, function (val, key) {
                val.covenantchecked = selected;
                var dropdown = '#drop_' + key;
                if (selected)
                    $(dropdown).prop('disabled', false);
                else {
                    $(dropdown).prop('disabled', true);
                    // val.covenantperiod = "";

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
                credit_gid: group_gid
            }

            if (doc_gid != undefined) {
                var url = 'api/MstCADCreditAction/PostGroupCheckList';
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
            angular.forEach($scope.covenant_documentlist, function (val) {
                if ($scope.covenant_taggeddoclist != undefined) {
                    var getselected = $scope.covenant_taggeddoclist.find(function (a) { return a.companydocument_gid === val.document_gid })
                    if (getselected != null) {
                        val.group2document_gid = getselected.group2document_gid;
                        val.groupcovdocumentchecklist_gid = getselected.groupcovdocumentchecklist_gid;
                    }
                }
                val.application_gid = application_gid;
                val.credit_gid = group_gid;
                val.lstype = 'Group';
                val.companydocument_gid = val.document_gid;
                if (val.covenantchecked == true) {
                    if (val.covenantperiod == "" || val.covenantperiod == undefined || val.covenantperiod == null)
                        emptycovenant = true;
                    val.covenantperiod = val.covenantperiod == 1 ? 'Every month' : val.covenantperiod == 2 ? 'Every 3 months' : val.covenantperiod == 3 ? 'Every 6 months' : ""
                }
            });
            if (emptycovenant) {
                angular.forEach($scope.covenant_documentlist, function (val) {
                    val.covenantperiod = val.covenantperiod == 'Every month' ? 1 : val.covenantperiod == 'Every 3 months' ? 2 : val.covenantperiod == 'Every 6 months' ? 3 : "";
                });
                unlockUI();
                Notify.alert('Kindly Select the Covenant Periods for the selected Document..!', 'warning')
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

        $scope.CheckAllGroupDocument = function () {
            var params = {
                application_gid: application_gid,
                credit_gid: group_gid,
                applicant_type: 'Group'
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

        $scope.group_addguarantee = function () {
            $location.url('app/MstCADGroupGuaranteeDtlAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }

        $scope.group_docchecklist = function () {
            $location.url('app/MstCADGroupDocCheckList?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }

        $scope.group_covenantdocchecklist = function () {
            $location.url('app/MstCADGroupCovenantDocChecklist?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }

        $scope.group_bureauadd = function () {
            $location.url('app/MstCADCreditGroupDtlAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }

        $scope.group_bankaccount = function () {
            $location.url('app/MstCADCreditGroupBankAcctAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }

        $scope.group_existingbankaccount = function () {
            $location.url('app/MstCADCreditGroupExistingBankAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }

        $scope.group_PSLdata = function () {
            $location.url('app/MstCADCreditGroupPSLDataFlagAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }

        $scope.group_repayment = function () {
            $location.url('app/MstCADCreditGroupRepaymentAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }

        $scope.group_observation = function () {
            $location.url('app/MstCADCreditGroupObservationAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }

        $scope.company_bankstatement = function () {
            $location.url('app/MstCADCreditGroupBankStatementAnalysisAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }
    }
})();
