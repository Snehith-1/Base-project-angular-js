(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstIndividualDocumentController', MstIndividualDocumentController);

    MstIndividualDocumentController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService'];

    function MstIndividualDocumentController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstIndividualDocumentController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() {
            var url = 'api/MstApplication360/GetIndividualDocument';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.individualdocument_data = resp.data.application_list;
                $scope.document_list = resp.data.document_list;
                $scope.severity_list = resp.data.severity_list;
                unlockUI();
            });
        }

        $scope.addIndividualDocument = function () {
            $state.go('app.MstIndividualDocumentAdd');
        }

        //$scope.editIndividualDocument = function (individualdocument_gid){
        //    $location.url('app/MstIndividualDocumentEdit?lsindividualdocument_gid=' + individualdocument_gid);
        //}

        $scope.editIndividualDocument = function (individualdocument_gid) {
            $location.url('app/MstIndividualDocumentEdit?lsindividualdocument_gid=' + individualdocument_gid);
        }

        //$scope.viewIndividualDocument = function () {
        //    $state.go('app.MstIndividualDocumentView');
        //}

        $scope.viewIndividualDocument = function (individualdocument_gid) {
            $location.url('app/MstIndividualDocumentView?lsindividualdocument_gid=' + individualdocument_gid);
        }

        $scope.delete = function (individualdocument_gid) {
            var params = {
                individualdocument_gid: individualdocument_gid
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
                    var url = 'api/MstApplication360/DeleteIndividualDocument';
                    lockUI();
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
                        }
                    });
                }
            });
        }

        $scope.Status_update = function (individualdocument_gid) {
                var modalInstance = $modal.open({
                    templateUrl: '/statusIndividualDocument.html',
                    controller: ModalInstanceCtrl,
                    backdrop: 'static',
                    keyboard: false,
                    size: 'md'
                });
                ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                function ModalInstanceCtrl($scope, $modalInstance) {

                    var params = {
                        individualdocument_gid: individualdocument_gid
                    }
                    var url = 'api/MstApplication360/EditIndividualDocument';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.individualdocument_gid = resp.data.individualdocument_gid
                        $scope.individualdocument_name = resp.data.individualdocument_name;
                        $scope.rbo_status = resp.data.Status;
                    });

                    $scope.ok = function () {
                        $modalInstance.close('closed');
                    };
                    $scope.update_status = function () {

                        var params = {
                            individualdocument_gid: individualdocument_gid,
                            remarks: $scope.txtremarks,
                            rbo_status: $scope.rbo_status

                        }
                        var url = 'api/MstApplication360/InactiveIndividualDocument';
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
                        individualdocument_gid: individualdocument_gid
                    }

                    var url = 'api/MstApplication360/InactiveIndividualDocumentHistory';
                    lockUI();
                    SocketService.getparams(url, param).then(function (resp) {
                        $scope.individualdocumentinactivelog_data = resp.data.inactivehistory_list;
                        unlockUI();
                    });

                }
        }

        $scope.excelreport = function () {
            lockUI();
            var url = 'api/MstApplicationReport/ExportMstIndividualReport';
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

