(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnDeferralMyApprovalcontroller', AgrTrnDeferralMyApprovalcontroller);

    AgrTrnDeferralMyApprovalcontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','DownloaddocumentService'];

    function AgrTrnDeferralMyApprovalcontroller($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnDeferralMyApprovalcontroller';


        var application_gid = $location.search().application_gid;
        var extendorwaiverapproval_gid = $location.search().extendorwaiverapproval_gid;
        var initiateextendorwaiver_gid = $location.search().initiateextendorwaiver_gid;
        var documentcheckdtl_gid = $location.search().documentcheckdtl_gid;
        activate();

        function activate() {
            lockUI();
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
                initiateextendorwaiver_gid: initiateextendorwaiver_gid
            }

            var url = 'api/AgrMstScannedDocument/GetApprovalExtensionwaiver';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lblactivity_type = resp.data.activity_type;
                $scope.lblactivity_title = resp.data.activity_title;
                $scope.lblextendeddue_date = resp.data.extendeddue_date;
                $scope.lblreason = resp.data.reason;
                $scope.lblcreated_by = resp.data.created_by;
                $scope.lblcreated_date = resp.data.created_date;
                $scope.deferralapprovaldtl = resp.data.mdlapprovaldtl;
                $scope.lblfromphysical_document = resp.data.fromphysical_document;
                if ($scope.lblfromphysical_document == 'Y') {
                    var params = {
                        documentcheckdtl_gid: documentcheckdtl_gid
                    }
                    var url = 'api/AgrTrnPhysicalDocument/GetPhysicalDocument';
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.physicaluploaddocumentlist = resp.data.scanneduploaddocument;
                    });
                }
            });
            var params = {
                documentcheckdtl_gid: documentcheckdtl_gid
            }
            var url = 'api/AgrMstScannedDocument/GetInitiatedExtensionorwaiver';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.intiatextension_list = resp.data.mdlinitiateextendwaiver;
            });

            var params = {
                documentcheckdtl_gid: documentcheckdtl_gid,
                signeddocument_flag: 'Y'
            }
            var url = 'api/AgrMstScannedDocument/GetScannedDocument';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.scanneduploaddocumentlist = resp.data.scanneduploaddocument;

            });
            

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
        $scope.recproof_downloads = function (val1, val2) {
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
                //var url = 'api/AgrMstApplicationView/GetPurposeofLoan';
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

    }
})();
