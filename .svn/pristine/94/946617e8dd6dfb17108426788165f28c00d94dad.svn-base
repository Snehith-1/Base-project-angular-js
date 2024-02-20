(function () {
    'use strict';

    angular
        .module('angle')
        .controller('sanctionViewcontroller', sanctionViewcontroller);

    sanctionViewcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function sanctionViewcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'sanctionViewcontroller';

        activate();

        function activate() {
            lockUI();
            var params = {
                customer2sanction_gid: localStorage.getItem('customer2sanction_gid')
            }

            var url = 'api/sanction/getsanctiondetails';
            SocketService.getparams(url, params).then(function (resp) {
              
                $scope.customer_name = resp.data.customer_name;
                $scope.txteditsanctionRefNo = resp.data.sanction_refno;
                $scope.txteditSanctionDate = resp.data.sanction_date;
                $scope.txteditSanctionAmount = resp.data.sanction_amount;
                $scope.txteditSanctionLimit = resp.data.sanction_limit;
                $scope.txtEditValidity = resp.data.sanction_validity;
                $scope.txtEditExpiryDate = resp.data.expiry_date;
                $scope.txtEditReviewDate = resp.data.review_date;
                $scope.cboeditapproval_authority = resp.data.approval_authority;
                $scope.cboEditnature_proposal = resp.data.natureof_proposal;
                $scope.cboEditconstitution = resp.data.constitution;
                $scope.cboEditauthorizedsignatory = resp.data.authorizedsignatoryname;
                $scope.txtEditRevisiedLimit = resp.data.revisied_limit;
                $scope.txtEditexistingLimit = resp.data.existing_limit;
                $scope.cboEditEscrowAccount = resp.data.escrow_account;
                $scope.txtEditspecificcondition = resp.data.specific_conditions;
                $scope.cboEditfacilitytype = resp.data.facility_type;
                $scope.documentationname = resp.data.documentationname;
                $scope.customer_urn = resp.data.customer_urn;
            });
            unlockUI();
        }

        $scope.back = function () {
            $state.go('app.sanctionManagement');
        }
    }
})();
