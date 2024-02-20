(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCADGroupAssignmentSummaryController', MstCADGroupAssignmentSummaryController);

        MstCADGroupAssignmentSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCADGroupAssignmentSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCADGroupAssignmentSummaryController';

        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        
        activate();

        function activate() { 
           var url = 'api/MstCADGroupAssignment/GetCADGroupAssignmentSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.cadgroupassignment_list = resp.data.group_list;
                unlockUI();
            }); 
        }

        //Maker Popup
        $scope.showmaker = function (cadgroupassign_gid) {
            lockUI();
            var modalInstance = $modal.open({
                templateUrl: '/showMaker.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    cadgroupassign_gid: cadgroupassign_gid
                }
                var url = 'api/MstCADGroupAssignment/GetCADGroupMaker';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.maker_name = resp.data.cadgroupmaker
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        //Checker Popup
        $scope.showchecker = function (cadgroupassign_gid) {
            lockUI();
            var modalInstance = $modal.open({
                templateUrl: '/showChecker.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    cadgroupassign_gid: cadgroupassign_gid
                }
                var url = 'api/MstCADGroupAssignment/GetCADGroupChecker';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.checker_name = resp.data.cadgroupchecker
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        //Approver Popup
        $scope.showapprover = function (cadgroupassign_gid) {
            lockUI();
            var modalInstance = $modal.open({
                templateUrl: '/showApprover.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    cadgroupassign_gid: cadgroupassign_gid
                }
                var url = 'api/MstCADGroupAssignment/GetCADGroupApprover';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.approver_name = resp.data.cadgroupapprover
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }


        //Add page
        $scope.addCADAssignment = function(){
            $location.url('app/MstCADGroupAssignmentAdd');
        }
        //Edit page
        $scope.edit = function(cadgroupassign_gid){
            $location.url('app/MstCADGroupAssignmentEdit?cadgroupassign_gid='+cadgroupassign_gid);
        }
        //Delete 
        $scope.delete = function (cadgroupassign_gid) {
            var params = {
                cadgroupassign_gid: cadgroupassign_gid
           } 
           
                   SweetAlert.swal({
                       title: 'Are you sure?',
                       text: 'Do You Want To Delete the Record ?',
                       showCancelButton: true,
                       confirmButtonColor: '#DD6B55',
                       confirmButtonText: 'Yes, delete it!',
                       closeOnConfirm: false
                   }, function (isConfirm) {
                       if (isConfirm) {
                           lockUI();
                           var url = 'api/MstCADGroupAssignment/DeleteCADGroupAssignment';
                           SocketService.getparams(url, params).then(function (resp) {
                               unlockUI();
                               if (resp.data.status == true) {
                                   SweetAlert.swal('Deleted Successfully!');
                                   activate();
                               }
                               else {
                                   alert(resp.data.message, {
                                       status: 'warning',
                                       pos: 'top-center',
                                       timeout: 3000
                                   });
                                   activate();
                                   unlockUI;
                               }
                           });
                           }
                   });
        }
    }
})();