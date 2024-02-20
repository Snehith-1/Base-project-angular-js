(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstBusinessApprovalController', MstBusinessApprovalController);

        MstBusinessApprovalController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstBusinessApprovalController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstBusinessApprovalController';
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.employee_gid = $location.search().employee_gid;
        var employee_gid = $scope.employee_gid;
        $scope.applicationapproval_gid = $location.search().applicationapproval_gid;
        var applicationapproval_gid = $scope.applicationapproval_gid;
        $scope.lspage = $location.search().lspage;
        var lspage =  $scope.lspage;
        $scope.lsflag = $location.search().lsflag;
        $scope.initiate_flag = $location.search().initiate_flag;
        var initiate_flag = $scope.initiate_flag;
        $scope.lsccmeetingflag = $location.search().lsccmeetingflag;

        lockUI();
        activate();
        function activate() {
            
            var param = {
                application_gid: $scope.application_gid,
                employee_gid: $scope.employee_gid
            };

            var url = 'api/MstApplicationApproval/Getapplicationhierarchylist';

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

            var url = 'api/MstApplicationApproval/GetAppApprovalSummary';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.approval_list = resp.data.applicationapprovallist;
            });
            
            var url = 'api/MstApplicationApproval/GetAppcommentsSummary';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.comment_list = resp.data.applicationcommentslist;
            });

            var param = {
                application_gid: $scope.application_gid
            };

            var url = 'api/MstApplicationApproval/Getapplicationdetails';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.lblapplref_no = resp.data.application_no;
                $scope.lblapplicant_name = resp.data.customer_name;
                $scope.lblcurrentappl_stage = resp.data.approval_status;
                $scope.lblappl_state = resp.data.region;
                $scope.lbloveralllimit_request = resp.data.overalllimit_amount;
                $scope.productlist = resp.data.productlist;
            });

            var url = 'api/MstApplicationApproval/GetAppCommentStatus';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.commentstatus_flag = resp.data.commentstatus_flag;
                $scope.approved_flag = resp.data.approved_flag;

            });

            var url = 'api/MstCreditApproval/Getcreditheadsview';
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

            var url = 'api/MstCreditApproval/GetAppcreditApprovalSummary';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.creditapproval_list = resp.data.appcreditapprovallist;
            });

            var url = 'api/MstCreditApproval/GetAppcreditquerysSummary';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.creditquery_list = resp.data.appcreditquerylist;
            });

            var url = 'api/MstCreditApproval/GetApprmquerysSummary';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.rmquery_list = resp.data.appcreditquerylist;
            });

            var url = 'api/MstCreditApproval/GetAppcreditRejectedSummary';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.creditrejected_list = resp.data.Getappcreditrejectedlist;
                $scope.rejectstatus_flag = $scope.creditrejected_list[0].rejectstatus_flag;
            });  

            var params = {
                application_gid: $scope.application_gid
            }

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

            var url = 'api/MstCAD/GetCreditWithoutCCLog';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.creditwithoutcclog_list = resp.data.creditwithoutcclog_list;
            });

            var url = 'api/MstCAD/GetCADtoCCMeetingLog';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.cadtocclog_list = resp.data.cadtoccmeetinglog_list;
            });


            var params = {
                application_gid: application_gid
            }
            lockUI();
            var url = 'api/MstCC/GetScheduleMeeting';
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

            var url = 'api/MstCC/GetScheduleMeeting';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.ccmember_list = resp.data.ccmember_list;
                $scope.otheruser_list = resp.data.otheruser_list;
            });

        }
        

        $scope.Back = function () {
            if (lspage == 'BusinessApproval') {
                $state.go('app.MstBusinessApprovalSummary');
            }
            else if (lspage == 'BusinessReject') {
                $state.go('app.MstBusinessRejectedSummary');
            } 
            else if (lspage == 'BusinessHold') {
                $state.go('app.MstBusinessHoldSummary');
            } 
            else if (lspage == 'BusinessApproved') {
                $state.go('app.MstBusinessApprovedSummary');
            }
            else if (lspage == 'UpcomingBusinessApproval') {
                $state.go('app.MstUpcomingBusinessApprovalSummary');
            }
            else {
               
            } 
        }

        $scope.create_comment = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addcomments.html',
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
                $scope.comment_add = function () {
                   var params = {
                        commenttitle: $scope.txtcomment_title,
                        commentdesc: $scope.txtcomment_desc,
                        application_gid: application_gid,
                        applicationapproval_gid: applicationapproval_gid
                    }
                    var url = 'api/MstApplicationApproval/PostApplicationcommentadd';
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
                  var url = 'api/MstApplicationApproval/GetAppcommentdesc';
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

        $scope.approve = function () {
            var params = {
                    applicationapproval_gid : applicationapproval_gid,
                    approval_status : 'Approved',
                    approval_remarks : $scope.txtapproval_remarks,
                    application_gid : application_gid 
                   }
            var url = 'api/MstApplicationApproval/PostApplicationHeadApproval';
               lockUI();
               SocketService.post(url, params).then(function (resp) {
                   unlockUI();
                   if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                       $state.go('app.MstBusinessApprovalSummary');
                 /*   activate();*/
                       $scope.initiate_flag = 'N';
                       
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

        $scope.reject = function () {
            var params = {
                    applicationapproval_gid : applicationapproval_gid,
                    approval_status : 'Rejected',
                    approval_remarks : $scope.txtapproval_remarks,
                    application_gid : application_gid 
                   }
            var url = 'api/MstApplicationApproval/PostApplicationHeadApproval';
               lockUI();
               SocketService.post(url, params).then(function (resp) {
                   unlockUI();
                   if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                     /*  activate();*/
                       $state.go('app.MstBusinessApprovalSummary');
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

        $scope.hold = function () {
            var params = {
                    applicationapproval_gid : applicationapproval_gid,
                    approval_status : 'Hold',
                    approval_remarks : $scope.txtapproval_remarks,
                    application_gid : application_gid 
                   }
            var url = 'api/MstApplicationApproval/PostApplicationHeadApproval';
               lockUI();
               SocketService.post(url, params).then(function (resp) {
                   unlockUI();
                   if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    /*   activate();*/
                       $state.go('app.MstBusinessApprovalSummary');
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
                var url = 'api/MstCreditApproval/GetAppcreditqueryesc';
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

    }
})();

