(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSAOnboardingBDVerificationPendingController', MstSAOnboardingBDVerificationPendingController);

    MstSAOnboardingBDVerificationPendingController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstSAOnboardingBDVerificationPendingController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSAOnboardingBDVerificationPendingController';
        


        activate();
        lockUI();

        function activate() {
            //  var url = 'api/MstSAOnboardingBussDevtVerification/GetSBAInstitutionSummary';
            var url = 'api/MstSAOnboardingBussDevtVerification/GetSaInstitutionCompletedSummary';
            SocketService.get(url).then(function (resp) {
                //$scope.Institution_summary = resp.data.Institution_summary;
                //unlockUI();
                var update = resp.data.update_flag;
                $scope.saOnboardSummary_list = resp.data.saOnboardSummary_list;
                unlockUI();
            });
            var url = 'api/MstSAOnboardingBussDevtVerification/GetSaonboardingBDVerificationCompletedCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.institution_count = resp.data.institution_count;
                $scope.individual_count = resp.data.individual_count;

            });
        }
        //$scope.editInstitution = function (sacontactinstitution_gid) {
        //    $location.url('app/MstSAOnboardingSBAVerification?lssacontactinstitution_gid=' + sacontactinstitution_gid);
        //}

        $scope.editIndividual = function (sacontactinstitution_gid) {
            $location.url('app/MstSAonboardingSBAinstitutionverification?hash=' + cmnfunctionService.encryptURL('lssacontactinstitution_gid=' + sacontactinstitution_gid));
        }
        $scope.verifyindividualupdate = function (sacontactinstitution_gid) {
            $location.url('app/MstSAOnboardingBDVerificationTrainingStatus?hash=' + cmnfunctionService.encryptURL('lssacontactinstitution_gid=' + sacontactinstitution_gid));
        }
        $scope.verifyIndividual = function (sacontactinstitution_gid) {
            $location.url('app/MstSAOnboardingInstitutionBDView?hash=' + cmnfunctionService.encryptURL('lssacontactinstitution_gid=' + sacontactinstitution_gid));
        }
       
        $scope.bd_PendingwithRM = function () {
            $state.go('app.MstSAOnboardingPendingwithRMSummary');
        }
        $scope.bd_pendingwithCAD = function () {
            $state.go('app.MstSAOnboardingPendingwithCADSummary');
        }
        // Tagged Request
        $scope.Institution = function () {
            $state.go('app.MstSAOnboardingBDVerificationPending');
        }
        $scope.Individual = function () {
            $state.go('app.MstSAOnboardingBussDevtVerificationIndividualSummary');
        }
        $scope.bd_verificationinstitution = function () {
            $state.go('app.MstSAOnboardingBussDevtVerificationSummary');
        }
        $scope.bd_verificationdeferred = function () {
            $state.go('app.MstSABussDevtInstitutionDeferredSummary');
        }

        $scope.bd_verificationindividual = function () {
            $state.go('app.MstSAOnboardingBDVerificationPending');
        }
        
        $scope.bd_verificationpending = function () {
            $state.go('app.MstSAOnboardingBussDevtVerificationInstitutionPendingBDSummary');
        }
       
        $scope.bd_verificationrejected = function () {
            $state.go('app.MstSAOnboardingBussDevRejectedSummary');
        }

        $scope.bd_individual = function () {
            $state.go('app.MstSAOnboardingBDVerificationIndividualPending');
        }
        
        $scope.rmtaggedemployee = function (sacontactinstitution_gid) {
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

                var url = 'api/MstSAOnboardingBussDevtVerification/GetEmployeelist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list1 = resp.data.rmemployeelists;
                    unlockUI();
                });

                $scope.submit = function () {

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
                        tagged_remarks: $scope.txtremarks,
                        sacontactinstitution_gid: sacontactinstitution_gid

                    }

                    var url = 'api/MstSAOnboardingBussDevtVerification/InstitutionRMTaggedUpdate';
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
                    sacontactinstitution_gid: sacontactinstitution_gid
                }

                var url = 'api/MstSAOnboardingBussDevtVerification/InstitutionRMTaggedLog';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.rmtaggedassign_list = resp.data.taggedinstitution_list;
                    unlockUI();
                });
            }
        }

    }

})();
