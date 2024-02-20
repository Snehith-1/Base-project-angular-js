(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstAppScoreCardViewdtlcontroller', MstAppScoreCardViewdtlcontroller);

    MstAppScoreCardViewdtlcontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$sce', '$anchorScroll'];

    function MstAppScoreCardViewdtlcontroller($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $sce, $anchorScroll) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstAppScoreCardViewdtlcontroller';
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.lstab = $location.search().lstab;
        var lstab = $scope.lstab;

        activate();

        function activate() {
            lockUI();
            var param = {
                application_gid: application_gid
            }

            var url = 'api/MstCreditMapping/GetCreditScorecardViewdtl';
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.GroupTitle_list = resp.data.GroupTitle_dtl;
                $scope.GroupQuestion_list = resp.data.MdlCreditGroupTitleQuestion; 

                angular.forEach($scope.GroupTitle_list, function (value, key) {
                    if (value.grouptitle_gid != "") {
                        var getGroupQuestionListArray = $scope.GroupQuestion_list.filter(function (el) { return el.grouptitle_gid === value.grouptitle_gid });
                        if (getGroupQuestionListArray != null) {
                            value.GroupQuestion_list = getGroupQuestionListArray;
                        }
                    }
                });  
            });
        }

       
      
        $scope.Back = function () {
            if (lspage == 'SubmittedToApproval') {
                $location.url('app/MstCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=SubmittedToApproval');
            }
            else if (lstab == 'MyApplications') {
                $location.url('app/MstApplicationCreationView?application_gid=' + application_gid + '&lstab=MyApplications');
            }
            else if (lspage == 'CreditSubmittedtoCC') {
                $location.url('app/MstCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=CreditSubmittedtoCC');
            }
            else if (lspage == 'CreditApproval') {
                $location.url('app/MstCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=CreditApproval');
            }
            else if (lspage == 'SubmittedToCC') {
                $location.url('app/MstCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=SubmittedToCC');
            }
            else if (lspage == 'UpcomingCreditApproval') {
                $location.url('app/MstCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=UpcomingCreditApproval');
            }
            else if (lspage == 'CreditApproved') {
                $location.url('app/MstCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=CreditApproved');
            }
            else if (lspage == 'CreditRejectHold') {
                $location.url('app/MstCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=CreditRejectHold');
            }
            else if (lspage == 'CCMmeeting') {
                $location.url('app/MstCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=CCMmeeting');
            }
            else if (lspage == 'ScheduleMeeting') {
                $location.url('app/MstCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=ScheduleMeeting');
            }
            else if (lspage == 'ScheduledMeetingsummary') {
                $location.url('app/MstCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=ScheduledMeetingsummary');
            }
            else if (lspage == 'CompletedMeetingsummary') {
                $location.url('app/MstCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=CompletedMeetingsummary');
            }
            else if (lspage == 'SentBackToCredit') {
                $location.url('app/MstCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=SentBackToCredit');
            }
            else if (lspage == 'Approvalmeeting') {
                $location.url('app/MstCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=Approvalmeeting');
            }
            else if (lspage == 'RejectHoldAppl') {
                $location.url('app/MstCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=RejectHoldAppl');
            }
            else if (lspage == 'CCApproved') {
                $location.url('app/MstCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=CCApproved');
            }
            else if (lstab == 'applicationcreation') {            
                $location.url('app/MstApplicationCreationView?application_gid=' + application_gid + '&lstab=applicationcreation');
            }
            else if (lstab == 'UpcomingBusinessApproval') {
                $location.url('app/MstApplicationCreationView?application_gid=' + application_gid + '&lstab=UpcomingBusinessApproval');
            }
            else if (lstab == 'BusinessReject') {
                $location.url('app/MstApplicationCreationView?application_gid=' + application_gid + '&lstab=BusinessReject');
            }
            else if (lstab == 'BusinessApproved') {
                $location.url('app/MstApplicationCreationView?application_gid=' + application_gid + '&lstab=BusinessApproved');
            }
            else if (lstab == 'BusinessApproval') {
                $location.url('app/MstApplicationCreationView?application_gid=' + application_gid + '&lstab=BusinessApproval');
            }
            else if (lstab == 'BusinessHold') {
                $location.url('app/MstApplicationCreationView?application_gid=' + application_gid + '&lstab=BusinessHold');
            }
            else if (lstab == 'Pencreditmapping') {
                $location.url('app/MstApplicationCreationView?application_gid=' + application_gid + '&lstab=Pencreditmapping');
            }
            else if (lstab == 'Asscreditmapping') {
                $location.url('app/MstApplicationCreationView?application_gid=' + application_gid + '&lstab=Asscreditmapping');
            }
            else if (lstab == 'ApplSubmittedToCC') {
                $location.url('app/MstApplicationCreationView?application_gid=' + application_gid + '&lstab=ApplSubmittedToCC');
            }
            else if (lstab == 'CCApproved') {
                $location.url('app/MstApplicationCreationView?application_gid=' + application_gid + '&lstab=CCApproved');
            }
            else if (lstab == 'RejectedMyApplication') {
                $location.url('app/MstApplicationCreationView?application_gid=' + application_gid + '&lstab=RejectedMyApplication');
            }
            else if (lstab == 'CreditPendingVerification') {
                $location.url('app/MstApplicationCreationView?application_gid=' + application_gid + '&lstab=CreditPendingVerification');
            }
            else if (lspage == 'CCMmeetingScheduledcompleted') {
                $location.url('app/MstCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=CCMmeetingScheduledcompleted');
            }
            else if (lspage == 'CreditHoldRevokeAppl') {
                $location.url('app/MstCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=CreditHoldRevokeAppl');
            }
        }
    }
})();
