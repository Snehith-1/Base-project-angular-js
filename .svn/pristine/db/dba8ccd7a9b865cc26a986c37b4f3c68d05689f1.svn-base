(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstRptCadPhysicalDeferralCovenantDtlsController', MstRptCadPhysicalDeferralCovenantDtlsController);

    MstRptCadPhysicalDeferralCovenantDtlsController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstRptCadPhysicalDeferralCovenantDtlsController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstRptCadPhysicalDeferralCovenantDtlsController';
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

            var params = {
                application_gid: application_gid,
            }

            var url = 'api/MstCADFlow/GetApplicationBasicView';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtapplication_no = resp.data.application_no;
                $scope.txtbasiccustomer_name = resp.data.customer_name;
                $scope.txtcustomer_urn = resp.data.customer_urn;
            });


            var url = 'api/MstPhysicalDocument/GetPhysicalIndividualList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.CreditIndividual_List = resp.data.individual_List;
            });

            var url = 'api/MstPhysicalDocument/GetPhysicalGeneralInfo';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.generalinfo = resp.data;
            });

            var url = 'api/MstPhysicalDocument/GetPhysicalInstitutionList';
            lockUI();
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

            var params = { application_gid: application_gid }
            var url = "api/MstPhysicalDocument/GetPhysicalGroupSummary";
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.group_list = resp.data.group_list;
                angular.forEach($scope.group_list, function (value, key) {
                    var params = {
                        group_gid: value.group_gid
                    };

                    var url = 'api/MstPhysicalDocument/GetPhysicalGrouptoMemberList';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        value.groupmember_list = resp.data.groupmember_list;
                        value.expand = false;
                    });
                });
            });

            var url = 'api/MstPhysicalDocument/GetPhysicalDocApprovalDtls';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.maker_name = resp.data.maker_name;
                $scope.checker_name = resp.data.checker_name;
                $scope.approver_name = resp.data.approver_name;
                $scope.maker_approveddate = resp.data.maker_approveddate;
                $scope.checker_approveddate = resp.data.checker_approveddate;
                $scope.approver_approveddate = resp.data.approver_approveddate;
            });
        }



        $scope.Back = function () {
            if (lspage == 'CadPhysicalDocMaker') {
                $location.url('app/MstCadPhysicalDocSummary');
            }
            else if (lspage == 'CadPhysicalDocChecker') {
                $location.url('app/MstCadPhysicalDocCheckerSummary');
            }
            else if (lspage == 'CadPhysicalDocApproval') {
                $location.url('app/MstCadPhysicalDocApprovalSummary');
            }
            else if (lspage == 'physicalcovenant') {
                $location.url('app/MstRptCadCovenant');
            }
            else if (lspage == 'physicaldeferral') {
                $location.url('app/MstRptCadDeferral');
            }
            else {

            }
        }

        $scope.institution_add = function (institution_gid) {
            $location.url('app/MstRptCadPhysicalDocchecklist?application_gid=' + application_gid + '&credit_gid=' + institution_gid + '&lspage=' + lspage + '&processtypeassign_gid=' + processtypeassign_gid + '&lspath=' + lstype + '&lstype=Institution');
        }

        $scope.individual_add = function (contact_gid) {
            $location.url('app/MstRptCadPhysicalDocchecklist?application_gid=' + application_gid + '&credit_gid=' + contact_gid + '&lspage=' + lspage + '&processtypeassign_gid=' + processtypeassign_gid + '&lspath=' + lstype + '&lstype=Individual');
        }

        $scope.group_add = function (group_gid) {
            $location.url('app/MstRptCadPhysicalDocchecklist?application_gid=' + application_gid + '&credit_gid=' + group_gid + '&lspage=' + lspage + '&processtypeassign_gid=' + processtypeassign_gid + '&lspath=' + lstype + '&lstype=Group');
        }

        $scope.member_add = function (contact_gid) {
            $location.url('app/MstRptCadPhysicalDocchecklist?application_gid=' + application_gid + '&credit_gid=' + contact_gid + '&lspage=' + lspage + '&processtypeassign_gid=' + processtypeassign_gid + '&lspath=' + lstype + '&lstype=Individual');
        }

        $scope.proceedsubmit = function () {
            lockUI();
            var params = {
                lstype: lstype,
                processtypeassign_gid: processtypeassign_gid
            }

            var url = 'api/MstPhysicalDocument/UpdatePhysicalApproval';
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
