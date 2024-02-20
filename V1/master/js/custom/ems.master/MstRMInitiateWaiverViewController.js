(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstRMInitiateWaiverViewController', MstRMInitiateWaiverViewController);

    MstRMInitiateWaiverViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function MstRMInitiateWaiverViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstRMInitiateWaiverViewController';

        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.rmpostccwaiver_gid = $location.search().rmpostccwaiver_gid;
        var rmpostccwaiver_gid = $scope.rmpostccwaiver_gid;
        activate();

        function activate() {
            var params = {
                rmpostccwaiver_gid: $scope.rmpostccwaiver_gid
            }
            var url = 'api/MstRMPostCCWaiver/EditRMPostCCWaiver';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lblwaivercategory = resp.data.waiver_category;
                $scope.lblsanctionref_no = resp.data.sanction_refno;
                $scope.lbllan = resp.data.lan;
                $scope.lblurn = resp.data.urn;
                $scope.lblcustomer_info = resp.data.customer_info;
                $scope.lblwaiver_title = resp.data.waiver_title;
                $scope.lblwaiver_description = resp.data.waiver_description;
                $scope.lblwaiver_amount = resp.data.waiver_amount;
                $scope.lblapproval_type = resp.data.approval_type;
                $scope.lblapproval_remarks = resp.data.approval_remarks;
                $scope.sanctionwaiver_namelist = resp.data.sanctionwaiver_name;
                $scope.lanwaiver_namelist = resp.data.lanwaiver_name;
                $scope.waivergroup_namelist = resp.data.groupwaiver_name;
             

                if ($scope.cbowaivercategory == 'Sanction Waiver') {
                    $scope.sanctionviewdrop = true;
                    $scope.sanctionview = false;
                    $scope.lanwaiverdrop = false;
                    $scope.lanwaiver = false;
                }
                else if ($scope.cbowaivercategory == 'LAN Waiver') {
                    $scope.lanwaiverdrop = true;
                    $scope.lanwaiver = false;
                    $scope.sanctionviewdrop = false;
                    $scope.sanctionview = false;
                }            
                else {
                    $scope.sanctionview = true;
                    $scope.lanwaiver = true;
                    $scope.sanctionviewdrop = false;
                    $scope.lanwaiverdrop = false;
                }
                
            });

            var url = 'api/MstRMPostCCWaiver/WaiverDocList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.uploadwaiverdoc_list = resp.data.uploadwaiverdoc_list;
            });

            var url = 'api/MstRMPostCCWaiver/ApprovalMemberList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.waiverapprovalmember_list = resp.data.waiverapprovalmember_list;
            });

        }

        $scope.Back = function () {
            $location.url('app/MstRMInitiateWaiverSummary?application_gid=' + application_gid);
        }

        $scope.doc_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.uploadwaiverdoc_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.uploadwaiverdoc_list[i].document_path, $scope.uploadwaiverdoc_list[i].document_name);
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
})();
