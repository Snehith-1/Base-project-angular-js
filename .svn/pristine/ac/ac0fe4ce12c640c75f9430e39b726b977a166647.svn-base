(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSAOnboardingBussDevtVerificationIndividualPendingBDSummaryController', MstSAOnboardingBussDevtVerificationIndividualPendingBDSummaryController);

    MstSAOnboardingBussDevtVerificationIndividualPendingBDSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstSAOnboardingBussDevtVerificationIndividualPendingBDSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSAOnboardingBussDevtVerificationIndividualPendingBDSummaryController';

        activate();

        function activate() {
            var url = 'api/MstSAOnboardingBussDevtVerification/GetPendingBDVerification';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.saOnboardSummary_list = resp.data.saOnboardSummary_list;
                unlockUI();
            });
            var url = 'api/MstSAOnboardingBussDevtVerification/GetBDVerificationPendingCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.pending_Inst_count = resp.data.institution_count;
                $scope.pending_Indi_count = resp.data.individual_count;

            });
        }
        $scope.view = function (sacontact_gid) {
            $location.url('app/MstSAOnboardingPendingRMIndividualView?hash=' + cmnfunctionService.encryptURL('lssacontact_gid=' + sacontact_gid));
        }
        $scope.editIndividual = function (sacontact_gid) {
            $location.url('app/MstSAonboardingSBAindividualverification?hash=' + cmnfunctionService.encryptURL('lssacontact_gid=' + sacontact_gid));
        }
        $scope.verifyIndividual = function (sacontact_gid) {
            $location.url('app/MstSAonboardingSBAVerifyindividual?hash=' + cmnfunctionService.encryptURL('lssacontact_gid=' + sacontact_gid));
        }
        // Tagged Request
        $scope.Institution = function () {
            $state.go('app.MstSAOnboardingBussDevtVerificationInstitutionPendingBDSummary');
        }        
        $scope.Individual = function () {
            $state.go('app.MstSAOnboardingBussDevtVerificationIndividualPendingBDSummary');
        }

        $scope.bd_PendingwithRM = function () {
            $state.go('app.MstSAOnboardingPendingwithRMIndividualSummary');
        }
        $scope.bd_pendingwithCAD = function () {
            $state.go('app.MstSAOnboardingPendingwithCADIndividualSummary');
        }
        $scope.bd_verificationinstitution = function () {
            $state.go('app.MstSAOnboardingBussDevtVerificationIndividualSummary');
        }
        $scope.bd_verificationpending = function () {
            $state.go('app.MstSAOnboardingBussDevtVerificationIndividualPendingBDSummary');
        }

        $scope.bd_verificationindividual = function () {
            $state.go('app.MstSAOnboardingBDVerificationIndividualPending');
        }
      
        $scope.viewIndividual = function (sacontact_gid) {
            $location.url('app/MstSAOnboardingIndividualView?hash=' + cmnfunctionService.encryptURL('lssacontact_gid=' + sacontact_gid));
        }
        $scope.bd_verificationrejected = function () {
            $state.go('app.MstSAOnboardingBussDevRejectedSummary');
        }
        $scope.bd_verificationdeferred = function () {
            $state.go('app.MstSABussDevtInstitutionDeferredSummary');
        }
        $scope.rmtaggedemployee = function (sacontact_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/tagemployee.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                var url = 'api/SystemMaster/GetEmployeelist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employeelist;
                    unlockUI();
                });

                $scope.taggedsubmit = function () {

                    var lsemployee_gid = '';
                    var lsemployee_name = '';
                    if ($scope.cboemployee != undefined || $scope.cboemployee != null) {
                        lsemployee_gid = $scope.cboemployee.employee_gid,
                            lsemployee_name = $scope.cboemployee.employee_name;
                    }


                    var params = {
                        //employee_gid: $scope.employee_gid,
                        rm_tagging_id: lsemployee_gid,
                        rm_tagging_name: lsemployee_name,
                        sacontact_gid: sacontact_gid,
                        tagged_remarks: $scope.txtremarks


                    }

                    var url = 'api/MstSAOnboardingBussDevtVerification/IndividualVerificationPendingRMTaggedUpdate';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                    });

                    $modalInstance.close('closed');

                }
                var param = {
                    sacontact_gid: sacontact_gid
                }

                var url = 'api/MstSAOnboardingBussDevtVerification/IndividualRMTaggedLog';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.rmtaggedassign_list = resp.data.taggedindividual_list;
                    unlockUI();
                });
            }
        }
    }
})();
