(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasMstSanctionLetterWordView', idasMstSanctionLetterWordView);

    idasMstSanctionLetterWordView.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$sce', '$anchorScroll', 'DownloaddocumentService'];

    function idasMstSanctionLetterWordView($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $sce, $anchorScroll, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'idasMstSanctionLetterWordView';
        var sanction_gid = $location.search().sanction_gid;
        var sanctionapprovallog_gid = $location.search().sanctionapprovallog_gid;
        var lspage = $location.search().lspage;

        activate();

        function activate() {
            var url = window.location.href;
            var relPath = url.split("lstab=");
            $scope.relpath1 = relPath[1];
            if ($scope.relpath1 == 'completed') {
                $scope.approvalicon = false;
            }
            else {
                $scope.approvalicon = true;
            }

            var url = 'api/IdasMstSanction/SanctionDtlsEdit';
            var params = {
                sanction_gid: sanction_gid
            };
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.sanction_refno = resp.data.sanction_refno;
                $scope.SanctionDateEdit = resp.data.sanctionDate;
                $scope.sanction_date = resp.data.sanction_date;
                $scope.sanction_amount = resp.data.sanction_amount;
                $scope.customername = resp.data.customername;
                $scope.customer_urn = resp.data.customer_urn;
            });

            var url = 'api/IdasMstSanction/SanctionDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.uploaddocument = resp.data.UploadDocumentList;
            });

            $scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };

            if (sanctionapprovallog_gid == '' || sanctionapprovallog_gid == undefined || sanctionapprovallog_gid == null) {
                $scope.templatelogdetails = false;
                $scope.templatedetails = true;
                var params = {
                    sanction_gid: sanction_gid
                };
                lockUI();
                var url = 'api/IdasMstSanction/GetTemplateDetails';
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
                    unlockUI();
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
                var url = 'api/IdasMstSanction/GetTemplateLogDetails';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.content = resp.data.template_content;
                    $scope.sanction_status = resp.data.sanction_status;
                    $scope.makersubmitted_by = resp.data.makersubmitted_by;
                    $scope.makersubmitted_on = resp.data.makersubmitted_on;
                    unlockUI();
                });
            }
        }

        $scope.downloaddocument = function (val1, val2) {
           
                DownloaddocumentService.Downloaddocument(val1, val2);
            
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
        }

        $scope.sanctionlogdocdownload = function () {
            var params = {
                sanctionapprovallog_gid: sanctionapprovallog_gid,
                sanction_gid: sanction_gid
            };
            lockUI();
            var url = 'api/IdasMstSanction/SanctionLetterLogDownload';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    var phyPath = resp.data.lspath;
                var filename1 = resp.data;
                var phyPath = phyPath.replace("\\", "/");
                var phyPath = phyPath.replace("//", "/");
                var relPath = phyPath.split("EMS/");
                var relpath1 = relPath[1].replace("\\", "/");
                var url1 = filename1;
                var filename = url1.substring(url1.lastIndexOf('/')+1);                                                                      
               var url = 'api/azurestorage/FileUploadDocument';
                var params = {
                    file_path : relpath1
                }
                SocketService.post(url,params).then(function (resp) {
                if (resp.data.status == true) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export PDF !', 'warning');
                }
                });
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
                    DownloaddocumentService.Downloaddocument(val1, val2);
                    Notify.alert(resp.data.message, 'success')
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, 'warning')
                    activate();
                }
            });
        }

        $scope.Back = function (relpath1) {
            if (lspage == 'checkersummary') {
                $state.go('app.MstCheckerSummary');
            }
            else if (lspage == 'checkerapprovalsummary') {
                $state.go('app.MstCheckerApprovalSummary');
            }
            else {
                $location.url('app/idasMstSanctionLetterGeneration?sanction_gid=' + sanction_gid + '&lstab=' + relpath1);
            }
        }

        $scope.proceedtocheckerapproval = function () {
            lockUI();
            var param = {
                sanction_gid: sanction_gid,
            };
            var url = 'api/IdasMstSanction/PostProceedToApproval';
            SocketService.post(url, param).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.MstCheckerSummary');
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
                    var url = 'api/IdasMstSanction/PusbackToMaker';
                    SocketService.post(url, param).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            $state.go('app.MstCheckerSummary');
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
            var url = 'api/IdasMstSanction/UpdateCheckerApproval';
            SocketService.post(url, param).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.MstCheckerApprovalSummary');
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
                    var url = 'api/IdasMstSanction/UpdateCheckerApproval';
                    SocketService.post(url, param).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            $state.go('app.MstCheckerApprovalSummary');
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
            var url = 'api/IdasMstSanction/PostDigitalSignature';
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
            DownloaddocumentService.Downloaddocument(val1, val2);

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
        }
        // Download Document
        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);

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
        }
    }
})();
