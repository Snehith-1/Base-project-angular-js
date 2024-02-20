(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstLSAApprovalCompletedcontroller', MstLSAApprovalCompletedcontroller);

    MstLSAApprovalCompletedcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function MstLSAApprovalCompletedcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstLSAApprovalCompletedcontroller';

        activate();

        function activate() {
            lockUI();
            var url = "api/MstLSA/GetLSACompletedSummary";
            SocketService.get(url).then(function (resp) {
                $scope.LSAApproverSummary = resp.data.MdlLSAApproverSummary;
            });
            var url = 'api/MstLSA/CADLSASummaryCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.MakerPendingCount = resp.data.MakerPendingCount;
                $scope.MakerFollowUpCount = resp.data.MakerFollowUpCount;
                $scope.CheckerPendingCount = resp.data.CheckerPendingCount;
                $scope.CheckerFollowUpCount = resp.data.CheckerFollowUpCount;
                $scope.ApproverPendingCount = resp.data.ApproverPendingCount;
                $scope.CompletedCount = resp.data.CompletedCount;
            });
        }

        $scope.LSACompletedSummary = function () {
            lockUI();
            var url = "api/MstLSA/GetLSACompletedSummary";
            SocketService.get(url).then(function (resp) {
                $scope.LSAApproverSummary = resp.data.MdlLSAApproverSummary;
            });
            var url = 'api/MstLSA/CADLSASummaryCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.MakerPendingCount = resp.data.MakerPendingCount;
                $scope.MakerFollowUpCount = resp.data.MakerFollowUpCount;
                $scope.CheckerPendingCount = resp.data.CheckerPendingCount;
                $scope.CheckerFollowUpCount = resp.data.CheckerFollowUpCount;
                $scope.ApproverPendingCount = resp.data.ApproverPendingCount;
                $scope.CompletedCount = resp.data.CompletedCount;
            });
        }

        $scope.ReinitiateEligible = function () {
            lockUI();
            var url = "api/MstLSA/GetLSAReinitiateEligible";
            SocketService.get(url).then(function (resp) {
                $scope.LSAApproverSummary = resp.data.MdlLSAApproverSummary;
            });
            var url = 'api/MstLSA/CADLSASummaryCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.MakerPendingCount = resp.data.MakerPendingCount;
                $scope.MakerFollowUpCount = resp.data.MakerFollowUpCount;
                $scope.CheckerPendingCount = resp.data.CheckerPendingCount;
                $scope.CheckerFollowUpCount = resp.data.CheckerFollowUpCount;
                $scope.ApproverPendingCount = resp.data.ApproverPendingCount;
                $scope.CompletedCount = resp.data.CompletedCount;
            });
        }

        $scope.maker = function () {
            $location.url('app/MstCadLSASummary');
        }

        $scope.checker = function () {
            $location.url('app/MstCadLSACheckerSummary');
        }

        $scope.approval = function () {
            $location.url('app/MstCadLSAApprovalSummary');
        }

        $scope.view = function (val) {
            $location.url('app/MstCadApplicationView?application_gid=' + val + '&lspage=CadLsaCompleted');
        }

        $scope.LSApdf = function (lsgeneratelsa_gid) {
            lockUI();
            var params = {
                generatelsa_gid: lsgeneratelsa_gid
            }
            var url = 'api/MstLSA/GetLSApdf'; 
            SocketService.getparams(url, params).then(function (resp) {

                if (resp.data.status == true) {
                    var filepath = resp.data.file_path;
                    var filename = resp.data.file_name;
                    DownloaddocumentService.Downloaddocument(filepath, filename);
                        Notify.alert('LSA Report Downloaded Successfully', 'success')
                        unlockUI();
                    }
                    else {
                        unlockUI();
                        Notify.alert('Error Occurred While Export PDF !', 'warning');
                    }

            });

        }

        $scope.maker_process = function (val, val1, followup) {
            $location.url('app/MstCadLSADtlSummary?application_gid=' + val + '&application2sanction_gid=' + val1 + '&lspage=CadLsaCompleted&lsfollowup=' + followup);
        }

        $scope.reinitiate_lsa = function (generatelsa_gid) {

            var modalInstance = $modal.open({
                templateUrl: '/closedContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {


                var params = {
                    generatelsa_gid: generatelsa_gid
                }

                $scope.reinitiate_lsa = function () {
                    var params = {
                        generatelsa_gid: generatelsa_gid,
                        reinitatelsa_remarks: $scope.reinitatelsa_remarks
                    }

                    var url = "api/MstLSA/SubmitLSAReinitiate";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'success');
                            activate();
                        }
                        else {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'warning');
                            activate();
                        }
                    });


                }

                $scope.close = function () {
                    modalInstance.close('closed');
                };
            }
        }
    }
})();
