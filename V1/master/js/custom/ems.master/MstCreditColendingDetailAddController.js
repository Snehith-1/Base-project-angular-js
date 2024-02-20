﻿(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditColendingDetailAddController', MstCreditColendingDetailAddController);

    MstCreditColendingDetailAddController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$sce', '$anchorScroll', 'DownloaddocumentService', 'cmnfunctionService'];

    function MstCreditColendingDetailAddController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $sce, $anchorScroll, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCreditColendingDetailAddController';
        $scope.institution_gid = $location.search().institution_gid;
        var institution_gid = $scope.institution_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.lstype = $location.search().lstype;
        var lstype = $scope.lstype;

        activate();
        lockUI();
        function activate() {

            var params = {
                credit_gid: institution_gid,
                application_gid: application_gid
            }           
            var url = 'api/MstAppCreditUnderWriting/GetColendingDtlSummary';
            SocketService.getparams(url,params).then(function (resp) {
                unlockUI();
                $scope.creditcolendingdtl_list = resp.data.creditcolendingdtl_list;
            });            

            var params = {
                credit_gid: institution_gid,
                application_gid: application_gid
            }
            var url = 'api/MstAppCreditUnderWriting/GetColendingApplicabilityStatus';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.rdbcolendingapplicability = resp.data.colendingaapplicability_stats;
                if ($scope.rdbcolendingapplicability == 'Yes') {
                    $scope.creditcolendingsummary = true;
                }
                else {
                    $scope.creditcolendingsummary = false;
                }
            });
            
            var params = {
                credit_gid: institution_gid,
                applicant_type: 'Institution'
            }
            var url = 'api/MstAppCreditUnderWriting/GetCreditOperationsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtinstitution_name = resp.data.company_name;
                $scope.txtstakeholder_type = resp.data.stakeholder_type;
            });

            var url = 'api/MstAppCreditUnderWriting/ColendingDocTmpClear';
            SocketService.get(url).then(function (resp) {
                unlockUI();
            });
        }      
        $scope.CreditColendingRule = function (colendingprogram_gid) {
            $location.url('app/MstCreditColendingRule?lscolendingprogram_gid=' + colendingprogram_gid + '&application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage + '&lscompany=' + "Company" ); 
        }        
       
        $scope.Colending_Applicability = function (rdbcolendingapplicability) {
            var rdbcolendingapplicability = rdbcolendingapplicability;
            if (rdbcolendingapplicability =='Yes') {
                $scope.creditcolendingsummary = true;
            }
            else {
                $scope.creditcolendingsummary = false;
            }
        } 

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }      

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.creditguaranteedocument_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.creditguaranteedocument_list[i].document_path, $scope.creditguaranteedocument_list[i].document_name);
            }
        }

        $scope.colending_remarks = function (portfolio_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/RemarksView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    portfolio_gid: portfolio_gid
                }
                var url = 'api/MstAppCreditUnderWriting/GetColendingRemarksView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtcolending_remarks = resp.data.colending_remarks;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.colending_docview = function (portfolio_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/ColenderdocumentsView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    portfolio_gid: portfolio_gid
                }
                var url = 'api/MstAppCreditUnderWriting/GetColendingRemarksView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtcolending_remarks = resp.data.colending_remarks;
                    $scope.txtwef_date = resp.data.wef_date;
