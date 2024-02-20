(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrPropertyTaxViewController', AgrPropertyTaxViewController);

    AgrPropertyTaxViewController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$sce'];

    function AgrPropertyTaxViewController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'PropertyTaxViewController';
        var propertytax_gid = localStorage.getItem('propertytax_gid');

        activate();

        function activate() {
            var params = {
                propertytax_gid: propertytax_gid,
            }

            var url = 'api/AgrMstAPIVerifications/PropertyTaxViewDetails';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();


                $scope.txtpropertyId = resp.data.result.propertyDetails.propertyId;
                $scope.txtpropertyAddress = resp.data.result.propertyDetails.propertyAddress;
                $scope.txtplotAreaInSqYrd = resp.data.result.propertyDetails.plotAreaInSqYrd;
                $scope.txtplotAreaInSqMtrs = resp.data.result.propertyDetails.plotAreaInSqMtrs;
                $scope.txtvacantAreaInSqYrd = resp.data.result.propertyDetails.vacantAreaInSqYrd;
                $scope.txtconstructedArea = resp.data.result.propertyDetails.constructedArea;
                $scope.txtexemptionCategory = resp.data.result.propertyDetails.exemptionCategory;
                $scope.txtmultipurposeOwnership = resp.data.result.propertyDetails.multipurposeOwnership;
                $scope.txtownershipType = resp.data.result.propertyDetails.ownershipType;
                $scope.txtregistrationDocNo = resp.data.result.propertyDetails.registrationDocNo;
                $scope.txtregistrationDocDate = resp.data.result.propertyDetails.registrationDocDate;
                $scope.txtBillingName = resp.data.result.propertyDetails.BillingName;
                $scope.txtBillingAddress = resp.data.result.propertyDetails.BillingAddress;

                $scope.floorDetails_list = resp.data.result.propertyDetails.floorDetails;

                $scope.taxCalculations_list = resp.data.result.taxCalculations;

                $scope.txtdebitAmount = resp.data.result.penalty.debitAmount;
                $scope.txtcreditAmount = resp.data.result.penalty.creditAmount;
                $scope.txtbalanceTaxAmount = resp.data.result.penalty.balanceTaxAmount;

                $scope.penaltydetail = "defined";
                if ($scope.txtdebitAmount == null && $scope.txtcreditAmount == null && $scope.txtbalanceTaxAmount == null) {
                    $scope.contactdetail = null;
                }

                $scope.ownerDetails_list = resp.data.result.propertyDetails.ownerDetails;

                $scope.paymentDetails_list = resp.data.result.propertyDetails.paymentDetails;
            
            });






        }

        $scope.close = function () {
            window.close();
        }
    }
})();
