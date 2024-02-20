(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstAppContractLetterWordView', AgrMstAppContractLetterWordView);

    AgrMstAppContractLetterWordView.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$sce', '$anchorScroll', 'DownloaddocumentService','cmnfunctionService'];

    function AgrMstAppContractLetterWordView($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $sce, $anchorScroll, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstAppContractLetterWordView';
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.application2sanction_gid = $location.search().application2sanction_gid;
        var application2sanction_gid = $location.search().application2sanction_gid;
        $scope.employee_gid = $location.search().employee_gid;
        var employee_gid = $scope.employee_gid;
        $scope.sanction_gid = $location.search().sanction_gid;
        var sanction_gid = $location.search().sanction_gid;
        $scope.sanctionapprovallog_gid = $location.search().sanctionapprovallog_gid;
        var sanctionapprovallog_gid = $location.search().sanctionapprovallog_gid;

        activate();

        function activate() {


            if ($scope.lspage == 'checkersummary' || $scope.lspage == 'checkerapprovalsummary') {
                $scope.sanupload = 'Y';
            }
            else {
                $scope.sanupload = 'N';
            }

         /*   var url = 'api/MstCadFlow/CADSanctionDtls';*/
            var params = {
                sanction_gid: sanction_gid
            };
            //lockUI();
            //SocketService.getparams(url, params).then(function (resp) {
            //    unlockUI();
            //    $scope.sanction_refno = resp.data.sanction_refno;
            //    $scope.SanctionDateEdit = resp.data.sanctionDate;
            //    $scope.sanction_date = resp.data.sanction_date;
            //    $scope.sanction_amount = resp.data.sanction_amount;
            //    $scope.sanctionto_name = resp.data.sanctionto_name;
            //    $scope.customer_urn = resp.data.customer_urn;
            //});
            var url = 'api/AgrTrnContract/GetTemplateDetails';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.lspath1 = resp.data.makerfile_path;
                    $scope.lsname1 = resp.data.makerfile_name;
                });


            var params = {
                application2sanction_gid: sanction_gid
            };
            var url = 'api/AgrTrnContract/GetContractDtl';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtcontract_id = resp.data.contract_id;
                $scope.txtvalidityfrom_date = resp.data.validityfrom_date;
                $scope.txtvalidityto_date = resp.data.validityto_date;
                $scope.txtcontract_date = resp.data.contract_date;
            });
            var params = {
                sanction_gid: sanction_gid
            };
            $scope.checkerfollowup = false;
            if (lspage == "checkerfollowupsummary")
                $scope.checkerfollowup = true;

            $scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };

            if (sanctionapprovallog_gid == '' || sanctionapprovallog_gid == undefined || sanctionapprovallog_gid == null) {
                $scope.templatelogdetails = false;
                $scope.templatedetails = true;
                var params = {
                    sanction_gid: sanction_gid
                };
                lockUI();
                var url = 'api/AgrTrnContract/GetTemplateDetails';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.lspath = resp.data.makerfile_path;
                    $scope.lsname = resp.data.makerfile_name;
                    $scope.content = resp.data.template_content;
                    $scope.checkerletter_flag = resp.data.checkerletter_flag;
                    $scope.checkerapproval_flag = resp.data.checkerapproval_flag;
                    $scope.sanctionletter_flag = resp.data.sanctionletter_flag;
                    $scope.sanctionletter_status = resp.data.sanctionletter_status;
                    $scope.digitalsignature_flag = resp.data.digitalsignature_flag;
                    $scope.checkerupdated_by = resp.data.checkerupdated_by;
                    $scope.checkerupdated_on = resp.data.checkerupdated_on;
                    $scope.makersubmitted_by = resp.data.makersubmitted_by;
                    $scope.makersubmitted_on = resp.data.makersubmitted_on;
                    $scope.approved_by = resp.data.approved_by;
                    $scope.approved_date = resp.data.approved_date;
                    unlockUI();
                    console.log('flag', $scope.digitalsignature_flag);
                    console.log('lspage', lspage);
                    if ($scope.digitalsignature_flag != "Y" && lspage != "RMSanctionSummary") {
                        $scope.download_show = true;
                    }
                    else if ($scope.digitalsignature_flag != "Y" && lspage == "RMSanctionSummary") {
                        $scope.download_show = false;
                    }
                    else {

                    }
                });
            }
            else {
                $scope.templatelogdetails = true;
                $scope.templatedetails = false;
                var params = {
                    sanctionapprovallog_gid: sanctionapprovallog_gid,
                    sanction_gid: sanction_gid
                };
                lockUI();
                var url = 'api/AgrTrnContract/GetTemplateLogDetails';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.content = resp.data.template_content;
                    $scope.sanction_status = resp.data.sanction_status;
                    $scope.makersubmitted_by = resp.data.makersubmitted_by;
                    $scope.makersubmitted_on = resp.data.makersubmitted_on;
                    unlockUI();
                });
            }
            var params = {
                sanctionapprovallog_gid: sanctionapprovallog_gid,
                application2sanction_gid: application2sanction_gid
            };
            var url = 'api/AgrTrnContract/CADSanctionLetterSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.sanctiontocheckerlist = resp.data.sanctiondetails;
            });
            //Contract EDit List
            var param = {
                application2sanction_gid: sanction_gid
            };
            var url = 'api/AgrTrnContract/ContractEditDocList';
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.upload_list = resp.data.Contractupload_list;
            });
         
        }

        $scope.downloaddocument = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("EMS");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //var name = val2.split(".")
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        

        $scope.sanctionlogdocdownload = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
            //var params = {
            //    sanctionapprovallog_gid: sanctionapprovallog_gid,
            //    sanction_gid: sanction_gid
            //};
            //lockUI();
            //var url = 'api/MstCAD/SanctionLetterLogDownload';
            //SocketService.getparams(url, params).then(function (resp) {
            //    if (resp.data.status == true) {
            //        unlockUI();
                    //var phyPath = resp.data.lspath;
                    //var relPath = phyPath.split("EMS");
                    //var relpath1 = relPath[1].replace("\\", "/");
                    //var hosts = window.location.host;
                    //var prefix = location.protocol + "//";
                    //var str = prefix.concat(hosts, relpath1);
                    //var link = document.createElement("a");
                    //var name = resp.data.lsname.split(".")
                    //link.download = resp.data.lsname;
                    //var uri = str;
                    //link.href = uri;
                    //link.click();
               
                    //Notify.alert(resp.data.message, 'success')
            //    }
            //    else {
            //        unlockUI();
            //        Notify.alert(resp.data.message, 'warning')
            //        activate();
            //    }
            //});
        }

        $scope.Back = function () {
            if (lspage == 'checkersummary' || lspage == "checkerfollowupsummary") {
                $location.url('app/AgrMstContractDtlViewSummary?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == 'checkerapprovalsummary') {
                $location.url('app/AgrMstContractDtlViewSummary?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == 'RMSanctionSummary') {
                $location.url('app/AgrMstRMSanctionSummary?sanction_gid=' + sanction_gid + '&application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == 'ContractMakerFollowup') {
                $location.url('app/AgrMstContractDtlViewSummary?sanction_gid=' + sanction_gid + '&application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == 'ContractApprovalCompleted') {
                $location.url('app/AgrMstContractDtlViewSummary?sanction_gid=' + sanction_gid + '&application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == 'checkerfollowupsummary') {
                $location.url('app/AgrMstContractDtlViewSummary?sanction_gid=' + sanction_gid + '&application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == 'SanctionAcceptedCustomer') {
                $location.url('app/AgrMstContractDtlViewSummary?sanction_gid=' + sanction_gid + '&application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == 'SanctionApprovalCompleted') {
                $location.url('app/AgrMstAppSanctionLetterGeneration?sanction_gid=' + sanction_gid + '&employee_gid=' + employee_gid + '&application_gid=' + application_gid + '&lspage=SanctionApprovalCompleted' + '&lsresubmit=rewubmit_flag');
            }
            else if (lspage == 'ContractRejected'){
                $location.url('app/AgrMstContractDtlViewSummary?sanction_gid=' + sanction_gid + '&employee_gid=' + employee_gid + '&application_gid=' + application_gid + '&lspage=ContractRejected');
            }
            else {
                $location.url('app/AgrMstAppSanctionLetterGeneration?sanction_gid=' + sanction_gid + '&employee_gid=' + employee_gid + '&application_gid=' + application_gid + '&lspage=ContractApprovalCompleted');
            }
        }

        $scope.proceedtocheckerapproval = function () {
            var application_gid = $location.search().application_gid;
            lockUI();
            var param = {
                sanction_gid: sanction_gid,
                application_gid: application_gid
            };
            var url = 'api/AgrTrnContract/PostProceedToApproval';
            SocketService.post(url, param).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.AgrMstContractCheckerSummary');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                }
            });
        }

        // Pushback the sanction to maker 
        $scope.pushbacktomaker = function () {
            var modalInstance = $modal.open({
                templateUrl: '/pushbacksanctionpopup.html',
                controller: ModalInstanceCtrl,
                size: 'md',
                backdrop: 'static',
                keyboard: false,
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.PushbackSanctionSubmit = function () {
                    var param = {
                        sanction_gid: sanction_gid,
                        pushback_remarks: $scope.pushback_remarks
                    };
                    var url = 'api/AgrTrnContract/PusbackToMaker';
                    SocketService.post(url, param).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            $state.go('app.AgrMstContractCheckerSummary');
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                        }
                    });
                }

                $scope.close = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.checkerapprove = function () {
            lockUI();
            var param = {
                sanction_gid: sanction_gid,
                sanction_status: 'Approved'
            };
            var url = 'api/AgrTrnContract/UpdateCheckerApproval';
            SocketService.post(url, param).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.AgrMstContractCheckerSummary');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                }
            });
        }

        // Sanction Reject 
        $scope.checkerreject = function () {
            var modalInstance = $modal.open({
                templateUrl: '/rejectsanctionpopup.html',
                controller: ModalInstanceCtrl,
                size: 'md',
                backdrop: 'static',
                keyboard: false,
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.RejectSanctionSubmit = function () {
                    var param = {
                        sanction_gid: sanction_gid,
                        reject_remarks: $scope.reject_remarks,
                        sanction_status: 'Rejected'
                    };
                    var url = 'api/AgrTrnContract/UpdateCheckerApproval';
                    SocketService.post(url, param).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            $state.go('app.AgrMstContractCheckerSummary');
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                        }
                    });
                }

                $scope.close = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.insert_signature = function () {
            lockUI();
            var param = {
                sanction_gid: sanction_gid,
            };
            var url = 'api/AgrTrnContract/PostDigitalSignature';
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
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
                    unlockUI();
                }
            });
        }

        $scope.download_signature = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("EMS");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //var name = val2.split(".")
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
           DownloaddocumentService.Downloaddocument(val1, val2);
        }
        // Download Document
        $scope.downloads = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("EMS");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //var name = val2.split(".")
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.upload_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.upload_list[i].document_path, $scope.upload_list[i].document_name);
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
        $scope.uploaddocumentcancel = function (contractdocumentupload_gid) {
            lockUI();
            var params = {
                contractdocumentupload_gid: contractdocumentupload_gid
            }
            var url = 'api/AgrTrnContract/TmpDocumentDelete';
            SocketService.getparams(url, params).then(function (resp) {
                /*  $scope.upload_list = resp.data.upload_list;*/
                /*   $scope.upload_list = resp.data.Contractupload_list;*/
                if (resp.data.status == true) {
                    var param = {
                        application2sanction_gid: application2sanction_gid
                    };
                    var url = 'api/AgrTrnContract/ContractEditDocList';
                    SocketService.getparams(url, param).then(function (resp) {
                        unlockUI();
                        $scope.upload_list = resp.data.Contractupload_list;
                    });
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
    }
})();