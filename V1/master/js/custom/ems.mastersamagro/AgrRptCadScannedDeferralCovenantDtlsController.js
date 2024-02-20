(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrRptCadScannedDeferralCovenantDtlsController', AgrRptCadScannedDeferralCovenantDtlsController);

    AgrRptCadScannedDeferralCovenantDtlsController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AgrRptCadScannedDeferralCovenantDtlsController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrRptCadScannedDeferralCovenantDtlsController';
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        var processtypeassign_gid = "";
        if (($location.search().lsprocesstypeassign_gid) != undefined)
            processtypeassign_gid = $location.search().lsprocesstypeassign_gid;
        else
            processtypeassign_gid = $location.search().processtypeassign_gid;
        var lstype = $location.search().lspath;
        activate();

        function activate() {
            lockUI();
            var params = {
                application_gid: application_gid,
            }

            var url = 'api/AgrMstApplicationView/GetApplicationBasicView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtapplication_no = resp.data.application_no;
                $scope.txtbasiccustomer_name = resp.data.customer_name;
                $scope.txtcustomer_urn = resp.data.customer_urn;
            });

            var url = 'api/AgrMstApplicationView/GetIndividualList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.CreditIndividual_List = resp.data.individual_List;
            });

            var url = 'api/AgrMstScannedDocument/GetScannedGeneralInfo';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.scannedgeneralinfo = resp.data;
            });

            var url = 'api/AgrMstApplicationView/GetInstitutionList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.CreditInstitution_List = resp.data.institution_List;
            });
            $scope.pendingapproval = true;
            if (lstype == 'Maker') {
                $scope.btnname = 'Proceed To Checker';
            }
            else if (lstype == 'Checker') {
                $scope.btnname = 'Proceed To Approval';
            }
            else if (lstype == 'Approver') {
                $scope.btnname = 'Approve';
            }
            else {
                $scope.pendingapproval = false;;
            }
        }

        var params =
        {
            application_gid: application_gid
        }
        var url = "api/AgrMstApplicationEdit/GetGroupSummary";
        SocketService.getparams(url, params).then(function (resp) {
            $scope.group_list = resp.data.group_list;
            angular.forEach($scope.group_list, function (value, key) {
                var params = {
                    group_gid: value.group_gid
                };

                var url = 'api/AgrMstApplicationView/GetGrouptoMemberList';
                SocketService.getparams(url, params).then(function (resp) {
                    value.groupmember_list = resp.data.groupmember_list;
                    value.expand = false;
                });
            });
        });

        $scope.Back = function () {
            if (lspage == 'scanneddeferral') {
                $location.url('app/AgrRptCadDeferral');
            }
            else if (lspage == 'scannedcovenant') {
                $location.url('app/AgrRptCadCovenant');
            }
            else if (lspage == 'physicalcovenant') {
                $location.url('app/AgrRptCadCovenant');
            }
            else if (lspage == 'scannedcheckercovenant') {
                $location.url('app/AgrRptCadCovenantChecker');
            }
            else if (lspage == 'physicalcheckercovenant') {
                $location.url('app/AgrRptCadCovenantChecker');
            }
            else if (lspage == 'scannedapprovalcovenant') {
                $location.url('app/AgrRptCadCovenantApproval');
            }
            else if (lspage == 'physicalapprovalcovenant') {
                $location.url('app/AgrRptCadCovenantApproval');
            }
            else if (lspage == 'physicaldeferral') {
                $location.url('app/AgrRptCadDeferral');
            }
            else if (lspage == 'physicaldeferral') {
                $location.url('app/AgrRptCadDeferral');
            }
            else {

            }
            //else if (lspage == 'CadDeferralApproval') {
            //    $location.url('app/AgrMstCadDeferralApprovalSummary');
            //}
            //else {
            //    $location.url('app/AgrMstScannedCompletedSummary');
            //}

           /* $location.url('app/AgrRptCadDeferral');*/

        }

        $scope.institution_add = function (institution_gid) {
            $location.url('app/AgrRptCadScannedDocchecklist?application_gid=' + application_gid + '&credit_gid=' + institution_gid + '&lspage=' + lspage + '&processtypeassign_gid=' + processtypeassign_gid + '&lspath=' + lstype + '&lstype=Institution');
        }

        $scope.individual_add = function (contact_gid) {
            $location.url('app/AgrRptCadScannedDocchecklist?application_gid=' + application_gid + '&credit_gid=' + contact_gid + '&lspage=' + lspage + '&processtypeassign_gid=' + processtypeassign_gid + '&lspath=' + lstype + '&lstype=Individual');
        }

        $scope.member_add = function (contact_gid) {
            $location.url('app/AgrRptCadScannedDocchecklist?application_gid=' + application_gid + '&credit_gid=' + contact_gid + '&lspage=' + lspage + '&processtypeassign_gid=' + processtypeassign_gid + '&lspath=' + lstype + '&lstype=Individual');
        }

        $scope.proceedsubmit = function () {
            lockUI();
            var params = {
                lstype: lstype,
                processtypeassign_gid: processtypeassign_gid
            }

            var url = 'api/AgrMstScannedDocument/UpdateScannedApproval';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $scope.Back();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'error',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }
    }
})();
