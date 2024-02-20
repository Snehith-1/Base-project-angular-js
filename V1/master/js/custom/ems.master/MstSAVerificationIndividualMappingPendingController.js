(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSAVerificationIndividualMappingPendingController', MstSAVerificationIndividualMappingPendingController);

    MstSAVerificationIndividualMappingPendingController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstSAVerificationIndividualMappingPendingController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSAVerificationIndividualMappingPendingController';

        activate();

        function activate() {
            var url = 'api/MstSAOnboardingIndividual/GetSAVerfiyindividualMappingPendingSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.saOnboardSummary_list = resp.data.saOnboardSummary_list;
                unlockUI();
            });
            var url = 'api/MstSAOnboardingIndividual/GetSaPendingAssignmentCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.institution_count = resp.data.institution_count;
                $scope.individual_count = resp.data.individual_count;

            });
        }

        $scope.verification_pending = function () {
            $location.url('app/MstSAVerificationPending')
        }
        $scope.verification_institution = function () {
            $location.url('app/MstSAVerificationPending')
        }

        $scope.verification_approved = function () {
            $location.url('app/MstSAVerificationApprovedSummary')
        }

        $scope.verification_rejected = function () {
            $location.url('app/MstSAVerificationRejectedSummary')
        }

        $scope.verification_deferred = function () {
            $location.url('app/MstSAVerificationInstitutionDeferredSummary')
        }
        $scope.verification_individual = function () {
            $location.url('app/MstSAVerificationIndividualPending')
        }

        $scope.institution_pending = function () {
            $state.go('app.MstSAVerificationInstPendingSummary');
        }

        $scope.mapping_pending = function () {
            $location.url('app/MstSAVerificationMappingPending')
        }
        $scope.mapping_institution = function () {
            $location.url('app/MstSAVerificationMappingPending')
        }
        $scope.mapping_individual = function () {
            $location.url('app/MstSAVerificationIndividualMappingPending')
        }


        $scope.saonboardingverification = function (sacontact_gid) {

            //$location.url('app/MstSAOnboardingIndividualVerification');

            $location.url('app/MstSAVerificationPendingIndividualView?hash=' + cmnfunctionService.encryptURL('lssacontact_gid=' + sacontact_gid + '&lspage=IndividualPending'));
        }

        $scope.samapping = function (sacontact_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/addauditmapping.html',
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

                $scope.submit = function () {
                    var lsemployee_gid = '';
                    var lsemployee_name = '';
                    if ($scope.cboemployee != undefined || $scope.cboemployee != null) {
                        lsemployee_gid = $scope.cboemployee.employee_gid,
                        lsemployee_name = $scope.cboemployee.employee_name;
                    }
                    var lscheckeremployee_gid = '';
                    var lscheckeremployee_name = '';
                    if ($scope.cboemployeechecker != undefined || $scope.cboemployeechecker != null) {
                        lscheckeremployee_gid = $scope.cboemployeechecker.employee_gid,
                        lscheckeremployee_name = $scope.cboemployeechecker.employee_name;
                    }

                    var params = {
                        //employee_gid: $scope.employee_gid,
                        employee_gid: lsemployee_gid,
                        employee_name: lsemployee_name,
                        checkeremployee_gid: lscheckeremployee_gid,
                        checkeremployee_name: lscheckeremployee_name,
                        samapping_name: $scope.sa_mapping,
                        samapping_code: $scope.txtsamapping_code,
                        sacontact_gid: sacontact_gid

                    }

                    var url = 'api/MstSAOnboardingIndividual/CreateSAMapping';
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

            }
        }

        $scope.mappinginformation = function (sacontact_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/Approvalinformation.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    sacontact_gid: sacontact_gid
                }
                var url = 'api/MstSAOnboardingIndividual/GetAssignedInformation';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.lblmaker_name = resp.data.employee_name;
                    $scope.lblchecker_name = resp.data.checkeremployee_name;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

    }
})();
