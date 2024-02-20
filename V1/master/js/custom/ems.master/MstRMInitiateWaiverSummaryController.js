(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstRMInitiateWaiverSummaryController', MstRMInitiateWaiverSummaryController);

    MstRMInitiateWaiverSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstRMInitiateWaiverSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstRMInitiateWaiverSummaryController';
        var application_gid = $location.search().application_gid;
        $scope.application_gid = application_gid;
        activate();
        lockUI();
        function activate() {
            var params =
            {
                application_gid: $scope.application_gid
            }
            var url = 'api/MstRMPostCCWaiver/GetSanctionWaiverSummary';
            SocketService.getparams(url,params).then(function (resp) {
                unlockUI();
                $scope.rmpostccsanctionwaiver_list = resp.data.rmpostccwaiver_list;
            });

            var url = 'api/MstRMPostCCWaiver/GetLANWaiverSummary';
            SocketService.getparams(url,params).then(function (resp) {
                unlockUI();
                $scope.rmpostcclanwaiver_list = resp.data.rmpostccwaiver_list;
            });

        }

        //Add

        $scope.addsanction = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addsanction.html',
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
                $scope.submit = function () {
                    var params = {
                        sanctionwaiver_code: $scope.txtsanctionwaiver_code,
                        sanctionwaiver_name: $scope.txtsanctionwaiver_name,
                        description: $scope.txtdescription
                    }
                    lockUI();
                    var url = 'api/MstSanctionWaiver/PostSanctionWaiver';
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                        }
                    });
                }
            }
        }

        // Showoverpopup

        $scope.description = function (description) {
            var modalInstance = $modal.open({
                templateUrl: '/description.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.description = description;
                $scope.back = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.waiver_view = function (rmpostccwaiver_gid) {
            $location.url('app/MstRMInitiateWaiverView?application_gid=' + application_gid + '&rmpostccwaiver_gid=' + rmpostccwaiver_gid);
        }

        $scope.edit_waiver = function (rmpostccwaiver_gid) {
            $location.url('app/MstEditWaiver?application_gid=' + application_gid + '&rmpostccwaiver_gid=' + rmpostccwaiver_gid);
        }
       
        $scope.waiver_create = function () {
            $location.url('app/MstAddWaiver?application_gid=' + application_gid);
        }

        $scope.waiver_delete = function (rmpostccwaiver_gid) {
            var params = {
                rmpostccwaiver_gid: rmpostccwaiver_gid
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
                   var url = 'api/MstRMPostCCWaiver/DeleteRMPostCCWaiver';
                   SocketService.getparams(url, params).then(function (resp) {
                       if (resp.data.status == true) {
                           activate();
                       }
                       else {
                           Notify.alert('Error Occurred While Deleting Waiver!', {
                               status: 'warning',
                               pos: 'top-center',
                               timeout: 3000
                           });
                           activate();
                       }
                   }); 
                   SweetAlert.swal('Deleted Successfully!');
               }

           });
       };

        $scope.Back = function () {
            $state.go('app.MstPostCcActivitiesRMView');
        }
       
      
    }
})();