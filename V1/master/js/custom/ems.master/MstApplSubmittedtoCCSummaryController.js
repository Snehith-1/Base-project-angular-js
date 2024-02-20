(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstApplSubmittedtoCCSummaryController', MstApplSubmittedtoCCSummaryController);

    MstApplSubmittedtoCCSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstApplSubmittedtoCCSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstApplSubmittedtoCCSummaryController';
        lockUI();
        activate();
        function activate() {
            var url = 'api/MstApplicationAdd/GetApplSubmittedToCCSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.submittedtocc_list = resp.data.applicationadd_list;
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

        $scope.applcreation_view = function (val) {
            $location.url('app/MstApplicationCreationView?application_gid=' + val + '&lstab=ApplSubmittedToCC');
        }

        $scope.assigned_applications = function (val) {
            $location.url('app/MstAppassignedAssignmentSummary');
        }

        $scope.pending_applications = function (val) {
            $location.url('app/MstApplicationAssignmentSummary');
        }

        $scope.ccapproved = function (val) {
            $location.url('app/MstApplCCApproved');
        }

        $scope.rejected = function (val) {
            $location.url('app/MstApplicationAssigRejectSummary');
        }

        $scope.assign = function (creditgroup_gid, application_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/assign.html',
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
                var params = {
                    creditmapping_gid: creditgroup_gid
                }
                var url = 'api/MstCreditMapping/GetCreditgroupname';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.cbocreditgroup = resp.data.creditgroup_gid;
                    $scope.creditgrouplist = resp.data.creditgoupname;

                });

                $scope.creditgroup_change = function (cbocreditgroup) {
                    var params = {
                        creditmapping_gid: cbocreditgroup
                    }
                    var url = 'api/MstCreditMapping/GetCredit2Heads';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.creditheadlist = resp.data.Credithead;
                        $scope.nationalcreditlist = resp.data.Creditnationalmanager;
                        $scope.regionalcreditlist = resp.data.Creditregionalmanager;
                        $scope.creditmanagerlist = resp.data.CreditManager;
                    });
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.assign_application = function () {
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
                        lscreditmanager_gid = $scope.cbocreditmanager.employee_gid;
                        lscreditmanager_name = $scope.cbocreditmanager.employee_name;
                    }
                    if ($scope.cboregionalcredit != undefined || $scope.cboregionalcredit != null) {
                        lsregionalcredit_gid = $scope.cboregionalcredit.employee_gid;
                        lsregionalcredit_name = $scope.cboregionalcredit.employee_name;
                    }
                    if ($scope.cbonationalcredit != undefined || $scope.cbonationalcredit != null) {
                        lsnationalcredit_gid = $scope.cbonationalcredit.employee_gid;
                        lsnationalcredit_name = $scope.cbonationalcredit.employee_name;
                    }
                    if ($scope.cbocredithead != undefined || $scope.cbocredithead != null) {
                        lscredithead_gid = $scope.cbocredithead.employee_gid;
                        lscredithead_name = $scope.cbocredithead.employee_name;
                    }

                    var params = {
                        application_gid: application_gid,
                        creditgroup_gid: $scope.cbocreditgroup,
                        creditgroup_name: creditgroupname,
                        creditmanager_gid: lscreditmanager_gid,
                        creditmanager_name: lscreditmanager_name,
                        regionalcredit_gid: lsregionalcredit_gid,
                        regionalcredit_name: lsregionalcredit_name,
                        nationalcredit_gid: lsnationalcredit_gid,
                        nationalcredit_name: lsnationalcredit_name,
                        credithead_gid: lscredithead_gid,
                        credithead_name: lscredithead_name,
                        remarks: $scope.txtremarks
                    }
                    var url = 'api/MstCreditMapping/PostCreditassignUpdate';
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

        $scope.submittedto_cc = function () {
            $location.url('app/MstApplSubmittedtoCCSummary');
        }

    }
})();
