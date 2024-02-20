(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrBaseDetailsViewController', AgrBaseDetailsViewController);

        AgrBaseDetailsViewController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$sce'];

    function AgrBaseDetailsViewController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrBaseDetailsViewController';        
        var institutionprobedetails_gid = $location.search().institutionprobedetails_gid;

        activate();

        function activate() {
            var params = {
                institutionprobedetails_gid: institutionprobedetails_gid,
            }

            var url = 'api/AgrProbeAPI/BaseDetailsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();


                $scope.cin = resp.data.data.company.cin;
                $scope.legal_name = resp.data.data.company.legal_name;
                $scope.efiling_status = resp.data.data.company.efiling_status;
                $scope.incorporation_date = resp.data.data.company.incorporation_date;
                $scope.paid_up_capital = resp.data.data.company.paid_up_capital;
                $scope.sum_of_charges = resp.data.data.company.sum_of_charges;
                $scope.authorized_capital = resp.data.data.company.authorized_capital;
                $scope.active_compliance = resp.data.data.company.active_compliance;
                $scope.cirp_status = resp.data.data.company.cirp_status;
                $scope.registered_address = resp.data.data.company.registered_address.address_line1 + ',' + resp.data.data.company.registered_address.address_line2 + ',' + resp.data.data.company.registered_address.city + ',' + resp.data.data.company.registered_address.pincode + ',' + resp.data.data.company.registered_address.state;
                $scope.classification = resp.data.data.company.classification;
                $scope.status = resp.data.data.company.status;
                $scope.next_cin = resp.data.data.company.next_cin;
                $scope.last_agm_date = resp.data.data.company.last_agm_date;
                $scope.last_filing_date = resp.data.data.company.last_filing_date;
                $scope.email = resp.data.data.company.email;

                $scope.authorizedsignatories_list = resp.data.data.authorized_signatories;
                $scope.opencharges_list = resp.data.data.open_charges;

               
            });






        }

        $scope.close = function () {
            window.close();
        }
    }
})();
