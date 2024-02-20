(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSentbackCadtoCcHistoryController', MstSentbackCadtoCcHistoryController);

    MstSentbackCadtoCcHistoryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstSentbackCadtoCcHistoryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSentbackCadtoCcHistoryController';
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
            var url = 'api/MstCC/GetCCMeetingHistoryLog';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.ccmeetinghistorylog_list = resp.data.ccmeetinghistorylog_list;
            });           

            var url = 'api/MstCC/GetCCtoCreditLog';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.cctocreditlog_list = resp.data.cctocreditlog_list;
            });

            var url = 'api/MstCAD/GetCADtoCreditLog';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.cadtocreditlog_list = resp.data.cadtocreditlog_list;
            });

            var url = 'api/MstCAD/GetCADtoCCMeetingLog';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.cadtoccmeetinglog_list = resp.data.cadtoccmeetinglog_list;
            });

            var url = 'api/MstApplicationView/GetApplicationBasicView';

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
            var url = 'api/MstCC/GetScheduleMeetingLog';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lblccschedule_list = resp.data.ccschedule_list;
            });

            var params = {
                application_gid: application_gid
            }
            var url = 'api/MstCAD/GetAppRevertReasonRemarks';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lblprocesstype_remarks = resp.data.processtype_remarks;
            });

            var url = 'api/MstCC/GetCCMeetingSkipHistory';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.ccmeetingskiphistory_list = resp.data.ccmeetingskiphistory_list;
            });

        }

        $scope.Back = function () {
            if (lspage == 'Pending') {
                $state.go('app.MstCreditCommitteeSummary');
            }
            else if (lspage == 'ScheduledMeeting') {
                $state.go('app.MstCCscheduledSummary');
            }
            else if (lspage == 'CCCompletedMeeting') {
                $state.go('app.MstCCCompletedSummary');
            }
            else if (lspage == 'SentbackcctoCredit') {
                $state.go('app.MstSentbackcctoCredit');
            }
            else if (lspage == 'CCScheduledMeetingSummary') {
                $state.go('app.MstCcScheduledMeetingSummary');
            }
            else if (lspage == 'CCCompletedScheduledMeetingSummary') {
                $state.go('app.MstCcCompletedScheduledMeeting');
            }
            else if (lspage == 'CADPendingSummary') {
                $state.go('app.MstPendingCADReview');
            }
            else {

            }

        }

        //$scope.cc_remarksview = function (ccmeeting2members_gid) {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/viewccremarks.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'lg'
        //    });
        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {
        //        var params =
        //           {
        //               application_gid: application_gid,
        //               ccmeeting2members_gid: ccmeeting2members_gid
        //           }
        //        var url = 'api/MstCC/ViewCCRemarks';
        //        SocketService.getparams(url, params).then(function (resp) {
        //            $scope.lblremarks = resp.data.cc_remarkslog;
        //        });
        //        $scope.ok = function () {
        //            $modalInstance.close('closed');
        //        };


        //    }

        //}

        $scope.ccmeetingdetails_view = function (application_gid,ccschedulemeetinglog_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewschedulemeetingpopup.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                       application_gid: application_gid,
                       ccschedulemeetinglog_gid: ccschedulemeetinglog_gid
                 }
                var url = 'api/MstCC/GetScheduleMeetingBasicLog';
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

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.ccapprovaldetails_view = function (application_gid, ccschedulemeetinglog_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewapprovaldetailspopup.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    application_gid: application_gid,
                    ccschedulemeeting_gid: ccschedulemeetinglog_gid
                }
                var url = 'api/MstCC/GetCCMeetingApprovalDtls';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.ccmember_list = resp.data.ccmemberlog_list;
                    $scope.otheruser_list = resp.data.otheruserlog_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.cc_remarksview = function (ccmeeting2members_gid) {
                    var modalInstance = $modal.open({
                        templateUrl: '/viewccremarks.html',
                        controller: ModalInstanceCtrl,
                        backdrop: 'static',
                        keyboard: false,
                        size: 'md'
                    });
                    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                    function ModalInstanceCtrl($scope, $modalInstance) {
                        var params =
                           {
                               application_gid: application_gid,
                               ccmeeting2members_gid: ccmeeting2members_gid
                           }
                        var url = 'api/MstCC/ViewCCRemarks';
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.lblremarks = resp.data.cc_remarkslog;
                        });
                        $scope.ok = function () {
                            $modalInstance.close('closed');
                        };


                    }

                }


            }

        }

        $scope.ccskipped_reason = function (ccmeetingskip_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/ccskippedreason.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    ccmeetingskip_gid: ccmeetingskip_gid
                }
                var url = 'api/MstCC/GetCCMeetingSkippedReason';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblccmeetingskipreason = resp.data.reason;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

    }
})();
