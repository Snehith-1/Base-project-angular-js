(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmMstCheckpointGroupAddController', AtmMstCheckpointGroupAddController);

    AtmMstCheckpointGroupAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function AtmMstCheckpointGroupAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmMstCheckpointGroupAddController';

        activate();

        function activate() {
            var url = 'api/AtmMstCheckpointGroup/GetCheckpointGroup';
             lockUI();
             SocketService.get(url).then(function (resp) {
                 $scope.checkpointgroup_data = resp.data.checkpointgroup_list;
                 unlockUI();
             });


            //var url = 'api/AtmMstCheckpointGroup/GetCheckpointGroup';
            //SocketService.get(url).then(function (resp) {
            //    unlockUI()
            //    $scope.checkpointgroup_data = resp.data.checkpointgroup_list
            //    angular.forEach($scope.checkpointgroup_data, function (value, key) {
            //        var params = {
            //            checkpointgroup_gid: value.checkpointgroup_gid
            //        };

            //        var url = 'api/AtmMstCheckpointGroup/GetCheckpointAdd';
            //        SocketService.getparams(url, params).then(function (resp) {
            //            value.checkpointgroupadd_list = resp.data.checkpointgroupadd_list;
            //            value.expand = false;

            //        });


                    //var url = 'api/AtmMstCheckpointGroup/GetChecklistMasterAdd';
                    //SocketService.getparams(url, params).then(function (resp) {
                    //    value.checklistmasteradd_list = resp.data.checklistmasteradd_list;
                    //    value.expand = false;

                    //});
            //    });
            //});

        }


        $scope.createchecklist = function (val) {
            $location.url('app/AtmMstCheckpointAddSummary?hash=' + cmnfunctionService.encryptURL('checkpointgroup_gid=' + val))
        }

        //$scope.createchecklist = function (val) {
        //    $location.url('app/AtmMstCheckpointAdd?checkpointgroup_gid=' + val)
        //}

        $scope.edit = function (val1) {
            $location.url('app/AtmMstCheckpointEdit?checkpointgroupadd_gid=' + val1)
        }

        $scope.view = function (val2) {
            $location.url('app/AtmMstCheckpointView?checkpointgroupadd_gid=' + val2)
        }


        $scope.addCheckpointGroup = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addCheckpointGroup.html',
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
                        checkpointgroup_name: $scope.txtcheckpointgroup_name,
                        checkpointgroup_code: $scope.txtcheckpointgroup_code,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/AtmMstCheckpointGroup/CreateCheckpointGroup';
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

            }
        }

        $scope.editCheckpointGroup = function (checkpointgroup_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editCheckpointGroup.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    checkpointgroup_gid: checkpointgroup_gid
                }
                var url = 'api/AtmMstCheckpointGroup/EditCheckpointGroup';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditcheckpointgroup_code = resp.data.checkpointgroup_code;
                    $scope.txteditcheckpointgroup_name = resp.data.checkpointgroup_name;
                    $scope.checkpointgroup_gid = resp.data.checkpointgroup_gid;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/AtmMstCheckpointGroup/UpdateCheckpointGroup';
                    var params = {
                        checkpointgroup_code: $scope.txteditcheckpointgroup_code,
                        checkpointgroup_name: $scope.txteditcheckpointgroup_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        checkpointgroup_gid: $scope.checkpointgroup_gid
                    }
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();

                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                    });
                    $modalInstance.close('closed');
                }

            }
        }

        $scope.Status_update = function (checkpointgroup_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusCheckpointGroup.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    checkpointgroup_gid: checkpointgroup_gid
                }
                var url = 'api/AtmMstCheckpointGroup/EditCheckpointGroup';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.checkpointgroup_gid = resp.data.checkpointgroup_gid
                    $scope.txtcheckpointgroup_name = resp.data.checkpointgroup_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        checkpointgroup_name: $scope.txtcheckpointgroup_name,
                        checkpointgroup_gid: $scope.checkpointgroup_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/AtmMstCheckpointGroup/InactiveCheckpointGroup';
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
                    checkpointgroup_gid: checkpointgroup_gid
                }

                var url = 'api/AtmMstCheckpointGroup/CheckpointGroupInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.checkpointgroupinactivelog_data = resp.data.checkpointgroup_list;
                    unlockUI();
                });
            }
        }


        $scope.delete = function (checkpointgroup_gid) {
            var params = {
                checkpointgroup_gid: checkpointgroup_gid
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
                    var url = 'api/AtmMstCheckpointGroup/DeleteCheckpointGroup';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Check Point Group !!!', {
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


        $scope.checkpointintent = function (checkpointgroupadd_gid, checkpoint_intent) {
            var modalInstance = $modal.open({
                templateUrl: '/checkpointintent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    checkpointgroupadd_gid: checkpointgroupadd_gid
                }
                lockUI();
                var url = 'api/AtmMstCheckpointGroup/GetCheckpointIntent';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtcheckpointintent = resp.data.checkpoint_intent;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.description = function (checkpointgroupadd_gid, checkpoint_description) {
            var modalInstance = $modal.open({
                templateUrl: '/checkpointdescription.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                'use strict';

                angular
                    .module('angle')
                var params = {
                    checkpointgroupadd_gid: checkpointgroupadd_gid
                }
                lockUI();
                var url = 'api/AtmMstCheckpointGroup/GetCheckpointDescription';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtcheckpointdescription = resp.data.checkpoint_description;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.notetoauditor = function (checkpointgroupadd_gid, noteto_auditor) {
            var modalInstance = $modal.open({
                templateUrl: '/notetoauditor.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    checkpointgroupadd_gid: checkpointgroupadd_gid
                }
                lockUI();
                var url = 'api/AtmMstCheckpointGroup/GetCheckpointNotestoAuditor';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtnotetoauditor = resp.data.noteto_auditor;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }



        $scope.deletecheckpoint = function (checkpointgroupadd_gid) {
            var params = {
                checkpointgroupadd_gid: checkpointgroupadd_gid
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
                    var url = 'api/AtmMstCheckpointGroup/DeleteCheckpointAdd';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Check Point Group !!!', {
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



        $scope.importexcel = function (checkpointgroup_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/importexcel.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {


                var params = {
                    checkpointgroup_gid: checkpointgroup_gid,
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.downloadtemplate_importexcel = function () {
                   
                    var Templateurl = apiManage.GetCommonData['TemplatePath'].Path;
                    var filename = "\ImportExcelCheckpoint.xlsx";
                    //var phyPath = resp.data.file_path;
                    var phyPath = Templateurl + filename;
                    var relPath = phyPath.split("EMS");
                    var relpath1 = relPath[1].replace("\\", "/");
                    var prefix = window.location.protocol + "//";
                    var hosts = window.location.host;
                    var str = prefix.concat(hosts, relpath1);
                    var link = document.createElement("a");
                    var name = filename.split('.');
                    link.download = name[0];
                    link.href = str;
                    link.click();


                }

                $scope.excelupload = function (val, val1, name) {

                    var fileInput = document.getElementById('fileimport');
                    var filePath = fileInput.value;

                    $scope.fileinputvalue = filePath;

                    // Allowing file type
                    var allowedExtensions = /(\.xls|\.xlsx|\.csv)$/i;

                    if (!allowedExtensions.exec(filePath)) {
                        Notify.alert('File Format Not Supported!', 'warning')
                        $modalInstance.close('closed');
                        //fileInput.value = '';
                    }
                    else if (filePath.includes("ImportExcelCheckpoint") == false) {
                        Notify.alert('File Name / Template Not Supported!', 'warning')
                        $modalInstance.close('closed');
                    }
                    else {
                        var item = {
                            name: val[0].name,
                            file: val[0]
                        };
                        var frm = new FormData();
                        frm.append('fileupload', item.file);
                        frm.append('file_name', item.name);
                        frm.append('checkpointgroup_gid', checkpointgroup_gid);
                        $scope.uploadfrm = frm;
                    }
                }

                $scope.uploadexcel = function () {

                    if ($scope.fileinputvalue == '' || $scope.fileinputvalue == undefined || $scope.fileinputvalue == null) {
                        Notify.alert('Kindly Select the Excel file', 'warning')
                    }
                    else {
                        var url = 'api/AtmMstCheckpointGroup/ImportExcelCheckpoint';
                        lockUI();
                        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                            if (resp.data.status == true) {
                                activate();
                                unlockUI();
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $state.go('app.AtmMstCheckpointGroupAdd');

                                $modalInstance.close('closed');
                            }
                            else {
                                unlockUI();
                                Notify.alert(resp.data.message, {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $modalInstance.close('closed');
                            }
                            $("#fileimport").val('');
                        });
                    }

                }

                $scope.uploadexcelcancel = function () {
                    $("#fileimport").val('');
                };
            }
        }




    }
})();
