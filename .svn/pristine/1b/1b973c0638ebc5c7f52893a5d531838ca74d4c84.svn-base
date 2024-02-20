(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnRMDeferralCloseQueryController', AgrTrnRMDeferralCloseQueryController);

    AgrTrnRMDeferralCloseQueryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function AgrTrnRMDeferralCloseQueryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnRMDeferralCloseQueryController';
        var lspage = $location.search().lspage;
        var application_gid = $location.search().application_gid;
        var credit_gid = $location.search().credit_gid;
        var lstype = $location.search().lstype;
        var lsdocumentcheckdtl_gid = $location.search().lsdocumentcheckdtl_gid

        activate();
        lockUI();
        function activate() {
            var param = {
                application_gid: application_gid
            };

            var url = 'api/AgrMstApplicationApproval/Getapplicationdetails';
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.lblapplref_no = resp.data.application_no;
                $scope.lblapplicant_name = resp.data.customer_name;
                $scope.lblcurrentappl_stage = resp.data.approval_status;
                $scope.lblappl_state = resp.data.region;
                $scope.lbloveralllimit_request = resp.data.overalllimit_amount;
                $scope.productlist = resp.data.productlist;
            });

            var param = {
                documentcheckdtl_gid: lsdocumentcheckdtl_gid
            };
            var url = 'api/AgrMstScannedDocument/GetAppcadQuerySummary';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.query_list = resp.data.mdlcadquery;
                $scope.lbldocumentcode = resp.data.documenttype_code;
                $scope.lbldocumenttype_name = resp.data.documenttype_name;
            }); 
            var url = 'api/AgrTrnPhysicalDocument/GetPhysicalAppcadQuerySummary';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.physicalquery_list = resp.data.mdlcadquery; 
            }); 
        }

        $scope.query_close = function (tagquery_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/queryClose.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {
                    var params = {
                        tagquery_gid: tagquery_gid, 
                        query_responseremarks: $scope.txtcloseremarks,
                        documentcheckdtl_gid: lsdocumentcheckdtl_gid
                    }
                    var url = 'api/AgrMstScannedDocument/PostAppcadresponsequery';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        } 
                    }); 
                    $modalInstance.close('closed');
                }

            }
        }

        $scope.view_querydesc = function (query_description, query_responseremarks) {
            var modalInstance = $modal.open({
                templateUrl: '/queryDescriptionView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.lblquery_desc = query_description;
                $scope.lblquery_responseremarks = query_responseremarks;
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.defferaldoc_view = function (tagquery_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/document_view.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    tagquery_gid: tagquery_gid
                }
                var url = 'api/AgrMstScannedDocument/GetQueryDocument';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.queryuploaddocument = resp.data.queryuploaddocument;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.download_doc1 = function (val1, val2) {
                    // var phyPath = val1;
                    // var relPath = phyPath.split("StoryboardAPI");
                    // var relpath1 = relPath[1].replace("\\", "/");
                    // var hosts = window.location.host;
                    // var prefix = location.protocol + "//";
                    // var str = prefix.concat(hosts, relpath1);
                    // var link = document.createElement("a");
                    // link.download = val2;
                    // var uri = str;
                    // link.href = uri;
                    // link.click();
                    DownloaddocumentService.Downloaddocument(val1, val2);

                }
                $scope.downloadall = function () {
                    for (var i = 0; i < $scope.queryuploaddocument.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.queryuploaddocument[i].file_path, $scope.queryuploaddocument[i].file_name);
                    }
                }
            }

        }

        $scope.Back = function () {
            $location.url('app/AgrTrnRMDeferralDtlsView?application_gid=' + application_gid + '&credit_gid=' + credit_gid + '&lspage=' + lspage + '&lstype=' + lstype);
        }

        // $scope.downloadall = function () {
        //     for (var i = 0; i < $scope.queryuploaddocument.length; i++) {
        //         DownloaddocumentService.Downloaddocument($scope.queryuploaddocument[i].file_path, $scope.queryuploaddocument[i].file_name);
        //     }
        // }

    }
})();
