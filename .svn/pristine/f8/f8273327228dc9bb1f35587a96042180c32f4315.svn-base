(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstApprovedDisbursementSummaryController', MstApprovedDisbursementSummaryController);

    MstApprovedDisbursementSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstApprovedDisbursementSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstApprovedDisbursementSummaryController';

        activate();
        lockUI();
        function activate() {
            var url = 'api/MstCreditOpsApplication/GetDisbursementCompletedSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.disbursementcompleted_list = resp.data.disbursementcompleted_list;
            });            
        }
        
        $scope.view = function (val) {
            $location.url('app/MstCadApplicationView?application_gid=' + val + '&lspage=ApprovedDisbursement');
        }

        $scope.completed_process = function (application_gid, application2sanction_gid, application2loan_gid, customer_urn, rmdisbursementrequest_gid, lsareference_gid) {
            $location.url('app/MstApprovedDisbursementView?application_gid=' + application_gid + '&application2sanction_gid=' + application2sanction_gid + '&application2loan_gid=' + application2loan_gid + '&customer_urn=' + customer_urn + '&rmdisbursementrequest_gid=' + rmdisbursementrequest_gid + '&lsareference_gid=' + lsareference_gid + '&lspage=ApprovedDisbursement');
        }

        $scope.posttolms_openloanaccount = function (application_gid) {
            var params = {
                application_gid: application_gid
            }
            var url = 'api/SamFinHAPIOpenLoanAccount/PostOpenLoanAccounttoEncore';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                   /* GetBuyeronboardingApprovedlistERP();*/
                }
                else {
                    if (resp.data.error_response != null) {
                        var error_message = resp.data.message;
                        error_message += " - Encore Response: " + resp.data.error_response;
                        Notify.alert(error_message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 10000
                        });
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                }


            });
        }
    }
})();
