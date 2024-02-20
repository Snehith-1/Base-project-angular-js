(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstApplicationAssigRejectSummaryController', MstApplicationAssigRejectSummaryController);

    MstApplicationAssigRejectSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstApplicationAssigRejectSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstApplicationAssigRejectSummaryController';

        activate();

        function activate() {
            var url = 'api/MstApplicationAdd/GetAppAssignedRejectedSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.appassignment_list = resp.data.applicationadd_list;
            });

            var url = 'api/MstApplicationAdd/AssignApplicationCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.pending_count = resp.data.pending_count;
                $scope.assigned_count = resp.data.assigned_count;
                $scope.lstotalcount = resp.data.lstotalcount;
                $scope.submittedtocc_count = resp.data.submittedtocc_count;
                $scope.ccapproved_count = resp.data.ccapproved_count;
                $scope.rejected_count = resp.data.rejected_count;
            });
        }

        $scope.GradingTool_add = function (val) {
            $location.url('app/MstCreditAssessedScoreAdd?application_gid=' + val + '&lstab=ApplAssignmnet');
        }

        $scope.VisitReport_add = function (val) {
            $location.url('app/MstCreditVisitReportAdd?application_gid=' + val + '&lstab=ApplAssignmnet');
        }

        $scope.applcreation_view = function (val) {
            $location.url('app/MstApplicationCreationView?application_gid=' + val + '&lstab=RejectedMyApplication');
        }
        $scope.assigned_applications = function (val) {
            $location.url('app/MstAppassignedAssignmentSummary');
        }

        $scope.pending_applications = function (val) {
            $location.url('app/MstApplicationAssignmentSummary');
        }

        $scope.rejected = function (val) {
            $location.url('app/MstApplicationAssigRejectSummary');
        }

        $scope.submittedto_cc = function () {
            $location.url('app/MstApplSubmittedtoCCSummary');
        }

        $scope.ccapproved = function (val) {
            $location.url('app/MstApplCCApproved');
        }

        $scope.creditcmd_statusupdate = function (application_gid, employee_gid, applicationapproval_gid, initiate_flag) {
            $location.url('app/MstCreditQueryStatus?application_gid=' + application_gid + '&employee_gid=' + employee_gid + '&initiate_flag=N' + '&lspage=RejectedMyApplication');
        }

        $scope.reassignapplication = function (creditgroup_gid, application_gid) {

            var modalInstance = $modal.open({
                templateUrl: '/reassigncreditapproval.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    creditmapping_gid: creditgroup_gid
                }
                var url = 'api/MstCreditMapping/GetCredit2Heads';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.creditheadlist = resp.data.Credithead;
                    $scope.nationalcreditlist = resp.data.Creditnationalmanager;
                    $scope.regionalcreditlist = resp.data.Creditregionalmanager;
                    $scope.creditmanagerlist = resp.data.CreditManager;
                });

                var url = 'api/MstCreditMapping/GetCreditgroupname';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.creditgoupname) {                       
                        var getcreditgroup_namedtl = resp.data.creditgoupname.filter(function (c) { return c.creditgroup_gid === resp.data.creditgroup_gid });
                        $scope.cbocreditgroup = getcreditgroup_namedtl[0].creditgroup_name;
                    }
                });

                var params1 = {
                    application_gid: application_gid
                }
                var url = 'api/MstAppCreditUnderWriting/GetApplicationCreditAprovalinfo';
                SocketService.getparams(url, params1).then(function (resp) {
                    if (resp.data.pendingapproval == 'Y') {
                        $scope.cbocreditmanager = resp.data.creditmanager_gid;

                        $scope.cboregionalcredit = resp.data.creditregionalmanager_gid;
                        $scope.cboregionalcreditname = resp.data.creditregionalmanager_name;
                        $scope.approvedregionalcredit = false;

                        $scope.cbonationalcredit = resp.data.creditnationalmanager_gid;
                        $scope.cbonationalcreditname = resp.data.creditnationalmanager_name;
                        $scope.approvednationalcredit = false;

                        $scope.cbocreditheadgid = resp.data.credithead_gid;
                        $scope.cbocreditheadname = resp.data.credithead_name;
                        $scope.approvedcredithead = false;
                        $scope.txtreassignremarks = resp.data.approval_remarks;
                    }
                    else {
                        $scope.cbocreditmanager = resp.data.creditApproval_list.find(function (c) { return c.hierary_level === "0" }).approval_gid;

                        var getregionalcredit = resp.data.creditApproval_list.filter(function (c) { return c.hierary_level === "1" });
                        if (getregionalcredit) {
                            $scope.cboregionalcredit = getregionalcredit[0].approval_gid;
                            $scope.cboregionalcreditname = getregionalcredit[0].approval_name;
                            $scope.approvedregionalcredit = getregionalcredit[0].approval_status === 'Approved' ? true : false;
                        }
                        var getnationalcredit = resp.data.creditApproval_list.filter(function (c) { return c.hierary_level === "2" });
                        if (getnationalcredit) {
                            $scope.cbonationalcredit = getnationalcredit[0].approval_gid;
                            $scope.cbonationalcreditname = getnationalcredit[0].approval_name;
                            $scope.approvednationalcredit = getnationalcredit[0].approval_status === 'Approved' ? true : false;
                        }
                        var getcredithead = resp.data.creditApproval_list.filter(function (c) { return c.hierary_level === "3" });
                        if (getcredithead) {
                            $scope.cbocreditheadgid = getcredithead[0].approval_gid;
                            $scope.cbocreditheadname = getcredithead[0].approval_name;
                            $scope.approvedcredithead = getcredithead[0].approval_status === 'Approved' ? true : false;
                        }
                        $scope.txtreassignremarks = resp.data.approval_remarks;
                    } 
                    
                });

                var url = "api/MstCreditMapping/GetReassignedLog"
                SocketService.getparams(url, params1).then(function (resp) {
                    $scope.reassignedloglist = resp.data.reassignedloglist;
                    angular.forEach($scope.reassignedloglist, function (value, key) {
                        if (value.creditmanger_gid === "" || value.creditmanger_gid === null) {
                            value.showcreditmanger_name = false;
                        }
                        else {
                            value.showcreditmanger_name = true;
                        }
                        if (value.creditregionalmanager_gid === "" || value.creditregionalmanager_gid === null) {
                            value.showcreditregionalmanager_name = false;
                        }
                        else {
                            value.showcreditregionalmanager_name = true;
                        }
                        if (value.creditnationalmanager_gid === "" || value.creditnationalmanager_gid === null) {
                            value.showcreditnationalmanager_name = false;
                        }
                        else {
                            value.showcreditnationalmanager_name = true;
                        }
                        if (value.credithead_gid === "" || value.credithead_gid === null) {
                            value.showcredithead_name = false;
                        }
                        else {
                            value.showcredithead_name = true;
                        }
                    });
                    if ($scope.reassignedloglist && $scope.reassignedloglist.length != 0)
                        $scope.showreassignedlist = true;
                    else
                        $scope.showreassignedlist = false;
                    unlockUI();
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.reassign_application = function () {
                    var lscreditmanager_gid = '';
                    var lscreditmanager_name = '';
                    var lsregionalcredit_gid = '';
                    var lsregionalcredit_name = '';
                    var lsnationalcredit_gid = '';
                    var lsnationalcredit_name = '';
                    var lscredithead_gid = '';
                    var lscredithead_name = '';

                    var creditgroupname = $('#creditgroup :selected').text();
                    if ($scope.cbocreditmanager != undefined || $scope.cbocreditmanager != null) {
                        lscreditmanager_gid = $scope.cbocreditmanager;
                        lscreditmanager_name = $scope.creditmanagerlist.find(function (x) { return x.employee_gid === $scope.cbocreditmanager }).employee_name;
                    }
           
                    if ($scope.cboregionalcredit != undefined || $scope.cboregionalcredit != null) {
                        lsregionalcredit_gid = $scope.cboregionalcredit;
                        lsregionalcredit_name = $scope.regionalcreditlist.find(function (x) { return x.employee_gid === $scope.cboregionalcredit });
                        if (lsregionalcredit_name)
                            lsregionalcredit_name = lsregionalcredit_name.employee_name;
                        else
                            lsregionalcredit_name = $scope.cboregionalcreditname;
                    }
                    if ($scope.cbonationalcredit != undefined || $scope.cbonationalcredit != null) {
                        lsnationalcredit_gid = $scope.cbonationalcredit;
                        lsnationalcredit_name = $scope.nationalcreditlist.find(function (x) { return x.employee_gid === $scope.cbonationalcredit });
                        if (lsnationalcredit_name)
                            lsnationalcredit_name = lsnationalcredit_name.employee_name;
                        else
                            lsnationalcredit_name = $scope.cbonationalcreditname;
                    }
                    if ($scope.cbocreditheadgid != undefined || $scope.cbocreditheadgid != null) {
                        lscredithead_gid = $scope.cbocreditheadgid;
                        lscredithead_name = $scope.creditheadlist.find(function (x) { return x.employee_gid === $scope.cbocreditheadgid });
                        if (lscredithead_name)
                            lscredithead_name = lscredithead_name.employee_name;
                        else
                            lscredithead_name = $scope.cbocreditheadname;
                    }

                    var params = {
                        application_gid: application_gid,
                        creditgroup_name: $scope.cbocreditgroup,
                        creditmanager_gid: lscreditmanager_gid,
                        creditmanager_name: lscreditmanager_name,
                        regionalcredit_gid: lsregionalcredit_gid,
                        regionalcredit_name: lsregionalcredit_name,
                        nationalcredit_gid: lsnationalcredit_gid,
                        nationalcredit_name: lsnationalcredit_name,
                        credithead_gid: lscredithead_gid,
                        credithead_name: lscredithead_name,
                        remarks: $scope.txtreassignremarks
                    }
                    var url = 'api/MstCreditMapping/GetCreditReassignUpdate';
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
                                status: 'error',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });

                    $modalInstance.close('closed');
                }
            }
        }
         
    }
})();
