(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnSuprSentbackCadtoCcHistoryController', AgrTrnSuprSentbackCadtoCcHistoryController);

    AgrTrnSuprSentbackCadtoCcHistoryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrTrnSuprSentbackCadtoCcHistoryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnSuprSentbackCadtoCcHistoryController';
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        activate();
        lockUI();
        function activate() {
            var params = {
                application_gid: application_gid
            }
            lockUI();
            var url = 'api/AgrTrnSuprCC/GetScheduleMeetingBasicLog';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lblccmeeting_no = resp.data.ccmeeting_no;
                $scope.lblccmeeting_title = resp.data.ccmeeting_title;
                $scope.lblmeeting_time = resp.data.ccmeeting_time;
                $scope.lblccmeeting_mode = resp.data.ccmeeting_mode;
                $scope.lblccgroup_name = resp.data.ccgroup_name;
                $scope.lbldescription = resp.data.description;
                $scope.lblccmeeting_date = resp.data.ccmeeting_date;
                $scope.lblccmember_list = resp.data.ccmember_list;
                $scope.lblotheruser_name = resp.data.otheruser_name;
                $scope.lblccadmin_name = resp.data.ccadmin_name;
                $scope.ccschedulemeeting_gid = resp.data.ccschedulemeeting_gid
            });

            var url = 'api/AgrTrnSuprCC/GetScheduleMeeting';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.ccmember_list = resp.data.ccmemberlog_list;
                $scope.otheruser_list = resp.data.otheruserlog_list;
            });

            var url = 'api/AgrTrnSuprCC/GetCCtoCreditLog';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.cctocreditlog_list = resp.data.cctocreditlog_list;
            });

            var url = 'api/AgrTrnSuprCAD/GetCADtoCCMeetingLog';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.cadtoccmeetinglog_list = resp.data.cadtoccmeetinglog_list;
            });

            var url = 'api/AgrMstSuprApplicationView/GetApplicationBasicView';

            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtapplication_no = resp.data.application_no;
                $scope.txtbasiccustomer_name = resp.data.customer_name;
                $scope.txtcustomer_urn = resp.data.customer_urn;
                $scope.txtvertical = resp.data.vertical_name;
                $scope.txtvertical_tag = resp.data.verticaltaggs_name;
                $scope.txtconstitution = resp.data.constitution_name;
                $scope.txt_strategicbusiness_unit = resp.data.businessunit_name;
                $scope.txtprimayvalue_chain = resp.data.primaryvaluechain_name;
                $scope.txtsecondaryvalue_chain = resp.data.secondaryvaluechain_name;
                $scope.txtvernacular_language = resp.data.vernacular_language;
                $scope.txtApplfrom_SA = resp.data.sa_status;
                $scope.txtSAM_associateID = resp.data.sa_id;
                $scope.txtSAM_associatename = resp.data.sa_name;
                $scope.txtcontactperson_name = resp.data.contactperson_name;
                $scope.txtbasicdesignation = resp.data.designation_type;
                $scope.txtlandline_number = resp.data.landline_no;
                $scope.txtsocial_capital = resp.data.social_capital;
                $scope.txttrade_capital = resp.data.trade_capital;
                $scope.borrower_flag = resp.data.borrower_flag;
                $scope.borrower_type = resp.data.borrower_type;
                $scope.momapproval_flag = resp.data.momapproval_flag;
                $scope.txtcredit_group = resp.data.creditgroup_name;
            });
            var url = 'api/AgrTrnSuprCC/GetScheduleMeetingLog';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lblccschedule_list = resp.data.ccschedule_list;
            });

            var params = {
                application_gid: application_gid
            }
            var url = 'api/AgrTrnSuprCAD/GetAppRevertReasonRemarks';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lblprocesstype_remarks = resp.data.processtype_remarks;
            });

        }

        $scope.Back = function () {
            if (lspage == 'Pending') {
                $state.go('app.AgrTrnSuprCreditCommitteeSummary');
            }
            else if (lspage == 'ScheduledMeeting') {
                $state.go('app.AgrTrnSuprCCscheduledSummary');
            }
            else if (lspage == 'CCCompletedMeeting') {
                $state.go('app.AgrTrnSuprCCCompletedSummary');
            }
            else if (lspage == 'SentbackcctoCredit') {
                $state.go('app.AgrTrnSuprSentbackcctoCredit');
            }
            else if (lspage == 'CCScheduledMeetingSummary') {
                $state.go('app.AgrTrnSuprCcScheduledMeetingSummary');
            }
            else if (lspage == 'CCCompletedScheduledMeetingSummary') {
                $state.go('app.AgrTrnSuprCcCompletedScheduledMeeting');
            }
            else {

            }

        }

        $scope.cc_remarksview = function (ccmeeting2members_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewccremarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       application_gid: application_gid,
                       ccmeeting2members_gid: ccmeeting2members_gid
                   }
                var url = 'api/AgrTrnSuprCC/ViewCCRemarks';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.lblremarks = resp.data.cc_remarkslog;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

    }
})();
