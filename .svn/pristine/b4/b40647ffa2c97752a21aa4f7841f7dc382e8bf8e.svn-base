(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCcScheduledMeetingDtlViewController', MstCcScheduledMeetingDtlViewController);

        MstCcScheduledMeetingDtlViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', '$sce','SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCcScheduledMeetingDtlViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http,$sce,SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCcScheduledMeetingDtlViewController';
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;        
        activate();
        function activate() {
            $scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };  
            var application_gid = $location.search().application_gid;
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
                                                                                            
                SocketService.getparams(url, params).then(function (resp) {                
                    $scope.ccmember_list = resp.data.ccmember_list;
                    $scope.otheruser_list = resp.data.otheruser_list;  
                });
                
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
                var url = 'api/MstCC/ViewCCRemarks';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.lblremarks = resp.data.remarks;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }
        
        $scope.Back = function () {          
            if (lspage == 'CCCompletedView') {
                $location.url('app/MstCCCompletedSummary');
            }
            else {
               
            }  
        }
      
    }
})();
