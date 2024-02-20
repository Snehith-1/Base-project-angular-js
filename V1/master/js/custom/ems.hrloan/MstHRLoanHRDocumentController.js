(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHRLoanHRDocumentController', MstHRLoanHRDocumentController);

        MstHRLoanHRDocumentController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function MstHRLoanHRDocumentController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHRLoanHRDocumentController';

        activate();

        function activate() { 
            var url = 'api/MstHRLoanHRDocument/GetHRDocument';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.hrdocument_data = resp.data.hrloanhrdocument_list;
                $scope.document_list = resp.data.document_list;
                $scope.hrdocumentseverity_list = resp.data.hrdocumentseverity_list;
                unlockUI();
            });
        }
        $scope.addhrdocument = function () {
            $state.go('app.MstHRLoanHRDocumentAdd');
        }
       

        $scope.viewhrdocument = function (hrdocument_gid) {
            $location.url('app/MstHRLoanHRDocumentView?hash=' + cmnfunctionService.encryptURL('lshrdocument_gid=' + hrdocument_gid));
        }

        $scope.edithrdocument = function (hrdocument_gid) {
            $location.url('app/MstHRLoanHRDocumentEdit?hash=' + cmnfunctionService.encryptURL('lshrdocument_gid=' + hrdocument_gid));
        }
       
        $scope.Status_update = function (hrdocument_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statushrdocument.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    hrdocument_gid: hrdocument_gid
                }
                var url = 'api/MstHRLoanHRDocument/EditHRDocument';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.hrdocument_gid = resp.data.hrdocument_gid
                    $scope.hrdocument_name = resp.data.hrdocument_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        hrdocument_gid: hrdocument_gid,
                        hrdocument_name: $scope.hrdocument_name,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MstHRLoanHRDocument/InactiveHRDocument';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        activate();
                    });

                    $modalInstance.close('closed');

                }

                var param = {
                    hrdocument_gid: hrdocument_gid
                }

                var url = 'api/MstHRLoanHRDocument/InactiveHRDocumentHistory';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.hrdocumentinactivelog_data = resp.data.hrdocumentinactivehistory_list;
                    unlockUI();
                });

            }
        }

        $scope.delete = function (hrdocument_gid) {
            var params = {
                hrdocument_gid: hrdocument_gid
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
                            var url = 'api/MstHRLoanHRDocument/DeleteHRDocument';
                            SocketService.getparams(url, params).then(function (resp) {
                                unlockUI();
                                if (resp.data.status == true) {
                                    SweetAlert.swal('Deleted Successfully!');
                                    activate();
                                }
                                else {
                                    Notify.alert(resp.data.message, {
                                        status: 'warning',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    activate();
                                    unlockUI();
                                }
                            });
                            }
                    });
        }
        }
})();

