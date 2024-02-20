(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstSuprCcCommitteeKycViewController', AgrMstSuprCcCommitteeKycViewController);

    AgrMstSuprCcCommitteeKycViewController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$sce', '$anchorScroll'];

    function AgrMstSuprCcCommitteeKycViewController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $sce, $anchorScroll) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstSuprCcCommitteeKycViewController';
        var application_gid = $location.search().application_gid;
        var lsstatus = $location.search().lsstatus;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.lstab = $location.search().lstab;
        var lstab = $scope.lstab;
        activate();

        function activate() {

            var param = {
                application_gid: application_gid
            }

            var url = 'api/AgrSuprKycView/GetPANAuthenticationDtl';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.panauthentication_list = resp.data.panauthentication_list;
            });

            /*     var url = 'api/KycView/GetPANAadhaarLinkDtl';
                 SocketService.getparams(url, param).then(function (resp) {
                     $scope.panaadhaarlink_list = resp.data.panaadhaarlink_list;
                 }); */

            var url = 'api/AgrSuprKycView/GetDLAuthenticationDtl';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.dlauthentication_list = resp.data.dlauthentication_list;
            });

            var url = 'api/AgrSuprKycView/GetEPICAuthenticationDtl';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.epicauthentication_list = resp.data.epicauthentication_list;
            });

            var url = 'api/AgrSuprKycView/GetIFSCAuthenticationDtl';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.ifscauthentication_list = resp.data.ifscauthentication_list;
            });

            var url = 'api/AgrSuprKycView/GetBankAccVerificationDtl';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.bankaccverification_list = resp.data.bankaccverification_list;
            });

            var url = 'api/AgrSuprKycView/GetGSTSBPANDtl';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.gstsbpan_list = resp.data.gstsbpan_list;
            });

        }

        $scope.Back = function () {
            if (lspage == 'ScheduleMeeting') {
                $location.url('app/AgrTrnSuprCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=ScheduleMeeting');
            }
            else if (lspage == 'ScheduledMeetingsummary') {
                $location.url('app/AgrTrnSuprCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=ScheduledMeetingsummary');
            }
            else if (lspage == 'CompletedMeetingsummary') {
                $location.url('app/AgrTrnSuprCCCompletedSummary?application_gid=' + application_gid + '&lspage=CompletedMeetingsummary');
            }
            else if (lspage == 'CCMmeetingScheduledcompleted') {
                $location.url('app/AgrTrnSuprCcCompletedScheduledMeeting?application_gid=' + application_gid + '&lspage=CCMmeetingScheduledcompleted');
            }
            else if (lstab == 'SubmittedToApproval') {
                $location.url('app/AgrMstSuprCcCommitteeApplicationView?application_gid=' + application_gid + '&lstab=SubmittedToApproval');
            }
            else if (lstab == 'SubmittedToCC') {
                $location.url('app/AgrMstSuprCcCommitteeApplicationView?application_gid=' + application_gid + '&lstab=SubmittedToCC');
            }
            else if (lstab == 'CCSkippedAppl') {
                $location.url('app/AgrMstSuprCcCommitteeApplicationView?application_gid=' + application_gid + '&lstab=CCSkippedAppl');
            }
            else if (lstab == 'RejectHoldAppl') {
                $location.url('app/AgrMstSuprCcCommitteeApplicationView?application_gid=' + application_gid + '&lstab=RejectHoldAppl');
            }
            else if (lspage == 'SentBackToCredit') {
                $location.url('app/AgrTrnSuprCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=SentBackToCredit');
            }
            else if (lspage == 'CCMmeetingScheduledcompleted') {
                $location.url('app/AgrTrnSuprCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=CCMmeetingScheduledcompleted');
            }
            else if (lstab == 'CCApproved') {
                $location.url('app/AgrMstSuprCcCommitteeApplicationView?application_gid=' + application_gid + '&lstab=CCApproved');
            }
            else {
                $location.url('app/AgrTrnSuprCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=CCMmeeting');
            }

        }




    }
})();
