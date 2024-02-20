
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('complianceManagementcontroller', complianceManagementcontroller);

    complianceManagementcontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function complianceManagementcontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'complianceManagementcontroller';

        activate();


        function activate() {
            $scope.totalDisplayedpending= 100;
            $scope.totalDisplayedcompleted= 100;
            $scope.totalDisplayedrejected = 100; 
            $scope.totalDisplayedworkinprogress = 100;
            $scope.totalDisplayedtaggedlawyer = 100;
            $scope.tab = {};
            var url = window.location.href;
            var relPath = url.split("lstab=");
            var relpath1 = relPath[1];
            if (relpath1 != undefined) {
                if (relpath1 == "Pending") {
                    $scope.tabpending = true;
                }
                else if (relpath1 == "Completed") {
                    $scope.tabcompleted = true;
                }
                else if (relpath1 == "Rejected") {
                    $scope.tabrejected = true;
                }
                else if (relpath1 == "Tagged-Lawyer") {
                    $scope.tabtaggedlawyer = true;
                }
                else if (relpath1 == "Work-InProgress") {
                    $scope.tabworkinprogress = true;
                }

            }
            else {
                if ($scope.tab.activeTabId == undefined) {
                    $scope.tabpending = true;
                }
                else if ($scope.tab.activeTabId == 'Pending') {
                    $scope.tabpending = true;

                }
                else if ($scope.tab.activeTabId == 'Completed') {
                    $scope.tabcompleted = true;
                }
                else if ($scope.tab.activeTabId == 'Rejected') {
                    $scope.tabrejected = true;
                }
                else if ($scope.tab.activeTabId == 'Tagged-Lawyer') {
                    $scope.tabtaggedlawyer = true;
                }
                else if ($scope.tab.activeTabId == 'Work-InProgress') {
                    $scope.tabworkinprogress = true;
                }

            }
            var url = 'api/requestCompliance/Compliancemanagementsummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.requestcompliance_data = resp.data.requestcompliance_list;
                $scope.requestcomplianceworkinprogress_data = resp.data.requestcomplianceworkinprogress_list;
                $scope.requestcompliancerejected_data = resp.data.requestcompliancerejected_list;
                $scope.requestcompliancecompleted_data = resp.data.requestcompliancecompleted_list;
                $scope.requestcompliancetaggedlawyer_data = resp.data.requestcompliancetaggedlawyer_list;
                $scope.pending_count = resp.data.pending_count;
                $scope.completed_count = resp.data.completed_count;
                $scope.rejected_count = resp.data.rejected_count;
                $scope.workinprogress_count = resp.data.workinprogress_count;
                $scope.taggedlawyer_count = resp.data.taggedlawyer_count;

                for (var i = 0; i < $scope.requestcompliance_data.length; i++) {
                    if ($scope.requestcompliance_data[i].response_flag == "Y") {
                        $scope.pendingtab_alert = "Y";
                        break;
                    }
                }
                for (var i = 0; i < $scope.requestcomplianceworkinprogress_data.length; i++) {
                    if ($scope.requestcomplianceworkinprogress_data[i].response_flag == "Y") {
                        $scope.workinprogresstab_alert = "Y";
                        break;
                    }
                }
                for (var i = 0; i < $scope.requestcompliancerejected_data.length; i++) {
                    if ($scope.requestcompliancerejected_data[i].response_flag == "Y") {
                        $scope.rejectedtab_alert = "Y";
                        break;
                    }
                }
                for (var i = 0; i < $scope.requestcompliancecompleted_data.length; i++) {
                    if ($scope.requestcompliancecompleted_data[i].response_flag == "Y") {
                        $scope.completedtab_alert = "Y";
                        break;
                    }
                }
                for (var i = 0; i < $scope.requestcompliancetaggedlawyer_data.length; i++) {
                    if ($scope.requestcompliancetaggedlawyer_data[i].response_flag == "Y") {
                        $scope.taggedlawyertab_alert = "Y";
                        break;
                    }
                }

                

                if ($scope.requestcompliance_data == null) {
                    $scope.pendingCount = 0;
                }
                else {
                    $scope.pendingCount = $scope.requestcompliance_data.length;
                }
                if ($scope.requestcomplianceworkinprogress_data == null) {
                    $scope.workinprogressCount = 0;
                }
                else {
                    $scope.workinprogressCount = $scope.requestcomplianceworkinprogress_data.length;
                }
                $scope.requestcompliancetaggedlawyer_data = resp.data.requestcompliancetaggedlawyer_list;
                if ($scope.requestcompliancetaggedlawyer_data == null) {
                    $scope.taggedlawyerCount = 0;
                }
                else {
                    $scope.taggedlawyerCount = $scope.requestcompliancetaggedlawyer_data.length;
                }
                $scope.requestcompliancecompleted_data = resp.data.requestcompliancecompleted_list;
                if ($scope.requestcompliancecompleted_data == null) {
                    $scope.completedCount = 0;
                }
                else {
                    $scope.completedCount = $scope.requestcompliancecompleted_data.length;
                }
                $scope.requestcompliancerejected_data = resp.data.requestcompliancerejected_list;
                if ($scope.requestcompliancerejected_data == null) {
                    $scope.rejectedCount = 0;
                }
                else {
                    $scope.rejectedCount = $scope.requestcompliancerejected_data.length;
                }
            });
           
        }
 
   
        $scope.loadMorepending = function (pagecount) {
            lockUI();
            var Number = parseInt(pagecount);

            $scope.totalDisplayedpending += Number;
            unlockUI();
        };
        $scope.loadMorecompleted= function (pagecount) {
            lockUI();
            var Number = parseInt(pagecount);

            $scope.totalDisplayedcompleted += Number;
            unlockUI();
        };
        $scope.loadMorerejected = function (pagecount) {
            lockUI();
            var Number = parseInt(pagecount);

            $scope.totalDisplayedrejected += Number;
            unlockUI();
        };
        $scope.loadMoretaggedlawyer = function (pagecount) {
            lockUI();
            var Number = parseInt(pagecount);

            $scope.totalDisplayedtaggedlawyer += Number;
            unlockUI();
        };
        $scope.loadMoreworkinprogress= function (pagecount) {
            lockUI();
            var Number = parseInt(pagecount);

            $scope.totalDisplayedworkinprogress += Number;
            unlockUI();
        };
        $scope.pendingview360 = function (val) {
            $scope.requestcompliance_gid = val;
            $scope.requestcompliance_gid = localStorage.setItem('requestcompliance_gid', val);
            $location.url('app/LglTrnCompliancePending?lstab=Pending');
        }
        $scope.rejectedview360 = function (val) {
            $scope.requestcompliance_gid = val;
            $scope.requestcompliance_gid = localStorage.setItem('requestcompliance_gid', val);
            $location.url('app/LglTrnComplianceRejected?lstab=Rejected');
        }
        $scope.completedview360 = function (val) {
            $scope.requestcompliance_gid = val;
            $scope.requestcompliance_gid = localStorage.setItem('requestcompliance_gid', val);
            $location.url('app/LglTrnComplianceCompleted?lstab=Completed');
        }
        $scope.taggedlawyerview360 = function (val) {
            $scope.requestcompliance_gid = val;
            $scope.requestcompliance_gid = localStorage.setItem('requestcompliance_gid', val);
            $location.url('app/LglMstCompliance2TagLawyer?lstab=Tagged-Lawyer');
        }
        $scope.workinprogressview360 = function (val) {
            $scope.requestcompliance_gid = val;
            $scope.requestcompliance_gid = localStorage.setItem('requestcompliance_gid', val);
            $location.url('app/requestCompliance360?lstab=Work-InProgress');
        }
        $scope.showPopover = function (requestcompliance_gid, requestref_no) {
            var modalInstance = $modal.open({
                templateUrl: '/addcompliance.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    requestcompliance_gid : requestcompliance_gid
                }
                var url = 'api/requestCompliance/GetlawyerStatus';
                SocketService.getparams(url,params).then(function (resp) {
                    if(resp.data.status == true)
                    {
                        $scope.MdlLawyerSummary = resp.data.MdlLawyerSummary;
                        $scope.lawyeruser_name = resp.data.lawyeruser_name;
                        $scope.request_status = resp.data.request_status;

                        $scope.requestref_no = requestref_no;
                    
                    }
                    else {
                    }
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
       
    }
})();
