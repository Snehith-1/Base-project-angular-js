(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstDeferralMyApprovalHistorycontroller', MstDeferralMyApprovalHistorycontroller);

    MstDeferralMyApprovalHistorycontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','DownloaddocumentService'];

    function MstDeferralMyApprovalHistorycontroller($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstDeferralMyApprovalHistorycontroller';

       
        var application_gid = $location.search().application_gid;
        var extendorwaiverapproval_gid = $location.search().extendorwaiverapproval_gid;
        var initiateextendorwaiver_gid = $location.search().initiateextendorwaiver_gid;
        var documentcheckdtl_gid = $location.search().documentcheckdtl_gid;
        var lsfromphysical_document = $location.search().fromphysical_document;
        activate();
        function activate() {
            lockUI();
            var params = {
                application_gid: application_gid
            }
            var url = 'api/MstApplicationView/GetApplicationBasicView';
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

            var url = 'api/MstScannedDocument/GetApprovalExtensionwaiver';
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
                if ($scope.lblfromphysical_document == 'Y') {
                    var params = {
                        documentcheckdtl_gid: documentcheckdtl_gid
                    }
                    var url = 'api/MstPhysicalDocument/GetPhysicalDocument';
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.physicaluploaddocumentlist = resp.data.scanneduploaddocument;
                    });
                }
            });
            var params = {
                documentcheckdtl_gid: documentcheckdtl_gid 
            }
            var url="";
            if(lsfromphysical_document=="Y"){
                url = 'api/MstPhysicalDocument/GetInitiatedExtensionorwaiver';
            }
            else{
                url = 'api/MstScannedDocument/GetInitiatedExtensionorwaiver';
            }   
            SocketService.getparams(url, params).then(function (resp) {
                $scope.intiatextension_list = resp.data.mdlinitiateextendwaiver; 
            });

            var params = {
                documentcheckdtl_gid: documentcheckdtl_gid,
                signeddocument_flag: 'Y'
            }
            var url = 'api/MstScannedDocument/GetScannedDocument';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.scanneduploaddocumentlist = resp.data.scanneduploaddocument; 
            });
        }

        $scope.Back = function () {
            $state.go('app.myApproval');
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

        // Get Request Remarks
        $scope.approval_remarks = function (approval_remarks) {
            var modalInstance = $modal.open({
                templateUrl: '/remarkshistory.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.lblapproval_remarks = approval_remarks;

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
                    var url = 'api/MstScannedDocument/GetApprovalExtensionwaiver';
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
