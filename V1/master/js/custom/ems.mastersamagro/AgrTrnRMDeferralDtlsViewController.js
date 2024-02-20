(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnRMDeferralDtlsViewController', AgrTrnRMDeferralDtlsViewController);

    AgrTrnRMDeferralDtlsViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', '$anchorScroll', 'DownloaddocumentService','cmnfunctionService'];

    function AgrTrnRMDeferralDtlsViewController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, $anchorScroll, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnRMDeferralDtlsViewController';

        var lspage = $location.search().lspage;
        var application_gid = $location.search().application_gid;
        var credit_gid = $location.search().credit_gid;
        var lstype = $location.search().lstype;

        $scope.company_name = $location.search().lscompany_name;
        var company_name = $scope.company_name;

        $scope.individual_name = $location.search().lsindividual_name;
        var individual_name = $scope.individual_name;

        $scope.companystakeholder = $location.search().lscompanystakeholder;
        var companystakeholder = $scope.companystakeholder;

        $scope.individualstakeholder = $location.search().lsindividualstakeholder;
        var individualstakeholder = $scope.individualstakeholder;

        activate();

        function activate() {
            lockUI();
            var params = {
                credit_gid: credit_gid,
                application_gid: application_gid
            }

            var url = 'api/AgrMstScannedDocument/GetCADTrnScannedDocList';
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

            var url = 'api/AgrMstScannedDocument/GetCompletedDocumentInfo';
            SocketService.getparams(url, params).then(function (resp) { 
                $scope.PhysicalCompleted = resp.data.PhysicalCompleted;
                $scope.ScannedCompleted = resp.data.ScannedCompleted; 
            }); 

            //if (lstype == 'Institution') {
            //    $scope.txtcustomer_name = $scope.company_name;
            //    $scope.txtstakeholder = $scope.companystakeholder
            //}
            //else if (lstype == 'Individual') {
            //    $scope.txtcustomer_name = $scope.individual_name;
            //    $scope.txtstakeholder = $scope.individualstakeholder
            //}
            //else { }
            var params = {
                credit_gid: credit_gid,
            }
            var url = 'api/AgrMstApplicationView/Getlabels';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtcustomer_name = resp.data.customer_name;
                $scope.txtstakeholder = resp.data.stakeholder_type;
            });
        }

        $scope.Back = function () {
            $location.url('app/AgrTrnRMDeferralDtls?application_gid=' + application_gid);
        }

        $scope.scanneddocumentssummary = function () {
            var params = {
                credit_gid: credit_gid,
                application_gid: application_gid
            }

            var url = 'api/AgrMstScannedDocument/GetCADTrnScannedDocList';
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

            var url = 'api/AgrTrnPhysicalDocument/GetCADTrnPhysicalDocList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtapplication_no = resp.data.application_no;
                $scope.txtbasiccustomer_name = resp.data.customer_name;
                $scope.PhysicalDocumentlist = resp.data.PhysicalDocTaggedDocument;
                $scope.PhysicalCovenantDocumentlist = resp.data.PhysicalCovenantDocTaggedDocument;
                angular.forEach($scope.PhysicalDocumentlist, function (value, key) {
                    var getscanneddocument_count = $scope.ScannedDocumentlist.filter(function (el) { return el.groupdocumentchecklist_gid == value.groupdocumentchecklist_gid });
                    if (getscanneddocument_count && getscanneddocument_count.length > 0)
                        value.scanned_documentcount = getscanneddocument_count[0].scanned_documentcount;
                    else
                        value.scanned_documentcount = 0;
                });
                angular.forEach($scope.PhysicalCovenantDocumentlist, function (value, key) {
                    var getscanneddocument_count = $scope.ScannnedCovenantDocumentlist.filter(function (el) { return el.groupdocumentchecklist_gid == value.groupdocumentchecklist_gid });
                    if (getscanneddocument_count && getscanneddocument_count.length > 0)
                        value.scanned_documentcount = getscanneddocument_count[0].scanned_documentcount;
                    else
                        value.scanned_documentcount = 0;
                });
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
                var url = 'api/AgrMstScannedDocument/GetScannedDocument';
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

                $scope.documentviewer = function (val1, val2) {
                    lockUI();
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                    if (IsValidExtension == false) {
                        $modalInstance.close('closed');
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
                var url = 'api/AgrTrnPhysicalDocument/GetPhysicalDocument';
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
                        var url = 'api/AgrTrnPhysicalDocument/PhysicalDocumentUpload';
                        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                            $("#scannedfile").val('');
                            var params = {
                                documentcheckdtl_gid: documentcheckdtl_gid
                            }
                            var url = 'api/AgrTrnPhysicalDocument/GetPhysicalDocument';
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
                    var url = 'api/AgrTrnPhysicalDocument/cancelphysicaluploaddocument';
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
                        var url = 'api/AgrTrnPhysicalDocument/GetPhysicalDocument';
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            $scope.physicaluploaddocumentlist = resp.data.scanneduploaddocument;

                        });
                    });
                }

                $scope.documentviewer = function (val1, val2) {
                    lockUI();
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                    if (IsValidExtension == false) {
                        $modalInstance.close('closed');
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
                var url = 'api/AgrTrnPhysicalDocument/GetPhysicalDocument';
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

        //$scope.deferraldoc_upload = function (documentcheckdtl_gid, documenttype_name, covenant_type, signeddocument_flag) {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/defferal_docupload.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'lg'
        //    });
        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {
        //        $scope.txtdocument_name = documenttype_name;
        //        var params = {
        //            documentcheckdtl_gid: documentcheckdtl_gid,
        //            signeddocument_flag: signeddocument_flag
        //        }
        //        var url = 'api/AgrMstScannedDocument/GetRMScannedDocument';
        //        lockUI();
        //        SocketService.getparams(url, params).then(function (resp) {
        //            unlockUI();
        //            $scope.scannedmyuploaddocumentlist = resp.data.scanneduploaddocument;
        //        });

        //        $scope.scannedDocumentUpload = function (val, val1, name) {
        //            lockUI();
        //            var frm = new FormData();
                    
        //            for (var i = 0; i < val.length; i++) {
        //                var item = {
        //                    name: val[i].name,
        //                    file: val[i]
        //                };
        //                frm.append('fileupload', item.file);
        //                frm.append('file_name', item.name);
        //                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[i].name, "documentformatonly");
    
        //                    if (IsValidExtension == false) {
        //                        Notify.alert("File format is not supported..!", {
        //                            status: 'danger',
        //                            pos: 'top-center',
        //                            timeout: 3000
        //                        });
        //                        unlockUI();
        //                        return false;
        //                    }
        //            }

        //            frm.append('document_title', documenttype_name);
        //            frm.append('RMupload', 'Y');
        //            frm.append('documentcheckdtl_gid', documentcheckdtl_gid);
        //            frm.append('application_gid', application_gid);
        //            frm.append('credit_gid', credit_gid);
        //            frm.append('covenant_type', covenant_type);
        //            frm.append('signeddocument_flag', signeddocument_flag);
        //frm.append('project_flag', "documentformatonly");
        //            $scope.uploadfrm = frm;
        //            if ($scope.uploadfrm != undefined) {
        //                var url = 'api/AgrMstScannedDocument/ScannedDocumentUpload';
        //                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
        //                    $("#scannedfile").val('');
        //                    var params = {
        //                        documentcheckdtl_gid: documentcheckdtl_gid,
        //                        signeddocument_flag: signeddocument_flag
        //                    }
        //                    var url = 'api/AgrMstScannedDocument/GetRMScannedDocument';
        //                    lockUI();
        //                    SocketService.getparams(url, params).then(function (resp) {
        //                        unlockUI();
        //                        $scope.scannedmyuploaddocumentlist = resp.data.scanneduploaddocument;
        //                    });
        //                    if (resp.data.status == true) {
        //                        Notify.alert(resp.data.message, {
        //                            status: 'success',
        //                            pos: 'top-center',
        //                            timeout: 3000
        //                        });
        //                    }
        //                    else {
        //                        Notify.alert(resp.data.message, {
        //                            status: 'warning',
        //                            pos: 'top-center',
        //                            timeout: 3000
        //                        });
        //                    }
        //                    unlockUI();
        //                });
        //            }
        //            else {
        //                alert('Please select a file.')

        //            }
        //        }

        //        $scope.ok = function () {
        //            $modalInstance.close('closed');
        //        };

        //        $scope.defdoc_delete = function (scanneddocument_gid) {
        //            lockUI();
        //            var params = {
        //                scanneddocument_gid: scanneddocument_gid
        //            }
        //            var url = 'api/AgrMstScannedDocument/cancelscanneduploaddocument';
        //            SocketService.getparams(url, params).then(function (resp) {

        //                if (resp.data.status == true) {

        //                    Notify.alert(resp.data.message, {
        //                        status: 'success',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                }
        //                else {
        //                    Notify.alert(resp.data.message, {
        //                        status: 'warning',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });

        //                }
        //                var params = {
        //                    documentcheckdtl_gid: documentcheckdtl_gid,
        //                    signeddocument_flag: 'N'
        //                }
        //                var url = 'api/AgrMstScannedDocument/GetRMScannedDocument';
        //                lockUI();
        //                SocketService.getparams(url, params).then(function (resp) {
        //                    unlockUI();
        //                    $scope.scannedmyuploaddocumentlist = resp.data.scanneduploaddocument;

        //                });
        //            });
        //        }
        //    }

        //}

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
                var url = 'api/AgrTrnPhysicalDocument/GettaggedDeferralinfo';
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
                        var url = 'api/AgrTrnPhysicalDocument/GetPhysicalConfirmationValidation';
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
                    var url = 'api/AgrTrnPhysicalDocument/UpdatePhysicalDeferralTaggedDoc';
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
                var url = 'api/AgrMstScannedDocument/GettaggedDeferralinfo';
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
                        var url = 'api/AgrMstScannedDocument/GetConfirmationValidation';
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
                    var url = 'api/AgrMstScannedDocument/UpdateDeferralTaggedDoc';
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
                var url = 'api/AgrMstScannedDocument/GetScannedCovenantPeriodsSummary';
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
            $location.url('app/AgrTrnRMDeferralCloseQuery?application_gid=' + application_gid + '&credit_gid=' + credit_gid + '&lspage=' + lspage + '&lstype=' + lstype + '&lsdocumentcheckdtl_gid=' + documentcheckdtl_gid);
        }

        $scope.extension_waiver = function (documentcheckdtl_gid, lsdeferraltag, scanned) {
            var lscompleted="";
            if (scanned == 'Y' && $scope.ScannedCompleted=='Y') {
                lscompleted = "CadScannedCompleted";
            }
            else if (scanned == 'N' && $scope.PhysicalCompleted=='Y') {
                lscompleted = "CadScannedCompleted";
            } 

            $location.url('app/AgrTrnPMGDeferralStatus?application_gid=' + application_gid + '&credit_gid=' + credit_gid + '&lspage=' + lspage + '&lstype=' + lstype + '&lsdocumentcheckdtl_gid=' + documentcheckdtl_gid + '&lsdeferraltag=' + lsdeferraltag + '&lscompleted=' + lscompleted + '&lsscanned=' + scanned);
        }

        $scope.defferal_history = function (documentcheckdtl_gid) {
            $location.url('app/AgrTrnPhysicalDeferralHistory?application_gid=' + application_gid + '&credit_gid=' + credit_gid + '&lspage=' + lspage + '&lstype=' + lstype + '&lsdocumentcheckdtl_gid=' + documentcheckdtl_gid);
        }

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.scanneduploaddocumentlist.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.scanneduploaddocumentlist[i].file_path, $scope.scanneduploaddocumentlist[i].file_name);
            }
        }

        $scope.initiatebulkapproval = function (lsactivity, lscovenant) {
            var params = {
                lsinitiate: lsactivity,
                application_gid: application_gid,
                credit_gid: credit_gid,
                covenant_type: lscovenant
            }
            var url = 'api/AgrMstScannedDocument/PostMultipleInitiateExtensionorwaiver';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
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

        $scope.deferraldocmultiple_upload = function (covenant_type) {
            var modalInstance = $modal.open({
                templateUrl: '/defferal_docmultipleupload.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.scannedDocumentUpload = function (val, val1, name) {
                    lockUI();
                    if ($scope.txtdocument_name == "" || $scope.txtdocument_name == undefined || $scope.txtdocument_name == null) {
                        alert('Kindly enter the document title');
                        $("#scannedmultiplefile").val('');
                        unlockUI();
                        return;
                    }
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
                    frm.append('tagquery_gid', '');
                    frm.append('document_title', $scope.txtdocument_name);
                    frm.append('application_gid', application_gid);
                    frm.append('RMupload', 'Y');
                    frm.append('covenant_type', covenant_type);
                    frm.append('project_flag', "documentformatonly");
                    $scope.uploadfrm = frm;
                    if ($scope.uploadfrm != undefined) {
                        var url = 'api/AgrMstScannedDocument/ScannedDocumentMultiUpload';
                        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                            unlockUI();
                            $("#scannedmultiplefile").val('');
                            unlockUI();
                            if (resp.data.status == true) {
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                activate();
                                $modalInstance.close('closed');
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
                        alert('Please select a file.')

                    }
                }

                $scope.ok = function () {
                    activate();
                    $modalInstance.close('closed');
                };

                $scope.documentviewer = function (val1, val2) {
                    lockUI();
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                    if (IsValidExtension == false) {
                        $modalInstance.close('closed');
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
                var url = 'api/AgrMstScannedDocument/GetScannedDocument';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.scanneduploaddocumentlist = resp.data.scanneduploaddocument;

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
                    frm.append('tagquery_gid', '');
                    frm.append('application_gid', application_gid);
                    frm.append('credit_gid', credit_gid);
                    frm.append('RMupload', 'Y');
                    frm.append('covenant_type', covenant_type);
                    frm.append('signeddocument_flag', signeddocument_flag);
                    frm.append('project_flag', "documentformatonly");
                    $scope.uploadfrm = frm;
                    if ($scope.uploadfrm != undefined) {
                        var url = 'api/AgrMstScannedDocument/ScannedDocumentUpload';
                        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                            unlockUI();
                            $("#scannedfile").val('');
                            var params = {
                                documentcheckdtl_gid: documentcheckdtl_gid,
                                signeddocument_flag: signeddocument_flag
                            }
                            var url = 'api/AgrMstScannedDocument/GetScannedDocument';
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

                $scope.ok = function () {
                    activate();
                    $modalInstance.close('closed');
                };

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
                    var url = 'api/AgrMstScannedDocument/cancelscanneduploaddocument';
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
                            documentcheckdtl_gid: documentcheckdtl_gid,
                            signeddocument_flag: 'Y'
                        }
                        var url = 'api/AgrMstScannedDocument/GetScannedDocument';
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            $scope.scanneduploaddocumentlist = resp.data.scanneduploaddocument;

                        });
                    });
                }

                $scope.documentviewer = function (val1, val2) {
                    lockUI();
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                    if (IsValidExtension == false) {
                        $modalInstance.close('closed');
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
            }

        }

        $scope.movedtosigned_doc = function (documentcheckdtl_gid, scanneddocument_gid, ScannedDocumentlist) {
            var modalInstance = $modal.open({
                templateUrl: '/MovedtosignedDoc.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.showsubmitbtn = true;
                var params = {
                    documentcheckdtl_gid: documentcheckdtl_gid,
                    signeddocument_flag: 'N'
                }
                var url = 'api/AgrMstScannedDocument/GetScannedDocument';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.scanneduploaddocumentlist = resp.data.scanneduploaddocument;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.checkall = function (selected, covenantdocumentclick) {
                    if (selected == true)
                        $scope.showsubmitbtn = false;
                    else
                        $scope.showsubmitbtn = true;
                    angular.forEach($scope.scanneduploaddocumentlist, function (val, key) {
                        val.checked = selected;
                    });
                }
                $scope.checkboxchange = function () {
                    var getselected = $scope.scanneduploaddocumentlist.filter(function (el) { return el.checked == true });
                    if (getselected && getselected != null && getselected.length != 0) {
                        $scope.showsubmitbtn = false;
                    }
                    else {
                        $scope.showsubmitbtn = true;
                    }
                }
                $scope.download_doc = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }

                $scope.downloadallsca = function () {
                    for (var i = 0; i < $scope.scanneduploaddocumentlist.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.scanneduploaddocumentlist[i].file_path, $scope.scanneduploaddocumentlist[i].file_name);
                    }
                }

                $scope.yesmovedtosigned = function () {
                    lockUI();
                    var doc_gid;
                    var doclistGId = [];
                    var count = 0;

                    if ($scope.scanneduploaddocumentlist == null) {
                        Notify.alert("Select atleast one document..!", 'warning');
                        $modalInstance.close('closed');
                        unlockUI();
                        return false;
                    }
                    else {

                        angular.forEach($scope.scanneduploaddocumentlist, function (val) {
                            if (val.checked == true) {
                                count = count + 1;
                                var doclist_gid = val.scanneddocument_gid;
                                doc_gid = val.scanneddocument_gid;
                                doclistGId.push(doclist_gid);
                            }
                        });

                        var params = {
                            scanneddocument_gid: doclistGId,
                            documentcheckdtl_gid: documentcheckdtl_gid
                        }
                        if (doc_gid != undefined) {
                            var url = 'api/AgrMstScannedDocument/PostMovedtoSignedDocPMG';
                            lockUI();
                            SocketService.post(url, params).then(function (resp) {
                                unlockUI();
                                if (resp.data.status == true) {
                                    $modalInstance.close('closed');
                                    activate();
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
                                    $modalInstance.close('closed');
                                }

                            });
                        }
                        else {
                            Notify.alert('Select atleast one document..!', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }


                    }
                }

                $scope.documentviewer = function (val1, val2) {
                    lockUI();
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                    if (IsValidExtension == false) {
                        $modalInstance.close('closed');
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
            }

        }


        $scope.Close_query = function (documentcheckdtl_gid, lsdeferraltag, lscovenant_type) {
            $location.url('app/AgrTrnPMGDeferralCloseQuery?application_gid=' + application_gid + '&credit_gid=' + credit_gid + '&lspage=' + lspage + '&lstype=' + lstype + '&lsdeferraltag=' + lsdeferraltag + '&lscovenant_type=' + lscovenant_type + '&lsdocumentcheckdtl_gid=' + documentcheckdtl_gid);
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


        $scope.addcovenantperiods = function (groupdocumentchecklist_gid, documenttype_name, covenant_periods, buffer_days, Completed) {
            var modalInstance = $modal.open({
                templateUrl: '/AddCovenantPeriods.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.Completed = false;
                if (Completed == "Y")
                    $scope.Completed = true;

                $scope.lbldocumentname = documenttype_name;
                $scope.lblcovenantperiods = covenant_periods;
                $scope.lblbufferdays = buffer_days;
                $scope.dropdowncovenant = false;
                var params = {
                    groupdocumentdtl_gid: groupdocumentchecklist_gid,
                }
                var url = 'api/AgrMstScannedDocument/GetScannedCovenantPeriodsSummary';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.scannedcovenantperiodlist = resp.data.mdlscannedcovenantperiod;
                    if ($scope.scannedcovenantperiodlist && $scope.scannedcovenantperiodlist.length > 0) {
                        $scope.dropdowncovenant = true;
                        $scope.cbocovenantperiod = covenant_periods;
                        $scope.cbobufferdays = buffer_days;
                    }
                });
                

                $scope.ok = function () {
                    $modalInstance.close('closed');
                   
                };

              

               
            }
        }


        $scope.addphycovenantperiods = function (groupdocumentchecklist_gid, documenttype_name, covenant_periods, buffer_days, Completed) {
            var modalInstance = $modal.open({
                templateUrl: '/AddCovenantPeriods.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.Completed = false;
                if (Completed == "Y")
                    $scope.Completed = true;

                $scope.lbldocumentname = documenttype_name;
                $scope.lblcovenantperiods = covenant_periods;
                $scope.lblbufferdays = buffer_days;
                $scope.dropdowncovenant = false;
                var params = {
                    groupdocumentdtl_gid: groupdocumentchecklist_gid,
                }
                var url = 'api/AgrTrnPhysicalDocument/GetPhysicalCovenantPeriodsSummary';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.scannedcovenantperiodlist = resp.data.mdlscannedcovenantperiod;
                    if ($scope.scannedcovenantperiodlist && $scope.scannedcovenantperiodlist.length > 0) {
                        $scope.dropdowncovenant = true;
                        $scope.cbocovenantperiod = covenant_periods;
                        $scope.cbobufferdays = buffer_days;
                    }
                });
              

         

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

              

            }
        }

        $scope.initiatephysicalbulkapproval = function (lsactivity,lscovenant) {
            var params = {
                lsinitiate: lsactivity,
                application_gid: application_gid,
                credit_gid: credit_gid,
                covenant_type : lscovenant
            }
            var url = 'api/AgrTrnPhysicalDocument/PostMultipleInitiateExtensionorwaiver';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $scope.physicaldocumentsSummary();
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

        $scope.defferalphysicaldoc_sent = function (documentcheckdtl_gid, covenant_type) {
            var params = {
                documentcheckdtl_gid: documentcheckdtl_gid,
                application_gid: application_gid,
                scanneddoc_flag: 'N',
                covenant_type: covenant_type,
                document_status: 'Document Sent'
            }
            var url = 'api/AgrTrnPhysicalDocument/PostPhysicalDocSent';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $scope.physicaldocumentsSummary();
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

        $scope.defferalphysicaldoc_undo = function (documentcheckdtl_gid, covenant_type) {
            var params = {
                documentcheckdtl_gid: documentcheckdtl_gid,
                application_gid: application_gid,
                scanneddoc_flag: 'N',
                covenant_type: covenant_type,
                document_status: ''
            }
            var url = 'api/AgrTrnPhysicalDocument/PostPhysicalDocSent';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $scope.physicaldocumentsSummary();
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

    }
})();
