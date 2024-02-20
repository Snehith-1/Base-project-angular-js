(function () {
    'use strict';

    angular
        .module('angle')
        .controller('viewServicecontroller', viewServicecontroller);

    viewServicecontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function viewServicecontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'viewServicecontroller';

        activate();

        function activate() {
            lockUI();
            var url = 'api/viewServiceTicket/viewserviceticket';
            
            SocketService.get(url).then(function (resp) {
               
                $scope.viewservice_list = resp.data.viewservice_list;
                $scope.leadstage_name = resp.data.leadstage_name;
                $scope.response_new = resp.data.response_new;
                unlockUI();
            });

        }
        var params = {
            complaint_gid: $scope.complaint_gid
        }

        var url = 'api/viewServiceTicket/responselogdetails_view';
        lockUI();
        SocketService.getparams(url, params).then(function (resp) {
          
            $scope.responselog_detailslist = resp.data.responselog_detailslist;
              unlockUI();
        });


        $scope.btn_incompleteclick = function (val) {
            var params = {
                complaint_gid: val
            };
            var url = 'api/viewServiceTicket/incompleteticket';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                location.reload();
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert('Ticket Incompleted ...!!', {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }
        $scope.btn_closeclick = function (complaint_gid) {
            var params = {
                complaint_gid: complaint_gid
            };
            var url = 'api/viewServiceTicket/closeticket';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                location.reload();
                if (resp.data.status == true) {

                    Notify.alert('Ticket Closed Successfully..!!', {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                else {
                    Notify.alert('Internal Error Occurred!', {
                        status: 'Warning',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
            });
        }


        $scope.btn_viewclick = function (complaint_gid) {

            $scope.complaint_gid = localStorage.setItem('complaint_gid', complaint_gid);
            console.log(complaint_gid);
            $state.go('app.viewTicketDetails');
        }

        $scope.btnviewresponselog = function (complaint_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/myModaledit.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


                $scope.complaint_gid = complaint_gid;
                var params = {
                    complaint_gid: $scope.complaint_gid
                }
                var url = 'api/viewServiceTicket/responselogdetails_view';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.responselog_detailslist = resp.data.responselog_detailslist;
                });
                $scope.responseclick = function (complaint_gid) {
                    var params = {
                        complaint_gid: $scope.complaint_gid,
                        response_description: $scope.txtresponse
                    }
                    var url = 'api/viewServiceTicket/response_logdetails';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        $('#txt_response').val('');

                        if (resp.data.status == true) {
                            var url = 'api/viewServiceTicket/responselogdetails_view';
                            lockUI();
                            SocketService.getparams(url, params).then(function (resp) {
                                unlockUI();
                                $scope.responselog_detailslist = resp.data.responselog_detailslist;

                            });
                        }
                        else {
                            Notify.alert('Internal Error Occurred!', {
                                status: 'Warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                    });
                }
            }
        }
    }
})();
