(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstRMDisbursementRequestController', MstRMDisbursementRequestController);

    MstRMDisbursementRequestController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstRMDisbursementRequestController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstRMDisbursementRequestController';
        var application_gid = $location.search().application_gid;
        $scope.customer_urn = $location.search().customer_urn;
        var customer_urn = $scope.customer_urn;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;

        activate();
        function activate() {
            var params = {
                customer_urn: customer_urn,
                application_gid: application_gid
            }
            var url = 'api/MstCreditOpsApplication/GetDisbursementRequestSummary';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.disbursementrequest_list = resp.data.disbursementrequest_list;
            });
        }

        $scope.initiate_disbursement = function () {
            $location.url('app/MstRMInitiateDisbursement?customer_urn=' + customer_urn);
        }

        $scope.edit = function (application_gid, application2sanction_gid, application2loan_gid, generatelsa_gid, rmdisbursementrequest_gid, lsgeneratelsa_gid, disbursement_to) {
            $location.url('app/MstRMDisbursementRequestEdit?application_gid=' + application_gid + '&application2sanction_gid=' + application2sanction_gid + '&disbursement_to=' + disbursement_to + '&application2loan_gid=' + application2loan_gid + '&customer_urn=' + customer_urn + '&generatelsa_gid=' + generatelsa_gid + '&rmdisbursementrequest_gid=' + rmdisbursementrequest_gid + '&lsgeneratelsa_gid=' + lsgeneratelsa_gid);
        }

        $scope.view = function (application_gid, application2sanction_gid, application2loan_gid, generatelsa_gid, rmdisbursementrequest_gid, lsgeneratelsa_gid) {
            $location.url('app/MstRMDisbursementRequestView?application_gid=' + application_gid + '&application2sanction_gid=' + application2sanction_gid + '&application2loan_gid=' + application2loan_gid + '&customer_urn=' + customer_urn + '&generatelsa_gid=' + generatelsa_gid + '&rmdisbursementrequest_gid=' + rmdisbursementrequest_gid + '&lsgeneratelsa_gid=' + lsgeneratelsa_gid + '&lspage=MyDisbursementRM');
        }

        $scope.Back = function () {            
                $location.url('app/MstRMCustomerDashboard?application_gid=' + application_gid + '&customer_urn=' + customer_urn);                      
        }
        $scope.completed_process = function (application_gid, application2sanction_gid, application2loan_gid,  rmdisbursementrequest_gid, lsareference_gid) {
            $location.url('app/MstApprovedDisbursementView?application_gid=' + application_gid + '&application2sanction_gid=' + application2sanction_gid + '&application2loan_gid=' + application2loan_gid + '&customer_urn=' + customer_urn + '&rmdisbursementrequest_gid=' + rmdisbursementrequest_gid + '&lsareference_gid=' + lsareference_gid + '&lspage=MyDisbursementRM');
        }
    }
})();
