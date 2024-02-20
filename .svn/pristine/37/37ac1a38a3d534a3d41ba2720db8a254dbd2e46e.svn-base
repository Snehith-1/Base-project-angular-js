(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSAVerificationMappingPendingController', MstSAVerificationMappingPendingController);

    MstSAVerificationMappingPendingController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$filter', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstSAVerificationMappingPendingController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $filter, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSAVerificationMappingPendingController';


        activate();

        function activate() {
            var url = 'api/MstSAOnboardingInstitution/GetSAVerfiyinstitutionMappingPendingSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.saOnboardSummary_list = resp.data.saOnboardSummary_list;
                unlockUI();
            });
            var url = 'api/MstSAOnboardingInstitution/GetSaPendingAssignmentCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.institution_count = resp.data.institution_count;
                $scope.individual_count = resp.data.individual_count;

            });
        }
        $scope.verification_pending = function () {
            $location.url('app/MstSAVerificationPending')
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

        $scope.verification_individual = function () {
            $location.url('app/MstSAVerificationIndividualPending')
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
        $scope.saverificationedit = function (sacontactinstitution_gid) {

            // $location.url('app/MstSAOnboardingInstitutionVerification');

            $location.url('app/MstSAVerificationPendingInstitutionView?hash=' + cmnfunctionService.encryptURL('lssacontactinstitution_gid=' + sacontactinstitution_gid));
        }
        $scope.saonboardingmapping = function (sacontactinstitution_gid) {
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
                        sacontactinstitution_gid: sacontactinstitution_gid

                    }

                    var url = 'api/MstSAOnboardingInstitution/CreateSAMapping';
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

        $scope.mappinginformation = function (sacontactinstitution_gid) {
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
                    sacontactinstitution_gid: sacontactinstitution_gid
                }
                var url = 'api/MstSAOnboardingInstitution/GetAssignedInformation';
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


