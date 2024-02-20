(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstProgramController', MstProgramController);

    MstProgramController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService'];

    function MstProgramController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstProgramController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() {
            var url = 'api/MstApplication360/GetProgram';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.program_list = resp.data.application_list;
                unlockUI();
            });
        }
        //$scope.addprogram = function () {
        //    $location.url('app/MstProgramAdd')
        //}

        $scope.addprogram = function () {
            $location.url('app/MstProgramAdd');
        }
       
        $scope.editprogram = function (program_gid) {
            $location.url('app/MstProgramEdit?lsprogram_gid=' + program_gid);
        }

        $scope.viewprogram = function (program_gid) {
            $location.url('app/MstProgramView?lsprogram_gid=' + program_gid);
        }
        
       
        $scope.Status_update = function (program_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusprogram.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    program_gid: program_gid
                }
                var url = 'api/MstApplication360/EditProgram';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.program_gid = resp.data.program_gid
                    $scope.txtprogram = resp.data.program;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        program_gid: program_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MstApplication360/InactiveProgram';
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
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        activate();
                    });

                    $modalInstance.close('closed');

                }

                var param = {
                    program_gid: program_gid
                }

                var url = 'api/MstApplication360/ProgramInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.programinactivelog_list = resp.data.application_list;
                    unlockUI();
                });

            }
        }

        $scope.delete = function (program_gid) {
            var params = {
                program_gid: program_gid
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
                    var url = 'api/MstApplication360/DeleteProgram';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Program!', {
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

        $scope.ExportexcelAddprogram = function () {
            lockUI();
            var url = 'api/MstApplicationReport/ExportExcelAddprogram';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                    //var phyPath = resp.data.lspath;
                    //var relPath = phyPath.split("EMS");
                    //var relpath1 = relPath[1].replace("\\", "/");
                    //var hosts = window.location.host;
                    //var prefix = location.protocol + "//";
                    //var str = prefix.concat(hosts, relpath1);
                    //var link = document.createElement("a");
                    //var name = resp.data.lsname.split('.');
                    //link.download = name[0];
                    //var uri = str;
                    //link.href = uri;
                    //link.click();
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'warning')
                    
                }

            });
        }
    }
})();

