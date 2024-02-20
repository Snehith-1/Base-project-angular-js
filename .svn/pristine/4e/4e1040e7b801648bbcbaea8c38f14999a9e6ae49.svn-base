(function () {
    'use strict';

    angular
        .module('angle')
        .controller('tier1Approvalcontroller', tier1Approvalcontroller);

    tier1Approvalcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams','DownloaddocumentService'];

    function tier1Approvalcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'tier1Approvalcontroller';

        activate();

        function activate() {
            var params = {
                tier1format_gid: localStorage.getItem('tier1format_gid')
            }
            lockUI();
            var url = "api/TierMeeting/GetTier1ApprovalDtl";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customer_name = resp.data.customer_name;
                $scope.customer_urn = resp.data.customer_urn;
                $scope.tier1_observations = resp.data.tier1_observations;
                $scope.tier1_code = resp.data.tier1_code;
                $scope.tier1_justification = resp.data.tier1_justification;
                $scope.tier1_processgap = resp.data.tier1_processgap;
                $scope.tier1_processrecommendation = resp.data.tier1_processrecommendation;
                $scope.tier1_managementcomments = resp.data.tier1_managementcomments;
                $scope.tier1_reverts_actionplan = resp.data.tier1_reverts_actionplan;
                $scope.tier1_atrdate = resp.data.tier1_atrdate;
                $scope.tier1approvallog = resp.data.tier1approvallog;
                $scope.txtcode_changereason = resp.data.tier1code_changereason;
                $scope.tier1code_changeflag = resp.data.tier1code_changeflag;
                $scope.tier3_status = resp.data.tier3_status;
                $scope.uploaddocument_list = resp.data.tier1doc;
                $scope.tier1rejectlog = resp.data.tier1rejectlog;
                $scope.tier1_approvalstatus = resp.data.tier1_approvalstatus;
                if ($scope.tier1code_changeflag == 'Y') {
                    $scope.disablecodechangereasonshow = true;
                }
                else {
                    $scope.disablecodechangereasonshow = false;
                }
                if ($scope.tier1_approvalstatus == "Approved") {
                    $scope.editdisable = true;
                }
                else {
                    $scope.editenable = true;
                }
                if (resp.data.tier1approvallog == null) {
                    $scope.nohistoryapproval = true;
                }
                else {
                    $scope.historyapproval = true;
                }
                if (resp.data.tier1rejectlog == null) {
                    $scope.rejecthistory = false;
                }
                else {
                    $scope.rejecthistory = true;
                }
                unlockUI();
            });
        }

        $scope.viewcustomerdtl = function () {
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/tierCustomer360";
            window.open(URL, '_blank');
        }

        $scope.tier1approve = function () {
            var params = {
                tier1format_gid: localStorage.getItem('tier1format_gid'),
                approval_remarks: $scope.txttier1_approvalremarks
            }
            lockUI();
            var url = "api/TierMeeting/PostTier1Approved";
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $state.go('app.tier1Summary');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                }
            });
        }

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.tier1reject = function () {
            var params = {
                tier1format_gid: localStorage.getItem('tier1format_gid'),
                approval_remarks: $scope.txttier1_approvalremarks
            }
            lockUI();
            var url = "api/TierMeeting/PostTier1Rejected";
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $state.go('app.tier1Summary');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                }
            });
        }
    }
})();