;
                });
                
                var params = {
                    portfolio_gid: portfolio_gid
                }
                var url = 'api/MstAppCreditUnderWriting/GetColendingDocDtl';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.ColendingDocumentView_List = resp.data.ColendingDocumentView_List;

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

                $scope.downloadallcolending_doc = function () {
                    for (var i = 0; i < $scope.ColendingDocumentView_List.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.ColendingDocumentView_List[i].document_path, $scope.ColendingDocumentView_List[i].document_name);
                    }
                }

                $scope.download_colendingdoc = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }


            }

        }

        $scope.creditcolending_add = function (creditcolendingdtl_gid, colendingprogram_gid, portfolio_gid) {
            var creditcolendingdtl_gid = creditcolendingdtl_gid;
            var colendingprogram_gid = colendingprogram_gid;
            var portfolio_gid = portfolio_gid;
            var application_gid = $location.search().application_gid;
            var credit_gid = $location.search().institution_gid;

            var modalInstance = $modal.open({
                templateUrl: '/CreditColendingAdd.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
               
                var url = 'api/MstAppCreditUnderWriting/ColendingDocTmpClear';
                SocketService.get(url).then(function (resp) {
                    unlockUI();
                });

                var params = {
                    credit_gid: credit_gid,
                    application_gid: application_gid
                }
                var url = 'api/MstAppCreditUnderWriting/GetMyApplApplicabilityView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblcolendingapplicability = resp.data.colendingaapplicability_stats;
                });

                var params = {
                    creditcolendingdtl_gid: creditcolendingdtl_gid,
                    application_gid: application_gid
                }
                var url = 'api/MstAppCreditUnderWriting/GetColendingDtlsView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.rdbapplicability = resp.data.applicability;
                    $scope.txtcolendingremarks = resp.data.remarks;
                    $scope.creditcolendingdtl_gid = resp.data.creditcolendingdtl_gid;
                });

                var params = {
                    creditcolendingdtl_gid: creditcolendingdtl_gid,
                    credit_gid: institution_gid,
                    application_gid: application_gid
                }
                var url = 'api/MstAppCreditUnderWriting/ColendingDtlDocumentView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.creditcolendingdocument_list = resp.data.creditcolendingdocument_list;
                });

                $scope.submit_creditcolendingdtl = function () {
                    if (($scope.rdbapplicability == '') || ($scope.rdbapplicability == undefined) || ($scope.rdbapplicability == '') ||
                       ($scope.txtcolendingremarks == '') || ($scope.txtcolendingremarks == undefined) || ($scope.txtcolendingremarks == '')) {
                        alert('Enter All Mandatory Fields', 'warning');
                    }
                    else {
                       
                        var params = {
                            application_gid: application_gid,
                            credit_gid: institution_gid,
                            applicant_type: 'Institution',
                            colendingprogram_gid: colendingprogram_gid,
                            portfolio_gid : portfolio_gid,
                            applicability: $scope.rdbapplicability,
                            remarks: $scope.txtcolendingremarks,
                            creditcolendingdtl_gid: creditcolendingdtl_gid,
                            colendingaapplicability_stats: 'Yes'
                        }
                        var url = 'api/MstAppCreditUnderWriting/PostColendingDtlAdd';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {  
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $modalInstance.close('closed');
                                activate();
                            }
                            else {
                                Notify.alert(resp.data.message, {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $modalInstance.close('closed');
                            }                           
                            activate();
                        });
                    }
                }

                $scope.colendingdocumentUpload = function (val) {
                    if (($scope.txtdocument_title == null) || ($scope.txtdocument_title == '') || ($scope.txtdocument_title == undefined)) {
                        $("#Colendingfile").val('');
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
                        frm.append('document_title', $scope.txtdocument_title);
                        frm.append('creditcolendingdtl_gid', creditcolendingdtl_gid);
                        frm.append('colendingprogram_gid', colendingprogram_gid);
                        frm.append('portfolio_gid', portfolio_gid);
                        frm.append('application_gid', application_gid);
                        frm.append('credit_gid', credit_gid);
                        frm.append('project_flag', "Default");
                        $scope.uploadfrm = frm;
                        if ($scope.uploadfrm != undefined) {
                            lockUI();
                            var url = 'api/MstAppCreditUnderWriting/ColendingDocumentUpload';
                            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                                $scope.creditcolendingdocument_list = resp.data.creditcolendingdocument_list;
                                unlockUI();
                                $("#Colendingfile").val('');
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

                $scope.uploaddocumentcancel = function (creditcolendingdtldocument_gid) {
                    lockUI();
                    var params = {
                        creditcolendingdtldocument_gid: creditcolendingdtldocument_gid,
                        creditcolendingdtl_gid: creditcolendingdtl_gid,
                        credit_gid: institution_gid
                        /*application_gid: application_gid*/
                    }
                    var url = 'api/MstAppCreditUnderWriting/DeleteColendingDtlDocument';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $scope.creditcolendingdocument_list = resp.data.creditcolendingdocument_list;
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

                $scope.downloadall_colender = function () {
                    for (var i = 0; i < $scope.creditcolendingdocument_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.creditcolendingdocument_list[i].document_path, $scope.creditcolendingdocument_list[i].document_name);
                    }
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.company_addcolending = function () {
            $location.url('app/MstCreditColendingDetailAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_addguarantee = function () {
            $location.url('app/MstCreditGuaranteeDetailAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
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
                $location.url('app/MstStartCreditUnderwriting?application_gid=' + application_gid + '&lspage=' + lspage);
            }
        }

        $scope.submit_creditcolendingstatus = function () {
            if (($scope.rdbcolendingapplicability == '') || ($scope.rdbcolendingapplicability == undefined) || ($scope.rdbcolendingapplicability == '')) {
                Notify.alert('Select Co-lending Applicability', 'warning');
            }
            else if ((($scope.rdbapplicability != '') || ($scope.rdbapplicability != undefined) || ($scope.rdbapplicability != '')) &&
                ($scope.creditcolendingdtl_list == undefined) || ($scope.creditcolendingdtl_list == '') || ($scope.creditcolendingdtl_list == undefined)) {
                Notify.alert('Add Colending Details', 'warning');
            }
            else {

                 var params = {
                    application_gid: application_gid,
                    credit_gid: institution_gid,
                    colendingaapplicability_stats: $scope.rdbcolendingapplicability
                 }
                 var url = 'api/MstAppCreditUnderWriting/PostColendingApplicabilityStatusAdd';
                  lockUI();
                 SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
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
                    activate();
                 });
            }
        }

    }
})();
