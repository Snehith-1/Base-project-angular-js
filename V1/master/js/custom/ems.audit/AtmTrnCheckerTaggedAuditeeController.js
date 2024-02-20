(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmTrnCheckerTaggedAuditeeController', AtmTrnCheckerTaggedAuditeeController);

    AtmTrnCheckerTaggedAuditeeController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$filter', '$route', 'ngTableParams'];

    function AtmTrnCheckerTaggedAuditeeController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $filter, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmTrnCheckerTaggedAuditeeController';

        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;


        activate();
        function activate() {

            var url = 'api/AtmTrnMyAuditTaskAuditee/GetMyAuditTaskAuditeeMaker';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.myaudittaskauditee_list = resp.data.myaudittaskauditee_list;
                $scope.employee_gid = resp.data.employee_gid;
                unlockUI();

            });

            var url = 'api/AtmTrnMyAuditTaskAuditee/GetMyAuditTaskCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.auditsonhold_count = resp.data.auditsonhold_count;
                $scope.closedaudit_count = resp.data.closedaudit_count;
                $scope.openaudit_count = resp.data.openaudit_count;
                $scope.taggedsample_count = resp.data.taggedsample_count;

            });


        }

        $scope.auditee_maker = function () {
            $location.url('app/AtmTrnMyAuditTaskAuditeeSummary')
        }

        $scope.auditee_checker = function () {
            $location.url('app/AtmTrnMyAuditeeCheckerSummary')
        }

        $scope.open_audit = function () {
            $location.url('app/AtmTrnMyAuditeeCheckerSummary')
        }

        $scope.pending_approval = function () {
            $location.url('app/AtmTrnCheckerPendingApproval')
        }

        $scope.hold_audit = function () {
            $location.url('app/AtmTrnCheckerHoldAuditee')
        }

        $scope.closed_audit = function () {
            $location.url('app/AtmTrnCheckerClosedAuditee')
        }

        $scope.tagged_items = function () {
            $location.url('app/AtmTrnCheckerTaggedAuditee')
        }

        $scope.completed_audit = function () {
            $location.url('app/AtmTrnCheckerCompletedAuditee')
        }


        $scope.viewtask = function (val, val1) {
            $location.url('app/AtmTrnMyAuditTaskAuditeeView?auditcreation_gid=' + val + '&sampleimport_gid=' + val1 + '&lspage=auditeecheckertagged')

        }

        $scope.auditraisequery = function (val1) {
            $location.url('app/AtmTrnAuditRaiseQuery?auditcreation_gid=' + val1 + '&lspage=auditeecheckertagged')
        }

        $scope.raisequery = function (auditcreation_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/raisequery.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    auditcreation_gid: auditcreation_gid
                }
                var url = 'api/AtmTrnMyAuditTaskAuditee/EditMyAuditTaskAuidtee';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.auditcreation_gid = resp.data.auditcreation_gid

                });


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                var url = 'api/SystemMaster/GetEmployeelist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employeelist;
                    unlockUI();
                });



                $scope.submit = function () {

                    var params = {
                        auditcreation_gid: $scope.auditcreation_gid,
                        employe: $scope.cboemployee_name,
                        description: $scope.txtdescription,

                    }

                    var url = 'api/AtmTrnMyAuditTaskAuditee/PostAuditeeRaiseQuery';
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

                        }
                    });

                    $modalInstance.close('closed');

                }

            }
        }

        $scope.importexcel = function (auditcreation_gid) {
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
                    auditcreation_gid: auditcreation_gid,
                }

                var url = 'api/AtmTrnSampling/GetSample';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.sample_list = resp.data.sample_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.downloadtemplate_importexcel = function () {
                    var Templateurl = apiManage.GetCommonData['TemplatePath'].Path;
                    var filename = "\ImportExcelSample.xlsx";
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
                    else if (filePath.includes("ImportExcelSample") == false) {
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
                        frm.append('auditcreation_gid', auditcreation_gid);
                        $scope.uploadfrm = frm;
                    }
                }

                $scope.uploadexcel = function () {

                    if ($scope.fileinputvalue == '' || $scope.fileinputvalue == undefined || $scope.fileinputvalue == null) {
                        Notify.alert('Kindly Select the Excel file', 'warning')
                    }
                    else {
                        var url = 'api/AtmTrnSampling/ImportExcelSample';
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
