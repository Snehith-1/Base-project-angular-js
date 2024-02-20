(function () {
    'use strict';

    angular
        .module('angle')
        .controller('viewlegalSRcontroller', viewlegalSRcontroller);



    viewlegalSRcontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function viewlegalSRcontroller($rootScope, $scope, $state, $modal, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {


        /* jshint validthis:true */
        var vm = this;
        vm.title = 'viewlegalSRcontroller';
        activate();
        function activate() {
            $scope.templegalsr_gid = localStorage.getItem('templegalsr_gid');
            $scope.customer_gid = localStorage.getItem('customer_gid');

            var customer = {
                //customer_gid: localStorage.getItem('legalsr_customergid')
                templegalsr_gid: $scope.templegalsr_gid,
                customer_gid: $scope.customer_gid
            }

            var url = 'api/raiseLegalSR/Getpromoter';

            SocketService.getparams(url, customer).then(function (resp) {
                $scope.promoters_list = resp.data.promoter_list;
            });
            var url = 'api/raiseLegalSR/Getguarantor';

            SocketService.getparams(url, customer).then(function (resp) {
                $scope.guarantors_list = resp.data.guarantor_list;
            });

            var url = 'api/raiseLegalSR/Getcustomerdtl';
            SocketService.getparams(url, customer).then(function (resp) {
                $scope.address = resp.data.address;
                $scope.email = resp.data.email_id;
                $scope.customer_name = resp.data.customer_name;
            });

            var url = 'api/raiseLegalSR/getfacilitydtl';

            SocketService.getparams(url, customer).then(function (resp) {
                $scope.facility_list = resp.data.facility_list;
            });

            var param = {
                raiselegalSR_gid: localStorage.getItem('templegalsr_gid')
            };

            var url = 'api/raiseLegalSR/viewLegalSR';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.account_name=resp.data.account_name;
                $scope.financed_by = resp.data.financed_by;
                $scope.constitution = resp.data.constitution;
                $scope.deal_year = resp.data.deal_year;
                $scope.cycles_sanctiondated = resp.data.cycles_sanctiondated;
                $scope.limit_sanction = resp.data.limit_sanction;
                $scope.business_activity = resp.data.business_activity;
                $scope.primary_securities = resp.data.primary_securities;
                $scope.collateral_securities = resp.data.collateral_securities;
                $scope.details_UDC_PDC = resp.data.details_UDC_PDC;
                $scope.unit_working_status = resp.data.unit_working_status;
                $scope.txtother_banker = resp.data.txtother_banker;
                $scope.other_banker_exposures = resp.data.other_banker_exposures;
                $scope.status_current_overdue = resp.data.status_current_overdue;
                $scope.cibil_data = resp.data.cibil_data;
                $scope.churing_account = resp.data.churing_account;
                $scope.other_group_companies = resp.data.other_group_companies;
                $scope.meeting_details = resp.data.meeting_details;
                $scope.restructuring_data = resp.data.restructuring_data;
                $scope.txtother_banker = resp.data.otherbankers_borrower;
                $scope.instances_PTP = resp.data.instances_PTP;
                $scope.statuslegal_action = resp.data.statuslegal_action;
                $scope.demandnotice_details = resp.data.demandnotice_details;
                $scope.created_date = resp.data.created_date;
                console.log(resp.data.created_date);
            });

            var url = "api/raiseLegalSR/getlegalSRcontactdtl";
            SocketService.getparams(url, param).then(function (resp) {
                console.log(resp);
                $scope.contactdetailsRM = resp.data.contactdetailsRM;
            });
        }

        $scope.legalSRback = function (val) {
            $state.go('app.legalSRsummary');
        }


    }

})();
