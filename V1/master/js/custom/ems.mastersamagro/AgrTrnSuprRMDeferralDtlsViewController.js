(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnSuprRMDeferralDtlsViewController', AgrTrnSuprRMDeferralDtlsViewController);

    AgrTrnSuprRMDeferralDtlsViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', '$anchorScroll', 'DownloaddocumentService','cmnfunctionService'];

    function AgrTrnSuprRMDeferralDtlsViewController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, $anchorScroll, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnSuprRMDeferralDtlsViewController';

        var lspage = $location.search().lspage;
        var application_gid = $location.search().application_gid;
        var credit_gid = $location.search().credit_gid;
        var lstype = $location.search().lstype;

        activate();

        function activate() {
            lockUI();
            var params = {
                credit_gid: credit_gid,
                application_gid: application_gid
            }

            var url = 'api/AgrMstSuprScannedDocument/GetCADTrnScannedDocList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtapplication_no = resp.data.application_no;
                $scope.txtbasiccustomer_name = resp.data.customer_name;
                $scope.ScannedDocumentlist = resp.data.ScannnedDocTaggedDocument;
                $scope.ScannnedCovenantDocumentlist = resp.data.ScannnedCovenantDocTaggedDocument;
            });
            var params = { 
                application_gid: application_gid
            }

            var url = 'api/AgrMstSuprScannedDocument/GetCompletedDocumentInfo';
            SocketService.getparams(url, params).then(function (resp) { 
                $scope.PhysicalCompleted = resp.data.PhysicalCompleted;
                $scope.ScannedCompleted = resp.data.ScannedCompleted; 
            }); 
        }

        $scope.Back = function () {
            $location.url('app/AgrTrnSuprRMDeferralDtls?application_gid=' + application_gid);
        }

        $scope.scanneddocumentssummary = function () {
            var params = {
                credit_gid: credit_gid,
                application_gid: application_gid
            }

            var url = 'api/AgrMstSuprScannedDocument/GetCADTrnScannedDocList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtapplication_no = resp.data.application_no;
                $scope.txtbasiccustomer_name = resp.data.customer_name;
                $scope.ScannedDocumentlist = resp.data.ScannnedDocTaggedDocument;
                $scope.ScannnedCovenantDocumentlist = resp.data.ScannnedCovenantDocTaggedDocument;
            });
        }

        $scope.physicaldocumentsSummary = function () {
            var params = {
                credit_gid: credit_gid,
                application_gid: application_gid
            }

            var url = 'api/AgrTrnSuprPhysicalDocument/GetCADTrnPhysicalDocList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtapplication_no = resp.data.application_no;
                $scope.txtbasiccustomer_name = resp.data.customer_name;
                $scope.PhysicalDocumentlist = resp.data.PhysicalDocTaggedDocument;
                $scope.PhysicalCovenantDocumentlist = resp.data.PhysicalCovenantDocTaggedDocument;
            });
        }

        $scope.gotoscanneddeferral = function () {
            $location.hash('scanneddeferraldocdtl');
            $anchorScroll();
        };

        $scope.gotoscannedcovenant = function () {
            $location.hash('scannedcovenantdocdtl');
            $anchorScroll();
        };

        $scope.gotophysicaldeferral = function () {
            $location.hash('physicaldeferraldocdtl');
            $anchorScroll();
        };

        $scope.gotophysicalcovenant = function () {
            $location.hash('physicalcovenantdocdtl');
            $anchorScroll();
        };

        $scope.deferraldoc_view = function (documentcheckdtl_gid, signeddocument_flag) {
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
                    documentcheckdtl_gid: documentcheckdtl_gid,
                    signeddocument_flag: signeddocument_flag
                }
                var url = 'api/AgrMstSuprScannedDocument/GetScannedDocument';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.scanneduploaddocumentlist = resp.data.scanneduploaddocument;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.download_doc = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }
                $scope.downloadall = function () {
                    for (var i = 0; i < $scope.scanneduploaddocumentlist.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.scanneduploaddocumentlist[i].file_path, $scope.scanneduploaddocumentlist[i].file_name);
                    }
                }
            }

        }

        $scope.deferralphysicaldoc_upload = function (documentcheckdtl_gid, documenttype_name, covenant_type, signeddocument_flag) {
            var modalInstance = $modal.open({
                templateUrl: '/defferal_docupload.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.txtdocument_name = documenttype_name;
                var params = {
                    documentcheckdtl_gid: documentcheckdtl_gid
                }
                var url = 'api/AgrTrnSuprPhysicalDocument/GetPhysicalDocument';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.physicaluploaddocumentlist = resp.data.scanneduploaddocument;
                });

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
                        var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[i].name, "documentformatonly");
    
                            if (IsValidExtension == false) {
                                Notify.alert("File format is not supported..!", {
                                    status: 'danger',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                unlockUI();
                                return false;
                            }
                    }

                    frm.append('document_title', documenttype_name);
                    frm.append('documentcheckdtl_gid', documentcheckdtl_gid);
                    frm.append('application_gid', application_gid);
                    frm.append('credit_gid', credit_gid);
                    frm.append('RMupload', 'N');
                    frm.append('covenant_type', covenant_type);
                    frm.append('project_flag', "documentformatonly");
                    $scope.uploadfrm = frm;
                    if ($scope.uploadfrm != undefined) {
                        var url = 'api/AgrTrnSuprPhysicalDocument/PhysicalDocumentUpload';
                        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                            $("#scannedfile").val('');
                            var params = {
                                documentcheckdtl_gid: documentcheckdtl_gid
                            }
                            var url = 'api/AgrTrnSuprPhysicalDocument/GetPhysicalDocument';
                            lockUI();
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.physicaluploaddocumentlist = resp.data.scanneduploaddocument;
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

                $scope.ok = function () {
                    activate();
                    $modalInstance.close('closed');
                };

                $scope.defdoc_delete = function (physicaldocument_gid) {
                    lockUI();
                    var params = {
                        physicaldocument_gid: physicaldocument_gid
                    }
                    var url = 'api/AgrTrnSuprPhysicalDocument/cancelphysicaluploaddocument';
                    SocketService.getparams(url, params).then(function (resp) {

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
                            documentcheckdtl_gid: documentcheckdtl_gid
                        }
                        var url = 'api/AgrTrnSuprPhysicalDocument/GetPhysicalDocument';
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            $scope.physicaluploaddocumentlist = resp.data.scanneduploaddocument;

                        });
                    });
                }
            }

        }

        $scope.defferalphysicaldoc_view = function (documentcheckdtl_gid) {
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
                    documentcheckdtl_gid: documentcheckdtl_gid
                }
                var url = 'api/AgrTrnSuprPhysicalDocument/GetPhysicalDocument';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.scanneduploaddocumentlist = resp.data.scanneduploaddocument;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.download_doc = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }
                $scope.downloadall = function () {
                    for (var i = 0; i < $scope.scanneduploaddocumentlist.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.scanneduploaddocumentlist[i].file_path, $scope.scanneduploaddocumentlist[i].file_name);
                    }
                }

            }
        }

        $scope.deferraldoc_upload = function (documentcheckdtl_gid, documenttype_name, covenant_type, signeddocument_flag) {
            var modalInstance = $modal.open({
                templateUrl: '/defferal_docupload.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.txtdocument_name = documenttype_name;
                var params = {
                    documentcheckdtl_gid: documentcheckdtl_gid,
                    signeddocument_flag: signeddocument_flag
                }
                var url = 'api/AgrMstSuprScannedDocument/GetRMScannedDocument';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.scannedmyuploaddocumentlist = resp.data.scanneduploaddocument;
                });

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
                        var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[i].name, "documentformatonly");
    
                            if (IsValidExtension == false) {
                                Notify.alert("File format is not supported..!", {
                                    status: 'danger',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                unlockUI();
                                return false;
                            }
                    }

                    frm.append('document_title', documenttype_name);
                    frm.append('RMupload', 'Y');
                    frm.append('documentcheckdtl_gid', documentcheckdtl_gid);
                    frm.append('application_gid', application_gid);
                    frm.append('credit_gid', credit_gid);
                    frm.append('covenant_type', covenant_type);
                    frm.append('signeddocument_flag', signeddocument_flag);
                    frm.append('project_flag', "documentformatonly");
                    $scope.uploadfrm = frm;
                    if ($scope.uploadfrm != undefined) {
                        var url = 'api/AgrMstSuprScannedDocument/ScannedDocumentUpload';
                        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                            $("#scannedfile").val('');
                            var params = {
                                documentcheckdtl_gid: documentcheckdtl_gid,
                                signeddocument_flag: signeddocument_flag
                            }
                            var url = 'api/AgrMstSuprScannedDocument/GetRMScannedDocument';
                            lockUI();
                            SocketService.getparams(url, params).then(function (resp) {
                                unlockUI();
                                $scope.scannedmyuploaddocumentlist = resp.data.scanneduploaddocument;
                            });
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

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.defdoc_delete = function (scanneddocument_gid) {
                    lockUI();
                    var params = {
                        scanneddocument_gid: scanneddocument_gid
                    }
                    var url = 'api/AgrMstSuprScannedDocument/cancelscanneduploaddocument';
                    SocketService.getparams(url, params).then(function (resp) {

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
                            documentcheckdtl_gid: documentcheckdtl_gid,
                            signeddocument_flag: 'N'
                        }
                        var url = 'api/AgrMstSuprScannedDocument/GetRMScannedDocument';
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            $scope.scannedmyuploaddocumentlist = resp.data.scanneduploaddocument;

                        });
                    });
                }
            }

        }

        $scope.tagto_defferal = function () {
            var modalInstance = $modal.open({
                templateUrl: '/TagToDefferal.html',
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
                $scope.pandtl_submit = function () {

                    //var params = {
                    //    address_typegid: $scope.cboaddresstype.address_gid,
                    //    address_type: $scope.cboaddresstype.address_type,

                    //}
                    //var url = 'api/MstApplicationAdd/PostInstitutionAddressDetail';
                    //lockUI();
                    //SocketService.post(url, params).then(function (resp) {
                    //    unlockUI();
                    //    if (resp.data.status == true) {

                    //        Notify.alert(resp.data.message, {
                    //            status: 'success',
                    //            pos: 'top-center',
                    //            timeout: 3000
                    //        });
                    //        institutionaddress_list();
                    //    }
                    //    else {
                    //        Notify.alert(resp.data.message, {
                    //            status: 'warning',
                    //            pos: 'top-center',
                    //            timeout: 3000
                    //        });
                    //    }
                    //});

                    $modalInstance.close('closed');
                }
            }
        }

        $scope.physicaltagtodefferal_edit = function (deferraltagdoc_gid, documentcheckdtl_gid, scanned_documentcount) {
            var modalInstance = $modal.open({
                templateUrl: '/PhysicalTagToDefferalEdit.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                lockUI();

                var params = {
                    documentcheckdtl_gid: documentcheckdtl_gid
                }
                var url = 'api/AgrTrnSuprPhysicalDocument/GettaggedDeferralinfo';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.errormsg = "";
                    $scope.showerrordiv = false;
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

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.documentverifiededitcheck = function (index) {
                    var querynotclosed = false;
                    $scope.errormsgScannedDoc = false;
                    $scope.errorBothmsg = false;
                    $scope.errormsgScannedDoc = false;
                    angular.forEach($scope.deferraltaggedchecklist, function (value, key) {
                        if (key == index)
                            value.deferraltagged = false;
                    });
                    var deferrralchecked = $scope.deferraltaggedchecklist.filter(function (el) { return el.deferraltagged === true || el.deferraltagged === "true" });
                    if (deferrralchecked && deferrralchecked.length > 0)
                        $scope.showdeferraltag = true;
                    else
                        $scope.showdeferraltag = false;
                    var documentverifychecked = $scope.deferraltaggedchecklist.filter(function (el) { return el.documentverified === true || el.documentverified === "true" });
                    if (documentverifychecked != null && documentverifychecked.length == $scope.deferraltaggedchecklist.length) {
                        var params = {
                            groupdocumentcheckdtl_gid: documentcheckdtl_gid,
                            lstype: ''
                        }
                        var url = 'api/AgrTrnSuprPhysicalDocument/GetPhysicalConfirmationValidation';
                        SocketService.getparams(url, params).then(function (resp) {
                            if (resp.data.status == true) {
                                querynotclosed = true;
                                $scope.showerrordiv = true;
                                if (scanned_documentcount == "0" && querynotclosed == true)
                                    $scope.errorBothmsg = true;
                                else if (scanned_documentcount != "0" && querynotclosed == true)
                                    $scope.errormsgqueryNC = true;
                            }
                            else {
                                if (scanned_documentcount == "0" && querynotclosed == false) {
                                    $scope.errormsgScannedDoc = true;
                                    $scope.showerrordiv = true;
                                }
                            }
                        });
                    }
                }

                $scope.deferraltaggededitcheck = function (index) {
                    angular.forEach($scope.deferraltaggedchecklist, function (value, key) {
                        if (key == index) {
                            value.documentverified = "false";
                            value.deferraltagged = "true";
                        }
                    });
                    var deferrralchecked = $scope.deferraltaggedchecklist.filter(function (el) { return el.deferraltagged === true || el.deferraltagged === "true" });
                    if (deferrralchecked && deferrralchecked.length > 0) {
                        $scope.showdeferraltag = true;
                    }
                    else {
                        $scope.showdeferraltag = false;
                    }
                }

                $scope.tagdoc_update = function () {
                    var params = {
                        documentcheckdtl_gid: documentcheckdtl_gid,
                        application_gid: application_gid,
                        credit_gid: credit_gid,
                        deferraltagdoc_gid: deferraltagdoc_gid,
                        documentseverity_gid: $scope.documentseverity_gid,
                        documentseverity_name: $scope.txtdocumentseverity_name,
                        tagged_to: $scope.cbotaggedto,
                        due_date: $scope.txtdue_date,
                        cad_remarks: $scope.txtcad_remarks,
                        deferraltaggedchecklist: $scope.deferraltaggedchecklist
                    }
                    var url = 'api/AgrTrnSuprPhysicalDocument/UpdatePhysicalDeferralTaggedDoc';
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
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });

                    $modalInstance.close('closed');
                }

            }
        }


        $scope.tagtodefferal_edit = function (deferraltagdoc_gid, documentcheckdtl_gid, scanned_documentcount) {
            var modalInstance = $modal.open({
                templateUrl: '/TagToDefferalEdit.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                lockUI();

                var params = {
                    documentcheckdtl_gid: documentcheckdtl_gid
                }
                var url = 'api/AgrMstSuprScannedDocument/GettaggedDeferralinfo';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.errormsg = "";
                    $scope.showerrordiv = false;
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

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.documentverifiededitcheck = function (index) {
                    var querynotclosed = false;
                    $scope.errormsgScannedDoc = false;
                    $scope.errorBothmsg = false;
                    $scope.errormsgScannedDoc = false;
                    angular.forEach($scope.deferraltaggedchecklist, function (value, key) {
                        if (key == index)
                            value.deferraltagged = false;
                    });
                    var deferrralchecked = $scope.deferraltaggedchecklist.filter(function (el) { return el.deferraltagged === true || el.deferraltagged === "true" });
                    if (deferrralchecked && deferrralchecked.length > 0)
                        $scope.showdeferraltag = true;
                    else
                        $scope.showdeferraltag = false;
                    var documentverifychecked = $scope.deferraltaggedchecklist.filter(function (el) { return el.documentverified === true || el.documentverified === "true" });
                    if (documentverifychecked != null && documentverifychecked.length == $scope.deferraltaggedchecklist.length) {
                        var params = {
                            groupdocumentcheckdtl_gid: documentcheckdtl_gid,
                            lstype: ''
                        }
                        var url = 'api/AgrMstSuprScannedDocument/GetConfirmationValidation';
                        SocketService.getparams(url, params).then(function (resp) {
                            if (resp.data.status == true) {
                                querynotclosed = true;
                                $scope.showerrordiv = true;
                                if (scanned_documentcount == "0" && querynotclosed == true)
                                    $scope.errorBothmsg = true;
                                else if (scanned_documentcount != "0" && querynotclosed == true)
                                    $scope.errormsgqueryNC = true;
                            }
                            else {
                                if (scanned_documentcount == "0" && querynotclosed == false) {
                                    $scope.errormsgScannedDoc = true;
                                    $scope.showerrordiv = true;
                                }
                            }
                        });
                    }
                }

                $scope.deferraltaggededitcheck = function (index) {
                    angular.forEach($scope.deferraltaggedchecklist, function (value, key) {
                        if (key == index) {
                            value.documentverified = "false";
                            value.deferraltagged = "true";
                        }
                    });
                    var deferrralchecked = $scope.deferraltaggedchecklist.filter(function (el) { return el.deferraltagged === true || el.deferraltagged === "true" });
                    if (deferrralchecked && deferrralchecked.length > 0) {
                        $scope.showdeferraltag = true;
                    }
                    else {
                        $scope.showdeferraltag = false;
                    }
                }

                $scope.tagdoc_update = function () {
                    var params = {
                        documentcheckdtl_gid: documentcheckdtl_gid,
                        application_gid: application_gid,
                        credit_gid: credit_gid,
                        deferraltagdoc_gid: deferraltagdoc_gid,
                        documentseverity_gid: $scope.documentseverity_gid,
                        documentseverity_name: $scope.txtdocumentseverity_name,
                        tagged_to: $scope.cbotaggedto,
                        due_date: $scope.txtdue_date,
                        cad_remarks: $scope.txtcad_remarks,
                        deferraltaggedchecklist: $scope.deferraltaggedchecklist
                    }
                    var url = 'api/AgrMstSuprScannedDocument/UpdateDeferralTaggedDoc';
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
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });

                    $modalInstance.close('closed');
                }

            }
        }



        $scope.viewcovenantperioddtl = function (groupdocumentchecklist_gid, documenttype_name, covenant_periods) {
            var modalInstance = $modal.open({
                templateUrl: '/covenantperiod_view.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.lbldocumentname = documenttype_name;
                $scope.lblcovenantperiods = covenant_periods;
                var params = {
                    groupdocumentdtl_gid: groupdocumentchecklist_gid,
                }
                var url = 'api/AgrMstSuprScannedDocument/GetScannedCovenantPeriodsSummary';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.scannedcovenantperiodlist = resp.data.mdlscannedcovenantperiod;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.Close_query = function (documentcheckdtl_gid) {
            $location.url('app/AgrTrnSuprRMDeferralCloseQuery?application_gid=' + application_gid + '&credit_gid=' + credit_gid + '&lspage=' + lspage + '&lstype=' + lstype + '&lsdocumentcheckdtl_gid=' + documentcheckdtl_gid);
        }

        $scope.extension_waiver = function (documentcheckdtl_gid, lsdeferraltag, scanned) {
            var lscompleted="";
            if (scanned == 'Y' && $scope.ScannedCompleted=='Y') {
                lscompleted = "CadScannedCompleted";
            }
            else if (scanned == 'N' && $scope.PhysicalCompleted=='Y') {
                lscompleted = "CadScannedCompleted";
            } 

            $location.url('app/AgrTrnSuprCadDeferralStatus?application_gid=' + application_gid + '&credit_gid=' + credit_gid + '&lspage=' + lspage + '&lstype=' + lstype + '&lsdocumentcheckdtl_gid=' + documentcheckdtl_gid + '&lsdeferraltag=' + lsdeferraltag + '&lscompleted=' + lscompleted + '&lsscanned=' + scanned);
        }

        $scope.defferal_history = function (documentcheckdtl_gid) {
            $location.url('app/AgrTrnScannedDeferralHistory?application_gid=' + application_gid + '&credit_gid=' + credit_gid + '&lspage=' + lspage + '&lstype=' + lstype + '&lsdocumentcheckdtl_gid=' + documentcheckdtl_gid);
        }

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.scanneduploaddocumentlist.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.scanneduploaddocumentlist[i].file_path, $scope.scanneduploaddocumentlist[i].file_name);
            }
        }

    }
})();
