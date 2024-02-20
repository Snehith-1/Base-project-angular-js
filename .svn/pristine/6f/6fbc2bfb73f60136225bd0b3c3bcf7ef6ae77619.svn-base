(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstRMWaiverApprovalViewController', MstRMWaiverApprovalViewController);

    MstRMWaiverApprovalViewController.$inject = ['DownloaddocumentService','$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstRMWaiverApprovalViewController(DownloaddocumentService,$rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstRMWaiverApprovalViewController';
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;        
        $scope.rmpostccwaiver_gid = $location.search().rmpostccwaiver_gid;
        var rmpostccwaiver_gid = $scope.rmpostccwaiver_gid;
       
        activate();

        function activate() {
            var params = {
                rmpostccwaiver_gid: rmpostccwaiver_gid
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
                $scope.sanctionwaivergen_list = resp.data.sanctionwaivergen_list;
                $scope.lanwaivergen_list = resp.data.lanwaivergen_list;
                $scope.waivergroupgen_list = resp.data.waivergroupgen_list;

                var sanctionwaiver_namelist = '';
                for(var i = 0;i < $scope.sanctionwaivergen_list.length; i++) {
                    sanctionwaiver_namelist = sanctionwaiver_namelist + $scope.sanctionwaivergen_list[i].sanctionwaiver_name + ',';
                }
                sanctionwaiver_namelist = sanctionwaiver_namelist.substring(0, sanctionwaiver_namelist.length - 1);
                $scope.sanctionwaiver_namelist = sanctionwaiver_namelist;

                var lanwaiver_namelist = '';
                for(var i = 0;i < $scope.lanwaivergen_list.length; i++) {
                    lanwaiver_namelist = lanwaiver_namelist + $scope.lanwaivergen_list[i].lanwaiver_name + ',';
                }
                lanwaiver_namelist = lanwaiver_namelist.substring(0, lanwaiver_namelist.length - 1);
                $scope.lanwaiver_namelist = lanwaiver_namelist;

                var waivergroup_namelist = '';
                for(var i = 0;i < $scope.waivergroupgen_list.length; i++) {
                    waivergroup_namelist = waivergroup_namelist + $scope.waivergroupgen_list[i].groupwaiver_name + ',';
                }
                waivergroup_namelist = waivergroup_namelist.substring(0, waivergroup_namelist.length - 1);
                $scope.waivergroup_namelist = waivergroup_namelist;

             

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

            var url = 'api/MstRMPostCCWaiver/WaiverApprovalDetailList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.waiverapprovaldetail_list = resp.data.waiverapprovaldetail_list;
            });

            var paramsapp = {
                application_gid: application_gid,
                rmpostccwaiver_gid: rmpostccwaiver_gid
            }

            var url = 'api/MstRMPostCCWaiver/WaiverApprovalHistoryList';
            SocketService.post(url, paramsapp).then(function (resp) {
                $scope.waiverapprovalhistory_list = resp.data.rmpostccwaiver_list;
            });

        }

        $scope.Back = function () {
            $state.go('app.myApproval');
        }

        

        // Get Request Remarks
        $scope.approval_remarks = function (approval_remarks) {
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

                $scope.approval_remarks = approval_remarks;

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.descdoc_view = function (rmpostccwaiver_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/waiverdescdoc_view.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                  {
                    rmpostccwaiver_gid: rmpostccwaiver_gid
                  }
                var url = 'api/MstRMPostCCWaiver/GetDescDocAppPersons';
                lockUI();

                SocketService.getparams(url, params).then(function (resp) {
                   unlockUI();
                   $scope.lblwaiver_description = resp.data.waiver_description;
                   $scope.uploadwaiverdoc_list = resp.data.uploadwaiverdoc_list;
                   $scope.waiverapprovalmember_list = resp.data.waiverapprovalmember_list;
                    
                });  

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

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.approve_submit = function () {          
            var params = {
                rmpostccwaiver_gid: $scope.rmpostccwaiver_gid,
                approval_remarks: $scope.txtapproval_remarks,
            }
            lockUI();
            var url = "api/MstRMPostCCWaiver/PostWaiverApproved";
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
            var params = {
                rmpostccwaiver_gid: $scope.rmpostccwaiver_gid,
                approval_remarks: $scope.txtapproval_remarks,
            }
            lockUI();
            var url = "api/MstRMPostCCWaiver/PostWaiverRejected";
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
        $scope.doc_downloads = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
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

    }
})();
