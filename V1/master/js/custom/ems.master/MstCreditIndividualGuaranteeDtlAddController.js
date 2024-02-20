(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditIndividualGuaranteeDtlAddController', MstCreditIndividualGuaranteeDtlAddController);

    MstCreditIndividualGuaranteeDtlAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService','cmnfunctionService'];

    function MstCreditIndividualGuaranteeDtlAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCreditIndividualGuaranteeDtlAddController';

        $scope.contact_gid = $location.search().contact_gid;
        var contact_gid = $scope.contact_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;

        lockUI();
        activate();
        function activate() {

            var url = 'api/MstAppCreditUnderWriting/GetGuaranteeProgramType';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.guaranteedtltype_list = resp.data.guaranteedtltype_list;
            });

            var params = {
                credit_gid: contact_gid
            }
            var url = 'api/MstAppCreditUnderWriting/GetInstitutionGuaranteeDtl';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.creditguaranteedtl_list = resp.data.creditguaranteedtl_list;
            });

            var params = {
                credit_gid: contact_gid,
                applicant_type: 'Individual'
            }
            var url = 'api/MstAppCreditUnderWriting/GetCreditOperationsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtindividual_name = resp.data.individual_name;
                $scope.txtstakeholder_type = resp.data.stakeholder_type;
            });

            var url = 'api/MstAppCreditUnderWriting/GuaranteeDocTmpClear';
            SocketService.get(url).then(function (resp) {
                unlockUI();
            });
        }

        $scope.guarantee_applicable = function (rdbGuaranteeApplicability) {

            var rdbGuaranteeApplicability = rdbGuaranteeApplicability;

            if (rdbGuaranteeApplicability == 'Yes') {
                $scope.guarantee_show = true;
            }
            else if (rdbGuaranteeApplicability == 'No') {
                $scope.guarantee_show = false;
            }
            else {
                $scope.guarantee_show = false;
            }
        }


        $scope.guaranteedocumentUpload = function (val) {

            if (($scope.txtdocument_title == null) || ($scope.txtdocument_title == '') || ($scope.txtdocument_title == undefined)) {
                $("#chequefilefile").val('');
                Notify.alert('Kindly Enter the Document Title', 'warning');
            }
            else {

                var frm = new FormData();
                for (var i = 0; i < val.length; i++) {
                    var item = {
                        name: val[i].name,
                        file: val[i]
                    };
                    frm.append('fileupload', item.file);
                    frm.append('file_name', item.name);
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[i].name, "documentformatonly");
                    if (IsValidExtension == false) {
                        Notify.alert("File format is not supported..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });

                        return false;
                    }

                }
                frm.append('creditguaranteedtl_gid', '');
                frm.append('document_title', $scope.txtdocument_title);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    lockUI();
                    var url = 'api/MstAppCreditUnderWriting/GuaranteeDocumentUpload';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                        $scope.creditguaranteedocument_list = resp.data.creditguaranteedocument_list;
                        unlockUI();

                        $("#chequefilefile").val('');
                        $scope.uploadfrm = undefined;

                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $scope.txtdocument_title = '';
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
                else {
                    alert('Document is not Available..!');
                    return;
                }
            }
        }

        $scope.documentviewer = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
                return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
        }

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.uploaddocumentcancel = function (creditguaranteedtldocument_gid) {
            lockUI();
            var params = {
                creditguaranteedtldocument_gid: creditguaranteedtldocument_gid,
                credit_gid: contact_gid
            }
            var url = 'api/MstAppCreditUnderWriting/DeleteGuaranteeDtlDocument';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.creditguaranteedocument_list = resp.data.creditguaranteedocument_list;
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
                unlockUI();
            });
        }

        $scope.downloadall_guarantee = function () {
            for (var i = 0; i < $scope.creditguaranteedocument_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.creditguaranteedocument_list[i].document_path, $scope.creditguaranteedocument_list[i].document_name);
            }
        }

        $scope.guarantee_delete = function (creditguaranteedtl_gid) {
            var params = {
                creditguaranteedtl_gid: creditguaranteedtl_gid,
                credit_gid: contact_gid
            }
            var url = 'api/MstAppCreditUnderWriting/DeleteGuaranteeDtl';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.creditguaranteedtl_list = resp.data.creditguaranteedtl_list;
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

        $scope.guarantee_remarks = function (creditguaranteedtl_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/RemarksView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                {
                    creditguaranteedtl_gid: creditguaranteedtl_gid
                }
                var url = 'api/MstAppCreditUnderWriting/GetGuaranteeRemarksView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtguarantee_remarks = resp.data.remarks;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.guarantee_docview = function (creditguaranteedtl_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/GuaranteedocumentsView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                {
                    creditguaranteedtl_gid: creditguaranteedtl_gid
                }
                var url = 'api/MstAppCreditUnderWriting/GetGuaranteeDocDtl';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.GuaranteeDocumentView_List = resp.data.GuaranteeDocumentView_List;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.documentviewer = function (val1, val2) {
                    lockUI();
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                    if (IsValidExtension == false) {
                        Notify.alert("View is not supported for this format..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        return false;
                    }
                    DownloaddocumentService.DocumentViewer(val1, val2);
                }

                $scope.downloadallguarantee_doc = function () {
                    for (var i = 0; i < $scope.GuaranteeDocumentView_List.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.GuaranteeDocumentView_List[i].document_path, $scope.GuaranteeDocumentView_List[i].document_name);
                    }
                }

                $scope.download_guaranteedoc = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }


            }

        }

        $scope.add_creditguaranteedtl = function () {
            if (($scope.rdbGuaranteeApplicability == 'Yes') && (($scope.cboGuaranteeProgram == '') || ($scope.cboGuaranteeProgram == undefined) || ($scope.cboGuaranteeProgram == '')
                )) {
                Notify.alert('Enter All Mandatory Fields', 'warning');
            }
            else if (($scope.rdbGuaranteeApplicability == '') || ($scope.rdbGuaranteeApplicability == undefined) || ($scope.rdbGuaranteeApplicability == '')) {
                Notify.alert('Enter All Mandatory Fields', 'warning');
            }
            else {
                var lsguaranteprogram_gid = '';
                var lsguaranteeprogram_name = '';

                if ($scope.cboGuaranteeProgram != undefined || $scope.cboGuaranteeProgram != null) {
                    lsguaranteprogram_gid = $scope.cboGuaranteeProgram.guarantee_gid;
                    lsguaranteeprogram_name = $scope.cboGuaranteeProgram.guarantee_name;
                }

                var params = {
                    application_gid: application_gid,
                    credit_gid: contact_gid,
                    applicant_type: 'Individual',
                    guarantee_applicability: $scope.rdbGuaranteeApplicability,
                    guaranteprogram_gid: lsguaranteprogram_gid,
                    guaranteeprogram_name: lsguaranteeprogram_name,
                    remarks: $scope.txtremarks
                }
                var url = 'api/MstAppCreditUnderWriting/PostGuaranteeDtlAdd';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        $scope.creditguaranteedtl_list = resp.data.creditguaranteedtl_list;
                        $scope.creditguaranteedocument_list = null;
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
                    $scope.rdbGuaranteeApplicability = '';
                    $scope.cboGuaranteeProgram = '';
                    $scope.txtremarks = '';
                    activate();
                });
            }
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

        $scope.individual_addcolending = function () {
            $location.url('app/MstCreditIndividualColendingDtlAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_addguarantee = function () {
            $location.url('app/MstCreditIndividualGuaranteeDtlAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_docchecklist = function () {
            $location.url('app/MstIndividualDocCheckList?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_covenantdocchecklist = function () {
            $location.url('app/MstIndividualCovenantDocChecklist?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_bureauadd = function () {
            $location.url('app/MstCreditIndividualDtlAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_bankaccount = function () {
            $location.url('app/MstCreditIndividualBankAcctAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_existingbankaccount = function () {
            $location.url('app/MstCreditIndividualExistingBankAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_PSLdata = function () {
            $location.url('app/MstCreditIndividualPSLDataFlagAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_repayment = function () {
            $location.url('app/MstCreditIndividualRepaymentAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_observation = function () {
            $location.url('app/MstCreditIndividualObservationAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.creditbankacctdtl_edit = function (creditbankdtl_gid) {
            $location.url('app/MstCreditIndividualBankAcctEdit?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&creditbankdtl_gid=' + creditbankdtl_gid + '&lspage=' + lspage);
        }
        //KYC API
        $scope.iecdetailed_vertification = function () {
            $location.url('app/MstCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=IECDETAILED' + '&lspage=' + lspage);
        }
        $scope.fssai_vertification = function () {
            $location.url('app/MstCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=FSSAI' + '&lspage=' + lspage);
        }
        $scope.fda_vertification = function () {
            $location.url('app/MstCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=FDA' + '&lspage=' + lspage);
        }
        $scope.lpgid_vertification = function () {
            $location.url('app/MstCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=LPGID' + '&lspage=' + lspage);
        }
        $scope.shop_vertification = function () {
            $location.url('app/MstCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=SHOP' + '&lspage=' + lspage);
        }
        $scope.rcauthadv_vertification = function () {
            $location.url('app/MstCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=RCAUTHADV' + '&lspage=' + lspage);
        }
        $scope.rcsearch_vertification = function () {
            $location.url('app/MstCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=RCSEARCH' + '&lspage=' + lspage);
        }
        $scope.propertytax_vertification = function () {
            $location.url('app/MstCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=PROPERTYTAX' + '&lspage=' + lspage);
        }
        $scope.company_bankstatement = function () {
            $location.url('app/MstCreditIndividualBankStatementAnalysisAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }
        $scope.crimecheck_record = function () {
            $location.url('app/MstCreditCrimeCheckRecordAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=PROPERTYTAX' + '&lspage=' + lspage);
        }
        $scope.crimecheckreport_realtime = function () {
            $location.url('app/MstCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=CRIMECHECKREPORTREALTIME' + '&lspage=' + lspage);
        }


        $scope.downloadall = function () {
            for (var i = 0; i < $scope.credituploaddocument_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.credituploaddocument_list[i].chequeleaf_path, $scope.credituploaddocument_list[i].chequeleaf_name);
            }
        }
    }
})();
