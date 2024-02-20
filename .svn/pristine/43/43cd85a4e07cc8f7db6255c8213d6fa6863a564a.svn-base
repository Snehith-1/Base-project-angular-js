(function () {
    'use strict';

    angular
        .module('angle')
        .controller('cacApprovalcontroller', cacApprovalcontroller);

    cacApprovalcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$cookies', '$filter', 'DownloaddocumentService'];

    function cacApprovalcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $cookies, $filter, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'cacApprovalcontroller';

        activate();

        function activate() {
            $scope.release_gid = localStorage.getItem('release_gid');
            var params = {
                release_gid: $scope.release_gid
            };
            var url = 'api/myApprovals/releasedetails';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.releaseapprovaldetails = resp.data;
                $scope.releaseissue_list = resp.data.releaseissue_list;
                $scope.uatlog_list = resp.data.uatlog_list;
                $scope.dependency_list = resp.data.dependency_list;
                $scope.cab_list = resp.data.cab_list;
                $scope.uatdocument_list = resp.data.uatdocument_list;
                $scope.approvaldoc_list = resp.data.ApprovalDocuments_List;
               
              
            });
        }

        // View UAT- Details..//

        $scope.uatdetails = function (issuetracker_gid, id) {
            var params = {
                issuetracker_gid: issuetracker_gid
            };
            var url = 'api/myApprovals/uatdetails';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.releaseissue_list[id][issuetracker_gid] = resp.data.uatlog_list;
            });
        }


        // CAB Approve & Reject .....//
      

        $scope.btn_cabapprove = function (cacapproval_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/getCACApprovalmodal.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
              
                $scope.ok = function () {
                    modalInstance.close('closed');
                };

                $scope.getcacapprovalclick = function () {
                    var params = {
                        cacapproval_gid: cacapproval_gid,
                        approval_remarks: $scope.approval_remarks
                            }
                            var url = 'api/myApprovals/cabapprove';
                            SocketService.post(url, params).then(function (resp) {
                                if (resp.data.status = true) {
                                    Notify.alert(resp.data.message, {
                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                         
                                    });
                                    modalInstance.close('closed');
                                }
                                else {
                                    Notify.alert(resp.data.message, {
                                        status: 'warning',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    modalInstance.close('closed');
                                }
                                $state.go('app.myApproval');
                            });
                          
                }
            }
        }

        $scope.btn_cabreject = function (cacapproval_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/getCACRejectmodal.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    modalInstance.close('closed');
                };

                $scope.getcacrejectclick = function () {
                    var params = {
                        cacapproval_gid: cacapproval_gid,
                        reject_remarks: $scope.reject_remarks
                            }
                            var url ='api/myApprovals/cabreject';
                            SocketService.post(url, params).then(function (resp) {
                                if (resp.data.status = true) {
                                    //$scope.close('view_cabdetails');
                                    Notify.alert(resp.data.message, {
                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                        
                                    });
                                  
                                    modalInstance.close('closed');
                                }
                                else {
                                    Notify.alert(resp.data.message, {
                                        status: 'warning',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    modalInstance.close('closed');
                                }
                                $scope.cabapproval = resp.data.cabapproval_list;
                               
                                $state.go('app.myApproval');
                            });
                           
                }
            }
        }

        //$scope.btn_cabapprove = function (cacapproval_gid) {
        //    var params = {
        //        cacapproval_gid: cacapproval_gid
        //    }
        //    var url = 'api/myApprovals/cabapprove';
        //    SocketService.post(url, params).then(function (resp) {
        //        if (resp.data.status = true) {
        //            Notify.alert(resp.data.message, {
        //                status: 'success',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
        //        }
        //        else {
        //            Notify.alert(resp.data.message, {
        //                status: 'warning',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
        //        }
        //        $state.go('app.myApproval');
        //    });
        //}

        //$scope.btn_cabreject = function (cacapproval_gid) {
        //    var params = {
        //        cacapproval_gid: cacapproval_gid
        //    }
        //    var url ='api/myApprovals/cabreject';
        //    SocketService.post(url, params).then(function (resp) {
        //        if (resp.data.status = true) {
        //            $scope.close('view_cabdetails');
        //            Notify.alert(resp.data.message, {
        //                status: 'success',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
        //        }
        //        else {
        //            Notify.alert(resp.data.message, {
        //                status: 'warning',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
        //        }
        //        $scope.cabapproval = resp.data.cabapproval_list;
        //        $state.go('app.myApproval');
        //    });
        //}


        // Download Document

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        //$scope.downloads = function (val1, val2) {

        //    var phyPath = val1;
        //    var relPath = phyPath.split("StoryboardAPI");
        //    var relpath1 = relPath[1].replace("\\", "/");
        //    var hosts = window.location.host;
        //    var prefix = location.protocol + "//";
        //    var str = prefix.concat(hosts, relpath1);
        //    var link = document.createElement("a");
        //    var name = val2.split(".")
        //    link.download = name[0];
        //    var uri = str;
        //    link.href = uri;
        //    link.click();
        //}

        $scope.back = function () {
            $state.go('app.myApproval');
        }
    }
})();
