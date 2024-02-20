(function () {
    'use strict';

    angular
        .module('angle')
        .controller('segmentcontroller', segmentcontroller);

    segmentcontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function segmentcontroller($rootScope, $scope, $state, $modal, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'segmentcontroller';
        //console.log($scope.segment_name);
        activate();


        function activate() {

            var url = 'api/segment/segment';
            SocketService.get(url).then(function (resp) {
                $scope.segment = resp.data.segment_list;
                //console.log($scope.segment);
            });
        }        
              
              $scope.delete = function (segment_gid) {
                var params = {
                    segment_gid: segment_gid
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
                        var url = 'api/segment/segmentDelete';
                        SocketService.getparams(url, params).then(function (resp) {
                            if (resp.data.status == true) {
                                activate();
                            }
                            else {
                                Notify.alert('Error Occurred While Deleting Customer!', {
                                    status: 'warning',
                                    pos: 'top-right',
                                    timeout: 3000
                                });
                                activate();
                            }
                        });
                        SweetAlert.swal('Deleted Successfully!');
                    }

                });
            };
        
        $scope.popupsegment = function () {
            var modalInstance = $modal.open({
                templateUrl: '/myModalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {             
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.segmentSubmit = function () {
                    var params = {
                        segment_code: $scope.segment_code,
                        segment_name: $scope.segment_name
                    }                    
                    var url = 'api/segment/createSegment';
                   
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert('Vertical Added Successfully..!!', 'success')
                            activate();
                        
                          }
                        else {
                            Notify.alert('Error Occurred While Adding Vertical!', 'warning')
                            activate();
                        }
                    });
                    $state.go('app.segment');
                }
            }
        }

        $scope.edit = function (segment_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/myModaledit.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    segment_gid: segment_gid
                }
                var url = 'api/segment/Getsegmentupdate';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.verticalNameedit = resp.data.verticalNameedit;
                    $scope.descriptionedit = resp.data.descriptionedit;
                    $scope.segment_gid = resp.data.segment_gid;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.segmentUpdate = function () {

                    var params = {
                        verticalNameedit: $scope.verticalNameedit,
                        descriptionedit: $scope.descriptionedit,
                        segment_gid: $scope.segment_gid
                    }
                    var url = 'api/segment/segmentUpdate';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                            $modalInstance.close('closed');
                            Notify.alert('Vertical Updated Successfully..!!', 'success')

                        }
                        else {
                            Notify.alert('Error Occurred While Updating Vertical !', 'success')
                            activate();

                        }
                    });
                }
            }

        }
       }
})();