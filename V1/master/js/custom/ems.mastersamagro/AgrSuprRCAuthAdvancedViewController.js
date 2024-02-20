(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrSuprRCAuthAdvancedViewController', AgrSuprRCAuthAdvancedViewController);

    AgrSuprRCAuthAdvancedViewController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$sce'];

    function AgrSuprRCAuthAdvancedViewController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrSuprRCAuthAdvancedViewController';
        var vehiclercauthadvanced_gid = localStorage.getItem('vehiclercauthadvanced_gid');

        activate();

        function activate() {
            var params = {
                vehiclercauthadvanced_gid: vehiclercauthadvanced_gid,
            }

            var url = 'api/AgrMstSuprAPIVerifications/RCAuthAdvancedViewDetails';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtstatePermitIssuedDate = resp.data.result.statePermitIssuedDate;
                $scope.txtcolor = resp.data.result.color;
                $scope.txtbodyTypeDescription = resp.data.result.bodyTypeDescription;
                $scope.txtfatherName = resp.data.result.fatherName;

                $scope.txttaxPaidUpto = resp.data.result.taxPaidUpto;
                $scope.txtinsuranceUpto = resp.data.result.insuranceUpto;
                $scope.txtpucNumber = resp.data.result.pucNumber;
                $scope.txtownerSerialNumber = resp.data.result.ownerSerialNumber;

                $scope.txtblackListStatus = resp.data.result.blackListStatus;
                $scope.txtnationalPermitIssuedBy = resp.data.result.nationalPermitIssuedBy;
                $scope.txtmakerDescription = resp.data.result.makerDescription;
                $scope.txtnumberOfCylinders = resp.data.result.numberOfCylinders;

                $scope.txtvehicleClassDescription = resp.data.result.vehicleClassDescription;
                $scope.txtchassisNumber = resp.data.result.chassisNumber;
                $scope.txtmanufacturedMonthYear = resp.data.result.manufacturedMonthYear;
                $scope.txtstatePermitNumber = resp.data.result.statePermitNumber;

                $scope.txtcubicCapacity = resp.data.result.cubicCapacity;
                $scope.txtnationalPermitNumber = resp.data.result.nationalPermitNumber;
                $scope.txtregistrationNumber = resp.data.result.registrationNumber;
                $scope.txtseatingCapacity = resp.data.result.seatingCapacity;

                $scope.txtengineNumber = resp.data.result.engineNumber;
                $scope.txtstatePermitType = resp.data.result.statePermitType;
                $scope.txtinsuranceCompany = resp.data.result.insuranceCompany;
                $scope.txtsleeperCapacity = resp.data.result.sleeperCapacity;

                $scope.txtfitnessUpto = resp.data.result.fitnessUpto;
                $scope.txtpresentAddress = resp.data.result.presentAddress;
                $scope.txtownerName = resp.data.result.ownerName;
                $scope.txtfinancier = resp.data.result.financier;

                $scope.txtgrossVehicleWeight = resp.data.result.grossVehicleWeight;
                $scope.txtunladenWeight = resp.data.result.unladenWeight;
                $scope.txtinsurancePolicyNumber = resp.data.result.insurancePolicyNumber;
                $scope.txtnationalPermitExpiryDate = resp.data.result.nationalPermitExpiryDate;

                $scope.txtregisteredAt = resp.data.result.registeredAt;
                $scope.txtpucExpiryDate = resp.data.result.pucExpiryDate;
                $scope.txtmakerModel = resp.data.result.makerModel;
                $scope.txtpermanentAddress = resp.data.result.permanentAddress;

                $scope.txtstandingCapacity = resp.data.result.standingCapacity;
                $scope.txtnormsDescription = resp.data.result.normsDescription;
                $scope.txtstatusAsOn = resp.data.result.statusAsOn;
                $scope.txtregistrationDate = resp.data.result.registrationDate;

                $scope.txtfuelDescription = resp.data.result.fuelDescription;
                $scope.txtstatePermitExpiryDate = resp.data.result.statePermitExpiryDate;
                $scope.txtwheelbase = resp.data.result.wheelbase;

            });






        }

        $scope.close = function () {
            window.close();
        }
    }
})();
