(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmMstCheckpointAddController', AtmMstCheckpointAddController);

    AtmMstCheckpointAddController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$filter', '$route', 'ngTableParams'];

    function AtmMstCheckpointAddController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $filter, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmMstCheckpointAddController';
        $scope.checkpointgroup_gid = $location.search().checkpointgroup_gid;
        var checkpointgroup_gid = $location.search().checkpointgroup_gid;
        var checkpointgroup_gid = $scope.checkpointgroup_gid;
        activate();

        function activate() {


            var url = 'api/AtmMstPositiveConfirmity/GetPositiveConfirmity';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.positiveconfirmity_data = resp.data.positiveconfirmity_list;
                unlockUI();
            });


            var url = 'api/AtmMstRiskcategory/GetRiskcategory';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.riskcategory_list = resp.data.riskcategory_list;
                unlockUI();
            });

            var params = {
                checkpointgroup_gid: checkpointgroup_gid
            }
        }

        $scope.changevalue = function () {
            var val1 = 0;
            var val2 = 0;
            var val3 = 0;
            var val4 = 0;
            var val5 = 0;
            if ($scope.txtna_score != undefined || $scope.txtna_score != null) {
                val1 = $scope.txtna_score;
            }

            if ($scope.txtno_score != undefined || $scope.txtno_score != null) {
                val2 = $scope.txtno_score;
            }

            if ($scope.txtyes_score != undefined || $scope.txtyes_score != null) {
                val3 = $scope.txtyes_score;
            }

            if ($scope.txtpartial_score != undefined || $scope.txtpartial_score != null) {
                val4 = $scope.txtpartial_score;
            }


            val5 = val1 + val2 + val3 + val4;


            $scope.txt_totalscore = parseFloat(val5.toFixed(2));

        }
        $scope.submitAdd = function () {

            var lsriskcategory_gid = '';
            var lsriskcategory_name = '';
            if ($scope.cboriskcategory != undefined || $scope.cboriskcategory != null) {
                lsriskcategory_gid = $scope.cboriskcategory.riskcategory_gid;
                lsriskcategory_name = $scope.cboriskcategory.riskcategory_name;
            }

            var lspositiveconfirmity_gid = '';
            var lspositiveconfirmity_name = '';
            if ($scope.cbopositiveconfirmity != undefined || $scope.cbopositiveconfirmity != null) {
                lspositiveconfirmity_gid = $scope.cbopositiveconfirmity.positiveconfirmity_gid;
                lspositiveconfirmity_name = $scope.cbopositiveconfirmity.positiveconfirmity_name;
            }
            var params = {
                positiveconfirmity_gid: lspositiveconfirmity_gid,
                positiveconfirmity_name: lspositiveconfirmity_name,
                riskcategory_gid: lsriskcategory_gid,
                riskcategory_name: lsriskcategory_name,
                checkpointgroup_gid: checkpointgroup_gid,
                checkpoint_intent: $scope.txtcheckpoint_intent,
                checkpoint_description: $scope.txtcheckpoint_description,
                noteto_auditor: $scope.txtnoteto_auditor,
                yes_score: $scope.txtyes_score,
                yes_disposition: $scope.txtyes_disposition,
                no_score: $scope.txtno_score,
                no_disposition: $scope.txtno_disposition,
                partial_score: $scope.txtpartial_score,
                partial_disposition: $scope.txtpartial_disposition,
                na_score: $scope.txtna_score,
                na_disposition: $scope.txtna_disposition,
                total_score: $scope.txt_totalscore,

            }

            var url = 'api/AtmMstCheckpointGroup/PostCheckpointAdd';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {


                    $scope.cboriskcategory = '';
                    $scope.cbopositiveconfirmity = '';
                    $scope.txtcheckpoint_intent = '';
                    $scope.txtcheckpoint_description = '';
                    $scope.txtnoteto_auditor = '';
                    $scope.txtyes_score = '';
                    $scope.txtyes_disposition = '';
                    $scope.txtno_score = '';
                    $scope.txtno_disposition = '';
                    $scope.txtpartial_score = '';
                    $scope.txtpartial_disposition = '';
                    $scope.txtna_score = '';
                    $scope.txtna_disposition = '';
                    $scope.txt_totalscore = '';
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    //$state.go('app.AtmMstChecklistMasterAudit');
                    //activate();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }

        $scope.back = function (val) {
            $state.go('app.AtmMstCheckpointGroupAdd');
        }

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