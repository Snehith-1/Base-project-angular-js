(function () {
    'use strict';

    angular
        .module('angle')
        .controller('osdTrnMyActivityHistory', osdTrnMyActivityHistory);

    osdTrnMyActivityHistory.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$modal', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout', 'DownloaddocumentService','cmnfunctionService'];

    function osdTrnMyActivityHistory($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $modal, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'osdTrnMyActivityHistory';
        //var page;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var servicerequest_gid = searchObject.servicerequest_gid;
        activate();
       
        function activate() {
        //    var url = window.location.href;
        //    var relPath = url.split("redirect_page=");
        //    $scope.relpath1 = relPath[1];
        //    page = localStorage.getItem('page');
            var param = {
                servicerequest_gid: servicerequest_gid
            }
            var url = "api/OsdTrnMyTicket/GetAllotted360";
            SocketService.getparams(url, param).then(function (resp) {

                $scope.request_refno = resp.data.request_refno;
                $scope.raised_date = resp.data.raised_date;
                $scope.request_title = resp.data.request_title;
                $scope.raised_by = resp.data.raised_by;
                $scope.request_status = resp.data.request_status;
                $scope.request_description = resp.data.request_description;
                // $('#request_description').html(resp.data.request_description);
                $scope.alloteddocumentdtl = resp.data.alloteddocumentdtl;
                $scope.forwarddocumentdtl = resp.data.forwarddocumentdtl;
                $scope.forward_remarks = resp.data.forward_remarks;
                $scope.forward_date = resp.data.forward_date;
                $scope.forward_to = resp.data.forward_to;
                $scope.forward_flag = resp.data.forward_flag;
                $scope.transfer_flag = resp.data.transfer_flag;
                $scope.assigned_team = resp.data.assigned_team;
                $scope.assigned_member = resp.data.assigned_member;
                $scope.reopenrequestdocumentdtl = resp.data.reopenrequestdocumentdtl;
                $scope.reopen_reason = resp.data.reopen_reason;
                $scope.reopened_date = resp.data.reopened_date;
                $scope.reopen_flag = resp.data.reopen_flag;
                $scope.lblfilename = resp.data.filename;
                $scope.lblfilepath = resp.data.filepath;

                $scope.reopendtl = resp.data.reopendtl;
            });

            var url = "api/OsdTrnTicketManagement/GetTransferMemberlist"

            SocketService.getparams(url, param).then(function (resp) {
                $scope.transferlistdtl = resp.data.transferlistdtl;
                unlockUI();
            });

            var url = "api/OsdTrnMyTicket/GetApprovalDtls"

            SocketService.getparams(url, param).then(function (resp) {
                $scope.approvaldetails = resp.data.approvaldetails;
                $scope.approvaldetailshistory = resp.data.approvaldetailshistory;

            });

            var url = "api/OsdTrnMyTicket/GetMultipleForward"

            SocketService.getparams(url, param).then(function (resp) {
                $scope.forwarddtl = resp.data.forwarddtl;
                unlockUI();
            });
            

            var url = 'api/OsdTrnMyTicket/GetCompletedDetails';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.completerequestdocumentdtl = resp.data.completerequestdocumentdtl;
                $scope.completed_remarks = resp.data.completed_remarks;
                $scope.completed_by = resp.data.completed_by;
                $scope.completed_date = resp.data.completed_date;
                $scope.lblfilename = resp.data.filename;
                $scope.lblfilepath = resp.data.filepath;
                unlockUI();
            });

            var url = "api/OsdTrnMyTicket/GetRequestorlist"

            SocketService.getparams(url, param).then(function (resp) {
                $scope.requestorlist = resp.data.requestordtl;
                $scope.requestordtlhistory = resp.data.requestordtlhistory;
                unlockUI();
            });

            var  params={
                servicerequest_gid: servicerequest_gid
            }
            var url = 'api/OsdTrnServiceRequest/GetServiceRequestView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.tagmemberdtl = resp.data.tagmemberdtl;
            });
            var params = {
                servicerequest_gid: servicerequest_gid
            }
            var url = "api/OsdTrnTicketManagement/GetAllocateManagerlist"
            SocketService.getparams(url, params).then(function (resp) {
                $scope.allocatelistdtl = resp.data.allocatelistdtl;

            });
        }

        $scope.close = function () {
            window.close();
        }
        //$scope.downloads = function (val1, val2) {
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
        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.downloadall = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        // Get Request Remarks
        $scope.request_remarks = function (requestapproval_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/RequestRemarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       requestapproval_gid: requestapproval_gid,
                   }
                var url = 'api/osdTrnMyTicket/GetRequestRemarks';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtrequestapproval_remarks = resp.data.requestapproval_remarks;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


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
