(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstPostCcActivitiesRMViewController', MstPostCcActivitiesRMViewController);

    MstPostCcActivitiesRMViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstPostCcActivitiesRMViewController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstPostCcActivitiesRMViewController';
        var application_gid = $location.search().application_gid;
        $scope.customer_urn = $location.search().customer_urn;
        var customer_urn = $scope.customer_urn;
        var lspage = $location.search().lspage;

        activate();

        function activate() {
            var params =
            {
                application_gid: application_gid
            }
            var url = 'api/MstCAD/GetCadDocumentSubmissionFlag';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.docsubmission_flag = resp.data.docsubmission_flag;
                if ($scope.docsubmission_flag == 'Y') {
                    $scope.docsubmission_enable = true;
                    $scope.docsubmission_disable = false;                   
                }
                else {
                    $scope.docsubmission_disable = true;
                    $scope.docsubmission_enable = false;
                }
            });
        }

        $scope.Back = function () {
            if (lspage == 'MstRMCustomerSummary') {
                $location.url('app/MstRMCustomerSummary?application_gid=' + application_gid + '&lspage=RMMyCustomerList');
            }
            else if (lspage == 'RMCADUrnGrouping') {
                $location.url('app/MstRMCadUrnAcceptedCustomerDtls?application_gid=' + application_gid + '&customer_urn=' + customer_urn + '&lspage=RMCADUrnGrouping');
            }
            else {
                $location.url('app/MstRMCustomerSummary?application_gid=' + application_gid + '&lspage=RMMyCustomerList');
            }
            
        }

        $scope.GotoSanction = function () {
            $location.url('app/MstRMSanctionSummary?application_gid=' + application_gid);
        }

        $scope.GotoDeferral = function () {
            $location.url('app/MstRMDeferralDtls?application_gid=' + application_gid + '&customer_urn=' + customer_urn);
        }
        $scope.GotoWaiver = function () {
            $location.url('app/MstRMInitiateWaiverSummary?application_gid=' + application_gid);
        }

        $scope.GotoDocumentChecklist = function () {
            $location.url('app/MstRMDocChecklistDtls?application_gid=' + application_gid);
        }

        $scope.GotoPenalty = function () {
            $location.url('app/MstRMPenaltyDtls?application_gid=' + application_gid);
        }

        $scope.GotoLoanDetails = function () {
            $location.url('app/MstRMLoanDetailsDtls?application_gid=' + application_gid);
        }
        $scope.GotoTDS = function () {
            $location.url('app/MstRMTDSDtls?application_gid=' + application_gid);
        }
        $scope.GotoNDC = function () {
            $location.url('app/MstRMNDCDtls?application_gid=' + application_gid);
        }
        $scope.GotoNOC = function () {
            $location.url('app/MstRMNOCDtls?application_gid=' + application_gid);
        }
        $scope.GotoMoratorium = function () {
            $location.url('app/MstRMMoratoriumDtls?application_gid=' + application_gid);
        }
        $scope.GotoBankAccountDetails = function () {
            $location.url('app/MstRMBankAccountDetails?application_gid=' + application_gid);
        }
        $scope.GotoDeviation = function () {
            $location.url('app/MstRMDeviationDtls?application_gid=' + application_gid);
        }
        $scope.GotoDisbursementRequest = function () {
            $location.url('app/MstRMDisbursementRequest?application_gid=' + application_gid + '&customer_urn=' + customer_urn);
        }

    }
})();
