(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnSuprCadPhysicalDochecklistController', AgrTrnSuprCadPhysicalDochecklistController);

    AgrTrnSuprCadPhysicalDochecklistController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

        function AgrTrnSuprCadPhysicalDochecklistController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnSuprCadPhysicalDochecklistController';
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        var credit_gid = $location.search().credit_gid;
        var lstype = $location.search().lstype;
        var processtypeassign_gid = $location.search().processtypeassign_gid;
        var lspath = $location.search().lspath;
        activate();

        function activate() {
            lockUI();

            $scope.hideeditevent = true;
            if (lspage == "CadPhysicalCompleted")
                $scope.hideeditevent = false;
            var params = {
                credit_gid: credit_gid,
                application_gid: application_gid
            }

            var url = 'api/AgrTrnSuprPhysicalDocument/GetCADTrnPhysicalDocList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtapplication_no = resp.data.application_no;
                $scope.txtbasiccustomer_name = resp.data.customer_name;
                $scope.ScannedDocumentlist = resp.data.PhysicalDocTaggedDocument;
                $scope.ScannnedCovenantDocumentlist = resp.data.PhysicalCovenantDocTaggedDocument;
            });

        }

        $scope.Back = function () {
            if (lspage == 'CadPhysicalDocMaker') {
                $location.url('app/AgrTrnSuprCadPhysicalDocGuarantorDtls?application_gid=' + application_gid + '&lspage=CadPhysicalDocMaker&processtypeassign_gid=' + processtypeassign_gid + '&lspath=' + lspath);
            }
            else {
                $location.url('app/AgrTrnSuprCadPhysicalDocGuarantorDtls?application_gid=' + application_gid + '&lspage=' + lspage + '&processtypeassign_gid=' + processtypeassign_gid + '&lspath=' + lspath);
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
                $scope.PopupHeader = "Physical Documents";
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
                    //var phyPath = val1;
                    //var relPath = phyPath.split("StoryboardAPI");
                    //var relpath1 = relPath[1].replace("\\", "/");
                    //var hosts = window.location.host;
                    //var prefix = location.protocol + "//";
                    //var str = prefix.concat(hosts, relpath1);
                    //var link = document.createElement("a");
                    //link.download = val2;
                    //var uri = str;
                    //link.href = uri;
                    //link.click();

                    DownloaddocumentService.Downloaddocument(val1, val2);
                }
                $scope.downloadall = function () {
                    for (var i = 0; i < $scope.scanneduploaddocumentlist.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.scanneduploaddocumentlist[i].file_path, $scope.scanneduploaddocumentlist[i].file_name);
                    }
                }


            }
        }

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
                $scope.PopupHeader = "Signed Documents";
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
                    //var phyPath = val1;
                    //var relPath = phyPath.split("StoryboardAPI");
                    //var relpath1 = relPath[1].replace("\\", "/");
                    //var hosts = window.location.host;
                    //var prefix = location.protocol + "//";
                    //var str = prefix.concat(hosts, relpath1);
                    //var link = document.createElement("a");
                    //link.download = val2;
                    //var uri = str;
                    //link.href = uri;
                    //link.click();

                    DownloaddocumentService.Downloaddocument(val1, val2);
                }
                $scope.downloadall = function () {
                    for (var i = 0; i < $scope.scanneduploaddocumentlist.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.scanneduploaddocumentlist[i].file_path, $scope.scanneduploaddocumentlist[i].file_name);
                    }
                }


            }

        }

        $scope.addcovenantperiods = function (groupdocumentchecklist_gid, documenttype_name, covenant_periods, Completed) {
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
                $scope.dropdowncovenant = false;
                var params = {
                    groupdocumentdtl_gid: groupdocumentchecklist_gid,
                }
                var url = 'api/AgrTrnSuprPhysicalDocument/GetPhysicalCovenantPeriodsSummary';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.scannedcovenantperiodlist = resp.data.mdlscannedcovenantperiod;
                    if ($scope.scannedcovenantperiodlist && $scope.scannedcovenantperiodlist.length > 0) {
                        $scope.dropdowncovenant = true;
                        $scope.cbocovenantperiod = covenant_periods;
                    }
                });
                $scope.calender01 = function ($event) {
                    $event.preventDefault();
                    $event.stopPropagation();

                    $scope.open01 = true;
                };
                // Calender Popup... //

                $scope.calender02 = function ($event) {
                    $event.preventDefault();
                    $event.stopPropagation();

                    $scope.open02 = true;
                };
                // Calender Popup... //

                $scope.calender03 = function ($event) {
                    $event.preventDefault();
                    $event.stopPropagation();

                    $scope.open03 = true;
                };
                $scope.formats = ['dd-MM-yyyy'];
                $scope.format = $scope.formats[0];
                $scope.dateOptions = {
                    formatYear: 'yy',
                    startingDay: 1
                };

                $scope.onchangecovenantstart_date = function () {
                    var addmonth = 0;
                    if ($scope.cbocovenantperiod == "Each month")
                        addmonth = 1;
                    else if ($scope.cbocovenantperiod == "Every 3 months")
                        addmonth = 3;
                    else if ($scope.cbocovenantperiod == "Every 6 months")
                        addmonth = 6;
                    var getdate = $scope.txtcovenantstart_date;
                    var d = new Date(getdate);
                    d = new Date(d.setMonth(d.getMonth() + parseInt(addmonth)));
                    //let day = newDate.getDate();
                    //let month = newDate.getMonth() + 1;
                    //let year = newDate.getFullYear();
                    var datestring = ("0" + d.getDate()).slice(-2) + "-" + ("0" + (d.getMonth() + 1)).slice(-2) + "-" + d.getFullYear();

                    $scope.txtcovenantend_date = datestring;
                    $scope.txtcovenantSubmission_date = $scope.txtcovenantend_date;

                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                    activate();
                };

                $scope.addcovenentperiodsclick = function () {
                    lockUI();
                    try {
                        if ($scope.txtcovenantend_date.split("-"))
                            $scope.txtcovenantend_date = $scope.txtcovenantend_date.split("-").reverse().join("-");
                    }
                    catch (e) { }
                    try {
                        if ($scope.txtcovenantSubmission_date.split("-"))
                            $scope.txtcovenantSubmission_date = $scope.txtcovenantSubmission_date.split("-").reverse().join("-");
                    }
                    catch (e) { }

                    var params = {
                        covenant_periods: $scope.cbocovenantperiod,
                        groupdocumentdtl_gid: groupdocumentchecklist_gid,
                        credit_gid: credit_gid,
                        covenant_startdate: $scope.txtcovenantstart_date,
                        covenant_enddate: $scope.txtcovenantend_date,
                        covenant_submissiondate: $scope.txtcovenantSubmission_date
                    }
                    console.log(params);
                    var url = 'api/AgrTrnSuprPhysicalDocument/PostPhysicalCovenantPeriods';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $scope.txtcovenantstart_date = "";
                            $scope.txtcovenantend_date = "";
                            $scope.txtcovenantSubmission_date = "";
                            $scope.dropdowncovenant = true;
                            var params = {
                                groupdocumentdtl_gid: groupdocumentchecklist_gid,
                            }
                            var url = 'api/AgrTrnSuprPhysicalDocument/GetPhysicalCovenantPeriodsSummary';
                            lockUI();
                            SocketService.getparams(url, params).then(function (resp) {
                                unlockUI();
                                $scope.scannedcovenantperiodlist = resp.data.mdlscannedcovenantperiod;

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
                            $modalInstance.close('closed');
                        }

                    });
                    unlockUI();

                }

                $scope.covenantperiod_delete = function (covenantperioddtl_gid) {
                    lockUI();
                    var params = {
                        covenantperioddtl_gid: covenantperioddtl_gid,
                        previous_covenantperiods: $scope.lblcovenantperiods,
                        groupdocumentdtl_gid: groupdocumentchecklist_gid,
                    }
                    var url = 'api/AgrTrnSuprPhysicalDocument/GetcancelPhysicalCovenantPeriod';
                    SocketService.post(url, params).then(function (resp) {

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
                            groupdocumentdtl_gid: groupdocumentchecklist_gid,
                        }
                        var url = 'api/AgrTrnSuprPhysicalDocument/GetPhysicalCovenantPeriodsSummary';
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            $scope.scannedcovenantperiodlist = resp.data.mdlscannedcovenantperiod;
                            if ($scope.scannedcovenantperiodlist == null) {
                                $scope.dropdowncovenant = false;
                            }
                        });
                    });
                }
            }
        }

        $scope.tagto_deferral = function (documentcheckdtl_gid, scanned_documentcount) {
            var modalInstance = $modal.open({
                templateUrl: '/TagToDeferral.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                lockUI();
                $scope.showdeferraltag = false;
                $scope.errormsg = "";
                $scope.showerrordiv = false;
                var querynotclosed = false;
                $scope.NoMasterData = false;
                var params = {
                    documentcheckdtl_gid: documentcheckdtl_gid,
                    lstype: lstype
                }
                var url = 'api/AgrTrnSuprPhysicalDocument/GetPhysicalMStDeferraltag';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.documentseverity_gid = resp.data.documentseverity_gid;
                    $scope.txtdocumentseverity_name = resp.data.documentseverity_name;
                    $scope.Checklist = resp.data.MstChecklist;
                    if ($scope.Checklist == null || $scope.Checklist == undefined) {
                        $scope.showerrordiv = true;
                        $scope.NoMasterData = true;
                    }
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.tagdoc_submit = function () {
                    var params = {
                        documentcheckdtl_gid: documentcheckdtl_gid,
                        application_gid: application_gid,
                        credit_gid: credit_gid,
                        documentseverity_gid: $scope.documentseverity_gid,
                        documentseverity_name: $scope.txtdocumentseverity_name,
                        tagged_to: $scope.cbotaggedto,
                        due_date: $scope.txtdue_date,
                        cad_remarks: $scope.txtcad_remarks,
                        deferraltaggedchecklist: $scope.Checklist
                    }
                    var url = 'api/AgrTrnSuprPhysicalDocument/PostPhysicalDeferralTaggedDoc';
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

                $scope.documentverifiedcheck = function (index) {
                    $scope.errormsgScannedDoc = false;
                    $scope.errorBothmsg = false;
                    $scope.errormsgScannedDoc = false;
                    angular.forEach($scope.Checklist, function (value, key) {
                        if (key == index)
                            value.deferraltagged = false;
                    });
                    var deferrralchecked = $scope.Checklist.filter(function (el) { return el.deferraltagged === "true" });
                    if (deferrralchecked && deferrralchecked.length > 0)
                        $scope.showdeferraltag = true;
                    else
                        $scope.showdeferraltag = false;
                    var documentverifychecked = $scope.Checklist.filter(function (el) { return el.documentverified === "true" });
                    if (documentverifychecked != null && documentverifychecked.length == $scope.Checklist.length) {
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

                $scope.deferraltaggedcheck = function (index) {
                    angular.forEach($scope.Checklist, function (value, key) {
                        if (key == index)
                            value.documentverified = false;
                    });
                    var deferrralchecked = $scope.Checklist.filter(function (el) { return el.deferraltagged === "true" });
                    if (deferrralchecked && deferrralchecked.length > 0) {
                        $scope.showdeferraltag = true;
                    }
                    else {
                        $scope.showdeferraltag = false;
                    }
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

        $scope.defferal_history = function (documentcheckdtl_gid) {
            $location.url('app/AgrTrnSuprPhysicalDeferralHistory?application_gid=' + application_gid + '&credit_gid=' + credit_gid + '&lspage=' + lspage + '&lstype=' + lstype + '&lsdocumentcheckdtl_gid=' + documentcheckdtl_gid + '&processtypeassign_gid=' + processtypeassign_gid + '&lspath=' + lspath);
        }

        $scope.raise_query = function (documentcheckdtl_gid) {
            $location.url('app/AgrTrnSuprCadPhysicalDocQuery?application_gid=' + application_gid + '&credit_gid=' + credit_gid + '&lspage=' + lspage + '&lstype=' + lstype + '&lsdocumentcheckdtl_gid=' + documentcheckdtl_gid + '&processtypeassign_gid=' + processtypeassign_gid + '&lspath=' + lspath);
        }

        $scope.extension_waiver = function (documentcheckdtl_gid, deferraltagged) {
            $location.url('app/AgrTrnSuprCadPhysicalDocStatus?application_gid=' + application_gid + '&credit_gid=' + credit_gid + '&lspage=' + lspage + '&lstype=' + lstype + '&lsdocumentcheckdtl_gid=' + documentcheckdtl_gid + '&processtypeassign_gid=' + processtypeassign_gid + '&lspath=' + lspath + '&lsdeferraltag=' + deferraltagged);
        }


        $scope.doc_confirmation = function () {
            var modalInstance = $modal.open({
                templateUrl: '/DocumentConfirmation.html',
                controller: ModalInstanceCtrl,
                size: 'md',
                backdrop: 'static',
                keyboard: false
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.onchangeremarks = function (txtfinalremarks) {
                    if (txtfinalremarks == 'Others') {
                        $scope.other_remarks = true;
                        $scope.scanfinal_remarks = '';
                    } else {
                        $scope.other_remarks = false;
                        $scope.scanfinal_remarks = '';
                    }
                }

                $scope.docconfirmation_Submit = function () {
                    if ($scope.txtfinalremarks == 'Others' && ($scope.scanfinal_remarks == '' || $scope.scanfinal_remarks == undefined || $scope.scanfinal_remarks == null)) {
                        $modalInstance.close('closed');
                        Notify.alert('Kindly Enter Remarks', 'warning')
                    }
                    else {
                        //var params = {
                        //    sanctiondocument_gid: documentation_list, 
                        //    finalremarks: $scope.txtfinalremarks,
                        //    scanfinal_remarks: $scope.scanfinal_remarks,
                        //    confirmation_type: 'Maker'
                        //}
                        //if (sanctiondocument_gid != undefined) {
                        //    var url = 'api/IdasTrnSanctionDoc/MkrChrBulkDocVerification';
                        //    lockUI()
                        //    SocketService.post(url, params).then(function (resp) {
                        //        if (resp.data.status == true) {

                        //            Notify.alert(resp.data.message, {
                        //                status: 'success',
                        //                pos: 'top-center',
                        //                timeout: 3000
                        //            });
                        //            $modalInstance.close('closed');
                        //            unlockUI();
                        //            activate();
                        //            $scope.checkallDocument = '';
                        //        }
                        //        else {
                        //            unlockUI();
                        //            Notify.alert(resp.data.message, {
                        //                status: 'warning',
                        //                pos: 'top-center',
                        //                timeout: 3000
                        //            });
                        //            $modalInstance.close('closed');
                        //            unlockUI();
                        //        }
                        //    });
                        //}
                        //else {
                        //    $modalInstance.close('closed');
                        //    Notify.alert('Select Atleast One Record!', 'warning')
                        //}

                        $modalInstance.close('closed');
                    }
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }

        }

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.scanneduploaddocumentlist.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.scanneduploaddocumentlist[i].file_path, $scope.scanneduploaddocumentlist[i].file_name);
            }
        }

    }
})();
