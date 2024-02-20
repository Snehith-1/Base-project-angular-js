(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSAOnboardingInstitutionGroupingController', MstSAOnboardingInstitutionGroupingController);

    MstSAOnboardingInstitutionGroupingController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstSAOnboardingInstitutionGroupingController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSAOnboardingInstitutionGroupingController';

        activate();
        lockUI();
        function activate() {
            var url = 'api/MstSAOnboardingInstitution/GetSaInstitutionGroupingSummary';
            SocketService.get(url).then(function (resp) {
                $scope.saOnboardSummary_list = resp.data.saOnboardSummary_list;
                unlockUI();
            });
            var url = 'api/MstSAOnboardingBussDevtVerification/GetIndividualOnboardingCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.pending_Inst_count = resp.data.institution_count;
                $scope.pending_Indi_count = resp.data.individual_count;
                $scope.institution_rejectedcount = resp.data.institution_rejectedcount;
                $scope.individual_rejectedcount = resp.data.individual_rejectedcount;
                $scope.institution_groupingcount = resp.data.institution_groupingcount;
                $scope.individual_groupingcount = resp.data.individual_groupingcount;

            });
        }

        $scope.institution_pending = function () {
            $state.go('app.MstSAOnboardingSummary');
        }
        $scope.institution_rejected = function () {
            $state.go('app.MstSAOnboardingRejectedSummary');
        }
        $scope.institution_grouping = function () {
            $state.go('app.MstSAOnboardingInstitutionGroupingSummary');
        }

        $scope.individual_pending = function () {
            $state.go('app.MstSAOnboardingIndividualSummary');
        }
        $scope.individual_rejected = function () {
            $state.go('app.MstSAOnboardingIndividualRejectedSummary');
        }
        $scope.individual_grouping = function () {
            $state.go('app.MstSAOnboardingIndividualGrouping');
        }

       
        $scope.viewIndividual = function (samfin_code,samagro_code,sacontactinstitution_gid) {
            $location.url('app/MstSAOnboardingInstitutionRenewalGrouping?hash=' + cmnfunctionService.encryptURL('samfin_code=' + samfin_code + '&samagro_code=' + samagro_code + '&lssacontactinstitution_gid=' + sacontactinstitution_gid));
        }
       
        $scope.addsaonboarding = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addsaonboarding.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.submit = function () {
                    if ($scope.rbocompany == "Y") {
                        $state.go('app.MstSAOnboardingAdd');
                    }
                    else if ($scope.rbocompany == "N") {
                        $state.go('app.MstaddSAOnboarding');
                    }
                    $modalInstance.close('closed');
                };
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.addcompany = function () {
                    $location.url('app/MstSAOnboardingAddInstitution');
                    $modalInstance.close('closed');
                }
                $scope.addindividual = function () {
                    $state.go('app.MstaddSAOnboarding');
                    $modalInstance.close('closed');
                }
                // $scope.editIndividual = function (val) {
                //     localStorage.setItem('sacontact_gid', val);
                //     $state.go('app.MstSAOnboardingIndividualEdit');
                // }
            }
        }

        $scope.delete = function (sacontactinstitution_gid) {
            var params = {
                sacontactinstitution_gid: sacontactinstitution_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = 'api/MstSAOnboardingInstitution/DeleteInstitution';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Constitution!', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        };

    }
})();