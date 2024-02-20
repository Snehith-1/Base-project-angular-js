(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCADGroupGuaranteeDtlAddController', MstCADGroupGuaranteeDtlAddController);

    MstCADGroupGuaranteeDtlAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService'];

    function MstCADGroupGuaranteeDtlAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCADGroupGuaranteeDtlAddController';
        $scope.group_gid = $location.search().group_gid;
        var group_gid = $scope.group_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        lockUI();
        activate();

        function activate() {

            var url = 'api/MstCADApplication/GetGuaranteeProgramType';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.guaranteedtltype_list = resp.data.guaranteedtltype_list;
            });

            var params = {
                credit_gid: group_gid
            }
            var url = 'api/MstCADApplication/GetInstitutionGuaranteeDtl';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.creditguaranteedtl_list = resp.data.creditguaranteedtl_list;
            });

            var params = {
                credit_gid: group_gid,
                applicant_type: 'Group'
            }

            var url = 'api/MstCADCreditAction/GetCreditOperationsView'
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtgroup_name = resp.data.group_name;
                $scope.txtgroup_type = resp.data.group_type;
            });

            var url = 'api/MstCADApplication/GuaranteeDocTmpClear';
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


                }
                frm.append('cadcreditguaranteedtl_gid', '');
                frm.append('document_title', $scope.txtdocument_title);
                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    lockUI();
                    var url = 'api/MstCADApplication/GuaranteeDocumentUpload';
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

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.uploaddocumentcancel = function (cadcreditguaranteedtldocument_gid) {
            lockUI();
            var params = {
                cadcreditguaranteedtldocument_gid: cadcreditguaranteedtldocument_gid,
                credit_gid: group_gid
            }
            var url = 'api/MstCADApplication/DeleteGuaranteeDtlDocument';
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

        $scope.guarantee_downloadall = function () {
            for (var i = 0; i < $scope.creditguaranteedocument_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.creditguaranteedocument_list[i].document_path, $scope.creditguaranteedocument_list[i].document_name);
            }
        }

        $scope.guarantee_delete = function (cadcreditguaranteedtl_gid) {
            var params = {
                cadcreditguaranteedtl_gid: cadcreditguaranteedtl_gid,
                credit_gid: group_gid
            }
            var url = 'api/MstCADApplication/DeleteGuaranteeDtl';
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

        $scope.guarantee_remarks = function (cadcreditguaranteedtl_gid) {
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
                    cadcreditguaranteedtl_gid: cadcreditguaranteedtl_gid
                }
                var url = 'api/MstCADApplication/GetGuaranteeRemarksView';
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

        $scope.guarantee_docview = function (cadcreditguaranteedtl_gid) {
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
                    cadcreditguaranteedtl_gid: cadcreditguaranteedtl_gid
                }
                var url = 'api/MstCADApplication/GetGuaranteeDocDtl';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.GuaranteeDocumentView_List = resp.data.GuaranteeDocumentView_List;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.downloadallguarantee_doc = function () {
                    for (var i = 0; i < $scope.GuaranteeDocumentView_List.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.GuaranteeDocumentView_List[i].document_path, $scope.GuaranteeDocumentView_List[i].document_name);
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
                    credit_gid: group_gid,
                    applicant_type: 'Group',
                    guarantee_applicability: $scope.rdbGuaranteeApplicability,
                    guaranteprogram_gid: lsguaranteprogram_gid,
                    guaranteeprogram_name: lsguaranteeprogram_name,
                    remarks: $scope.txtremarks
                 }
                 var url = 'api/MstCADApplication/PostGuaranteeDtlAdd';
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
            if (lspage == "StartCreditUnderwriting") {
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

        $scope.creditbankacctdtl_edit = function (creditbankdtl_gid) {
            $location.url('app/MstCADCreditGroupBankAcctEdit?application_gid=' + application_gid + '&group_gid=' + group_gid + '&creditbankdtl_gid=' + creditbankdtl_gid + '&lspage=' + lspage);
        }
        $scope.company_bankstatement = function () {
            $location.url('app/MstCADCreditGroupBankStatementAnalysisAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }

    }
})();
