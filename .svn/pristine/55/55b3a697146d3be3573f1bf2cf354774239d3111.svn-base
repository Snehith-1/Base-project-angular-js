(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndMstQuestionBankMaster', FndMstQuestionBankMaster);

        FndMstQuestionBankMaster.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function FndMstQuestionBankMaster($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndMstQuestionBankMaster';
       // console.log('test');
        activate();

        function activate() {
            var url = 'api/FndQuestionnarieMaster/GetQuestionnarie';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.questionarie = resp.data.Questionnarie_list;
                unlockUI();
            });
        }
        $scope.addQuestionTitle = function () {
            $state.go('app.FndAddQuestion');
        }
        $scope.Back = function () {
            activate();
            $state.go('app.FndMstQuestionBankMaster');
        }

        $scope.edit = function (val) {
            localStorage.setItem('Questionnarie_gid', val);
            $state.go('app.FndEditQuestion');
        }
        $scope.delete = function (Questionnarie_gid) {
            var params = {
                Questionnarie_gid: Questionnarie_gid
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

                    var url = 'api/FndQuestionnarieMaster/DeleteQuestionnarie';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Questionnarie !!!', {
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

        $scope.showPopover = function (Questionnarie_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/showremarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    Questionnarie_gid: Questionnarie_gid
                }
                var url = 'api/FndQuestionnarieMaster/QuestionnarieEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditQuestionnarie_code = resp.data.Questionnarie_code;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.txteditQuestionnarie = resp.data.Questionnarie_name;
                    $scope.txteditremarks = resp.data.remarks;
                    $scope.Questionnarie_gid = resp.data.Questionnarie_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };



            }
        }

        //Divya

        $scope.importexcel = function () {
            var modalInstance = $modal.open({
                templateUrl: '/importexcel.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];

            function ModalInstanceCtrl($scope, $modalInstance) {
               

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.downloadtemplate_importexcel = function () {
                  
                    var Templateurl = apiManage.GetCommonData['TemplatePath'].Path;
                    var filename = "\ImportQuestionsMaster.xlsx";
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
                  
                    /* var filename = "ImportQuestionsMaster.xlsx";
                    //var phyPath = resp.data.file_path;
                    var phyPath = "E:\\Web\\EMS\\templates\\ImportQuestionsMaster.xlsx";
                    var relPath = phyPath.split("EMS");
                    var relpath1 = relPath[1].replace("\\", "/");
                    var hosts = window.location.host;
                    var prefix = "http://"
                    var str = prefix.concat(hosts, relpath1);
                    var link = document.createElement("a");
                    var name = filename.split('.');
                    link.download = name[0];
                    var uri = str;
                    link.href = uri;
                    link.click();*/
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
                    else if (filePath.includes("ImportQuestionsMaster") == false) {
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
                        // frm.append('questionnarieanswer_gid', questionnarieanswer_gid);
                        frm.append('project_flag', "RSK");
                        $scope.uploadfrm = frm;
                    }
                }

                $scope.uploadexcel = function () {

                    if ($scope.fileinputvalue == '' || $scope.fileinputvalue == undefined || $scope.fileinputvalue == null) {
                        Notify.alert('Kindly Select the Excel file', 'warning')
                    }
                    else {

                        var url = 'api/FndQuestionnarieMaster/ImportExcelSample';
                        lockUI();
                        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                            $modalInstance.close('closed');

                            if (resp.data.status == true) {
                                //  defaultdynamic();
                                unlockUI();
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                            activate();
                                //  $location.url('app/AtmTrnAudit360View?auditcreation_gid=' + val1 + '&checklistmaster_gid=' + val2 + '&sampleimport_gid=' + val3)
                               

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

        //Divya


        $scope.Status_update = function (Questionnarie_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusQuestionnarie.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                 var params = {
                    Questionnarie_gid: Questionnarie_gid
                }
                var url = 'api/FndQuestionnarieMaster/QuestionnarieEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditQuestionnarie_code = resp.data.Questionnarie_code;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.txteditQuestionnarie = resp.data.Questionnarie_name;
                    $scope.txteditremarks = resp.data.remarks;
                    $scope.Questionnarie_gid = resp.data.Questionnarie_gid;
                    $scope.rbo_status = resp.data.Status;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        Questionnarie_name: $scope.Questionnarie_name,
                        Questionnarie_gid: Questionnarie_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/FndQuestionnarieMaster/InactiveQuestionnarie';
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
                    Questionnarie_gid: Questionnarie_gid
                }

                var url = 'api/FndQuestionnarieMaster/QuestionnarieInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.statusQuestionnarieinactivelog_list = resp.data.Questionnarie_list;
                    unlockUI();
                });
            }
        }
    }
})();
