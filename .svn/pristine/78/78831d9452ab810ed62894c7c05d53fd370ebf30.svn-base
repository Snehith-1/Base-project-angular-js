(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnBuyerDeferralMyApprovalcontroller', AgrTrnBuyerDeferralMyApprovalcontroller);

    AgrTrnBuyerDeferralMyApprovalcontroller.$inject = ['DownloaddocumentService', '$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AgrTrnBuyerDeferralMyApprovalcontroller(DownloaddocumentService, $rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnBuyerDeferralMyApprovalcontroller';


        var application_gid = $location.search().application_gid;
        var approval_initiationgid = $location.search().approval_initiationgid;
        var initiateextendorwaiver_gid = $location.search().initiateextendorwaiver_gid;
        var documentcheckdtl_gid = $location.search().documentcheckdtl_gid;
        var lsfromphysical_document = $location.search().fromphysical_document;

        activate();

        function activate() {
            lockUI();
            $scope.rejectselected = false;
            var params = {
                application_gid: application_gid
            }
            var url = 'api/AgrMstApplicationView/GetApplicationBasicView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtapplication_no = resp.data.application_no;
                $scope.txtbasiccustomer_name = resp.data.customer_name;
                $scope.txtbasicdesignation = resp.data.designation_type;
                $scope.txtvertical = resp.data.vertical_name;
                $scope.txtconstitution = resp.data.constitution_name;
                $scope.txtcredit_group = resp.data.creditgroup_name;
                $scope.txtapproval_status = resp.data.approval_status;
                $scope.txtccapproved_date = resp.data.ccapproved_date;
                $scope.lblcustomer_urn = resp.data.customer_urn;
            });

            var params = {
                approval_initiationgid: approval_initiationgid
            }
            if (lsfromphysical_document == "Y") {
                url = 'api/AgrTrnPhysicalDocument/GetInitiatedApprovalExtensionorwaiver';
            }
            else {
                url = 'api/AgrMstScannedDocument/GetInitiatedApprovalExtensionorwaiver';
            }
            SocketService.getparams(url, params).then(function (resp) {
                $scope.intiatextension_list = resp.data.mdlinitiateextendwaiver;
                //angular.forEach($scope.intiatextension_list, function (value, key) {
                    /*value.rdbapprovalstatus = true;*/
                /*});*/
            });

            var params = {
                initiateextendorwaiver_gid: initiateextendorwaiver_gid
            }

            var url = 'api/AgrMstScannedDocument/GetApprovalExtensionwaiver';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lblactivity_type = resp.data.activity_type;
                $scope.lblactivity_title = resp.data.activity_title;
                $scope.lblextendeddue_date = resp.data.extendeddue_date;
                $scope.lbldue_date = resp.data.due_date;
                $scope.lblreason = resp.data.reason;
                $scope.lblcreated_by = resp.data.created_by;
                $scope.lblcreated_date = resp.data.created_date;
                $scope.deferralapprovaldtl = resp.data.mdlapprovaldtl;
                $scope.lblfromphysical_document = resp.data.fromphysical_document;
            });

            unlockUI();
        }

        $scope.Back = function () {
            $state.go('app.myApproval');
        }



        // Get Request Remarks
        $scope.approval_remarks = function () {
            var modalInstance = $modal.open({
                templateUrl: '/ApprovalRemarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                //var params =
                //   {
                //       requestapproval_gid: requestapproval_gid,
                //   }
                //var url = 'api/osdTrnMyTicket/GetRequestRemarks';
                //lockUI();
                //SocketService.getparams(url, params).then(function (resp) {
                //    unlockUI();
                //    $scope.txtrequestapproval_remarks = resp.data.requestapproval_remarks;

                //});

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }


        //$scope.doc_downloads = function (val1, val2) {
        //    var phyPath = val1;
        //    var relPath = phyPath.split("StoryboardAPI");
        //    var relpath1 = relPath[1].replace("\\", "/");
        //    var hosts = window.location.host;
        //    var prefix = location.protocol + "//";
        //    var str = prefix.concat(hosts, relpath1);
        //    var link = document.createElement("a");
        //    link.download = val2;
        //    var uri = str;
        //    link.href = uri;
        //    link.click();
        //}

        $scope.doc_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.reject_submit = function () {
            var params = {
                extendorwaiverapproval_gid: extendorwaiverapproval_gid,
                initiateextendorwaiver_gid: initiateextendorwaiver_gid,
                approval_remarks: $scope.txtremarks,
                approval_status: 'Rejected'
            }
            var url = "";
            if ($scope.lblfromphysical_document == 'Y')
                url = 'api/AgrTrnPhysicalDocument/PostPhysicalextenstionwaiverApproval';
            else
                url = 'api/AgrMstScannedDocument/PostextenstionwaiverApproval';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.myApproval');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }

        $scope.approve_submit = function () {
            var params = {
                extendorwaiverapproval_gid: extendorwaiverapproval_gid,
                initiateextendorwaiver_gid: initiateextendorwaiver_gid,
                approval_remarks: $scope.txtremarks,
                approval_status: 'Approved'
            }
            var url = "";
            if ($scope.lblfromphysical_document == 'Y')
                url = 'api/AgrTrnPhysicalDocument/PostPhysicalextenstionwaiverApproval';
            else
                url = 'api/AgrMstScannedDocument/PostextenstionwaiverApproval';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.myApproval');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }
        $scope.overall_submit = function () {
            var lsgetdata = $scope.intiatextension_list.filter(function (el) { return el.rdbapprovalstatus == '' });
            if (lsgetdata != null && lsgetdata.length != 0) {
                Notify.alert('Kindly fill all approval status', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
                return;
            }
            angular.forEach($scope.intiatextension_list, function (value, key) {
                value.approval_remarks = $scope.txtremarks;
                value.approval_status = value.rdbapprovalstatus;
                if (value.rdbapprovalstatus == null) {
                    Notify.alert("Kindly fill the approval status / approval remarks", 'warning');
                    unlockUI();
                    return false;
                }

               else if ($scope.txtremarks == null || $scope.txtremarks == '' || $scope.txtremarks == undefined) {
                    Notify.alert("Kindly fill the approval status / approval remarks", 'warning');
                    unlockUI();
                    return false;
                }

                else {

                    var params = {
                        mdlapprovaldtl: $scope.intiatextension_list
                    }
                    var url = "";
                    if (lsfromphysical_document == "Y") {
                        url = 'api/AgrTrnPhysicalDocument/PostPhysicalextenstionwaiverApproval';
                    }
                    else {
                        url = 'api/AgrMstScannedDocument/PostextenstionwaiverApproval';
                    }
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $state.go('app.myApproval');
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });
    
                }

            });
           
        }
        $scope.descdoc_view = function () {
            var modalInstance = $modal.open({
                templateUrl: '/waiverdescdoc_view.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                //var params =
                //   {
                //       application2loan_gid: application2loan_gid
                //   }
                //var url = 'api/MstApplicationView/GetPurposeofLoan';
                //lockUI();
                //SocketService.getparams(url, params).then(function (resp) {
                //    unlockUI();
                //    $scope.txtpurposeof_loan = resp.data.enduse_purpose;

                //});  



                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.reasonapproval_view = function (reason, approval_status, initiateextendorwaiver_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/remarksandreasondtl.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.lblreason = reason;
                $scope.lblapproval_status = approval_status;
                if (approval_status != 'No Approval') {
                    $scope.lblapproval_status = '';
                    var params = {
                        initiateextendorwaiver_gid: initiateextendorwaiver_gid
                    }
                    var url = 'api/AgrMstScannedDocument/GetApprovalExtensionwaiver';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.approvallist = resp.data.mdlapprovaldtl;

                    });
                }


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }

        }

        $scope.softcopydetailsview = function (documentcheckdtl_gid, document_type) {
            var modalInstance = $modal.open({
                templateUrl: '/sofycopydocumentdetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance', 'DownloaddocumentService', 'cmnfunctionService'];
            function ModalInstanceCtrl($scope, $modalInstance, DownloaddocumentService, cmnfunctionService) {
                lockUI();
                $scope.txtdocument_type = document_type;
                var params = {
                    documentcheckdtl_gid: documentcheckdtl_gid,
                    signeddocument_flag: 'Y'
                }
                var url = 'api/AgrMstScannedDocument/GetScannedDocument';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.scanneduploaddocumentlist = resp.data.scanneduploaddocument;

                });
                lockUI();
                var params = {
                    documentcheckdtl_gid: documentcheckdtl_gid
                }
                var url = 'api/AgrTrnPhysicalDocument/GetPhysicalDocument';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.physicaluploaddocumentlist = resp.data.scanneduploaddocument;
                });
                $scope.recproof_downloads = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.downloadall = function () {
                    for (var i = 0; i < $scope.scanneduploaddocumentlist.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.scanneduploaddocumentlist[i].file_path, $scope.scanneduploaddocumentlist[i].file_name);
                    }
                }

                $scope.downloadall1 = function () {
                    for (var i = 0; i < $scope.physicaluploaddocumentlist.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.physicaluploaddocumentlist[i].file_path, $scope.physicaluploaddocumentlist[i].file_name);
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


            }
        }

        $scope.approvalhistory = function (documentcheckdtl_gid, document_type, initiateextendorwaiver_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/approvalHistorypopup.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                lockUI();
                $scope.txtdocument_type = document_type;
                var params = {
                    documentcheckdtl_gid: documentcheckdtl_gid
                }
                var url = 'api/AgrMstScannedDocument/GetInitiatedExtensionorwaiver';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.documentintiatextension_list = resp.data.mdlinitiateextendwaiver;
                    $scope.mdlapprovaldtl = resp.data.mdlapprovaldtl;
                    $scope.documentintiatextension_list = $scope.documentintiatextension_list.filter(function (el) { return el.initiateextendorwaiver_gid != initiateextendorwaiver_gid });
                    angular.forEach($scope.documentintiatextension_list, function (value, key) {
                        if (value.initiateextendorwaiver_gid != "") {
                            var getListArray = $scope.mdlapprovaldtl.filter(function (el) { return el.initiateextendorwaiver_gid === value.initiateextendorwaiver_gid });
                            if (getListArray != null) {
                                value.documentapprovalList = getListArray;
                            }
                        }
                    });
                });
                lockUI();
                var url = 'api/AgrTrnPhysicalDocument/GetInitiatedExtensionorwaiver';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.originaldocumentintiatextension_list = resp.data.mdlinitiateextendwaiver;
                    $scope.mdlapprovaldtl = resp.data.mdlapprovaldtl;
                    $scope.originaldocumentintiatextension_list = $scope.originaldocumentintiatextension_list.filter(function (el) { return el.initiateextendorwaiver_gid != initiateextendorwaiver_gid });
                    angular.forEach($scope.originaldocumentintiatextension_list, function (value, key) {
                        if (value.initiateextendorwaiver_gid != "") {
                            var getListArray = $scope.mdlapprovaldtl.filter(function (el) { return el.initiateextendorwaiver_gid === value.initiateextendorwaiver_gid });
                            if (getListArray != null) {
                                value.documentapprovalList = getListArray;
                            }
                        }
                    });
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.checkallapprove = function (checkall) {
            if (checkall == true) {
                angular.forEach($scope.intiatextension_list, function (value, key) {
                    value.rdbapprovalstatus = 'Approved';
                });
                $scope.allreject = false;
            }
            else {
                angular.forEach($scope.intiatextension_list, function (value, key) {
                    value.rdbapprovalstatus = '';
                });
            }
            $scope.rejectselected = false;
        }

        $scope.checkallreject = function (checkall) {
            if (checkall == true) {
                angular.forEach($scope.intiatextension_list, function (value, key) {
                    value.rdbapprovalstatus = 'Rejected';
                    $scope.rejectselected = true;
                });
                $scope.allapprove = false;
            }
            else {
                angular.forEach($scope.intiatextension_list, function (value, key) {
                    value.rdbapprovalstatus = '';
                    $scope.rejectselected = false;
                });
            }
        }

        $scope.changeapproval = function () {
            lockUI();
            var lsgetrejectdata = $scope.intiatextension_list.filter(function (el) { return el.rdbapprovalstatus === 'Rejected' });
            if (lsgetrejectdata != "" && lsgetrejectdata != null)
                $scope.rejectselected = true;
            else
                $scope.rejectselected = false;
            unlockUI();
        }
    }
})();
