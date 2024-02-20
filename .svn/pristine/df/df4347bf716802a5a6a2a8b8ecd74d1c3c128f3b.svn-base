(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnSuprCadPhysicalDocGuarantorDtlsController', AgrTrnSuprCadPhysicalDocGuarantorDtlsController);

        AgrTrnSuprCadPhysicalDocGuarantorDtlsController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AgrTrnSuprCadPhysicalDocGuarantorDtlsController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnSuprCadPhysicalDocGuarantorDtlsController';
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

            var url = 'api/AgrMstSuprApplicationView/GetApplicationBasicView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtapplication_no = resp.data.application_no;
                $scope.txtbasiccustomer_name = resp.data.customer_name;
                $scope.txtcustomer_urn = resp.data.customer_urn;
            });


            var url = 'api/AgrTrnSuprPhysicalDocument/GetPhysicalIndividualList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.CreditIndividual_List = resp.data.individual_List;
            });

            var url = 'api/AgrTrnSuprPhysicalDocument/GetPhysicalGeneralInfo';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.generalinfo = resp.data;
            });

            var url = 'api/AgrTrnSuprPhysicalDocument/GetPhysicalInstitutionList';
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
            var url = "api/AgrTrnSuprPhysicalDocument/GetPhysicalGroupSummary";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.group_list = resp.data.group_list;
                angular.forEach($scope.group_list, function (value, key) {
                    var params = {
                        group_gid: value.group_gid
                    };

                    var url = 'api/AgrTrnSuprPhysicalDocument/GetPhysicalGrouptoMemberList';
                    SocketService.getparams(url, params).then(function (resp) {
                        value.groupmember_list = resp.data.groupmember_list;
                        value.expand = false;
                    });
                });
            });
        }



        $scope.Back = function () {
            if (lspage == 'CadPhysicalDocMaker') {
                $location.url('app/AgrTrnSuprCadPhysicalDocSummary');
            }
            else if (lspage == 'CadPhysicalDocChecker') {
                $location.url('app/AgrTrnSuprCadPhysicalDocCheckerSummary');
            }
            else if (lspage == 'CadPhysicalDocApproval') {
                $location.url('app/AgrTrnSuprCadPhysicalDocApprovalSummary');
            }
            else {
                $location.url('app/AgrTrnSuprCadPhysicalDocCompletedSummary');
            }
        }

        $scope.institution_add = function (institution_gid) {
            $location.url('app/AgrTrnSuprCadPhysicalDochecklist?application_gid=' + application_gid + '&credit_gid=' + institution_gid + '&lspage=' + lspage + '&processtypeassign_gid=' + processtypeassign_gid + '&lspath=' + lstype + '&lstype=Institution');
        }

        $scope.individual_add = function (contact_gid) {
            $location.url('app/AgrTrnSuprCadPhysicalDochecklist?application_gid=' + application_gid + '&credit_gid=' + contact_gid + '&lspage=' + lspage + '&processtypeassign_gid=' + processtypeassign_gid + '&lspath=' + lstype + '&lstype=Individual');
        } 

        $scope.group_add = function (group_gid) {
            $location.url('app/AgrTrnSuprCadPhysicalDochecklist?application_gid=' + application_gid + '&credit_gid=' + group_gid + '&lspage=' + lspage + '&processtypeassign_gid=' + processtypeassign_gid + '&lspath=' + lstype + '&lstype=Group');
         }

        $scope.member_add = function (contact_gid) {
            $location.url('app/AgrTrnSuprCadPhysicalDochecklist?application_gid=' + application_gid + '&credit_gid=' + contact_gid + '&lspage=' + lspage + '&processtypeassign_gid=' + processtypeassign_gid + '&lspath=' + lstype + '&lstype=Individual');
        }

        $scope.proceedsubmit = function () {
            lockUI();
            var params = {
                lstype: lstype,
                processtypeassign_gid: processtypeassign_gid
            }

            var url = 'api/AgrTrnSuprPhysicalDocument/UpdatePhysicalApproval';
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
