(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstGroupDocumentController', MstGroupDocumentController);

    MstGroupDocumentController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService'];

    function MstGroupDocumentController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstGroupDocumentController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() {
            var url = 'api/MstApplication360/GetGroupDocument';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.groupdocument_data = resp.data.application_list;
                $scope.document_list = resp.data.document_list;
                $scope.documentseverity_list = resp.data.documentseverity_list;
                unlockUI();
            });
        }


        $scope.addgroupdocument = function () {
            $state.go('app.MstGroupDocumentAdd');
        }
      
        $scope.editGroupDocument = function (groupdocument_gid) {
            $location.url('app/MstGroupDocumentEdit?lsgroupdocument_gid=' + groupdocument_gid);
        }

        $scope.viewGroupDocument = function (groupdocument_gid) {
            $location.url('app/MstGroupDocumentView?lsgroupdocument_gid=' + groupdocument_gid);
        }
     
        $scope.Status_update = function (groupdocument_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusGroupDocument.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    groupdocument_gid: groupdocument_gid
                }
                var url = 'api/MstApplication360/EditGroupDocument';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.groupdocument_gid = resp.data.groupdocument_gid
                    $scope.groupdocument_name = resp.data.groupdocument_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        groupdocument_gid: groupdocument_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MstApplication360/InactiveGroupDocument';
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
                    groupdocument_gid: groupdocument_gid
                }

                var url = 'api/MstApplication360/GroupDocumentInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.groupdocumentinactivelog_data = resp.data.application_list;
                    unlockUI();
                });

            }
        }

        $scope.delete = function (groupdocument_gid) {
            var params = {
                groupdocument_gid: groupdocument_gid
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
                            var url = 'api/MstApplication360/DeleteGroupDocument';
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

        $scope.excelreport = function () {
            lockUI();
            var url = 'api/MstApplicationReport/ExportMstGroupReport';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);

                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'warning')

                }

            });
        }
    }
})();

