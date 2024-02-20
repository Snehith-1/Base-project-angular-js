(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstColendingCreditVerificationController', MstColendingCreditVerificationController);

    MstColendingCreditVerificationController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$sce', '$anchorScroll', 'DownloaddocumentService', 'cmnfunctionService'];

    function MstColendingCreditVerificationController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $sce, $anchorScroll, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstColendingCreditVerificationController';
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;

        const lspagename = "MstColendingCreditVerification";
        const lspagetype = "CAD_Accepted";
       
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
        }

        $scope.CreditColendingRule = function (colendingprogram_gid) {
            $location.url('app/MstCreditColendingRuleView?lscolendingprogram_gid=' + colendingprogram_gid + '&application_gid=' + application_gid + '&lspage=' + lspage + '&lspagename=' + lspagename + '&lspagetype=' + lspagetype + '&lscolending_applicanttype=Company_Individual'); 
        }

        $scope.Colending_Applicability = function (rdbcolendingapplicability) {
            var rdbcolendingapplicability = rdbcolendingapplicability;
            if (rdbcolendingapplicability == 'Yes') {
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

                $scope.downloadallcolending_doc = function () {
                    for (var i = 0; i < $scope.ColendingDocumentView_List.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.ColendingDocumentView_List[i].document_path, $scope.ColendingDocumentView_List[i].document_name);
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
                $scope.download_colendingdoc = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }


            }

        }

        $scope.creditcolending_add = function (creditverification_gid, colendingprogram_gid, portfolio_gid) {
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
                    application_gid: application_gid
                }
                var url = 'api/MstAppCreditUnderWriting/GetMyApplCreditApplicabilityView';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblcolendingaapplicability_stats = resp.data.colendingaapplicability_stats;
                });

                var params = {
                    creditverification_gid: creditverification_gid,
                    application_gid: application_gid
                }
                var url = 'api/MstAppCreditUnderWriting/GetCreditVerificationDtlView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.rdbapplicability = resp.data.applicability;
                    $scope.txtverification_remarks = resp.data.remarks;
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

                $scope.submit_creditcolendingdtl = function () {
                    if (($scope.rdbapplicability == '') || ($scope.rdbapplicability == undefined) || ($scope.rdbapplicability == '') ||
                        ($scope.txtverification_remarks == '') || ($scope.txtverification_remarks == undefined) || ($scope.txtverification_remarks == '')) {
                        alert('Enter All Mandatory Fields', 'warning');
                    }
                    else {

                        var params = {
                            application_gid: application_gid,
                            colendingprogram_gid: colendingprogram_gid,
                            portfolio_gid: portfolio_gid,
                            applicability: $scope.rdbapplicability,
                            remarks: $scope.txtverification_remarks,
                            creditverification_gid: creditverification_gid,
                            colending_applicability: $scope.lblcolendingaapplicability_stats
                        }
                        var url = 'api/MstAppCreditUnderWriting/PostCreditVerificationDtl';
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

                $scope.creditverificationdocumentUpload = function (val) {
                    if (($scope.txtdocument_title == null) || ($scope.txtdocument_title == '') || ($scope.txtdocument_title == undefined)) {
                        $("#Creditverificationfile").val('');
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
                        frm.append('creditverification_gid', creditverification_gid);
                        frm.append('colendingprogram_gid', colendingprogram_gid);
                        frm.append('portfolio_gid', portfolio_gid);
                        frm.append('application_gid', application_gid);
                        frm.append('project_flag', "Default");
                        $scope.uploadfrm = frm;
                        if ($scope.uploadfrm != undefined) {
                            lockUI();
                            var url = 'api/MstAppCreditUnderWriting/CreditVerificationDocUpload';
                            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                                $scope.creditverificationdoc_list = resp.data.creditverificationdoc_list;
                                unlockUI();
                                $("#Creditverificationfile").val('');
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

                $scope.uploaddocumentcancel = function (creditverificationdoc_gid) {
                    lockUI();
                    var params = {
                        creditverificationdoc_gid: creditverificationdoc_gid,
                        application_gid: application_gid
                    }
                    var url = 'api/MstAppCreditUnderWriting/DeleteCreditVerificationDoc';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $scope.CreditVerificationDocUpload = resp.data.CreditVerificationDocUpload;
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

        $scope.confirm_creditverification = function () {          

                var params = {
                    application_gid: application_gid,
                    colending_applicability: $scope.rdbcolendingapplicability
                }
            var url = 'api/MstAppCreditUnderWriting/PostConfirmCreditVerification';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $location.url('app/MstColendingVerificationSummary');     
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
            $location.url('app/MstColendingVerificationSummary');           
        }
    }
})();
