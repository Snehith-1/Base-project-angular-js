(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSAOnboardingBDVerificationIndividualPendingController', MstSAOnboardingBDVerificationIndividualPendingController);

    MstSAOnboardingBDVerificationIndividualPendingController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstSAOnboardingBDVerificationIndividualPendingController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSAOnboardingBDVerificationIndividualPendingController';
        var lspage = 'Individual'
        activate();

        function activate() {
            var url = 'api/MstSAOnboardingBussDevtVerification/GetOnboardCompletedSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
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
        /*   $scope.addsaonboarding = function () {
   
               $state.go('app.MstSAOnboardingAdd');
           }*/

        $scope.editIndividual = function (sacontact_gid) {
            $location.url('app/MstSAonboardingSBAindividualverification?hash=' + cmnfunctionService.encryptURL('lssacontact_gid=' + sacontact_gid));
        }
        $scope.verifyindividualupdate = function (sacontact_gid) {
            $location.url('app/MstSAOnboardingBDVerificationTrainingStatusindividual?hash=' + cmnfunctionService.encryptURL('lssacontact_gid=' + sacontact_gid));
        }
        
        $scope.verifyIndividual = function (sacontact_gid) {
            $location.url('app/MstSAOnboardingIndividualBDView?hash=' + cmnfunctionService.encryptURL('lssacontact_gid=' + sacontact_gid));
        }
        $scope.bd_PendingwithRM = function () {
            $state.go('app.MstSAOnboardingPendingwithRMIndividualSummary');
        }
        $scope.bd_pendingwithCAD = function () {
            $state.go('app.MstSAOnboardingPendingwithCADIndividualSummary');
        }
        // Tagged Request
        $scope.Institution = function () {
            $state.go('app.MstSAOnboardingBDVerificationPending');
        }
        $scope.Individual = function () {
            $state.go('app.MstSAOnboardingBDVerificationIndividualPending');
        }


        $scope.viewIndividual = function (sacontact_gid) {
            $location.url('app/MstSAOnboardingIndividualView?hash=' + cmnfunctionService.encryptURL('lssacontact_gid=' + sacontact_gid));
        }
        $scope.bd_verificationinstitution = function () {
            $state.go('app.MstSAOnboardingBussDevtVerificationIndividualSummary');
        }

        $scope.bd_verificationindividual = function () {
            $state.go('app.MstSAOnboardingBDVerificationIndividualPending');
        }
       
        $scope.bd_verificationpending = function () {
            $state.go('app.MstSAOnboardingBussDevtVerificationIndividualPendingBDSummary');
        }
       
        $scope.bd_verificationrejected = function () {
            $state.go('app.MstSAOnboardingBussDevIndividualRejectedSummary');
        }
        $scope.bd_verificationdeferred = function () {
            $state.go('app.MstSABussDevtInstitutionDeferredSummary');
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
        $scope.StaticMapAndPhotos_View = function (latitude, longitude, addressline1, addressline2, postal_code) {
            var modalInstance = $modal.open({
                templateUrl: '/StaticMapAndPhotosView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    longitude: longitude,
                    latitude: latitude
                }
                var url = 'api/GoogleMapsAPI/GetStaticMapUrl';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.staticmapImgUrl = resp.data;
                });
                if (addressline2 == '') {
                    var addressString = ''.concat(addressline1.toString(), ",", postal_code.toString());
                } else {
                    var addressString = ''.concat(addressline1.toString(), ",", addressline2.toString(), ",", postal_code.toString());
                }
                var params = {
                    address: addressString
                }
                var url = 'api/GoogleMapsAPI/GetPlaceImage';
                SocketService.getparams(url, params).then(function (resp) {
                    var photoUrlArray = [];
                    for (var i = 0; i < resp.data.length; i++) {
                        if (resp.data[i] != null) {
                            photoUrlArray[i] = resp.data[i];
                        }
                    }
                    if (photoUrlArray.length == 0) {
                        $scope.photoNotFound = true;
                    } else {
                        $scope.photoUrlList = photoUrlArray;
                        $scope.photoFound = true;
                    }
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
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
                    $scope.rmtaggedassign_list = resp.data.taggedindividual_list;
                    unlockUI();
                });
            }

        }
    }
})();

