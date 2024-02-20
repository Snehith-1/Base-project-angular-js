(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstColendingCCApprovedVerificationController', MstColendingCCApprovedVerificationController);

    MstColendingCCApprovedVerificationController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$sce', '$anchorScroll', 'DownloaddocumentService', 'cmnfunctionService'];

    function MstColendingCCApprovedVerificationController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $sce, $anchorScroll, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstColendingCCApprovedVerificationController';
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;

        const lspagename = "MstColendingCCApprovedVerification";
        const lspagetype = "CAD_Pending";

        activate();
        lockUI();
        function activate() {

            var params = {
                application_gid: application_gid
            }

            var url = 'api/MstAppCreditUnderWriting/GetVerifyColendingDtlSummary';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.creditcolendingdtl_list = resp.data.creditcolendingdtl_list;
            });

            var params = {
                application_gid: application_gid
            }
            var url = 'api/MstAppCreditUnderWriting/GetColendingVerificationSummary';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.creditcolendingverification_list = resp.data.creditcolendingverification_list;
            });

            var params = {
                application_gid: application_gid
            }
            var url = 'api/MstAppCreditUnderWriting/GetCCColendingVerificationSummary';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.cccolendingverification_list = resp.data.cccolendingverification_list;
            });

            var params = {
                application_gid: $scope.application_gid
            }

            var url = 'api/MstApplicationView/GetApplicationBasicView';

            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtapplication_no = resp.data.application_no;
                $scope.txtbasiccustomer_name = resp.data.customer_name;
                $scope.txtcustomer_urn = resp.data.customer_urn;
                unlockUI();
            });

            var url = 'api/MstAppCreditUnderWriting/CreditVerificationDocTmpClear';
            SocketService.get(url).then(function (resp) {
                unlockUI();
            });

            var params = {
                application_gid: application_gid
            }
            var url = 'api/MstAppCreditUnderWriting/GetCreditApplicabilityView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lblcolending_applicability = resp.data.colending_applicability;
                $scope.lblcreditverified_by = resp.data.creditverified_by;
                $scope.lblcreditverified_date = resp.data.creditverified_date;
                if ($scope.lblcolending_applicability == 'Yes') {
                    $scope.creditcolendingsummary = true;
                }
                else {
                    $scope.creditcolendingsummary = false;
                }
            });

            var params = {
                application_gid: application_gid
            }
            var url = 'api/MstAppCreditUnderWriting/GetMyApplCreditApplicabilityView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lblcolendingaapplicability_stats = resp.data.colendingaapplicability_stats;
                $scope.lblmyapplcreditadd_by = resp.data.myapplcreditadd_by;
                $scope.lblmyapplcreditadd_date = resp.data.myapplcreditadd_date;
                if ($scope.lblcolendingaapplicability_stats == 'Yes') {
                    $scope.myapplcreditcolendingsummary = true;
                }
                else {
                    $scope.myapplcreditcolendingsummary = false;
                }
            });

            var params = {
                application_gid: application_gid
            }

            var url = 'api/MstAppCreditUnderWriting/GetCcApprovedColendingHistory';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.ccverifiedlog_list = resp.data.ccverifiedlog_list;
            });

            var params = {
                application_gid: application_gid
            }
            var url = 'api/MstAppCreditUnderWriting/GetCcApprovedApplicabilityView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.rdbcccolendingapplicability = resp.data.colending_applicability;
                $scope.ccapprovedverified_team = resp.data.ccapprovedverified_team;
                $scope.cccapprovedverified_by = resp.data.cccapprovedverified_by;
                $scope.cccapprovedverified_date = resp.data.cccapprovedverified_date;
                if ($scope.rdbcccolendingapplicability == 'Yes') {
                    $scope.cccolendingsummary = true;
                }
                else {
                    $scope.cccolendingsummary = false;
                }
            });
        }

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.creditguaranteedocument_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.creditguaranteedocument_list[i].document_path, $scope.creditguaranteedocument_list[i].document_name);
            }
        }

        $scope.remarkslog_view = function (ccapprovedverificationlog_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/RemarksLogView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    ccapprovedverificationlog_gid: ccapprovedverificationlog_gid
                }
                var url = 'api/MstAppCreditUnderWriting/GetCcColendingRemarksView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtcccolendinglog_remarks = resp.data.cccolendinglog_remarks;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


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

        $scope.creditcompletedcolending_view = function (creditverification_gid, colendingprogram_gid, portfolio_gid) {
            var creditverification_gid = creditverification_gid;
            var colendingprogram_gid = colendingprogram_gid;
            var portfolio_gid = portfolio_gid;
            var application_gid = $location.search().application_gid;

            var modalInstance = $modal.open({
                templateUrl: '/CreditColendingVerification.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var url = 'api/MstAppCreditUnderWriting/CreditVerificationDocTmpClear';
                SocketService.get(url).then(function (resp) {
                    unlockUI();
                });

                var params = {
                    creditverification_gid: creditverification_gid,
                    application_gid: application_gid
                }
                var url = 'api/MstAppCreditUnderWriting/GetCreditVerificationDtlView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblcompleted_applicability = resp.data.applicability;
                    $scope.lblcompleted_remarks = resp.data.remarks;
                    $scope.creditverification_gid = resp.data.creditverification_gid;
                });

                var params = {
                    creditverification_gid: creditverification_gid,
                    application_gid: application_gid
                }
                var url = 'api/MstAppCreditUnderWriting/CreditVerificationDocView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.creditverificationdoc_list = resp.data.creditverificationdoc_list;
                });
                
                $scope.downloads = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
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
                $scope.downloadall_verificationdoc = function () {
                    for (var i = 0; i < $scope.creditverificationdoc_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.creditverificationdoc_list[i].document_path, $scope.creditverificationdoc_list[i].document_name);
                    }
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.creditcolending_view = function (creditcolendingdtl_gid, colendingprogram_gid, portfolio_gid) {
            var creditcolendingdtl_gid = creditcolendingdtl_gid;
            var colendingprogram_gid = colendingprogram_gid;
            var portfolio_gid = portfolio_gid;
            var application_gid = $location.search().application_gid;

            var modalInstance = $modal.open({
                templateUrl: '/CreditColendingDtlview.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    creditcolendingdtl_gid: creditcolendingdtl_gid,
                    application_gid: application_gid
                }
                var url = 'api/MstAppCreditUnderWriting/GetColendingDtlsView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblapplicability = resp.data.applicability;
                    $scope.lblcolender_remarks = resp.data.remarks;
                    $scope.creditcolendingdtl_gid = resp.data.creditcolendingdtl_gid;
                });

                var params = {
                    creditcolendingdtl_gid: creditcolendingdtl_gid,
                    application_gid: application_gid,
                    credit_gid: ''
                }
                var url = 'api/MstAppCreditUnderWriting/ColendingDtlDocumentView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.creditcolendingdocument_list = resp.data.creditcolendingdocument_list;
                });

                $scope.downloads = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
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

        $scope.ccapprovedcolending_add = function (ccapprovedverification_gid, colendingprogram_gid, portfolio_gid) {
            var ccapprovedverification_gid = ccapprovedverification_gid;
            var colendingprogram_gid = colendingprogram_gid;
            var portfolio_gid = portfolio_gid;
            var application_gid = $location.search().application_gid;

            var modalInstance = $modal.open({
                templateUrl: '/CCApprovedColendingVerification.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var url = 'api/MstAppCreditUnderWriting/CCVerificationDocTmpClear';
                SocketService.get(url).then(function (resp) {
                    unlockUI();
                });

                var params = {
                    application_gid: application_gid
                }
                var url = 'api/MstAppCreditUnderWriting/GetCcApprovedApplicabilityView';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblcccolendingapplicability = resp.data.colending_applicability;
                });

                var params = {
                    ccapprovedverification_gid: ccapprovedverification_gid,
                    application_gid: application_gid
                }
                var url = 'api/MstAppCreditUnderWriting/GetCCVerificationDtlView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.rdbccapplicability = resp.data.applicability;
                    $scope.txtccverification_remarks = resp.data.remarks;
                    $scope.ccapprovedverification_gid = resp.data.ccapprovedverification_gid;
                });

                var params = {
                    ccapprovedverification_gid: ccapprovedverification_gid,
                    application_gid: application_gid
                }
                var url = 'api/MstAppCreditUnderWriting/CCVerificationDocView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.ccapprovedverificationdoc_list = resp.data.ccapprovedverificationdoc_list;
                });

                $scope.submit_cccolendingdtl = function () {
                    if (($scope.rdbccapplicability == '') || ($scope.rdbccapplicability == undefined) || ($scope.rdbccapplicability == '') ||
                        ($scope.txtccverification_remarks == '') || ($scope.txtccverification_remarks == undefined) || ($scope.txtccverification_remarks == '')) {
                        alert('Enter All Mandatory Fields', 'warning');
                    }
                    else {

                        var params = {
                            application_gid: application_gid,
                            colendingprogram_gid: colendingprogram_gid,
                            portfolio_gid: portfolio_gid,
                            applicability: $scope.rdbccapplicability,
                            remarks: $scope.txtccverification_remarks,
                            ccapprovedverification_gid: ccapprovedverification_gid,
                            colending_applicability: $scope.lblcccolendingapplicability
                        }
                        var url = 'api/MstAppCreditUnderWriting/PostCCApprovedVerificationDtl';
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

                $scope.ccverificationdocumentUpload = function (val) {
                    if (($scope.txtdocument_title == null) || ($scope.txtdocument_title == '') || ($scope.txtdocument_title == undefined)) {
                        $("#ccverificationfile").val('');
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


                        }
                        frm.append('document_title', $scope.txtdocument_title);
                        frm.append('ccapprovedverification_gid', ccapprovedverification_gid);
                        frm.append('colendingprogram_gid', colendingprogram_gid);
                        frm.append('portfolio_gid', portfolio_gid);
                        frm.append('application_gid', application_gid);
                        frm.append('project_flag', "Default");
                        $scope.uploadfrm = frm;
                        if ($scope.uploadfrm != undefined) {
                            lockUI();
                            var url = 'api/MstAppCreditUnderWriting/CCVerificationDocUpload';
                            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                                $scope.ccapprovedverificationdoc_list = resp.data.ccapprovedverificationdoc_list;
                                unlockUI();
                                $("#ccverificationfile").val('');
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

                $scope.uploaddocumentcancel = function (ccapprovedverificationdoc_gid) {
                    lockUI();
                    var params = {
                        ccapprovedverificationdoc_gid: ccapprovedverificationdoc_gid,
                        application_gid: application_gid
                    }
                    var url = 'api/MstAppCreditUnderWriting/DeleteCCVerificationDoc';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $scope.ccapprovedverificationdoc_list = resp.data.ccapprovedverificationdoc_list;
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

                $scope.downloadall_verificationdoc = function () {
                    for (var i = 0; i < $scope.ccapprovedverificationdoc_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.ccapprovedverificationdoc_list[i].document_path, $scope.ccapprovedverificationdoc_list[i].document_name);
                    }
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.CreditColendingRule = function (colendingprogram_gid) {
            $location.url('app/MstCreditColendingRuleView?lscolendingprogram_gid=' + colendingprogram_gid + '&application_gid=' + application_gid + '&lspage=' + lspage + '&lspagename=' + lspagename + '&lspagetype=' + lspagetype + '&lscolending_applicanttype=Company_Individual'); 
        }

        $scope.Colending_Applicability = function (rdbcccolendingapplicability) {
            var rdbcccolendingapplicability = rdbcccolendingapplicability;
            if (rdbcccolendingapplicability == 'Yes') {
                $scope.cccolendingsummary = true;
            }
            else {
                $scope.cccolendingsummary = false;
            }
        } 

        $scope.confirm_ccverification = function () {

            var params = {
                application_gid: application_gid,
                ccapprovedverified_team: lspage,
                colending_applicability: $scope.rdbcccolendingapplicability
            }
            var url = 'api/MstAppCreditUnderWriting/PostConfirmCCVerification';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    if (lspage=='CAD') {
                        $location.url('app/MstPendingCADReview');
                    }
                    else if(lspage=='ColendingCCApproved') {
                        $location.url('app/MstColendingCCApprovedVerifySummary');
                    }
                    else {
                        $location.url('app/MstColendingCCApprovedVerifySummary');
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
        $scope.Back = function () {
            if (lspage=='CAD' || lspage=='CAD_Pending' || lspage=='PendingCADReview') {
                $location.url('app/MstPendingCADReview');
            }
            else if(lspage=='ColendingCCApproved') {
                $location.url('app/MstColendingCCApprovedVerifySummary');
            }
            else {
                // $location.url('app/MstColendingCCApprovedVerifySummary');
            }
        }
    }
})();