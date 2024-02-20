(function () {
    'use strict';

    angular
        .module('angle')
        .controller('extensionApprovalcontroller', extensionApprovalcontroller);

    extensionApprovalcontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$filter', 'ngTableParams', '$resource', '$timeout', 'DownloaddocumentService'];
    function extensionApprovalcontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $filter, ngTableParams, $resource, $timeout, DownloaddocumentService) {
       // var vm = this;
        //   vm.title = 'extensionApprovalcontroller';

        activate();
        function activate() {
            var url = apiManage.apiList['extensionSummary'].api;

            SocketService.get(url).then(function (resp) {
                $scope.cadApproval = resp.data.deferralSummaryDtls;

                //$scope.usersTable = new ngTableParams({
                //    page: 1,
                //    count: 5

                //}, {
                //    total: $scope.cadApprovaldata.length,
                //    getData: function ($defer, params) {
                //        $scope.cadApproval = $scope.cadApprovaldata.slice((params.page() - 1) * params.count(), params.page() * params.count());
                //        $defer.resolve($scope.cadApproval);
                //    }
                //});
            });
        };


        /////////////////////////


        $scope.popupapproveExtension = function (deferral_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/extensionDetails.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.deferral_gid = deferral_gid;
                $scope.deferral_gid = localStorage.setItem('deferral_gid', deferral_gid);
                var params = {
                    deferral_gid: deferral_gid
                }

                var url = apiManage.apiList['GetDetails'].api;
            
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {                     
                        $scope.UploadDocumentname = resp.data;
                        $scope.deferral_gid = resp.data.deferral_gid;
                        $scope.deferralextension_gid = resp.data.deferralextension_gid;
                        $scope.loanref_no = resp.data.loanref_no;
                        $scope.loan_title = resp.data.loan_title;
                        $scope.record_id = resp.data.record_id;
                        $scope.deferral_name = resp.data.deferral_name;
                        $scope.filename_list = resp.data.filename_list;
                        $scope.extensionapplied_remarks = resp.data.extensionapplied_remarks;
                        //document.getElementById("tabledocuments").style.visibility = "visible";
                    }
                    else if (resp.data.status == false) {
                        //unlockUI();
                        //$scope.UploadDocumentname = resp.data;
                        //$scope.deferral_gid = resp.data.deferral_gid;
                        //$scope.deferralextension_gid = resp.data.deferralextension_gid;
                        //$scope.loanref_no = resp.data.loanref_no;
                        //$scope.loan_title = resp.data.loan_title;
                        //$scope.deferral_id = resp.data.deferral_id;
                        //$scope.deferral_name = resp.data.deferral_name;
                        //$scope.extensionapplied_remarks = resp.data.extensionapplied_remarks;
                        //document.getElementById("tabledocuments").style.visibility = "hidden";

                      
                      
                        Notify.alert('No Extension Request Raised!', {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                });
               
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.cancel = function () {
                    $modalInstance.dismiss('cancel');
                };

               
                $scope.downloads = function (val1, val2) {
                    //var phyPath = val1;
                    //var relPath = phyPath.split("StoryboardAPI");
                    //var relpath1 = relPath[1].replace("\\", "/");
                    //var hosts = window.location.host;
                    //var prefix = location.protocol + "//";
                    //var str = prefix.concat(hosts, relpath1);
                    //var link = document.createElement("a");
                    //var name = val2.split(".")
                    //link.download = val2;
                    //var uri = str;
                    //link.href = uri;
                    //link.click();

                    DownloaddocumentService.Downloaddocument(val1, val2);
                }
            }
        };


        ///////////////////////////
        // View Issue 
        //$scope.popupapproveExtension = function (deferral_gid) {
        //    var doc = document.getElementById('popupapproveExtension');

        //    doc.style.display = 'block';
        //    $scope.deferral_gid = deferral_gid;
        //    $scope.deferral_gid = localStorage.setItem('deferral_gid', deferral_gid);
        //    var params = {
        //        deferral_gid: deferral_gid
        //    }

        //    var url = apiManage.apiList['GetDetails'].api;
        //    lockUI();
        //    SocketService.getparams(url, params).then(function (resp) {
        //        if (resp.data.status == true) {
        //            unlockUI();
        //            $scope.UploadDocumentname = resp.data;
        //            $scope.deferral_gid = resp.data.deferral_gid;
        //            $scope.deferralextension_gid = resp.data.deferralextension_gid;
        //            $scope.loanref_no = resp.data.loanref_no;
        //            $scope.loan_title = resp.data.loan_title;
        //            $scope.deferral_id = resp.data.deferral_id;
        //            $scope.deferral_name = resp.data.deferral_name;
        //            $scope.filename_list = resp.data.filename_list;
        //            $scope.extensionapplied_remarks = resp.data.extensionapplied_remarks;
        //            document.getElementById("tabledocuments").style.visibility = "visible";
        //        }
        //        else if (resp.data.status == false) {
                   

        //            unlockUI();
        //            console.log(resp.data);
        //            doc.style.display = 'none';
        //            Notify.alert('No Extension Request Raised!', {
        //                status: 'warning',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });

        //        }
        //    });
        //};

        $scope.approveExtension = function () {
            var params = {
                deferral_gid: $scope.deferral_gid,
                deferralextension_gid: $scope.deferralextension_gid,
                extensionapproval_remarks: $scope.extensionapproval_remarks,
                due_date: $scope.due_date
            }

            var url = apiManage.apiList['approveExtension'].api;
            lockUI();
            SocketService.post(apiManage.apiList['approveExtension'].api, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.close('popupapproveExtension');
                    location.reload();
                    Notify.alert('Extension Approved Successfully..!!', {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert('Error While Approving Extension !', {
                        status: 'warning',
                        pos: 'top-right',
                        timeout: 3000
                    });

                }
            });
        };

        $scope.downloads = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("EMS");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = "http://"
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //var name = val2.split(".")
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        };

        $scope.close = function (val) {
            document.getElementById("userformextension").reset();
            var doc = document.getElementById(val);
            doc.style.display = 'none';
        };

    };
})();