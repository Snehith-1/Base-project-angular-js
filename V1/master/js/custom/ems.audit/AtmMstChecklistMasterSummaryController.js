(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmMstChecklistMasterSummaryController', AtmMstChecklistMasterSummaryController);

    AtmMstChecklistMasterSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$modal','cmnfunctionService'];

    function AtmMstChecklistMasterSummaryController($rootScope, $scope, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $modal, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmMstChecklistMasterSummaryController';

        activate();

        function activate() {


            var url = 'api/AtmMstChecklistMaster/GetChecklistMaster';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.checklistmaster_list = resp.data.checklistmaster_list;
                unlockUI();
            });

            //var url = 'api/AtmMstChecklistMaster/GetChecklistMaster';
            //SocketService.get(url).then(function (resp) {
            //    unlockUI()
            //    $scope.checklistmaster_list = resp.data.checklistmaster_list
            //    //angular.forEach($scope.checklistmaster_list, function (value, key) {
            //        var params = {
            //            checklistmaster_gid: value.checklistmaster_gid
            //        };

                   
            //    });

        }

        $scope.addchecklist = function () {
            $state.go('app.AtmMstChecklistMasterAdd');
        }     
        $scope.Edit = function (val1,val2) {
            $location.url('app/AtmMstChecklistMasterEdit?hash=' + cmnfunctionService.encryptURL('checklistmaster_gid=' + val1 + '&checkpointgroup_gid=' + val2))
        }
        $scope.createchecklist = function (val) {
            $location.url('app/AtmMstCheckpointSummary?checklistmaster_gid=' + val)
        }
       
        $scope.deletechecklist = function (checklistmasteradd_gid) {
            var params = {
                checklistmasteradd_gid: checklistmasteradd_gid
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

                    var url = 'api/AtmMstChecklistMaster/DeleteChecklistMasterAdd';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting ChecklistMaster !!!', {
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

        $scope.delete = function (checklistmaster_gid) {
            var params = {
                checklistmaster_gid: checklistmaster_gid
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

                    var url = 'api/AtmMstChecklistMaster/DeleteChecklistMaster';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting ChecklistMaster !!!', {
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


        $scope.checkpointintent = function (checklistmasteradd_gid, checkpoint_intent) {
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
                    checklistmasteradd_gid: checklistmasteradd_gid
                }
                lockUI();
                var url = 'api/AtmMstChecklistMaster/GetChecklistMasterIntent';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtcheckpointintent = resp.data.checkpoint_intent;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.description = function (checklistmasteradd_gid, checkpoint_description) {
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
                    checklistmasteradd_gid: checklistmasteradd_gid
                }
                lockUI();
                var url = 'api/AtmMstChecklistMaster/GetChecklistMasterDescription';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtcheckpointdescription = resp.data.checkpoint_description;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.notetoauditor = function (checklistmasteradd_gid, noteto_auditor) {
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
                    checklistmasteradd_gid: checklistmasteradd_gid
                }
                lockUI();
                var url = 'api/AtmMstChecklistMaster/GetChecklistMasterAuditor';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtnotetoauditor = resp.data.noteto_auditor;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.auditname = function (checklistmaster_gid, audit_description) {
            var modalInstance = $modal.open({
                templateUrl: '/auditname.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    checklistmaster_gid: checklistmaster_gid
                }
                lockUI();
                var url = 'api/AtmMstChecklistMaster/GetChecklistMasterAuditorName';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtaudit_description = resp.data.audit_description;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.importexcel = function (checklistmaster_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/importexcel.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.checklistmaster_gid = checklistmaster_gid;

                var params = {
                    checklistmaster_gid: checklistmaster_gid
                }

                //var url = 'api/AtmMstChecklistMaster/GetExcelImportLog';
                //SocketService.getparams(url, params).then(function (resp) {
                //    $scope.excelimport_List = resp.data.excelimport_List;
                //});



                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.downloadtemplate_importexcel = function () {                    
                    var Templateurl = apiManage.GetCommonData['TemplatePath'].Path;
                    var filename = "\ImportExcelChecklist.xlsx";
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
                    var checklistmaster_gid = $scope.checklistmaster_gid;

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
                    else if (filePath.includes("ImportExcelChecklist") == false) {
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
                        frm.append('checklistmaster_gid', checklistmaster_gid);
                        $scope.uploadfrm = frm;
                    }
                }


                $scope.uploadexcel = function () {

                    if ($scope.fileinputvalue == '' || $scope.fileinputvalue == undefined || $scope.fileinputvalue == null) {
                        Notify.alert('Kindly Select the Excel file', 'warning')
                    }
                    else {
                        var url = 'api/AtmMstChecklistMaster/ImportExcelChecklist';
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
