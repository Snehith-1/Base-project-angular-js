(function () {
    'use strict';

    angular
        .module('angle')
        .controller('osdTrnMyActivityReopenHistory', osdTrnMyActivityReopenHistory);

    osdTrnMyActivityReopenHistory.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$modal', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout', 'DownloaddocumentService','cmnfunctionService'];

    function osdTrnMyActivityReopenHistory($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $modal, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'osdTrnMyActivityReopenHistory';
        //var page;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var servicerequest_gid = searchObject.servicerequest_gid;
        var requestreopen_gid = searchObject.requestreopen_gid;
        activate();

        function activate() {
            //var url = window.location.href;
            //var relPath = url.split("redirect_page=");
            //$scope.relpath1 = relPath[1];

            //page = localStorage.getItem('page');
            $scope.requestreopen_gid = requestreopen_gid;
            $scope.servicerequest_gid = servicerequest_gid;
            var param = {
                requestreopen_gid: requestreopen_gid
            }
            var url = "api/OsdTrnMyTicket/GetReopenHistory";
            SocketService.getparams(url, param).then(function (resp) {

                $scope.forwardreopendtl = resp.data.forwardreopendtl;
                $scope.forwarddocumentdtl = resp.data.forwarddocumentdtl;
                $scope.transferlistdtlreopen = resp.data.transferlistdtlreopen;
                $scope.completereopendocumentdtl = resp.data.completereopendocumentdtl;
                $scope.completed_by = resp.data.completed_by;
                $scope.completed_date = resp.data.completed_date;
                $scope.completed_remarks = resp.data.completed_remarks;
                $scope.lblfilename = resp.data.filename;
                $scope.lblfilepath = resp.data.filepath;

                $scope.raised_by = resp.data.raised_by;
                $scope.raised_date = resp.data.raised_date;
                $scope.reopencompleted_flag = resp.data.reopencompleted_flag;
                if($scope.reopencompleted_flag == 'Y')
                {
                   $scope.reopencompleteddtls=true;
                }
                else
                {
                    $scope.reopencompleteddtls=false;
                }
            });

            var params = {
                servicerequest_gid: servicerequest_gid
            }

            var url = "api/OsdTrnMyTicket/GetApprovalDtls";
                SocketService.getparams(url, params).then(function (resp) {
                $scope.approvaldetails = resp.data.approvaldetails;
            });

            var params = {
                servicerequest_gid: servicerequest_gid
            }
            var url = 'api/OsdTrnServiceRequest/GetServiceRequestView';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.tagmemberdtl = resp.data.tagmemberdtl;
                    $scope.closed_by = resp.data.closed_by;
                $scope.closed_date = resp.data.closed_date;
                $scope.closed_flag = resp.data.closed_flag;
                if ($scope.closed_flag == "Y") {

                    $scope.closeddtls = true;
                }
                else {

                    $scope.closeddtls = false;
                }

                });
            var url = "api/OsdTrnTicketManagement/GetAllocateManagerlist"
            SocketService.getparams(url, params).then(function (resp) {
                $scope.allocatelistdtlreopen = resp.data.allocatelistdtlreopen;

            });
        }


        $scope.close = function () {
            window.close();
        }
        $scope.CancelApprovalPerson = function (approval_token) {

            var modalInstance = $modal.open({
                templateUrl: '/cancelmembermodal.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    modalInstance.close('closed');
                };
                lockUI();
                var param = {
                    approval_token: approval_token
                };
                var url = 'api/OsdTrnRequestApproval/GetRequestDtl';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.request_title = resp.data.request_title;
                    $scope.request_refno = resp.data.request_refno;
                    $scope.activity_name = resp.data.activity_name;
                    $scope.assigned_dtl = resp.data.assigned_dtl;
                    $scope.getapproval_remarks = resp.data.getapproval_remarks;
                    $scope.hierary_level = resp.data.hierary_level;
                    $scope.servicerequest_gid = resp.data.servicerequest_gid;
                    $scope.approval_type = resp.data.approval_type;
                    unlockUI();
                });


                $scope.CancelMemberSubmit = function () {
                    var hierarylevel = $scope.hierary_level;
                    var level = ++hierarylevel;

                    var params = {
                        approval_token: approval_token,
                        approval_remarks: $scope.txtremarks,
                        hierary_level: level,
                        servicerequest_gid: $scope.servicerequest_gid,
                        approval_type: $scope.approval_type
                    }
                    lockUI();
                    var url = "api/OsdTrnRequestApproval/PostRequestCancelled";
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();

                            activate();
                        }
                        else {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();

                            activate();

                        }
                    });
                }
            }
        }

        $scope.downloads = function (val1, val2) {
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
