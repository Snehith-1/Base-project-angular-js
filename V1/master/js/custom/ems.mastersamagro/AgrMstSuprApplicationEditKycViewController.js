(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstSuprApplicationEditKycViewController', AgrMstSuprApplicationEditKycViewController);

    AgrMstSuprApplicationEditKycViewController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$sce', '$anchorScroll'];

    function AgrMstSuprApplicationEditKycViewController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $sce, $anchorScroll) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstSuprApplicationEditKycViewController';


        var application_gid = $location.search().application_gid;
        var lstab = $location.search().lstab;
        var lsstatus = $location.search().lsstatus;

        activate();

        function activate() {

            var param = {
                application_gid: application_gid
            }

            var url = 'api/AgrSuprKycView/GetPANAuthenticationDtl';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.panauthentication_list = resp.data.panauthentication_list;
            });

            /*     var url = 'api/AgrSuprKycView/GetPANAadhaarLinkDtl';
                 SocketService.getparams(url, param).then(function (resp) {
                     $scope.panaadhaarlink_list = resp.data.panaadhaarlink_list;
                 }); */

            var url = 'api/AgrSuprKycView/GetDLAuthenticationDtl';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.dlauthentication_list = resp.data.dlauthentication_list;
            });

            var url = 'api/AgrSuprKycView/GetEPICAuthenticationDtl';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.epicauthentication_list = resp.data.epicauthentication_list;
            });

            var url = 'api/AgrSuprKycView/GetPassportAuthenticationDtl';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.passportauthentication_list = resp.data.passportauthentication_list;
            });

            var url = 'api/AgrSuprKycView/GetIFSCAuthenticationDtl';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.ifscauthentication_list = resp.data.ifscauthentication_list;
            });

            var url = 'api/AgrSuprKycView/GetBankAccVerificationDtl';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.bankaccverification_list = resp.data.bankaccverification_list;
            });

            var url = 'api/AgrSuprKycView/GetGSTSBPANDtl';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.gstsbpan_list = resp.data.gstsbpan_list;
            });

        }

        $scope.epicauthenticationView = function (kycepicauthentication_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewEpicAuthentication.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       kycepicauthentication_gid: kycepicauthentication_gid
                   }
                var url = 'api/AgrSuprKycView/VoterIDViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.name = resp.data.result.name;
                    $scope.rln_name = resp.data.result.rln_name;
                    $scope.rln_type = resp.data.result.rln_type;
                    $scope.gender = resp.data.result.gender;

                    $scope.district = resp.data.result.district;
                    $scope.ac_name = resp.data.result.ac_name;
                    $scope.pc_name = resp.data.result.pc_name;
                    $scope.state = resp.data.result.state;

                    $scope.epic_no = resp.data.result.epic_no;
                    $scope.dob = resp.data.result.dob;
                    $scope.age = resp.data.result.age;
                    $scope.part_no = resp.data.result.part_no;

                    $scope.slno_inpart = resp.data.result.slno_inpart;
                    $scope.ps_name = resp.data.result.ps_name;
                    $scope.part_name = resp.data.result.part_name;
                    $scope.last_update = resp.data.result.last_update;

                    $scope.ps_lat_long = resp.data.result.ps_lat_long;
                    $scope.rln_name_v1 = resp.data.result.rln_name_v1;
                    $scope.rln_name_v2 = resp.data.result.rln_name_v2;
                    $scope.rln_name_v3 = resp.data.result.rln_name_v3;

                    $scope.section_no = resp.data.result.section_no;
                    $scope.id = resp.data.result.id;
                    $scope.name_v1 = resp.data.result.name_v1;
                    $scope.name_v2 = resp.data.result.name_v2;

                    $scope.name_v3 = resp.data.result.name_v3;
                    $scope.ac_no = resp.data.result.ac_no;
                    $scope.st_code = resp.data.result.st_code;
                    $scope.house_no = resp.data.result.house_no;



                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.dlauthenticationView = function (kycdlauthentication_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewDLAuthentication.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       kycdlauthentication_gid: kycdlauthentication_gid
                   }
                var url = 'api/AgrSuprKycView/DrivingLicenseViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.status = resp.data.result.status;
                    $scope.fatherhusband = resp.data.result.fatherhusband;
                    $scope.bloodGroup = resp.data.result.bloodGroup;
                    $scope.dlNumber = resp.data.result.dlNumber;

                    $scope.name = resp.data.result.name;
                    $scope.dob = resp.data.result.dob;
                    $scope.issueDate = resp.data.result.issueDate;

                    $scope.validity_nonTransport = resp.data.result.validity.nonTransport;
                    $scope.validity_transport = resp.data.result.validity.transport;

                    $scope.statusDetails_remarks = resp.data.result.statusDetails.remarks;
                    $scope.statusDetails_to = resp.data.result.statusDetails.to;
                    $scope.statusDetails_from = resp.data.result.statusDetails.from;

                    $scope.address_list = resp.data.result.address;
                    $scope.covDetails_list = resp.data.result.covDetails;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.passportauthenticationView = function (kycpassportauthentication_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewPassportAuthentication.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       kycpassportauthentication_gid: kycpassportauthentication_gid
                   }
                var url = 'api/AgrSuprKycView/PassportViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.passportNumberFromSource = resp.data.result.passportNumber.passportNumberFromSource;
                    $scope.passportNumberMatch = resp.data.result.passportNumber.passportNumberMatch;

                    $scope.typeOfApplication = resp.data.result.typeOfApplication;
                    $scope.applicationDate = resp.data.result.applicationDate;

                    $scope.dispatchedOnFromSource = resp.data.result.dateOfIssue.dispatchedOnFromSource;
                    $scope.dateOfIssueMatch = resp.data.result.dateOfIssue.dateOfIssueMatch;

                    $scope.nameFromPassport = resp.data.result.name.nameFromPassport;
                    $scope.surnameFromPassport = resp.data.result.name.surnameFromPassport;
                    $scope.nameMatch = resp.data.result.name.nameMatch;
                    $scope.nameScore = resp.data.result.name.nameScore;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.GSTSBPANView = function (kycgstsbpan_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewGSTSBPAN.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       kycgstsbpan_gid: kycgstsbpan_gid
                   }
                var url = 'api/AgrSuprKycView/GSTSBPANViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.GSTSBPAN_list = resp.data.result;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.IFSCAuthenticationView = function (kycifscauthentication_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewIFSCAuthentication.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       kycifscauthentication_gid: kycifscauthentication_gid
                   }
                var url = 'api/AgrSuprKycView/IFSCViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.city = resp.data.result.city;
                    $scope.office = resp.data.result.office;
                    $scope.district = resp.data.result.district;
                    $scope.ifsc = resp.data.result.ifsc;
                    $scope.micr = resp.data.result.micr;
                    $scope.state = resp.data.result.state;
                    $scope.contact = resp.data.result.contact;
                    $scope.branch = resp.data.result.branch;
                    $scope.address = resp.data.result.address;
                    $scope.bank = resp.data.result.bank;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.BankAccVerificationView = function (kycbankaccverification_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewBankAccVerification.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       kycbankaccverification_gid: kycbankaccverification_gid
                   }
                var url = 'api/AgrSuprKycView/BankAccViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.bankTxnStatus = resp.data.result.bankTxnStatus;
                    $scope.accountNumber = resp.data.result.accountNumber;
                    $scope.ifsc = resp.data.result.ifsc;
                    $scope.accountName = resp.data.result.accountName;
                    $scope.bankResponse = resp.data.result.bankResponse;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

            }
        }

        $scope.Back = function () {
            $location.url('app/AgrMstSuprApplicationCreationView?application_gid=' + application_gid + '&lstab=' + lstab);
        }




    }
})();
