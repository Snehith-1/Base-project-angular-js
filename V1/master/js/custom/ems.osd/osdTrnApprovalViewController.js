(function () {
    'use strict';

    angular
        .module('angle')
        .controller('osdTrnApprovalViewController', osdTrnApprovalViewController);

    osdTrnApprovalViewController.$inject = ['$rootScope', '$scope', '$sce', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout', 'DownloaddocumentService', 'cmnfunctionService'];

    function osdTrnApprovalViewController($rootScope, $scope, $sce, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'osdTrnApprovalViewController';
        var url = window.location.href;
        var relPath = url.split("?id=");
        var relpath1 = relPath[1];
        activate();

        function activate() {
            var param = {
                requestapproval_gid: localStorage.getItem('requestapproval_gid')
            }
            
            var url = 'api/OsdTrnRequestApproval/GetApprovaldetails';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.request_title = resp.data.request_title;
                $scope.request_refno = resp.data.request_refno;
                $scope.activity_name = resp.data.activity_name;
                $scope.assigned_dtl = resp.data.assigned_dtl;
                $scope.getapproval_remarks = resp.data.getapproval_remarks;
                $scope.approval_token = resp.data.approval_token;
                $scope.servicerequest_gid = resp.data.servicerequest_gid;
                $scope.approval_type = resp.data.approval_type;
                $scope.hierarylevel = resp.data.hierary_level;
                $scope.approvalreq_by = resp.data.approvalreq_by;
                $scope.approvalreqdate = resp.data.approvalreqdate;
                $scope.created_by = resp.data.created_by;
                $scope.created_date = resp.data.created_date;
                $scope.requestapproval_remarks = resp.data.requestapproval_remarks;
                $scope.request_description = resp.data.request_description;
                var param = {
                    servicerequest_gid : $scope.servicerequest_gid  
                }
                var url = "api/OsdTrnMyTicket/GetAllotted360";
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.alloteddocumentdtl = resp.data.alloteddocumentdtl;
                    //$scope.lblfilename = resp.data.filename;
                    //$scope.lblfilepath = resp.data.filepath;
                    $scope.lblallotfilename = resp.data.allofilename;
                    $scope.lblallotfilepath = resp.data.allofilepath;
                });
                unlockUI();
            
            });

            var url = "api/OsdTrnMyTicket/GetApprovalDtlsByToken"
            SocketService.getparams(url, param).then(function (resp) {
                $scope.approvaldetails = resp.data.approvaldetails;
                unlockUI();
            });
            

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

        //$scope.recproof_downloads = function (val1, val2) {
        //    DownloaddocumentService.Downloaddocument(val1, val2);
        //}
        $scope.downloadall = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.approve_submit = function () {

            var hierarylevel = $scope.hierarylevel;
            var level = ++hierarylevel;
            var params = {
                approval_remarks: $scope.txtremarks,
                approval_token: $scope.approval_token,
                hierary_level: level,
                servicerequest_gid: $scope.servicerequest_gid,
                approval_type: $scope.approval_type,
            }
            lockUI();
            var url = "api/OsdTrnRequestApproval/PostRequestApproved";
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.myApproval');
                    activate();
                    unlockUI();
                    $scope.showapproval = false;
                    $scope.hideapproval = false;
                }
                else {

                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.myApproval');
                    activate();
                    unlockUI();

                }
            });
        }

        $scope.reject_submit = function () {
            var hierarylevel = $scope.hierarylevel;
            var level = ++hierarylevel;
            var params = {
                approval_remarks: $scope.txtremarks,
                approval_token: $scope.approval_token,
                hierary_level: level,
                servicerequest_gid: $scope.servicerequest_gid,
                approval_type: $scope.approval_type,
            }
            lockUI();
            var url = "api/OsdTrnRequestApproval/PostRequestRejected";
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });

                    $state.go('app.myApproval');
                    activate();
                    unlockUI();

                }
                else {

                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();

                }
            });
        }

        $scope.back = function () {
            $state.go('app.myApproval');
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
