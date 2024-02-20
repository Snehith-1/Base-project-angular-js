(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstRMDeferralCloseQueryController', MstRMDeferralCloseQueryController);

    MstRMDeferralCloseQueryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'cmnfunctionService', 'DownloaddocumentService'];

    function MstRMDeferralCloseQueryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstRMDeferralCloseQueryController';
        var lspage = $location.search().lspage;
        var application_gid = $location.search().application_gid;
        var credit_gid = $location.search().credit_gid;
        var lstype = $location.search().lstype;
        var lsdocumentcheckdtl_gid = $location.search().lsdocumentcheckdtl_gid;
        var lsdeferraltag = $location.search().lsdeferraltag;
        var lscovenant_type = $location.search().lscovenant_type;
        $scope.lscovenant_type = lscovenant_type;
        var lspagetype = $location.search().lspagetype;
        $scope.lspagetype = $location.search().lspagetype;

        activate();
        lockUI();
        function activate() {
            var param = {
                application_gid: application_gid
            };

            var url = 'api/MstApplicationApproval/Getapplicationdetails';
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.lblapplref_no = resp.data.application_no;
                $scope.lblapplicant_name = resp.data.customer_name;
                $scope.lblcurrentappl_stage = resp.data.approval_status;
                $scope.lblappl_state = resp.data.region;
                $scope.lbloveralllimit_request = resp.data.overalllimit_amount;
                $scope.productlist = resp.data.productlist;
                $scope.lbloveralllimit_request = (parseInt($scope.lbloveralllimit_request.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountwords = defaultamountwordschange($scope.lbloveralllimit_request);
            });

            var param = {
                documentcheckdtl_gid: lsdocumentcheckdtl_gid
            };
            var url = 'api/MstScannedDocument/GetAppcadQuerySummaryRm';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.query_list = resp.data.mdlcadquery;
                if( resp.data.mdlcadquery!="") {
                    $scope.query_list =  resp.data.mdlcadquery.filter(function (el) { return el.query_status != "Pending" });
                }
                $scope.lbldocumentcode = resp.data.documenttype_code;
                $scope.lbldocumenttype_name = resp.data.documenttype_name;
            }); 
            var url = 'api/MstPhysicalDocument/GetPhysicalAppcadQuerySummaryRm';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.physicalquery_list = resp.data.mdlcadquery; 
                if( resp.data.mdlcadquery!="") {
                    $scope.physicalquery_list =  resp.data.mdlcadquery.filter(function (el) { return el.query_status != "Pending" });
                }
            }); 
            var params = {
                documentcheckdtl_gid: lsdocumentcheckdtl_gid,
                signeddocument_flag: 'Y'
            }
            var url = 'api/MstScannedDocument/GetScannedDocument';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.softcopyuploaddocumentlist = resp.data.scanneduploaddocument;
            });
            var params = {
                documentcheckdtl_gid: lsdocumentcheckdtl_gid
            } 
            var url = 'api/MstPhysicalDocument/GetPhysicalDocument';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.originaluploaddocumentlist = resp.data.scanneduploaddocument;
            }); 
            var url = 'api/MstScannedDocument/GettaggedDeferralinfo';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.errormsg = "";
                $scope.showerrordiv = false;
                $scope.adddeferraltable = false;
                $scope.editdeferraltable = true;
                $scope.showdeferraltag = false;
                if (resp.data.tracking_id != null)
                    $scope.showdeferraltag = true;

                $scope.documentseverity_gid = resp.data.documentseverity_gid;
                $scope.txtdocumentseverity_name = resp.data.documentseverity_name;
                $scope.tracking_id = resp.data.tracking_id;
                $scope.cbotaggedto = resp.data.tagged_to;
                $scope.txtdue_date = resp.data.Duedate; 
                if ($scope.txtdue_date != "") {
                    $scope.txtdue_date = new Date($scope.txtdue_date);
                }
                $scope.txtcad_remarks = resp.data.cad_remarks;
                $scope.deferraltaggedchecklist = resp.data.deferraltaggedchecklist;
                angular.forEach($scope.deferraltaggedchecklist, function (value, key) {
                    if (value.documentverified == true) {
                        value.documentverified = "true";
                        value.deferraltagged = false;
                    }
                    else if (value.deferraltagged == true) {
                        value.documentverified = false;
                        value.deferraltagged = "true";
                    }
                });
            }); 
            var url = 'api/MstPhysicalDocument/GettaggedDeferralinfo';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI(); 
                $scope.physicaldeferraltaggedchecklist = resp.data.deferraltaggedchecklist;
                angular.forEach($scope.physicaldeferraltaggedchecklist, function (value, key) {
                    if (value.documentverified == true) {
                        value.documentverified = "true";
                        value.deferraltagged = false;
                    }
                    else if (value.deferraltagged == true) {
                        value.documentverified = false;
                        value.deferraltagged = "true";
                    }
                });
            });
        }

        $scope.query_close = function (tagquery_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/queryClose.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {
                    var params = {
                        tagquery_gid: tagquery_gid, 
                        query_responseremarks: $scope.txtcloseremarks,
                        documentcheckdtl_gid: lsdocumentcheckdtl_gid
                    }
                    var url = 'api/MstScannedDocument/PostAppcadresponsequery';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
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
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        } 
                    }); 
                    $modalInstance.close('closed');
                }

            }
        }

        $scope.softcopyquery_close = function (tagquery_gid, lscovenant_type, groupdocumentchecklist_gid, document_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/softcopyqueryClose.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                lockUI();
                var param = {
                    tagquery_gid: tagquery_gid
                }; 
                var url = 'api/MstScannedDocument/tmpcleartagquerydocument';
                SocketService.getparams(url, param).then(function (resp) {
                    unlockUI();
                });


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {
                    var params = {
                        tagquery_gid: tagquery_gid,
                        query_responseremarks: $scope.txtcloseremarks,
                        documentcheckdtl_gid: lsdocumentcheckdtl_gid,
                        queryclosed_status: $scope.rdbclosequerystatus,
                        document_gid: document_gid
                    }
                    var url = 'api/MstScannedDocument/PostAppcadresponsequery';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
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
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });
                    $modalInstance.close('closed');
                }

                $scope.closequeryupdation = function (rdbclosequerystatus) {
                    if (rdbclosequerystatus == "3") {
                        $modalInstance.close('closed'); 
                        $location.url('app/MstCadDeferralStatus?application_gid=' + application_gid + '&credit_gid=' + credit_gid + '&lspage=' + lspage + '&lstype=' + lstype + '&lsdocumentcheckdtl_gid=' + lsdocumentcheckdtl_gid + '&lsdeferraltag=' + lsdeferraltag + '&lstagquery_gid=' + tagquery_gid +'&lscompleted=""&lsscanned=Y');
                    }
                }

                $scope.scannedDocumentUpload = function (val, val1, name) {
                    lockUI();
                    var frm = new FormData();

                    for (var i = 0; i < val.length; i++) {
                        var item = {
                            name: val[i].name,
                            file: val[i]
                        };
                        frm.append('fileupload', item.file);
                        frm.append('file_name', item.name);
                    }

                    frm.append('document_title', $scope.txtdocument_name);
                    frm.append('tagquery_gid', tagquery_gid);
                    frm.append('documentcheckdtl_gid', groupdocumentchecklist_gid);
                    frm.append('application_gid', application_gid);
                    frm.append('credit_gid', credit_gid);
                    frm.append('RMupload', 'Y');
                    frm.append('covenant_type', lscovenant_type);
                    frm.append('signeddocument_flag', 'Y');
                    frm.append('project_flag', "Default");
                    $scope.uploadfrm = frm;
                    if ($scope.uploadfrm != undefined) {
                        var url = 'api/MstScannedDocument/ScannedDocumentUpload';
                        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                            unlockUI();
                            $("#scannedmultiplefile").val('');
                            $scope.txtdocument_name = "";
                            var params = {
                                documentcheckdtl_gid: tagquery_gid,
                                signeddocument_flag: 'Y'
                            }
                            var url = 'api/MstScannedDocument/GetScannedDocument';
                            lockUI();
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.scanneduploaddocumentlist = resp.data.scanneduploaddocument;
                            });

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
                            unlockUI();
                        });
                    }
                    else {
                        alert('Please select a file.')

                    }
                }

                $scope.downloads = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }

                $scope.downloadall = function () {
                    for (var i = 0; i < $scope.scanneduploaddocumentlist.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.scanneduploaddocumentlist[i].file_path, $scope.scanneduploaddocumentlist[i].file_name);
                    }
                }

                $scope.defdoc_delete = function (scanneddocument_gid) {
                    lockUI();
                    var params = {
                        scanneddocument_gid: scanneddocument_gid
                    }
                    var url = 'api/MstScannedDocument/cancelscanneduploaddocument';
                    SocketService.getparams(url, params).then(function (resp) {
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
                        var params = {
                            documentcheckdtl_gid: tagquery_gid,
                            signeddocument_flag: 'Y'
                        }
                        var url = 'api/MstScannedDocument/GetScannedDocument';
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            $scope.scanneduploaddocumentlist = resp.data.scanneduploaddocument;

                        });
                    });
                }
            }
        }
         
        $scope.view_querydesc = function (query_description, query_responseremarks) {
            var modalInstance = $modal.open({
                templateUrl: '/queryDescriptionView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.lblquery_desc = query_description;
                $scope.lblquery_responseremarks = query_responseremarks;
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.defferaldoc_view = function (tagquery_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/document_view.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    tagquery_gid: tagquery_gid
                }
                var url = 'api/MstScannedDocument/GetQueryDocument';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.queryuploaddocument = resp.data.queryuploaddocument;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.download_doc1 = function (val1, val2) {
                    var phyPath = val1;
                    var relPath = phyPath.split("StoryboardAPI");
                    var relpath1 = relPath[1].replace("\\", "/");
                    var hosts = window.location.host;
                    var prefix = location.protocol + "//";
                    var str = prefix.concat(hosts, relpath1);
                    var link = document.createElement("a");
                    link.download = val2;
                    var uri = str;
                    link.href = uri;
                    link.click();
                }
            }

        }

        $scope.Back = function () {
            $location.url('app/MstRMDeferralDtlsView?application_gid=' + application_gid + '&credit_gid=' + credit_gid + '&lspage=' + lspage + '&lstype=' + lstype);
        }

        function defaultamountwordschange(input) {
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords = cmnfunctionService.fnConvertNumbertoWord(str);
            return lswords;
        }

        $scope.originalcopyquery_close = function (tagquery_gid, lscovenant_type, groupdocumentchecklist_gid, document_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/originalcopyqueryClose.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                lockUI();
                var param = {
                    tagquery_gid: tagquery_gid
                }; 
                var url = 'api/MstPhysicalDocument/tmpcleartagquerydocument';
                SocketService.getparams(url, param).then(function (resp) {
                    unlockUI();
                });


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {
                    var params = {
                        tagquery_gid: tagquery_gid,
                        query_responseremarks: $scope.txtcloseremarks,
                        documentcheckdtl_gid: lsdocumentcheckdtl_gid,
                        queryclosed_status: $scope.rdbclosequerystatus,
                        document_gid: document_gid
                    }
                    var url = 'api/MstPhysicalDocument/PostAppcadresponsequery';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
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
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });
                    $modalInstance.close('closed');
                }

                $scope.closequeryupdation = function (rdbclosequerystatus) {
                    if (rdbclosequerystatus == "3") {
                        $modalInstance.close('closed'); 
                        $location.url('app/MstCadDeferralStatus?application_gid=' + application_gid + '&credit_gid=' + credit_gid + '&lspage=' + lspage + '&lstype=' + lstype + '&lsdocumentcheckdtl_gid=' + lsdocumentcheckdtl_gid + '&lsdeferraltag=' + lsdeferraltag + '&lstagquery_gid=' + tagquery_gid +'&lscompleted=""&lsscanned=N');
                    }
                }
 
            }
        }

        $scope.download_doc = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
    }
})();
