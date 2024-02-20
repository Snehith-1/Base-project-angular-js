(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditApprovalController', MstCreditApprovalController);

    MstCreditApprovalController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function MstCreditApprovalController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCreditApprovalController';
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.employee_gid = $location.search().employee_gid;
        var employee_gid = $scope.employee_gid;
        $scope.appcreditapproval_gid = $location.search().appcreditapproval_gid;
        var appcreditapproval_gid = $scope.appcreditapproval_gid;
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
                appcreditapproval_gid: appcreditapproval_gid
            };

            var url = 'api/MstCreditApproval/GetCreditapplicationdetails';
            SocketService.getparams(url, param).then(function (resp) {
               
                $scope.lsapproval_status = resp.data.approval_status;
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

            //var url = 'api/MstCreditApproval/GetAppqueryStatus';
            //SocketService.getparams(url, param).then(function (resp) {
            //    $scope.querystatus_flag = resp.data.querystatus_flag;
            //    $scope.approved_flag = resp.data.approved_flag;
            //});

            var url = 'api/MstCreditApproval/GetAppcreditRejectedSummary';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.creditrejected_list = resp.data.Getappcreditrejectedlist;
                $scope.rejectstatus_flag = $scope.creditrejected_list[0].rejectstatus_flag;
            });

            var param = {
                application_gid: $scope.application_gid,
                appcreditapproval_gid: appcreditapproval_gid
            };
            var url = 'api/MstCreditApproval/GetAppqueryStatus';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.querystatus_flag = resp.data.querystatus_flag;
                $scope.approved_flag = resp.data.approved_flag;

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

            var url = 'api/MstCC/GetCCMeetingSkipHistory';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.ccmeetingskiphistory_list = resp.data.ccmeetingskiphistory_list;
            });

            //Get CAM Document 
            var url = 'api/MstAppCreditUnderWriting/GetCAM';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.camuploaddocument_list = resp.data.camdocument_list;
            });
        }
        

        $scope.Back = function () {
            if (lspage == 'CreditApproval') {
                $state.go('app.MstCreditApprovalSummary');
            }
            else if (lspage == 'CreditApproved') {
                $state.go('app.MstCreditApprovedSummary');
            }
            else if (lspage == 'CreditSubmittedtoCC') {
                $state.go('app.MstCreditSubmittedtoCCSummary');
            }
            else if (lspage == 'CreditCCSkipped') {
                $state.go('app.MstCreditCCSkippedSummary');
            }
            else if (lspage == 'CreditRejectHold') {
                $state.go('app.MstCreditRejectandHoldSummary');
            }
            else if (lspage == 'UpcomingCreditApproval') {
                $state.go('app.MstUpcomingCreditApprovalSummary');
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
                       appcreditapproval_gid: appcreditapproval_gid,
                       queryraised_to :'Credit'
                    }
                   var url = 'api/MstCreditApproval/PostAppcreditqueryadd';
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
                                status: 'warning',
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

        $scope.approve = function () {
            var params = {
                    appcreditapproval_gid: appcreditapproval_gid,
                    approval_status : 'Approved',
                    approval_remarks : $scope.txtapproval_remarks,
                    application_gid : application_gid 
                   }
            var url = 'api/MstCreditApproval/PostAppcreditHeadApproval';
               lockUI();
               SocketService.post(url, params).then(function (resp) {
                   unlockUI();
                   if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                      /* activate();*/
                       $state.go('app.MstCreditApprovalSummary');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }    
            }); 
        }

        $scope.reject = function () {
            var params = {
                appcreditapproval_gid: appcreditapproval_gid,
                    approval_status : 'Rejected',
                    approval_remarks : $scope.txtapproval_remarks,
                    application_gid : application_gid 
                   }
            var url = 'api/MstCreditApproval/PostAppcreditHeadApproval';
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
                       $state.go('app.MstCreditApprovalSummary');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
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
                    var url = 'api/MstApplicationApproval/GetUpdateCommentStatus';
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
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }

                    });

                    $modalInstance.close('closed');
                }

            }
        }

        $scope.view_querydescription = function (appcreditquery_gid) {
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
                    var url = 'api/MstCreditApproval/GetUpdatequerStatus';
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
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }

                    });

                    $modalInstance.close('closed');
                }

            }
        }

        $scope.hold = function () {
            var params = {
                     appcreditapproval_gid: appcreditapproval_gid,
                    approval_status : 'Hold',
                    approval_remarks : $scope.txtapproval_remarks,
                    application_gid : application_gid 
                   }
            var url = 'api/MstCreditApproval/PostAppcreditHeadApproval';
               lockUI();
               SocketService.post(url, params).then(function (resp) {
                   unlockUI();
                   if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                       /*activate();*/
                       $state.go('app.MstCreditApprovalSummary');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }    
            }); 
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

        //CAM Document Upload

        $scope.camdocumentUpload = function (val) {
            if (($scope.txtcamdocument_title == null) || ($scope.txtcamdocument_title == '') || ($scope.txtcamdocument_title == undefined)) {
                $("#momdocument").val('');
                Notify.alert('Kindly Enter the Document Title', 'warning');
            } else {
                var frm = new FormData();
                for (var i = 0; i < val.length; i++) {
                    var item = {
                        name: val[i].name,
                        file: val[i]
                    };
                    
                    frm.append('fileupload', item.file);
                    frm.append('file_name', item.name);
                   
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[i].name, "documentformatonly");
                    if (IsValidExtension == false) {
                        Notify.alert("File format is not supported..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });

                        return false;
                    }

                }
                frm.append('document_title', $scope.txtcamdocument_title);
                frm.append('application_gid', $scope.application_gid);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    lockUI();
                    var url = 'api/MstAppCreditUnderWriting/CAMocumentUpload';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                        $scope.camuploaddocument_list = resp.data.camdocument_list;
                        unlockUI();

                        $("#camdocument").val('');
                        $scope.uploadfrm = undefined;

                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $scope.$parent.txtcamdocument_title = '';
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        unlockUI();
                    });
                }
                else {
                    alert('Document is not Available..!');
                    return;
                }
            }
        }

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.deleteCAM = function (val) {
            var params = {
                application2camdoc_gid: val,
                application_gid: application_gid
            };

            var url = 'api/MstAppCreditUnderWriting/CAMdoc_delete';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.camuploaddocument_list = resp.data.camdocument_list;

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'Warning',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
            });
        }
        $scope.documentviewer = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
                return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
        }
        $scope.downloadall = function () {
            for (var i = 0; i < $scope.camuploaddocument_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.camuploaddocument_list[i].document_path, $scope.camuploaddocument_list[i].document_name);
            }
        }
    }
})();

