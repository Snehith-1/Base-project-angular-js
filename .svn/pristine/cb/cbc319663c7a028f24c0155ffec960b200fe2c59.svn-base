(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHRLoanRaiseRequestController', MstHRLoanRaiseRequestController);

    MstHRLoanRaiseRequestController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function MstHRLoanRaiseRequestController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHRLoanRaiseRequestController';

        activate();
        lockUI();
        function activate() {
            var url = 'api/MstHRLoanRequest/GetHRloanDetails';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.hrloanrequestSummary_list = resp.data.hrloanrequest;               
                unlockUI();
            });
            var url = 'api/MstHRLoanRequest/GetHRloanDetailscount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.lspendingrequests_count = resp.data.pendingrequests_count;
                $scope.lscompletedrequests_count = resp.data.completedrequests_count;
                $scope.lsrejectedrequests_count = resp.data.rejectedrequests_count;
                $scope.lswithdrawn_count = resp.data.withdrawn_count;               
                $scope.lstotalcount = resp.data.totalcount;
          }); 
        }

        $scope.PendingRequests = function () {
            $state.go('app.MstHRLoanRaiseRequest');
        }

        $scope.completed_Requests = function () {
            $state.go('app.MstHRLoanRaiseRequestCompleted');
        }

        $scope.Rejected_Requests = function(){
            $state.go('app.MstHRloanRaiseRequestRejected');
        }

        $scope.withdrawn_Requests = function () {
            $state.go('app.MstHRLoanRaiseRequestWithdrawn');
        } 

        $scope.editrequests = function (request_gid,employee_gid) {
            $location.url('app/MstHRLoanEditRequest?hash=' + cmnfunctionService.encryptURL('request_gid=' + request_gid + '&employee_gid=' + employee_gid));
        }

        $scope.view360 = function (request_gid,employee_gid) {
            $location.url('app/MstHRLoanEditRequest360?hash=' + cmnfunctionService.encryptURL('request_gid=' + request_gid + '&employee_gid=' + employee_gid));
        }
        // $scope.editrequests = function () {
        //     $state.go('app.MstHRLoanEditRequest');
        // }
        // $scope.viewrequests = function (request_gid) {
        //     $location.url('app/MstHRLoanViewRequest?request_gid=' + request_gid  );
        // }
        // $scope.viewrequests = function () {           
        //     $state.go('app.MstHRLoanViewRequest');
        // }
      
        $scope.addrequest = function () {
            $state.go('app.MstHRLoanAddRequest');

        }

        $scope.withdraw = function (request_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/withdraw.html',
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
                var params = {
                    request_gid: request_gid
                }
                var url = 'api/MstHRLoanRequest/EditHRLoanRequest';
                SocketService.getparams(url, params).then(function (resp) { 
                    $scope.lsrequest_refno = resp.data.request_refno;
                    unlockUI();
                });
                $scope.submit_withdraw = function () {

                    var params = {
                        request_gid: request_gid,
                        withdraw_remarks: $scope.txtwithdraw_remarks,                       
                    }
                    var url = 'api/MstHRLoanRequest/PostHrLoanwithdrawUpdate';
                    lockUI();
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
                            activate();
                        }
                    });
                }
                
            }
        }

    }
})();
