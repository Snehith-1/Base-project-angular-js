(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSAOnboardingBussDevtIndividualRejectedSummaryController', MstSAOnboardingBussDevtIndividualRejectedSummaryController);

    MstSAOnboardingBussDevtIndividualRejectedSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstSAOnboardingBussDevtIndividualRejectedSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSAOnboardingBussDevtIndividualRejectedSummaryController';
        var lspage = 'Individual'
        activate();

        function activate() {
            var url = 'api/MstSAOnboardingBussDevtVerification/GetIndividualRejectedSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.saOnboardSummary_list = resp.data.saOnboardSummary_list;
                unlockUI();
            });
            var url = 'api/MstSAOnboardingBussDevtVerification/SaonboardingBDVerificationRejectedCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.pending_Inst_count = resp.data.institution_count;
                $scope.pending_Indi_count = resp.data.individual_count;

            });

        }
        /*   $scope.addsaonboarding = function () {
   
               $state.go('app.MstSAOnboardingAdd');
           }*/
        $scope.view = function (sacontact_gid) {
            $location.url('app/MstSAOnboardingIndividualBDView?hash=' + cmnfunctionService.encryptURL('lssacontact_gid=' + sacontact_gid));
        }
        $scope.editIndividual = function (sacontact_gid) {
            $location.url('app/MstSAonboardingSBAindividualverification?lssacontact_gid=' + sacontact_gid);
        }
        $scope.verifyIndividual = function (sacontact_gid) {
            $location.url('app/MstSAonboardingSBAVerifyindividual?lssacontact_gid=' + sacontact_gid);
        }
        $scope.Institution = function () {
            $state.go('app.MstSAOnboardingBussDevRejectedSummary');
        }
        $scope.bd_PendingwithRM = function () {
            $state.go('app.MstSAOnboardingPendingwithRMSummary');
        }
        $scope.bd_pendingwithCAD = function () {
            $state.go('app.MstSAOnboardingPendingwithCADSummary');
        }
        // Tagged Request
        $scope.Individual = function () {
            $state.go('app.MstSAOnboardingBussDevtVerificationIndividualSummary');
        }

        $scope.bd_verificationinstitution = function () {
            $state.go('app.MstSAOnboardingBussDevtVerificationSummary');
        }

        $scope.bd_verificationindividual = function () {
            $state.go('app.MstSAOnboardingBDVerificationPending');
        }
        $scope.bd_institution = function () {
            $state.go('app.MstSAOnboardingBDVerificationPending');
        }
        $scope.bd_verificationpending1 = function () {
            $state.go('app.MstSAOnboardingBussDevtVerificationIndividualPendingBDSummary');
        }
        $scope.bd_individual = function () {
            $state.go('app.MstSAOnboardingBDVerificationindividualPending');
        }
        $scope.viewIndividual = function (sacontact_gid) {
            $location.url('app/MstSAOnboardingBussDevtIndividualRejectedView?hash=' + cmnfunctionService.encryptURL('lssacontact_gid=' + sacontact_gid));
        }
        $scope.bd_verificationrejected = function () {
            $state.go('app.MstSAOnboardingBussDevRejectedSummary');
        }
        $scope.bd_verificationindividualrejected = function () {
            $state.go('app.MstSAOnboardingBussDevIndividualRejectedSummary');
        }
        $scope.RejectedIndividual = function () {
            $state.go('app.MstSAOnboardingBussDevIndividualRejectedSummary');
        }
        $scope.RejectedInstitution = function () {
            $state.go('app.MstSAOnboardingBussDevRejectedSummary');
        }
        $scope.bd_verificationdeferred = function () {
            $state.go('app.MstSABussDevInstitutiondeferredSummary');
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
                $scope.editIndividual = function (val) {
                    localStorage.setItem('sacontact_gid', val);
                    $state.go('app.MstSAOnboardingIndividualEdit');
                }
            }
        }

        $scope.delete = function (sacontact_gid) {
            var params = {
                sacontact_gid: sacontact_gid
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
                    var url = 'api/MstSAOnboardingIndividual/DeleteIndividual';
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
                        sacontact_gid: sacontact_gid

                    }

                    var url = 'api/MstSAOnboardingBussDevtVerification/IndividualRMTaggedUpdate';
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
                    $scope.rmtaggedassign_list = resp.data.taggedinstitution_list;
                    unlockUI();
                });
            }
        }



    }
})();

