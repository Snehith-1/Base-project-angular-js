(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnSuprCadDocumentChecklistAddController', AgrTrnSuprCadDocumentChecklistAddController);

    AgrTrnSuprCadDocumentChecklistAddController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AgrTrnSuprCadDocumentChecklistAddController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnSuprCadDocumentChecklistAddController';

        $scope.credit_gid = $location.search().credit_gid;
        var credit_gid = $scope.credit_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.lstype = $location.search().lstype;
        var lstype = $scope.lstype;
        $scope.lspath = localStorage.getItem('TaggedBy');
        var lspath = $scope.lspath;
        var lsfollowup = $location.search().lsfollowup;

        lockUI();
        activate();
        function activate() {

            var params = {
                credit_gid: credit_gid
            }
            //var url = 'api/AgrMstApplicationAdd/GetGeneticCodeList';
            //SocketService.get(url).then(function (resp) {
            //    unlockUI();
            //    $scope.genetic_list = resp.data.genetic_list;
            //});

            var url = 'api/AgrTrnSuprAppCreditUnderWriting/GetGeneticCodeList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.mstgeneticcode_list = resp.data.mstcuwgeneticcode_list;
            });
            $scope.showdeferraledit = true;
            $scope.showcovenentedit = true;
            if (lsfollowup == 'Y') {
                $scope.followup = false; 
            } 
            else
                $scope.followup = true;

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
                credit_gid: credit_gid,
                applicant_type: 'Institution'
            }

            var url = 'api/AgrTrnSuprAppCreditUnderWriting/GetCreditOperationsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtinstitution_name = resp.data.company_name;
                $scope.txtstakeholder_type = resp.data.stakeholder_type;
            });
            var params = {
                credit_gid: credit_gid
            }
            if ($scope.lstype == 'Institution') {
                var url = "api/AgrTrnSuprCAD/GetDocumentTypeList";
                SocketService.getparams(url, params).then(function (resp) { 
                    $scope.documentlist_gid = resp.data.CADDocument;
                });

                var url = "api/AgrTrnSuprCAD/GetCovenantDocumentTypeList";
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.CADDocument != null) {
                        $scope.covenant_documentlist = resp.data.CADDocument;
                    }
                });
            }
            else if ($scope.lstype == 'Individual') {
                var url = "api/AgrTrnSuprCAD/GetIndividualTypeList";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.documentlist_gid = resp.data.CADDocument;
                });

                var url = "api/AgrTrnSuprCAD/GetCovenantIndividualDocumentList";
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.CADDocument != null) {
                        $scope.covenant_documentlist = resp.data.CADDocument;
                    }
                });
            }
            else if ($scope.lstype == 'Group') {
                var url = "api/AgrTrnSuprCAD/GetGroupTypeList";
                SocketService.getparams(url, params).then(function (resp) { 
                    $scope.documentlist_gid = resp.data.CADDocument;
                });

                var url = "api/AgrTrnSuprCAD/GetCovenantGroupDocumentList";
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.CADDocument != null) {
                        $scope.covenant_documentlist = resp.data.CADDocument;
                    }
                });
            }
            else {
                var url = "api/AgrTrnSuprCAD/GetDocumentTypeList";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.documentlist_gid = resp.data.CADDocument;
                });

                var url = "api/AgrTrnSuprCAD/GetCovenantDocumentTypeList";
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.CADDocument != null) {
                        $scope.covenant_documentlist = resp.data.CADDocument;
                    }
                });
            }

            var params = {
                credit_gid: credit_gid
            }
            var url = "api/AgrTrnSuprCAD/GetCADTrnTaggedDocList";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.taggeddoc_list = resp.data.TaggedDocument;
                angular.forEach($scope.taggeddoc_list, function (value, key) {
                    if (value.taggedby == "N")
                        value.taggedby = 'Business'; 
                });
            });


            var geturl = "api/AgrTrnSuprCAD/GetCADTrnCovenantTaggedDocList";
            SocketService.getparams(geturl, params).then(function (resp) {
                if (resp.data.TaggedDocument != null) {
                    $scope.covenant_taggeddoclist = resp.data.TaggedDocument;
                    angular.forEach($scope.covenant_taggeddoclist, function (value, key) {
                        if (value.taggedby == "N")
                            value.taggedby = 'Business'; 
                    });
                    angular.forEach($scope.covenant_documentlist, function (value, key) {
                        var getselected = $scope.covenant_taggeddoclist.find(function (a) { return a.companydocument_gid === value.document_gid})
                        if (getselected != null) {
                            value.covenantperiod = getselected.covenantperiod == 'Each month' ? 1 : getselected.covenantperiod == 'Every 3 months' ? 2 : getselected.covenantperiod == 'Every 6 months' ? 3 : ""
                            value.groupcovdocumentchecklist_gid = getselected.groupcovdocumentchecklist_gid;
                            if (getselected.taggedby == "N")
                                value.taggedby = 'Business';
                            else
                                value.taggedby = getselected.taggedby;
                        }
                    });
                }
            });
        }

        $scope.changeDocumenttypeinfo = function (cbocadgroup_name) {

            $scope.txtdocumenttype_name = cbocadgroup_name.documenttype_name;
        }

        $scope.buttonEditDeferralDocu = function () {
            $scope.showdeferraledit = false;
        }

        $scope.buttonCancelDeferralDocu = function () {
            $scope.showdeferraledit = true;
        }

        $scope.buttonEditCovenantDocu = function () {
            $scope.showcovenentedit = false;
        }

        $scope.buttonCancelCovenantDocu = function () {
            $scope.showcovenentedit = true;
        }

        $scope.untag = function (groupcovdocumentchecklist_gid, covenant) {
            var url = "api/AgrTrnSuprCAD/UnTagDocument";
            var params = {
                documentcheckdtl_gid: groupcovdocumentchecklist_gid
            };
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    activate();
                    if (covenant) {
                        $scope.showdeferraledit = true;
                        $scope.showcovenentedit = false;
                    }
                    else {
                        $scope.showdeferraledit = false;
                        $scope.showcovenentedit = true;
                    }
                   
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
        $scope.checkall = function (selected) {
            angular.forEach($scope.documentlist_gid, function (val) {
                val.checked = selected;
            });
        }

        $scope.addCovenanatDoc_checklist = function (covenantdocumentclick, cbodocumentinfo, cbocovenantperiod) {
            if (covenantdocumentclick && (cbocovenantperiod == undefined || cbocovenantperiod == "")) {
                Notify.alert('Please fill all mandatory details!', 'warning')
            }
            else {
                var doc_gid = cbodocumentinfo.document_gid;
                cbodocumentinfo.covenantperiod = cbocovenantperiod == 1 ? 'Each month' : cbocovenantperiod == 2 ? 'Every 3 months' : cbocovenantperiod == 3 ? 'Every 6 months' : ""
                cbodocumentinfo.covenantchecked = true;
                cbodocumentinfo.documentcheckdtl_gid = null;
                cbodocumentinfo.application_gid = application_gid;
                cbodocumentinfo.credit_gid = credit_gid;
                cbodocumentinfo.companydocument_gid = cbodocumentinfo.document_gid;
                cbodocumentinfo.lstype = lstype;
                var taggeddocument_list = [];
                taggeddocument_list.push(cbodocumentinfo);

                var params = {
                    taggedby: lspath,
                    CovenantPeriod: taggeddocument_list
                }
                var url = 'api/AgrTrnSuprCAD/PostCovenantPeriods';
                if (doc_gid != undefined) {
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            activate();
                            $scope.showdeferraledit = true;
                            $scope.showcovenentedit = false;
                            $scope.cbocovenantperiod = "";
                            $scope.txtdocumenttype_name = "";
                            Notify.alert('Covenant Periods are added successfully..!', {
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
        }

        $scope.covenantcancelclick = function () {
            $scope.cbocovenantperiod = "";
            $scope.cbodocumentinfo = "";
            $scope.txtdocumenttype_name = "";
        }

        $scope.addDoc_checklist = function () {

            var doc_gid;
            var doclistGId = [];
            angular.forEach($scope.documentlist_gid, function (val) {
                if (val.checked == true) {
                    var doclist_gid = val.document_gid;
                    doc_gid = val.document_gid;
                    doclistGId.push(doclist_gid);
                }
            });

            var params = {
                document_gid: doclistGId,
                application_gid: application_gid,
                credit_gid: credit_gid,
                lstype: lstype,
                taggedby: lspath
            }

            if (doc_gid != undefined) {
                var url = 'api/AgrTrnSuprCAD/PostDocumentCheckList';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI();
                        activate();

                        $scope.showdeferraledit = false;
                        $scope.showcovenentedit = true;
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

        $scope.CheckAllDocument = function () {
            var params = {
                application_gid: application_gid,
                credit_gid: institution_gid,
                applicant_type: lstype
            };
            var url = "api/AgrTrnSuprCAD/CheckALLDocumentList";
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
            if (lspage == "CadDocumentChecklist") {
                $location.url('app/AgrTrnSuprCadGuarantorDetails?application_gid=' + application_gid + '&lspage=' + lspage + '&lsfollowup=' + lsfollowup);
            }
            else {

            }

        }
    }
})();