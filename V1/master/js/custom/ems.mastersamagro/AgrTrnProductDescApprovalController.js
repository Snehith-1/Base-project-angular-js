﻿(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnProductDescApprovalController', AgrTrnProductDescApprovalController);

    AgrTrnProductDescApprovalController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService'];

    function AgrTrnProductDescApprovalController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnProductDescApprovalController';
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.employee_gid = $location.search().employee_gid;
        var employee_gid = $scope.employee_gid;
        $scope.appproductapproval_gid = $location.search().appproductapproval_gid;
        var appproductapproval_gid = $scope.appproductapproval_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.lsflag = $location.search().lsflag;
        $scope.lsPendingapproval = $location.search().lsPendingapproval; 
        $scope.shortclosing_flag = $location.search().shortclosing_flag;
        var shortclosing_flag = $scope.shortclosing_flag;

        lockUI();
        activate();
        function activate() {

            if (lspage == 'ProductDescApproved' || lspage == 'ProductDescRejectHold') {

                $scope.Querybtn = true

            }

            else {

                $scope.Querybtn = false

            }

            var param = {
                application_gid: $scope.application_gid,
                employee_gid: $scope.employee_gid
            };

            var url = 'api/AgrTrnApplicationApproval/Getapplicationhierarchylist';

            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.level_zero = resp.data.level_zero;
                $scope.level_one = resp.data.level_one;
                $scope.clusterhead = resp.data.clusterhead;
                $scope.zonalhead = resp.data.zonalhead;
                $scope.regionhead = resp.data.regionhead;
                $scope.businesshead = resp.data.businesshead;
            });

            var param = {
                application_gid: $scope.application_gid
            };

            var url = 'api/AgrTrnApplicationApproval/GetAppApprovalSummary';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.approval_list = resp.data.applicationapprovallist;
            });

            var url = 'api/AgrTrnApplicationApproval/GetAppcommentsSummary';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.comment_list = resp.data.applicationcommentslist;
            });

            var param = {
                application_gid: $scope.application_gid
            };

            var url = 'api/AgrTrnApplicationApproval/Getapplicationdetails';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.lblapplref_no = resp.data.application_no;
                $scope.lblapplicant_name = resp.data.customer_name;
                $scope.lblcurrentappl_stage = resp.data.approval_status;
                $scope.lblappl_state = resp.data.region;
                $scope.lbloveralllimit_request = resp.data.overalllimit_amount;
                $scope.productlist = resp.data.productlist;
                $scope.shortclosing_reason = resp.data.shortclosing_reason;
                $scope.expired_flag = resp.data.expired_flag;
            });
             
            var url = 'api/AgrTrnCreditApproval/Getcreditheadsview';
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.txtcredit_head = resp.data.credithead_name;
                $scope.txtnational_manager = resp.data.nationalcredit_name;
                $scope.txtregional_manager = resp.data.regionalcredit_name;
                $scope.txtcredit_manager = resp.data.creditmanager_name;
                $scope.txtcredit_group = resp.data.creditgroup_name;
                $scope.remarks = resp.data.remarks;
                unlockUI();
            });

            var url = 'api/AgrTrnProductApproval/GetMemberMangerApprovalDtls';
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.product_membername = resp.data.member_name;
                $scope.productmember_approvaldate = resp.data.memberapproval_date;
                $scope.productmember_approvalflag = resp.data.memberapproval_flag;
                $scope.product_managername = resp.data.manager_name;
                $scope.productmanager_approvaldate = resp.data.manager_approvaldate;
                $scope.productmanager_approvalflag = resp.data.manager_approvalflag;
                $scope.productmanager_approvalremarks = resp.data.manager_approvalremarks;
                unlockUI();
            });

            var url = 'api/AgrTrnCreditApproval/GetAppcreditApprovalSummary';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.creditapproval_list = resp.data.appcreditapprovallist;
            });

            var url = 'api/AgrTrnCreditApproval/GetAppcreditquerysSummary';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.creditquery_list = resp.data.appcreditquerylist;
            });

            var url = 'api/AgrTrnCreditApproval/GetApprmquerysSummary';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.rmquery_list = resp.data.appcreditquerylist;
            });

            var url = 'api/AgrTrnProductApproval/GetAppqueryStatus';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.querystatus_flag = resp.data.querystatus_flag;
                $scope.approved_flag = resp.data.approved_flag;

            });

            var params = {
                application_gid: $scope.application_gid
            }

            var url = 'api/AgrTrnCC/GetCCtoCreditLog';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.cctocreditlog_list = resp.data.cctocreditlog_list;
            });

            var url = 'api/AgrTrnCAD/GetCADtoCreditLog';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.cadtocreditlog_list = resp.data.cadtocreditlog_list;
            });
            $scope.amendmentshow = false;
            var url = 'api/AgrMstApplicationView/GetApplicationBasicView';
            SocketService.getparams(url, params).then(function (resp) {
              
                $scope.lblamendment_remarks = resp.data.amendment_remarks;
                unlockUI();

                if ($scope.lblamendment_remarks == null || $scope.lblamendment_remarks == '' || $scope.lblamendment_remarks == undefined) {
                    $scope.amendmentshow = false;
                }
                else {
                    $scope.amendmentshow = true;
                }
            });

            var params = {
                application_gid: application_gid
            }
            lockUI();
            var url = 'api/AgrTrnCC/GetScheduleMeeting';
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

            var url = 'api/AgrTrnCC/GetScheduleMeeting';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.ccmember_list = resp.data.ccmember_list;
                $scope.otheruser_list = resp.data.otheruser_list;
            });

            var url = 'api/AgrTrnProductApproval/GetAppProductrmquerysSummary';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.appproductquerylist = resp.data.appproductquerylist;
            });

            var url = 'api/AgrTrnProductApproval/GetManagerquerySummary';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.appmanagerquerylist = resp.data.appmanagerquerylist;
            });

            var url = 'api/AgrTrnProductApproval/GetAppProductAprovalinfo';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.productdesk_name = resp.data.productdesk_name;
                $scope.product_managername = resp.data.product_managername;
                $scope.product_membername = resp.data.product_membername;

            });

        }


        $scope.Back = function () {
            if (lspage == 'ProductDescMyApproval') {
                $state.go('app.AgrMstProductcDescApprovalSummary');
            }
            else if (lspage == 'ProductDescApproved') {
                $state.go('app.AgrTrnProductDescApprovedSummary');
            }
            else if (lspage == 'ProductDescRejectHold') {
                $state.go('app.AgrTrnProductDescRejectandHoldSummary');
            }
            else {

            }
        }

        $scope.create_query = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addquery.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.query_add = function () {
                    var params = {
                        querytitle: $scope.txtquery_title,
                        querydesc: $scope.txtquery_desc,
                        application_gid: application_gid,
                        appproductapproval_gid: $location.search().appproductapproval_gid,
                        queryraised_to: 'Member'
                    }
                    var url = 'api/AgrTrnProductApproval/PostManagerqueryadd';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });

                    $modalInstance.close('closed');
                }
            }
        }


        $scope.view_querydesc = function (appcreditquery_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/queryDescriptionView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                 {
                     appcreditquery_gid: appcreditquery_gid
                 }
                var url = 'api/AgrTrnCreditApproval/GetAppcreditqueryesc';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblquery_desc = resp.data.querydesc;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.approve = function (appproductapproval_gid) {
            var params = {
                appproductapproval_gid: appproductapproval_gid,
                approval_status: 'Approved',
                approval_remarks: $scope.txtapproval_remarks,
                application_gid: application_gid
            }
            var url = 'api/AgrTrnProductApproval/PostAppProductManagerApproval';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    }); 
                    activate();

                    $state.go('app.AgrTrnProductDescApprovedSummary');

                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }

        $scope.reject = function (appproductapproval_gid) {
            var params = {
                appproductapproval_gid: appproductapproval_gid,
                approval_status: 'Rejected',
                approval_remarks: $scope.txtapproval_remarks,
                application_gid: application_gid
            }
            var url = 'api/AgrTrnProductApproval/PostAppProductManagerApproval';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    }); 
                    activate();
                    $state.go('app.AgrTrnProductDescRejectandHoldSummary');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }

        $scope.view_commentdesc = function (applicationcomment_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/CommentDescriptionView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                 {
                     applicationcomment_gid: applicationcomment_gid
                 }
                var url = 'api/AgrTrnApplicationApproval/GetAppcommentdesc';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblComment_desc = resp.data.commentdesc;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }


        $scope.comment_close = function (applicationcomment_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/commentClose.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {
                    var params = {
                        applicationcomment_gid: applicationcomment_gid,
                        application_gid: application_gid,
                        close_remarks: $scope.txtcloseremarks
                    }
                    var url = 'api/AgrTrnApplicationApproval/GetUpdateCommentStatus';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }

                    });

                    $modalInstance.close('closed');
                }

            }
        }

        $scope.view_querydescription = function (appproductquery_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/queryDescriptionView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                 {
                     appproductquery_gid: appproductquery_gid
                 }
                var url = 'api/AgrTrnProductApproval/GetAppcreditqueryesc';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblquery_desc = resp.data.querydesc;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.query_close = function (appcreditquery_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/queryClose.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {
                    var params = {
                        appcreditquery_gid: appcreditquery_gid,
                        application_gid: application_gid,
                        close_remarks: $scope.txtcloseremarks
                    }
                    var url = 'api/AgrTrnCreditApproval/GetUpdatequerStatus';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }

                    });

                    $modalInstance.close('closed');
                }

            }
        }

        $scope.hold = function (appproductapproval_gid) {
            var params = {
                appproductapproval_gid: appproductapproval_gid,
                approval_status: 'Hold',
                approval_remarks: $scope.txtapproval_remarks,
                application_gid: application_gid
            }
            var url = 'api/AgrTrnProductApproval/PostAppProductManagerApproval';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    }); 
                    activate();
                    $state.go('app.AgrTrnProductDescRejectandHoldSummary');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }
    }
})();
